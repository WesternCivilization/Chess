using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace Chess
{
    [Serializable]
    public class Database
    {
        public List<RegistrationData> Registered { get; private set; }
        
        public Database()
        {
            Registered = new List<RegistrationData>();
        }

        public static Database FromFile( string fileName )
        {
            return BinSerializer<Database>.FromFile( fileName );
        }

        public void Save( string fileName )
        {
            BinSerializer<Database>.ToFile( this, fileName );
        }

        public RegistrationData Find( string login )
        {
            foreach ( RegistrationData data in Registered )
            {
                if ( data.Login == login )
                    return data;
            }
            return null;
        }

        public bool ContainsLogin( string login )
        {
            foreach ( RegistrationData data in Registered )
            {
                if ( data.Login == login )
                    return true;
            }
            return false;
        }

    }
}
