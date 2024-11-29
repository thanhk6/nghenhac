using System;
using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Caching;
using Microsoft.CSharp.RuntimeBinder;

namespace VSW.Core.Web
{

	public static class Cache
	{
		public static void Clear()
		{
			foreach (object obj in HttpRuntime.Cache)
			{
				string text = (string)((DictionaryEntry)obj).Key;
				if (text.StartsWith("VSW.Core.Cache.", StringComparison.OrdinalIgnoreCase))
				{
					HttpRuntime.Cache.Remove(text);
				}
			}
		}
		public static void Clear(string startKey)
		{
			foreach (object obj in HttpRuntime.Cache)
			{
				string text = (string)((DictionaryEntry)obj).Key;
				if (text.StartsWith("VSW.Core.Cache." + startKey, StringComparison.Ordinal))
				{
					HttpRuntime.Cache.Remove(text);
				}
			}
		}

		public static void Clear(dynamic service)
		{
			foreach (DictionaryEntry entry in HttpRuntime.Cache)
			{
				var key = (string)entry.Key;
				if (key.StartsWith("VSW.Core.Cache.Data." + service.TableName, StringComparison.Ordinal))
					HttpRuntime.Cache.Remove(key);
			}
		}



		//public static void Clear(dynamic service)
		//{
		//	foreach (object obj in HttpRuntime.Cache)
		//	{
		//		string text = (string)((DictionaryEntry)obj).Key;
		//		if (Cache.o__2.p__3 == null)
		//		{
		//			Cache.o__2.p__3 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		Func<CallSite, object, bool> target = Cache.o__2.p__3.Target;
		//		CallSite p__ = Cache.o__2.p__3;
		//		if (Cache.o__2.p__2 == null)
		//		{
		//			Cache.o__2.p__2 = CallSite<Func<CallSite, string, object, StringComparison, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "StartsWith", null, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
		//			}));
		//		}
		//		Func<CallSite, string, object, StringComparison, object> target2 = Cache.o__2.p__2.Target;
		//		CallSite p__2 = Cache.o__2.p__2;
		//		string text2 = text;
		//		if (Cache.o__2.p__1 == null)
		//		{
		//			Cache.o__2.p__1 = CallSite<Func<CallSite, string, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		Func<CallSite, string, object, object> target3 = Cache.o__2.p__1.Target;
		//		CallSite p__3 = Cache.o__2.p__1;
		//		string text3 = "VSW.Core.Cache.Data.";
		//		if (Cache.o__2.p__0 == null)
		//		{
		//			Cache.o__2.p__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "TableName", typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		if (Cache.<>o__3.<>p__4 == null)
		//		{
		//			Cache.<>o__3.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(Cache)));
		//		}
		//		Func<CallSite, object, bool> target4 = Cache.<>o__3.<>p__4.Target;
		//		CallSite <>p__ = Cache.<>o__3.<>p__4;
		//		if (Cache.<>o__3.<>p__3 == null)
		//		{
		//			Cache.<>o__3.<>p__3 = CallSite<Func<CallSite, Func<CallSite, object, bool>, CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Invoke(CSharpBinderFlags.None, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		Func<CallSite, Func<CallSite, object, bool>, CallSite, object, object> target5 = Cache.<>o__3.<>p__3.Target;
		//		CallSite <>p__2 = Cache.<>o__3.<>p__3;
		//		Func<CallSite, object, bool> arg = target;
		//		CallSite arg2 = p__;
		//		if (Cache.<>o__3.<>p__2 == null)
		//		{
		//			Cache.<>o__3.<>p__2 = CallSite<Func<CallSite, Func<CallSite, string, object, StringComparison, object>, CallSite, string, object, StringComparison, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Invoke(CSharpBinderFlags.None, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
		//			}));
		//		}
		//		Func<CallSite, Func<CallSite, string, object, StringComparison, object>, CallSite, string, object, StringComparison, object> target6 = Cache.<>o__3.<>p__2.Target;
		//		CallSite <>p__3 = Cache.<>o__3.<>p__2;
		//		Func<CallSite, string, object, StringComparison, object> arg3 = target2;
		//		CallSite arg4 = p__2;
		//		string arg5 = text2;
		//		if (Cache.<>o__3.<>p__1 == null)
		//		{
		//			Cache.<>o__3.<>p__1 = CallSite<Func<CallSite, Func<CallSite, string, object, object>, CallSite, string, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Invoke(CSharpBinderFlags.None, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		Func<CallSite, Func<CallSite, string, object, object>, CallSite, string, object, object> target7 = Cache.<>o__3.<>p__1.Target;
		//		CallSite <>p__4 = Cache.<>o__3.<>p__1;
		//		Func<CallSite, string, object, object> arg6 = target3;
		//		CallSite arg7 = p__3;
		//		string arg8 = text3;
		//		if (Cache.<>o__3.<>p__0 == null)
		//		{
		//			Cache.<>o__3.<>p__0 = CallSite<Func<CallSite, Func<CallSite, object, object>, CallSite<Func<CallSite, object, object>>, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Invoke(CSharpBinderFlags.None, typeof(Cache), new CSharpArgumentInfo[]
		//			{
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
		//				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
		//			}));
		//		}
		//		if (target4(<>p__, target5(<>p__2, arg, arg2, target6(<>p__3, arg3, arg4, arg5, target7(<>p__4, arg6, arg7, arg8, Cache.<>o__3.<>p__0.Target(Cache.<>o__3.<>p__0, Cache.o__2.p__0.Target, Cache.o__2.p__0, service)), StringComparison.Ordinal))))
		//		{
		//			HttpRuntime.Cache.Remove(text);
		//		}
		//	}
		//}




		public static void ClearTable(string tableName)
		{
			foreach (object obj in HttpRuntime.Cache)
			{
				string text = (string)((DictionaryEntry)obj).Key;
				if (text.StartsWith("VSW.Core.Cache.Data." + tableName, StringComparison.Ordinal) || text.StartsWith("VSW.Core.Cache.Lib.App.", StringComparison.Ordinal))
				{
					HttpRuntime.Cache.Remove(text);
				}
			}
			try
			{
				Directory.Delete(HttpContext.Current.Server.MapPath("~/Data/html/" + tableName + "/"), true);
			}
			catch
			{
			}
		}

		public static object GetValue(string key)
		{
			return HttpRuntime.Cache["VSW.Core.Cache." + key];
		}
		private static void Remove(string key)
		{
			if (HttpRuntime.Cache["VSW.Core.Cache." + key] != null)
			{
				HttpRuntime.Cache.Remove("VSW.Core.Cache." + key);
			}
		}
		public static void SetValue(string key, object obj)
		{
			Cache.SetValue(key, obj, -1);
		}
		public static void SetValue(string key, object obj, int minutes)
		{
			if (obj != null)
			{
				if (minutes > 0)
				{
					HttpRuntime.Cache.Insert("VSW.Core.Cache." + key, obj, null, DateTime.Now.AddMinutes((double)minutes), TimeSpan.Zero, CacheItemPriority.Normal, null);
					return;
				}
				HttpRuntime.Cache.Insert("VSW.Core.Cache." + key, obj);
			}
		}
		public static string CreateKey()
		{
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			return "Lib.App." + currentMethod.ReflectedType.Name + "." + currentMethod.Name;
		}
		public static string CreateKey(string key)
		{
			return Cache.CreateKey() + "." + key;
		}

		public static string CreateKey(string DbName, string key)
		{
			return string.Concat(new string[]
			{
				Cache.CreateKey(),
				".",
				DbName,
				".",
				key
			});
		}
		[CompilerGenerated]
		private static class o__2
		{
			public static CallSite<Func<CallSite, object, object>> p__0;
			public static CallSite<Func<CallSite, string, object, object>> p__1;
			public static CallSite<Func<CallSite, string, object, StringComparison, object>> p__2;
			public static CallSite<Func<CallSite, object, bool>> p__3;
		}
	}
}
