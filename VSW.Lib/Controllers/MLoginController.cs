using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Đăng nhập", Code = "MLogin", Order = 99)]
    public class MLoginController : Controller
    {
        public void ActionIndex(MLoginModel model)
        {
            ViewBag.Model = model;

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
        public void ActionAddPOST(MLoginModel model)
        {
            ViewBag.Model = model;

            var webUser = ModWebUserService.Instance.GetForLogin(model.Email, Security.Md5(model.Password));
            if (webUser == null)
            {
                ViewPage.Alert("Bạn chưa đăng nhập được.");
                return;
            }
            WebLogin.SetLogin(webUser.ID, model.IsSave);
            ViewPage.Response.Redirect(string.IsNullOrEmpty(model.returnpath) ? "/" : model.returnpath);
        }
    }
    public class MLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsSave { get; set; }
        public string returnpath { get; set; }
    }
}