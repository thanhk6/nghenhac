using System;
using System.IO;
using System.Text;
using System.Web.UI;
using VSW.Core.Global;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Core.MVC;
using VSW.Core.Web;
namespace VSW.Core.Design
{
	public class ViewPageDesign : Page
	{
		public string ApplicationPath
		{
			get
			{
				return HttpRequest.ApplicationPath;
			}
		}
		internal bool Boolean_0 { get; set; }
		internal bool Boolean_1 { get; set; }
		public string CPPath
		{
			get
			{
				return this.ApplicationPath + Setting.Sys_CPDir;
			}
		}
		public virtual IPageInterface CurrentPage { get; protected set; }
		public virtual ITemplateInterface CurrentTemplate { get; protected set; }
		public IModuleServiceInterface ModuleService { get; protected set; }
		public IPageServiceInterface PageService { get; protected set; }
		public ViewState PageViewState { get; private set; }
		public ITemplateServiceInterface TemplateService { get; protected set; }
		public ViewPageDesign()
		{
			this.PageViewState = new ViewState();
		}
		private void BuildDesign(Control control)
		{
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				if (control2 is StaticControl)
				{
					StaticControl staticControl = control2 as StaticControl;
					string controlPath = File.Exists(base.Server.MapPath("~/Views/Design/" + staticControl.Code + ".ascx")) ? ("~/Views/Design/" + staticControl.Code + ".ascx") : ("~/" + Setting.Sys_CPDir + "/Design/EditControl.ascx");
					object[] @params = new object[]
					{
						"Code",
						staticControl.Code,
						"VSWID",
						staticControl.VSWID,
						"CphName",
						string.Empty
					};
					this.BuildDesign(staticControl, controlPath, @params);
				}
				else if (!(control2 is DynamicControl))
				{
					if (control2.HasControls())
					{
						this.BuildDesign(control2);
					}
				}
				else
				{
					DynamicControl dynamicControl = control2 as DynamicControl;
					string controlPath2 = "~/" + Setting.Sys_CPDir + "/Design/EditControlBegin.ascx";
					object[] params2 = new object[]
					{
						"CphName",
						dynamicControl.Code
					};
					this.BuildDesign(dynamicControl, controlPath2, params2);
					this.BuildDesign(control2, dynamicControl.Code, this.CurrentCustom.GetValue("Template_" + dynamicControl.Code).ToString());
					controlPath2 = "~/" + Setting.Sys_CPDir + "/Design/EditControlEnd.ascx";
					this.BuildDesign(dynamicControl, controlPath2, params2);
				}
			}
		}
		private void BuildDesign(Control control, string customValue, string designCode)
		{
			if (designCode == string.Empty)
			{
				string controlPath = "~/" + Setting.Sys_CPDir + "/Design/EditControl.ascx";
				object[] @params = new object[]
				{
					"Code",
					"VSWMODULE",
					"VSWID",
					string.Empty,
					"CphName",
					customValue,
					"Visible",
					false
				};
				this.BuildDesign(control, controlPath, @params);
				return;
			}
			string[] array = designCode.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				string text = array2[0].Trim();
				string text2 = (array2.Length == 2) ? array2[1] : string.Empty;
				if (!(text == string.Empty))
				{
					if (text != "VSWMODULE")
					{
						int num = 2;
						if (i == 0)
						{
							num = 1;
						}
						if (i > 0 && i == array.Length - 1)
						{
							num = 4;
						}
						if (i == 0 && i == array.Length - 1)
						{
							num = 5;
						}
						string controlPath2 = File.Exists(base.Server.MapPath("~/Views/Design/" + text + ".ascx")) ? ("~/Views/Design/" + text + ".ascx") : ("~/" + Setting.Sys_CPDir + "/Design/EditControl.ascx");
						object[] array3 = new object[8];
						array3[0] = "PosID";
						array3[1] = num;
						array3[2] = "Code";
						array3[3] = text;
						array3[4] = "VSWID";
						object[] array4 = array3;
						array4[5] = text2;
						array4[6] = "CphName";
						array4[7] = customValue;
						this.BuildDesign(control, controlPath2, array4);
					}
					else
					{
						string controlPath3 = "~/" + Setting.Sys_CPDir + "/Design/EditControl.ascx";
						object[] params2 = new object[]
						{
							"Code",
							"VSWMODULE",
							"VSWID",
							string.Empty,
							"CphName",
							customValue
						};
						this.BuildDesign(control, controlPath3, params2);
					}
				}
			}
		}
		private void BuildDesign(Control control, string controlPath, params object[] Params)
		{
			Control control2 = base.LoadControl(controlPath);
			if (Params != null && Params.Length != 0)
			{
				Class @class = new Class(control2);
				for (int i = 0; i < Params.Length - 1; i++)
				{
					string propertyName = Params[i].ToString();
					int num = i + 1;
					i = num;
					object propertyValue = Params[num];
					if (@class.ExistsProperty(propertyName))
					{
						@class.SetProperty(propertyName, propertyValue);
					}
				}
			}
			control.Controls.Add(control2);
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.CurrentTemplate == null)
			{
				base.Response.End();
				return;
			}
			string[] allKeys = this.CurrentTemplate.Items.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				if (allKeys[i].StartsWith("Template_"))
				{
					this.CurrentCustom.SetValue(allKeys[i], this.CurrentTemplate.Items.GetValue(allKeys[i]).Current);
				}
			}
			if (this.CurrentPage != null)
			{
				allKeys = this.CurrentPage.Items.AllKeys;
				for (int j = 0; j < allKeys.Length; j++)
				{
					if (allKeys[j].StartsWith("Template_"))
					{
						this.CurrentCustom.SetValue(allKeys[j], this.CurrentPage.Items.GetValue(allKeys[j]).Current);
					}
				}
			}
			this.BuildDesign(this);
		}
		protected sealed override void Render(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			base.Render(new HtmlTextWriter(new StringWriter(stringBuilder)));
			string text = stringBuilder.ToString().Replace("{ApplicationPath}", this.ApplicationPath).Replace("{CPPath}", this.CPPath);
			writer.Write(string.Concat(new object[]
			{
				text,
				"\r\n<!-- Copyright © by ",
				Setting.Copyright,
				" -->"
			}));
		}
		private Custom CurrentCustom = new Custom();
	}
}
