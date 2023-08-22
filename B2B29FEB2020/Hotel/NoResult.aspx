<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterAfterLogin.master" CodeFile="NoResult.aspx.vb" Inherits="NoResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="font-size: 16px; text-align: center; font-weight: bold; color: #0b2b57;
        width: 100%; border: 1px solid #1c2b54;">
       
        <br>
              Sorry,Please Try Again
              <br>
              <br>
              
               <asp:Label ID="errormsg" runat="server"></asp:Label>
       <%-- <p>
            <img src="images/msg.png" />
        </p>--%>
    </div>
</asp:Content>
