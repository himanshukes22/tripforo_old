<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="AirlineFee.aspx.vb" Inherits="Reports_Admin_AirlineFee" %>
<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <style>
        /* RESET */ html, body, div, span, h1, h2, h3, h4, h5, h6, p, blockquote, a, font, img, dl, dt, dd, ol, ul, li, legend, table, tbody, tr, th, td {
            margin: 0px;
            padding: 0px;
            border: 0;
            outline: 0;
            font-weight: inherit;
            font-style: inherit;
            font-size: 100%;
            font-family: inherit;
            list-style: none
        }

            a img {
                border: none;
            }

            ol li {
                list-style: decimal outside;
            }

        fieldset {
            border: 0;
            padding: 0;
        }

        body {
            font-family: sans-serif;
            font-size: 1em;
        }

        div#container {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }

        p {
            margin: 1em 0;
            max-width: 700px;
        }

        h1 + p {
            margin-top: 0;
        }

        h1, h2 {
            font-family: Georgia, Times, serif;
        }

        h1 {
            font-size: 2em;
            margin-bottom: .75em;
        }

        h2 {
            font-size: 1.5em;
            margin: 2.5em 0 .5em;
            border-bottom: 1px solid #999;
            padding-bottom: 5px;
        }

        h3 {
            font-weight: bold;
        }

        ul li {
            list-style: disc;
            margin-left: 1em;
        }

        ol li {
            margin-left: 1.25em;
        }

        div.side-by-side {
            width: 100%;
            margin-bottom: 1em;
        }

            div.side-by-side > div {
                float: left;
                width: 50%;
            }

                div.side-by-side > div > em {
                    margin-bottom: 10px;
                    display: block;
                }

        a {
            color: Navy;
            text-decoration: underline;
        }

        .faqs em {
            display: block;
        }

        .clearfix:after {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }

        footer {
            margin-top: 2em;
            border-top: 1px solid #666;
            padding-top: 5px;
        }
    </style>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../chosen/chosen.css" />

    <script src="../../chosen/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script src="../../chosen/chosen.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>

    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode


            if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8)) {
                //                alert("This field accepts only Float Number.");
                return false;


            }

            status = "";
            return true;

        }


    </script>
    <div class="mtop80">
     <div class="large-12 medium-12 small-12">
      <div class="large-3 medium-3 small-12 columns">
       
                <uc1:Settings runat="server" ID="Settings" />
            
    </div>
    <div class="large-8 medium-8 small-12 columns heading end">
                                            <div class="large-12 medium-12 small-12 heading1">Airline Fee
                            </div>
                                            <div class="clear1"></div>
  
     
            <div class="redlnk large-6 medium-6 small-12">
                <div class="center">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged ="RadioButtonList1_SelectedIndexChanged">
                        <asp:ListItem Text="Domestic" Value="D" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="International" Value="I" ></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            
                        <div class="redlnk large-12 medium-12 small-12">
                            <div class="large-2 medium-2 small-4 columns">Airline Code:
                            </div>
                            <div  class="large-3 medium-3 small-8 columns">
                                <asp:DropDownList runat="server" data-placeholder="Choose a Airline..." TabIndex="2"
                                    ID="airline_code"><%--CssClass="chzn-select"--%>
                                </asp:DropDownList>
                            </div>
                       
                       
                            <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2">Sevice Tax :
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:TextBox runat="server" ID="txt_SeviceTax"  CssClass="combobox"
                                    onKeyPress="return checkit(event)" Text="0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="txt_SeviceTax"
                                    ErrorMessage="*" Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                       <div class="clear1"></div>
             <div class="redlnk large-12 medium-12 small-12">
                            <div class="large-2 medium-2 small-4 columns">Transaction Fee :
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:TextBox runat="server" ID="txt_TrasFee" CssClass="combobox"
                                    onKeyPress="return checkit(event)" Text="0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_TrasFee"
                                    ErrorMessage="*" Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                <%--<asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                                                Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>--%>
                            </div>
                        
                            <div class="large-2 medium-2 small-4 columns large-push-2 medium-push-2"">IATA Commission :
                            </div>
                            <div class="large-3 medium-3 small-8 columns">
                                <asp:TextBox runat="server" ID="txtIataComm" CssClass="combobox"
                                    onKeyPress="return checkit(event)" Text="0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RQFV" runat="server" ControlToValidate="txtIataComm"
                                    ErrorMessage="*" Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>
                                <%--<asp:RequiredFieldValidator ID="RFVMK" runat="server" ControlToValidate="mk" ErrorMessage="*"
                                                Display="dynamic"><span style="color:#FF0000">*</span></asp:RequiredFieldValidator>--%>
                            </div>
                 </div>
            <div class="clear1"></div>
                        
            <div class="large-12 medium-12 small-12">
                <div class="large-4 medium-4 small-12 large-push-8 medium-push-8">
                                        <div class="large-6 medium-6 small-6 columns">
                                        <asp:Button ID="btnAdd" runat="server" Text="New Entry"  OnClientClick="return confirm('Are you sure you want to add this?');" /></div>
                                            
                                            <div class="large-6 medium-6 small-6 columns">&nbsp<asp:Button
                                            ID="btn_Submit" runat="server" Text="Search"/></div>
                                        &nbsp;&nbsp;&nbsp;</div>
                                        <asp:Label ID="lbl" runat="server" Font-Bold="True" Style="color: #CC0000;"></asp:Label>
                                    </div>
                    <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
                           
                                
                                    
                               <div class="clear1"></div>
                            <div class="large-12 medium-12 small-12" >
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                            CssClass="table table-hover" GridLines="None" Font-Size="12px" >
                                            <Columns>
                                                <asp:CommandField ShowEditButton="True" />
                                                <%-- <asp:BoundField DataField="counter" HeaderText="Sr.No" ReadOnly="True" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Counter") %>' CssClass="hide"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <asp:BoundField DataField="UserId" HeaderText="Agent ID" ControlStyle-CssClass="textboxflight1"
                                            ReadOnly="true" />--%>
                                                <asp:BoundField DataField="AirlineCode" HeaderText="AirlineCode" ControlStyle-CssClass="textboxflight1"
                                                    ReadOnly="true">
                                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SrvTax" HeaderText="Service Tax" ControlStyle-CssClass="textboxflight1">
                                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TranFee" HeaderText="TransFee" ControlStyle-CssClass="textboxflight1">
                                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IATAComm" HeaderText="IATA Comm." ControlStyle-CssClass="textboxflight1">
                                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Trip" HeaderText="Trip" ControlStyle-CssClass="textboxflight1"
                                                    ReadOnly="true">
                                                    <ControlStyle CssClass="textboxflight1"></ControlStyle>
                                                </asp:BoundField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                           
                                        </asp:GridView>
                                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <div class="clear1"></div>
                <div class="large-12 medium-12 small-12">
                    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UP">
                        <ProgressTemplate>
                            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
                            </div>
                            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center; z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px; font-weight: bold; color: #000000">
                                Please Wait....<br />
                                <br />
                                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                <br />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress></div>
               
            </div>
        </div>
    </div>
         </div>
</asp:Content>
