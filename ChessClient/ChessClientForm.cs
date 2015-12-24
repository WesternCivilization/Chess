using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Chess
{
    public partial class ChessClientForm : Form
    {
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

        private Socket socket = Connection.CreateSocket();

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
            registrationPage.buttonBack.Click += ( object sender, EventArgs e ) => Page = signInPage;
            Controls.Add( registrationPage );

            selectContenderPage = new SelectСontenderPage();
            selectContenderPage.buttonPlay.Click += OnPlayButtonClick;
            selectContenderPage.buttonUpdate.Click += OnUpdateButtonClick;
            Controls.Add( selectContenderPage );

            gamePage = new ChessGamePage( new Skin( "resources/skin/skin.xml" ) );
            Controls.Add( gamePage );

            Page = connectPage;
        }

        private void ChessClientForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            socket.Close();
        }

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
                socket.Send( Packet.SingInPacket( signInData ).ToBytes() );
                
                byte[] buffer = new byte[ 1024 ];
                int readBytes;
                do
                    readBytes = socket.Receive( buffer );
                while ( readBytes == 0 );

                Packet packet = Packet.FromBytes( buffer );
                if ( packet.PacketType == Packet.Type.SignInResult )
                {
                    switch ( packet.SignInResult )
                    {
                        case SignInResult.OK:
                            Page = selectContenderPage;
                            UpdateContendersList();
                            Task.Factory.StartNew( RecieveConnection );
                            break;
                        case SignInResult.InvalidPasswordOrLogin:
                            MessageBox.Show( "Invalid password or login" );
                            break;
                        case SignInResult.UnknownError:
                            MessageBox.Show( "Unknown error" );
                            break;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void RecieveConnection()
        {
        }

        private void OnClickRegistration( object sender, EventArgs e )
        {
            try
            {
                RegistrationData registration = new RegistrationData(
                        registrationPage.textBoxLogin.Text
                    ,   registrationPage.textBoxPassword.Text
                    ,   registrationPage.textBoxFullName.Text
                    ,   ( int ) registrationPage.numericUpDownAge.Value
                );
                socket.Send( Packet.RegistrationPacket( registration ).ToBytes() );

                byte[] buffer = new byte[ 1024 ];
                int readBytes;
                do
                    readBytes = socket.Receive( buffer );
                while ( readBytes == 0 );

                Packet packet = Packet.FromBytes( buffer );
                if ( packet.PacketType == Packet.Type.RegistrationResult )
                {
                    switch ( packet.RegistrationResult )
                    {
                        case RegistrationResult.OK:
                            Page = signInPage;
                            break;
                        case RegistrationResult.LoginAllreadyExist:
                            MessageBox.Show( "Login allready exist" );
                            break;
                        case RegistrationResult.UnknownError:
                            MessageBox.Show( "Unknown error" );
                            break;
                    }
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void OnPlayButtonClick( object sender, EventArgs e )
        {
            string selectedPlayer = ( selectContenderPage.listBoxContenders.SelectedItem as RegistrationData ).Login;
            socket.Send( Packet.StartGamePacket( selectedPlayer ).ToBytes() );

            byte[] buffer = new byte[ 255 ];
            int readBytes;
            do
                readBytes = socket.Receive( buffer );
            while ( readBytes == 0 );

            Packet packet = Packet.FromBytes( buffer );
            if ( packet.PacketType == Packet.Type.StartGameResult )
            {
                if ( packet.StartGameResult == StartGameResult.OK )
                {

                }
            }
        }

        private void UpdateContendersList()
        {
            selectContenderPage.listBoxContenders.Items.Clear();

            socket.Send( Packet.GetConnectedPlayersPacket( signInPage.textBoxLogin.Text ).ToBytes() );

            byte[] buffer = new byte[ 1024 * 1024 ];
            int readBytes;
            do
                readBytes = socket.Receive( buffer );
            while ( readBytes == 0 );

            Packet packet = Packet.FromBytes( buffer );
            selectContenderPage.listBoxContenders.Items.AddRange( packet.ConnectedPlayers.ToArray() );
        }

        private void OnUpdateButtonClick( object sender, EventArgs e )
        {
            UpdateContendersList();
        }

    }
}
