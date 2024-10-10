using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Sản phẩm", Code = "CProduct", IsControl = true, Order = 3)]
    public class CProductController : Controller
    {
        //[PropertyInfo("Default[MenuID-true|PageID-true|PageID2-false|PageSize-true],Home[MenuID-true|PageID-true|PageID2-true|PageSize-true]")]
        //public string LayoutDefine;
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Product")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Trang")]
        public int PageID;
        [Core.MVC.PropertyInfo("Vị trí", "ConfigKey|Mod.ProductState")]
        public int State;
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 6;
        public override void OnLoad()
        {
            ViewBag.Data = ModProductService.Instance.CreateQuery()
               .Select(o => new { o.ID, o.MenuID, o.Name, o.File,  o.View, o.Code, o.Order,o.Activity,o.State})
                                    .Where(o => o.Activity == true)
                                    .Where(State > 0, o => (o.State & State) == State)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => new { o.Order, o.ID })                                  
                                    .Take(PageSize)
                                    .ToList_Cache();


            ViewBag.FlashSale= ModProductService.Instance.CreateQuery()
                .Select(o=>new {o.ID,o.MenuID,o.Activity,o.State,o.Code,o.Order,o.File})
                                    .Where(o => o.Activity == true)
                                    .Where(State > 0, o => (o.State & State)== 6)                                   
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => new { o.Order, o.ID })
                                    .Take(PageSize)
                                    .ToList_Cache();
             ViewBag.Page = SysPageService.Instance.GetByID_Cache(PageID);
        }
    }
}