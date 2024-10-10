using System;
using System.Text.RegularExpressions;
using System.Web;
using VSW.Core.Web;
namespace VSW.Core.Global
{
	public abstract class Device
	{
		private static bool smethod_0()
		{
			string text = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
			return !string.IsNullOrEmpty(text) && text.Length >= 4 && (regex_0.IsMatch(text) || Device.regex_2.IsMatch(text.Substring(0, 4)));
		}
		private static bool smethod_1()
		{
			string text = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
			return !string.IsNullOrEmpty(text) && text.Length >= 4 && (regex_1.IsMatch(text) || Device.regex_2.IsMatch(text.Substring(0, 4)));
		}
		private static bool MobileMode
		{
			get
			{
				return !VSW.Core.Web.HttpRequest.CPMode && (Setting.Sys_MobileDebug || (Setting.Sys_Mobile && Device.smethod_0()) || (Setting.Sys_TabletAsMobile && Device.TabletMode));
			}
		}
		private static bool TabletMode
		{
			get
			{
				return !VSW.Core.Web.HttpRequest.CPMode && (Setting.Sys_TabletDebug || (Setting.Sys_Tablet && Device.smethod_1()));
			}
		}
		private static bool CPMobileMode
		{
			get
			{
				return VSW.Core.Web.HttpRequest.CPMode && (Setting.Sys_MobileDebug || (Setting.Sys_Mobile && Device.smethod_0()) || (Setting.Sys_TabletAsMobile && Device.TabletMode));
			}
		}
		private static bool CPTabletMode
		{
			get
			{
				return VSW.Core.Web.HttpRequest.CPMode && (Setting.Sys_TabletDebug || (Setting.Sys_Tablet && Device.smethod_1()));
			}
		}
		public static int Mode
		{
			get
			{
				int num = Convert.ToInt(Session.GetValue("CustomDevice"));
				int result;
				if (num > 0)
				{
					result = num;
				}
				else
				{
					if (Device.MobileMode)
					{
						Device.int_0 = 1;
					}
					else if (Device.CPMobileMode)
					{
						Device.int_0 = 2;
					}
					else if (Device.TabletMode)
					{
						Device.int_0 = 3;
					}
					else if (Device.CPTabletMode)
					{
						Device.int_0 = 4;
					}
					else
					{
						Device.int_0 = 5;
					}
					Session.SetValue("CustomDevice", Device.int_0);
					result = Device.int_0;
				}
				return result;
			}
		}
		public static bool Mobile
		{
			get
			{
				return Device.Mode == 1;
			}
		}
		public static bool CPMobile
		{
			get
			{
				return Device.Mode == 2;
			}
		}
		public static bool Tablet
		{
			get
			{
				return Device.Mode == 3;
			}
		}
		public static bool CPTablet
		{
			get
			{
				return Device.Mode == 4;
			}
		}
		public static bool Desktop
		{
			get
			{
				return Device.Mode == 5;
			}
		}
		private static readonly Regex regex_0 = new Regex("(android|bb\\d+|meego).+mobile|avantgo|bada\\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

		private static readonly Regex regex_1 = new Regex("ipad|android|android 3.0|xoom|sch-i800|playbook|tablet|kindle|nexus", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

		private static readonly Regex regex_2 = new Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

		private static int int_0 = 0;
	}
}
