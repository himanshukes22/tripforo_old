<%@ Page Title="" Language="VB" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="false"
    CodeFile="AcceptRequestSeriesDepart.aspx.vb" Inherits="SprReports_OLSeries_AcceptRequestSeriesDepart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/Series.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/main2.css" rel="stylesheet" type="text/css" />
    <link href="../../css/StyleSheet.css" rel="stylesheet" type="text/css" />
     <script src="JS/JScript.js" type="text/javascript"></script>
     <div>
      <asp:UpdatePanel ID="UP" runat="server">
                        <ContentTemplate>
         <div id="divrequest" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 50px;" class="Heading">
                    Series Departure Request<br />
                    <asp:LinkButton ID="lnkviewProcess" runat="server" Text="(Click here to see Inprocess details)"
                        Font-Bold="True" Font-Underline="False" ForeColor="#000099" Font-Size="12pt"></asp:LinkButton>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table cellpadding="0" cellspacing="0" width="100%" >
                        <tr>
                            <td>
                                <asp:GridView ID="grd_requestseries" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    Width="100%">
                                    <Columns>
                                     <asp:TemplateField HeaderText="AgentID">
                                            <ItemTemplate>
                                               
                                                  <a href='../Admin/Update_Agent.aspx?AgentID=<%#Eval("Agent_ID") %> ' target="_blank"
                                                rel="lyteframe" rev="width: 850px; height: 400px; overflow:hidden;" 
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                color: #004b91; text-decoration: underline;" ><asp:Label ID="lblagentid" runat="server" Text='<%#Eval("Agent_ID") %>'></asp:Label></a>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="AgencyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgencyName" runat="server" Text='<%#Eval("Agency_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactPersonName" runat="server" Text='<%#Eval("ContactPerson_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactNo" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactEmailID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactEmail_Id" runat="server" Text='<%#Eval("ContactEmail_Id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lblairlinere" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Itinerary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaircodere" runat="server" Text='<%#Eval("Airline_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsectorre" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamtre" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Available Seats">
                                            <ItemTemplate>
                                                <asp:Label ID="lblavlseatre" runat="server" Text='<%#Eval("Available_Seat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Departure Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeptdatere" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblretdatere" runat="server" Text='<%#Eval("Ret_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Pax">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotpaxre" runat="server" Text='<%#Eval("NoOfPax") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adult">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladtre" runat="server" Text='<%#Eval("NoOfadult") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Child">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchdre" runat="server" Text='<%#Eval("NoOfChild") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Infant">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfre" runat="server" Text='<%#Eval("NoOfInfant") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblseriesidreq" Visible="false" runat="server" Text='<%#Eval("SeriesID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarkre" runat="server" Text='<%#Eval("Agent_Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="RequestedDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreated_date" runat="server" Text='<%#Eval("created_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accept/Reject">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_accept" runat="server" Text="Accept" CommandName="accept" CommandArgument='<%#Eval("Counter") %>' CssClass="newbutton_2" Width="70px" ></asp:Button><asp:Button ID="btn_reject" runat="server" Text="Reject" CommandName="reject" CommandArgument='<%#Eval("Counter") %>' CssClass="newbutton_2"  Width="70px" ></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" Height="35px" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divProcess" runat="server" visible="false">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 50px;" class="Heading">
                    Series Departure Inprocess<br />
                    <asp:LinkButton ID="lnkclose" runat="server" Text="(Click here to see Request details)"
                        Font-Bold="True" Font-Underline="False" ForeColor="#000099" Font-Size="12pt"></asp:LinkButton>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grd_processSeries" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Agent Id">
                                            <ItemTemplate>
  <a href='../Admin/Update_Agent.aspx?AgentID=<%#Eval("Agent_ID") %> '
                                                rel="lyteframe" rev="width: 850px; height: 400px; overflow:hidden;"  target="_blank"
                                                style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;
                                                color: #004b91; text-decoration: underline;" ><asp:Label ID="lblagentid" runat="server" Text='<%#Eval("Agent_ID") %>'></asp:Label></a>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="AgencyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIAgencyName1" runat="server" Text='<%#Eval("Agency_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactPersonName1" runat="server" Text='<%#Eval("ContactPerson_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIContactNo1" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ContactEmailID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactEmail_Id1" runat="server" Text='<%#Eval("ContactEmail_Id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Counter" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCounter" runat="server" Text='<%#Eval("Counter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline">
                                            <ItemTemplate>
                                                <asp:Label ID="lblairline" runat="server" Text='<%#Eval("AirlineName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sector">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsector" runat="server" Text='<%#Eval("Sector") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available Seats" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblavlseat" runat="server" Text='<%#Eval("Available_Seat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Booked Seats" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_updAvlseat" onkeypress="return ValidateNoPax(event);" runat="server" Visible="false" Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Itinerary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaircode" runat="server" Text='<%#Eval("Airline_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeptdate" runat="server" Text='<%#Eval("Depart_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblretdate" runat="server" Text='<%#Eval("Ret_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TotalPax">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotpax" runat="server" Text='<%#Eval("NoOfPax") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adult">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladt" runat="server" Text='<%#Eval("NoOfadult") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Child">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchd" runat="server" Text='<%#Eval("NoOfChild") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Infant">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinf" runat="server" Text='<%#Eval("NoOfInfant") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremark" runat="server" Text='<%#Eval("Agent_Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="RequestedDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreated_date" runat="server" Text='<%#Eval("created_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblseriesid" Visible="false" runat="server" Text='<%#Eval("SeriesID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" Text="Update" CommandName="Change" CommandArgument='<%#Eval("SeriesID") %>' CssClass="newbutton_2" Width="70px"
                                                    Visible="false" />
                                                <asp:Button ID="Button2" runat="server" Text="Cancel" CommandName="Can" Visible="false" CssClass="newbutton_2" Width="70px"/>
                                                <asp:Button ID="btn_reject" runat="server" Text="Modify" CommandName="modify" CommandArgument='<%#Eval("Counter") %>' CssClass="newbutton_2" Width="70px"/>
                                                <asp:Button ID="btnreject" runat="server" Text="Reject" CommandName="reject1" CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" Width="70px"/>
                                                <asp:Button ID="btnrelease" runat="server" Text="Release" CommandName="release" CommandArgument='<%#Eval("Counter")%>' CssClass="newbutton_2" Width="70px"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <HeaderStyle CssClass="HeaderStyle" Height="35px" />
                                    <EditRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
                            <br>
                 </br>  
                            
                            <table ID="rejectTD" runat="server" align="center" cellpadding="0" 
                                cellspacing="10" class="tbltbl" visible="false" width="90%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="right" style="font-family: arial, Helvetica, sans-serif; font-size: medium;
                            color: #009900; font-weight: bold; padding-right: 15px;" width="180">
                                                    &nbsp;Comment:
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txt_rejectRmk" runat="server" CssClass="multitxt" 
                                                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left" style="padding-top: 5px">
                                                    <asp:Button ID="btn_submit" runat="server" CssClass="button" Text="Reject" />
                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="button" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
    
    </ContentTemplate>
                    </asp:UpdatePanel>
                     <%--<asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="UP">
                        <ProgressTemplate>
                            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden;
                                padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5;
                                z-index: 1000;">
                            </div>
                            <div style="position: fixed; top: 30%; left: 43%; padding: 10px; width: 20%; text-align: center;
                                z-index: 1001; background-color: #fff; border: solid 1px #000; font-size: 12px;
                                font-weight: bold; color: #000000">
                                Please Wait....<br />
                                <br />
                                <img alt="loading" src="<%= ResolveUrl("~/images/loadingAnim.gif")%>" />
                                <br />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
    </div>

</asp:Content>
