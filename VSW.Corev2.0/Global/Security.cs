using System;
using System.Collections;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace VSW.Core.Global
{
	
	internal sealed class Security
	{
		internal static string BitConverter(string s)
		{
			byte[] bytes = new UnicodeEncoding().GetBytes(s);
			return System.BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes));
		}

		internal static bool VerifyData(string s, string base64, string xmlString)
		{
			bool result;
			try
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
				rsacryptoServiceProvider.FromXmlString(xmlString);
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				byte[] signature = System.Convert.FromBase64String(base64);
				result = rsacryptoServiceProvider.VerifyData(bytes, new SHA1CryptoServiceProvider(), signature);
			}
			catch
			{
				result = false;
			}
			return result;
		}
		internal static bool VerifyData(string domain, string license)
		{
			bool result;
			try
			{
				string text = "<BitStrength>1024</BitStrength><RSAKeyValue><Modulus>i5RV0ogGxruUSECEniYDyVFyiDwh23QIvw8tuiOD5qJRmAQ+akaVHTrhSfqRFVZSDdKmO6SDWW+t+njtoCik5j+UvbKxd7x3hmlhBo343v+8X8j1MQH26qLhqyFcd0CLq6dsJy/NDiI7+SNZ19caOqprKAuHUdCvN0veS1wxWZ0=</Modulus><Exponent>AQAB</Exponent><P>vM/6h9t3Vq1z1YU/ICyq+mO5LT9ANatwuj1UhR/zS4HXRWOQqs62jYIfClsVpJtJhZgivXOhlT7HSA1L3J3Pbw==</P><Q>vT9wGTMLSYra0a/CiZEGv3qf1ZPuC9Fk3IRRXk34EaGIj7QCYgPIdIKKtpXq0h46zTs0PSjFc1AbY3PXkaQhsw==</Q><DP>aEH/eN06vGXyvEhjXwTLNI+0RMPUVM5h5LTQ4uGNmngLwokD7Q1PyDu9oB5DiS0mH9qt9CbRZnPP3n1ZEm0hiQ==</DP><DQ>pCjwQ1us0dl6IOQ2ewBrexOSrEDLAENeG49HyecWZazaedUrL/yaGL7YNrPq4uNIHJjboqXISVPWvjlRaEuY0w==</DQ><InverseQ>rZ5XdVEbMyJB6cqvVrQFsm+Pu/wsGym0H4RhHZYy1AIkdeck4dJIBvW1QIoYtS3ESQB7jqx/W4Wp5tgS8wZLGg==</InverseQ><D>AXpUsRhDaU3c8Cm0UPZaR0+A9NdQjNtjW+tQ7FRQiU8bqdsVpbHRbCm07yZJ7F3P6MyO4DljZXC5ko0+oUAwHfbHbOc8mfeU5cXwPS1rdC6PfycQfuxc/ovbBO/9O01XmKhSWlUtrcYSDKZs/ln+lb+pefG1Awv5mtvHalMdgoM=</D></RSAKeyValue>";
				string text2 = text.Substring(0, text.IndexOf("</BitStrength>", StringComparison.Ordinal) + 14);
				text = text.Replace(text2, "");
				int dwKeySize = System.Convert.ToInt32(text2.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));
				result = string.Equals(domain, Security.DecryptString(license, dwKeySize, text));
			}
			catch
			{
				result = false;
			}
			return result;
		}
		internal static string EncryptString(string inputString, int dwKeySize, string xmlString)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
			rsacryptoServiceProvider.FromXmlString(xmlString);
			int num = dwKeySize / 8;
			byte[] bytes = Encoding.UTF32.GetBytes(inputString);
			int num2 = num - 42;
			int num3 = bytes.Length;
			int num4 = num3 / num2;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i <= num4; i++)
			{
				byte[] array = new byte[(num3 - num2 * i > num2) ? num2 : (num3 - num2 * i)];
				Buffer.BlockCopy(bytes, num2 * i, array, 0, array.Length);
				byte[] array2 = rsacryptoServiceProvider.Encrypt(array, true);
				System.Array.Reverse(array2);
				stringBuilder.Append(System.Convert.ToBase64String(array2));
			}
			return stringBuilder.ToString();
		}
		internal static string DecryptString(string inputString, int dwKeySize, string xmlString)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
			rsacryptoServiceProvider.FromXmlString(xmlString);
			int num = (dwKeySize / 8 % 3 != 0) ? (dwKeySize / 8 / 3 * 4 + 4) : (dwKeySize / 8 / 3 * 4);
			int num2 = inputString.Length / num;
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < num2; i++)
			{
				byte[] array = System.Convert.FromBase64String(inputString.Substring(num * i, num));
				System.Array.Reverse(array);
				arrayList.AddRange(rsacryptoServiceProvider.Decrypt(array, true));
			}
			return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
		}
		internal static bool HasAccess(string path)
		{
			bool result;
			try
			{
				bool flag = false;
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				foreach (object obj in Directory.GetAccessControl(path).GetAccessRules(true, true, typeof(SecurityIdentifier)))
				{
					FileSystemAccessRule fileSystemAccessRule = (FileSystemAccessRule)obj;
					if (windowsPrincipal.IsInRole(fileSystemAccessRule.IdentityReference as SecurityIdentifier) && (FileSystemRights.WriteData & fileSystemAccessRule.FileSystemRights) == FileSystemRights.WriteData)
					{
						if (fileSystemAccessRule.AccessControlType == AccessControlType.Allow)
						{
							flag = true;
						}
						else if (fileSystemAccessRule.AccessControlType == AccessControlType.Deny)
						{
							result = false;
							return result;
						}
					}
				}
				result = flag;
			}
			catch (UnauthorizedAccessException)
			{
				result = false;
			}
			return result;
		}
	}
}
