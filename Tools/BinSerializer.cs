using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tools
{
    public static class BinSerializer<T> where T : class
    {
        public static T FromBytes( byte[] buffer )
        {
            MemoryStream memoryStream = new MemoryStream( buffer );
            try
            {
                return ( T ) ( new BinaryFormatter() ).Deserialize( memoryStream );
            }
            catch
            {
                throw;
            }
            finally
            {
                memoryStream.Close();
            }
        }

        public static byte[] ToBytes( T data )
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                new BinaryFormatter().Serialize( ms, data );
                byte[] buffer = ms.GetBuffer();
                return buffer;
            }
            catch
            {
                throw;
            }
            finally
            {
                ms.Close();
            }
        }

        public static T FromFile( string filename )
        {
            FileStream fileStream = File.Open( filename, FileMode.Open );
            try
            {
                return ( T ) ( new BinaryFormatter() ).Deserialize( fileStream );
            }
            catch
            {
                throw;
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static void ToFile( T data, string filename )
        {
            FileStream fileStream = File.Open( filename, FileMode.OpenOrCreate );
            try
            {
                new BinaryFormatter().Serialize( fileStream, data );
            }
            catch
            {
                throw;
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}