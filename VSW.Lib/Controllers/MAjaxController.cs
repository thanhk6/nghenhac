using System;
using System.Drawing.Imaging;
using VSW.Core.Global;
using VSW.Lib.Global;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO: MAjax", Code = "MAjax", Order = 10, Activity = false)]
    public class MAjaxController : Controller
    {
        public void ActionIndex()
        {

        }
        public void ActionGetMP3TongQuan()
        {
            var json = new Json();

           



            json.Create();
        }
    }
}


