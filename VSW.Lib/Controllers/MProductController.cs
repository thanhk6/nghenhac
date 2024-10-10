using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Sản phẩm", Code = "MProduct", Order = 3)]
    public class MProductController : Controller
    {
        [Core.MVC.PropertyInfo("Chuyên mục", "Type|Product")]
        public int MenuID;
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 12;
        [Core.MVC.PropertyInfo("Giao diện chi tiết", "Chi tiết|2")]
        public int TemplateID = 36;
        [Core.MVC.PropertyInfo("Thương hiệu", "Type|Brand")]
        public int BrandID;
        [Core.MVC.PropertyInfo("Vị trí", "ConfigKey|Mod.ProductState")]
        public int State;

        public void ActionIndex(MProductModel model)
        {
            //var a = ViewPage.CurrentPage.TemplateID ;
            //if (a==1047)
            //{
            //    var tempate = SysTemplateService.Instance.GetByID(47);
            //    ViewPage.ChangeTemplate(tempate);
            //}         
            var listChildItem = SysPageService.Instance.GetByID(ViewPage.CurrentPage.ID);
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;
            int BrandID = ViewPage.CurrentPage.BrandID;
            string keyword = !string.IsNullOrEmpty(model.keyword) ? Data.GetCode(model.keyword) : string.Empty;

            var dbQuery = ModProductService.Instance.CreateQuery()
                .Select(o => new { o.ID, o.MenuID, o.Name, o.File, o.View, o.Code, o.Order, o.Activity, o.State })
                                    .Where(o => o.Activity == true)
                                    .Where(State > 0, o => o.State == State)
                                    .Where(!string.IsNullOrEmpty(keyword), o => (o.Model.Contains(keyword) || o.Code.Contains(keyword)))
                                    .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID));

            ViewBag.Data = dbQuery.Skip(PageSize * model.page)
                                    .Take(PageSize).ToList_Cache();
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
            var item = ModProductService.Instance.CreateQuery()
                                .Where(o => o.Activity == true && o.Code == endCode)
                                .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                .ToSingle_Cache();

           // if(item==null)re

            if (item != null)
            {
                var tempate = SysTemplateService.Instance.GetByID(TemplateID);
               
                if (tempate != null)
                    ViewPage.ChangeTemplate(tempate);

                if (string.IsNullOrEmpty(item.Model))
                    item.Model = "homeled-" + item.ID;
                //up view
                item.UpView();
                ViewBag.Other = ModProductService.Instance.CreateQuery()
                    .Select(o => new {o.Name,o.MenuID,o.Code,o.File,o.Order,o.Activity})
                                            .Where(o => o.Activity == true && o.ID != item.ID)
                                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product", MenuID, ViewPage.CurrentLang.ID))
                                            .OrderByDesc(o => new { o.Order, o.ID })
                                            .ToList_Cache();
                ViewPage.ViewBag.Data = ViewBag.Data = item;

                var listComment = ModCommentService.Instance.CreateQuery()
                   .Where(o => o.Activity == true && o.ProductID == item.ID && o.ParentID == 0);
                //ViewBag.CommentNormal = ModCommentNormalService.Instance.CreateQuery()
                //                                        .Where(o => o.Activity == true && o.ProductID == item.ID && o.ParentID == 0)
                //                                        .OrderByDesc(o => o.ID)
                //                                        .ToList_Cache();
                var dbClone = listComment.Clone();
                ViewBag.Comment = listComment.OrderByDesc(o => o.ID).ToList();
                ViewBag.CountComment = dbClone.Select(o => o.ID).Count().ToValue().ToInt(0);

                //SEO
                ViewPage.CurrentPage.PageKeywords = item.PageKeywords;
              
               
                ViewPage.CurrentPage.PageURL = ViewPage.GetURL(item.MenuID, item.Code);
                ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(item.File, 200, 200);          
            }
            else
            {
                ViewPage.Error404();
            }
        }
    }

    public class MProductModel
    {
        private int _page;
        public int page
        {
            get { return _page; }
            set { _page = value - 1; }
        }

        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
        public string keyword { get; set; }
        public long min { get; set; }
        public long max { get; set; }
    }
}