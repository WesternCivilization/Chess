using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            const string FILE_DATABASE = "DATA.BIN";

            Database database;
            if ( File.Exists( FILE_DATABASE ) )
                database = Database.FromFile( FILE_DATABASE );
            else
                database = new Database();

            Application.Run( new ChessServerForm( database ) );

            database.Save( FILE_DATABASE );
        }
    }
}
