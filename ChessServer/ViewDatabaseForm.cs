using System;
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
            listBoxDatabase.Items.AddRange( database.ToArray() );
        }

        private SignInData SelectedItem
        {
            get { return listBoxDatabase.SelectedItem as SignInData; }
        }

        private void buttonClose_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
