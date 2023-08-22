Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
''' <summary>
''' Summary description for pnrinfo
''' </summary>
Public Class pnrinfo
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private ds As DataSet
    Private cmd As SqlCommand
    Public Sub New()

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub

    Public Function InsertImportPNR(ByVal PNR As String, ByVal Airline As String, ByVal Dept As String, ByVal dest As String, ByVal DDate As String, ByVal DTime As String, _
     ByVal Adate As String, ByVal ATime As String, ByVal FNo As String, ByVal RBD As String, ByVal Status As String, ByVal BlockPNR As Boolean, ByVal userid As String, ByVal agency As String) As Integer
        Try
            Dim rowsudated As Integer = 0
            con.Open()
            cmd = New SqlCommand("InsertImportPNR", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PNR", PNR)
            cmd.Parameters.AddWithValue("@Airline", Airline)
            cmd.Parameters.AddWithValue("@Dept", Dept)
            cmd.Parameters.AddWithValue("@dest", dest)
            cmd.Parameters.AddWithValue("@DDate", DDate)
            cmd.Parameters.AddWithValue("@DTime", DTime)
            cmd.Parameters.AddWithValue("@Adate", Adate)
            cmd.Parameters.AddWithValue("@ATime", ATime)
            cmd.Parameters.AddWithValue("@FNo", FNo)
            cmd.Parameters.AddWithValue("@RBD", RBD)
            cmd.Parameters.AddWithValue("@userid", userid)
            cmd.Parameters.AddWithValue("@agency", agency)
            cmd.Parameters.AddWithValue("@Status", Status)
            cmd.Parameters.AddWithValue("@BlockPNR", False)
            rowsudated = cmd.ExecuteNonQuery()
            Return rowsudated
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
        End Try
    End Function

    Public Function ImportPNRDetail(ByVal st As String, Optional ByVal pnr As String = "", Optional ByVal id As String = "") As DataSet
        Dim ds As New DataSet()
        If st = "Pending" And pnr = "" And id = "" Then
            adap = New SqlDataAdapter("Select * from ImportPNR where status='" & st & "';Select distinct(pnrno) from ImportPNR where status='" & st & "' ", con)
        ElseIf st = "Pending" And pnr <> "" Then
            adap = New SqlDataAdapter("Select * from ImportPNR where pnrno='" & pnr & "';Select * from Pax_information where pnr_locator='" & pnr & "' ", con)
        ElseIf st = "Processed" And pnr = "" And id <> "" Then
            adap = New SqlDataAdapter("Select * from ImportPNR where status='" & st & "' and Exec_Id='" & id & "';Select distinct(pnrno) from ImportPNR where status='" & st & "' and Exec_Id='" & id & "'", con)
        ElseIf st = "" And pnr = "" And id <> "" Then
            adap = New SqlDataAdapter("Select * from ImportPNR where Exec_Id='" & id & "';Select distinct(pnrno) from ImportPNR where Exec_Id='" & id & "'", con)
        End If
        adap.Fill(ds)
        Return ds

    End Function
    Public Function getImpPnrStatus(ByVal id As String, ByVal usertype As String, ByVal fromDate As String, ByVal ToDate As String, Optional ByVal AgencyId As String = "")
        Dim ds As New DataSet()
        Try
            If usertype = "ADMIN" Then
                If AgencyId = "--Select Agency--" Then
                    adap = New SqlDataAdapter("Select * from ImportPNR where (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc;Select distinct(pnrno) from ImportPNR where (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "'))", con)
                Else
                    adap = New SqlDataAdapter("Select * from ImportPNR where AgentId='" & AgencyId & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc;Select distinct(pnrno) from ImportPNR where AgentId='" & AgencyId & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "'))", con)
                End If
            ElseIf usertype = "EXEC" Then
                If AgencyId = "--Select Agency--" Then
                    adap = New SqlDataAdapter("Select * from ImportPNR where Exec_Id='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc;Select distinct(pnrno),RequestDateTime from ImportPNR where Exec_Id='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc", con)
                Else
                    adap = New SqlDataAdapter("Select * from ImportPNR where AgentId='" & AgencyId & "' and Exec_Id='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc;Select distinct(pnrno),RequestDateTime from ImportPNR where AgentId='" & AgencyId & "' and Exec_Id='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc", con)
                End If
            ElseIf usertype = "AGENT" Then
                adap = New SqlDataAdapter("Select * from ImportPNR where AgentId='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "')) order by RequestDateTime desc;Select distinct(pnrno) from ImportPNR where AgentId='" & id & "' and (requestDateTime Between CONVERT(datetime,'" & fromDate & "') AND CONVERT(datetime,'" & ToDate & "'))", con)
            End If
            adap.Fill(ds)
        Catch ex As Exception
        End Try
        Return ds
    End Function
    Public Function getImpPnrStatusByAgencyName(ByVal id As String, ByVal usertype As String, ByVal AgencyId As String)
        Dim ds As New DataSet()
        Try
            If usertype = "ADMIN" Then
               
                adap = New SqlDataAdapter("Select * from ImportPNR where AgentId='" & AgencyId & "'  order by RequestDateTime desc;Select distinct(pnrno) from ImportPNR where AgentId='" & AgencyId & "' ", con)

            ElseIf usertype = "EXEC" Then
                adap = New SqlDataAdapter("Select * from ImportPNR where Exec_Id='" & id & "' order by RequestDateTime desc;Select distinct(pnrno),RequestDateTime from ImportPNR where Exec_Id='" & id & "'  order by RequestDateTime desc", con)

             End If
            adap.Fill(ds)
        Catch ex As Exception
        End Try
        Return ds
    End Function
    Public Function UpdateImpPnr(ByVal pnr As String, ByVal st As String, ByVal id As String, ByVal ImportDate As String, Optional ByVal ESCharge As String = "") As Integer
        Dim cmd As SqlCommand
        con.Open()
        Try
            If ImportDate = "Accept" Then
                If ESCharge = "" Then
                    cmd = New SqlCommand("update ImportPNR set status='" & st & "',Exec_ID='" & id & "',AcceptedDate='" & System.DateTime.Now & "'  where pnrno='" & pnr & "'", con)
                Else
                    cmd = New SqlCommand("update ImportPNR set status='" & st & "',Exec_ID='" & id & "',AcceptedDate='" & System.DateTime.Now & "' ,ESCharge='" & ESCharge & "',AcceptedDate='" & System.DateTime.Now & "' where pnrno='" & pnr & "'", con)
                End If

            End If
            If ImportDate = "Update" Then
                If ESCharge = "" Then
                    cmd = New SqlCommand("update ImportPNR set status='" & st & "',Exec_ID='" & id & "',UpdatedDate='" & System.DateTime.Now & "'  where pnrno='" & pnr & "'", con)
                Else
                    cmd = New SqlCommand("update ImportPNR set status='" & st & "',Exec_ID='" & id & "',UpdatedDate='" & System.DateTime.Now & "' ,ESCharge='" & ESCharge & "' where pnrno='" & pnr & "'", con)
                End If

            End If
            
            Return cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try

    End Function

    Public Function insertTransReportwithESCharge(ByVal pnr As String, ByVal AgName As String, ByVal id As String, ByVal ESCharge As String, ByVal AvlBal As String, ByVal remark As String) As Integer
        Dim cmd As SqlCommand
        con.Open()
        Try
            cmd = New SqlCommand("insert into trans_report(user_id,pnr_locator,booking_date,Aval_Bal,Debit,Rm,Ag_Name)values('" & id & "','" & pnr & "','" & CDate(Now()) & "','" & AvlBal & "','" & ESCharge & "','" & remark & "','" & AgName & "')", con)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try

    End Function
    Public Function InsertPaxInfo(ByVal pnr As String, ByVal tittle As String, ByVal fname As String, ByVal lname As String) As Integer
        Try

            Dim rowsupdated As Integer = 0
            con.Open()
            cmd = New SqlCommand("InsertPax", con)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pnrlocator", pnr)
            cmd.Parameters.AddWithValue("@tittle", tittle)
            cmd.Parameters.AddWithValue("@first_name", fname)
            cmd.Parameters.AddWithValue("@last_name", lname)
            cmd.Parameters.AddWithValue("@paxtype", "")
            cmd.Parameters.AddWithValue("@ticketno", "")
            rowsupdated = cmd.ExecuteNonQuery()
            con.Close()
            Return rowsupdated
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
        End Try

        Return 0

    End Function

    Public Function UpdateTktno(ByVal pnr As String, ByVal paxname As String, ByVal paxtype As String, ByVal tktno As String) As Integer
        Try
            con.Open()
            Dim cnt As Integer = 0
            cmd = New SqlCommand("UpdatePaxInfofromImportPnr", con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim pax() As String = Split(paxname, " ")
            cmd.Parameters.AddWithValue("@pnr", pnr)
            cmd.Parameters.AddWithValue("@title", pax(0).ToString)
            cmd.Parameters.AddWithValue("@fname", pax(1).ToString)
            cmd.Parameters.AddWithValue("@lname", pax(2).ToString)
            cmd.Parameters.AddWithValue("@paxtype", paxtype)
            cmd.Parameters.AddWithValue("@tktno", tktno)
            cnt = cmd.ExecuteNonQuery()
            Return cnt
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
        End Try

    End Function

    Public Function cchkPnr(ByVal pnr As String) As Integer
        Try
            con.Open()
            cmd = New SqlCommand("CheckPnr", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PNR", pnr)
            cmd.Parameters.AddWithValue("@st", 1)
            Return cmd.ExecuteScalar()
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
        End Try
    End Function

    Public Function insertFlightDetails(ByVal gdsPnr As String, ByVal ds As DataSet, ByVal total_no_adult As String, ByVal total_no_child As String, ByVal total_no_infant As String, ByVal total_adult As String, ByVal adult_tax As String, ByVal total_child As String, _
     ByVal child_tax As String, ByVal total_infant As String, ByVal infant_tax As String, ByVal total_booking_cost As String, ByVal pnr_status As String, _
      ByVal Distr As String, ByVal Air_PNR As String, _
     ByVal T_Fee As String, ByVal S_Tax As String, ByVal Exec As String, ByVal VC As String) As Integer
        Try
            con.Open()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                cmd = New SqlCommand("InsBkgDtlsFromImpPnr", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@GDSPNR", gdsPnr)
                cmd.Parameters.AddWithValue("@sector", ds.Tables(0).Rows(0)("Departure").ToString & " - " & ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("Destination").ToString)
                cmd.Parameters.AddWithValue("@D_Date", ds.Tables(0).Rows(i)("Departdate").ToString)
                cmd.Parameters.AddWithValue("@Dept", ds.Tables(0).Rows(i)("Departure").ToString)
                cmd.Parameters.AddWithValue("@Dest", ds.Tables(0).Rows(i)("Destination").ToString)
                cmd.Parameters.AddWithValue("@DepatDate", ds.Tables(0).Rows(i)("Departdate").ToString)
                cmd.Parameters.AddWithValue("@DTime", ds.Tables(0).Rows(i)("DepartTime").ToString)
                cmd.Parameters.AddWithValue("@ADate", ds.Tables(0).Rows(i)("ArrivalDate").ToString)
                cmd.Parameters.AddWithValue("@Atime", ds.Tables(0).Rows(i)("ArrivalTime").ToString)
                cmd.Parameters.AddWithValue("@FlightNo", ds.Tables(0).Rows(i)("FlightNo").ToString)
                cmd.Parameters.AddWithValue("@Air", ds.Tables(0).Rows(i)("Airline").ToString)
                cmd.Parameters.AddWithValue("@BDate", DateTime.Now)
                cmd.Parameters.AddWithValue("@NOA", total_no_adult)
                cmd.Parameters.AddWithValue("@NOC", total_no_child)
                cmd.Parameters.AddWithValue("@NOI", total_no_infant)
                cmd.Parameters.AddWithValue("@ATotal", total_adult)
                cmd.Parameters.AddWithValue("@ATax", adult_tax)
                cmd.Parameters.AddWithValue("@CTotal", total_child)
                cmd.Parameters.AddWithValue("@CTax", child_tax)
                cmd.Parameters.AddWithValue("@ITotal", total_infant)
                cmd.Parameters.AddWithValue("@ITax", infant_tax)
                cmd.Parameters.AddWithValue("@TotalBCost", total_booking_cost)
                cmd.Parameters.AddWithValue("@PNRStatus", pnr_status)
                cmd.Parameters.AddWithValue("@UID", ds.Tables(0).Rows(i)("agentId").ToString)
                cmd.Parameters.AddWithValue("@FName", ds.Tables(1).Rows(i)("first_name").ToString)
                cmd.Parameters.AddWithValue("@LName", ds.Tables(1).Rows(i)("last_name").ToString)
                cmd.Parameters.AddWithValue("@MobNo", "")
                cmd.Parameters.AddWithValue("@Email", "")
                cmd.Parameters.AddWithValue("@Distr", Distr)
                cmd.Parameters.AddWithValue("@AirPNR", Air_PNR)
                cmd.Parameters.AddWithValue("@class", ds.Tables(0).Rows(i)("RDB").ToString)
                cmd.Parameters.AddWithValue("@TFee", T_Fee)
                cmd.Parameters.AddWithValue("@Stax", S_Tax)
                cmd.Parameters.AddWithValue("@Exec", Exec)
                cmd.Parameters.AddWithValue("@AgName", ds.Tables(0).Rows(i)("Ag_Name").ToString)
                cmd.Parameters.AddWithValue("@PType", "CL")
                cmd.Parameters.AddWithValue("@AMRK", "0")
                cmd.Parameters.AddWithValue("@VC", VC)
                cmd.ExecuteNonQuery()
            Next
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
        End Try

    End Function

    Public Function getCalcDetails(ByVal airline As String, Optional ByVal agType As String = "") As DataTable
        Dim dt As New DataTable
        con.Open()
        Try
            If agType = "" Then
                adap = New SqlDataAdapter("select * from airline_pa where code='" & airline & "'", con)
            ElseIf agType <> "" Then
                adap = New SqlDataAdapter("select * from agent_CD where Grade='" & agType & "' and airline='" & airline & "' ", con)
            End If
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            con.Close()
        End Try
        Return dt
    End Function

    Public Function getAgentDtls(ByVal userid As String) As DataTable
        Dim dt As New DataTable
        Try
            con.Open()
            adap = New SqlDataAdapter("select * from new_regs where user_id='" & userid & "'", con)
            adap.Fill(dt)
        Catch ex As Exception
        Finally
            con.Close()
        End Try
        Return dt
    End Function

    Public Function InsertTransReport(ByVal UID As String, ByVal GDSPNR As String, ByVal PNRStatus As String, ByVal BDate As DateTime, ByVal Credit As String, ByVal Discount As String, _
     ByVal AvalBal As String, ByVal CB As String, ByVal Debit As String, ByVal TDS As String, ByVal Sector As String, ByVal FName As String, _
     ByVal FlightNo As String, ByVal MobileNo As String, ByVal Email As String, ByVal Rm As String, ByVal PType As String, ByVal AgentMRK As String, _
     ByVal Dist As String, ByVal FareAFTDIS As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TotBF As String, _
     ByVal Ag_Name As String) As Integer



        con.Open()
        Try
            cmd = New SqlCommand("InsertTransReportOneWay", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UID", UID)
            cmd.Parameters.AddWithValue("@GDSPNR", GDSPNR)
            cmd.Parameters.AddWithValue("@PNRStatus", PNRStatus)
            cmd.Parameters.AddWithValue("@BDate", BDate)
            cmd.Parameters.AddWithValue("@Credit", Credit)
            cmd.Parameters.AddWithValue("@Discount", Discount)
            cmd.Parameters.AddWithValue("@AvalBal", AvalBal)
            cmd.Parameters.AddWithValue("@CB", CB)
            cmd.Parameters.AddWithValue("@Debit", Debit)
            cmd.Parameters.AddWithValue("@TDS", TDS)
            cmd.Parameters.AddWithValue("@Sector", Sector)
            cmd.Parameters.AddWithValue("@FName", FName)
            cmd.Parameters.AddWithValue("@FlightNo", FlightNo)
            cmd.Parameters.AddWithValue("@MobileNo", MobileNo)
            cmd.Parameters.AddWithValue("@Email", Email)
            cmd.Parameters.AddWithValue("@Rm", Rm)
            cmd.Parameters.AddWithValue("@PType", PType)
            cmd.Parameters.AddWithValue("@AgentMRK", AgentMRK)
            cmd.Parameters.AddWithValue("@Dist", Dist)
            cmd.Parameters.AddWithValue("@FareAFTDIS", FareAFTDIS)
            cmd.Parameters.AddWithValue("@Tax", Tax)
            cmd.Parameters.AddWithValue("@YQ", YQ)
            cmd.Parameters.AddWithValue("@STax", STax)
            cmd.Parameters.AddWithValue("@TotBF", TotBF)
            cmd.Parameters.AddWithValue("@Ag_Name", Ag_Name)
            cmd.Parameters.AddWithValue("@SFDis", "")
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            con.Close()

        End Try

    End Function
    Public Function getImportPNRDetails(ByVal pnr As String) As DataSet
        Dim ds As New DataSet
        adap = New SqlDataAdapter("select * from dbo.ImportPNR where PNRNo='" & pnr & "'", con)
        adap.Fill(ds)
        Return ds
    End Function
    'New Code
    Public Function AgentComment(ByVal pnr As String, ByVal comment As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ImportPNR SET  UpdRemark='" & comment & "' where PNRNo='" & pnr & "'  ", con)
        adap.Fill(ds)
        Return ds
    End Function
    Public Function cchkPnrIntl(ByVal pnr As String) As Integer
        Try
            con.Open()
            cmd = New SqlCommand("CheckPnr", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PNR", pnr)
            cmd.Parameters.AddWithValue("@st", 2)
            Return cmd.ExecuteScalar()
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
        End Try
    End Function


    Public Function AgentCommentIntl(ByVal OrderId As String, ByVal comment As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ImportPNRIntl SET  UpdRemark='" & comment & "' where OrderId='" & OrderId & "'  ", con)
        adap.Fill(ds)
        Return ds
    End Function

End Class