<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage_Test.master" AutoEventWireup="false" EnableEventValidation="false"
    CodeFile="Login.aspx.vb" Inherits="Login" %>

<%@ Register Src="~/UserControl/LoginControl.ascx" TagPrefix="UC1" TagName="Login" %>
<%@ Register Src="~/UserControl/IssueTrack.ascx" TagName="Holiday" TagPrefix="ucholiday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<UC1:Login ID="uc" runat="server"/>
        

         
  <style type="text/css">
      table {
          width: 100% !important;
      }
  </style>
    

    
    <div class="toPopup1" style="display: none;">
        <div style="width: 32%; padding: 1%; margin: 50px auto 0; background: #f9f9f9;">
            <div style="font-weight: bold;">
                Timings 10 AM to 6 PM, Monday to Saturday. Issues Reported after 6 PM and on Sunday will be addressed on the following day.
            </div>
            <div>
                <ucholiday:Holiday ID="uc_holiday" runat="server" />
            </div>
        </div>
    </div>
    

    <script>
        localStorage.removeItem("isnotification");
    </script>
</asp:Content>
