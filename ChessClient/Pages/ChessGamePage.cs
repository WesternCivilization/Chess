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

        private ChessGameControl gameControl;
        public Game Game { get { return gameControl.Game; } }

        public ChessGamePage( Skin skin )
        {
            Paint += OnPaint;

            gameControl = new ChessGameControl( skin );
            gameControl.ClientSize = new Size( cellSize.Width * 8, cellSize.Width * 8 );
            ClientSize = new Size(
                    indent + gameControl.ClientSize.Width + indent
                ,   indent + gameControl.ClientSize.Height + indent
            );
            gameControl.Location = new Point( indent, indent );
            Controls.Add( gameControl );
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
                    ,   font
                    ,   Brushes.Black
                    ,   ( indent * 0.2F )
                    ,   indent + ( cellSize.Height * i ) + ( cellSize.Height / 2 ) - ( font.Height / 2 )
                );

                g.DrawString(
                        char.ConvertFromUtf32( 'A' + i )
                    ,   font
                    ,   Brushes.Black
                    ,   indent + ( cellSize.Width * i ) + ( cellSize.Width * 0.3F )
                    ,   ( indent / 2 ) - ( font.Height / 2 )
                );
            }
        }

        private void OnPaint( object sender, PaintEventArgs e )
        {
            DrawCoords( e.Graphics, Game.Player1.Direction );
        }
    }
}
