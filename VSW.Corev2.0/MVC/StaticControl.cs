using System;
using System.Web.UI;

namespace VSW.Core.MVC
{
	public class StaticControl : Control
	{
		public string Code { get; set; }
		public string DefaultAction { get; set; }
		public string DefaultLayout { get; set; }
		public string DefaultProperties { get; set; }
		public string VSWID { get; set; }
	}
}
