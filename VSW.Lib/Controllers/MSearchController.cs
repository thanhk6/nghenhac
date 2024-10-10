using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: Tìm kiếm", Code = "MSearch", Order = 50)]
    public class MSearchController : Controller
    {
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 20;
        public void ActionIndex(MSearchModel model)
        {
            string keyword = !string.IsNullOrEmpty(model.keyword) ? Data.GetCode(model.keyword) : string.Empty;
            //string keyword = model.keyword;
            var dbQuery = ModProductService.Instance.CreateQuery()
                  .Select(o => new { o.MenuID, o.Name, o.File, o.Code, o.Order, o.Price, o.Price2 })
                                    .Where(o => o.Activity == true)
                                    .Where(!string.IsNullOrEmpty(keyword), o => (o.Code.Contains(keyword)|| o.Name.Contains(keyword)||o.KeyWordSearch.Contains(keyword)))
                                    .OrderByDesc(o => new { o.ID })
                                    .Take(PageSize)
                                    .Skip(PageSize * model.page);

            if (model.min > 0)
                dbQuery.Where(o => o.Price >= model.min);
            if (model.max > 0)
                dbQuery.Where(o => o.Price < model.max);
            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;
            //SEO
            ViewPage.CurrentPage.PageURL = ViewPage.CurrentURL;
            ViewPage.CurrentPage.PageFile = Core.Web.HttpRequest.Domain + Utils.GetCropFile(ViewPage.CurrentPage.File, 200, 200);
        }
    }    
    public class MSearchModel
    {
        private int _page;
        public int page
        {
            get { return _page; }
            set { _page = value - 1; }
        }
        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
        public int State { get; set; }
        public string keyword { get; set; }
        public long min { get; set; }
        public long max { get; set; }
    }
}
