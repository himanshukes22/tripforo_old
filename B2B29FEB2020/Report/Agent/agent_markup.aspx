<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageForDash.master" AutoEventWireup="false"
    CodeFile="agent_markup.aspx.vb" Inherits="Reports_Agent_agent_markup" %>

<%@ Register Src="~/UserControl/Settings.ascx" TagPrefix="uc1" TagName="Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <%-- <link href="../../css/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <style>
        label {
            color: #000;
        }

        table {
            /*width: 750px;*/
            border-collapse: collapse;
            /*margin:50px auto;*/
        }

        tr:nth-of-type(odd) {
            background: #eee;
        }

        th {
            background: #2e323e !important;
            color: white;
            /*font-weight: bold;*/
        }

        td, th {
            padding: 2px;
            border: 1px solid #ccc;
            text-align: left;
            /*font-size: 18px;*/
        }


        @media only screen and (max-width: 760px), (min-device-width: 768px) and (max-device-width: 1024px) {

            table {
                width: 100%;
            }


            table, thead, tbody, th, td, tr {
                display: block;
                padding: inherit;
            }


                thead tr {
                    position: absolute;
                    top: -9999px;
                    left: -9999px;
                }

            tr {
                border: 1px solid #ccc;
            }

            td {
                border: none;
                border-bottom: 1px solid #eee;
                position: relative;
                padding-left: 50%;
            }

                td:before {
                    position: absolute;
                    top: 6px;
                    left: 6px;
                    width: 45%;
                    padding-right: 10px;
                    white-space: nowrap;
                    content: attr(data-column);
                    color: #000;
                    font-weight: bold;
                }
        }
    </style>
    <script type="text/javascript" src="../../js/chrome.js"></script>
    <script type="text/javascript">
        function phone_vali() {
            if ((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode == 32) || (event.keyCode == 45))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function validate() {
            if (document.getElementById("<%=mk.ClientID%>").value == "" || document.getElementById("<%=mk.ClientID%>").value == "0") {
                 alert("Please provide markup value.");
                 debugger;
                 document.getElementById("<%=mk.ClientID%>").focus();
              return false;
          }

      }
    </script>


      <ol class="breadcrumb-arrow">
        <li><a href="/Search.aspx"><i class="fa fa-home"></i></a></li>
        <li><a href="#">Settings</a></li>
        <li><a href="#">Domestic Airline Markup</a></li>
        
    </ol>



     <div class="card-main">

   
    <div class="card-body">
        <div class="inner-box ">
          <div class="row">
                <div class="col-md-3">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-user"></i>
                    <asp:TextBox runat="server" ID="uid" placeholder="Agent ID" class="theme-search-area-section-input" ReadOnly="true"></asp:TextBox>
                                </div>
                        </div>
                </div>
                <div class="col-md-3">
                  <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-plane"></i>
                    <asp:DropDownList runat="server" ID="air" CssClass="theme-search-area-section-input">
                        <asp:ListItem>Select Airline</asp:ListItem>
                          <asp:ListItem Value="ALL" Selected="true">ALL</asp:ListItem>
                        <asp:ListItem Value="AI">Air India</asp:ListItem>
                        <asp:ListItem Value="G8S">GoAir Special</asp:ListItem>
                        <asp:ListItem Value="AIS">Air India Special</asp:ListItem>
                        <asp:ListItem Value="SGS">Spice Jet Special</asp:ListItem>
                        <asp:ListItem Value="9W">Jet Airways</asp:ListItem>
                        <asp:ListItem Value="SG">Spice Jet</asp:ListItem>
                        <asp:ListItem Value="9WS">Jet Airways Special</asp:ListItem>
                        <asp:ListItem Value="6E">Indigo</asp:ListItem>
                        <asp:ListItem Value="G8">GoAir</asp:ListItem>
                        <asp:ListItem Value="6ES">Indigo Special</asp:ListItem>
                        <asp:ListItem Value="UK">Air Vistara</asp:ListItem>
                        <asp:ListItem Value="UKS">Air Vistara Special</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                      </div>
                </div>
                <div class="col-md-3">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                    <asp:TextBox runat="server" ID="mk" CssClass="theme-search-area-section-input" placeholder="Set MarkUp Per Pax"></asp:TextBox>                   
                </div>
                        </div>
                            </div>
                <div class="col-md-3" id="tr_Cat" runat="server">
                    <div class="theme-search-area-section theme-search-area-section-line">
                            <div class="theme-search-area-section-inner">
                                <i class="theme-search-area-section-icon lin lin-tag"></i>
                    <asp:DropDownList ID="ddl_MarkupType" runat="server" CssClass="theme-search-area-section-input" placeholder="MarkUp Type">
                        <asp:ListItem Value="F" Selected="true">--Select Markup Type--</asp:ListItem>
                        <asp:ListItem Value="F" >Fixed</asp:ListItem>
                        <asp:ListItem Value="P" ForeColor="#999999">Percentage</asp:ListItem>
                    </asp:DropDownList>
                                </div>
                        </div>
                </div>
               </div>
            <br />
            <div class="row">
               <div class="btn-add col-md-3">
                           <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" class="btn cmn-btn"  OnClientClick ="return validate();" Text="New Entry"/>
                            </div>
         </div>
            

              </div>
       
    </div>
</div>     
     
        
    <div class="card-main">
            <div class="table-responsive">
                <asp:UpdatePanel ID="UP" runat="server">
                    <ContentTemplate>
                       
                            <div class="col-md-1">
                                <asp:Label ID="lbl" runat="server" Style="color: #CC0000;"></asp:Label>
                            </div>
                            

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="counter"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting"
                                OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="8"
                                CssClass="rtable domestic-table" GridLines="None" Font-Size="12px">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:TemplateField HeaderText="Agent ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluser_id" runat="server" Text='<%#Eval("user_id")%>'></asp:Label>
                                            <asp:Label ID="lbl_counter" Visible="false" runat="server" Text='<%#Eval("counter")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Airline" HeaderText="Airline" ControlStyle-CssClass="textboxflight1"
                                        ReadOnly="true" ItemStyle-CssClass="passenger" HeaderStyle-CssClass="passenger" />
                                    <asp:TemplateField HeaderText="Markup Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MarkupValue" runat="server" Text='<%#Eval("Markup")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_markupvalue" runat="server" onkeypress="return checkit(event)" Text='<%#Eval("Markup")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Markup Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MarkupType" runat="server" Text='<%#Eval("Markup_type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddl_mktyp" runat="server">
                                                <asp:ListItem Value="F">Fixed (F)</asp:ListItem>
                                                <asp:ListItem Value="P">Percentage (P)</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    
                                </Columns>

                            </asp:GridView>

                
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                </asp:UpdateProgress>
            </div>

        </div>
 



       


     
    <script type="text/javascript">
        function checkit(evt) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (!(charCode == 46 || charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 || charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 || charCode == 8)) {
                return false;
            }
            status = "";
            return true;
        }
        function MyFunc(strmsg) {
            switch (strmsg) {
                case 1: {
                    alert("Data has been updated successfully !!");
                }
                    break;
                case 2: {
                    alert("Markup cannot be blank");
                }
                    break;
                case 3: {
                    alert("MarkUp For This AirLine Is Already Exist, Please Update.");
                }
                    break;
                case 4: {
                    alert("Record Added Successfully.");
                }
                    break;
            }
        }

    </script>

</asp:Content>


