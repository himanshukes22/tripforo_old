var SetOffersHandler;

var SetOffersHelper = function () {

}

SetOffersHelper.prototype.BindEvents = function () {
    var h = this;
    h.SetBanners();
}

SetOffersHelper.prototype.SetBanners = function () {
    var h = this;
    try {
 
        var banNames = 'ibebott1' + "," + 'ibebott2' + "," + 'iber1' + "," + 'iber2' + "," + 'iber3';
        $.ajax({
            url: UrlBase + "Adds/Offers/ViewOffers.aspx/GetAllBannersForThisPage",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: "{'BannerNames':'" + banNames + "'}",
            success: function (data) {
             
                try {
                    var banners = data.d;
                    if (banners.length > 0) {
                        for (var i = 0; i < banners.length; i++) {
                            if ($.trim(banners[i].BannerName.toLowerCase()) == "ibebott1") {
                                $("#imgibhomebot1").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancIbebott1").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "ibebott2") {
                                $("#imgibhomebot2").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancIbebott2").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "iber1") {
                                $("#imgibhomer1").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancIbehomer1").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "iber2") {
                                $("#imgibhomer2").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancIbehomer2").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "iber3") {
                                $("#imgibhomer3").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancIbehomer3").removeAttr("href");
                                }
                            }
                            ////if (i == 0) {
                            ////    $("#imgHomeR1").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                            ////}
                            ////else {
                            ////    $("#imgHomeb" + i).attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                            ////}
                        }
                    }
                }
                catch (err) {
                
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
        
            }
        });
    }
    catch (err) {
       
    }
}

$(document).ready(function () {
    SetOffersHandler = new SetOffersHelper();
    SetOffersHandler.BindEvents();
});