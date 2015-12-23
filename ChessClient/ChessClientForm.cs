using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessClientForm : Form
    {
        public ChessGamePage gamePage;

        private const int indent = 30;
        private Size cellSize = new Size( 50, 50 );
        private Font font = new Font( "Segoe UI", 16 );

        
        public ChessClientForm()
        {
            InitializeComponent();

            Player player1 = new Player();
            Player player2 = new Player();

            Skin skin = new Skin( @"Resources\skin\skin.xml" );

            ChessFactory factory = new ChessFactory( skin, ChessDirection.Up );

            gamePage = new ChessGamePage( new Game( player1, player2, factory ) );
            gamePage.ClientSize = new Size( cellSize.Width * 8, cellSize.Width * 8 );
            ClientSize = new Size(
                    indent + gamePage.ClientSize.Width + indent
                ,   indent + gamePage.ClientSize.Height + indent );
            
            gamePage.Location = new Point( indent, indent );

            Controls.Add( gamePage );
        }

        private void DrawCoords( Graphics g )
        {
            for ( int i = 0; i < 8; ++i )
            {
                g.DrawString(
                        ( i + 1 ).ToString()
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

        private void ChessClientForm_Paint( object sender, PaintEventArgs e )
        {
            DrawCoords( e.Graphics );
        }
    }
}
