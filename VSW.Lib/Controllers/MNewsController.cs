using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Tin tức", Code = "MNews", Order = 6)]
    public class MNewsController : Controller
    {
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|News")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 12;
        public void ActionIndex(MNewsModel model)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;               
            var dbQuery = ModNewsService.Instance.CreateQuery()
                  .Select(o => new { o.MenuID, o.Name, o.File, o.View, o.Code, o.Order, o.Content, o.Summary,o.Activity, })
                                    .Where(o => o.Activity == true)
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                   .Skip(PageSize * model.page)
                                    .Take(PageSize);

            if (model.sort == "news_desc") dbQuery.OrderByDesc(o => o.Published);
            else if (model.sort == "news_asc") dbQuery.OrderByAsc(o => o.Published);
            else dbQuery.OrderByDesc(o => new { o.Order, o.ID });
            ViewBag.Data = dbQuery.ToList_Cache();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;
            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
        public void ActionDetail(string endCode)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;
            var item = ModNewsService.Instance.CreateQuery()
               
                                .Where(o => o.Activity == true && o.Code == endCode)
                                .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                .ToSingle_Cache();
            if (item != null)
            {
              //  item.Summary = item.Summary2;
                //up view
                item.UpView();
                ViewBag.Other = ModNewsService.Instance.CreateQuery()
                      .Select(o => new { o.MenuID, o.Name, o.File, o.View, o.Code, o.Order, o.Content, o.Summary })
                                            .Where(o => o.Activity == true && o.ID != item.ID)
                                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                            .OrderByDesc(o => new { o.Order, o.ID })
                                            .Take(PageSize)
                                            .ToList_Cache();

                ViewPage.ViewBag.Data = ViewBag.Data = item;
                //SEO
                ViewPage.CurrentPage.PageTitle = string.IsNullOrEmpty(item.PageTitle) ? item.Name : item.PageTitle;
                ViewPage.CurrentPage.PageDescription = string.IsNullOrEmpty(item.PageDescription) ? item.Summary : item.PageDescription;
                ViewPage.CurrentPage.PageURL = ViewPage.GetURL(item.MenuID, item.Code);
                ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(item.File, 200, 200);

            //    ViewPage.CurrentPage.PageURL = ViewPage.GetURL(item.MenuID, item.Code);
            //    ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(item.File, 200, 200);
            //
            }
            else
            {
                ViewPage.Error404();
            }
        }
    }
    public class MNewsModel
    {
        private int _page;
        public int page
        {
            get { return _page; }
            set { _page = value - 1; }
        }
        public int TotalRecord { get; set; }
        public int PageSize { get; set; }

        public string sort { get; set; }
    }
}