namespace VSW.Lib.Global
{
    public class Pager
    {
        private int _totalRecord = -1;
        private int _pageSize = 5;
        private int _pageMax = 5;

        public int TotalRecord
        {
            get => _totalRecord;
            set
            {
                if (value > 0)
                    _totalRecord = value;
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > 0)
                    _pageSize = value;
            }
        }
        public int PageIndex { get; set; }

        public int PageMax
        {
            get => _pageMax;
            set
            {
                if (value > 0)
                    _pageMax = value;
            }
        }
        public int Skip => PageIndex * PageSize;

        public int TotalBegin { get; private set; }

        public int TotalEnd { get; private set; }
        public int TotalPage
        {
            get
            {
                if (PageSize == 0)
                    return 0;

                return (TotalRecord % PageSize == 0 ? 0 : 1) + (TotalRecord / PageSize);
            }
        }
        public string Url { get; set; } = string.Empty;

        public string ParamName { get; set; } = "page";

        public string ActionName { get; set; } = "";

        public string CssClass { get; set; } = string.Empty;

        public string BackText { get; set; } = "Trang trước";

        public string NextText { get; set; } = "Trang sau";

        public string BackEndText { get; set; } = "Trang đầu";

        public string NextEndText { get; set; } = "Trang cuối";

        public bool IsCpLayout { get; set; }

        public bool DisableMode { get; set; }

        public string Html { get; private set; } = string.Empty;
        public void Update()
        {
            var pageIndex = PageIndex;
            var minPage = pageIndex / _pageMax * _pageMax;
            var maxPage = minPage + _pageMax;

            var maxPageIndex = _totalRecord / ((double)_pageSize);
            TotalBegin = pageIndex * _pageSize;
            TotalEnd = TotalBegin + _pageSize;

            if (maxPageIndex - pageIndex < 1)
                TotalEnd = _totalRecord;

            var url = Url;

            string[] allKey = Core.Web.HttpQueryString.AllKeys;
            for (var i = 0; i < allKey.Length; i++)
            {
                var key = allKey[i].Trim();
                if (string.IsNullOrEmpty(key) || key.Equals("page", System.StringComparison.OrdinalIgnoreCase)) continue;

                var value = Core.Web.HttpQueryString.GetValue(key).ToString().Trim();

                if (!url.Contains("?"))
                    url += "?" + key + "=" + System.Web.HttpContext.Current.Server.UrlEncode(value);
                else
                    url += "&" + key + "=" + System.Web.HttpContext.Current.Server.UrlEncode(value);
            }
            if (url.EndsWith("/"))
                url = ParamName;
            else if (url.Contains("?"))
                url += "&" + ParamName;
            else
                url += "?" + ParamName;

            if (IsCpLayout)
            {
                #region CP

                if (!(maxPageIndex > 1)) return;

                if (maxPage > _pageMax)
                {
                    //Html += @"<li class=""page-item""><a href=""javascript:VSWRedirect('" + ActionName + @"', " + 1 + @", '" + ParamName + @"')"" class=""page-link"">" + BackEndText + @"</a></li>";
                    Html += @"<li class=""page-item""><a href=""javascript:VSWRedirect('" + ActionName + @"', " + minPage + @", '" + ParamName + @"')"" class=""page-link"">" + BackText + @"</a></li>";
                }
                else
                {
                    //Html += @"<li class=""page-item disabled""><a href=""#"" class=""page-link"">" + BackEndText + @"</a></li>";
                    Html += @"<li class=""page-item disabled""><a href=""#"" class=""page-link"">" + BackText + @"</a></li>";
                }

                for (var i = minPage; i < maxPage; i++)
                {
                    if (i != pageIndex)
                    {
                        if (i < maxPageIndex)
                            Html += @"<li class=""page-item""><a href=""javascript:VSWRedirect('" + ActionName + @"', " + (i + 1) + @", '" + ParamName + @"')"" class=""page-link"">" + (i + 1) + @"</a></li>";
                    }
                    else
                    {
                        if (i < maxPageIndex)
                            Html += @"<li class=""page-item""><a href= ""#"" class=""page-link disabled"">" + (i + 1) + @"</a></li>";
                    }
                }

                if (maxPage < maxPageIndex)
                {
                    Html += @"<li class=""page-item""><a href=""javascript:VSWRedirect('" + ActionName + @"', " + (maxPage + 1) + @", '" + ParamName + @"')"" class=""page-link"">" + NextText + @"</a></li>";
                    //Html += @"<li class=""page-item""><a href=""javascript:VSWRedirect('" + ActionName + @"', " + (maxPageIndex > (int)maxPageIndex ? (int)maxPageIndex + 1 : maxPageIndex) + @", '" + ParamName + @"')"" class=""page-link"">" + NextEndText + @"</a></li>";
                }
                else
                {
                    Html += @"<li class=""page-item disabled""><a href=""#"" class=""page-link"">" + NextText + @"</a></li>";
                    //Html += @"<li class=""page-item disabled""><a href=""#"" class=""page-link"">" + NextEndText + @"</a></li>";
                }

                #endregion CP
            }
            else
            {
                #region Web

                Html = string.Empty;
                if (!(maxPageIndex > 1)) return;

                if (maxPage > _pageMax)
                {
                    //Html += @"<li><a href=""" + url + (url.Contains("?") ? "=" : "/") + 1 + @""">« " + BackEndText + @"</a></li>";
                    Html += @"<li><a href=""" + url + (url.Contains("?") ? "=" : "/") + minPage + @""">« " + BackText + @"</a></li>";
                }
                else if (DisableMode)
                {
                    //Html += @"<li><a href=""javascript:void(0)"">« " + BackEndText + @"</a></li>";
                    Html += @"<li><a href=""javascript:void(0)"">« " + BackText + @"</a></li>";
                }

                for (var i = minPage; i < maxPage; i++)
                {
                    if (i != pageIndex)
                    {
                        if (i < maxPageIndex)
                            Html+= @"<li><a href=""" + url + (url.Contains("?") ? "=" : "/") + (i + 1) + @""">" + (i + 1) + @"</a></li>";

                    }
                    else
                    {
                        if (i < maxPageIndex)
                            Html += @"<li class=""active""><a href=""javascript:void(0)"" >" + (i + 1) + @"</a></li>";
                    }
                }
                if (maxPage < maxPageIndex)
                {
                    Html += @"<li><a href=""" + url + (url.Contains("?") ? "=" : "/") + (maxPage + 1) + @""">" + NextText + @" »</a></li>";

                    //Html += @"<li><a href=""" + url + (url.Contains("?") ? "=" : "/") + (maxPageIndex > (int)maxPageIndex ? (int)maxPageIndex + 1 : maxPageIndex) + @""">" + NextEndText + @" »</a></li>";
                }
                else if (DisableMode)
                {
                    Html += @"<li><a href=""javascript:void(0)"">" + NextText + @" »</a></li>";
                    //Html += @"<li><a href=""javascript:void(0)"">" + NextEndText + @" »</a></li>";
                }

                #endregion Web
            }
        }
    }
}