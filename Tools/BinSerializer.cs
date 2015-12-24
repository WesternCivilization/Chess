using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tools
{
    public static class BinSerializer<T> where T : class
    {
        public static T FromBytes( byte[] buffer )
        {
            using ( MemoryStream memoryStream = new MemoryStream( buffer ) )
            {
                return ( T ) ( new BinaryFormatter() ).Deserialize( memoryStream );
            }
        }

        public static byte[] ToBytes( T data )
        {
            using ( MemoryStream memoryStream = new MemoryStream() )
            {

                new BinaryFormatter().Serialize( memoryStream, data );
                byte[] buffer = memoryStream.GetBuffer();
                return buffer;
            }
        }

        public static T FromFile( string filename )
        {
            using ( FileStream fileStream = File.Open( filename, FileMode.Open ) )
            {
                return ( T ) ( new BinaryFormatter() ).Deserialize( fileStream );
            }
        }

        public static void ToFile( T data, string filename )
        {
            using ( FileStream fileStream = File.Open( filename, FileMode.OpenOrCreate ) )
            {
                new BinaryFormatter().Serialize( fileStream, data );
            }
        }
    }
}