using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Thương hiệu", Code = "MBrand", Order = 6)]
    public class MAuthorController : Controller
    {
        [Core.MVC.PropertyInfo("Thương hiệu", "Type|Brand")]
        public int MenuID;
        //[Core.MVC.PropertyInfo("Giao diện chi tiết", "Chi tiết|2")]
        //public int TemplateID = 34;
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 20;
        public void ActionIndex()
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;
            var page = SysPageService.Instance.GetByParent_Cache(112);
            ViewBag.Data = page;
            //ViewBag.Data = SysPageService.Instance.CreateQuery()
            //                       .Where(o => o.Activity == true && o.BrandID > 0 && o.BrandID == MenuID)
            //                       .Where(o => o.FileBrand != null && o.FileBrand != string.Empty)
            //                        .OrderByAsc(o => new { o.Order, o.MenuID })                                   
            //                         .Take(PageSize)
            //                       .ToList_Cache();
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
      

    }

}


