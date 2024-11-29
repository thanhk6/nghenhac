using System;
using System.Collections.Generic;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModCommentEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public override string Name { get; set; }
        [DataInfo]
        public string Phone { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public DateTime Published { get; set; }

        [DataInfo]
        public DateTime Updated { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }
        [DataInfo]
        public int Vote { get; set; }
        [DataInfo]
        public string Email { get; set; }
        [DataInfo]
        public int  Mp3ID { get; set; }
        [DataInfo]
        public int ParentID { get; set; }
        [DataInfo]
        public int WebUserID { get; set; }

        [DataInfo]
        public bool Activity { get; set; }


        private ModWebUserEntity _oWebUser;
        public ModWebUserEntity GetWebUser()
        {
            if (_oWebUser == null && WebUserID > 0)
                _oWebUser = ModWebUserService.Instance.GetByID(WebUserID);

            return _oWebUser ?? (_oWebUser = new ModWebUserEntity());
        }     
        private ModCommentEntity _oParent;

        public ModCommentEntity GetParent()
        {
            if (_oParent == null && ParentID > 0)
                _oParent = ModCommentService.Instance.GetByID(ParentID);

            return _oParent ?? (_oParent = new ModCommentEntity());
        }
        public List<ModCommentEntity> GetComment()
        {
            return ModCommentService.Instance.CreateQuery()
                .Where(o => o.Activity == true)
                            .Where(o => o.ParentID == ID)
                            .OrderByAsc(o => o.Created)
                            .ToList();
        }

        public List<ModCommentEntity> GetCPComment()
        {
            return ModCommentService.Instance.CreateQuery()
                            .Where(o => o.ParentID == ID)
                            .OrderByAsc(o => o.Created)
                            .ToList();
        }

        public string Time
        {
            get
            {
                if ((DateTime.Now - Created).TotalDays >= 365) return ((DateTime.Now - Created).TotalDays / 365) + " năm trước.";
                else if ((DateTime.Now - Created).TotalDays >= 30) return ((DateTime.Now - Created).TotalDays / 30) + " tháng trước.";
                else if ((DateTime.Now - Created).TotalDays >= 7) return ((DateTime.Now - Created).TotalDays / 7) + " tuần trước.";
                else if ((DateTime.Now - Created).TotalDays >= 1) return Math.Round((DateTime.Now - Created).TotalDays) + " ngày trước.";
                else if ((DateTime.Now - Created).TotalHours >= 1) return Math.Round((DateTime.Now - Created).TotalHours) + " giờ trước.";
                else if ((DateTime.Now - Created).TotalMinutes >= 1) return Math.Round((DateTime.Now - Created).TotalMinutes) + " phút trước.";
                else return Math.Round((DateTime.Now - Created).TotalSeconds) + " giây trước.";
            }
        }


        #endregion Autogen by VSW

    }
    public class ModCommentService : ServiceBase<ModCommentEntity>
    {
        #region Autogen by VSW

        public ModCommentService() : base("[Mod_Comment]")
        {
        }

        private static ModCommentService _instance;
        public static ModCommentService Instance => _instance ?? (_instance = new ModCommentService());

        #endregion Autogen by VSW

        public ModCommentEntity GetByID(int id)
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