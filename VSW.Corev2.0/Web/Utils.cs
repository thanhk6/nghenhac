using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Core.Global;

namespace VSW.Core.Web
{
	public class Utils
	{
		public static string GetHtmlControl(Page viewPage, string controlPath, params object[] Params)
		{
			string result;
			if (viewPage == null)
			{
				result = string.Empty;
			}
			else
			{
				UserControl userControl = viewPage.LoadControl(controlPath) as UserControl;
				Panel panel = new Panel();
				viewPage.Controls.Add(panel);
				if (Params != null && Params.Length != 0)
				{
					Class @class = new Class(userControl);
					for (int i = 0; i < Params.Length; i += 2)
					{
						string propertyName = Params[i].ToString();
						object propertyValue = Params[i + 1];
						if (@class.ExistsProperty(propertyName))
						{
							@class.SetProperty(propertyName, propertyValue);
						}
					}
					panel.Controls.Add((UserControl)@class.Instance);
				}
				else
				{
					panel.Controls.Add(userControl);
				}
				StringBuilder stringBuilder = new StringBuilder();
				StringWriter stringWriter = new StringWriter(stringBuilder);
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
				panel.RenderControl(htmlTextWriter);
				string text = stringBuilder.ToString();
				stringWriter.Close();
				htmlTextWriter.Close();
				viewPage.Controls.Remove(panel);
				result = text;
			}
			return result;
		}
	}
}
