using System;
using VSW.Core.Models;

namespace VSW.Core.Web
{
	public class ViewState : Custom
	{
		public ViewState()
		{
		}
		public ViewState(bool autoUpdate)
		{
			if (autoUpdate)
			{
				this.NguyenThanh_0();
			}
		}
		private void NguyenThanh_0()
		{
			this.UpdateQueryString();
			this.UpdateForm();
		}
		public void UpdateForm()
		{
			try
			{
				string[] allKeys = HttpForm.AllKeys;
				int num = 0;
				while (allKeys != null && num < allKeys.Length)
				{
					if (!(allKeys[num] == "__VIEWSTATE") && !(allKeys[num] == "__VIEWSTATEENCRYPTED") && !(allKeys[num] == "__EVENTVALIDATION") && allKeys[num] != null)
					{
						base.SetValue(allKeys[num], HttpForm.GetValue(allKeys[num]).ToString());
					}
					num++;
				}
			}
			catch
			{
			}
		}
		public void UpdateQueryString()
		{
			try
			{
				string[] allKeys = HttpQueryString.AllKeys;
				int num = 0;
				while (allKeys != null && num < allKeys.Length)
				{
					if (allKeys[num] != null)
					{
						base.SetValue(allKeys[num], HttpQueryString.GetValue(allKeys[num]).ToString());
					}
					num++;
				}
			}
			catch
			{
			}
		}
	}
}
