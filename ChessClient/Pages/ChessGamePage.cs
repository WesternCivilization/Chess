using System;
using System.Windows.Forms;
using System.Drawing;
using Tools;

namespace Chess
{
    public class ChessGamePage : UserControl
    {
        private const int indent = 30;
        private Size cellSize = new Size( 50, 50 );
        private Font font = new Font( "Segoe UI", 16 );

        public ChessGameControl GameControl;
        public Game Game { get; private set; }

        public ChessGamePage( Skin skin )
        {
            Game = new Game( skin );

            GameControl = new ChessGameControl();
            GameControl.ClientSize = new Size( cellSize.Width * 8, cellSize.Width * 8 );
            OnResize();
            ClientSize = new Size(
                    indent + GameControl.ClientSize.Width + indent
                ,   indent + GameControl.ClientSize.Height + indent
            );
            GameControl.Location = new Point( indent, indent );
            Controls.Add( GameControl );

            Paint += OnPaint;
        }

        private void DrawCoords( Graphics g, ChessDirection direction )
        {
            for ( int i = 0; i < 8; ++i )
            {
                int textNumer = i + 1;
                if ( direction == ChessDirection.Down )
                    textNumer = 9 - textNumer;

                g.DrawString(
                        textNumer.ToString()
                    , font
                    , Brushes.Black
                    , ( indent * 0.2F )
                    , indent + ( cellSize.Height * i ) + ( cellSize.Height / 2 ) - ( font.Height / 2 )
                );

                g.DrawString(
                        char.ConvertFromUtf32( 'A' + i )
                    , font
                    , Brushes.Black
                    , indent + ( cellSize.Width * i ) + ( cellSize.Width * 0.3F )
                    , ( indent / 2 ) - ( font.Height / 2 )
                );
            }
        }

        private void OnPaint( object sender, PaintEventArgs e )
        {
            if ( Game.GameState == Game.State.InTheGame )
                DrawCoords( e.Graphics, Game.Player1.Direction );
        }

        private void OnResize( object sender = null, EventArgs e = null )
        {
            Game.Desk.Rectangle = GameControl.ClientRectangle;
            Game.Factory.ChessSize = Game.Desk.CellsSize;
            Game.Factory.ActiveChess.UpdatePositions();
            this.Repaint();
        }
    }
}
