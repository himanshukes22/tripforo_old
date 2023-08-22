var SetPkgBannerHandler;

var SetPkgBannerHelper = function () {

}

SetPkgBannerHelper.prototype.BindEvents = function () {
    var h = this;
    h.SetBanners();
}

SetPkgBannerHelper.prototype.SetBanners = function () {
    var h = this;
    try {
        var banNames = 'pkgsearch' + ",";
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
                            if ($.trim(banners[i].BannerName.toLowerCase()) == "pkgsearch") {
                                $("#imgPkgSearchMain").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancpkgsearch").removeAttr("href");
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
    SetPkgBannerHandler = new SetPkgBannerHelper();
    SetPkgBannerHandler.BindEvents();
});