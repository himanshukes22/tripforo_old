<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAfterLogin.master" AutoEventWireup="true"
    CodeFile="BusSearch.aspx.cs" Inherits="BS_BusSearch" %>
<%@ Register Src="~/UserControl/LeftMenu.ascx" TagPrefix="uc1" TagName="LeftMenu"  %>
<%@ Register Src="~/BS/UserControl/BusSearch.ascx" TagName="BusSearch" TagPrefix="UC1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <link href="<%= ResolveUrl("~/CSS/style.css") %>" rel="stylesheet" type="text/css" />
    <div align="center">
        <UC1:BusSearch ID="search" runat="server">
        </UC1:BusSearch>
    </div>
</asp:Content>
