Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsCorporate
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable


    Dim ds As New DataSet
    Dim dt As New DataTable
    Dim sql As New SqlTransaction
   
    Private I As New Invoice()
    Dim ST As New SqlTransaction()
    Dim Conn As New SqlConnection
    Public Function GetManagementFeeSrvTax(ByVal Type As String, ByVal Airline As String, ByVal Basic As Decimal, ByVal YQ As Decimal, ByVal Trip As String, ByVal TOTAL As Decimal) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@TYPE", Type)
        paramHashtable.Add("@AIRLINE", Airline)
        paramHashtable.Add("@BASIC", Basic)
        paramHashtable.Add("@YQ", YQ)
        paramHashtable.Add("@TRIP", Trip)
        paramHashtable.Add("@TOTAL", TOTAL)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CORP_CALC_MANAGEMENTFEE", 3)
    End Function
    Public Sub calcFareCorp(ByVal tax() As String, ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal admrk As Integer, ByVal agmrk As Integer, ByVal dismrk As Integer, ByVal mgtfee As Integer, ByVal comm As Integer, ByVal srvtax As Integer, ByVal cb As Integer, ByVal tds As Integer, ByVal vc As String, ByVal trip As String)
        paramHashtable.Clear()
        Dim tax1() As String
        Dim YQ As Integer = 0, YR As Integer = 0, WO As Integer = 0, OT As Integer = 0, QTax As Integer = 0, totTax As Double = 0
        Dim totFare As Integer = 0, netFare As Integer = 0, TF As Integer = 0, TF1 As Double = 0, SrvTax1 As Double = 0
        Dim JN As Double = 0, K3 As Double = 0, F2 As Double = 0, G1 As Double = 0, YM As Double = 0
        Try
            For i As Integer = 0 To tax.Length - 2
                If InStr(tax(i), "YQ") Then
                    tax1 = tax(i).Split(":")
                    YQ = YQ + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "YR") Then
                    tax1 = tax(i).Split(":")
                    YR = YR + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "WO") Then
                    tax1 = tax(i).Split(":")
                    WO = WO + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "JN") Then
                    tax1 = tax(i).Split(":")
                    JN = JN + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "K3") Then
                    tax1 = tax(i).Split(":")
                    K3 = K3 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "F2") Then
                    tax1 = tax(i).Split(":")
                    F2 = F2 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "G1") Then
                    tax1 = tax(i).Split(":")
                    G1 = G1 + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "YM") Then
                    tax1 = tax(i).Split(":")
                    YM = YM + Convert.ToInt32(tax1(1))
                ElseIf InStr(tax(i), "Q") Then
                    tax1 = tax(i).Split(":")
                    QTax = QTax + Convert.ToInt32(tax1(1))
                Else
                    tax1 = tax(i).Split(":")
                    OT = OT + Convert.ToInt32(tax1(1))
                End If
            Next
            TF = 0
            totTax = YQ + YR + WO + OT + JN + K3 + F2 + G1 + YM
            totFare = basefare + totTax + srvtax + TF + admrk + mgtfee
            netFare = (totFare + tds) - comm
        Catch ex As Exception

        End Try

        paramHashtable.Clear()
        paramHashtable.Add("@OrderID", trackid)
        paramHashtable.Add("@BaseFare", basefare)
        paramHashtable.Add("@YQ", YQ)
        paramHashtable.Add("@YR", YR)
        paramHashtable.Add("@WO", WO)
        paramHashtable.Add("@OT", OT)
        paramHashtable.Add("@Qtax", QTax)
        paramHashtable.Add("@TotalTax", totTax)
        paramHashtable.Add("@TotalFare", totFare)
        paramHashtable.Add("@ServiceTax", srvtax)
        paramHashtable.Add("@TranFee", TF)
        paramHashtable.Add("@AdminMrk", admrk)
        paramHashtable.Add("@AgentMrk", agmrk)
        paramHashtable.Add("@DistrMrk", dismrk)
        paramHashtable.Add("@TotalDiscount", 0)
        paramHashtable.Add("@PLb", 0)
        paramHashtable.Add("@Discount", 0)
        paramHashtable.Add("@CashBack", 0)
        paramHashtable.Add("@Tds", tds)
        paramHashtable.Add("@TdsOn", 0)
        paramHashtable.Add("@TotalAfterDis", netFare)
        paramHashtable.Add("@PaxType", paxtype)
        paramHashtable.Add("@UpdateDate", DateTime.Now)
        paramHashtable.Add("@MgtFee", mgtfee)
        paramHashtable.Add("@JN", JN)
        paramHashtable.Add("@K3", K3)
        paramHashtable.Add("@F2", F2)
        paramHashtable.Add("@G1", G1)
        paramHashtable.Add("@YM", YM)
        objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertFltFareDetails", 1)
    End Sub
    Public Function Get_Corp_Project_Details_By_AgentID(ByVal agentId As String, ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AgentID", agentId.Trim())
        paramHashtable.Add("@UserType", Type.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "USP_Get_Corp_Project_Details_By_AgentID", 3)
    End Function
    Public Function Get_Corp_BookedBy(ByVal agentId As String, ByVal Type As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@AGENTID", agentId.Trim())
        paramHashtable.Add("@TYPE", Type.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CORP_GETBOOKEDBY", 3)
    End Function

    Public Function GenerateBillNoCorp(ByVal type As String) As String
        paramHashtable.Clear()
        paramHashtable.Add("@type", type.Trim())
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "SP_CORP_GENERATE_BILLNO", 2)
        'Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        'Dim result As String = ""
        'Try
        '    Dim cmd As New SqlCommand("Sp_Generate_BillNoCorp", con)
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.Parameters.AddWithValue("@type", type)

        '    con.Open()
        '    result = cmd.ExecuteScalar()

        '    con.Close()
        'Catch ex As SqlException
        '    'throw ex;
        '    ' ex.ToString();
        '    result = ""
        'Finally
        '    con.Dispose()
        'End Try
        'Return result
    End Function
    
    

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
                If Not IsDBNull(dtAAdd.Rows(0)("IsCorp")) Then
                    If Convert.ToBoolean(dtAAdd.Rows(0)("IsCorp")) Then
                        MgtFeeVisibleStatus = True
                        IsCorp = True

                        'mgtFee = If(IsDBNull(dt.Rows(0)("MgtFee")), 0, Convert.ToDouble(dt.Rows(0)("MgtFee")))


                    End If

                End If
                Dim my_table As String = ""
                my_table += "   <table cellpadding='0' cellspacing='0' width='900px' align='center'> <tr>"
                Dim dtaddress As New DataTable
                Dim STDom As New SqlTransactionDom
                If (IsCorp = True) Then


                    dtaddress = STDom.GetCompanyAddress(ADDRESS.CORP.ToString().Trim()).Tables(0)
                    my_table += "<td id='td_corp' runat='server'><table border='0' cellpadding='0' cellspacing='0' align='center'>"
                    my_table += " <tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold; color: #000000'>"
                    my_table += " " & dtaddress.Rows(0)("COMPANYNAME") & " </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    my_table += " " & dtaddress.Rows(0)("COMPANYADDRESS") & " </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    my_table += " &nbsp;Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "  </td></tr><tr><td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    my_table += "Email:          " & dtaddress.Rows(0)("EMAIL") & " </td></tr></table></td>"

                Else
                    dtaddress = STDom.GetCompanyAddress(ADDRESS.FWU.ToString().Trim()).Tables(0)
                    my_table += " <td id='td_notcorp' runat='server'>"
                    my_table += " <table cellpadding='0' cellspacing='0' width='900px' align='center'>"
                    my_table += "<tr>    <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 25px; font-weight: bold;   color: #000000'>"
                    my_table += "    " & dtaddress.Rows(0)("COMPANYNAME") & "    </td>    </tr>    <tr>    <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    my_table += " " & dtaddress.Rows(0)("COMPANYADDRESS") & "     </td>     </tr>    <tr>  <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;  color: #666666'>"
                    my_table += " &nbsp;Ph " & dtaddress.Rows(0)("PHONENO") & " Fax : " & dtaddress.Rows(0)("FAX") & "   </td>  </tr>  <tr> <td align='center' style='font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #666666'>"
                    my_table += "   Email:          " & dtaddress.Rows(0)("EMAIL") & " </td> </tr></table></td>"

                End If

                my_table += "</tr>"





                my_table += "  <tr> <td style='height: 30px'> </td> </tr> <tr>"
                my_table += "   <td style='background-color:#ccc; padding-left:5px; font-size: 14px;' height='30px'>" & If(billNo = "", "<strong>Invoice No.&nbsp;:&nbsp;</strong>" & orderId, "<strong>BILL NO.&nbsp;:&nbsp;</strong>" & billNo)

                my_table += " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                my_table += If(projID <> "", "<strong>PROJECT ID&nbsp;:&nbsp;</strong>" & projID, "")

                my_table += " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                my_table += If(bookedBy <> "", "<strong>BOOKED BY&nbsp;:&nbsp;</strong>" & bookedBy, "") & " </td> </tr>"





                Dim dtAgent As DataTable = sql.GetAgencyDetails(dt.Rows(0)("AgentId").ToString()).Tables(0)

                my_table += "<tr> <td height='90px' style='border: thin solid #999999'> <table border='0' cellpadding='0' cellspacing='0' width='100%'>"
                my_table += "  <tr> <td rowspan='4' width='200px' class='SubHeading'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address =&gt;</td>"
                my_table += "  <td >" & dtAgent.Rows(0)("Agency_Name").ToString() & "</td></tr> <tr><td >" & dtAgent.Rows(0)("Address").ToString() & "</td></tr>"
                my_table += "  <tr><td > <table border='0' cellpadding='0' cellspacing='0' width='100%'><tr> <td >" & dtAgent.Rows(0)("City").ToString & "," & dtAgent.Rows(0)("Zipcode").ToString & "," & dtAgent.Rows(0)("State").ToString & " </td>"
                my_table += "   </tr> </table> </td></tr> <tr> <td>" & dtAgent.Rows(0)("Country").ToString() & "</td></tr> </table>"
                my_table += " </td></tr>   <tr>  <td></td> </tr>"










                my_table += " <tr><td>"
                my_table += "<table width='100%' border='1' style='border: thin solid #999999' cellspacing='0' style='border=collapse:collapse' cellpadding='0' align='center'>"

                my_table += "<tr>"
                If (IsCorp = True) Then

                    my_table += "<td  class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>CERATED DATE</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>PAX NAME</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TICKET NO</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>SECTORS</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>DEP DATE</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>AIRLINE</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>FLIGHT NO</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>FARE</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TAX</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>TOTAL</td>"
                Else
                    my_table += "<td  class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Pax</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'  align='center'>Ticket No.</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>PNR</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>APNR</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Sectors</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Create Date</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Fare</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Tax</td>"
                    my_table += "<td class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;' align='center'>Total</td>"

                End If

                my_table += "<tr>"
                For Each dr As DataRow In dt.Rows
                    my_table += "<tr class='InvoiceText' align='center'>"
                    If (IsCorp = True) Then
                        If (ReissueId = "") Then
                            my_table += "<td>" & dr("Createdate").ToString() & " </td>"
                            my_table += "<td>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td>" & dr("TicketNumber").ToString() & "</td>"
                            my_table += "<td>" & dr("Sector").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()
                            my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"
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
                            my_table += "<td>" & dtflt.Rows(0)("DepDate").ToString() & "</td>"
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
                            my_table += "<td>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td>" & dr("TicketNumber").ToString() & ".</td>"
                            my_table += "<td>" & dr("GDSPnr").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()
                            my_table += "<td>" & dr("AirlinePnr").ToString() & "</td>"
                            my_table += "<td>" & dr("Sector").ToString() & "</td>"
                            my_table += "<td>" & dr("Createdate").ToString() & " </td>"
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


                            my_table += "<td>" & dr("title").ToString() & " " & dr("fname").ToString() & "" & dr("mname") & " " & dr("lname") & "</td>"
                            my_table += "<td>" & dr("TicketNumber").ToString() & ".</td>"
                            my_table += "<td>" & dr("GDSPnr").ToString() & "</td>"
                            gdspnr = dr("GDSPnr").ToString()
                            my_table += "<td>" & dr("AirlinePnr").ToString() & "</td>"
                            my_table += "<td>" & dr("Sector").ToString() & "</td>"
                            my_table += "<td>" & dr("Createdate").ToString() & " </td>"
                            If dr("paxtype").ToString() = "ADT" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & "</td>"
                                adult += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                adulttax += 0
                            End If
                            If dr("paxtype").ToString() = "CHD" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & "</td>"
                                child += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                childtax += 0
                            End If
                            If dr("paxtype").ToString() = "INF" Then
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & " </td>"
                                my_table += "<td>0</td>"
                                my_table += "<td>" & dr("ResuFareDiff").ToString() & "</td>"
                                infant += Convert.ToDouble(dr("ResuFareDiff").ToString())
                                infanttax += 0
                            End If
                        End If
                    End If


                    my_table += "</tr>"
                    If (ReissueId = "") Then
                        total = (Convert.ToDouble(dr("basefare").ToString()) + Convert.ToDouble(dr("totaltax").ToString()))

                    Else
                        total = (Convert.ToDouble(dr("ResuFareDiff").ToString()))

                    End If


                Next

                totalfare = adult + child + infant
                totalfaretax = adulttax + childtax + infanttax
                total = totalfare + totalfaretax


                my_table += "<tr   style=' padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>"
                my_table += "<td></td>"
                'my_table += "<td></td>"
                my_table += "<td></td>"
                my_table += "<td></td>"
                my_table += "<td></td>"
                my_table += "<td></td>"
                If (IsCorp = True) Then
                    my_table += "<td></td>"
                End If
                my_table += "<td align='center' >Total</td>"
                my_table += "<td align='center'>" & totalfare & "</td>"
                my_table += "<td align='center'>" & totalfaretax & "</td>"
                my_table += "<td align='center'>" & total & "</td>"

                my_table += "</tr>"
                Dim srvTax As Double = 0, tf As Double = 0, admMrk As Double = 0, agMrk As Double = 0, totDis As Double = 0

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
                    If (IsCorp = True) Then
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Management Fee</td>"
                        my_table += "<td  align='center'>" & mgtFee.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Service Tax</td>"
                        my_table += "<td align='center'>" & srvTax.ToString & "</td>" ''tf.ToString
                        my_table += "</tr>"
                        TotalST = total + srvTax ' + admMrk '+ tf + agentmrk

                    Else
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Service Tax</td>"
                        my_table += "<td  align='center'>" & srvTax.ToString & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Transaction Fee</td>"
                        my_table += "<td align='center'>0</td>" ''tf.ToString
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Transaction Charge</td>"
                        my_table += "<td align='center'>" & admMrk.ToString & "</td>"
                        TotalST = total + srvTax + admMrk '+ tf + agentmrk
                        my_table += "</tr>"
                        my_table += "<tr style='font-family: arial;font-size: 12px;font-weight: bold; color: #000099; line-height: 25px;' >"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2' style='padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>&nbsp;Total(Inc. STax & T.F.)</td>"
                        my_table += "<td  align='center' style='padding: 4px 2px;color: #000000; background-color: #FFFF66; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91; line-height: 20px;'>" & TotalST & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Less Discount</td>"
                        my_table += "<td  align='center'>" & (totDis - (CB + tf)).ToString() & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2' >&nbsp;Less Cash Back</td>"
                        my_table += "<td  align='center'>" & CB.ToString & "</td>"

                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice' >"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Add TDS</td>"
                        my_table += "<td  align='center'>" & TDS.ToString & "</td>"
                        my_table += "</tr>"
                    End If


                Else

                    If (IsCorp = True) Then
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'>&nbsp;Reissue Charge</td>"
                        my_table += "<td  align='center'>" & Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge")) & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='2'></td>"
                        my_table += "<td align='center'></td>" ''tf.ToString
                        my_table += "</tr>"
                        TotalST = total + Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge")) ' + admMrk '+ tf + agentmrk

                    Else
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='1'>&nbsp;Reissue Charge</td>"
                        my_table += "<td  align='center'>" & dt.Rows(0)("ResuCharge").ToString() & "</td>"
                        my_table += "</tr>"
                        my_table += "<tr class='TransInvoice'>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td></td>"
                        my_table += "<td colspan='1'>&nbsp;Service Charge</td>"
                        my_table += "<td align='center'>" & dt.Rows(0)("ResuServiseCharge").ToString() & "</td>" ''tf.ToString
                        my_table += "</tr>"
                        TotalST = total + Convert.ToDouble(dt.Rows(0)("ResuCharge")) + Convert.ToDouble(dt.Rows(0)("ResuServiseCharge"))
                    End If

                End If






                If (ReissueId = "") Then
                    GrandTotal = (TotalST + TDS + mgtFee) - (totDis - tf)

                Else
                    GrandTotal = TotalST

                End If

                Dim GTInWord As New NumToWord.NumberToWord()
                my_table += "<tr class='Proxy' style='padding: 4px 2px; color: #fff; background-color: #004b91; font-size: 12px;font-weight: bold; font-family: arial, Helvetica, sans-serif; border-left-color: #004b91;'>"
                my_table += "<td align='center'>Amount in word</td>"
                my_table += "<td colspan='5'>&nbsp;" & GTInWord.AmtInWord(Convert.ToDecimal(GrandTotal)) & "</td>"
                If (IsCorp = True) Then
                    my_table += "<td colspan='3'>&nbsp;Grand Total</td>"
                Else
                    my_table += "<td colspan='2'>&nbsp;Grand Total</td>"
                End If

                my_table += "<td align='center'>" & GrandTotal & "</td>"

                my_table += "</tr>"

                my_table += "</table></td></tr>"


                my_table += "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>"
                my_table += "    <tr>  <td>  E &amp; O.E : Payment to be made to the cashier and print Official Receipt must to be Obtained. </td>"
                my_table += "  </tr>  <tr> <td class='MsgText'> CASH & CHEQUE : All Cheques/Demand Drafts in Payment of bills must be crossed 'A/c"
                my_table += "  Payee Only and all drawn in favour of . </td>  </tr> <tr>"
                my_table += " <td class='MsgText'> LATE PAYMENT : If bill is not paid within 15 days,Interest @2 4% will be charged."
                my_table += " </td> </tr>  <tr> <td class='MsgText'>  DISPUTES :All dispute will be subject to Delhi Jurisdiction."
                my_table += " </td>  </tr>  <tr> <td class='MsgText'>  SERVICE TAX NO :</td>"
                my_table += " </tr>  <tr>  <td class='MsgText'>  This is Computer generated invoice,hence no signature required  </td>  </tr>"

                my_table += "</table>"

                result = my_table
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

        Return result
    End Function



    Public Function GetMarkUp(ByVal AgentID As String, ByVal distrubid As String, ByVal Trip As String, ByVal typeId As String) As DataSet
        paramHashtable.Clear()
        paramHashtable.Add("@trip", Trip)
        paramHashtable.Add("@agid", AgentID)
        paramHashtable.Add("@distrid", distrubid)
        paramHashtable.Add("@idtype", typeId)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetMarkup", 3)
    End Function

    Public Function CalcMarkup(ByVal Mrkdt As DataTable, ByVal VC As String, ByVal fare As Double, ByVal Trip As String) As Double
        Dim airMrkArray As Array
        Dim mrkamt As Double = 0
        Try
            airMrkArray = Mrkdt.Select("AirlineCode='" & VC & "'", "")
            If airMrkArray.Length > 0 Then
                If Trip = "I" Then
                    If (airMrkArray(0))("MarkupType") = "P" Then
                        mrkamt = Math.Round((fare * (airMrkArray(0))("MarkupValue")) / 100, 0)
                    ElseIf (airMrkArray(0))("MarkupType") = "F" Then
                        mrkamt = (airMrkArray(0))("MarkupValue")
                    End If
                Else
                    mrkamt = (airMrkArray(0))("MarkUp")
                End If
            Else
                mrkamt = 0
            End If
        Catch ex As Exception
            mrkamt = 0
        End Try
        Return mrkamt
    End Function

End Class
