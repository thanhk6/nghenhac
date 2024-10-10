using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Video", Code = "CVideo", IsControl = true, Order = 4)]
    public class CVideoController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|Video")]
        public int MenuID;

        [VSW.Core.MVC.PropertyInfo("Trang")]
        public int PageID;

        [VSW.Core.MVC.PropertyInfo("Vị trí", "ConfigKey|Mod.VideoState")]
        public int State;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 4;

        //[VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        //public string Title = string.Empty;

        public override void OnLoad()
        {
            ViewBag.Data = ModVideoService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .Where(State > 0, o => (o.State & State) == State)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Video", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => new { o.Order, o.ID })
                                    .Take(PageSize)
                                    .ToList_Cache();

            ViewBag.Page = SysPageService.Instance.GetByID_Cache(PageID);
        }
    }
}
