using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace VSW.Core.MVC
{
	public class ViewControl : UserControl
	{	
		public Controller Controller { get; internal set; }
		public string VSWID { get; set; }		
		public dynamic ViewBag {  get;  internal set; }

		public Dictionary<string, object> ViewData { get; set; }

		public object GetObject(string key)
		{
			string id = key.Split(new char[]
			{
				'.'
			})[0];
			string name = key.Split(new char[]
			{
				'.'
			})[1];
			Control control = this.FindControl(id);
			object result;
			if (control != null)
			{
				result = control.GetType().InvokeMember(name, BindingFlags.GetProperty, null, control, null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void SetObject(string key, object value)
		{
			string id = key.Split(new char[]
			{
				'.'
			})[0];
			string name = key.Split(new char[]
			{
				'.'
			})[1];
			Control control = this.FindControl(id);
			if (control != null)
			{
				control.GetType().InvokeMember(name, BindingFlags.SetProperty, null, control, new object[]
				{
					value
				});
			}
		}
	}
}
