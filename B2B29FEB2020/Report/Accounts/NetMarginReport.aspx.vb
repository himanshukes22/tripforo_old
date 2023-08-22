Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class SprReports_Accounts_NetMarginReport
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Private STDom As New SqlTransactionDom()
    Dim AgencyDDLDS, grdds, fltds As New DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim AgentID As String = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    td_Agency.Visible = False
                    AgentID = Session("UID").ToString()
                Else

                    
                End If

               
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            tr_sale.Visible = True
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

            Dim Airline As String = If([String].IsNullOrEmpty(txt_Airline.Text), "", txt_Airline.Text.Trim)
            grdds.Clear()
            grdds = ST.GetNetMarginReport(Session("UID").ToString, Session("User_Type").ToString, FromDate, ToDate, AgentID, ddl_Trip.SelectedValue, Airline)
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim dt2 As New DataTable
            If (Request("hidtxtAgencyName") <> "" AndAlso Request("hidtxtAgencyName") IsNot Nothing) Then
                Dim dtAgentAddress As New DataTable
                dtAgentAddress = STDom.GetAgencyDetails(AgentID).Tables(0)
                agent_address.Visible = True
                td_agentid.InnerText = dtAgentAddress.Rows(0)("User_ID").ToString()
                td_agenttype.InnerText = dtAgentAddress.Rows(0)("Agent_Type").ToString()
                td_Pan.InnerText = dtAgentAddress.Rows(0)("PanNo").ToString()
                td_Address.InnerText = dtAgentAddress.Rows(0)("Address").ToString() & "," & dtAgentAddress.Rows(0)("City").ToString() & "," & dtAgentAddress.Rows(0)("State").ToString() & "," & dtAgentAddress.Rows(0)("Country").ToString() & "," & dtAgentAddress.Rows(0)("ZipCode").ToString() & ""
                'td_Address1.InnerText = dtAgentAddress.Rows(0)("City") & "," & dtAgentAddress.Rows(0)("State") & "," & dtAgentAddress.Rows(0)("Country") & "," & dtAgentAddress.Rows(0)("ZipCode") & ""
                td_Mobile.InnerText = dtAgentAddress.Rows(0)("Mobile").ToString()
                td_Email.InnerText = dtAgentAddress.Rows(0)("Email").ToString()
                td_SalesRef.InnerText = dtAgentAddress.Rows(0)("SalesExecID").ToString()

            Else
                agent_address.Visible = False
            End If


            Dim dtme As New DataTable, sortexpr As String = ""
            dt = grdds.Tables(0)
            dt1 = grdds.Tables(1)
            dt2 = grdds.Tables(2)
            Dim my_table As String = ""

            my_table = "<table    border='0' cellspacing='0'  cellpadding='0'>"
            my_table += "<tr style='background-color:#004b91;color: #FFF;font-family: arial; font-size: 13px; font-weight: bold;'  class='hrdtext'>"
            my_table += "<td>"
            my_table += "<table   style='border: thin solid #000000' cellspacing='0'  cellpadding='0'>"
            my_table += "</table>"
            my_table += "</td>"
            my_table += "</tr>"

            my_table += "<tr>"
            my_table += "<td>"
            my_table += "<table  border='0'  cellspacing='0'  cellpadding='0'>"
            my_table += "<tr>"
            my_table += "<td>"
            my_table += "<table  border='0'  cellspacing='0'  cellpadding='0'   border='0' >"
            Dim TotalSale As Integer = 0
            Dim TotalRefund As Integer = 0
            Dim netfare As Double = 0
            Dim TotalTicketSale As Integer = 0
            Dim TotalRefundTicket As Integer = 0
            Dim TotalReissueTicket As Integer = 0
            For Each drsale As DataRow In dt.Rows

                Dim salearray As Array = dt.Select("VC='" & drsale("VC") & "'", "")
                Dim refundarray As Array = dt1.Select("VC='" & drsale("VC") & "'", "")
                Dim Reissuearray As Array = dt2.Select("VC='" & drsale("VC") & "'", "")
                ' Dim TotalTicketarray As Array = dt3.Select("VC='" & drsale("VC") & "'", "")
                Dim DtAirName As New DataTable

                DtAirName = ST.GetAirlineNameByCode(salearray(0)("VC").ToString()).Tables(0)

                my_table += "<tr>"
                If (DtAirName.Rows.Count = 0) Then
                    my_table += "<td align='center'>   <fieldset style='border: 1px solid #004b91;padding:5px;  '> <legend style='font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; color: #004b91;'> (" & salearray(0)("VC").ToString() & ")</legend>"

                Else
                    my_table += "<td align='center'>   <fieldset style='border: 1px solid #004b91;padding:5px;  '> <legend style='font-family: arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold; color: #004b91;'> (" & DtAirName.Rows(0)("AL_Name") & "(" & salearray(0)("VC").ToString() & "))</legend>"

                End If
                my_table += "<table  border='0'  cellspacing='0'  cellpadding='0'>"


                If (refundarray.Length = 0) Then
                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'></td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>TotalTicket</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Base Fare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>YQ</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Total Tax</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Service Tax</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Trans Fee</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Discount</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>TDS</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Total Fare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>NetFare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Refund/Reissue</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'>SALE</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalTicket").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("BaseFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("YQ").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("ServiceTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TransactionFee").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalDiscount").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TDS").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("NetFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("RefundFare").ToString() & "</td>"
                    my_table += "</tr>"

                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'>REFUND</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "</tr>"

                Else
                    DtAirName = ST.GetAirlineNameByCode(salearray(0)("VC").ToString()).Tables(0)
                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'></td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>TotalTicket</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Base Fare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>YQ</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Total Tax</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Service Tax</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Trans Fee</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Discount</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>TDS</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Total Fare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>NetFare</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #CCCCCC;font-family: arial; font-size: 13px; font-weight: bold;'>Refund/Reissue</td>"
                    my_table += "</tr>"
                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #fff;font-family: arial; font-size: 12px; font-weight: bold;'>SALE</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalTicket").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("BaseFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("YQ").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("ServiceTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TransactionFee").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalDiscount").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TDS").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("TotalFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("NetFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & salearray(0)("RefundFare").ToString() & "</td>"
                    my_table += "</tr>"
                    DtAirName.Clear()
                    DtAirName = ST.GetAirlineNameByCode(refundarray(0)("VC").ToString()).Tables(0)
                    my_table += "<tr>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'>REFUND</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TotalTicket").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("BaseFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("YQ").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TotalTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("ServiceTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TransactionFee").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TotalDiscount").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TDS").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("TotalFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("NetFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & refundarray(0)("RefundFare").ToString() & "</td>"
                    my_table += "</tr>"
                    TotalRefund += refundarray(0)("RefundFare").ToString()
                    TotalRefundTicket += refundarray(0)("TotalTicket").ToString()
                End If
                If (Reissuearray.Length = 0) Then
                    my_table += "<tr>"
                    my_table += "<td td width='80px' style='padding:7px;background-color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'>REISSUE</td>"

                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>0</td>"

                    my_table += "</td>"
                    my_table += "</tr>"
                    TotalSale += salearray(0)("NetFare").ToString()
                    TotalTicketSale += salearray(0)("TotalTicket").ToString()


                    If (refundarray.Length = 0) Then
                        If (Reissuearray.Length = 0) Then
                            netfare = salearray(0)("NetFare").ToString()
                        Else
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + Convert.ToDouble(Reissuearray(0)("ReissueCharge").ToString())) - 0
                            'TotalReissueTicket += Reissuearray(0)("TotalTicket").ToString()
                        End If
                        my_table += "<tr style='padding:7px;background-color: #FFFF99;font-family: arial; font-size: 13px; font-weight: bold;'>"
                        my_table += "<td colspan='11' style='padding:7px;color:#004b91' align='right'>NET SALE (" & salearray(0)("VC").ToString() & ")</td>"
                        my_table += "<td>" & netfare & "</td>"

                        my_table += "</td>"
                        my_table += "</tr>"
                    Else
                        If (Reissuearray.Length = 0) Then
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + 0) - Convert.ToDouble(refundarray(0)("RefundFare").ToString())
                        Else
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + Convert.ToDouble(Reissuearray(0)("ReissueCharge").ToString())) - Convert.ToDouble(refundarray(0)("RefundFare").ToString())
                            TotalReissueTicket += Reissuearray(0)("TotalTicket").ToString()
                        End If
                        my_table += "<tr style='padding:7px;background-color: #FFFF99;font-family: arial; font-size: 13px; font-weight: bold;'>"

                        my_table += "<td colspan='11' style='padding:7px;color:#004b91' align='right'>NET SALE (" & salearray(0)("VC").ToString() & ")</td>"
                        my_table += "<td>" & netfare & "</td>"

                        my_table += "</td>"
                        my_table += "</tr>"

                    End If





                Else
                    DtAirName.Clear()
                    DtAirName = ST.GetAirlineNameByCode(Reissuearray(0)("VC").ToString()).Tables(0)
                    my_table += "<tr>"
                    my_table += "<td td width='80px' style='padding:7px;background-color: #FFF;font-family: arial; font-size: 12px; font-weight: bold;'>REISSUE</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TotalTicket").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("BaseFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("YQ").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TotalTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("ServiceTax").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TransactionFee").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TotalDiscount").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TDS").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("TotalFare").ToString() & "</td>"
                    my_table += "<td width='80px' style='padding:7px;background-color: #FFF'>" & Reissuearray(0)("NetFare").ToString() & "</td>"


                    my_table += "<td  style='padding:7px;color:#004b91;background-color: #FFF' >" & Reissuearray(0)("ReissueCharge").ToString() & "</td>"
                    my_table += "</td>"
                    my_table += "</tr>"
                    TotalSale += Convert.ToDouble(salearray(0)("NetFare").ToString()) + Convert.ToDouble(Reissuearray(0)("ReissueCharge").ToString())
                    TotalTicketSale += salearray(0)("TotalTicket").ToString()
                    'TotalReissueTicket += Reissuearray(0)("TotalTicket").ToString()
                    If (refundarray.Length = 0) Then
                        If (Reissuearray.Length = 0) Then
                            netfare = salearray(0)("NetFare").ToString()
                        Else
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + Convert.ToDouble(Reissuearray(0)("ReissueCharge").ToString())) - 0
                            TotalReissueTicket += Reissuearray(0)("TotalTicket").ToString()
                        End If
                        my_table += "<tr style='padding:7px;background-color: #FFFF99;font-family: arial; font-size: 13px; font-weight: bold;'>"
                        my_table += "<td colspan='11' style='padding:7px;color:#004b91' align='right'>NET SALE(" & salearray(0)("VC").ToString() & ")</td>"
                        my_table += "<td>" & netfare & "</td>"

                        my_table += "</td>"
                        my_table += "</tr>"
                    Else
                        If (Reissuearray.Length = 0) Then
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + 0) - Convert.ToDouble(refundarray(0)("RefundFare").ToString())
                        Else
                            netfare = (Convert.ToDouble(salearray(0)("NetFare").ToString()) + Convert.ToDouble(Reissuearray(0)("ReissueCharge").ToString())) - Convert.ToDouble(refundarray(0)("RefundFare").ToString())
                            TotalReissueTicket += Reissuearray(0)("TotalTicket").ToString()
                        End If
                        my_table += "<tr style='padding:7px;background-color: #FFFF99;font-family: arial; font-size: 13px; font-weight: bold;'>"

                        my_table += "<td colspan='11' style='padding:7px;color:#004b91' align='right'>NET SALE(" & salearray(0)("VC").ToString() & ")</td>"
                        my_table += "<td>" & netfare & "</td>"

                        my_table += "</td>"
                        my_table += "</tr>"

                    End If

                End If

                my_table += "</table>"
                my_table += "</fieldset>"
                my_table += "</td>"
                my_table += "</tr>"

            Next

            my_table += "</table>"
            my_table += "</td>"
            my_table += "</tr>"
            my_table += "</table>"
            my_table += "</td>"
            my_table += "</tr>"

            lbl_report.Text = my_table
            lbltotalrefund.Text = TotalRefund
            lbltotalsale.Text = TotalSale
            lbl_NetSale.Text = TotalSale - TotalRefund
            lbl_TotalTicket.Text = TotalTicketSale
            lbl_RefundTicket.Text = TotalRefundTicket
            lbl_ReissueTicket.Text = TotalReissueTicket


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Protected Sub btn_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Try
            Dim filename As String = ""
            filename = "NetMarginReport.doc"
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & "")
            Response.Charset = ""
            Response.ContentType = "application/doc"
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            Div_Export.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
