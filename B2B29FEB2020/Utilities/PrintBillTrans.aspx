<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintBillTrans.aspx.cs" Inherits="DMT_Manager_PrintBillTrans" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <style>
        .main {
            margin-left: 20% !important;
        }

        .heading {
            text-align: left;
        }

        .sendmail {
            float: right;
            color: #fff;
            background-color: #ff8d3c;
            border: 1px solid #ff8d3c;
            border-radius: 0.2rem;
            padding: 2px;
        }

        @media print {
            .main {
                margin-left: 0px !important;
            }

            .heading {
                text-align: center !important;
            }

            .sendmail {
                display: none !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%=HtmlContent %>

        <script>
            $(window).on('load', function () { window.print(); });

            $(document.body).on('click', "#btnMailSend", function (e) {
                var emailid = $("#ReceiptSendMail").val().trim();

                if (emailid != "") {
                    if (ValidateEmail(emailid)) {
                        $("#btnMailSend").html("Sending... <i class='fa fa-pulse fa-spinner'></i>");
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            url: "PrintBillTrans.aspx/SendReceiptInMail",
                            data: '{emailid: ' + JSON.stringify(emailid) + '}',
                            datatype: "json",
                            success: function (data) {
                                if (data.d != null) {
                                    if (data.d == "sent") {
                                        alert("Transaction receipt has been sent to ' " + emailid + " ' successfully.");
                                        $("#ReceiptSendMail").val("");
                                    }
                                    else {
                                        alert("Transaction receipt send failed!");
                                    }
                                }
                                $("#btnMailSend").html("Send Mail");
                            },
                            failure: function (response) {
                                alert("failed");
                            }
                        });
                    }
                    else {
                        alert("Please enter valid email id !");
                    }
                }
                else {
                    alert("Please enter email id !");
                }
            });

            function ValidateEmail(email) {
                var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
                var valid = emailReg.test(email);
                if (!valid) { return false; }
                else { return true; }
            }
        </script>

    </form>
</body>
</html>
