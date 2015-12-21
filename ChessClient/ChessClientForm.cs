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

        public ChessClientForm()
        {
            InitializeComponent();

            Skin skin = new Skin( @"Resources\skin\skin.xml" );
            
            gamePage = new ChessGamePage( new Game(
                    new Player()
                ,   new Player()
                ,   new ChessFactory( skin, ChessDirection.Up )
            ) );

            Controls.Add( gamePage );
            gamePage.Dock = DockStyle.Fill;
            gamePage.Visible = true;
        }
    }
}
