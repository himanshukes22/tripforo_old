<%@ Page Title="" Language="C#" MasterPageFile="~/BS/BSer.master" AutoEventWireup="true"
    CodeFile="BusResult.aspx.cs" Inherits="BS_BusResult" %>

<%@ Register Src="~/BS/UserControl/BusSearch.ascx" TagName="BusSearch" TagPrefix="UC1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=ResolveUrl("~/BS/CSS/CommonCss.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/BS/CSS/basic.css")%>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function abc(id) {
            $("#" + id).click(function() {
                $("#cs11").hide();
                $("#ms1").hide();
                $("#bf1").hide();
                $("*").removeClass("tbs1");
                $(this).addClass("tbs1");
                $("#" + id + "1").show();
            });
        }

    </script>
   <script  type="text/javascript">
       $(document).ready(function () {
           $(".cls").click(function () {
                
               $(this).hide();

           });

       });
</script>
    <div style="width:80%;margin-left:auto; margin-right:auto;">

        <div class="tbs tbs1" id="cs1" onclick="abc(this.id)">
            Current Search
        </div>
        <div class="tbs" id="ms" onclick="abc(this.id)">
            Modify Search
        </div>
        <div class="tbs" id="bf" onclick="abc(this.id)">
            Bus Filter
        </div>
        <div class="clear">
        </div>
        <div id="cs11" class="tbsb" style="display: block;">
            <div id="CS" style="color: #000;">
            </div>
        </div>
        <div id="ms1" class="tbsb">
            <UC1:BusSearch ID="search" runat="server"></UC1:BusSearch>
        </div>
        <div id="bf1" class="tbsb">
            <table cellpadding="0" cellspacing="10" border="0" style="width: 80%;">
                <tr>
                    <td id="BusMatrix1" style="border: 1px solid #eee; padding: 20px; -webkit-border-radius: 10px;
                        line-height: 25px;">
                    </td>
                    <td id="BusMatrix2" style="border: 1px solid #eee; padding: 20px; -webkit-border-radius: 10px;
                        line-height: 25px;">
                    </td>
                    <td valign="top" style="border: 1px solid #eee; padding: 20px; -webkit-border-radius: 10px;
                        box-shadow: 0px 2px 3px #eee;">
                        <div style='font-size: 15px; font-weight: bold; text-decoration: underline; margin-bottom: 10px;'>
                            Filter By Fare</div>
                        <div style="float: left;">
                            <p>
                                INR
                                <input type="text" id="amount1" style="border-style: none; height: 15px; width: 45px;" />
                                <input type="hidden" id="minRtRange" name="minRtRange" runat="server" />
                            </p>
                        </div>
                        <div style="float: right;">
                            <p>
                                INR
                                <input type="text" id="amount2" style="border: 0; height: 15px; width: 40px;" />
                                <input type="hidden" id="maxRtRange" name="maxRtRange" runat="server" /></p>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <hr style="border: 2px dotted #ccc;" />
                        <div id="slider-range">
                        </div>
                    </td>
                    <td valign="top" style="border: 1px solid #eee; padding: 20px; -webkit-border-radius: 10px;
                        box-shadow: 0px 2px 3px #eee;">
                        <div style='font-size: 15px; font-weight: bold; text-decoration: underline; margin-bottom: 10px;'>
                            Filter By Departure Time</div>
                        <div style="float: left;">
                            <p>
                                <input type="text" id="Time1" style="border-style: none; height: 15px; width: 60px;" />
                                <input type="hidden" id="minDepTime" name="minDepTime" runat="server" />
                            </p>
                        </div>
                        <div style="float: right;">
                            <input type="text" id="Time2" style="border: 0; height: 15px; width: 60px;" />
                            <input type="hidden" id="maxDepTime" name="maxDepTime" runat="server" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <hr style="border: 2px dotted #ccc;" />
                        <div id="slider-Deptime">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
    </div>
    <div id="divresult" align="center">
    </div>
    <div class="b-modal __b-popup1__" style="background-color: rgb(0, 0, 0); position: fixed; top: 0px; right: 0px; bottom: 0px; left: 0px; opacity: 0.7; z-index: 9998; cursor: pointer;" ></div>
    <div id="divseat" class="divseat" style=" left: 333px; position: absolute; top: 21px; z-index: 9999;  opacity: 1;">
        <img src="<%= ResolveUrl("~/Images/cls.png")%>" class="cls" align="right" />
    </div>
    <div id="basic-modal-content" style="display: none" class="waittd">
        <strong>We are searching for best price.Please Wait...</strong><br />
        <br />
        <img src="Images/loaderB64.gif" /><br />
        <br />
        <br />
        <div id="source" class="divsrc">
        </div>
    </div>

    <script src="<%= ResolveUrl("~/BS/JS/jquery.blockUI.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/BS/JS/HandleQueryString.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/BS/JS/BusSearch.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/BS/JS/jquery.dataTables.min.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/BS/JS/BusResult.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/BS/JS/jquery.bpopup.min.js")%>" type="text/javascript"></script>
  
    
</asp:Content>
