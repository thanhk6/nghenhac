using System;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModVideoEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int WebUserID { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int CityID { get; set; }

        [DataInfo]
        public int State { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Summary { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string PageTitle { get; set; }

        [DataInfo]
        public string PageDescription { get; set; }

        [DataInfo]
        public string PageKeywords { get; set; }

        [DataInfo]
        public int View { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public DateTime Published { get; set; }

        [DataInfo]
        public DateTime Updated { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion Autogen by VSW

        private string _oEName;
        public string EName
        {
            get
            {
                if (string.IsNullOrEmpty(_oEName))
                    _oEName = GetWebUser().EName;

                return _oEName;
            }
        }

        private string _oECode;
        public string ECode
        {
            get
            {
                if (string.IsNullOrEmpty(_oECode))
                    _oECode = GetWebUser().ECode;

                return _oECode;
            }
        }

        private ModWebUserEntity _oWebUser;
        public ModWebUserEntity GetWebUser()
        {
            if (_oWebUser == null && WebUserID > 0)
                _oWebUser = ModWebUserService.Instance.GetByID(WebUserID);

            return _oWebUser ?? (_oWebUser = new ModWebUserEntity());
        }

        private WebMenuEntity _oMenu;
        public WebMenuEntity GetMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            return _oMenu ?? (_oMenu = new WebMenuEntity());
        }

        private WebMenuEntity _oCity;
        public WebMenuEntity GetCity()
        {
            if (_oCity == null && CityID > 0)
                _oCity = WebMenuService.Instance.GetByID_Cache(CityID);

            return _oCity ?? (_oCity = new WebMenuEntity());
        }

        public void UpView()
        {
            View++;
            ModVideoService.Instance.Save(this, o => o.View);
        }

        private string _youtubeID;
        public string YoutubeID
        {
            get
            {
                if (string.IsNullOrEmpty(_youtubeID) && !string.IsNullOrEmpty(File))
                {
                    string[] _ArrCode = System.Text.RegularExpressions.Regex.Split(File, "v=");
                    if (_ArrCode.Length > 1) _youtubeID = _ArrCode[1];
                }

                return _youtubeID;
            }
        }

        private string _thumbnail;
        public string Thumbnail
        {
            get
            {
                if (string.IsNullOrEmpty(_thumbnail) && !string.IsNullOrEmpty(File))
                    _thumbnail = "http://img.youtube.com/vi/" + YoutubeID + "/0.jpg";

                return _thumbnail;
            }
        }

        private string _duration;
        public string Duration
        {
            get
            {
                string url = @"https://www.googleapis.com/youtube/v3/videos?id=" + YoutubeID + @"&key=AIzaSyDYwPzLevXauI-kTSVXTLroLyHEONuF9Rw&part=snippet,contentDetails";
                string json = VSW.Lib.Global.Utils.GetResponseJson(url);

                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                dynamic items = data.items;

                if (items.Count == 0) return "0";

                dynamic contentDetails = items[0].contentDetails;
                dynamic duration = contentDetails.duration;

                var totalSeconds = System.Xml.XmlConvert.ToTimeSpan(duration.Value).TotalSeconds;

                _duration = VSW.Lib.Global.Utils.ConvertTime(VSW.Core.Global.Convert.ToInt(totalSeconds));

                return _duration;
            }
        }
    }

    public class ModVideoService : ServiceBase<ModVideoEntity>
    {
        #region Autogen by VSW

        public ModVideoService()
            : base("[Mod_Video]")
        {
        }

        private static ModVideoService _instance;
        public static ModVideoService Instance
        {
            get { return _instance ?? (_instance = new ModVideoService()); }
        }

        #endregion Autogen by VSW

        public ModVideoEntity GetByID(int id)
        {
            return CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        public bool Exists(string query)
        {
            return CreateQuery()
                           .Where(query)
                           .Count()
                           .ToValue()
                           .ToBool();
        }
    }
}