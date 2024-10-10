using VSW.Lib.MVC;
using VSW.Lib.Models;
using System.Collections.Generic;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Thuộc tính sản phẩm", Code = "CProperty", IsControl = true, Order = 2)]
    public class CPropertyController : Controller
    {
        Dictionary<WebPropertyEntity, List<WebPropertyEntity>> dicItem;
        public override void OnLoad()
        {
            if (ViewPage.CurrentModule.Code != "MProduct")

                return;
            var menu = WebMenuService.Instance.GetByID_Cache(ViewPage.CurrentPage.MenuID);

            if (menu == null || menu.PropertyID < 1)
                return;
            var listItem = WebPropertyService.Instance.CreateQuery()
                                                .Select(o => new { o.Name, o.ID })
                                                .Where(o => o.Activity == true && o.ParentID == menu.PropertyID)
                                                .OrderByAsc(o => o.Order)
                                                .ToList_Cache();


            if (listItem == null)
                return;
            var ArrAtr = ViewPage.PageViewState.Exists("atr") ? Core.Global.Array.ToInts(ViewPage.PageViewState.GetValue("atr").ToString().Trim(), '-') : null;
            ViewBag.ArrAtr = ArrAtr;
            dicItem = new Dictionary<WebPropertyEntity, List<WebPropertyEntity>>();
            for (var i = 0; listItem != null && i < listItem.Count; i++)
            {
                var listChildItem = WebPropertyService.Instance.CreateQuery()
                                                    .Select(o => new { o.Name, o.ID })
                                                //  .WhereIn(o => o.ID, ModPropertyService.Instance.CreateQuery().Select(o => o.ProductID).Distinct())
                                                    .Where(o => o.Activity == true && o.ParentID == listItem[i].ID)
                                                    .OrderByAsc(o => o.Order)
                                                    .ToList_Cache();
                if (listChildItem == null)
                    continue;
                for (var j = listChildItem.Count - 1; j > -1; j--)
                {
                    listChildItem[j].Selected = false;
                    int count = ModPropertyService.Instance.CreateQuery().Select(o => o.ID).Where(o => o.PropertyValueID == listChildItem[j].ID).Count().ToValue_Cache().ToInt(0);
                    if (count < 1)
                        listChildItem.RemoveAt(j);
                    else
                        listChildItem[j].Count = count;

                    //if (ArrAtr != null && System.Array.IndexOf(ArrAtr, listChildItem[j].ID) > -1)
                    //    listChildItem[j].Selected = true;
                }
                dicItem[listItem[i]] = listChildItem;
            }

            ViewBag.Data = dicItem;
        }

        private static List<WebPropertyEntity> Remove(List<WebPropertyEntity> list, WebPropertyEntity item)
        {
            var listItem = new List<WebPropertyEntity>(list.Count - 1);
            foreach (var o in list)
            {
                if (o.ID != item.ID)
                    listItem.Add(o);
            }
            return listItem;
        }
    }
}
