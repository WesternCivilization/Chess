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
        private ChessGamePage gamePage;
        
        public ChessClientForm()
        {
            InitializeComponent();
            
            gamePage = new ChessGamePage( new Skin( "resources/skin/skin.xml" ) );
            gamePage.Game.Start( new Player( "p1" ), new Player( "p2" ) );
            Controls.Add( gamePage );
        }
    }
}
