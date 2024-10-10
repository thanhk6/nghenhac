using System;
using System.Runtime.CompilerServices;

namespace VSW.Core.MVC
{
	[AttributeUsage(AttributeTargets.All)]
	public class PropertyInfo : Attribute
	{
		public string Key
		{
			[CompilerGenerated]
			get
			{
				return this.key;
			}
		}
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.value;
			}
		}
		public PropertyInfo(string key)
		{
			this.key = key;
		}
		public PropertyInfo(string key, object value)
		{
			this.key = key;
			this.value = value;
		}
		private string key;
		private object value;
	}
}
