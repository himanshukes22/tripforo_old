<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupRequestidDetails.aspx.cs" Inherits="GroupSearch_GroupRequestidDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/change.min.js"></script>
    <style type="text/css">
        input[type="text"], input[type="password"], select, textarea {
            background: #ffffff !important;
            color: #525865;
            border-radius: 4px;
            border: 1px solid #d1d1d1;
            box-shadow: inset 1px 2px 8px rgba(0, 0, 0, 0.10);
            font-size: 12px;
            height: 24px;
            width: 91px;
            line-height: 1.45;
            outline: none;
            padding: 0.1em 1.00em 0.2em;
            -webkit-transition: .18s ease-out;
            -moz-transition: .18s ease-out;
            -o-transition: .18s ease-out;
            transition: .18s ease-out;
        }

        #txt_price {
            width: 144px;
        }

        #txt_Remarks {
            width: 143px;
        }

        .clear1 {
            margin-top: 80px;
        }

        .auto-style1 {
            width: 100%;
        }

        .auto-style4 {
            width: 16%;
        }

        .HeaderStyle {
            height: 35px;
            background-color: #f1f1f1;
        }

            .HeaderStyle th {
                height: 35px;
                background-color: #f1f1f1;
                font-weight: normal;
                font-size: 13px;
            }
    </style>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.8.custom.min.js")%>"></script>
    <script src="../scripts/jquery.js" type="text/javascript"></script>
    <script src="../scripts/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="../scripts/json2.js" type="text/javascript"></script>
    <script src="../Scripts/JS-Book-A-Flight.js" type="text/javascript"></script>
    <link type="text/css" href="<%=ResolveUrl("~/Styles/jquery-ui-1.8.8.custom.css") %>" rel="stylesheet" />
    <link rel="stylesheet" href="/resources/demos/style.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        var UrlBase = '<%=ResolveUrl("~/") %>';
    </script>
    <script type="text/javascript">
        $("#OWayPrice1_btn_submit").click(function (e) {
            if (Attr('OWayPrice1_txt_Quoteprice').val() == "") {
                jAlert('Please enter the total number of passanger', 'Alert');
                Attr('OWayPrice1_txt_Quoteprice').focus();
            }
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".date").datepicker({
                dateFormat: 'dd/mm/yy',
                minDate: 0,
            }
                );
        });
    </script>
     <script type="text/javascript">
         function loadimg() {
             $("#waitMessage").show();
         }
    </script>
    <script lang="javascript" type="text/javascript">
        function Validate(str) {
            switch (str) {
                case 1: {
                    if (document.getElementById("<%=OWayPrice1_txt_Quoteprice.ClientID%>").value == "" || document.getElementById("<%=OWayPrice1_txt_Quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=OWayPrice1_txt_Quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid1.ClientID%>").value == "") {
                        alert("Please select the Offer date !!");
                        document.getElementById("<%=txt_offervalid1.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=OWayPrice1_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=OWayPrice1_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 2: {
                    if (document.getElementById("<%=OWayPrice2_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=OWayPrice2_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=OWayPrice2_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid2.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid2.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=OWayPrice2_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=OWayPrice2_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 3: {
                    if (document.getElementById("<%=OWayPrice3_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=OWayPrice3_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=OWayPrice3_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid3.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid3.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=OWayPrice3_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=OWayPrice3_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 4: {
                    if (document.getElementById("<%=OWayPrice4_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=OWayPrice4_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=OWayPrice4_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid4.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid4.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=OWayPrice4_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=OWayPrice4_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 5: {
                    if (document.getElementById("<%=OWayPrice5_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=OWayPrice5_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=OWayPrice5_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid5.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid5.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=OWayPrice5_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=OWayPrice5_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 6: {
                    if (document.getElementById("<%=RoundTrip1_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=RoundTrip1_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=RoundTrip1_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid6.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid6.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=RoundTrip1_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=RoundTrip1_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 7: {
                    if (document.getElementById("<%=RoundTrip2_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=RoundTrip2_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=RoundTrip2_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid7.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid7.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=RoundTrip2_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=RoundTrip2_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 8: {
                    if (document.getElementById("<%=RoundTrip3_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=RoundTrip3_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=RoundTrip3_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid8.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid8.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=RoundTrip3_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=RoundTrip3_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 9: {
                    if (document.getElementById("<%=RoundTrip4_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=RoundTrip4_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=RoundTrip4_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid9.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid9.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=RoundTrip4_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=RoundTrip4_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 10: {
                    if (document.getElementById("<%=RoundTrip5_txt_quoteprice.ClientID%>").value == "" || document.getElementById("<%=RoundTrip5_txt_quoteprice.ClientID%>").value == 0) {
                        alert("Please Provide Quote Amount or Quote price should be greater then 0 !!");
                        document.getElementById("<%=RoundTrip5_txt_quoteprice.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txt_offervalid10.ClientID%>").value == "") {
                        alert("Please select the offer date !!");
                        document.getElementById("<%=txt_offervalid10.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=RoundTrip5_txt_remarks.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=RoundTrip5_txt_remarks.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
                case 11: {
                    if (document.getElementById("<%=txtNoOfAdult.ClientID%>").value == "" || document.getElementById("<%=txtNoOfAdult.ClientID%>").value == 0) {
                        alert("Adult can't be blank or 0 !!");
                        document.getElementById("<%=txtNoOfAdult.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=txtExpectedFair.ClientID%>").value == "" || document.getElementById("<%=txtExpectedFair.ClientID%>").value == 0) {
                        alert("Please provide expected fare or expected fare should be greater then 0 !!");
                        document.getElementById("<%=txtExpectedFair.ClientID%>").focus();
                        return false;
                    }
                    if (document.getElementById("<%=textarea1.ClientID%>").value == "") {
                        alert("Remarks can't be blank, please fill the remark !!");
                        document.getElementById("<%=textarea1.ClientID%>").focus();
                        return false;
                    }
                    break;
                }
            }
            document.getElementById("waitMessage").style.display = "block";
        }
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            var btnclk = 0;
            $("#tblMainGroupFrame1").click(function (e) {
                var elem = document.getElementById('tblMainGroupFrame').getElementsByTagName("input");
                var Outbound = parseInt(0);
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_From") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Outbond Trip From,can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 3 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_To") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Outbond Trip To,can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 3 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_DepDate") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Departure date can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_DepTime") > 0) {
                        Outbound++;
                        if (elem[i].value == "" || elem[i].value.length > 5) {
                            alert('Please fill the Departure time or Invalid time format', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ArvlDate") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Arrival date can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_ArvlTime") > 0) {
                        Outbound++;
                        if (elem[i].value == "" || elem[i].value.length > 5) {
                            alert('Please fill the arrival time  or Invalid time format', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_Airline") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('airline can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                        else if ($.trim(elem[i].value).length < 2 && elem[i].value != "") {
                            alert('Atleast three characters required.', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_FlightNo") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Flight no can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_title") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Title can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_fname") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('First name can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_lNAME") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Last name can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_PaxType") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('Pax type can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                    if (elem[i].type == "text" && elem[i].id.indexOf("txt_DOB") > 0) {
                        Outbound++;
                        if (elem[i].value == "") {
                            alert('DOB can not be blank', 'Alert');
                            elem[i].focus();
                            return false;
                        }
                    }
                }
            });
        });
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            try {
                var charCode = (event.keyCode ? event.keyCode : event.which);
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            catch (exception) {
            }
        }
        function NumericOnly(event) {
            try {
                var charCode = (event.keyCode ? event.keyCode : event.which);
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }
            catch (exception) {
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="large-12 medium-12 small-12">
        <div class="large-12 medium-1 small-1 headbgs">
                        <span id="ctl00_ContentPlaceHolder1_Repeater_Adult_ctl00_pttextADT"><i class="fa fa-plane" aria-hidden="true"></i>Group Booking Details</span>
                    </div>
        
        <div class="large-12 medium-12 small-12" runat="server" id="Div_editpaxinfo" visible="false">
            <table class="table">
                <tr id="tr1" runat="server"  >
                    <td style='font-size: 13px; width:15%; text-align: center; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' class="auto-style4">Pax Details</td>
                    <td colspan="7">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_title" runat="server" Text='<%#Eval("Title")%>'></asp:TextBox>
                                                <asp:Label ID="lbl_paxcounter" runat="server" Text='<%#Eval("Counter")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_fname" runat="server" Text='<%#Eval("FName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Middle Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_mname" runat="server" Text='<%#Eval("MName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_lNAME" runat="server" Text='<%#Eval("LName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaxType">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_PaxType" MaxLength="3" runat="server" Text='<%#Eval("PaxType")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DOB" runat="server" CssClass="date" Text='<%#Eval("DOB")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="dll_gender" runat="server" DataTextField='<%#Eval("Gender")%>'>
                                                    <asp:ListItem>M</asp:ListItem>
                                                    <asp:ListItem>F</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_passport" runat="server" Text='<%#Eval("PassportNo")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport Ex.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_passportex" runat="server" Text='<%#Eval("PassportExpireDate")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nationality">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_NationalityCode" runat="server" Text='<%#Eval("NationalityCode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IssueCountry">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_IssueCountryCode" runat="server" Text='<%#Eval("IssueCountryCode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td colspan="7">
                        <asp:Button ID="btn_editpaxinfo" runat="server" CssClass="buttonfltbk"  OnClick="Button1_Click" Text="Update"  />
                    </td>
                </tr>
            </table>
        </div>
        <div class="main" runat="server" id="Div_Agent_EditBooikgINFO" visible="false">
            <table id="tblMainGroupFrame" class="table"   >
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td  style="height: 27px; background-color: #FFFFFF;" colspan="1">
                        <asp:RadioButton ID="rbtnDomestic" runat="server" Text="Domestic" GroupName="grpFlight"
                            ToolTip="Domestic Flight" Enabled="false" Style="cursor: pointer; font-size: 12px;" CssClass="ClsTripTravelType" />
                        <asp:RadioButton ID="rbtnInternational" Enabled="false" runat="server" Text="International" GroupName="grpFlight" CssClass="ClsTripTravelType"
                            ToolTip="International Flight" Style="cursor: pointer; font-size: 12px;" />
                    </td>
                    <td colspan="4" style="background-color: #FFFFFF; width:20%;">
                        <asp:RadioButton ID="rbtnOneWay" Enabled="false" CssClass="ClsOneWayOrMulty" runat="server" Text="One Way" GroupName="GrpTpType" Style="cursor: pointer; font-size: 12px;" ToolTip="One Way" />&nbsp;
                    <asp:RadioButton ID="rbtnRoundTrip" Enabled="false" runat="server" Text="Round Trip" GroupName="GrpTpType" Style="cursor: pointer; font-size: 12px;" ToolTip="Round Trip" />&nbsp;
                    <asp:RadioButton ID="rbtnMultiCity" runat="server" GroupName="GrpTpType" ToolTip="Multi City Flight"
                        Text="Multi City" Style="cursor: pointer; font-size: 12px;" Visible="False" />
                    </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
                <tr>
                    <td  style='font-size: 13px; width: 15%; text-align: center; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;'>Flight&nbsp; Details</td>
                </tr>   
                <tr>
                    
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="OneWayTrip" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" OnRowDataBound="OneWayTrip_RowDataBound" Width="100%" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Request No" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Request_no" runat="server" ReadOnly="true" Text='<%#Eval("ReqNo").ToString()%>'></asp:TextBox>
                                                <asp:Label ID="lbl_Reqnoid" runat="server" Text='<%#Eval("RequestID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trip From*" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_From" runat="server" ReadOnly="true" CssClass="CityName" Width="180px" Text='<%#Eval("Departure_Location").ToString()%>'></asp:TextBox>
                                                <asp:Label ID="lbl_DepAirportName" runat="server" Text='<%#Eval("Departure_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trip To*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_To" runat="server" ReadOnly="true" CssClass="CityName" Width="180px" Text='<%#Eval("Arrival_Location").ToString()%>'></asp:TextBox>
                                                <asp:Label ID="lbl_ArvlAirportName" runat="server" Text='<%#Eval("Arrival_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Date*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DepDate" ReadOnly="true" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DepTime" onkeypress="return isNumberKey(event)" ReadOnly="true" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Date*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ArvlDate" runat="server" ReadOnly="true" Text='<%#Eval("Arrival_Date")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ArvlTime" ReadOnly="true" onkeypress="return isNumberKey(event)" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Airline" runat="server" ReadOnly="true" CssClass="AirlineName" Text='<%#Eval("Aircode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flight No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FlightNo" ReadOnly="true" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gbdcounter" runat="server" Text='<%#Eval("GbdCounter")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_fgdcounter" runat="server" Text='<%#Eval("GfdCounter")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_SNO" runat="server" Text='<%#Eval("SNO")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_inbound" runat="server" visible="false">
                    <td style='font-size: 13px; width: 15%; text-align: center; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;'>Inbound Details</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="RoundTrip" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="table" GridLines="None" Width="100%" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trip From*" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_From" runat="server" CssClass="CityName" Width="180px" Text='<%#Eval("Departure_Location").ToString()%>'></asp:TextBox>
                                                <asp:Label ID="lbl_DepAirportName" runat="server" Text='<%#Eval("Departure_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trip To*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_To" runat="server" CssClass="CityName" Width="180px" Text='<%#Eval("Arrival_Location").ToString()%>'></asp:TextBox>
                                                <asp:Label ID="lbl_ArvlAirportName" runat="server" Text='<%#Eval("Arrival_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Date*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DepDate" runat="server" CssClass="date" Text='<%#Eval("Departure_Date")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DepTime" CssClass="my_time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Date*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ArvlDate" runat="server" CssClass="date" Text='<%#Eval("Arrival_Date")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ArvlTime" CssClass="my_time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Airline" runat="server" CssClass="AirlineName" Text='<%#Eval("Aircode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flight No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_FlightNo" CssClass="myFlight" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gbdcounter" runat="server" Text='<%#Eval("GbdCounter")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_fgdcounter" runat="server" Text='<%#Eval("GfdCounter")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="tr_PaxDetails" runat="server" visible="false"  >
                    <td style='font-size: 13px; width: 15%; text-align: center; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;'>Pax Details</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="paxdetails" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="table" GridLines="None" Width="100%" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_title" runat="server" Text='<%#Eval("Title")%>'></asp:TextBox>
                                                <asp:Label ID="lbl_paxcounter" runat="server" Text='<%#Eval("Counter")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_fname" runat="server" Text='<%#Eval("FName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Middle Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_mname" runat="server" Text='<%#Eval("MName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name*">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_lNAME" runat="server" Text='<%#Eval("LName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaxType">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_PaxType" MaxLength="3" runat="server" Text='<%#Eval("PaxType")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DOB" runat="server" CssClass="date" Text='<%#Eval("DOB")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="dll_gender" runat="server" DataTextField='<%#Eval("Gender")%>'>
                                                    <asp:ListItem>M</asp:ListItem>
                                                    <asp:ListItem>F</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_passport" runat="server" Text='<%#Eval("PassportNo")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport Ex.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_passportex" runat="server" Text='<%#Eval("PassportExpireDate")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nationality">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_NationalityCode" runat="server" Text='<%#Eval("NationalityCode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IssueCountry">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_IssueCountryCode" runat="server" Text='<%#Eval("IssueCountryCode")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="table">
                             <tr>
                    <th id="Noofadt" runat="server">No Of Adult*
                    </th>
                    <th id="Noofchd" runat="server">No Of Child*
                    </th>
                    <th id="Noofainf" runat="server">No Of Infant*
                    </th>
                    <th>Expected Fare*
                    </th>
                    <th>Remarks
                    </th>
                </tr>
                <tr>
                    <td>
                        <input name="nmOftxtNoOfPsgr" type="text" readonly="readonly" id="txtNoOfAdult" size="2" onkeypress="return NumericOnly(event);" runat="server" />
                    </td>
                    <td>
                        <input name="nmOftxtNoOfPsgr" type="text" readonly="readonly" id="txtNoOfChild" size="2" onkeypress="return NumericOnly(event);" runat="server" />
                    </td>
                    <td>
                        <input name="nmOftxtNoOfPsgr" type="text" readonly="readonly" id="txtNoOfInfant" size="2" onkeypress="return NumericOnly(event);" runat="server" />
                    </td>
                    <td>
                        <input type="text" id="txtExpectedFair" readonly="readonly" name="nmOftxtExpectedFair" onkeypress="return NumericOnly(event);" runat="server" />
                    </td>
                    <td>
                        <textarea name="textarea" id="textarea1" readonly="readonly" rows="5" style="width: 400px; height: 50px" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="align-items: center">
                        <asp:LinkButton ID="lb_submit" Visible="false" runat="server" Font-Bold="True" ForeColor="#0033CC" OnClick="lb_submit_Click" OnClientClick="return Validate(11);">Update</asp:LinkButton>
                        <asp:LinkButton ID="lb_reqcancl" runat="server" Font-Bold="True" ForeColor="Red" OnClick="lb_reqcancl_Click">Cancel Request</asp:LinkButton>
                    </td>
                </tr>
                        </table>
                    </td>
                </tr>
               
            </table>
           <div id="CancelRemarks" runat="server" visible="false">
               <table>
                   <tr>
                       <td>
                        <textarea name="textarea" id="txt_can_remarks" rows="5" style="width: 400px; height: 50px" runat="server"></textarea>
                           <asp:LinkButton ID="lnksubmitreq" runat="server" Font-Bold="True" ForeColor="#003399" OnClientClick="return loadimg();" OnClick="lnksubmitreq_Click">Submit</asp:LinkButton>
                           <asp:LinkButton ID="lnkcancelreq" runat="server" Font-Bold="True" ForeColor="#ff0000"  OnClick="lnkcancelreq_Click">Cancel</asp:LinkButton>
                       </td>
                   </tr>
               </table>
           </div>
        </div>
      <div class="clear1"></div>
        <div class="main" runat="server" id="Div_Agent_FinalBookingDetails" visible="false">
            <table id="tblMainGroupFrame_final" style="width: 100%; border-collapse: collapse;"  >
                <tr>
                    <td>Trip Type:<asp:Label ID="lbltriptyp" runat="server" colspan="6" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="2">Trip:<asp:Label ID="lbltrip" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="4" style="background-color: #FFFFFF;">Booking Status:<asp:Label ID="lbl_BkgSts" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style='font-size: 13px; width: 15%;'>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="finalBookingGrid" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" PageSize="30">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Trip From*" FooterStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbDepLoc" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                <asp:Label ID="lbl_DepAirportName" runat="server" Text='<%#Eval("Departure_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trip To*">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbArvlLoc" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                <asp:Label ID="lbl_ArvlAirportName" runat="server" Text='<%#Eval("Arrival_Location")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Date*">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbDepdate" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbDeptime" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Date*">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbArvltime" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival Time(HH:mm)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbArvlTime" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbAirline" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flight No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fbflgno" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_gbdcounter" runat="server" Text='<%#Eval("GbdCounter")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_fgdcounter" runat="server" Text='<%#Eval("GfdCounter")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <th>No Of Adult*
                    </th>
                    <th>No Of Child*
                    </th>
                    <th>No Of Infant*
                    </th>
                    <th>Expected Fare*
                    </th>
                    <th colspan="1">Remarks
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="FBtxt_totAdt" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="FBtxt_totchd" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="FBtxt_totinf" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="FBtxt_fare" runat="server"></asp:Label>
                    </td>
                    <td colspan="1">
                        <asp:Label ID="FBtxt_remarks" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="display: none"></td>
                </tr>
            </table>
        </div>
        <div class="large-12 medium-12 small-12" id="divpg" runat="server" visible="false">
            Payment Mode:&nbsp; 
                <asp:RadioButtonList ID="rblPaymentMode" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="WL">Wallet</asp:ListItem>
                    <asp:ListItem Value="OPTCRDC">Credit Card</asp:ListItem>
                    <asp:ListItem Value="OPTDBCRD">Debit Card</asp:ListItem>
                    <asp:ListItem Value="OPTNBK">Net Banking</asp:ListItem>
                    <asp:ListItem Value="OPTCASHC">Cash Card</asp:ListItem>
                    <asp:ListItem Value="OPTMOBP">Mobile Payments</asp:ListItem>
                </asp:RadioButtonList>
            <div class="large-12 medium-12 small-12" id="divpgCharge" style="display:none">
                <table>
                    <tr>
                        <td id="tdQuotePrice" runat="server">Booking Price :<asp:Label ID="lbl_tdQuotePrice" runat="server"></asp:Label></td>
                        <td id="tdpgcharge" runat="server">PG Charges : <asp:Label ID="lbl_tdpgcharge" runat="server"></asp:Label></td>
                        <td id="tdtotal" runat="server">Total Booking Price : <asp:Label ID="lbl_tdtotal" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="Div_Exec" runat="server" visible="false">
            <div></div>
            <div id="div_BookingDetails" runat="server">
                <table  style="    font-family: Arial, Helvetica, sans-serif !important;
    font-size: 12px !important;">
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Booking Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="BookingDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowCommand="BookingDetails_RowCommand"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Trip Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTripType" runat="server" Text='<%#Eval("TripType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adult">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdultCount" runat="server" CssClass="" Text='<%#Eval("AdultCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Child">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChildCount" runat="server" Text='<%#Eval("ChildCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Infant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInfantCount" runat="server" Text='<%#Eval("InfantCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExpectedPrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpactedPrice" runat="server" Text='<%#Eval("ExpactedPrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_status1" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="link_UpdateTktPnr"  runat="server" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Underline="False" OnClick="link_UpdateTktPnr_Click">Update Ticket/PNR</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="link_Reject_RefRequestID" runat="server" CommandArgument='<%#Eval("RequestID") %>'
                                                         Font-Underline="False" CommandName="CancelBooking">Cancel Booking</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="link_Payment" runat="server"  OnClientClick="return loadimg();" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Underline="False" OnClick="link_Payment_Click">MakePayment</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="link_paxInfor" runat="server"  OnClientClick="return loadimg();" CommandName="PaxInformation" 
                                                        CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Underline="False">Pax Information</asp:LinkButton>
                                                    <asp:Label ID="lbltrip" runat="server" Visible="false" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" Visible="false" runat="server" Height="47px" TextMode="MultiLine" Width="175px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                             <asp:TemplateField ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubmit_1" runat="server" Visible="false"
                                        CommandArgument='<%# Eval("RequestID") %>' OnClientClick="return loadimg();" CommandName="CancelReqSubmit"><img src="../Images/Submit.png" alt="Ok" /></asp:LinkButton><br />
                                    <asp:LinkButton ID="lnkHides_1" runat="server" Visible="false"
                                        CommandName="ReqCancel" CommandArgument='<%# Eval("RequestID") %>'><img src="../Images/Cancel.png" alt="Cancel" /></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" Total Booked Price" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbookedPrice" runat="server" Text='<%#Eval("BookedPrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Payment Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_status" runat="server" Text='<%#Eval("PaymentStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_paxinfo" runat="server" visible="false">
                <table  >
                    <tr>
                        <td style='font-size: 13px; width: 15%;  height:25px; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;'>Pax Information</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="pax_info" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("Title").ToString()+". "+Eval("FName").ToString()+" "+ Eval("LName").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pax Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ptype" runat="server" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_dob" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_gender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passport No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_passport" runat="server" Text='<%#Eval("PassportNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passport Ex.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_pexp" runat="server" Text='<%#Eval("PassportExpireDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nationality">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nationality" runat="server" Text='<%#Eval("NationalityCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IssueCountry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_issuecode" runat="server" Text='<%#Eval("IssueCountryCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_TktInfo" runat="server" visible="false">
                <table  >
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;' colspan="2">Ticket Details</td>
                    </tr>
                    <tr >
                        <td style='font-size: 13px; width: 15%;'>GDS Pnr Number :
                            <asp:Label ID="lbl_gdspnr" runat="server" Font-Bold="True" ForeColor="#FF9900"></asp:Label>
                        </td>
                        <td style='font-size: 13px; width: 15%;'>Airline Pnr Number :
                            <asp:Label ID="lbl_airlinePnr" runat="server" Font-Bold="True" ForeColor="#FF9900"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr_Inbond_pnrgds" runat="server" visible="false">
                        <td style='font-size: 13px; width: 15%;'>Inbond GDS Pnr Number :
                            <asp:Label ID="lbl_inbondgdspnr" runat="server" Font-Bold="True" ForeColor="#FF9900"></asp:Label>
                        </td>
                        <td style='font-size: 13px; width: 15%;'>Inbond Airline Pnr Number :
                            <asp:Label ID="lbl_inairline" runat="server" Font-Bold="True" ForeColor="#FF9900"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;' colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="tktInfromation" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Pax Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pax Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ptype" runat="server" CssClass="" Text='<%#Eval("PaxType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ticket Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_tktno" runat="server" Text='<%#Eval("TicketNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Ticket Number(Inbond)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lb_tktnoInbond" runat="server" Text='<%#Eval("INTicketNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_dob" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_gender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passport Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_passportno" runat="server" Text='<%#Eval("PassportNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expire Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Expire" runat="server" Text='<%#Eval("PassportExpireDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nationality">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_natnl" runat="server" Text='<%#Eval("NationalityCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div></div>
            <div></div>
            <div id="div_flightdetails1" runat="server" visible="false">
                <table  class="table" >
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="OFlightDetails1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowDataBound="OFlightDetails1_RowDataBound"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails1_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="OWayPrice1" runat="server" visible="false">
                <div runat="server" id="div2">
                    <table class="auto-style1"  >
                        <tr>
                            <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1" >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="OWayPrice1_txt_Quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="OWayPrice1_ddl_Partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid1" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="OWayPrice1_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="OWayPrice1_btn_submit" CssClass="buttonfltbk" runat="server" Text="Submit"   OnClientClick="return Validate(1);" OnClick="OWayPrice1_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote1" runat="server" visible="false">
                <table >
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:GridView ID="GVPriceQuote1" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                PageSize="30" OnRowCommand="GVPriceQuote1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="RequestID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QuotePrice">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Partner Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quote Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpdatedDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                 Font-Underline="False">Freeze</asp:LinkButton>
                                            <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                            <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offer valid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quote Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_flightdetails2" runat="server" visible="false">
                <table  >
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'> Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="OFlightDetails2" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowDataBound="OFlightDetails2_RowDataBound"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails2_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="OWayPrice2" runat="server" visible="false">
                <div runat="server" id="div3">
                    <table class="auto-style1" >
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="OWayPrice2_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="OWayPrice2_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                 <input id="txt_offervalid2" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="OWayPrice2_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="OWayPrice2_btn_submit" runat="server" CssClass="loader" Text="Submit" Width="75px" OnClientClick="return Validate(2);" OnClick="OWayPrice2_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote2" runat="server" visible="false">
                <table  >
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote2" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote2_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                          Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_flightdetails3" runat="server" visible="false">
                <table  >
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'> Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="OFlightDetails3" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowDataBound="OFlightDetails3_RowDataBound"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails3_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="OWayPrice3" runat="server" visible="false">
                <div runat="server" id="div5">
                    <table class="auto-style1"  >
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="OWayPrice3_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="OWayPrice3_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid3" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="OWayPrice3_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="OWayPrice3_btn_submit" runat="server" CssClass="loader" Text="Submit" Width="75px" OnClientClick="return Validate(3);" OnClick="OWayPrice3_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote3" runat="server" visible="false">
                <table  >
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote3" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote3_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                          Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_flightdetails4" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="OFlightDetails4" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowDataBound="OFlightDetails4_RowDataBound"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails4_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="OWayPrice4" runat="server" visible="false">
                <div runat="server" id="div7">
                    <table class="auto-style1"  >
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1"  >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="OWayPrice4_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="OWayPrice4_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid4" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="OWayPrice4_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="OWayPrice4_btn_submit" runat="server" CssClass="buttonfltbk" Text="Submit" Width="75px" OnClientClick="return Validate(4);" OnClick="OWayPrice4_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote4" runat="server" visible="false">
                <table  >
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote4" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote4_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                         Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div_flightdetails5" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="OFlightDetails5" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%" OnRowDataBound="OFlightDetails5_RowDataBound"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails5_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="OWayPrice5" runat="server" visible="false">
                <div runat="server" id="div9">
                    <table class="auto-style1" >
                        <tr>
                            <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1"  >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="OWayPrice5_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="OWayPrice5_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid5" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="OWayPrice5_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="OWayPrice5_btn_submit" CssClass="buttonfltbk" runat="server" Text="Submit"  OnClientClick="return Validate(5);" OnClick="OWayPrice5_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote5" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote5" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote5_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                         Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_flightdetails6" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RFlightDetails1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails6_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RoundTrip1" runat="server" visible="false">
                <div runat="server" id="div11">
                    <table class="auto-style1" >
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1">
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="RoundTrip1_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="RoundTrip1_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                              <input id="txt_offervalid6" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="RoundTrip1_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="RoundTrip1_btn_submit" runat="server" CssClass="buttonfltbk" Text="Submit" Width="75px" OnClientClick="return Validate(6);" OnClick="RoundTrip1_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote6" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote6" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote6_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                         Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div_flightdetails7" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #FFFFFF; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RFlightDetails2" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails7_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RoundTrip2" runat="server" visible="false">
                <div runat="server" id="div13">
                    <table class="auto-style1">
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1" >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="RoundTrip2_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="RoundTrip2_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid7" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="RoundTrip2_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="RoundTrip2_btn_submit" runat="server" CssClass="buttonfltbk" Text="Submit" Width="75px" OnClientClick="return Validate(7);" OnClick="RoundTrip2_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote7" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote7" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote7_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                          Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div_flightdetails8" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RFlightDetails3" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails8_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RoundTrip3" runat="server" visible="false">
                <div runat="server" id="div15">
                    <table class="auto-style1">
                        <tr>
                           <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1" >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="RoundTrip3_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="RoundTrip3_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                <input id="txt_offervalid8" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="RoundTrip3_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="RoundTrip3_btn_submit" runat="server" CssClass="loader" Text="Submit" Width="75px" OnClientClick="return Validate(8);" OnClick="RoundTrip3_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote8" runat="server" visible="false">
                <table  >
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote8" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote8_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                         Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div_flightdetails9" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RFlightDetails4" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails9_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RoundTrip4" runat="server" visible="false">
                <div runat="server" id="div17">
                    <table class="auto-style1">
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1"  >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="RoundTrip4_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="RoundTrip4_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp;OfferValid :
                                <input id="txt_offervalid9" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp;&nbsp; Remarks :<asp:TextBox ID="RoundTrip4_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="RoundTrip4_btn_submit" runat="server" CssClass="buttonfltbk" Text="Submit" Width="75px" OnClientClick="return Validate(9);" OnClick="RoundTrip4_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote10" runat="server" visible="false">
                <table>
                    <tr>
                         <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote9" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote9_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                          Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="div_flightdetails10" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>&nbsp;Request Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px;' colspan="8">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RFlightDetails5" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_RequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Departure_Location" runat="server" Text='<%#Eval("Departure_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Departure_Date" runat="server" Text='<%#Eval("Departure_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dep Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Departure_Time" runat="server" Text='<%#Eval("Departure_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Arrival_Location" runat="server" Text='<%#Eval("Arrival_Location")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Arrival_Date" runat="server" Text='<%#Eval("Arrival_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arvl Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Arrival_Time" runat="server" Text='<%#Eval("Arrival_Time")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aircode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Aircode" runat="server" Text='<%#Eval("Aircode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flight No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_FlightNumber" runat="server" Text='<%#Eval("FlightNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_flightdetails10_Trip" runat="server" Text='<%#Eval("Trip")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="RoundTrip5" runat="server" visible="false">
                <div runat="server" id="div19">
                    <table class="auto-style1">
                        <tr>
                             <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Partner Quote Price</td>
                        </tr>
                    </table>
                    <table class="auto-style1" >
                        <tr>
                            <td style='font-size: 13px; width: 15%;'>Quote Price
                        <asp:TextBox ID="RoundTrip5_txt_quoteprice" runat="server" size="8" MaxLength="12" onkeypress="return NumericOnly(event);" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; Partner :
                            <asp:DropDownList ID="RoundTrip5_ddl_partner" runat="server" Width="75px">
                                <asp:ListItem Selected="True" Value="YA">Yatra</asp:ListItem>
                                <asp:ListItem>TBO</asp:ListItem>
                                <asp:ListItem Value="EY">Ease My Trip</asp:ListItem>
                                <asp:ListItem Value="G1">Galileo</asp:ListItem>
                            </asp:DropDownList>
                                &nbsp; OfferValid :
                                 <input id="txt_offervalid10" type="text" readonly="true" class="date" runat="server" style="width:75px" />
                                &nbsp; Remarks :<asp:TextBox ID="RoundTrip5_txt_remarks" runat="server" Height="50px" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                        <asp:Button ID="RoundTrip5_btn_submit" runat="server" CssClass="buttonfltbk" Text="Submit" Width="75px" OnClientClick="return Validate(10);" OnClick="RoundTrip5_btn_submit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div></div>
            </div>
            <div id="PriceQuote9" runat="server" visible="false">
                <table>
                    <tr>
                        <td style='font-size: 13px; width: 15%; text-align: left; height:25px; color: #fff; padding: 5px; font-weight: bold; background-color: #004b91;'>Price Quote Details</td>
                    </tr>
                    <tr>
                        <td style='font-size: 13px;; width: 15%; text-align: center;'>
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GVPriceQuote10" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" Width="100%"
                                        PageSize="30" OnRowCommand="GVPriceQuote10_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RequestID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QuotePrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuotePrice" runat="server" Text='<%#Eval("QuotePrice")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBKG_PartnerName" runat="server" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" class="gridViewToolTip" onclick='openPopup("<%# Eval("Remarks")%>")'>Remarks</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UpdatedDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%#Eval("UpdatedDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Accept" runat="server" OnClientClick="return loadimg();" CommandName="Accept" CommandArgument='<%#Eval("SNO") %>'
                                                          Font-Underline="False">Freeze</asp:LinkButton>
                                                    <asp:Label ID="lblBKG_PName" runat="server" Visible="false" Text='<%#Eval("BKG_PartnerName")%>'></asp:Label>
                                                    <asp:Label ID="lblcounter" runat="server" Visible="false" Text='<%#Eval("counter")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer valid">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvalidoffer" runat="server" Text='<%#Eval("ValidOffer")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ITZ_Refund" runat="server" CommandName="Refund" CommandArgument='<%#Eval("RequestID") %>'
                                                        Font-Bold="True" Font-Underline="False">Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quote Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreezed" runat="server" Font-Bold="True" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNO" runat="server" Visible="false" Text='<%#Eval("SNO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </div></div>
        <%--<div id="waitMessage" style="display: none;">
            <div class="" style="text-align: center; opacity: 0.7; position: absolute; z-index: 99999; top: 10px; width: 100%; height: 100%; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000;  border-radius: 10px;">
                Please wait....<br />
                <br />
                <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
                <br />
            </div>
        </div>--%>
        <div id="waitMessage" style="display: none;">
        <div class="" style="text-align: center; opacity: 0.9; position: fixed; z-index: 99999; top: 0px; width: 100%; height: 100%; background-color: #afafaf; font-size: 12px;
                        font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000;">
            <div style="    position: absolute; top: 264px; left: 45%; font-size: 18px; color: #fff;">
            Please wait....<br />
            <br />
            <img alt="loading" src="<%=ResolveUrl("~/images/loadingAnim.gif")%>" />
            <br />
                </div>
        </div>
    </div>
        <div id="popupdiv" title="Basic modal dialog" style="display: none">
            Remarks:
            <label id="lbl_Remarks"></label>
        </div>
        <link href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <script type="text/javascript">
            function openPopup(Remarks) {
                $('#lbl_Remarks').text(Remarks);
                $("#popupdiv").dialog({
                    title: "Remarks",
                    width: 400,
                    height: 350,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });
            }
            function MyFunc(strmsg) {
                switch (strmsg) {
                    case 1: {
                        alert("Booking has been cancelled");
                        window.opener.location.reload('GroupDetails.aspx')
                        window.close();
                    }
                        break;
                    case 2: {
                        alert("Your Request has been accepted successfully!!");
                        window.opener.location.reload('GroupDetails.aspx')
                        window.close();
                    }
                        break;
                    case 3: {
                        alert("Something went worng,Please contact with admin !!");
                        window.opener.location.reload('GroupDetails.aspx')
                        window.close();
                    }
                        break;
                    case 4: {
                        alert("Booking has been cancelled!!");
                        window.opener.location.reload('ExecRequestDetails.aspx');
                        window.close();
                    }
                        break;
                    case 5: {
                        alert("Booking has been cancelled!!");
                        window.opener.location.reload('GroupDetails.aspx')
                        window.close();
                    }
                        break;
                    case 6: {
                        alert("Something went worng,Please contact with admin !!");
                        window.opener.location.reload('ExecRequestDetails.aspx');
                        window.close();
                    }
                        break;
                    case 7: {
                        alert("Request is already freezed or paid !!");
                        window.opener.location.reload('GroupDetails.aspx');
                        window.close();
                    }
                        break;
                    case 8: {
                        alert("Offer has been expired!!");
                        window.opener.location.reload('GroupDetails.aspx');
                        window.close();
                    }
                        break;
                    case 9: {
                        alert("Quote has been submitted successfully !!");
                        window.opener.location.reload();
                    }
                        break;
                    case 10: {
                        alert("Remark can not be blank,Please Fill Remark");
                        $("#waitMessage").hide();
                    }
                        break;
                }
            }
        </script>
    </form>
</body>
</html>
