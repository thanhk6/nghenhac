using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace VSW.Core.Global
{

	public class Support
	{

		protected static int GetCount(string input, string match)
		{
			List<int> list = new List<int>();
			int num = 0;
			while (num < input.Length && (num = input.IndexOf(match, num, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				list.Add(num);
				num += match.Length;
			}
			return list.Count;
		}


		protected static bool HasProperty(object item, string property)
		{
			return item.GetType().GetProperty(property) != null;
		}

		protected static dynamic Convert(dynamic source, Type destination)
		{
			return System.Convert.ChangeType(source, destination);
		}

		//protected static dynamic Convert(dynamic source, Type destination)
		//{
		//	if (Support.o__2.p__0 == null)
		//	{
		//		Support.o__2.p__0 = CallSite<Func<CallSite, Type, object, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ChangeType", null, typeof(Support), new CSharpArgumentInfo[]
		//		{
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
		//		}));
		//	}
		//	if (Support.<>o__3.<>p__0 == null)
		//	{
		//		Support.<>o__3.<>p__0 = CallSite<Func<CallSite, Func<CallSite, Type, object, Type, object>, CallSite<Func<CallSite, Type, object, Type, object>>, Type, object, Type, object>>.Create(Binder.Invoke(CSharpBinderFlags.None, typeof(Support), new CSharpArgumentInfo[]
		//		{
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
		//			CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
		//		}));
		//	}
		//	return Support.<>o__3.<>p__0.Target(Support.<>o__3.<>p__0, Support.o__2.p__0.Target, Support.o__2.p__0, typeof(Convert), source, destination);
		//}


		internal static string GetFileName(string query, params object[] param)
		{
			return CryptoString.MD5Hash(Support.BitConverter(query, param));
		}


		internal static string BitConverter(string source, params object[] Params)
		{
			int num = 0;
			while (Params != null && num < Params.Length)
			{
				string str = source;
				object obj = Params[num];
				source = str + ((obj != null) ? obj.ToString() : null);
				num++;
			}
			return Security.BitConverter(source);
		}


		internal static string EscapeQuote(string source)
		{
			if (!string.IsNullOrEmpty(source))
			{
				return source.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("\b", "\\b").Replace("\f", "\\f");
			}
			return string.Empty;
		}


		public static bool IsNull(object data)
		{
			return data == null || data == DBNull.Value || (data is DateTime && (DateTime)data == DateTime.MinValue);
		}


		[CompilerGenerated]
		private static class o__2
		{
			// Token: 0x040000CF RID: 207
			public static CallSite<Func<CallSite, Type, object, Type, object>> p__0;
		}
	}
}
