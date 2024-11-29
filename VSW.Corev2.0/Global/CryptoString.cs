using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VSW.Core.Global
{

	public sealed class CryptoString
	{
		public static byte[] IV { get; private set; }
		public static byte[] Key { get; private set; }
		private CryptoString()
		{
		}
		public static string MD5Hash(string source)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(source));
			byte[] hash = md5CryptoServiceProvider.Hash;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				stringBuilder.Append(hash[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}


		public static string Decrypt(string encrypt)
		{
			string result;
			try
			{
				byte[] array = System.Convert.FromBase64String(encrypt);
				byte[] array2 = new byte[array.Length];
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				MemoryStream memoryStream = new MemoryStream(array);
				if (CryptoString.IV == null || CryptoString.Key == null)
				{
					CryptoString.snguyenthanh_0(rijndaelManaged);
					CryptoString.snguyenthanh_1(rijndaelManaged);
				}
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor((byte[])CryptoString.Key.Clone(), (byte[])CryptoString.IV.Clone());
				CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
				cryptoStream.Read(array2, 0, array2.Length);
				memoryStream.Close();
				cryptoStream.Close();
				cryptoTransform.Dispose();
				rijndaelManaged.Clear();
				result = Encoding.ASCII.GetString(array2).Replace("\0", "");
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
		public static string Encrypt(string original)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(original);
			byte[] inArray = new byte[0];
			MemoryStream memoryStream = new MemoryStream(bytes.Length);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoString.snguyenthanh_0(rijndaelManaged);
			CryptoString.snguyenthanh_1(rijndaelManaged);
			if (CryptoString.IV == null || CryptoString.Key == null)
			{
				throw new NullReferenceException("savedIV and savedKey must be not null");
			}
			ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor((byte[])CryptoString.Key.Clone(), (byte[])CryptoString.IV.Clone());
			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			inArray = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			cryptoTransform.Dispose();
			rijndaelManaged.Clear();
			return System.Convert.ToBase64String(inArray);
		}
		private static void snguyenthanh_0(RijndaelManaged rdm)
		{
			if (CryptoString.Key == null)
			{
				string cryptoStringKey = Application.CryptoStringKey;
				if (cryptoStringKey != string.Empty)
				{
					CryptoString.Key = CryptoString.GetBytes(cryptoStringKey);
					rdm.Key = CryptoString.Key;
					return;
				}
				rdm.KeySize = 256;
				rdm.GenerateKey();
				CryptoString.Key = rdm.Key;
			}
		}
		private static void snguyenthanh_1(RijndaelManaged rdm)
		{
			if (CryptoString.IV == null)
			{
				string cryptoStringIV = Application.CryptoStringIV;
				if (cryptoStringIV != string.Empty)
				{
					CryptoString.IV = CryptoString.GetBytes(cryptoStringIV);
					rdm.IV = CryptoString.IV;
					return;
				}
				rdm.GenerateIV();
				CryptoString.IV = rdm.IV;
			}
		}
		private static byte[] GetBytes(string source)
		{
			return Encoding.ASCII.GetBytes(source);
		}

		private static void GenerateKey(RijndaelManaged rdm)
		{
			if (CryptoString.Key == null)
			{
				string cryptoStringKey = Application.CryptoStringKey;
				if (cryptoStringKey != string.Empty)
				{
					CryptoString.Key = CryptoString.GetBytes(cryptoStringKey);
					rdm.Key = CryptoString.Key;
					return;
				}
				rdm.KeySize = 256;
				rdm.GenerateKey();
				CryptoString.Key = rdm.Key;
			}
		}
	}
}
