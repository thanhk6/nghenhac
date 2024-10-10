using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Showroom", Code = "MShowroom", Order = 6)]
    public class MShowroomController : Controller
    {
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Showroom")]
        public int MenuID;

        public void ActionIndex()
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            ViewBag.Data = ModShowroomService.Instance.CreateQuery()

                                    .Where(o => o.Activity == true)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Showroom", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByAsc(o => new { o.Order, o.ID })
                                    .ToList_Cache();

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }

        public void ActionDetail(string endCode)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            var item = ModShowroomService.Instance.CreateQuery()
                                .Where(o => o.Activity == true && o.Code == endCode)
                                .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Showroom", MenuID, ViewPage.CurrentLang.ID))
                                .ToSingle_Cache();

            if (item != null)
            {
                ViewBag.Other = ModShowroomService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.ID != item.ID)
                                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Showroom", MenuID, ViewPage.CurrentLang.ID))
                                            .OrderByDesc(o => new { o.Order, o.ID })
                                            .ToList_Cache();

                ViewPage.ViewBag.Data = ViewBag.Data = item;

                //SEO
                ViewPage.CurrentPage.PageURL = ViewPage.GetURL(item.MenuID, item.Code);
                ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(item.File, 200, 200);
            }
            else
            {
                ViewPage.Error404();
            }
        }
    }
}