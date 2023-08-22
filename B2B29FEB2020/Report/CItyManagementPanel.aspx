<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true" CodeFile="CItyManagementPanel.aspx.cs" Inherits="SprReports_CItyManagementPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <style type="text/css">
      .frmContainer{
          width:60%;
          margin:auto;
      }
      .gv {
          width:60%;
          margin:auto;
          color:#93928d;
      }
      .gv th{
          height:35px;
          background-color:#7e1818;
          color:#fff;
      }
      .table{
          width:60%;
          margin:auto;
      }
  </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= btnSubmit.ClientID %>').click(function () {
                var state = $('#<%= ddlState.ClientID %>').val();
                var city = $('#<%= txtCity.ClientID %>').val();
                if(state==0){
                    window.alert("Select state from dropdouwn.!");
                    return false;
                }
                if(city==''){
                    window.alert("Enter the city name.!");
                    return false;
                }
                return true;
            })
        });
        $(document).ready(function () {
            $('#<%= txtCity.ClientID%>').keypress(function (e) {
                var uniCode = e.charChode ? e.charChode : e.keyCode;
                if ((uniCode == 8) || (uniCode == 9) || (uniCode > 64 && uniCode < 91) || (uniCode > 96 && uniCode < 123)) {
                    return true;
                }
                else {
                    window.alert("This field requires only character");
                    return false;
                }
            })
        });
        $(document).ready(function () {
            $('#<%= btnSearch.ClientID%>').click(function () {
                var state = $('#<%= ddlState.ClientID %>').val();
                var CityValue = $('#<%= txtCity.ClientID%>').val();
                if (state == 0) {
                    window.alert("Select state from dropdouwn.!");
                    return false;
                }
                if(CityValue==''){
                    window.alert("Enter the city name.!");
                    return false;
                }
                return true;
            })
        });
    </script>
    <div class="frmContainer">
        <table class="table">
            <tr>
                <td><asp:Label ID="lblState" runat="server" Text="Select State:" AssociatedControlID="ddlState"></asp:Label></td>
                <td><asp:DropDownList ID="ddlState" runat="server" Height="25px" Width="210px" AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblCity" runat="server" Text="Enter City Name:" AssociatedControlID="txtCity" ></asp:Label></td>
                <td><asp:TextBox ID="txtCity" runat="server" Height="25px" Width="200px" class="txtcity"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSubmit" runat="server" Text="Add City" Height="30px" Width="100px" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search City" Height="30px" Width="100px" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
            <br />
        <asp:GridView ID="cityGridview" runat="server" CssClass="gv" EmptyDataText="Sorry,no city found..!" AutoGenerateColumns="False" OnRowCancelingEdit="cityGridview_RowCancelingEdit" OnRowEditing="cityGridview_RowEditing" OnRowUpdating="cityGridview_RowUpdating" OnRowDeleting="cityGridview_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="City Name">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="counterHiddenField" Value='<%# Eval("COUNTER") %>' />
                        <asp:Label ID="lblCityName" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField runat="server" ID="counterHiddenField" Value='<%# Eval("COUNTER") %>' />
                        <asp:TextBox ID="txtEditCity" runat="server" Text='<%# Eval("CITY") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCityName" runat="server" ControlToValidate="txtEditCity" ErrorMessage="*" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Name">
                    <ItemTemplate>
                        <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("STATE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CREATEDDATE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnLinkUpdateCity" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="btnLinkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnLinkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        / <asp:LinkButton ID="btnLinkDelete" OnClientClick="return confirm('Are you sure to delete this city?');" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            </div>
</asp:Content>

