using VSW.Core.Global;

namespace VSW.Lib.Global
{
    public class Setting : Core.Web.Setting
    {
        //facebook
        public static string FacebookClientId = Config.GetValue("Facebook.ClientID").ToString();

        public static string FacebookClientSecret = Config.GetValue("Facebook.ClientSecret").ToString();
        public static string FacebookRedirectUri = "http://" + Core.Web.HttpRequest.Host + "/login-fb/Facebook";  //Config.GetValue("Facebook.RedirectUri").ToString();

        //google
        public static string GoogleClientId = Config.GetValue("Google.ClientID").ToString();

        public static string GoogleClientSecret = Config.GetValue("Google.ClientSecret").ToString();
        public static string GoogleRedirectUri = "http://" + Core.Web.HttpRequest.Host + "/login-gg/Google";//Config.GetValue("Google.RedirectUri").ToString();

        public static string GoogleAPI = Core.Web.HttpRequest.IsLocal ? Config.GetValue("Google.LocalAPI").ToString() : Config.GetValue("Google.ServerAPI").ToString();
    }
}