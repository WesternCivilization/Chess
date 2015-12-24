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
    public partial class ViewDatabaseForm : Form
    {
        private Database database;
        
        public ViewDatabaseForm( Database database )
        {
            InitializeComponent();
            this.database = database;

            listBoxDatabase.Items.AddRange( database.Registered.ToArray() );
        }

        private SignInData SelectedItem
        {
            get { return listBoxDatabase.SelectedItem as SignInData; }
        }

        private void buttonAdd_Click( object sender, EventArgs e )
        {
            // TODO
        }

        private void buttonDelete_Click( object sender, EventArgs e )
        {
            // TODO
        }

        private void buttonChange_Click( object sender, EventArgs e )
        {
            // TODO
        }
    }
}
