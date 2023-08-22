<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PnrSummaryIntl.ascx.cs" Inherits="DMT_Manager_User_Control_UseExpPdfPnrSummaryIntl" %>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>

<script language="javascript" type="text/javascript">  
function GetParameterValues(param)
    {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) { var urlparam = url[i].split('='); if (urlparam[0] == param) { return urlparam[1]; } }
    }

    var isprint = GetParameterValues('print');
    if (isprint != undefined && isprint == "true") { myPrint(); }   

    var ispdf = GetParameterValues('pdf');    
    if (ispdf != undefined && ispdf == "true") { DownloadPdfEvent(); }

    $("#ExportToPdfFun").click(function () {
        DownloadPdfEvent()
    });
	
    //$("#ExportToPdfFun").click(function () {
    //    DownloadPdfEvent();
    //});
	
	function DownloadPdfEvent()
    {
	    debugger;
	var temprefno = $("#hdnReferenceNo").val();
        //$("#agencylogo").attr("src", "");
        //$("#agencylogo").attr("src", "/AgentLogo/9372732306.jpg");

        html2canvas($('#divprint')[0], {
            onrendered: function (canvas) {
                var data = canvas.toDataURL();
                var docDefinition = {
                    content: [{
                        image: data,
                        width: 500
                    }]
                };
                pdfMake.createPdf(docDefinition).download("Ticket-" + temprefno + ".pdf");
            }
        });
	}

    //function AdditionalCharge() {
    //    debugger;
    //    var adultcount = 0; var childcount = 0; var infantcount = 0;
    //    var adultTaxAmt = 0; var childTaxAmt = 0; var infantTaxAmt = 0; var totalTaxAmt = 0;
    //    var totalFinalAMt = 0;

    //    var selectChargeType = $("#ddl_srvtype");
    //    var chargeAmount = $("#txt_srvcharge");

    //    if (selectChargeType.val() == "") { alert('Please select charge type'); selectChargeType.focus(); return false; }
    //    if (chargeAmount.val() == "") { alert('Please fill charge amount'); chargeAmount.focus(); return false; }

    //    var collection = {}; var k = 0;
    //    var pgUrl = window.location.search.substring(1);
    //    var qarray = pgUrl.split('&');
    //    for (var i = 0; i <= qarray.length - 1; i++) {
    //        var splt = qarray[i].split('=');
    //        if (splt.length > 0) {
    //            for (var j = 0; j < splt.length - 1; j++) { collection[k] = splt[j + 1]; }
    //            k += 1;
    //        }
    //    }

    //    adultcount = $("#td_adtcnt")[0].innerHTML;
    //    childcount = $("#td_chdcnt")[0].innerHTML;
    //    totalFinalAMt = $("#td_grandtot")[0].innerHTML;

    //    var noofperson = 0;

    //    if (collection[1] == "") {
    //        if (selectChargeType.val() == "TAX") {
    //            if ($("#hedFinalTotaltax").val() != "") {
    //                totalFinalAMt = parseInt($("#hedFinalTotaltax").val());
    //            }

    //            if ($("#hedFinalTotaltax").val() == "") {
    //                totalFinalAMt = $("#hedFinalTotaltax").val($("#td_grandtot")[0].innerHTML).val();
    //            }
    //            else {
    //                totalFinalAMt = $("#hedFinalTotaltax").val();
    //            }


    //            if (parseInt(adultcount) > 0) {
    //                adultTaxAmt = $("#td_taxadt").html();
    //                noofperson = parseInt(adultcount);
    //            }
    //            if (parseInt(childcount) > 0) {
    //                childTaxAmt = $("#td_taxchd")[0].innerHTML;
    //                noofperson = noofperson + parseInt(childcount);
    //            }

    //            $(".taxclass").html(parseInt(adultTaxAmt) + parseInt(childTaxAmt) + (parseInt(chargeAmount.val()) * parseInt(noofperson)));
    //            var totaltax = $(".taxclass").html();
    //            $("#td_grandtot").html(parseInt(totalFinalAMt) + (parseInt(chargeAmount.val()) * parseInt(noofperson)));
    //        }
    //        else {
    //            if ($("#hedFinalTotal").val() != "") {
    //                totalFinalAMt = parseInt($("#hedFinalTotal").val());
    //            }
    //            if ($("#hedFinalTotal").val() == "") {

    //                totalFinalAMt = $("#hedFinalTotal").val($("#td_grandtot")[0].innerHTML).val();
    //            }
    //            else {
    //                totalFinalAMt = $("#hedFinalTotal").val();
    //            }

    //            //if (parseInt(adultcount) > 0) {
    //            //    adultTaxAmt = $("#td_taxadt").html();
    //            //    noofperson = parseInt(adultcount);
    //            //}
    //            //if (parseInt(childcount) > 0) {
    //            //    childTaxAmt = $("#td_taxchd")[0].innerHTML;
    //            //    noofperson = noofperson + parseInt(childcount);
    //            //}

    //            $("#lbltransfee").html(((parseInt(adultcount) * parseInt(chargeAmount.val())) + ((parseInt(childcount) * parseInt(chargeAmount.val())))));
    //            $("#td_grandtot").html(parseInt(totalFinalAMt) + ((parseInt(adultcount) * parseInt(chargeAmount.val())) + ((parseInt(childcount) * parseInt(chargeAmount.val())))));
    //            $("#trtransfee").show();
    //        }

    //        UpdateCharges();
    //        alert('Fare summary changed sucessfully.');
    //    }
    //    else {
    //    }
    //}

    function AdditionalCharge() {
        debugger;
        if ($("#ddl_srvtype").val() == "") {
            alert('Please select charge type');
            $("#ddl_srvtype").focus();
            return false;
        }
        if ($("#txt_srvcharge").val() == "") {
            alert('Please fill charge amount');
            $("#txt_srvcharge").focus();
            return false;
        }
        
        //Get Query string 
        var collection = {}; var k = 0;
        var pgUrl = window.location.search.substring(1);
        var qarray = pgUrl.split('&');
        for (var i = 0; i <= qarray.length - 1; i++) {
            var splt = qarray[i].split('=');
            if (splt.length > 0) {
                for (var j = 0; j < splt.length - 1; j++) {
                    collection[k] = splt[j + 1];
                }
                k += 1;
            }
        }
        if (collection[1] == "") {
            //Claculation for whole order

            var SrvCharge = $("#txt_srvcharge").val();
            var SrvType = $("#ddl_srvtype").val();
            var adtcnt = $("#td_adtcnt")[0].innerHTML;
            var td_taxadt = $("#td_taxadt").html();

            var chdcnt = 0;
            var tcadt = 0, tcadttot = 0, tcchd = 0, tcchdtot = 0;
            var taxadt = 0, taxadttot = 0, taxchd = 0, taxchdtot = 0, TotalInfant = 0, FinalTotal = 0;;

            if ($('#td_chdcnt').length > 0) {
                chdcnt = $("#td_chdcnt")[0].innerHTML;
            }
            if (SrvType == "TC") {

                if ($("#hedFinalTotal").val() != "") {
                    $("#td_grandtot")[0].innerHTML = parseInt($("#hedFinalTotal").val());
                }
                if ($("#hedFinalTotal").val() == "") {

                    FinalTotal = $("#hedFinalTotal").val($("#td_grandtot")[0].innerHTML).val();
                }
                else {
                    FinalTotal = $("#hedFinalTotal").val();
                }
                if (adtcnt > 0) {
                    //Checking hidden field for tax
                    if ($("#hidtcadt").val() != "") {
                        $("#td_tcadt")[0].innerHTML = parseInt($("#hidtcadt").val());
                    }
                    if ($("#hidtotadt").val() != "") {
                        $("#td_adttot")[0].innerHTML = parseInt($("#hidtotadt").val());
                    }
                    if ($("#hidtcadt").val() == "") {
                        tcadt = $("#hidtcadt").val($("#td_tcadt")[0].innerHTML).val();
                    }
                    else {
                        tcadt = $("#hidtcadt").val();
                    }

                    if ($("#hidtotadt").val() == "") {
                        tcadttot = $("#hidtotadt").val($("#td_adttot")[0].innerHTML).val();
                    }
                    else {
                        tcadttot = $("#hidtotadt").val();
                    }
                    $("#td_tcadt")[0].innerHTML = parseInt(tcadt) + (parseInt(SrvCharge) * parseInt(adtcnt));
                    $("#td_adttot")[0].innerHTML = parseInt(tcadttot) + (parseInt(SrvCharge) * parseInt(adtcnt));

                }
                if (chdcnt > 0) {
                    //For CHD TC

                    if ($("#hidtcchd").val() != "") {
                        $("#td_tcchd")[0].innerHTML = parseInt($("#hidtcchd").val());
                    }

                    if ($("#hidtotchd").val() != "") {
                        $("#td_chdtot")[0].innerHTML = parseInt($("#hidtotchd").val());
                    }

                    if ($("#hidtcchd").val() == "") {
                        tcchd = $("#hidtcchd").val($("#td_tcchd")[0].innerHTML).val();
                    }

                    else {
                        tcchd = $("#hidtcchd").val();
                    }

                    if ($("#hidtotchd").val() == "") {
                        tcchdtot = $("#hidtotchd").val($("#td_chdtot")[0].innerHTML).val();
                    }
                    else {
                        tcchdtot = $("#hidtotchd").val();
                    }

                    $("#td_tcchd")[0].innerHTML = parseInt(tcchd) + (parseInt(SrvCharge) * parseInt(chdcnt));
                    $("#td_chdtot")[0].innerHTML = parseInt(tcchdtot) + (parseInt(SrvCharge) * parseInt(chdcnt));
                }

                $("#td_grandtot")[0].innerHTML = parseInt(FinalTotal) + (parseInt(SrvCharge) * parseInt(adtcnt)) + (parseInt(SrvCharge) * parseInt(chdcnt));
                $("#td_allcharge")[0].innerHTML = (parseInt(SrvCharge) * parseInt(adtcnt)) + (parseInt(SrvCharge) * parseInt(chdcnt));
                //$("#trtransfee").show();
            }

            if (SrvType == "TAX") {
                if ($("#hedFinalTotaltax").val() != "") {
                    $("#td_grandtot")[0].innerHTML = parseInt($("#hedFinalTotaltax").val());
                }
                if ($("#hedFinalTotaltax").val() == "") {

                    FinalTotal = $("#hedFinalTotaltax").val($("#td_grandtot")[0].innerHTML).val();
                }
                else {
                    FinalTotal = $("#hedFinalTotaltax").val();
                }
                if (adtcnt > 0) {
                    if ($("#hidtaxadt").val() != "") {
                        $("#td_taxadt")[0].innerHTML = parseInt($("#hidtaxadt").val());
                    }
                    if ($("#hidtaxtotadt").val() != "") {
                        $("#td_adttot")[0].innerHTML = parseInt($("#hidtaxtotadt").val());
                    }
                    //For Adult TAX
                    if ($("#hidtaxadt").val() == "") {
                        taxadt = $("#hidtaxadt").val($("#td_taxadt")[0].innerHTML).val();
                    }
                    else {
                        taxadt = $("#hidtaxadt").val();
                    }
                    if ($("#hidtaxtotadt").val() == "") {
                        taxadttot = $("#hidtaxtotadt").val($("#td_adttot")[0].innerHTML).val();
                    }
                    else {
                        taxadttot = $("#hidtaxtotadt").val();
                    }
                    $("#td_taxadt")[0].innerHTML = parseInt(taxadt) + (parseInt(SrvCharge) * parseInt(adtcnt));
                    $("#td_adttot")[0].innerHTML = parseInt(taxadttot) + (parseInt(SrvCharge) * parseInt(adtcnt));

                }
                if (chdcnt > 0) {
                    if ($("#hidtaxchd").val() != "") {
                        $("#td_taxchd")[0].innerHTML = parseInt($("#hidtaxchd").val());
                    }

                    if ($("#hidtaxtotchd").val() != "") {
                        $("#td_chdtot")[0].innerHTML = parseInt($("#hidtaxtotchd").val());
                    }

                    if ($("#hidtaxgrandtot").val() != "") {
                        $("#td_grandtot")[0].innerHTML = parseInt($("#hidtaxgrandtot").val());
                    }

                    //For Child TAX
                    if ($("#hidtaxchd").val() == "") {
                        taxchd = $("#hidtaxchd").val($("#td_taxchd")[0].innerHTML).val();
                    }

                    else {
                        taxchd = $("#hidtaxchd").val();
                    }

                    if ($("#hidtaxtotchd").val() == "") {
                        taxchdtot = $("#hidtaxtotchd").val($("#td_chdtot")[0].innerHTML).val();
                    }

                    else {
                        taxchdtot = $("#hidtaxtotchd").val();
                    }

                    $("#td_taxchd")[0].innerHTML = parseInt(taxchd) + (parseInt(SrvCharge) * parseInt(chdcnt));
                    $("#td_chdtot")[0].innerHTML = parseInt(taxchdtot) + (parseInt(SrvCharge) * parseInt(chdcnt));
                }

                $(".taxclass")[0].innerHTML = parseInt(td_taxadt) + parseInt(taxchd) + (parseInt(SrvCharge) * parseInt(adtcnt)) + (parseInt(SrvCharge) * parseInt(chdcnt));
                $("#td_grandtot")[0].innerHTML = parseInt(FinalTotal) + (parseInt(SrvCharge) * parseInt(adtcnt)) + (parseInt(SrvCharge) * parseInt(chdcnt));
                //$("#td_allcharge")[0].innerHTML = (parseInt(SrvCharge) * parseInt(adtcnt)) + (parseInt(SrvCharge) * parseInt(chdcnt));
            }
            //$("#td_tcadt").focus();
            UpdateCharges();
            //alert('Fare summary changed sucessfully.');
        }
        else {
            debugger;
            //Calculation by pax id
            var SrvCharge = $("#txt_srvcharge").val();
            var SrvType = $("#ddl_srvtype").val();
            var tcperpax = 0, tcpaxTotal = 0, perpaxgrandtot = 0;
            var taxperpax = 0;
            var paxtype = $("#td_perpaxtype")[0].innerHTML;
            if (paxtype == "INF") {
                alert('Fare will not change for Infant');
                return false;
            }
            //if (SrvType == "TC" && paxtype != "INF") {

            //    if ($("#hidperpaxtc").val() != "") {
            //        $("#td_perpaxtc")[0].innerHTML = parseInt($("#hidperpaxtc").val());
            //    }
            //    if ($("#hidperpaxTCtot").val() != "") {
            //        $("#td_totalfare")[0].innerHTML = parseInt($("#hidperpaxTCtot").val());
            //    }
            //    if ($("#hidperpaxgrandTCtot").val() != "") {
            //        $("#td_grandtot")[0].innerHTML = parseInt($("#hidperpaxgrandTCtot").val());
            //    }
            //    if ($("#hidperpaxtc").val() == "") {
            //        tcperpax = $("#hidperpaxtc").val($("#td_perpaxtc")[0].innerHTML).val();
            //    }
            //    else {
            //        tcperpax = $("#hidperpaxtc").val();
            //    }
            //    if ($("#hidperpaxTCtot").val() == "") {
            //        tcpaxTotal = $("#hidperpaxTCtot").val($("#td_totalfare")[0].innerHTML).val();
            //    }
            //    else {
            //        tcpaxTotal = $("#hidperpaxTCtot").val();
            //    }
            //    if ($("#hidperpaxgrandTCtot").val() == "") {
            //        perpaxgrandtot = $("#hidperpaxgrandTCtot").val($("#td_grandtot")[0].innerHTML).val();
            //    }
            //    else {
            //        perpaxgrandtot = $("#hidperpaxgrandTCtot").val();
            //    }
            //    $("#td_perpaxtc")[0].innerHTML = parseInt(tcperpax) + (parseInt(SrvCharge));
            //    $("#td_totalfare")[0].innerHTML = parseInt(tcpaxTotal) + (parseInt(SrvCharge));
            //    $("#td_grandtot")[0].innerHTML = parseInt(perpaxgrandtot) + (parseInt(SrvCharge));
            //}
            //if (SrvType == "TAX" && paxtype != "INF") {

            //    if ($("#hidperpaxtax").val() != "") {
            //        $("#td_perpaxtax")[0].innerHTML = parseInt($("#hidperpaxtax").val());
            //    }
            //    if ($("#hidperpaxTaxtot").val() != "") {
            //        $("#td_totalfare")[0].innerHTML = parseInt($("#hidperpaxTaxtot").val());
            //    }
            //    if ($("#hidperpaxgrandTaxtot").val() != "") {
            //        $("#td_grandtot")[0].innerHTML = parseInt($("#hidperpaxgrandTaxtot").val());
            //    }
            //    if ($("#hidperpaxtax").val() == "") {
            //        taxperpax = $("#hidperpaxtax").val($("#td_perpaxtax")[0].innerHTML).val();
            //    }
            //    else {
            //        taxperpax = $("#hidperpaxtax").val();
            //    }
            //    if ($("#hidperpaxTaxtot").val() == "") {
            //        tcpaxTotal = $("#hidperpaxTaxtot").val($("#td_totalfare")[0].innerHTML).val();
            //    }
            //    else {
            //        tcpaxTotal = $("#hidperpaxTaxtot").val();
            //    }
            //    if ($("#hidperpaxgrandTaxtot").val() == "") {
            //        perpaxgrandtot = $("#hidperpaxgrandTaxtot").val($("#td_grandtot")[0].innerHTML).val();
            //    }
            //    else {
            //        perpaxgrandtot = $("#hidperpaxgrandTaxtot").val();
            //    }
            //    $("#td_perpaxtax")[0].innerHTML = parseInt(taxperpax) + (parseInt(SrvCharge));
            //    $("#td_totalfare")[0].innerHTML = parseInt(tcpaxTotal) + (parseInt(SrvCharge));
            //    $("#td_grandtot")[0].innerHTML = parseInt(perpaxgrandtot) + (parseInt(SrvCharge));
            //}
            UpdateCharges();
            //alert('Fare summary changed sucessfully.');
            // $("#td_perpaxtax").focus();
        }
        $("#Hidden1").val($("#div_mail")[0].innerHTML);       
    }
</script>
