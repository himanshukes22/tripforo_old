<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PnrSummaryIntl.ascx.cs" Inherits="DMT_Manager_User_Control_UseExpPdfPnrSummaryIntl" %>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>

<%--<script language="javascript" type="text/javascript">           
    $("#ExportToPdfFun").click(function () {
        var temprefno = $("#hdnReferenceNo").val();
        $("#agencylogo").attr("src", "");
        $("#agencylogo").attr("src", "/Images/logo-new.png");

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
    });
</script>--%>
