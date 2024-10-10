using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Thương hiệu", Code = "CBrand", IsControl = true, Order = 2)]
    public class CBrandController : Controller
    {
        //[Core.MVC.PropertyInfo("Default[MenuID-true|PageID-true|PageID2-false|PageSize-true],Top[MenuID-true|PageID-true|PageID2-true|PageSize-true]")]
        //public string LayoutDefine;
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Brand")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Trang")]
        public int PageID;
        public override void OnLoad()
        {
            ViewBag.Data = ModBrandService.Instance.CreateQuery()
                .Select(o=> new { o.ID,o.MenuID,o.Code,o.Name,o.Order})
                                    .Where(o => o.Activity == true)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Brand", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByAsc(o => new { o.Order, o.ID })
                                    .ToList_Cache();
            ViewBag.Page = SysPageService.Instance.GetByID_Cache(PageID);
        }
    }
}