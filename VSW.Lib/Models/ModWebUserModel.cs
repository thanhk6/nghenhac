using System;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModWebUserEntity : EntityBase
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string Password { get; set; }

        [DataInfo]
        public string TempPassword { get; set; }

        [DataInfo]
        public override string Name { get; set; }
        [DataInfo]
        public string UserName { get; set; }
        [DataInfo]
        public string Shop { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string Logo { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Phone { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public string Facebook { get; set; }

        [DataInfo]
        public string Zalo { get; set; }

        [DataInfo]
        public long Point { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion Autogen by VSW
    }

    public class ModWebUserService : ServiceBase<ModWebUserEntity>
    {
        #region Autogen by VSW

        private ModWebUserService() : base("[Mod_WebUser]")
        {
        }

        private static ModWebUserService _instance;
        public static ModWebUserService Instance => _instance ?? (_instance = new ModWebUserService());

        #endregion Autogen by VSW

        public ModWebUserEntity GetByID(int id)
        {
            return CreateQuery()
                   .Where(o => o.ID == id)
                   .ToSingle();
        }

        public ModWebUserEntity GetByID_Cache(int id)
        {
            return CreateQuery()
                   .Where(o => o.ID == id)
                   .ToSingle_Cache();
        }

        public ModWebUserEntity GetByCode_Cache(string code)
        {
            return CreateQuery()
                   .Where(o => o.Code == code)
                   .ToSingle_Cache();
        }

        public int GetCount()
        {
            return CreateQuery()
                .Select(o => o.ID)
                .Count()
                .ToValue_Cache()
                .ToInt(0);
        }

        public ModWebUserEntity GetByEmail(string email)
        {
            return CreateQuery()
                    .Where(o => o.Email == email)
                    .ToSingle();
        }

        public bool CheckEmail(string email, int id)
        {
            return CreateQuery()
                   .Where(o => o.Email == email && o.ID != id)
                   .Count()
                   .ToValue()
                   .ToBool();
        }

        public ModWebUserEntity GetByCode(string code)
        {
            return CreateQuery()
                    .Where(o => o.Code == code)
                    .ToSingle();
        }

        public bool CheckCode(string code, int id)
        {
            return CreateQuery()
                   .Where(o => o.Code == code && o.ID != id)
                   .Count()
                   .ToValue()
                   .ToBool();
        }

        public ModWebUserEntity GetForLogin(int id)
        {
            if (id < 1) return null;

            return CreateQuery()
                      .Where(o => o.ID == id)
                      .ToSingle();
        }

        public ModWebUserEntity GetForLogin(string email, string md5_passwd)
        {
            var webUser = CreateQuery()
                                .Where(o => o.Email == email && (o.Password == md5_passwd || o.TempPassword == md5_passwd))
                                .ToSingle();

            if (webUser != null && webUser.TempPassword != string.Empty)
            {
                if (webUser.TempPassword == md5_passwd) webUser.Password = md5_passwd;
                webUser.TempPassword = string.Empty;
                Save(webUser);
            }

            return webUser;
        }
    }
}