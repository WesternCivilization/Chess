using System;
using System.Collections.Generic;
using System.Threading;
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
            registrationPage.buttonBack.Click += ( object sender, EventArgs e ) => Page = signInPage;
            Controls.Add( registrationPage );

            selectContenderPage = new SelectСontenderPage();
            selectContenderPage.buttonWait.Click += OnWaitButtonClick;
            selectContenderPage.buttonUpdate.Click += OnUpdateButtonClick;
            selectContenderPage.buttonConnect.Click += OnConnectButtonClick;
            Controls.Add( selectContenderPage );

            gamePage = new ChessGamePage( new Skin( "resources/skin/skin.xml" ) );
            gamePage.GameControl.MouseDown += OnMouseDown;
            gamePage.GameControl.MouseMove += OnMouseMove;
            gamePage.GameControl.MouseUp += OnMouseUp;
            gamePage.GameControl.Paint += OnPaint;
            gamePage.GameControl.Resize += OnResize;
            LoadSelect();
            Controls.Add( gamePage );

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
                e.Graphics.Draw( gamePage.Game.Factory.AllChess );
                e.Graphics.Draw( select );
                if ( holdChess != null )
                    e.Graphics.Draw( holdChess );
            }
        }

        private void OnResize( object sender, EventArgs e )
        {
            gamePage.Game.Desk.Rectangle = ClientRectangle;
            gamePage.Game.Factory.ChessSize = gamePage.Game.Desk.CellsSize;
            gamePage.Game.Factory.AllChess.UpdatePositions();
            this.Repaint();
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
                if ( chess != null && chess.Color == SelfPlayer.Color )
                {
                    holdChess = chess;
                    holdDiff = new PointF( e.X - chess.Sprite.Position.X, e.Y - chess.Sprite.Position.Y );
                }
                this.Repaint();
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
                this.Repaint();
            }
        }

        public void OnMouseUp( object sender, MouseEventArgs e )
        {
            if ( holdChess != null )
            {
                DeskCell cell = gamePage.Game.Desk.GetCellByMouse( e.Location );
                if ( cell != null )
                {
                    gamePage.Game.Move( holdChess, cell.Index );
                    holdChess = null;
                    this.Repaint();
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
        const int TIMEOUT_WAIT = 10 * 1000; // 10 sec

        private void WaitResult()
        {
            waitLocker.Reset();

            using ( SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs() )
            {
                socketAsyncEventArgs.Completed += OnRecieve;
                socketAsyncEventArgs.AcceptSocket = socket;
                socketAsyncEventArgs.SetBuffer( new byte[ 1024 ], 0, 1024 );
                socket.ReceiveAsync( socketAsyncEventArgs );
            }

            waitLocker.WaitOne( TIMEOUT_WAIT );
        }

        private Random random = new Random();
        public void OnConnectButtonClick( object sender, EventArgs e )
        {
            string selectedPlayer = ( selectContenderPage.listBoxContenders.SelectedItem as RegistrationData ).Login;
            StartGameData startGameData = new StartGameData(
                    signInPage.textBoxLogin.Text
                , selectedPlayer
                , ( GameColor ) random.Next( 1 )
                , ( ChessDirection ) random.Next( 1 )
            );
            socket.Send( Packet.StartGamePacket( startGameData ) );

            WaitResult();
        }
        
        private void OnWaitButtonClick( object sender, EventArgs e )
        {
            WaitResult();
        }

        private void UpdateContendersList()
        {
            socket.Send( Packet.GetConnectedPlayersPacket( signInPage.textBoxLogin.Text ) );
            WaitResult();
        }

        private void StartGame( StartGameData startGameData )
        {
            Page = gamePage;

            Player player1 = new Player( startGameData.LoginQuery, startGameData.ColorQuery, startGameData.DirectionQuery );
            Player player2 = new Player(
                    startGameData.LoginReply
                , startGameData.ColorQuery == GameColor.White ? GameColor.Black : GameColor.White
                , startGameData.DirectionQuery == ChessDirection.Up ? ChessDirection.Down : ChessDirection.Up
            );

            gamePage.Game.Start( player1, player2 );
        }

        private void OnRecieve( object sender, SocketAsyncEventArgs e )
        {
            waitLocker.Set();

            if ( e.AcceptSocket == null )
                return;

            Packet packet = Packet.FromBytes( e.Buffer );
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
            }
        }

        private void OnUpdateButtonClick( object sender, EventArgs e )
        {
            UpdateContendersList();
        }
    }
}
