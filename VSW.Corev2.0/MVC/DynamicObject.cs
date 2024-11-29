using System;
using System.Collections.Generic;
using System.Dynamic;

namespace VSW.Core.MVC
{
	public class DynamicObject :System.Dynamic.DynamicObject
	{
		public DynamicObject(Dictionary<string, object> viewData)
		{
			this.dynamicObject = viewData;
		}
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			string name = binder.Name;
			if (this.dynamicObject.ContainsKey(name))
			{
				result = this.dynamicObject[name];
			}
			else
			{
				result = null;
			}
			return true;
		}
		public override bool TrySetMember(SetMemberBinder binder, object c)
		{
			this.dynamicObject[binder.Name] = c;
			return true;
		}	
		private Dictionary<string, object> dynamicObject;
	}
}
