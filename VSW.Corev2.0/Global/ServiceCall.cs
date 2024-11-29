using System;
using System.Globalization;
using System.Reflection;

namespace VSW.Core.Global
{
	
	public class ServiceCall
	{		
		public static object Call(object module, string methodName, object[] Params)
		{
			object result;
			try
			{
				result = module.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, module, Params, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw ex;
			}
			return result;
		}
		public static object GetService(string assembly, string service)
		{
			return new Class(assembly, service).Instance;
		}
	}
}
