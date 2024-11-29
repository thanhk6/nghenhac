using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VSW.Core.Global
{
	
	public class Class
	{
		public object Instance
		{
			[CompilerGenerated]
			get
			{
				return this.instance;
			}
		}
		public Class(object obj)
		{
			this.instance = obj;
			this._module = obj.GetType();
		}
		public Class(Type module)
		{
			this._module = module;
			this.instance = Activator.CreateInstance(this._module);
		}
		public Class(string assembly, string typeName)
		{
			if (Class.dictionary_0 == null)
			{
				Class.dictionary_0 = new Dictionary<string, Assembly>();
			}
			string key = Support.BitConverter(assembly, new object[0]);
			Assembly assembly2;
			if (Class.dictionary_0.ContainsKey(key))
			{
				assembly2 = Class.dictionary_0[key];
			}
			else
			{
				assembly2 = Assembly.Load(assembly);
				Class.dictionary_0[key] = assembly2;
			}
			this._module = assembly2.GetType(typeName);
			if (this._module == null)
			{
				throw new Exception("Type " + typeName + " not found");
			}
			this.instance = Activator.CreateInstance(this._module);
		}
		public object CallMethod(string methodName)
		{
			return this.CallMethod(methodName, null);
		}

		
		public object CallMethod(string methodName, object[] args)
		{
			return this._module.InvokeMember(methodName, BindingFlags.InvokeMethod, null, this.Instance, args, CultureInfo.InvariantCulture);
		}

		
		public object CallMethodStatic(string methodName, object[] args)
		{
			return this._module.InvokeMember(methodName, BindingFlags.Static, null, this.Instance, args, CultureInfo.InvariantCulture);
		}

		
		public bool ExistsField(string fieldName)
		{
			return System.Array.IndexOf<string>(this.GetFields_Name(), fieldName) > -1;
		}
		public bool ExistsProperty(string propertyName)
		{
			return System.Array.IndexOf<string>(this.GetPropertiesName(), propertyName) > -1;
		}
		public object GetField(string fieldName)
		{
			return this._module.InvokeMember(fieldName, BindingFlags.GetField, null, this.Instance, new object[0], CultureInfo.InvariantCulture);
		}
		public FieldInfo GetFieldInfo(string fieldName)
		{
			return this._module.GetField(fieldName);
		}
		public FieldInfo[] GetFieldsInfo()
		{
			if (this._arrFieldInfo == null)
			{
				this._arrFieldInfo = this._module.GetFields();
			}
			return this._arrFieldInfo;
		}

		
		public string[] GetFields_Name()
		{
			if (this._arrFieldName == null)
			{
				FieldInfo[] fields = this._module.GetFields();
				this._arrFieldName = new string[fields.Length];
				for (int i = 0; i < fields.Length; i++)
				{
					this._arrFieldName[i] = fields[i].Name;
				}
			}
			return this._arrFieldName;
		}
		public List<string> GetMethodsName()
		{
			if (this.listMethodName == null)
			{
				MethodInfo[] methods = this._module.GetMethods();
				this.listMethodName = new List<string>();
				for (int i = 0; i < methods.Length; i++)
				{
					this.listMethodName.Add(methods[i].Name);
				}
			}
			return this.listMethodName;
		}
		public PropertyInfo[] GetPropertiesInfo()
		{
			if (this._arrPropertyInfo == null)
			{
				this._arrPropertyInfo = this._module.GetProperties();
			}
			return this._arrPropertyInfo;
		}
		public string[] GetPropertiesName()
		{
			if (this._arrPropertiesName == null)
			{
				PropertyInfo[] properties = this._module.GetProperties();
				this._arrPropertiesName = new string[properties.Length];
				for (int i = 0; i < properties.Length; i++)
				{
					this._arrPropertiesName[i] = properties[i].Name;
				}
			}
			return this._arrPropertiesName;
		}

		public object GetProperty(string propertyName)
		{
			return this._module.InvokeMember(propertyName, BindingFlags.GetProperty, null, this.Instance, new object[0], CultureInfo.InvariantCulture);
		}
		public PropertyInfo GetPropertyInfo(string propertyName)
		{
			return this._module.GetProperty(propertyName);
		}
		public void SetField(string fieldName, object fieldValue)
		{
			this._module.InvokeMember(fieldName, BindingFlags.SetField, null, this.Instance, new object[]
			{
				fieldValue
			}, CultureInfo.InvariantCulture);
		}
		public void SetProperty(string propertyName, object propertyValue)
		{
			this._module.InvokeMember(propertyName, BindingFlags.SetProperty, null, this.Instance, new object[]
			{
				propertyValue
			}, CultureInfo.InvariantCulture);
		}
		private static Dictionary<string, Assembly> dictionary_0;
		private FieldInfo[] _arrFieldInfo;
		private List<string> listMethodName;
		private PropertyInfo[] _arrPropertyInfo;
		private string[] _arrPropertiesName;
		private string[] _arrFieldName;
		private Type _module;
		private object instance;
	}
}
