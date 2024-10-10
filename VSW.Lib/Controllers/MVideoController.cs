using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Video", Code = "MVideo", Order = 2)]
    public class MVideoController : Controller
    {
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Video")]
        public int MenuID;

        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 5;
        
        int WebUserID = 0;
        public void ActionIndex(MVideoModel model)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            if (ViewPage.CurrentWebUser != null)
                WebUserID = ViewPage.CurrentWebUser.ID;

            var dbQuery = ModVideoService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.WebUserID == WebUserID)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Video", MenuID, ViewPage.CurrentLang.ID))
                                    .Skip(PageSize * model.page)
                                    .Take(PageSize)
                                    .OrderByDesc(o => new { o.Order, o.ID });

            ViewBag.Data = dbQuery.ToList_Cache();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;

            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = "http://" + ViewPage.Request.Url.Host + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }

        public void ActionDetail(string endCode)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            if (ViewPage.CurrentWebUser != null)
                WebUserID = ViewPage.CurrentWebUser.ID;

            var item = ModVideoService.Instance.CreateQuery()
                                .Where(o => o.Activity == true && o.Code == endCode && o.WebUserID == WebUserID)
                                .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                .ToSingle();

            if (item != null)
            {
                //up view
                item.UpView();

                ViewBag.Other = ModVideoService.Instance.CreateQuery()
                                            .Where(o => o.Activity == true && o.WebUserID == WebUserID && o.ID != item.ID)
                                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                            .OrderByDesc(o => new { o.Order, o.ID })
                                            .Take(PageSize)
                                            .ToList_Cache();

                ViewPage.ViewBag.Data = ViewBag.Data = item;

                //SEO
                ViewPage.CurrentPage.PageURL = ViewPage.GetURL(item.MenuID, item.Code);
                ViewPage.CurrentPage.PageFile = "http://" + ViewPage.Request.Url.Host + Utils.GetCropFile(item.Thumbnail, 200, 200);
            }
            else
            {
                ViewPage.Error404();
            }
        }
    }

    public class MVideoModel
    {
        private int _page;
        public int page
        {
            get { return _page; }
            set { _page = value - 1; }
        }

        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
    }
}