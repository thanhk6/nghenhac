using System;
using System.Collections.Generic;
using VSW.Core.Global;

namespace VSW.Core.Web
{

	public class VQS
	{
		public string BeginCode
		{
			get
			{
				return this.GetString(0);
			}
		}
		public int Count
		{
			get
			{
				return this.listURL.Count;
			}
		}
		public string EndCode
		{
			get
			{
				return this.GetString_End(0);
			}
		}
		public VQS()
		{
			this.listURL = new List<string>();
		}
		public VQS(string url)
		{
			this.listURL = new List<string>();
			if (!string.IsNullOrEmpty(url))
			{
				if (url.EndsWith("/"))
				{
					url = url.Substring(0, url.Length - 1);
				}
				int num = url.IndexOf('.');
				if (num > -1)
				{
					url = url.Substring(0, num);
				}
				this.listURL.AddRange(url.Split(new char[]
				{
					'/'
				}));
			}
		}
		public bool Equals(int index, string code)
		{
			return this.GetString(index).ToLower() == code.ToLower();
		}
		public string GetString(int index)
		{
			string result;
			if (this.nguyenthanh_1(index))
			{
				result = this.listURL[index].Trim();
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
		public string GetString_End(int index)
		{
			index = this.Count - index - 1;
			return this.GetString(index);
		}
		public VSW.Core.Global.Object GetValue(int index)
		{
			VSW.Core.Global.Object result;
			if (this.nguyenthanh_1(index))
			{
				result = new VSW.Core.Global.Object(this.listURL[index].Trim());
			}
			else
			{
				result = new VSW.Core.Global.Object();
			}
			return result;
		}
		public VSW.Core.Global.Object GetValue_End(int index)
		{
			index = this.Count - index - 1;
			return this.GetValue(index);
		}
		internal void nguyenthanh_0()
		{
			if (this.Count > 0)
			{
				this.listURL.RemoveAt(0);
			}
		}
		private bool nguyenthanh_1(int int_0)
		{
			return int_0 >= 0 && int_0 < this.Count;
		}
		public void Trunc(VQS vqs, int startIndex)
		{
			this.listURL = new List<string>();
			for (int i = startIndex; i < vqs.Count; i++)
			{
				this.listURL.Add(vqs.listURL[i]);
			}
		}
		private List<string> listURL;
	}
}
