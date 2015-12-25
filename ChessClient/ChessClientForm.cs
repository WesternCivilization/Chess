using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using Tools;

namespace Chess
{
    public partial class ChessClientForm : Form
    {
        public ChessClientForm()
        {
            InitializeComponent();

            connectPage = new ConnectPage();
            connectPage.buttonConnect.Click += OnButtonConnectClick;
            Controls.Add( connectPage );

            signInPage = new SignInPage();
            ClientSize = signInPage.ClientSize;
            signInPage.buttonSignIn.Click += OnClickSingIn;
            signInPage.buttonRegistration.Click += ( object sender, EventArgs e ) => Page = registrationPage;
            Controls.Add( signInPage );

            registrationPage = new RegistrationPage();
            registrationPage.buttonRegister.Click += OnClickRegistration;
            registrationPage.buttonBack.Click += OnRegister;
            Controls.Add( registrationPage );

            selectContenderPage = new SelectСontenderPage();
            selectContenderPage.buttonWait.Click += OnWaitButtonClick;
            selectContenderPage.buttonUpdate.Click += OnUpdateButtonClick;
            selectContenderPage.buttonConnect.Click += OnConnectButtonClick;
            Controls.Add( selectContenderPage );

            gamePage = new ChessGamePage( new Skin( "resources/skin/skin.xml" ) );
            gamePage.GameControl.Paint += OnPaint;
            gamePage.GameControl.MouseDown += OnMouseDown;
            gamePage.GameControl.MouseMove += OnMouseMove;
            gamePage.GameControl.MouseUp += OnMouseUp;
            Controls.Add( gamePage );
            LoadSelect();

            Page = connectPage;
        }

        #region Paging

        private ConnectPage connectPage;
        private SignInPage signInPage;
        private ChessGamePage gamePage;
        private RegistrationPage registrationPage;
        private SelectСontenderPage selectContenderPage;

        private UserControl currentPage;
        private UserControl Page
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                ClientSize = currentPage.ClientSize;
                foreach ( UserControl page in Controls )
                    page.Visible = ( page == currentPage );
            }
        }

        #endregion
        
        private Socket socket = Connection.CreateSocket();

        private void ChessClientForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            if ( gamePage.Game.GameState == Game.State.InTheGame )
                socket.Send( Packet.AbortGamePacket() );

            socket.Close();
        }

        private Player SelfPlayer
        {
            get
            {
                string selfLogin = signInPage.textBoxLogin.Text;
                if ( gamePage.Game.Player1.Name == selfLogin )
                    return gamePage.Game.Player1;
                else if ( gamePage.Game.Player2.Name == selfLogin )
                    return gamePage.Game.Player2;
                return null;
            }
        }
        private Player ContenderPlayer
        {
            get
            {
                return gamePage.Game.Player1 == SelfPlayer
                    ?   gamePage.Game.Player2
                    :   gamePage.Game.Player1;
            }
        }

        #region Game Control

        private ChessSelect select;
        private void LoadSelect()
        {
            select = new ChessSelect();
            select.DrawingHovered = new ChessSelect.CellDrawSelect(
                    new RectangleShape( Color.FromArgb( 50, Color.White ) )
                ,   new RectangleShape( Color.FromArgb( 50, Color.Black ) )
            );
        }
        
        private void OnPaint( object sender, PaintEventArgs e )
        {
            if ( gamePage.Game.GameState == Game.State.InTheGame )
            {
                e.Graphics.Draw( gamePage.Game.Desk );
                e.Graphics.Draw( gamePage.Game.Factory.ActiveChess );
                e.Graphics.Draw( select );
                if ( holdChess != null )
                    e.Graphics.Draw( holdChess );
            }
        }

        private bool IsMyChess( Chess chess )
        {
            return chess != null && SelfPlayer != null && chess.Color == SelfPlayer.Color;
        }
        private bool IsMyMove()
        {
            return gamePage.Game.Current == SelfPlayer;
        }
        private bool IsCheck
        {
            get { return gamePage.Game.IsCheck( GameColor.Black ) || gamePage.Game.IsCheck( GameColor.White ); }
        }
        private bool IsEndGame
        {
            get { return gamePage.Game.IsEndGame( GameColor.Black ) || gamePage.Game.IsEndGame( GameColor.White ); }
        }

        private Chess holdChess;
        private PointF holdDiff;

        public void OnMouseDown( object sender, MouseEventArgs e )
        {
            if ( e.Button != MouseButtons.Left )
                return;

            DeskCell cell = gamePage.Game.Desk.GetCellByMouse( e.Location );
            if ( cell != null )
            {
                Chess chess = cell.Chess;
                if ( IsMyChess( chess ) && IsMyMove() )
                {
                    holdChess = chess;
                    holdDiff = new PointF( e.X - chess.Sprite.Position.X, e.Y - chess.Sprite.Position.Y );
                }
                gamePage.GameControl.Repaint();
            }
        }

        public void OnMouseMove( object sender, MouseEventArgs e )
        {
            DeskCell cell = gamePage.Game.Desk.GetCellByMouse( e.Location );
            if ( cell != null )
            {
                select.HoveredCell = cell;
                select.DrawingHovered.Rectangle = cell.Rectangle;

                if ( holdChess != null )
                    holdChess.Sprite.Position = new PointF( e.X - holdDiff.X, e.Y - holdDiff.Y );
                gamePage.GameControl.Repaint();
            }
        }

        public void OnMouseUp( object sender, MouseEventArgs e )
        {
            if ( holdChess != null )
            {
                DeskCell cell = gamePage.Game.Desk.GetCellByMouse( e.Location );
                if ( cell != null )
                {
                    this.InvokeEx( () =>
                    {
                        Point from = holdChess.Cell.Index;
                        if ( gamePage.Game.Move( holdChess, cell.Index ) )
                        {
                            gamePage.Game.SwapPlayers();
                            socket.Send( Packet.MoveChessPacket( new ChessMoveData( from, cell.Index, ContenderPlayer.Name ) ) );

                            if ( IsEndGame )
                            {
                                socket.Send( Packet.EndGamePacket() );
                                gamePage.Game.GameState = Game.State.Finish;
                                Page = selectContenderPage;
                                UpdateContendersList();
                                MessageBox.Show( "Game over" );
                            }
                            if ( IsCheck )
                            {
                                MessageBox.Show( "Check" );
                            }
                        }
                        gamePage.GameControl.Repaint();
                        holdChess = null;

                    } );
                }
            }
        }

        #endregion

        #region Enter forms commands

        private void OnButtonConnectClick( object sender, EventArgs e )
        {
            try
            {
                connectPage.buttonConnect.Enabled = false;
                socket.Connect( IPAddress.Parse( connectPage.textBoxServerIP.Text ), Connection.PORT );
                connectPage.buttonConnect.Enabled = true;
                Page = signInPage;
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
                connectPage.buttonConnect.Enabled = true;
            }
        }

        private void OnClickSingIn( object sender, EventArgs e )
        {
            try
            {
                SignInData signInData = new SignInData( signInPage.textBoxLogin.Text, signInPage.textBoxPassword.Text );
                socket.Send( Packet.SingInPacket( signInData ) );
                
                byte[] buffer = new byte[ 1024 ];
                int readBytes;
                do
                    readBytes = socket.Receive( buffer );
                while ( readBytes == 0 );

                Packet packet = Packet.FromBytes( buffer );
                if ( packet.PacketType == Packet.Type.SignInResult )
                {
                    SignInResult result = ( SignInResult ) packet.Data;
                    switch ( result )
                    {
                        case SignInResult.OK:
                            Page = selectContenderPage;
                            UpdateContendersList();
                            break;
                        case SignInResult.InvalidPasswordOrLogin:
                            MessageBox.Show( "Invalid password or login" );
                            break;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void OnRegister( object sender, EventArgs e )
        {
            Page = signInPage;
            registrationPage.textBoxFullName.Text = string.Empty;
            registrationPage.textBoxLogin.Text = string.Empty;
            registrationPage.textBoxPassword.Text = string.Empty;
        }

        private void OnClickRegistration( object sender, EventArgs e )
        {
            try
            {
                RegistrationData registration = new RegistrationData(
                        registrationPage.textBoxLogin.Text
                    ,   registrationPage.textBoxPassword.Text
                    ,   registrationPage.textBoxFullName.Text
                );
                socket.Send( Packet.RegistrationPacket( registration ) );

                byte[] buffer = new byte[ 1024 ];
                int readBytes;
                do
                    readBytes = socket.Receive( buffer );
                while ( readBytes == 0 );

                Packet packet = Packet.FromBytes( buffer );
                if ( packet.PacketType == Packet.Type.RegistrationResult )
                {
                    RegistrationResult result = ( RegistrationResult ) packet.Data;
                    switch ( result )
                    {
                        case RegistrationResult.OK:
                            Page = signInPage;
                            break;
                        case RegistrationResult.LoginAllreadyExist:
                            MessageBox.Show( "Login allready exist" );
                            break;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        #endregion
        

        private static ManualResetEvent waitLocker = new ManualResetEvent( false );
        private void WaitResult( int timeout = 0 )
        {
            waitLocker.Reset();

            using ( SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs() )
            {
                socketAsyncEventArgs.Completed += OnRecieve;
                socketAsyncEventArgs.AcceptSocket = socket;
                socketAsyncEventArgs.SetBuffer( new byte[ 1024 ], 0, 1024 );
                socket.ReceiveAsync( socketAsyncEventArgs );
            }

            if ( timeout == 0 )
                waitLocker.WaitOne();
            else
                waitLocker.WaitOne( timeout );
        }

        private Random random = new Random();
        public void OnConnectButtonClick( object sender, EventArgs e )
        {
            this.InvokeEx( () => selectContenderPage.Enabled = false );

            string selectedPlayer = ( selectContenderPage.listBoxContenders.SelectedItem as RegistrationData ).Login;
            StartGameData startGameData = new StartGameData(
                    signInPage.textBoxLogin.Text
                ,   selectedPlayer
                ,   ( GameColor ) random.Next( 2 )
                ,   ( ChessDirection ) random.Next( 2 )
            );
            socket.Send( Packet.StartGamePacket( startGameData ) );

            WaitResult( 2000 );
            this.InvokeEx( () => selectContenderPage.Enabled = true );
        }
        
        private void OnWaitButtonClick( object sender, EventArgs e )
        {
            this.InvokeEx( () => selectContenderPage.Enabled = false );
            WaitResult( 20 * 1000 );
            this.InvokeEx( () => selectContenderPage.Enabled = true );
        }

        private void UpdateContendersList()
        {
            this.InvokeEx( () => selectContenderPage.Enabled = false );
            socket.Send( Packet.GetConnectedPlayersPacket( signInPage.textBoxLogin.Text ) );
            WaitResult( 2000 );
            this.InvokeEx( () => selectContenderPage.Enabled = true );
        }

        private void StartGame( StartGameData startGameData )
        {
            Page = gamePage;

            Player player1 = new Player( startGameData.LoginQuery, startGameData.ColorQuery, startGameData.DirectionQuery );
            Player player2 = new Player(
                    startGameData.LoginReply
                ,   startGameData.ColorQuery == GameColor.White ? GameColor.Black : GameColor.White
                ,   startGameData.DirectionQuery == ChessDirection.Up ? ChessDirection.Down : ChessDirection.Up
            );

            gamePage.Game.Start( player1, player2 );
            Task.Factory.StartNew( () =>
            {
                while ( !IsEndGame && !IsDisposed && socket.Connected )
                    WaitResult();
            } );
        }

        private void OnRecieve( object sender, SocketAsyncEventArgs e )
        {
            waitLocker.Set();

            if ( e.AcceptSocket == null )
                return;

            Packet packet;
            try
            {
                packet = Packet.FromBytes( e.Buffer );
            }
            catch
            {
                return;
            }
             
            switch ( packet.PacketType )
            {
                case Packet.Type.GiveConnectedPlayers:
                    {
                        List<RegistrationData> connectedPlayers = ( List<RegistrationData> ) packet.Data;
                        this.InvokeEx( () =>
                        {
                            selectContenderPage.listBoxContenders.Items.Clear();
                            selectContenderPage.listBoxContenders.Items.AddRange( connectedPlayers.ToArray() );
                        } );
                    }
                    break;
                case Packet.Type.StartGame:
                    {
                        StartGameData startGameData = ( StartGameData ) packet.Data;

                        DialogResult result = MessageBox.Show( startGameData.LoginQuery + " offers play", "Start game offer", MessageBoxButtons.YesNo );
                        if ( result == DialogResult.Yes )
                        {
                            socket.Send( Packet.StartGameReplyPacket( startGameData.Reply( StartGameResult.OK ) ) );
                            this.InvokeEx( () => StartGame( startGameData ) );
                        }
                        else if ( result == DialogResult.No )
                        {
                            socket.Send( Packet.StartGameReplyPacket( startGameData.Reply( StartGameResult.Cancel ) ) );
                        }
                    }
                    break;
                case Packet.Type.StartGameResult:
                    {
                        StartGameData startGameData = ( StartGameData ) packet.Data;
                        if ( startGameData.Result == StartGameResult.OK )
                        {
                            this.InvokeEx( () => StartGame( startGameData ) );
                        }
                    }
                    break;
                case Packet.Type.MoveChess:
                    {
                        ChessMoveData chessMoveData = ( ChessMoveData ) packet.Data;
                        this.InvokeEx( () =>
                        {
                            Chess chess = gamePage.Game.Desk[ chessMoveData.From ].Chess;
                            if ( gamePage.Game.Move( chess, chessMoveData.To ) )
                            {
                                gamePage.Game.SwapPlayers();
                                gamePage.GameControl.Repaint();
                                if ( IsEndGame )
                                {
                                    Page = selectContenderPage;
                                    UpdateContendersList();
                                    MessageBox.Show( "Game over" );
                                }
                                if ( IsCheck )
                                {
                                    MessageBox.Show( "Check" );
                                }
                            }
                        } );
                    }
                    break;
                case Packet.Type.AbortGame:
                    this.InvokeEx( () =>
                    {
                        MessageBox.Show( "Abort game" );
                        gamePage.Game.GameState = Game.State.Finish;
                        Page = selectContenderPage;
                        UpdateContendersList();
                    } );
                    break;
            }
        }

        private void OnUpdateButtonClick( object sender, EventArgs e )
        {
            UpdateContendersList();
        }
    }
}
