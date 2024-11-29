using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Ý kiến khách hàng", Code = "CComment", IsControl = true, Order = 2)]
    public class CCommentController : Controller
    {
        [Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public override void OnLoad()
        {
            ViewBag.Data = ModCommentService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .OrderByDesc(o => new { o.Order, o.ID })
                                    .Take(PageSize)
                                    .ToList_Cache();
        }
    }
}