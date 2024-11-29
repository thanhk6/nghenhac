using System;
using VSW.Core.Global;
using VSW.Core.Interface;
using VSW.Core.Web;

namespace VSW.Core.MVC
{
	public class CPViewPage : ViewPageBase
	{
		public string CPPath
		{
			get
			{
				return base.ApplicationPath + Setting.Sys_CPDir;
			}
		}
		public virtual IModuleInterface DefaultModule()
		{
			throw new Exception("DefaultModule not imp.");
		}
		public virtual IModuleInterface FindModule(string module)
		{
			throw new Exception("FindModule not imp.");
		}
		protected sealed override void InitializeCulture()
		{
			base.nguyenthanh_0();
			base.CurrentVQSMVC = new VQS(string.Empty);
			if (base.CurrentVQS.Count > 1)
			{
				base.CurrentVQSMVC.Trunc(base.CurrentVQS, 1);
			}
			base.ActionForm = base.Request.RawUrl;
			if (base.CurrentVQS.Count == 0)
			{
				base.CurrentModule = this.DefaultModule();
			}
			else
			{
				base.CurrentModule = this.FindModule(base.CurrentVQS.GetString(0));
				if (base.CurrentModule == null)
				{
					base.CurrentModule = this.DefaultModule();
				}
			}
			base.InitializeCulture();
			if (base.CurrentLang != null)
			{
				this.Page.Culture = base.CurrentLang.Code;
				this.Page.UICulture = base.CurrentLang.Code;
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (base.CurrentModule == null)
			{
				throw new Exception("CurrentModule = null");
			}
			Class @class = new Class(base.CurrentModule.ModuleType);
			base.Controller = (@class.Instance as Controller);
			base.Controller.InitPage(this);
			base.Controller.OnLoad();
			base.nguyenthanh_3(@class, null);
			base.nguyenthanh_1();
			base.nguyenthanh_2(true);
			base.nguyenthanh_4(Setting.Sys_CPDir + "/");
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.nguyenthanh_2(false);
			base.Controller.OnUnLoad();
			base.OnPreRender(e);
		}
		protected override string OnRender(string html)
		{
			html = html.Replace("{ActionForm}", base.ActionForm);
			html = html.Replace("{ApplicationPath}", base.ApplicationPath);
			html = html.Replace("{CPPath}", this.CPPath);
			return html;
		}
	}
}
