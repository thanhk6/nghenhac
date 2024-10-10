using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace VSW.Core.Web
{
	
	public static class ObjectStorage<T>
	{
		
		public static bool Exists(string key)
		{
			key = "OBJ_" + key;
			return Storage.Exists(key);
		}

		
		public static void SetValue(string key, T value)
		{
			key = "OBJ_" + key;
			try
			{
				XmlObjectSerializer xmlObjectSerializer = new DataContractJsonSerializer(typeof(T));
				MemoryStream memoryStream = new MemoryStream();
				xmlObjectSerializer.WriteObject(memoryStream, value);
				string value2 = Convert.ToBase64String(memoryStream.ToArray());
				memoryStream.Close();
				Storage.SetValue(key, value2, true);
			}
			catch
			{
				Storage.Remove(key);
			}
		}

		
		public static T GetValue(string key)
		{
			key = "OBJ_" + key;
			T result = default(T);
			if (!Storage.Exists(key))
			{
				return result;
			}
			try
			{
				byte[] array = Convert.FromBase64String(Storage.GetValue(key, true));
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(array, 0, array.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = (T)((object)new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream));
				memoryStream.Close();
			}
			catch
			{
				Storage.Remove(key);
			}
			return result;
		}

		
		public static void Remove(string key)
		{
			key = "OBJ_" + key;
			Storage.Remove(key);
		}
	}
}
