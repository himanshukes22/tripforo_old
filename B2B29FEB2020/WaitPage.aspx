<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Please Wait...</title>
    <link href="css/main2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

         <div    >
        <div  class="wait" style="text-align: center; z-index: 1001; background-color: #f9f9f9; font-size: 12px; font-weight: bold; padding: 20px; box-shadow: 0px 1px 5px #000; border: 1px solid #d1d1d1; border-radius: 0px;">
            <h1 class="text-center" style="font-size: 20px;">PLEASE WAIT</h1>
            <span>Booking Is Under Process.Please Wait...</span><br />
             <img src='<%=ResolveUrl("~/images/loadingAnim.gif")%>' alt="" /><br />
            <br />
            <div id="searchquery" style="color: #004b91; padding-top: 15px">
            </div>
        </div>
    </div>


     
    <%If Session("search_type").ToString = "Adv" Then%>

    <script type="text/javascript">
        window.location.href = 'Adv_Search/Book_con.aspx';
    </script>

    <%  ElseIf Session("search_type").ToString = "Flt" Then%>

    <script type="text/javascript">
        window.location.href = 'International/Booking.aspx?OBTID=<%=Request("OBTID")%>&IBTID=<%=Request("IBTID") %>&FT=<%=Request("FT") %>';
    </script>
    <%  ElseIf Session("search_type").ToString = "RTF" Then%>

    <script type="text/javascript">
        window.location.href = 'LccRF/LccRTFBooking.aspx?OBTID=<%=Request("OBTID")%>&IBTID=<%=Request("IBTID") %>&FT=<%=Request("FT") %>';
    </script>
  <%  ElseIf Session("search_type").ToString = "FltInt" Then%>

    <script type="text/javascript">
        window.location.href = 'International/Booking.aspx?OBTID=<%=Request("OBTID")%>&FT=<%=Request("FT") %>';

    </script>
    <%  Else%>
    <%  End If%>
    </form>
</body>
</html>

