using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Quảng cáo / Liên kết", Code = "CAdv", IsControl = true, Order = 1)]
    public class CAdvController : Controller
    {
        [Core.MVC.PropertyInfo("Default[MenuID-true|MultiRecord-true],SlideSub[MenuID-false|MultiRecord-false]")]
        public string LayoutDefine;

        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Adv")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Dữ liệu")]
        public bool MultiRecord = true;
        public override void OnLoad()
        {
            if (!MultiRecord)
                ViewBag.Data = ModAdvService.Instance.CreateQuery()
                      .Select(o => new { o.MenuID, o.Name, o.File, o.Order,o.URL,o.Activity})
                                        .Where(o => o.Activity == true && o.MenuID == MenuID)
                                        .OrderByAsc(o => new { o.Order, o.ID })
                                        .Take(1)
                                        .ToSingle_Cache();
            else
                ViewBag.Data = ModAdvService.Instance.CreateQuery()
                      .Select(o => new { o.MenuID, o.Name, o.File,o.URL,o.Order,o.Activity })
                                            .Where(o => o.Activity == true && o.MenuID == MenuID)
                                            .OrderByAsc(o => new { o.Order, o.ID })
                                            .ToList_Cache();
            ViewBag.MenuID = MenuID;
        }
    }
}