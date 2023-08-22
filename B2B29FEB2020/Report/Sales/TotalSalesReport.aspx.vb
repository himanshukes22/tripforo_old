Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Partial Class SprReports_Sales_TotalSalesReport
    Inherits System.Web.UI.Page
    Private con As New SqlConnection()
    Private con1 As New SqlConnection()
    Private adp As SqlDataAdapter
    Private STDom As New SqlTransactionDom
    Protected Sub SprReports_Sales_TotalSalesReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                'Response.Redirect("~/Login.aspx")
            End If
            If (RBL_RTYPE.SelectedValue = "SALES") Then
                td_SalesID.Visible = True
                td_GroupType.Visible = False
                btn_totsale.Visible = True
                btn_Type.Visible = False

            Else
                td_SalesID.Visible = False
                td_GroupType.Visible = True
                btn_totsale.Visible = False
                btn_Type.Visible = True
            End If

            If (Session("User_Type") = "SALES") Then
                SaleReport(Session("UID"), Session("User_Type").ToString(), "", "", "")
                btn_totsale.Visible = False
                'RB_OneWay.Visible = False
                'RB_RoundTrip.Visible = False
                'Else
                '    td_salesref.Visible = True
                'RB_OneWay.Visible = True
                'RB_RoundTrip.Visible = True
            End If
            If (Session("TypeID").ToString() = "AD2") Then
                btn_mail.Visible = True

            End If
            If Not IsPostBack Then

                Dim dttype As New DataTable
                dttype = STDom.GetAgentType().Tables(0)
                If dttype.Rows.Count > 0 Then
                    ddl_type.AppendDataBoundItems = True
                    ddl_type.Items.Clear()
                    ddl_type.Items.Insert(0, "--Select Type--")
                    ddl_type.DataSource = dttype
                    ddl_type.DataTextField = "GroupType"
                    ddl_type.DataValueField = "GroupType"
                    ddl_type.DataBind()
                End If

                Try
                    Dim dtsales As DataTable
                    dtsales = STDom.GetSalesRef().Tables(0)
                    Sales_DDL.AppendDataBoundItems = True
                    Sales_DDL.Items.Clear()
                    Sales_DDL.Items.Insert(0, "Select Sales Ref.")
                    Sales_DDL.DataSource = dtsales
                    Sales_DDL.DataTextField = "EmailId"
                    Sales_DDL.DataValueField = "EmailId"

                    Sales_DDL.DataBind()


                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try


            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub SaleReport(ByVal Id As String, ByVal Utype As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AgentID As String)
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim ds_cur As New DataSet



            adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@LOGINID", Id)
            adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", Utype)
            adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
            adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
            adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
            adp.Fill(ds_cur)

            Dim dtsalesid As New DataTable
            Dim dtticketrpt As New DataTable
            Dim dtrailrpt As New DataTable
            Dim dthtlrpt As New DataTable
            Dim dtrechargerpt As New DataTable
            Dim dteblrpt As New DataTable
            Dim dtbusrpt As New DataTable
            dtsalesid = ds_cur.Tables(0)
            dtticketrpt = ds_cur.Tables(1)
            dtrailrpt = ds_cur.Tables(2)
            dthtlrpt = ds_cur.Tables(3)
            dtrechargerpt = ds_cur.Tables(4)
            dteblrpt = ds_cur.Tables(5)
            dtbusrpt = ds_cur.Tables(6)

            Dim my_table As String = ""
            my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"
            my_table += "<tr style='background-color:#004b91;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'  >"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Agent ID</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Agency Name</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>MOBILE NO</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Ticket Count</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Sale Air</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Rail Booked</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Rail Sale</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Hotel Booked</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Hotel Sale</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Recharge No</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Recharge Sale</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Bill No</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Bill Sale</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Bus Ticket</td>"
            my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px'>Bus Sale</td>"
            my_table += "</tr>"
            If (dtsalesid.Rows.Count > 0) Then
                Dim SumTicket As Integer = 0
                Dim SumTotalTicketSale As Integer = 0
                Dim SumRailBooked As Integer = 0
                Dim SumTotalRailSale As Integer = 0
                Dim SumHotelBooked As Integer = 0
                Dim SumTotalHotelSale As Integer = 0
                Dim SumRecharge As Integer = 0
                Dim SumTotalRechargeSale As Integer = 0
                Dim SumBill As Integer = 0
                Dim SumTotalBillSale As Integer = 0
                Dim SumBus As Integer = 0
                Dim SumTotalBusSale As Integer = 0
                If (dtticketrpt.Rows.Count > 0) Then
                    SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                    SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                End If
                If (dtrailrpt.Rows.Count > 0) Then
                    SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                    SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                End If
                If (dthtlrpt.Rows.Count > 0) Then
                    SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                    SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                End If
                If (dtrechargerpt.Rows.Count > 0) Then
                    SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                    SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                End If
                If (dteblrpt.Rows.Count > 0) Then
                    SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                    SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                End If
                If (dtbusrpt.Rows.Count > 0) Then
                    SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                    SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                End If
                For Each drsales As DataRow In dtsalesid.Rows
                    Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim busarray As Array = dtbusrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                    Dim TotalTkt As Integer
                    Dim TktNetfare As Integer
                    TotalTkt = 0
                    TktNetfare = 0
                    If (ticketarray.Length > 0) Then
                        TotalTkt = ticketarray(0)("TOTALTICKET").ToString()
                        TktNetfare = ticketarray(0)("NETFARE").ToString()
                    End If
                    Dim TotalRailTkt As Integer
                    Dim TotalRailSale As Integer
                    TotalRailTkt = 0
                    TotalRailSale = 0
                    If (railarray.Length > 0) Then
                        TotalRailTkt = railarray(0)("TOTALRAILTICKET").ToString()
                        TotalRailSale = railarray(0)("RAILTOTALFARE").ToString()
                    End If

                    Dim TotalHotelBooked As Integer
                    Dim TotalHotelSale As Integer
                    TotalHotelBooked = 0
                    TotalHotelSale = 0
                    If (hotelarray.Length > 0) Then
                        TotalHotelBooked = hotelarray(0)("TOTALHOTELTICKET").ToString()
                        TotalHotelSale = hotelarray(0)("HOTELTOTALSALE").ToString()
                    End If

                    Dim TotalRechargeNo As Integer
                    Dim TotalRechargeSale As Integer
                    TotalRechargeNo = 0
                    TotalRechargeSale = 0
                    If (rechargearray.Length > 0) Then
                        TotalRechargeNo = rechargearray(0)("TOTALRECHARGE").ToString()
                        TotalRechargeSale = rechargearray(0)("RECHARGETOTALSALE").ToString()
                    End If

                    Dim TotalEblNo As Integer
                    Dim TotalEblSale As Integer
                    TotalEblNo = 0
                    TotalEblSale = 0
                    If (eblarray.Length > 0) Then
                        TotalEblNo = eblarray(0)("TOTALBILL").ToString()
                        TotalEblSale = eblarray(0)("EBLETOTALSALE").ToString()
                    End If

                    Dim TotalBusNo As Integer
                    Dim TotalBusSale As Integer
                    TotalBusNo = 0
                    TotalBusSale = 0
                    If (busarray.Length > 0) Then
                        TotalBusNo = busarray(0)("TOTALBUS").ToString()
                        TotalBusSale = busarray(0)("BUSTOTALSALE").ToString()
                    End If


                    my_table += "<tr >"
                    my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>" & drsales("AGENTID") & "</td>"
                    my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>" & drsales("AGENCYNAME") & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & drsales("MOBILE") & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalTkt & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TktNetfare & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalRailTkt & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalRailSale & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalHotelBooked & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalHotelSale & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalRechargeNo & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalRechargeSale & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalEblNo & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalEblSale & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalBusNo & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & TotalBusSale & "</td>"
                    my_table += "</tr>"
                Next
                my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;' colspan='3'>TOTAL</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBus & "</td>"
                my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBusSale & "</td>"
                my_table += "</tr>"
                my_table += "</table>"
                lblsale.Text = my_table

            Else
                lblsale.Text = ""
            End If

        Catch ex As Exception

        End Try


    End Sub
    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        lblmail.Text = ""
        lbl_totsale.Text = ""


        Try
            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            If (Session("User_Type") = "SALES") Then
                SaleReport(Session("UID"), Session("User_Type").ToString(), FromDate, ToDate, AgentID)
            End If
            If (Session("User_Type") = "ADMIN") Then
                If (Sales_DDL.SelectedIndex = 0) Then
                    btn_resultexportexcel.Visible = False
                    btn_resultexportword.Visible = False

                    btn_totsaleexportexcel.Visible = False
                    btn_totsaleexportword.Visible = False
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Select Sales Ref');", True)

                Else
                    btn_resultexportexcel.Visible = True
                    btn_resultexportword.Visible = True

                    btn_totsaleexportexcel.Visible = False
                    btn_totsaleexportword.Visible = False
                    SaleReport(Sales_DDL.SelectedValue, Session("User_Type").ToString(), FromDate, ToDate, AgentID)
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub btn_mail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_mail.Click
        Try
            lblsale.Text = ""
            lbl_totsale.Text = ""
            btn_totsaleexportexcel.Visible = False
            btn_totsaleexportword.Visible = False
            btn_resultexportexcel.Visible = False
            btn_resultexportword.Visible = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString


            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))



            Dim dtsalesmail As DataTable
            dtsalesmail = STDom.GetSalesRef().Tables(0)
            Dim my_table As String = ""
            Dim my_table_final As String = ""

            Dim cnt As Integer = 0
            If (Sales_DDL.SelectedIndex > 0) Then
                'For Each drsalesmail As DataRow In dtsalesmail.Rows


                my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"

                If (cnt = 0) Then


                    my_table += "<tr>"
                    my_table += "<td align='left' colspan='11' style='font-weight: bold;'>Dear Sir,</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td align='left' colspan='11'>Please find today's sales report mention below.  </td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td style='height:20px' colspan='11'></td>"
                    my_table += "</tr>"
                    cnt = cnt + 1
                End If


                my_table += "<tr>"
                my_table += "<td align='left'>"
                my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                my_table += "<tr>"
                my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [" & Sales_DDL.SelectedValue & "] </td>" '(" & drsalesmail("EmailId") & ")

                my_table += "</tr>"
                my_table += "</tabla>"
                Dim ds_cur As New DataSet
                adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                adp.SelectCommand.CommandType = CommandType.StoredProcedure
                adp.SelectCommand.Parameters.AddWithValue("@LOGINID", Sales_DDL.SelectedValue)
                adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                adp.Fill(ds_cur)

                Dim dtsalesid As New DataTable
                Dim dtticketrpt As New DataTable
                Dim dtrailrpt As New DataTable
                Dim dthtlrpt As New DataTable
                Dim dtrechargerpt As New DataTable
                Dim dteblrpt As New DataTable
                Dim dtbusrpt As New DataTable
                dtsalesid = ds_cur.Tables(0)
                dtticketrpt = ds_cur.Tables(1)
                dtrailrpt = ds_cur.Tables(2)
                dthtlrpt = ds_cur.Tables(3)
                dtrechargerpt = ds_cur.Tables(4)
                dteblrpt = ds_cur.Tables(5)
                dtbusrpt = ds_cur.Tables(6)


                my_table += "<table    border='0' cellspacing='0'  cellpadding='0' width='1200px'>"




                my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Sales Id</td>"
                'my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"

                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus Ticket</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                my_table += "</tr>"


                'my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                'my_table += "<td colspan='11' style='height: 2px;'></td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                'my_table += "</tr>"

                If (dtsalesid.Rows.Count > 0) Then
                    Dim SumTicket As Integer = 0
                    Dim SumTotalTicketSale As Integer = 0
                    Dim SumRailBooked As Integer = 0
                    Dim SumTotalRailSale As Integer = 0
                    Dim SumHotelBooked As Integer = 0
                    Dim SumTotalHotelSale As Integer = 0
                    Dim SumRecharge As Integer = 0
                    Dim SumTotalRechargeSale As Integer = 0
                    Dim SumBill As Integer = 0
                    Dim SumTotalBillSale As Integer = 0
                    Dim SumBus As Integer = 0
                    Dim SumTotalBusSale As Integer = 0
                    If (dtticketrpt.Rows.Count > 0) Then
                        SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                        SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                    End If
                    If (dtrailrpt.Rows.Count > 0) Then
                        SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                        SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                    End If
                    If (dthtlrpt.Rows.Count > 0) Then
                        SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                        SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                    End If
                    If (dtrechargerpt.Rows.Count > 0) Then
                        SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                        SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                    End If
                    If (dteblrpt.Rows.Count > 0) Then
                        SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                        SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                    End If
                    If (dtbusrpt.Rows.Count > 0) Then
                        SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                        SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                    End If
                    For Each drsales As DataRow In dtsalesid.Rows
                        'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        Dim TotalTkt As Integer
                        Dim TktNetfare As Integer
                        TotalTkt = 0
                        TktNetfare = 0
                        If (dtticketrpt.Rows.Count > 0) Then
                            TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                            TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                        End If
                        Dim TotalRailTkt As Integer
                        Dim TotalRailSale As Integer
                        TotalRailTkt = 0
                        TotalRailSale = 0
                        If (dtrailrpt.Rows.Count > 0) Then
                            TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                            TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                        End If

                        Dim TotalHotelBooked As Integer
                        Dim TotalHotelSale As Integer
                        TotalHotelBooked = 0
                        TotalHotelSale = 0
                        If (dthtlrpt.Rows.Count > 0) Then
                            TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                            TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                        End If

                        Dim TotalRechargeNo As Integer
                        Dim TotalRechargeSale As Integer
                        TotalRechargeNo = 0
                        TotalRechargeSale = 0
                        If (dtrechargerpt.Rows.Count > 0) Then
                            TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                            TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                        End If

                        Dim TotalEblNo As Integer
                        Dim TotalEblSale As Integer
                        TotalEblNo = 0
                        TotalEblSale = 0
                        If (dteblrpt.Rows.Count > 0) Then
                            TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                            TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                        End If

                        Dim TotalBusNo As Integer
                        Dim TotalBusSale As Integer
                        TotalBusNo = 0
                        TotalBusSale = 0
                        If (dtbusrpt.Rows.Count > 0) Then
                            TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                            TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                        End If
                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & Sales_DDL.SelectedValue & "</td>"
                        'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                        my_table += "</tr>"
                    Next
                    'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                    'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                    'my_table += "</tr>"
                    my_table += "</table>"
                Else
                    my_table += "<tr >"
                    my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & Sales_DDL.SelectedValue & "</td>"
                    'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "</tr>"
                End If
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tabla>"
                my_table_final += my_table
                'Next
            Else
                For Each drsalesmail As DataRow In dtsalesmail.Rows


                    my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"

                    If (cnt = 0) Then


                        my_table += "<tr>"
                        my_table += "<td align='left' colspan='11' style='font-weight: bold;'>Dear Sir,</td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td align='left' colspan='11'>Please find today's sales report mention below.  </td>"
                        my_table += "</tr>"
                        my_table += "<tr>"
                        my_table += "<td style='height:20px' colspan='11'></td>"
                        my_table += "</tr>"
                        cnt = cnt + 1
                    End If


                    my_table += "<tr>"
                    my_table += "<td align='left'>"
                    my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                    my_table += "<tr>"
                    my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [" & drsalesmail("Name") & "(" & drsalesmail("EmailId") & ")] </td>"

                    my_table += "</tr>"
                    my_table += "</tabla>"
                    Dim ds_cur As New DataSet
                    adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure
                    adp.SelectCommand.Parameters.AddWithValue("@LOGINID", drsalesmail("EmailId"))
                    adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                    adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                    adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                    adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                    adp.Fill(ds_cur)

                    Dim dtsalesid As New DataTable
                    Dim dtticketrpt As New DataTable
                    Dim dtrailrpt As New DataTable
                    Dim dthtlrpt As New DataTable
                    Dim dtrechargerpt As New DataTable
                    Dim dteblrpt As New DataTable
                    Dim dtbusrpt As New DataTable
                    dtsalesid = ds_cur.Tables(0)
                    dtticketrpt = ds_cur.Tables(1)
                    dtrailrpt = ds_cur.Tables(2)
                    dthtlrpt = ds_cur.Tables(3)
                    dtrechargerpt = ds_cur.Tables(4)
                    dteblrpt = ds_cur.Tables(5)
                    dtbusrpt = ds_cur.Tables(6)


                    my_table += "<table    border='0' cellspacing='0'  cellpadding='0' width='1200px'>"




                    my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                    my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Sales Id</td>"
                    my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"

                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus Ticket</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                    my_table += "</tr>"


                    'my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                    'my_table += "<td colspan='11' style='height: 2px;'></td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                    'my_table += "</tr>"

                    If (dtsalesid.Rows.Count > 0) Then
                        Dim SumTicket As Integer = 0
                        Dim SumTotalTicketSale As Integer = 0
                        Dim SumRailBooked As Integer = 0
                        Dim SumTotalRailSale As Integer = 0
                        Dim SumHotelBooked As Integer = 0
                        Dim SumTotalHotelSale As Integer = 0
                        Dim SumRecharge As Integer = 0
                        Dim SumTotalRechargeSale As Integer = 0
                        Dim SumBill As Integer = 0
                        Dim SumTotalBillSale As Integer = 0
                        Dim SumBus As Integer = 0
                        Dim SumTotalBusSale As Integer = 0
                        If (dtticketrpt.Rows.Count > 0) Then
                            SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                            SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                        End If
                        If (dtrailrpt.Rows.Count > 0) Then
                            SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                            SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                        End If
                        If (dthtlrpt.Rows.Count > 0) Then
                            SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                            SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                        End If
                        If (dtrechargerpt.Rows.Count > 0) Then
                            SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                            SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                        End If
                        If (dteblrpt.Rows.Count > 0) Then
                            SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                            SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                        End If
                        If (dtbusrpt.Rows.Count > 0) Then
                            SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                            SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                        End If
                        For Each drsales As DataRow In dtsalesid.Rows
                            'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            Dim TotalTkt As Integer
                            Dim TktNetfare As Integer
                            TotalTkt = 0
                            TktNetfare = 0
                            If (dtticketrpt.Rows.Count > 0) Then
                                TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                                TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                            End If
                            Dim TotalRailTkt As Integer
                            Dim TotalRailSale As Integer
                            TotalRailTkt = 0
                            TotalRailSale = 0
                            If (dtrailrpt.Rows.Count > 0) Then
                                TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                                TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                            End If

                            Dim TotalHotelBooked As Integer
                            Dim TotalHotelSale As Integer
                            TotalHotelBooked = 0
                            TotalHotelSale = 0
                            If (dthtlrpt.Rows.Count > 0) Then
                                TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                                TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                            End If

                            Dim TotalRechargeNo As Integer
                            Dim TotalRechargeSale As Integer
                            TotalRechargeNo = 0
                            TotalRechargeSale = 0
                            If (dtrechargerpt.Rows.Count > 0) Then
                                TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                                TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                            End If

                            Dim TotalEblNo As Integer
                            Dim TotalEblSale As Integer
                            TotalEblNo = 0
                            TotalEblSale = 0
                            If (dteblrpt.Rows.Count > 0) Then
                                TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                                TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                            End If
                            Dim TotalBusNo As Integer
                            Dim TotalBusSale As Integer
                            TotalBusNo = 0
                            TotalBusSale = 0
                            If (dtbusrpt.Rows.Count > 0) Then
                                TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                                TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                            End If
                            my_table += "<tr >"
                            my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("EmailId") & "</td>"
                            my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                            my_table += "</tr>"
                        Next
                        'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                        'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                        'my_table += "</tr>"
                        my_table += "</table>"
                    Else
                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("EmailId") & "</td>"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "</tr>"
                    End If
                    my_table += "</td>"
                    my_table += "</tr>"
                    my_table += "</tabla>"
                    my_table_final += my_table
                Next
            End If


            lblmail.Text = my_table_final
        Catch ex As Exception

        End Try


        Try
            Dim sw As New StringWriter()
            Dim w As New HtmlTextWriter(sw)
            lblmail.RenderControl(w)
            Dim s As String = sw.GetStringBuilder().ToString()
            Dim mail As New MailMessage()
            mail.From = New MailAddress("b2bsalessupport@RWT.com")
            mail.To.Add("devesh.singh@galileo.co.in")
            mail.Subject = "Sale Report"
            mail.Body += s
            mail.IsBodyHtml = True
            Dim smtp As New SmtpClient("shekhal.ITZ.com")
            smtp.Credentials = New System.Net.NetworkCredential("b2badmin@RWT.com", "america")
            smtp.Send(mail)
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Sales report sent sucessfully');", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to sent mail please try again');", True)
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_totsale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_totsale.Click
        Try
            lblsale.Text = ""
            lblmail.Text = ""
            btn_totsaleexportexcel.Visible = True
            btn_totsaleexportword.Visible = True


            btn_resultexportexcel.Visible = False
            btn_resultexportword.Visible = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString

            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))


            Dim dtsalesmail As DataTable
            dtsalesmail = STDom.GetSalesRef().Tables(0)
            Dim my_table As String = ""
            Dim my_table_final As String = ""

            Dim cnt As Integer = 0
            If (Sales_DDL.SelectedIndex > 0) Then
                'For Each drsalesmail As DataRow In dtsalesmail.Rows

                'Dim salesarray As Array = dtsalesmail.Select("SALESEXECID='" & Sales_DDL.SelectedValue & "'", "")

                my_table = "<table    border='0' cellspacing='0'  cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td align='left'>"
                my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                my_table += "<tr>"
                my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [(" & Sales_DDL.SelectedValue & ")] </td>"

                my_table += "</tr>"
                my_table += "</tabla>"
                Dim ds_cur As New DataSet
                adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                adp.SelectCommand.CommandType = CommandType.StoredProcedure
                adp.SelectCommand.Parameters.AddWithValue("@LOGINID", Sales_DDL.SelectedValue)
                adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                adp.Fill(ds_cur)

                Dim dtsalesid As New DataTable
                Dim dtticketrpt As New DataTable
                Dim dtrailrpt As New DataTable
                Dim dthtlrpt As New DataTable
                Dim dtrechargerpt As New DataTable
                Dim dteblrpt As New DataTable
                Dim dtbusrpt As New DataTable
                dtsalesid = ds_cur.Tables(0)
                dtticketrpt = ds_cur.Tables(1)
                dtrailrpt = ds_cur.Tables(2)
                dthtlrpt = ds_cur.Tables(3)
                dtrechargerpt = ds_cur.Tables(4)
                dteblrpt = ds_cur.Tables(5)
                dtbusrpt = ds_cur.Tables(6)


                my_table += "<table    border='0' cellspacing='0'  cellpadding='0' width='1100px'>"
                'If (cnt = 0) Then


                my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Sales Id</td>"
                'my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus Ticket</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                my_table += "</tr>"
                'cnt = cnt + 1
                'Else

                '    my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                '    my_table += "<td colspan='11' style='height: 2px;'></td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                '    my_table += "</tr>"
                'End If
                If (dtsalesid.Rows.Count > 0) Then
                    Dim SumTicket As Integer = 0
                    Dim SumTotalTicketSale As Integer = 0
                    Dim SumRailBooked As Integer = 0
                    Dim SumTotalRailSale As Integer = 0
                    Dim SumHotelBooked As Integer = 0
                    Dim SumTotalHotelSale As Integer = 0
                    Dim SumRecharge As Integer = 0
                    Dim SumTotalRechargeSale As Integer = 0
                    Dim SumBill As Integer = 0
                    Dim SumTotalBillSale As Integer = 0
                    Dim SumBus As Integer = 0
                    Dim SumTotalBusSale As Integer = 0
                    If (dtticketrpt.Rows.Count > 0) Then
                        SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                        SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                    End If
                    If (dtrailrpt.Rows.Count > 0) Then
                        SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                        SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                    End If
                    If (dthtlrpt.Rows.Count > 0) Then
                        SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                        SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                    End If
                    If (dtrechargerpt.Rows.Count > 0) Then
                        SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                        SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                    End If
                    If (dteblrpt.Rows.Count > 0) Then
                        SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                        SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                    End If
                    If (dtbusrpt.Rows.Count > 0) Then
                        SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                        SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                    End If
                    For Each drsales As DataRow In dtsalesid.Rows
                        'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        Dim TotalTkt As Integer
                        Dim TktNetfare As Integer
                        TotalTkt = 0
                        TktNetfare = 0
                        If (dtticketrpt.Rows.Count > 0) Then
                            TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                            TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                        End If
                        Dim TotalRailTkt As Integer
                        Dim TotalRailSale As Integer
                        TotalRailTkt = 0
                        TotalRailSale = 0
                        If (dtrailrpt.Rows.Count > 0) Then
                            TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                            TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                        End If

                        Dim TotalHotelBooked As Integer
                        Dim TotalHotelSale As Integer
                        TotalHotelBooked = 0
                        TotalHotelSale = 0
                        If (dthtlrpt.Rows.Count > 0) Then
                            TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                            TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                        End If

                        Dim TotalRechargeNo As Integer
                        Dim TotalRechargeSale As Integer
                        TotalRechargeNo = 0
                        TotalRechargeSale = 0
                        If (dtrechargerpt.Rows.Count > 0) Then
                            TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                            TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                        End If

                        Dim TotalEblNo As Integer
                        Dim TotalEblSale As Integer
                        TotalEblNo = 0
                        TotalEblSale = 0
                        If (dteblrpt.Rows.Count > 0) Then
                            TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                            TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                        End If

                        Dim TotalBusNo As Integer
                        Dim TotalBusSale As Integer
                        TotalBusNo = 0
                        TotalBusSale = 0
                        If (dtbusrpt.Rows.Count > 0) Then
                            TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                            TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                        End If
                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & Sales_DDL.SelectedValue & "</td>"
                        'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;</td>" '" & drsalesmail("MobileNo") & "
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                        my_table += "</tr>"
                    Next
                    'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                    'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                    'my_table += "</tr>"
                    my_table += "</table>"
                Else
                    my_table += "<tr >"
                    my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & Sales_DDL.SelectedValue & "</td>"
                    'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;</td>" '" & drsalesmail("MobileNo") & "
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "</tr>"
                End If
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tabla>"
                my_table_final += my_table
                'Next
            Else
                For Each drsalesmail As DataRow In dtsalesmail.Rows


                    'my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"
                    'my_table += "<tr>"
                    'my_table += "<td align='left'>"
                    'my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                    'my_table += "<tr>"
                    'my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [" & drsalesmail("Name") & "(" & drsalesmail("EmailId") & ")] </td>"

                    'my_table += "</tr>"
                    'my_table += "</tabla>"

                    'my_table += "</td>"
                    'my_table += "</tr>"
                    'my_table += "</table>"
                    Dim ds_cur As New DataSet
                    adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure
                    adp.SelectCommand.Parameters.AddWithValue("@LOGINID", drsalesmail("EmailId"))
                    adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                    adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                    adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                    adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                    adp.Fill(ds_cur)

                    Dim dtsalesid As New DataTable
                    Dim dtticketrpt As New DataTable
                    Dim dtrailrpt As New DataTable
                    Dim dthtlrpt As New DataTable
                    Dim dtrechargerpt As New DataTable
                    Dim dteblrpt As New DataTable
                    Dim dtbusrpt As New DataTable
                    dtsalesid = ds_cur.Tables(0)
                    dtticketrpt = ds_cur.Tables(1)
                    dtrailrpt = ds_cur.Tables(2)
                    dthtlrpt = ds_cur.Tables(3)
                    dtrechargerpt = ds_cur.Tables(4)
                    dteblrpt = ds_cur.Tables(5)
                    dtbusrpt = ds_cur.Tables(6)


                    my_table = "<table align='center'   border='0' cellspacing='0'  cellpadding='0' width='1100px'>"
                    'If (cnt = 0) Then
                    my_table += "<tr>"
                    my_table += "<td align='left' colspan='12' style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [" & drsalesmail("Name") & "(" & drsalesmail("EmailId") & ")] </td>"

                    my_table += "</tr>"

                    my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                    my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Sales Id</td>"
                    my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus Ticket</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                    my_table += "</tr>"
                    'cnt = cnt + 1
                    'Else

                    '    my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                    '    my_table += "<td colspan='11' style='height: 2px;'></td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                    '    my_table += "</tr>"
                    'End If
                    If (dtsalesid.Rows.Count > 0) Then
                        Dim SumTicket As Integer = 0
                        Dim SumTotalTicketSale As Integer = 0
                        Dim SumRailBooked As Integer = 0
                        Dim SumTotalRailSale As Integer = 0
                        Dim SumHotelBooked As Integer = 0
                        Dim SumTotalHotelSale As Integer = 0
                        Dim SumRecharge As Integer = 0
                        Dim SumTotalRechargeSale As Integer = 0
                        Dim SumBill As Integer = 0
                        Dim SumTotalBillSale As Integer = 0
                        Dim SumBus As Integer = 0
                        Dim SumTotalBusSale As Integer = 0
                        If (dtticketrpt.Rows.Count > 0) Then
                            SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                            SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                        End If
                        If (dtrailrpt.Rows.Count > 0) Then
                            SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                            SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                        End If
                        If (dthtlrpt.Rows.Count > 0) Then
                            SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                            SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                        End If
                        If (dtrechargerpt.Rows.Count > 0) Then
                            SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                            SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                        End If
                        If (dteblrpt.Rows.Count > 0) Then
                            SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                            SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                        End If

                        If (dtbusrpt.Rows.Count > 0) Then
                            SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                            SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                        End If
                        For Each drsales As DataRow In dtsalesid.Rows
                            'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            Dim TotalTkt As Integer
                            Dim TktNetfare As Integer
                            TotalTkt = 0
                            TktNetfare = 0
                            If (dtticketrpt.Rows.Count > 0) Then
                                TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                                TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                            End If
                            Dim TotalRailTkt As Integer
                            Dim TotalRailSale As Integer
                            TotalRailTkt = 0
                            TotalRailSale = 0
                            If (dtrailrpt.Rows.Count > 0) Then
                                TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                                TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                            End If

                            Dim TotalHotelBooked As Integer
                            Dim TotalHotelSale As Integer
                            TotalHotelBooked = 0
                            TotalHotelSale = 0
                            If (dthtlrpt.Rows.Count > 0) Then
                                TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                                TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                            End If

                            Dim TotalRechargeNo As Integer
                            Dim TotalRechargeSale As Integer
                            TotalRechargeNo = 0
                            TotalRechargeSale = 0
                            If (dtrechargerpt.Rows.Count > 0) Then
                                TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                                TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                            End If

                            Dim TotalEblNo As Integer
                            Dim TotalEblSale As Integer
                            TotalEblNo = 0
                            TotalEblSale = 0
                            If (dteblrpt.Rows.Count > 0) Then
                                TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                                TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                            End If

                            Dim TotalBusNo As Integer
                            Dim TotalBusSale As Integer
                            TotalBusNo = 0
                            TotalBusSale = 0
                            If (dtbusrpt.Rows.Count > 0) Then
                                TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                                TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                            End If

                            my_table += "<tr >"
                            my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("EmailId") & "</td>"
                            my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                            my_table += "</tr>"
                        Next
                        'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                        'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                        'my_table += "</tr>"
                        my_table += "</table>"
                    Else
                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & drsalesmail("EmailId") & "</td>"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "</tr>"
                    End If
                    my_table += "</td>"
                    my_table += "</tr>"
                    my_table += "</tabla>"
                    my_table_final += my_table
                Next

            End If


            lbl_totsale.Text = my_table_final
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub btn_resultexportexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_resultexportexcel.Click
        Try
            Dim filename As String = ""
            filename = "Salesreport.xls"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            lblsale.RenderControl(htmlWrite)
            Dim s As String = stringWrite.GetStringBuilder().ToString()
            Response.Write(s)
            Response.[End]()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub btn_resultexportword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_resultexportword.Click
        Try
            Dim filename As String = ""
            filename = "Salesreport.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            lblsale.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_totsaleexportexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_totsaleexportexcel.Click
        Try
            Dim filename As String = ""
            filename = "Totalsalesreport.xls"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")

            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

            totss.RenderControl(htmlWrite)
            Dim s As String = stringWrite.GetStringBuilder().ToString()
            Response.Write(s)
            Response.[End]()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_totsaleexportword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_totsaleexportword.Click
        Try
            Dim filename As String = ""
            filename = "Totalsalesreport.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            totss.RenderControl(htmlWrite)
            Dim s As String = stringWrite.GetStringBuilder().ToString()
            Response.Write(s)
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_Type_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Type.Click
        lbl_totsale.Text = ""
        Try
            lblsale.Text = ""
            lblmail.Text = ""
            btn_totsaleexportexcel.Visible = True
            btn_totsaleexportword.Visible = True


            btn_resultexportexcel.Visible = False
            btn_resultexportword.Visible = False
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString

            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))


            Dim dtsalesmail As DataTable
            dtsalesmail = STDom.GetAgentType().Tables(0)
            Dim my_table As String = ""
            Dim my_table_final As String = ""

            Dim cnt As Integer = 0
            If (ddl_type.SelectedIndex > 0) Then
                'For Each drsalesmail As DataRow In dtsalesmail.Rows

                'Dim salesarray As Array = dtsalesmail.Select("SALESEXECID='" & Sales_DDL.SelectedValue & "'", "")


                Dim ds_cur As New DataSet
                adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                adp.SelectCommand.CommandType = CommandType.StoredProcedure
                adp.SelectCommand.Parameters.AddWithValue("@LOGINID", Sales_DDL.SelectedValue)
                adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                adp.SelectCommand.Parameters.AddWithValue("@TYPE", ddl_type.SelectedValue)
                adp.Fill(ds_cur)

                Dim dtsalesid As New DataTable
                Dim dtticketrpt As New DataTable
                Dim dtrailrpt As New DataTable
                Dim dthtlrpt As New DataTable
                Dim dtrechargerpt As New DataTable
                Dim dteblrpt As New DataTable
                Dim dtbusrpt As New DataTable
                dtsalesid = ds_cur.Tables(0)
                dtticketrpt = ds_cur.Tables(1)
                dtrailrpt = ds_cur.Tables(2)
                dthtlrpt = ds_cur.Tables(3)
                dtrechargerpt = ds_cur.Tables(4)
                dteblrpt = ds_cur.Tables(5)
                dtbusrpt = ds_cur.Tables(6)

                Dim SumTicket As Integer = 0
                Dim SumTotalTicketSale As Integer = 0
                Dim SumRailBooked As Integer = 0
                Dim SumTotalRailSale As Integer = 0
                Dim SumHotelBooked As Integer = 0
                Dim SumTotalHotelSale As Integer = 0
                Dim SumRecharge As Integer = 0
                Dim SumTotalRechargeSale As Integer = 0
                Dim SumBill As Integer = 0
                Dim SumTotalBillSale As Integer = 0
                Dim SumBus As Integer = 0
                Dim SumTotalBusSale As Integer = 0
                If (dtticketrpt.Rows.Count > 0) Then
                    SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                    SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                End If
                If (dtrailrpt.Rows.Count > 0) Then
                    SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                    SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                End If
                If (dthtlrpt.Rows.Count > 0) Then
                    SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                    SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                End If
                If (dtrechargerpt.Rows.Count > 0) Then
                    SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                    SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                End If
                If (dteblrpt.Rows.Count > 0) Then
                    SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                    SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                End If
                If (dtbusrpt.Rows.Count > 0) Then
                    SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                    SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                End If

                Dim K As String
                K = "Sales Report (" & Sales_DDL.SelectedValue & ")  Total Transactions :" & (SumTicket + SumRailBooked + SumRecharge + SumBill + SumBus + SumHotelBooked)
                K = K & " Total Amount :  " & (SumTotalTicketSale + SumTotalRailSale + SumTotalHotelSale + SumTotalRechargeSale + SumTotalBillSale + SumTotalBusSale)
                my_table = "<table    border='0' cellspacing='0'  cellpadding='0' align='center'>"
                my_table += "<tr>"
                my_table += "<td align='left'>"
                my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                my_table += "<tr>"
                my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >" & K & "</td>"

                my_table += "</tr>"
                my_table += "</tabla>"


                my_table += "<table    border='0' cellspacing='0'  cellpadding='0' width='1100px'>"
                'If (cnt = 0) Then


                my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Group Type</td>"
                'my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus No</td>"
                my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                my_table += "</tr>"
                'cnt = cnt + 1
                'Else

                '    my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                '    my_table += "<td colspan='11' style='height: 2px;'></td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                '    my_table += "</tr>"
                'End If
                If (dtsalesid.Rows.Count > 0) Then
                    
                    For Each drsales As DataRow In dtsalesid.Rows
                        'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                        Dim TotalTkt As Integer
                        Dim TktNetfare As Integer
                        TotalTkt = 0
                        TktNetfare = 0
                        If (dtticketrpt.Rows.Count > 0) Then
                            TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                            TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                        End If
                        Dim TotalRailTkt As Integer
                        Dim TotalRailSale As Integer
                        TotalRailTkt = 0
                        TotalRailSale = 0
                        If (dtrailrpt.Rows.Count > 0) Then
                            TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                            TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                        End If

                        Dim TotalHotelBooked As Integer
                        Dim TotalHotelSale As Integer
                        TotalHotelBooked = 0
                        TotalHotelSale = 0
                        If (dthtlrpt.Rows.Count > 0) Then
                            TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                            TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                        End If

                        Dim TotalRechargeNo As Integer
                        Dim TotalRechargeSale As Integer
                        TotalRechargeNo = 0
                        TotalRechargeSale = 0
                        If (dtrechargerpt.Rows.Count > 0) Then
                            TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                            TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                        End If

                        Dim TotalEblNo As Integer
                        Dim TotalEblSale As Integer
                        TotalEblNo = 0
                        TotalEblSale = 0
                        If (dteblrpt.Rows.Count > 0) Then
                            TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                            TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                        End If

                        Dim TotalBusNo As Integer
                        Dim TotalBusSale As Integer
                        TotalBusNo = 0
                        TotalBusSale = 0
                        If (dtbusrpt.Rows.Count > 0) Then
                            TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                            TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                        End If

                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & ddl_type.SelectedValue & "</td>"
                        'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;</td>" '" & drsalesmail("MobileNo") & "
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                        my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                        my_table += "</tr>"
                    Next
                    'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                    'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                    'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                    'my_table += "</tr>"
                    my_table += "</table>"
                Else
                    my_table += "<tr >"
                    my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & ddl_type.SelectedValue & "</td>"
                    'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;</td>" '" & drsalesmail("MobileNo") & "
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                    my_table += "</tr>"
                End If
                my_table += "</td>"
                my_table += "</tr>"
                my_table += "</tabla>"
                my_table_final += my_table
                'Next
            Else
                For Each drsalesmail As DataRow In dtsalesmail.Rows


                    'my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"
                    'my_table += "<tr>"
                    'my_table += "<td align='left'>"
                    'my_table += "<table    border='0' cellspacing='0'  cellpadding='0'>"
                    'my_table += "<tr>"
                    'my_table += "<td style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >Sales Report [" & drsalesmail("Name") & "(" & drsalesmail("EmailId") & ")] </td>"

                    'my_table += "</tr>"
                    'my_table += "</tabla>"

                    'my_table += "</td>"
                    'my_table += "</tr>"
                    'my_table += "</table>"
                    Dim ds_cur As New DataSet
                    adp = New SqlDataAdapter("SP_TOTALSALEREPORT", con)
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure
                    adp.SelectCommand.Parameters.AddWithValue("@LOGINID", Session("UID"))
                    adp.SelectCommand.Parameters.AddWithValue("@USERTYPE", "MAIL")
                    adp.SelectCommand.Parameters.AddWithValue("@FROMDATE", FromDate)
                    adp.SelectCommand.Parameters.AddWithValue("@TODATE", ToDate)
                    adp.SelectCommand.Parameters.AddWithValue("@AGENTID", AgentID)
                    adp.SelectCommand.Parameters.AddWithValue("@TYPE", drsalesmail("GroupType"))
                    adp.Fill(ds_cur)

                    Dim dtsalesid As New DataTable
                    Dim dtticketrpt As New DataTable
                    Dim dtrailrpt As New DataTable
                    Dim dthtlrpt As New DataTable
                    Dim dtrechargerpt As New DataTable
                    Dim dteblrpt As New DataTable
                    Dim dtbusrpt As New DataTable
                    dtsalesid = ds_cur.Tables(0)
                    dtticketrpt = ds_cur.Tables(1)
                    dtrailrpt = ds_cur.Tables(2)
                    dthtlrpt = ds_cur.Tables(3)
                    dtrechargerpt = ds_cur.Tables(4)
                    dteblrpt = ds_cur.Tables(5)
                    dtbusrpt = ds_cur.Tables(6)

                    Dim SumTicket As Integer = 0
                    Dim SumTotalTicketSale As Integer = 0
                    Dim SumRailBooked As Integer = 0
                    Dim SumTotalRailSale As Integer = 0
                    Dim SumHotelBooked As Integer = 0
                    Dim SumTotalHotelSale As Integer = 0
                    Dim SumRecharge As Integer = 0
                    Dim SumTotalRechargeSale As Integer = 0
                    Dim SumBill As Integer = 0
                    Dim SumTotalBillSale As Integer = 0
                    Dim SumBus As Integer = 0
                    Dim SumTotalBusSale As Integer = 0
                    If (dtticketrpt.Rows.Count > 0) Then
                        SumTicket = dtticketrpt.Compute("Sum(TOTALTICKET)", "")
                        SumTotalTicketSale = dtticketrpt.Compute("Sum(NETFARE)", "")
                    End If
                    If (dtrailrpt.Rows.Count > 0) Then
                        SumRailBooked = dtrailrpt.Compute("Sum(TOTALRAILTICKET)", "")
                        SumTotalRailSale = dtrailrpt.Compute("Sum(RAILTOTALFARE)", "")
                    End If
                    If (dthtlrpt.Rows.Count > 0) Then
                        SumHotelBooked = dthtlrpt.Compute("Sum(TOTALHOTELTICKET)", "")
                        SumTotalHotelSale = dthtlrpt.Compute("Sum(HOTELTOTALSALE)", "")
                    End If
                    If (dtrechargerpt.Rows.Count > 0) Then
                        SumRecharge = dtrechargerpt.Compute("Sum(TOTALRECHARGE)", "")
                        SumTotalRechargeSale = dtrechargerpt.Compute("Sum(RECHARGETOTALSALE)", "")
                    End If
                    If (dteblrpt.Rows.Count > 0) Then
                        SumBill = dteblrpt.Compute("Sum(TOTALBILL)", "")
                        SumTotalBillSale = dteblrpt.Compute("Sum(EBLETOTALSALE)", "")
                    End If
                    If (dtbusrpt.Rows.Count > 0) Then
                        SumBus = dtbusrpt.Compute("Sum(TOTALBUS)", "")
                        SumTotalBusSale = dtbusrpt.Compute("Sum(BUSTOTALSALE)", "")
                    End If

                    Dim S As String
                    S = "Sales Report (" & drsalesmail("GroupType") & ")  Total Transactions :" & (SumTicket + SumRailBooked + SumRecharge + SumBill + SumBus + SumHotelBooked)
                    S = S & " Total Amount :  " & (SumTotalTicketSale + SumTotalRailSale + SumTotalHotelSale + SumTotalRechargeSale + SumTotalBillSale + SumTotalBusSale)
                    my_table = "<table align='center'   border='0' cellspacing='0'  cellpadding='0' width='1100px'>"
                    'If (cnt = 0) Then
                    my_table += "<tr>"
                    my_table += "<td align='left' colspan='12' style='color: #000;font-family: arial; font-size: 12px; font-weight: bold;height:25px;' >" & S & "</td>"

                    my_table += "</tr>"

                    my_table += "<tr style='background-color:#F0F0F0;color: #000;font-family: arial; font-size: 12px; font-weight: bold;'  >"
                    my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Gropu Type</td>"
                    'my_table += "<td align='left' style='border: thin solid #000000;height: 30px; '>&nbsp;&nbsp;&nbsp;Mobile No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Ticket Count</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Sale Air</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Rail Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Booked</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Hotel Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Recharge Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bill No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bill Sale</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px;'>&nbsp;Bus No</td>"
                    my_table += "<td style='border: thin solid #000000;height: 30px; '>&nbsp;Bus Sale</td>"
                    my_table += "</tr>"
                    'cnt = cnt + 1
                    'Else

                    '    my_table += "<tr style='background-color:#fff;color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;' >"
                    '    my_table += "<td colspan='11' style='height: 2px;'></td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Ticket Count</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Sale Air</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Booked</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Rail Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Booked</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Hotel Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge No</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Recharge Sale</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill No</td>"
                    '    'my_table += "<td style='border-right-style: solid; border-right-width: thin; border-right-color: #20313f;height: 30px; padding: 5px;'>Bill Sale</td>"
                    '    my_table += "</tr>"
                    'End If
                    If (dtsalesid.Rows.Count > 0) Then
                       

                        For Each drsales As DataRow In dtsalesid.Rows
                            'Dim ticketarray As Array = dtticketrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim railarray As Array = dtrailrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim hotelarray As Array = dthtlrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim rechargearray As Array = dtrechargerpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            'Dim eblarray As Array = dteblrpt.Select("AGENTID='" & drsales("AGENTID") & "'", "")
                            Dim TotalTkt As Integer
                            Dim TktNetfare As Integer
                            TotalTkt = 0
                            TktNetfare = 0
                            If (dtticketrpt.Rows.Count > 0) Then
                                TotalTkt = dtticketrpt.Rows(0)("TOTALTICKET").ToString()
                                TktNetfare = dtticketrpt.Rows(0)("NETFARE").ToString()
                            End If
                            Dim TotalRailTkt As Integer
                            Dim TotalRailSale As Integer
                            TotalRailTkt = 0
                            TotalRailSale = 0
                            If (dtrailrpt.Rows.Count > 0) Then
                                TotalRailTkt = dtrailrpt.Rows(0)("TOTALRAILTICKET").ToString()
                                TotalRailSale = dtrailrpt.Rows(0)("RAILTOTALFARE").ToString()
                            End If

                            Dim TotalHotelBooked As Integer
                            Dim TotalHotelSale As Integer
                            TotalHotelBooked = 0
                            TotalHotelSale = 0
                            If (dthtlrpt.Rows.Count > 0) Then
                                TotalHotelBooked = dthtlrpt.Rows(0)("TOTALHOTELTICKET").ToString()
                                TotalHotelSale = dthtlrpt.Rows(0)("HOTELTOTALSALE").ToString()
                            End If

                            Dim TotalRechargeNo As Integer
                            Dim TotalRechargeSale As Integer
                            TotalRechargeNo = 0
                            TotalRechargeSale = 0
                            If (dtrechargerpt.Rows.Count > 0) Then
                                TotalRechargeNo = dtrechargerpt.Rows(0)("TOTALRECHARGE").ToString()
                                TotalRechargeSale = dtrechargerpt.Rows(0)("RECHARGETOTALSALE").ToString()
                            End If

                            Dim TotalEblNo As Integer
                            Dim TotalEblSale As Integer
                            TotalEblNo = 0
                            TotalEblSale = 0
                            If (dteblrpt.Rows.Count > 0) Then
                                TotalEblNo = dteblrpt.Rows(0)("TOTALBILL").ToString()
                                TotalEblSale = dteblrpt.Rows(0)("EBLETOTALSALE").ToString()
                            End If

                            Dim TotalBusNo As Integer
                            Dim TotalBusSale As Integer
                            TotalBusNo = 0
                            TotalBusSale = 0
                            If (dtbusrpt.Rows.Count > 0) Then
                                TotalBusNo = dtbusrpt.Rows(0)("TOTALBUS").ToString()
                                TotalBusSale = dtbusrpt.Rows(0)("BUSTOTALSALE").ToString()
                            End If

                            my_table += "<tr >"
                            my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("GroupType") & "</td>"
                            'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TktNetfare & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailTkt & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRailSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelBooked & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalHotelSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalRechargeSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalEblSale & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusNo & "</td>"
                            my_table += "<td align='left' style='border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;" & TotalBusSale & "</td>"
                            my_table += "</tr>"
                        Next
                        'my_table += "<tr style='font-weight: bold;background-color: #FFFF00;'>"
                        'my_table += "<td align='center' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000;'>TOTAL</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTicket & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalTicketSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRailBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRailSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumHotelBooked & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalHotelSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumRecharge & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalRechargeSale & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumBill & "</td>"
                        'my_table += "<td align='center' style='padding: 2px;border: solid 1px #20313f;color: #000;'>" & SumTotalBillSale & "</td>"
                        'my_table += "</tr>"
                        my_table += "</table>"
                    Else
                        my_table += "<tr >"
                        my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & drsalesmail("GroupType") & "</td>"
                        'my_table += "<td align='left' style='height: 20px;padding: 2px;border: solid 1px #20313f;color: #000'>&nbsp;&nbsp;&nbsp;" & drsalesmail("MobileNo") & "</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "<td align='left' style='padding: 2px;border: solid 1px #20313f;color: #000;'>&nbsp;&nbsp;&nbsp;0</td>"
                        my_table += "</tr>"
                    End If
                    my_table += "</td>"
                    my_table += "</tr>"
                    my_table += "</tabla>"
                    my_table_final += my_table
                Next

            End If


            lbl_totsale.Text = my_table_final
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RBL_RTYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL_RTYPE.SelectedIndexChanged
        If (RBL_RTYPE.SelectedValue = "AGENTTYPE") Then
            td_GroupType.Visible = True
            td_SalesID.Visible = False
            btn_totsale.Visible = False
            btn_Type.Visible = True
            btn_result.Visible = False
            'btn_mail.Visible = False
        Else
            td_GroupType.Visible = False
            td_SalesID.Visible = True
            btn_totsale.Visible = True
            btn_Type.Visible = False
            btn_result.Visible = True
            'btn_mail.Visible = True
        End If
    End Sub
End Class
