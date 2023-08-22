Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.IO
Partial Class IntInvoiceDetails
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim sql As New SqlTransaction
    Dim adult As Double = 0
    Dim child As Double = 0
    Dim infant As Double = 0
    Dim totalfare As Double = 0
    Dim adulttax As Double = 0
    Dim childtax As Double = 0
    Dim infanttax As Double = 0
    Dim agentmrk As Double = 0
    Dim adminmrk As Double = 0
    Dim totalfaretax As Double = 0
    Dim total As Double = 0
    Dim GrandTotal As Double = 0
    Dim TotalST As Double = 0
    Dim TDS As Double = 0
    Dim CB As Double = 0
    Dim Dis As Double = 0
    Dim gdspnr As String = ""
    Private I As New Invoice()
    Dim ST As New SqlTransaction()
    Dim clsCorp As New ClsCorporate()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'lbl_IntInvoice.Text = clsCorp.ShowInvoice(Request.QueryString("OrderId").ToString())
                lbl_IntInvoice.Text = ShowInvoice(Request.QueryString("OrderId").ToString())
                'AgentAddress()
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
    End Sub
    Public Function ShowInvoice(ByVal orderId As String) As String

        Dim adult As Double = 0
        Dim child As Double = 0
        Dim infant As Double = 0
        Dim totalfare As Double = 0
        Dim adulttax As Double = 0
        Dim childtax As Double = 0
        Dim infanttax As Double = 0
        Dim agentmrk As Double = 0
        Dim adminmrk As Double = 0
        Dim totalfaretax As Double = 0
        Dim total As Double = 0
        Dim GrandTotal As Double = 0
        Dim TotalST As Double = 0
        Dim TDS As Double = 0
        Dim CB As Double = 0
        Dim Dis As Double = 0
        Dim gdspnr As String = ""

        Dim result As String = ""
        Try
            If (orderId <> "" AndAlso orderId IsNot Nothing) Then

                Dim id As String = orderId 'HttpContext.Current.Request.QueryString("OrderId").ToString()
                ds = sql.GetInvoice(id)
                dt = ds.Tables(0)
                Dim dtflt As New DataTable
                dtflt = ds.Tables(1)

                Dim projID As String = ""
                Dim bookedBy As String = ""
                Dim billNo As String = ""
                Dim ReissueId As String = ""

                If Not IsDBNull(dt.Rows(0)("ProjectID")) Then

                    projID = dt.Rows(0)("ProjectID").ToString()
                End If

                If Not IsDBNull(dt.Rows(0)("BookedBy")) Then

                    bookedBy = dt.Rows(0)("BookedBy").ToString()
                End If

                If Not IsDBNull(dt.Rows(0)("BillNoCorp")) Then

                    billNo = dt.Rows(0)("BillNoCorp").ToString()
                End If
                If Not IsDBNull(dt.Rows(0)("ResuId")) Then

                    ReissueId = dt.Rows(0)("ResuId").ToString()
                End If


                Dim mgtFee As Double = 0
                Dim dtAAdd As DataTable
                dtAAdd = ST.GetAgencyDetails(dt.Rows(0)("AgentId").ToString()).Tables(0)

                Dim MgtFeeVisibleStatus As Boolean = False
                Dim IsCorp As Boolean = False
                'IsCorp = True
                If Not IsDBNull(dtAAdd.Rows(0)("IsCorp")) Then
                    If Convert.ToBoolean(dtAAdd.Rows(0)("IsCorp")) Then
                        MgtFeeVisibleStatus = True
                        IsCorp = True

                        'mgtFee = If(IsDBNull(dt.Rows(0)("MgtFee")), 0, Convert.ToDouble(dt.Rows(0)("MgtFee")))


                    End If

                End If
                Dim my_table As String = ""

                Dim dtaddress As New DataTable
                Dim STDom As New SqlTransactionDom
                If (IsCorp = True) Then
                    dtaddress = STDom.GetCompanyAddress(ADDRESS.CORP.ToString().Trim()).Tables(0)
                    'my_table += "<td id='td_corp' runat='server'><table border='0' cellpadding='0' cellspacing='0' align='center'>"
                    'my_table += " <tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000'>"
                    'my_table += " " & dtaddress.Rows(0)("COMPANYNAME") & " </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    'my_table += " " & dtaddress.Rows(0)("COMPANYADDRESS") & " </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    'my_table += " &nbsp;Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "  </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    'my_table += "Email:          " & dtaddress.Rows(0)("EMAIL") & " </td></tr></table></td>"
                    my_table += "   <table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"

                    my_table += "<tr><td height='20'></td></tr>"

                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff' style='border-radius: 10px 10px 0 0;'>"
                    my_table += "<tr class='hiddenMobile'>"
                    my_table += "<td height='40'></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='30'></td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                    my_table += "<tbody>"
                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='left' class='col'>"
                    my_table += "<tbody>"
                    my_table += "<tr>"
                    my_table += "<td align='left'> <img src='http://www.supah.it/dribbble/017/logo.png' width='32' height='32' alt='logo' border='0' /></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='hiddenMobile'>"
                    my_table += "<td height='40'></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='20'></td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: left;'>Hello, Philip Brooks.<br> Thank you for shopping from our store and for your order.</td>"
                    my_table += "</tr>"
                    my_table += "</tbody>"
                    my_table += "</table>"
                    my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='right' class='col'>"
                    my_table += "<tbody>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='20'></td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td height='5'></td>"
                    my_table += "</tr>"


                Else
                    dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)
                    'my_table += " <td id='td_notcorp' runat='server'>"
                    'my_table += " <table cellpadding='0' cellspacing='0' width='900px' align='center'>"
                    'my_table += "<tr>    <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold;   color: #000000'>"
                    'my_table += "    " & dtaddress.Rows(0)("COMPANYNAME") & "    </td>    </tr>    <tr>    <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    'my_table += " " & dtaddress.Rows(0)("COMPANYADDRESS") & "     </td>     </tr>    <tr>  <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    'my_table += " &nbsp;Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "   </td>  </tr>  <tr> <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666'>"
                    'my_table += "   Email:          " & dtaddress.Rows(0)("EMAIL") & " </td> </tr></table></td>"



                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff' style='border-radius: 10px 10px 0 0;'>"
                    my_table += "<tr class='hiddenMobile'>"
                    my_table += "<td height='40'></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='30'></td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                    my_table += "<tbody>"
                    my_table += "<tr>"
                    my_table += "<td>"
                    my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='left' class='col'>"
                    my_table += "<tbody>"
                    my_table += "<tr>"
                    my_table += "<td align='left'> <img src='../../Advance_CSS/Icons/logo(ft).png' width='100' height='auto' alt='logo' border='0' /></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='hiddenMobile'>"
                    my_table += "<td height='40'></td>"
                    my_table += "</tr>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='20'></td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: left;'>" & dtaddress.Rows(0)("COMPANYNAME") & "<br> " & dtaddress.Rows(0)("COMPANYADDRESS") & "<br>Phone : " & dtaddress.Rows(0)("PHONENO") & "<br>Email : " & dtaddress.Rows(0)("EMAIL") & "</td>"
                    my_table += "</tr>"
                    my_table += "</tbody>"
                    my_table += "</table>"
                    my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='right' class='col'>"
                    my_table += "<tbody>"
                    my_table += "<tr class='visibleMobile'>"
                    my_table += "<td height='20'></td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td height='5'></td>"
                    my_table += "</tr>"



                End If


                'my_table += "  <tr> <td style='height: 30px'> </td> </tr> <tr>"
                'my_table += "   <td style='background-color:#ccc; padding-left:5px; font-size: 14px;' height='30px'>" & If(billNo = "", "<strong>Invoice No.&nbsp;:&nbsp;</strong>" & orderId, "<strong>BILL NO.&nbsp;:&nbsp;</strong>" & billNo)

                'my_table += " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                'my_table += If(projID <> "", "<strong>PROJECT ID&nbsp;:&nbsp;</strong>" & projID, "")

                'my_table += " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                'my_table += If(bookedBy <> "", "<strong>BOOKED BY&nbsp;:&nbsp;</strong>" & bookedBy, "") & " </td> </tr>"


                my_table += "<tr>"
                my_table += "<td style='font-size: 21px; color: #ff0000; letter-spacing: -1px; font-family: 'Open Sans', sans-serif; line-height: 1; vertical-align: top; text-align: right;'>Invoice</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<tr class='hiddenMobile'>"
                my_table += "<td height='50'></td>"
                my_table += "</tr>"
                my_table += "<tr class='visibleMobile'>"
                my_table += "<td height='20'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: right;'>"
                my_table += "<small></small>" & If(billNo = "", "<strong>Invoice No.&nbsp;:&nbsp;</strong>" & orderId, "<strong>BILL NO.&nbsp;:&nbsp;</strong>" & billNo)
                'my_table += "<td style='background-color:#ccc; padding-left:5px; font-size: 14px;' height='30px'>" & If(billNo = "", "<strong>Invoice No.&nbsp;:&nbsp;</strong>" & orderId, "<strong>BILL NO.&nbsp;:&nbsp;</strong>" & billNo)
                my_table += If(projID <> "", "<small>PROJECT ID&nbsp;:&nbsp;</small>" & projID, "")
                my_table += If(bookedBy <> "", "<small>BOOKED BY&nbsp;:&nbsp;</small>" & bookedBy, "") & " </td> </tr>"

                'my_table += "<small>MARCH 4TH 2016</small>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</table>"



                'my_table += " <tr><td>"
                'my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"




                'my_table += "<tr>"
                my_table += "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<tr class='hiddenMobile'>"
                my_table += "<td height='60'></td>"
                my_table += "</tr>"
                my_table += "<tr class='visibleMobile'>"
                my_table += "<td height='40'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='800' border='0' cellpadding='0' cellspacing='8' align='center' class='fullPadding'>"
                my_table += "<tbody>"



                If (IsCorp = True) Then

                    my_table += "<tr>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 10px 7px 0;' align='left'>CERATED DATE</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='left'><small>PAX NAME</small></th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>TICKET NO</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>Airline</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>SECTORS</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>DEP DATE</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>AIRLINE</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>FLIGHT NO</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>FARE</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>TAX</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>TOTAL</th>"
                    my_table += "</tr>"


                    'my_table += "<td  class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>CERATED DATE</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>PAX NAME</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TICKET NO</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>Airline</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>SECTORS</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>DEP DATE</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>AIRLINE</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>FLIGHT NO</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>FARE</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TAX</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TOTAL</td>"
                Else


                    my_table += "<tr>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 10px 7px 0;'  align='left'>Pax</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='left'><small>Ticket No.</small></th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Airline</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='right'>PNR</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>APNR</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Sectors</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Dep Date</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Create Date</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Fare</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Tax</th>"
                    my_table += "<th style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33; font-weight: normal; line-height: 1; vertical-align: top; padding: 0 0 7px;' align='center'>Total</th>"

                    my_table += "</tr>"


                    'my_table += "<td  class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Pax</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>Ticket No.</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>Airline</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>PNR</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>APNR</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Sectors</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Dep Date</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Create Date</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Fare</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Tax</td>"
                    'my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Total</td>"

                End If




                my_table += "<tr>"
                my_table += "<td height='1' style='background: #bebebe;' colspan='11'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td height='10' colspan='11'></td>"
                my_table += "</tr>"



                my_table += "<tr>"
                For Each dr As DataRow In dt.Rows
                    my_table += "<tr class='InvoiceText' align='center'>"
                    If (IsCorp = True) Then
                        If (ReissueId = "") Then
                            my_table += "<td>" & dr("Createdate").ToString() & " </td>"
                            my_table += "<td>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td>" & dr("TicketNumber").ToString() & "</td>"


                            my_table += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>"
                            my_table += "<img alt='' src='http://flywidus.co/AirLogo/sm" + dr("VC") + ".gif' ></img>"
                            my_table += dr("VC").ToString() & " " & dtflt.Rows(0)("FltNumber").ToString()

                            'TicketFormate += "<img alt='' src='" + ResolveUrl("~/AirLogo/sm" + FltDetailsList.Rows(f)("AirlineCode")) + ".gif' ></img>"
                            my_table += "</td>"



                            my_table += "<td>" & dr("Sector").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()


                            Dim strDepdt As String = Convert.ToString(dtflt.Rows(0)("DepDate"))
                            Try
                                strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                                Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                                strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")
                                Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                                strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                            End Try
                            my_table += "<td>" & strDepdt & "</td>"
                            'my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"

                            my_table += "<td>" & dr("VC").ToString() & "</td>"
                            my_table += "<td>" & dtflt.Rows(0)("FltNumber").ToString() & "</td>"
                            If dr("paxtype").ToString() = "ADT" Then
                                my_table += "<td>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                adult += Convert.ToDouble(dr("basefare").ToString())
                                adulttax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                            If dr("paxtype").ToString() = "CHD" Then
                                my_table += "<td>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                child += Convert.ToDouble(dr("basefare").ToString())
                                childtax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                            If dr("paxtype").ToString() = "INF" Then
                                my_table += "<td>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                infant += Convert.ToDouble(dr("basefare").ToString())
                                infanttax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                        Else
                            my_table += "<td>" & dr("Createdate").ToString() & " </td>"
                            my_table += "<td>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td>" & dr("TicketNumber").ToString() & "</td>"
                            my_table += "<td>" & dr("Sector").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()

                            Dim strDepdt As String = Convert.ToString(dtflt.Rows(0)("DepDate"))
                            Try
                                strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                                Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                                strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")
                                Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                                strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                            End Try
                            my_table += "<td>" & strDepdt & "</td>"
                            'my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"
                            my_table += "<td>" & dr("VC").ToString() & "</td>"
                            my_table += "<td>" & dtflt.Rows(0)("FltNumber").ToString() & "</td>"
                            If dr("paxtype").ToString() = "ADT" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & Convert.ToDouble(dr("ResuFareDiff").ToString()) & "</td>"
                                adult += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                adulttax += 0
                            End If
                            If dr("paxtype").ToString() = "CHD" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & Convert.ToDouble(dr("ResuFareDiff").ToString()) & "</td>"
                                child += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                childtax += 0
                            End If
                            If dr("paxtype").ToString() = "INF" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & Convert.ToDouble(dr("ResuFareDiff").ToString()) & "</td>"
                                infant += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                infanttax += 0
                            End If
                        End If


                    Else


                        If (ReissueId = "") Then


                            my_table += "<tr>"

                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #ff0000;  line-height: 18px;  vertical-align: top; padding:10px 0;' class='article'>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;'><small>" & dr("TicketNumber").ToString() & ".</small></td>"



                            ''my_table += "<td>" & dr("VC").ToString() & "</td>"
                            ''my_table += "<td>" & dtflt.Rows(0)("FltNumber").ToString() & "</td>"

                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='center'>"


                            'my_table += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>"
                            'my_table += "<img alt='' src='http://flywidus.co/AirLogo/sm" + dr("VC") + ".gif' ></img>"
                            my_table += dr("VC").ToString() & " " & dtflt.Rows(0)("FltNumber").ToString()


                            my_table += "</td>"



                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("GDSPnr").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("AirlinePnr").ToString() & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("Sector").ToString() & "</td>"
                            Dim strDepdt As String = Convert.ToString(dtflt.Rows(0)("DepDate"))
                            Try
                                strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                                Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                                strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")
                                Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                                strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                            End Try
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & strDepdt & "</td>"
                            'my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("Createdate").ToString() & " </td>"
                            If dr("paxtype").ToString() = "ADT" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                adult += Convert.ToDouble(dr("basefare").ToString())
                                adulttax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                            If dr("paxtype").ToString() = "CHD" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                child += Convert.ToDouble(dr("basefare").ToString())
                                childtax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                            If dr("paxtype").ToString() = "INF" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("BaseFare").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("TotalTax").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & Convert.ToDouble(dr("BaseFare").ToString()) + Convert.ToDouble(dr("TotalTax").ToString()) & " </td>"
                                infant += Convert.ToDouble(dr("basefare").ToString())
                                infanttax += Convert.ToDouble(dr("totaltax").ToString())
                            End If
                        Else





                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #ff0000;  line-height: 18px;  vertical-align: top; padding:10px 0;' class='article'>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;'><small>" & dr("TicketNumber").ToString() & ".</small></td>"

                            ''my_table += "<td>" & dr("VC").ToString() & "</td>"
                            ''my_table += "<td>" & dtflt.Rows(0)("FltNumber").ToString() & "</td>"

                            'my_table += "<td style='font-size: 11px; width: 10%; text-align: left; font-weight: bold; vertical-align: top;'>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='center'>"
                            'my_table += "<img alt='' src='https://richatravels.in/AirLogo/sm" + dr("VC") + ".gif' ></img>"
                            my_table += dr("VC").ToString() & " " & dtflt.Rows(0)("FltNumber").ToString()

                            my_table += "</td>"




                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("GDSPnr").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("AirlinePnr").ToString() & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("Sector").ToString() & "</td>"
                            Dim strDepdt As String = Convert.ToString(dtflt.Rows(0)("DepDate"))
                            Try
                                strDepdt = IIf(strDepdt.Length = 8, STD.BAL.Utility.Left(strDepdt, 4) & "-" & STD.BAL.Utility.Mid(strDepdt, 4, 2) & "-" & STD.BAL.Utility.Right(strDepdt, 2), "20" & STD.BAL.Utility.Right(strDepdt, 2) & "-" & STD.BAL.Utility.Mid(strDepdt, 2, 2) & "-" & STD.BAL.Utility.Left(strDepdt, 2))
                                Dim deptdt As DateTime = Convert.ToDateTime(strDepdt)
                                strDepdt = deptdt.ToString("dd/MMM/yy").Replace("-", "/")
                                Dim depDay As String = Convert.ToString(deptdt.DayOfWeek)
                                strDepdt = strDepdt.Split("/")(0) + " " + strDepdt.Split("/")(1) + " " + strDepdt.Split("/")(2)
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                            End Try
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & strDepdt & "</td>"
                            'my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"
                            my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("Createdate").ToString() & " </td>"
                            If dr("paxtype").ToString() = "ADT" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>0</td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & "</td>"
                                adult += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                adulttax += 0
                            End If
                            If dr("paxtype").ToString() = "CHD" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>0</td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & "</td>"
                                child += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                childtax += 0
                            End If
                            If dr("paxtype").ToString() = "INF" Then
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>0</td>"
                                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'>" & dr("ResuFareDiff").ToString() & "</td>"
                                infant += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                infanttax += 0
                            End If
                        End If
                    End If







                    'my_table += "</tr>"
                    If (ReissueId = "") Then
                        total = (Convert.ToDouble(dr("basefare").ToString()) + Convert.ToDouble(dr("totaltax").ToString()))

                    Else
                        total = (Convert.ToDouble(dr("ResuFareDiff").ToString()))

                    End If


                Next

                totalfare = adult + child + infant
                totalfaretax = adulttax + childtax + infanttax
                total = totalfare + totalfaretax


                'my_table += "<tr   style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'my_table += "<td></td>"
                'If (IsCorp = True) Then
                '    my_table += "<td></td>"
                'End If










                my_table += "<tr>"
                my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #ff0000;  line-height: 18px;  vertical-align: top; padding:10px 0;' class='article'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;'><small></small></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='center'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'><strong>Total</strong></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'><strong>" & totalfare & "</strong></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'><strong>" & totalfaretax & "</strong></td>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #1e2b33;  line-height: 18px;  vertical-align: top; padding:10px 0;' align='right'><strong>" & total & "</strong></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += " </tr>"


                my_table += "<tr>"
                my_table += "<td height='20'></td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"



                my_table += "</tbody>"
                my_table += "</table>"



                'my_table += "</tr>"

                my_table += "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td style='position: relative;right: 10%;'>"


                Dim srvTax As Double = 0, tf As Double = 0, admMrk As Double = 0, agMrk As Double = 0, totDis As Double = 0
                Dim creditBal As Double = 0, debitBal As Double = 0
                For j As Integer = 0 To dt.Rows.Count - 1
                    srvTax = srvTax + dt.Rows(j)("ServiceTax")
                    tf = tf + dt.Rows(j)("TranFee")
                    admMrk = admMrk + dt.Rows(j)("adminmrk")
                    agMrk = agMrk + dt.Rows(j)("AgentMrk")
                    totDis = totDis + dt.Rows(j)("TotalDiscount")
                    TDS = TDS + dt.Rows(j)("Tds")
                    CB = CB + dt.Rows(j)("CashBack")



                    If Not IsDBNull(dtAAdd.Rows(0)("IsCorp")) Then
                        If Convert.ToBoolean(dtAAdd.Rows(0)("IsCorp")) Then
                            'MgtFeeVisibleStatus = True
                            mgtFee = mgtFee + If(IsDBNull(dt.Rows(j)("MgtFee")), 0, Convert.ToDouble(dt.Rows(j)("MgtFee")))
                        End If
                    End If
                Next
                If (ReissueId = "") Then
                    my_table += "<table width='300' border='0' cellpadding='5' cellspacing='3' align='right' class='fullPadding'>"
                    my_table += "<tbody>"

                    If (IsCorp = True) Then

                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Management Fee</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; white-space:nowrap;' width='80'>" & mgtFee.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Service Tax</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & srvTax.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        TotalST = total + srvTax ' + admMrk '+ tf + agentmrk

                    Else



                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Service Tax</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & srvTax.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Transaction Fee</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>0</td>" ''tf.ToString
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"

                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Transaction Charge</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & admMrk.ToString & "</td>"

                        TotalST = total + srvTax + admMrk '+ tf + agentmrk
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"

                        my_table += "<tr>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #000; line-height: 22px; vertical-align: top; text-align:right; '><strong>Total(Inc. S.Tax & T.F.)</strong></td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #000; line-height: 22px; vertical-align: top; text-align:right; '><strong>" & TotalST & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"



                        'newly added

                        'If Not IsDBNull(dt.Rows(0)("CreditNode")) Then
                        '    creditBal = dt.Rows(0)("CreditNode")
                        'End If
                        'If Not IsDBNull(dt.Rows(0)("DebitNode")) Then
                        '    debitBal = dt.Rows(0)("DebitNode")
                        'End If

                        'my_table += "<tr class='TransInvoice'>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td colspan='3'>&nbsp;Credit Node</td>"
                        'my_table += "<td align='center'>" & creditBal & "</td>"

                        'my_table += "<tr class='TransInvoice'>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td></td>"
                        'my_table += "<td colspan='3'>&nbsp;Debit Node</td>"
                        'my_table += "<td align='center'>" & debitBal & "</td>"


                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Less Discount</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & (totDis - (CB + tf)).ToString() & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Less Cash Back</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & CB.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice' >"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Add TDS</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & TDS.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                    End If


                Else

                    If (IsCorp = True) Then
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Reissue Charge</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge")) & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '></td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '></td>" ''tf.ToString
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        TotalST = total + Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge")) ' + admMrk '+ tf + agentmrk

                    Else
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Reissue Charge</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & dt.Rows(0)("ResuCharge").ToString() & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>Service Charge</td>"
                        my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #646a6e; line-height: 22px; vertical-align: top; text-align:right; '>" & dt.Rows(0)("ResuServiseCharge").ToString() & "</td>" ''tf.ToString
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td height='1' colspan='11' style='border-bottom:1px solid #e4e4e4'></td>"
                        my_table += "</tr>"
                        TotalST = total + Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge"))
                    End If

                End If






                If (ReissueId = "") Then
                    'GrandTotal = (TotalST + TDS + mgtFee) - (totDis - tf) + debitBal - creditBal
                    GrandTotal = (TotalST + TDS + mgtFee) - (totDis - tf)

                Else
                    GrandTotal = TotalST

                End If

                'Dim GTInWord As New NumToWord.NumberToWord()
                'my_table += "<tr class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'>"
                'my_table += "<td align='center'>Amount in word</td>"
                'my_table += "<td colspan='6'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(GrandTotal)) & "</td>"




                If (IsCorp = True) Then
                    my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #000; line-height: 22px; vertical-align: top; text-align:right; '><strong>Grand Total</strong></td>"
                Else
                    my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #000; line-height: 22px; vertical-align: top; text-align:right; '><strong>Grand Total</strong></td>"
                End If

                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #000; line-height: 22px; vertical-align: top; text-align:right; '><strong>" & GrandTotal & "</strong></td>"

                my_table += "</tbody>"
                my_table += "</table>"

                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                'my_table += "</tr>"

                'my_table += "</table></td></tr>"



                'Agent Address--------------------------------------------------------------------------------------
                Dim dtAgent As DataTable = sql.GetAgencyDetails(dt.Rows(0)("AgentId").ToString()).Tables(0)


                'my_table += "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"
                'my_table += "<tbody>"
                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff'>"
                'my_table += "<tbody>"
                'my_table += "<tr>"
                'my_table += "<tr class='hiddenMobile'>"
                'my_table += "<td height='60'></td>"
                'my_table += "</tr>"
                'my_table += "<tr class='visibleMobile'>"
                'my_table += "<td height='40'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                'my_table += "<tbody>"
                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='left' class='col'>"

                'my_table += "<tbody>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 11px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 1; vertical-align: top; '>"
                'my_table += "<strong>" & dtAgent.Rows(0)("Agency_Name").ToString() & "</td></tr> <tr><td >" & dtAgent.Rows(0)("Address").ToString() & "</strong>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td width='100%' height='10'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 20px; vertical-align: top; '>" & dtAgent.Rows(0)("City").ToString & "," & dtAgent.Rows(0)("Zipcode").ToString & "," & dtAgent.Rows(0)("State").ToString & "," & dtAgent.Rows(0)("Country").ToString() & "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"



                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"
                'my_table += "</td>"
                'my_table += "</tr>"


                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                'my_table += "<tbody>"
                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='left' class='col'>"
                'my_table += "<tbody>"
                'my_table += "<tr class='hiddenMobile'>"
                'my_table += "<td height='35'></td>"
                'my_table += "</tr>"
                'my_table += "<tr class='visibleMobile'>"
                'my_table += "<td height='20'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 11px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 1; vertical-align: top;'>"
                'my_table += "<strong></strong>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td width='100%' height='10'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 20px; vertical-align: top;'>"

                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"


                'my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='right' class='col'>"
                'my_table += "<tbody>"
                'my_table += "<tr class='hiddenMobile'>"
                'my_table += "<td height='35'></td>"
                'my_table += "</tr>"
                'my_table += "<tr class='visibleMobile'>"
                'my_table += "<td height='20'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 11px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 1; vertical-align: top;'>"
                'my_table += "<strong></strong>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td width='100%' height='10'></td>"
                'my_table += "</tr>"
                'my_table += "<tr>"
                'my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 20px; vertical-align: top; '>"

                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"
                'my_table += "</td>"
                'my_table += "</tr>"

                'my_table += "<tr>"
                'my_table += "<td>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "<tr class='hiddenMobile'>"
                'my_table += "<td height='60'></td>"
                'my_table += "</tr>"
                'my_table += "<tr class='visibleMobile'>"
                'my_table += "<td height='30'></td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"
                'my_table += "</td>"
                'my_table += "</tr>"
                'my_table += "</tbody>"
                'my_table += "</table>"




                my_table += "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "</tr><tr class='hiddenMobile'>"
                my_table += "<td height='60'></td>"
                my_table += "</tr>"
                my_table += "<tr class='visibleMobile'>"
                my_table += "<td height='40'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='220' border='0' cellpadding='0' cellspacing='0' align='left' class='col'>"

                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td style='font-size: 11px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 1; vertical-align: top;'>"
                my_table += "<strong>" & dtAgent.Rows(0)("Agency_Name").ToString() & "</strong>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td width='100%' height='10'></td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td style='font-size: 12px; font-family: 'Open Sans', sans-serif; color: #5b5b5b; line-height: 20px; vertical-align: top;'>"
                my_table += "" & dtAgent.Rows(0)("Address").ToString() & "<br> " & dtAgent.Rows(0)("City").ToString & ", " & dtAgent.Rows(0)("Zipcode").ToString & ",<br> " & dtAgent.Rows(0)("State").ToString & ",<br>  " & dtAgent.Rows(0)("Country").ToString() & ""
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"



                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='480' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td>"




                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr class='hiddenMobile'>"
                my_table += "<td height='60'></td>"
                my_table += "</tr>"
                my_table += "<tr class='visibleMobile'>"
                my_table += "<td height='30'></td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"






                my_table += "<table width='100%' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#e1e1e1'>"

                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='1000' border='0' cellpadding='0' cellspacing='0' align='center' class='fullTable' bgcolor='#ffffff' style='border-radius: 0 0 10px 10px;'>"
                my_table += "<tr>"
                my_table += "<td>"
                my_table += "<table width='800' border='0' cellpadding='0' cellspacing='0' align='center' class='fullPadding'>"
                my_table += "<tbody>"
                my_table += "<tr>"
                my_table += "<td style='font-size: 12px; color: #5b5b5b; font-family: 'Open Sans', sans-serif; line-height: 18px; vertical-align: top; text-align: left;'>This is Computer generated invoice,hence no signature required.</td>"
                my_table += "</tr>"
                my_table += "</tbody>"
                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr class='spacer'>"
                my_table += "<td height='50'></td>"
                my_table += "</tr>"

                my_table += "</table>"
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "<tr>"
                my_table += "<td height='20'></td>"
                my_table += "</tr>"
                my_table += "</table>"

                'my_table += "<tr> <td height='90px' style='border: thin solid #999999'> <table border='0' cellpadding='0' cellspacing='0' width='100%'>"
                'my_table += "  <tr> <td rowspan='4' width='200px' class='SubHeading'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address =&gt;</td>"
                'my_table += "  <td >" & dtAgent.Rows(0)("Agency_Name").ToString() & "</td></tr> <tr><td >" & dtAgent.Rows(0)("Address").ToString() & "</td></tr>"
                'my_table += "  <tr><td > <table border='0' cellpadding='0' cellspacing='0' width='100%'><tr> <td >" & dtAgent.Rows(0)("City").ToString & "," & dtAgent.Rows(0)("Zipcode").ToString & "," & dtAgent.Rows(0)("State").ToString & " </td>"
                'my_table += "   </tr> </table> </td></tr> <tr> <td>" & dtAgent.Rows(0)("Country").ToString() & "</td></tr> </table>"
                'my_table += " </td></tr>   <tr>  <td></td> </tr>"

                '-------------------------------------------------------------


                'my_table += "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>"
                'my_table += "    <tr>  <td>  E &amp; O.E : Payment to be made to the cashier and print Official Receipt must to be Obtained. </td>"
                'my_table += "  </tr>  <tr> <td class='MsgText'> CASH & CHEQUE : All Cheques/Demand Drafts in Payment of bills must be crossed 'A/c"
                'my_table += "  Payee Only and all drawn in favour of ''. </td>  </tr> <tr>"
                'my_table += " <td class='MsgText'> LATE PAYMENT : If bill is not paid within 15 days,Interest @2 4% will be charged."
                'my_table += " </td> </tr>  <tr> <td class='MsgText'>  DISPUTES :All dispute will be subject to Delhi Jurisdiction."
                'my_table += " </td>  </tr>  <tr> <td class='MsgText'>  SERVICE TAX NO :DL1/ST/DELHI/43//97 </td></tr>"
                'my_table += "   <tr>  <td class='MsgText'>  <h3>This is Computer generated invoice,hence no signature required. </h3> </td>  </tr>"

                'my_table += "</table>"

                result = my_table
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return result
    End Function

    Protected Sub btn_PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_PDF.Click
        Dim filename As String = ""
        filename = "PackageReport.pdf"
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
        Response.Charset = ""
        Response.ContentType = "application/pdf"
        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        div_invoice.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.[End]()
    End Sub

    Protected Sub btn_Word_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Word.Click
        Try
            Dim filename As String = ""
            filename = "InvoiceDetail.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            div_invoice.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_Excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Excel.Click
        Try
            Dim filename As String = ""
            filename = "InvoiceDetail.xls"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            div_invoice.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        Try
            Dim sw As New StringWriter()
            Dim w As New HtmlTextWriter(sw)
            div_invoice.RenderControl(w)
            Dim s As String = sw.GetStringBuilder().ToString()
            Dim MailDt As New DataTable
            Dim STDOM As New SqlTransactionDom
            MailDt = STDOM.GetMailingDetails(MAILING.AIR_INVOICE.ToString(), Session("UID").ToString()).Tables(0)
            Dim email As String = Request("txt_email")
            If (MailDt.Rows.Count > 0) Then
                Dim Status As Boolean = False
                Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                Try
                    If Status = True Then
                        Dim i As Integer = STDOM.SendMail(txt_email.Text, MailDt.Rows(0)("MAILFROM").ToString(), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), s, MailDt.Rows(0)("SUBJECT").ToString(), "")
                        If i = 1 Then
                            mailmsg.Text = "Mail sent successfully."
                        Else
                            mailmsg.Text = "Unable to send mail.Please try again"
                        End If
                    End If
                    txt_email.Text = ""
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                    mailmsg.Text = ex.Message.ToString
                End Try
            Else
                mailmsg.Text = "Unable to send mail.Please contact to administrator"
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    'Public Sub AgentAddress()

    '    Dim dt As New DataTable()
    '    'lblPNR.Text = Request.QueryString("PNRNO")
    '    Try
    '        dt = sql.GetAgencyDetails(Request("AgentID")).Tables(0)
    '        td_AgName.InnerText = dt.Rows(0)("Agency_Name").ToString()
    '        td_Address.InnerText = dt.Rows(0)("Address").ToString()
    '        td_Add1.InnerText = dt.Rows(0)("City").ToString & "," & dt.Rows(0)("Zipcode").ToString & "," & dt.Rows(0)("State").ToString
    '        'td_zip.InnerText = dt.Rows[0]["zipcode"].ToString();
    '        'td_state.InnerText = dt.Rows[0]["State"].ToString();
    '        td_country.InnerText = dt.Rows(0)("Country").ToString()

    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try



    'End Sub
End Class
