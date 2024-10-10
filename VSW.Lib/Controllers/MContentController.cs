using VSW.Lib.Global;
using VSW.Lib.MVC;
using Controller = VSW.Lib.MVC.Controller;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Bài viết", Code = "MContent", Order = 11)]
    public class MContentController : Controller
    {
        [Core.MVC.PropertyInfo("Hiển thị", "Mặc định|Index,Giới thiệu|About")]
        public string View = "Index";

        public void ActionIndex()
        {
            if (View != "Index")
                RenderView(View);
            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
    }
}
