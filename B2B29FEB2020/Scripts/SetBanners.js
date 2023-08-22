var SetBannerHandler;

var SetBannerHelper = function() {
    this.imgHomeb1 = $("#imgHomeb1");
    this.imgHomeb2 = $("#imgHomeb2");
    this.imgHomeb3 = $("#imgHomeb3");
    this.imgHomeR1 = $("#imgHomeR1");
}

SetBannerHelper.prototype.BindEvents = function() {
    var h = this;
    h.SetBanners();
}

SetBannerHelper.prototype.SetBanners = function() {
    var h = this;
    try {
        var banNames = 'homer' + "," + 'homeb1' + "," + 'homeb2' + "," + 'homeb3';
        $.ajax({
            url: UrlBase + "Adds/Offers/ViewOffers.aspx/GetAllBannersForThisPage",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: "{'BannerNames':'" + banNames + "'}",
            success: function(data) {
                try {
                    var banners = data.d;
                    if (banners.length > 0) {

                        for (var i = 0; i < banners.length; i++) {
                            if ($.trim(banners[i].BannerName.toLowerCase()) == "homer") {
                                $("#imgHomeR1").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancHomer").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "homeb1") {
                                $("#imgHomeb1").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                $("#spnHomeb1").text($.trim(banners[i].Title));
                                $("#divhomebdur1").text($.trim(banners[i].Duration));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancHomeBott1").removeAttr("href");
                                }
                                else {
                                    if (banners[i].DetailImage.toLowerCase() == "demo.aspx")
                                    { $("#ancHomeBott1").attr("href", "demo.aspx"); }
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "homeb2") {
                                $("#imgHomeb2").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                $("#spnHomeb2").text($.trim(banners[i].Title));
                                $("#divhomebdur2").text($.trim(banners[i].Duration));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancHomeBott2").removeAttr("href");
                                }
                            }
                            else if ($.trim(banners[i].BannerName.toLowerCase()) == "homeb3") {
                                $("#imgHomeb3").attr("src", UrlBase + banners[i].DemoImage.replace("~/", ""));
                                $("#spnHomeb3").text($.trim(banners[i].Title));
                                $("#divhomebdur3").text($.trim(banners[i].Duration));
                                if (eval($.trim(banners[i].HasLink.toLowerCase())) == false) {
                                    $("#ancHomeBott3").removeAttr("href");
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
            error: function(XMLHttpRequest, textStatus, errorThrown) {
            }
        });
    }
    catch (err) {
    }
}

$(document).ready(function() {
    SetBannerHandler = new SetBannerHelper();
    SetBannerHandler.BindEvents();
});