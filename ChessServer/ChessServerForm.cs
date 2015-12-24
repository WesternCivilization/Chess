using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Tools;

namespace Chess
{
    public partial class ChessServerForm : Form
    {

        private Database database;

        public ChessServerForm( Database database )
        {
            InitializeComponent();

            this.database = database;

            comboBoxIpBind.Items.AddRange( Connection.GetLocalIps().ToArray() );
            comboBoxIpBind.SelectedIndex = 0;
            
            State = ServerState.Stop;
        }

        private void buttonCopy_Click( object sender, EventArgs e )
        {
            Clipboard.SetText( ( comboBoxIpBind.SelectedItem as IPAddress ).ToString() );
        }

        private void buttonViewDatabase_Click( object sender, EventArgs e )
        {
            ViewDatabaseForm viewDatabaseForm = new ViewDatabaseForm( database );
            viewDatabaseForm.ShowDialog();
        }

        private bool disconnect = false;
        private void buttonBind_Click( object sender, EventArgs e )
        {
            if ( State == ServerState.Stop )
                new Thread( ListeningConnection ).Start( comboBoxIpBind.SelectedItem as IPAddress );
            else
                disconnect = true;
        }

        private enum ServerState { Binded, Stop }
        private ServerState state;
        private ServerState State
        {
            get { return state; }
            set
            {
                state = value;
                if ( state == ServerState.Binded )
                {
                    buttonBind.Text = "Stop";
                    comboBoxIpBind.Enabled = false;
                }
                else
                {
                    buttonBind.Text = "Bind";
                    comboBoxIpBind.Enabled = true;
                }
            }
        }

        private Socket socket;
        public static ManualResetEvent listeningLocker = new ManualResetEvent( false );
        public void ListeningConnection( object paramIp )
        {
            this.InvokeEx( () => State = ServerState.Binded );

            socket = Connection.CreateSocket();
            socket.Bind( new IPEndPoint( paramIp as IPAddress, Connection.PORT ) );
            socket.Listen( 100 );
            
            while ( !IsDisposed )
            {
                try
                {
                    listeningLocker.Reset();

                    using ( SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs() )
                    {
                        socketAsyncEventArgs.Completed += OnConnect;
                        socket.AcceptAsync( socketAsyncEventArgs );
                    }

                    listeningLocker.WaitOne( 5000 );

                    if ( disconnect )
                    {
                        disconnect = false;
                        break;
                    }
                }
                catch ( SocketException ex )
                {
                    MessageBox.Show( "Listening connection: " + ex.Message );
                    return;
                }
            }
            
            socket.Close();

            this.InvokeEx( () => State = ServerState.Stop );
        }

        private Dictionary<string, Socket> connections = new Dictionary<string, Socket>();

        private static ManualResetEvent connectLocker = new ManualResetEvent( false );
        public void OnConnect( object sender, SocketAsyncEventArgs e )
        {
            if ( e.AcceptSocket == null )
                return;

            try
            {
                while ( !IsDisposed && e.AcceptSocket.Connected )
                {
                    connectLocker.Reset();

                    using ( SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs() )
                    {
                        socketAsyncEventArgs.Completed += OnReceive;
                        socketAsyncEventArgs.AcceptSocket = e.AcceptSocket;
                        socketAsyncEventArgs.SetBuffer( new byte[ 1024 ], 0, 1024 );
                        e.AcceptSocket.ReceiveAsync( socketAsyncEventArgs );
                    }

                    connectLocker.WaitOne();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( "Connection: " + ex.Message );
            }
            
            e.AcceptSocket.Close();
        }

        public void OnReceive( object sender, SocketAsyncEventArgs e )
        {
            connectLocker.Set();

            try
            {
                if ( e.Buffer != null )
                {
                    Packet packet = Packet.FromBytes( e.Buffer );

                    switch ( packet.PacketType )
                    {
                        case Packet.Type.SignIn:
                            {
                                SignInData signInData = ( SignInData ) packet.Data;
                                RegistrationData registration = database.Find( signInData.Login );
                                if ( registration == null || registration.Password != signInData.Password )
                                    e.AcceptSocket.Send( Packet.SignInResultPacket( SignInResult.InvalidPasswordOrLogin ) );
                                else
                                {
                                    e.AcceptSocket.Send( Packet.SignInResultPacket( SignInResult.OK ) );
                                    this.InvokeEx( () => listBoxFreeClients.Items.Add( registration ) );
                                    this.InvokeEx( () => connections.Add( registration.Login, e.AcceptSocket ) );
                                }
                            }
                            break;

                        case Packet.Type.Registration:
                            {
                                RegistrationData registrationData = ( RegistrationData ) packet.Data;
                                if ( database.ContainsLogin( registrationData.Login ) )
                                    e.AcceptSocket.Send( Packet.RegistrationResultPacket( RegistrationResult.LoginAllreadyExist ) );
                                else
                                {
                                    e.AcceptSocket.Send( Packet.RegistrationResultPacket( RegistrationResult.OK ) );
                                    database.Registered.Add( registrationData );
                                }
                            }
                            break;
                        case Packet.Type.GetConnectedPlayers:
                            {
                                string login = ( string ) packet.Data;
                                List<RegistrationData> players = new List<RegistrationData>();
                                foreach ( RegistrationData reg in listBoxFreeClients.Items )
                                {
                                    if ( reg.Login != login )
                                        players.Add( reg );
                                }
                                e.AcceptSocket.Send( Packet.GiveConnectedPlayersPacket( players ) );
                            }
                            break;
                        case Packet.Type.StartGame:
                            {
                                StartGameData startGameData = ( StartGameData ) packet.Data;
                                Socket query = connections[ startGameData.LoginReply ];
                                query.Send( packet.ToBytes() );
                            }
                            break;
                        case Packet.Type.StartGameResult:
                            {
                                StartGameData startGameData = ( StartGameData ) packet.Data;
                                Socket reply = connections[ startGameData.LoginQuery ];
                                reply.Send( packet.ToBytes() );
                            }
                            break;
                        case Packet.Type.MoveChess:
                            {
                                ChessMoveData chessMoveData = ( ChessMoveData ) packet.Data;
                                Socket send = connections[ chessMoveData.LoginСontender ];
                                send.Send( packet.ToBytes() );
                            }
                            break;
                    }
                }
            }
            catch
            {
                e.AcceptSocket.Close();
                this.InvokeEx( () =>
                {
                    string removeItem = string.Empty;
                    foreach ( var item in connections )
                    {
                        if ( item.Value == e.AcceptSocket )
                        {
                            RegistrationData registration = database.Find( item.Key );
                            if ( listBoxFreeClients.Items.Contains( registration ) )
                            {
                                listBoxFreeClients.Items.Remove( registration );
                                removeItem = registration.Login;
                                break;
                            }
                        }
                    }

                    if ( removeItem.Length > 0 )
                        connections.Remove( removeItem );
                } );
            }
        }


    }
}
