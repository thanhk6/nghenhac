using System;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK: Static", Code = "CStatic", IsControl = true, Order = 4)]
    public class CStaticController : Controller
    {
        //[Core.MVC.PropertyInfo("Default[MenuID-false|MenuID2-false],City[MenuID-true|MenuID2-false],SearchBox[MenuID-false|MenuID2-true]")]
        //public string LayoutDefine;

        public override void OnLoad()
        {
        }

       
    }
    public class MFeedbackModel1
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}