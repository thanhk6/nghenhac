using VSW.Lib.Models;
using VSW.Lib.MVC;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Menu", Code = "CMenu", IsControl = true, Order = 2)]
    public class CMenuController : Controller
    {
        //[Core.MVC.PropertyInfo("Default[PageID-true|MenuIDMF-false|MenuIDCT-false|State-false],Top[PageID-true|MenuIDMF-true|MenuIDCT-true|State-true],MobileHeader[PageID-true|MenuIDMF-true|MenuIDCT-true|State-true]")]
        //public string LayoutDefine;

        [Core.MVC.PropertyInfo("Trang")]
        public int PageID;
        [Core.MVC.PropertyInfo("Giao diện chi tiết", "Chi tiết|2")]
        public int TemplateID = 47;
        public override void OnLoad()
        {
            var page = SysPageService.Instance.GetByID_Cache(PageID);

            if (ViewLayout =="TopNew")
            {
                ViewBag.Data = SysPageService.Instance.CreateQuery()
                    .Select(o=>new { o.ID, o.Activity, o.State, o.Name, o.Code, o.Summary, o.Order, o.File })
                                        .Where(o => o.LangID == ViewPage.CurrentLang.ID && o.Activity == true && o.ShowMenuTop == true)
                                        .OrderByAsc(o => new { o.Order, o.ID })
                                        .ToList_Cache();
            }
           else if (page != null)
            {
               
                ViewBag.Data = SysPageService.Instance.GetByParent_Cache(page.ID);



                ViewBag.topmobile = SysPageService.Instance.GetByParent_Cachemobile(page.ID);
            }

            else
            {
                ViewBag.Data = SysPageService.Instance.CreateQuery()
                    .Select(o => new {o.ID,o.Name,o.Order,o.File,o.Code,o.Activity,o.State})
                                        .Where(o => o.LangID == ViewPage.CurrentLang.ID && o.Activity == true)
                                        .OrderByAsc(o => new { o.Order, o.ID })                                                
                                        .ToList_Cache();
            }
            //ViewBag.Page = page;
        }
    }
}