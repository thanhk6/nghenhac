using VSW.Lib.Global;

namespace VSW.Lib.MVC
{
    public class ViewControl : Core.MVC.ViewControl
    {
        public ViewPage ViewPage => Page as ViewPage;

        protected string GetPagination(int pageIndex, int pageSize, int totalRecord)
        {
            return GetPagination(ViewPage.CurrentURL, pageIndex, pageSize, totalRecord);
        }


        protected string GetPagination(string url, int pageIndex, int pageSize, int totalRecord)
        {
            var pager = new Pager { Url = url, PageIndex = pageIndex, PageSize = pageSize, TotalRecord = totalRecord };

            pager.Update();

            return pager.Html;
        }
    }
}