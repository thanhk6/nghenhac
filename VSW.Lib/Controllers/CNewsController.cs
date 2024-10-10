using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Tin tức", Code = "CNews", IsControl = true, Order = 2)]
    public class CNewsController : Controller
    {
        //[Core.MVC.PropertyInfo("Default[MenuID-true|PageID-true|PageID2-false|PageSize-true],Top[MenuID-true|PageID-true|PageID2-true|PageSize-true]")]
        //public string LayoutDefine;
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|News")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Vị trí", "ConfigKey|Mod.NewsState")]
        public int State;

        [Core.MVC.PropertyInfo("Trang")]
        public int PageID;

        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 2;

        public override void OnLoad()
        {
            ViewBag.Data = ModNewsService.Instance.CreateQuery()
                   .Select(o => new { o.MenuID, o.Name, o.File, o.View, o.Code, o.Order,o.Content,o.Summary,o.Activity,o.State,o.Published })
                                    .Where(o => o.Activity == true)
                                     .Where(o => (o.State & State) == State)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => new { o.View, o.ID })     
                                    .Take(PageSize)
                                    .ToList_Cache();

            ViewBag.WatchALot = ModNewsService.Instance.CreateQuery()
                  .Select(o => new { o.MenuID, o.Name, o.File, o.View, o.Code, o.Order, o.Content, o.Summary,o.State,o.Activity, o.Published })
                 .Where(o => o.Activity == true)
                 .Where(o => (o.State & State) == State)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                   .OrderByDesc(o => new { o.Order, o.ID })                                   
                                    .Take(PageSize)
                                    .ToList_Cache();
            ViewBag.Page = SysPageService.Instance.GetByID_Cache(PageID);
        }
    }
}