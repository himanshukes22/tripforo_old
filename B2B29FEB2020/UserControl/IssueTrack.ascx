<%@ Control Language="VB" AutoEventWireup="false" CodeFile="IssueTrack.ascx.vb" Inherits="UserControl_IssueTrack" %>
 <script type="text/javascript">
     function isNumberKey(event) {
         var charCode = (event.which) ? event.which : event.keyCode;
         if (charCode >= 48 && charCode <= 57 || charCode == 08 || charCode == 46) {
             return true;
         }
         else {
             // alert("please enter 0 to 9 only");
             return false;
         }
     }


     function isCharKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode;
         if (charCode >= 65 && charCode <= 90 || charCode >= 97 && charCode <= 122 || charCode == 32 || charCode == 08) {
             return true;
         }
         else {
             //alert("please enter Alphabets  only");
             return false;
         }
     }

     function getKeyCode(e) {
         if (window.event)
             return window.event.keyCode;
         else if (e)
             return e.which;
         else
             return null;
     }

     function keyRestrict(e, validchars) {
         var key = '', keychar = '';
         key = getKeyCode(e);
         if (key == null) return true;
         keychar = String.fromCharCode(key);
         keychar = keychar.toLowerCase();
         validchars = validchars.toLowerCase();
         if (validchars.indexOf(keychar) != -1)
             return true;
         if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
             return true;
         return false;
     }
    </script>
 <style>
        .text
        {
            padding: 5px;
            margin: 5px;
            font-size: 14px;
            width: 120px;
            font-weight: bold;
        }
        .txtbox
        {
            padding: 5px;
            border-radius: 5px;
            margin: 5px;
            background-color: #f1f1f1;
            font-size: 14px;
            width: 200px;
            border: thin solid #333;
        }
        /*.btn
        {
            border-radius: 5px;
            padding: 8px 15px 8px 15px;
            cursor: pointer;
            color: #fff;
            font-size: 14px;
            background-color: #18184B;
        }*/
        .mainbd
        {
            padding: 10px;
            border: thin solid #333;
            border-radius: 5px;
        }
        .heading
        {
            text-align: center;
            color: #f00;
        }
    </style>
 <script type="text/javascript">
     function Valid() {
         if (document.getElementById("ctl00_uc_Issue_UserID").value == "") {

             alert('Please Fill User ID');
             document.getElementById("ctl00_uc_Issue_UserID").focus();
             return false;
         }
         // change on 9 sep
         if (document.getElementById("ctl00_uc_Issue_txtmobileno").value == "") {

             alert('Please Fill Contact No.');
             document.getElementById("ctl00_uc_Issue_txtmobileno").focus();
             return false;
         }
         var emailidPat = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

         var emailid = document.getElementById("ctl00_uc_Issue_txtguestemail").value;
         var matchArray = emailid.match(emailidPat);
         if (document.getElementById("ctl00_uc_Issue_txtguestemail").value == "") {

             alert('Please Fill Your Emailid');
             document.getElementById("ctl00_uc_Issue_txtguestemail").focus();
             return false;
         }
         if (matchArray == null) {
             alert("Your emailid seems incorrect. Please try again.");
             document.getElementById("ctl00_uc_Issue_txtguestemail").focus();
             return false;
         }

         if (document.getElementById("ctl00_uc_Issue_txtFirstName").value == "") {

             alert('Please Fill First Name');
             document.getElementById("ctl00_uc_Issue_txtFirstName").focus();
             return false;
         }
         if (document.getElementById("ctl00_uc_Issue_txtLastName").value == "") {

             alert('Please Fill Last Name');
             document.getElementById("ctl00_uc_Issue_txtLastName").focus();
             return false;
         }



         if (document.getElementById("ctl00_uc_Issue_txtAgencyName").value == "") {

             alert('Please Fill Agency Name');
             document.getElementById("ctl00_uc_Issue_txtAgencyName").focus();
             return false;
         }
         if (document.getElementById("ctl00_uc_Issue_txtissue").value == "") {

             alert('Please Fill your Issue');
             document.getElementById("ctl00_uc_Issue_txtissue").focus();
             return false;
         }
     }
    </script>
<div id="editdiv" runat="server">
        <table style="width:96%; padding: 0 2%;">
            <tr>
                <td colspan="2" class="heading">
                    <h3>
                        Report Your Issue</h3>
                </td>
            </tr>
            <tr>
                <td class="text">
                    User ID
                </td>
                <td>
                    <input type="text" class="txtbox" runat="server" id="UserID" maxlength="100" style="width: 160px;
                        height: 20px" />
                </td>
            </tr>
            <tr>
                <td class="text">
                    Contact No.
                </td>
                <td>
                    <input type="text" class="txtbox" runat="server" id="txtmobileno" onkeypress="return isNumberKey(event)"
                        maxlength="10" style="width: 160px; height: 20px" />
                </td>
            </tr>
            <tr>
                <td class="text">
                    Email ID
                </td>
                <td>
                    <input type="text" class="txtbox" runat="server" id="txtguestemail" maxlength="100"
                        style="width: 160px; height: 20px" />
                </td>
            </tr>
            <tr>
                <td class="text">
                    First Name
                </td>
                <td>
                    <input runat="server" class="txtbox" id="txtFirstName" onkeypress="return isCharKey(event)"
                        maxlength="30" style="width: 160px; height: 20px" />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFirstName"
                        ID="RegularExpressionValidatorEF" ValidationExpression="^[a-zA-Z]{3,30}$" runat="server"
                        ErrorMessage="Miore than 2 characters required." ValidationGroup="A"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="text">
                    Last Name
                </td>
                <td>
                    <input runat="server" class="txtbox" id="txtLastName" onkeypress="return isCharKey(event)"
                        maxlength="30" style="width: 160px; height: 20px" />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtLastName"
                        ID="RegularExpressionValidatorEL" ValidationExpression="^[a-zA-Z]{3,30}$" runat="server"
                        ErrorMessage="More than 2 characters required." ValidationGroup="A"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="text">
                    Agency Name
                </td>
                <td>
                    <input runat="server" class="txtbox" id="txtAgencyName" maxlength="100" onkeypress="return isCharKey(event)"
                        style="width: 160px; height: 20px" />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAgencyName"
                        ID="RegularExpressionValidatorEFF" ValidationExpression="^[a-zA-Z ]{3,30}$" ValidationGroup="A"
                        runat="server" ErrorMessage="Agency Name  should be in Appropriate Length "></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="text">
                    Department
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Depart" runat="server" class="txtbox">
                        <asp:ListItem Value="AIR">AIR</asp:ListItem>
                        <asp:ListItem Value="HOTEL">HOTEL</asp:ListItem>
                        <asp:ListItem Value="BUS">BUS</asp:ListItem>
                        <asp:ListItem Value="RAIL">RAIL</asp:ListItem>
                        <asp:ListItem Value="ACCOUNT">ACCOUNT</asp:ListItem>
                        <asp:ListItem Value="HOLIDAYS">HOLIDAYS</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="text">
                    Issue
                </td>
                <td>
                    <asp:TextBox ID="txtissue" class="txtbox" runat="server" TextMode="MultiLine" Height="60px"
                        Width="250px" BackColor="#FFFFCC" MaxLength="500" Wrap="True"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp&nbsp &nbsp&nbsp
                    <asp:Button ID="b1" Text="SUBMIT" runat="server" OnClientClick="return Valid();" ValidationGroup="A"
                        OnClick="b1_Click" CssClass="btn" />
                </td>
                <td>
                    &nbsp&nbsp &nbsp&nbsp
                    <%--<asp:Button ID="btncancel" Text="CANCEL" runat="server" CssClass="btn"  />--%>
                    <input type="button"  name="CANCEL" id="CANCEL"  value="CANCEL" class="btn" />
                </td>
            </tr>
        </table>
    </div>
    
