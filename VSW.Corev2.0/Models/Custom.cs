using System;
using System.Collections.Generic;
using VSW.Core.Global;
namespace VSW.Core.Models
{
	public class Custom
	{
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[0];
				if (this._item.Keys != null)
				{
					array = new string[this._item.Keys.Count];
					this._item.Keys.CopyTo(array, 0);
				}
				return array;
			}
		}
		public int Count
		{
			get
			{
				return this._item.Count;
			}
		}
		public object this[string Key]
		{
			get
			{
				return this.GetValue(Key).Current;
			}
			set
			{
				this.SetValue(Key, value);
			}
		}
		public bool Exists(string key)
		{
			return this._item.ContainsKey(key);
		}
		public Global.Object GetValue(string key)
		{
			VSW.Core.Global.Object result;
			if (!this._item.ContainsKey(key))
			{
				result = new VSW.Core.Global.Object();
			}
			else
			{
				result = new VSW.Core.Global.Object(this._item[key]);
			}
			return result;
		}
		public void Remove(string key)
		{
			if (this.Exists(key))
			{
				this._item.Remove(key);
			}
		}
		public void SetValue(string key, object value)
		{
			if (!string.IsNullOrEmpty(key) && value != null && value != DBNull.Value)
			{
				this._item[key] = value;
			}
		}
		private Dictionary<string, object> _item = new Dictionary<string, object>();
	}
}
