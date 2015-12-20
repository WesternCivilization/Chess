using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chess
{
	static class BinarySerializer<T> where T : class
	{
		public static T Create(byte[] buffer)
		{
			return (T)(new BinaryFormatter()).Deserialize(new MemoryStream(buffer));
		}

		public static T FromFile(string filename)
		{
			return (T)(new BinaryFormatter()).Deserialize(File.Open(filename, FileMode.Open));
		}

		public static byte[] ToBytes(T data)
		{
			MemoryStream ms = new MemoryStream();
			new BinaryFormatter().Serialize(ms, data);
			byte[] buffer = ms.GetBuffer();
			ms.Close();
			return buffer;
		}

		public static void ToFile(T data, string filename)
		{
			new BinaryFormatter().Serialize(File.Open(filename, FileMode.OpenOrCreate), data);
		}
	}
}