using System;
using System.Collections.Generic;
using VSW.Core.Global;

namespace VSW.Core.Web
{	
	public class Setting
	{
		
		internal static readonly string Key = "6926F068-0EDC-4F90-A9BF-B743F65C5FC3";

		
		internal static readonly string Copyright = "IT Thanh ĐT:0964153988";

		
		public static readonly bool Sys_Copyright = Config.GetValue("Sys.Copyright").ToBool(false);

	
		public static readonly bool Sys_CompressionHtml = Config.GetValue("Sys.CompressionHtml").ToBool();

	
		public static readonly string Sys_DomainCookies = Config.GetValue("Sys.DomainCookies").ToString();


		public static readonly string Sys_CPDir = Config.GetValue("Sys.CPDir").ToString();


		public static readonly bool Sys_MultiPath = Config.GetValue("Sys.MultiPath").ToBool();


		public static readonly bool Sys_MultiSite = Config.GetValue("Sys.MultiSite").ToBool();


		public static readonly string Sys_PageExt = Config.GetValue("Sys.PageExt").ToString();

	

		public static readonly string Sys_SiteID = Config.GetValue("Sys.SiteID").ToString();


		public static readonly bool Sys_Mobile = Config.GetValue("Sys.Mobile").ToBool();

	
		public static readonly bool Sys_Tablet = Config.GetValue("Sys.Tablet").ToBool();


		public static readonly bool Sys_TabletAsMobile = Config.GetValue("Sys.TabletAsMobile").ToBool();


		public static readonly bool Sys_MobileDebug = Config.GetValue("Sys.MobileDebug").ToBool();


		public static readonly bool Sys_TabletDebug = Config.GetValue("Sys.TabletDebug").ToBool();


		public static readonly string Mod_DomainCookies = Config.GetValue("Mod.DomainCookies").ToString();

		public static readonly bool Mod_WriteError = Config.GetValue("Mod.WriteError").ToBool(true);

		public static readonly int Mod_CPTimeout = Config.GetValue("Mod.CPTimeout").ToInt();

		public static readonly bool Mod_LangUnABC = Config.GetValue("Mod.LangUnABC").ToBool();

		public static readonly bool Sys_SSLMode = Config.GetValue("Sys.SSLMode").ToBool(false);
		public static readonly bool Sys_WWWMode = Config.GetValue("Sys.WWWMode").ToBool(false);
		public static readonly string Sys_HideDirectory = Config.GetValue("Sys.HideDirectory").ToString();
		public static readonly List<object> listHideDirectory = VSW.Core.Global.Array.ToList(VSW.Core.Global.Array.ToString(Setting.Sys_HideDirectory));
	}
}
