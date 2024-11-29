<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModProductEntity;
    var getpage = ViewPage.CurrentPage;
    var getBand1 = SysPageService.Instance.GetByParent_Cache(11239);
    var getBand = getBand1.Where(o => o.MenuID == item.BrandID).ToList();
    var getparentpage1 = SysPageService.Instance.GetByParent_Cache(getpage.ParentID);
    var getparentpage = getparentpage1.Where(o => o.BrandID <= 0).ToList();
    string img = !string.IsNullOrEmpty(item.File) ? item.File.Replace("~/", "/") : "";
  
    var listItem = ViewBag.Other as List<ModProductEntity>;
    var listFile = item.GetFile();
   
    var listband = item.GetBrand();
   
    //if (!string.IsNullOrEmpty(item.File))
    //    item.File = listFile[0].File;
    //comment
    var listComment = ViewBag.Comment as List<ModCommentEntity>;
    int _TotalComment = listComment == null ? 0 : listComment.Count;
    var numberOneStar = 0; var onePer = 0;
    var numberTwoStar = 0; ; var twoPer = 0;
    var numberThreeStar = 0; ; var threePer = 0;
    var numberFourStar = 0; ; var fourPer = 0;
    var numberFiveStar = 0; ; var fivePer = 0;
    if (listComment != null)
    {
        var oneStar = listComment.FindAll(o => o.Vote == 1);
        numberOneStar = (oneStar == null) ? 0 : oneStar.Count;
        var twoStar = listComment.FindAll(o => o.Vote == 2);
        numberTwoStar = (twoStar == null) ? 0 : twoStar.Count;
        var threeStar = listComment.FindAll(o => o.Vote == 3);
        numberThreeStar = (threeStar == null) ? 0 : threeStar.Count;
        var fourStar = listComment.FindAll(o => o.Vote == 4);
        numberFourStar = (fourStar == null) ? 0 : fourStar.Count;
        var fiveStar = listComment.FindAll(o => o.Vote == 5);
        numberFiveStar = (fiveStar == null) ? 0 : fiveStar.Count;
    }
    if (_TotalComment > 0)
    {
        onePer = (numberOneStar / _TotalComment) * 100;
        twoPer = (numberTwoStar / _TotalComment) * 100;
        threePer = (numberThreeStar / _TotalComment) * 100;
        fourPer = (numberFourStar / _TotalComment) * 100;
        fivePer = (numberFiveStar / _TotalComment) * 100;
    }
    var average = _TotalComment == 0 ? 5 : Math.Round(((double)(5 * numberFiveStar + 4 * numberFourStar + 3 * numberThreeStar + 2 * numberTwoStar + 1 * numberOneStar) / _TotalComment), 1);
    var brand1 = SysPageService.Instance.CreateQuery().Where(o => o.MenuID == ViewPage.CurrentPage.MenuID && o.BrandID == item.BrandID).ToSingle_Cache();
    var brand = brand1 != null ? brand1 : ViewPage.CurrentPage;
%>





