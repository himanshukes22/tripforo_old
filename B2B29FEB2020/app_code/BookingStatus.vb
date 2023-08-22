Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

''' <summary>
''' Summary description for BookingStatus
''' </summary>
Public Class BookingStatus
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private cmd As SqlCommand
    
    Public Sub New(ByVal conn As String)
        con = New SqlConnection(conn)
    End Sub
    Public Function GetBookingDetails(ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator, flight_booking.sector, flight_booking.departure_date, flight_booking.departure_time, flight_booking.arrival_time,flight_booking.flight_no, flight_booking.airline, flight_booking.booking_date, flight_booking.total_booking_cost, Trans_Report.Fare_AFTDIS FROM flight_booking INNER JOIN Trans_Report ON flight_booking.pnr_locator = Trans_Report.pnr_locator where  flight_booking.user_id='" & userid & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function WithPnrDetails(ByVal pnr As String, ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("GetPNRDetails", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@pnr", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@userid", userid)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxAdultInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='ADT' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function Status(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from reissue where pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function CountRow(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("CountRow", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@pnr", pnr)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function TktCount(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select Count(tkt_no) from dbo.pax_information where pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function info1(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator,flight_booking.departure, flight_booking.destination, flight_booking.dept_date, flight_booking.arrival_time,flight_booking.flight_no,flight_booking.airline, flight_booking.booking_date, flight_booking.total_no_adult, flight_booking.total_no_child,flight_booking.total_no_infant, flight_booking.total_adult, flight_booking.adult_tax, flight_booking.total_child, flight_booking.child_tax,flight_booking.total_infant, flight_booking.infant_tax, flight_booking.total_booking_cost, flight_booking.VC, Trans_Report.Fare_AFTDIS,Trans_Report.YQ FROM flight_booking INNER JOIN Trans_Report ON flight_booking.pnr_locator = Trans_Report.pnr_locator AND flight_booking.pnr_status = Trans_Report.pnr_status where flight_booking.pnr_status='ticketed' and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function info2(ByVal airlinecode As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT Airline_Pa.TF, Airline_Pa.STax,New_Regs.Agent_Type,New_Regs.agency_name,New_Regs.User_Id from flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id INNER JOIN Airline_Pa ON flight_booking.VC = Airline_Pa.Code where Airline_Pa.Code='" & airlinecode & "' and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function info3(ByVal code As String, ByVal type As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT Agent_CD.CB, Agent_CD.Dis_YQ, Agent_CD.Dis_B_YQ,agent_cd.dis FROM Agent_CD INNER JOIN flight_booking ON Agent_CD.Airline = flight_booking.VC WHERE  Agent_CD.Airline = '" & code & "' and Agent_cd.grade='" & type & "' and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PassengerInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select pnr_locator,Tkt_No,Sector,FlightNo,departure,destination,pax_fname,pax_lname,pax_type,departure_date,UserID,Agency_Name,TotalFare,TotalFareAfterDiscount,Status from dbo.ReIssue where pnr_locator='" & pnr & "' and Status='Null'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxChildInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='CHD' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxInfantInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='INF' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function ReIUpdReq(ByVal remark As String, ByVal tktno As String, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.ReIssue set Status='Pending',RegardingIssue=@ReIRemark where tkt_no=@tktno and pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@ReIRemark", remark)
        cmd.Parameters.AddWithValue("@tktno", tktno)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function PassengerStatusInfo(ByVal st As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.ReIssue where Status='" & st & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function AcceptPassengerReissue(ByVal st As String, Optional ByVal pnr As String = "") As DataSet
        If pnr = "" Then
            adap = New SqlDataAdapter("select * from dbo.ReIssue where Status='" & st & "' order by Submitdate desc", con)
        Else
            adap = New SqlDataAdapter("select * from dbo.ReIssue where Status='" & st & "' and pnr_locator='" & pnr & "' order by Submitdate desc", con)
        End If
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function ReissueDetais(ByVal id As String, ByVal usertype As String, ByVal fromdate As String, ByVal todate As String) As DataSet
        If usertype = "ADMIN" Then
            adap = New SqlDataAdapter("select * from dbo.ReIssue where  status<>'NULL' and  SubmitDate>='" & fromdate & "' and SubmitDate<='" & todate & "' order by SubmitDate desc", con)
        ElseIf usertype = "EXEC" Then
            adap = New SqlDataAdapter("select * from dbo.ReIssue where ExecutiveID='" & id & "' and status<>'NULL' and  SubmitDate>='" & fromdate & "' and SubmitDate<='" & todate & "' order by SubmitDate desc", con)
        ElseIf usertype = "AGENT" Then
            adap = New SqlDataAdapter("select * from dbo.ReIssue where UserID='" & id & "' and status<>'NULL' and SubmitDate>='" & fromdate & "' and SubmitDate<='" & todate & "' order by SubmitDate desc", con)
        End If
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function

    Public Function UpdateReissue(ByVal st As String, ByVal id As String, ByVal tktno As String, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update ReIssue set status='" & st & "',executiveid=@executiveid,AcceptedDate='" & System.DateTime.Now & "' where tkt_no=@tkt_no and pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@executiveid", id)
        cmd.Parameters.AddWithValue("@tkt_no", tktno)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    
    Public Function PassengerProcess(Optional ByVal tktno As String = "", Optional ByVal pnr As String = "") As DataSet
        If tktno = "" AndAlso pnr = "" Then
            adap = New SqlDataAdapter("select * from dbo.ReIssue where Status='Process'", con)
        Else
            adap = New SqlDataAdapter("select * from dbo.ReIssue where Status='Process' and  Tkt_No='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        End If

        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateReissue2(ByVal [date] As String, ByVal deptime As String, ByVal GdsPnr As String, ByVal AirPnr As String, ByVal Newtktno As String, ByVal charge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal exec As String, ByVal remark As String, ByVal tktno As String, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.ReIssue set status='Ticketed',departure_date='" & [date] & "',deptime='" & deptime & "',pnr_locator='" & GdsPnr & "',airpnr='" & AirPnr & "',tkt_no='" & Newtktno & "',ReIssueCharge='" & charge & "',ServiceCharge='" & servicecharge & "',FareDiff='" & farediff & "',ExecutiveID='" & exec & "',ExecutiveRemark='" & remark & "',UpdatedDate='" & System.DateTime.Now & "' where status='Process' and tkt_no='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function FareDetails(ByVal tktno As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.ReIssue where tkt_no='" & tktno & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    'Public Function ProcessPassengerReissue(ByVal id As String) As DataSet
    '    adap = New SqlDataAdapter("select pnr_locator,tkt_no,sector,flightno,pax_fname,pax_lname,pax_type,departure_date,userid,Agency_Name,totalfare,totalfareafterdiscount,status,updateremark from ReIssue where Status='Process' and ExecutiveID='" & id & "'", con)
    '    Dim ds As New DataSet()
    '    adap.Fill(ds)
    '    Return ds
    'End Function
    Public Function ReissueProcess(ByVal id As String) As DataSet
        adap = New SqlDataAdapter("select * from ReIssue where Status='Process' and ExecutiveID='" & id & "' order by SubmitDate desc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetReissueDetails(ByVal tktno As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.ReIssue where Tkt_No='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateFlightBookingForAdult(ByVal noofadult As String, ByVal totaladult As Double, ByVal adulttax As Double, ByVal bookingcost As Double, ByVal tfee As String, ByVal servicetax As Double, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.flight_booking set total_no_adult=@noofadult,total_adult=@totaladult,adult_tax=@adulttax,total_booking_cost=@bookingcost,t_fee=@tfee,s_tax=@s_tax where pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@noofadult", noofadult)
        cmd.Parameters.AddWithValue("@totaladult", totaladult)
        cmd.Parameters.AddWithValue("@adulttax", adulttax)
        cmd.Parameters.AddWithValue("@bookingcost", bookingcost)
        cmd.Parameters.AddWithValue("@tfee", tfee)
        cmd.Parameters.AddWithValue("@s_tax", servicetax)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i



    End Function

    Public Function UpdateFlightBookingForChild(ByVal noofchild As String, ByVal totalchild As Double, ByVal childtax As Double, ByVal bookingcost As Double, ByVal tfee As String, ByVal servicetax As Double, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.flight_booking set total_no_child=@noofchild,total_child=@totalchild,child_tax=@childtax,total_booking_cost=@bookingcost,t_fee=@tfee,s_tax=@s_tax where pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@noofchild", noofchild)
        cmd.Parameters.AddWithValue("@totalchild", totalchild)
        cmd.Parameters.AddWithValue("@childtax", childtax)
        cmd.Parameters.AddWithValue("@bookingcost", bookingcost)
        cmd.Parameters.AddWithValue("@tfee", tfee)
        cmd.Parameters.AddWithValue("@s_tax", servicetax)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i



    End Function

    Public Function UpdateFlightBookingForInfant(ByVal noofinfant As String, ByVal totalinfant As Double, ByVal infanttax As Double, ByVal bookingcost As Double, ByVal tfee As String, ByVal servicetax As Double, ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.flight_booking set total_no_adult=@noofadult,total_adult=@totaladult,adult_tax=@adulttax,total_booking_cost=@bookingcost,t_fee=@tfee,s_tax=@s_tax where pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@noofinfant", noofinfant)
        cmd.Parameters.AddWithValue("@totaladult", totalinfant)
        cmd.Parameters.AddWithValue("@infanttax", infanttax)
        cmd.Parameters.AddWithValue("@bookingcost", bookingcost)
        cmd.Parameters.AddWithValue("@tfee", tfee)
        cmd.Parameters.AddWithValue("@s_tax", servicetax)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateReIssueStatus(ByVal pnr As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update dbo.flight_booking set pnr_status='ReIssued' where pnr_locator='" & pnr & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function InsertTransactionRepot(ByVal pnr As String, ByVal userid As String, ByVal credit As Double, ByVal avalbal As String, ByVal debit As String, ByVal sector As String, _
     ByVal paxname As String, ByVal flightno As String, ByVal agencyname As String, ByVal rm As String, ByVal status As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("InsertTransReport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pnr", pnr)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@credit", credit)
        cmd.Parameters.AddWithValue("@AvalBal", avalbal)
        cmd.Parameters.AddWithValue("@Debit", debit)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@paxname", paxname)
        cmd.Parameters.AddWithValue("@flightno", flightno)
        cmd.Parameters.AddWithValue("@agencyname", agencyname)
        cmd.Parameters.AddWithValue("@Rm", rm)
        cmd.Parameters.AddWithValue("@status", status)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function InsertintoPax(ByVal pnr As String, ByVal tittle As String, ByVal firstname As String, ByVal lastname As String, ByVal ticketno As String, ByVal paxtype As String)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("InsertPax", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pnrlocator", pnr)
        cmd.Parameters.AddWithValue("@tittle", tittle)
        cmd.Parameters.AddWithValue("@first_name", firstname)
        cmd.Parameters.AddWithValue("@last_name", lastname)
        cmd.Parameters.AddWithValue("@paxtype", paxtype)
        cmd.Parameters.AddWithValue("@ticketno", ticketno)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function InsertFlightBooking(ByVal pnr As String, ByVal sector As String, ByVal Ddate As String, ByVal Dept As String, ByVal Dest As String, ByVal Deptdate As String, _
     ByVal Dtime As String, ByVal Airline As String, ByVal Bdate As String, ByVal noofadutl As Integer, ByVal noofchild As Integer, ByVal noofinfant As Integer, ByVal adulttotal As Double, ByVal adulttax As Double, ByVal childtotal As Double, ByVal childtax As Double, ByVal infanttotal As Double, ByVal infanttax As Double, ByVal TotalBcost As Double, ByVal pnrstatus As String, ByVal uid As String, ByVal paxfname As String, ByVal paxlname As String, ByVal airpnr As String, ByVal tfee As String, ByVal stax As String, ByVal exec As String, ByVal agencyname As String, ByVal reisscharge As String, ByVal servicecharge As String, ByVal farediff As String, ByVal vc As String) As Integer
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("InsertFlightBooking", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pnr", pnr)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@D_Date", Ddate)
        cmd.Parameters.AddWithValue("@Dept", Dept)
        cmd.Parameters.AddWithValue("@Dest", Dest)
        cmd.Parameters.AddWithValue("@DepatDate", Deptdate)
        cmd.Parameters.AddWithValue("@DTime", Dtime)
        cmd.Parameters.AddWithValue("@Airline", Airline)
        cmd.Parameters.AddWithValue("@BDate", Bdate)
        cmd.Parameters.AddWithValue("@NOAdult", noofadutl)
        cmd.Parameters.AddWithValue("@NOChild", noofchild)
        cmd.Parameters.AddWithValue("@NOInfant", noofinfant)
        cmd.Parameters.AddWithValue("@AdultTotal", adulttotal)
        cmd.Parameters.AddWithValue("@AdultTax", adulttax)
        cmd.Parameters.AddWithValue("@ChildTotal", childtotal)
        cmd.Parameters.AddWithValue("@ChildTax", childtax)
        cmd.Parameters.AddWithValue("@InfantTotal", infanttotal)
        cmd.Parameters.AddWithValue("@InfantTax", infanttax)
        cmd.Parameters.AddWithValue("@TotalBookingCost", TotalBcost)
        cmd.Parameters.AddWithValue("@PNRStatus", pnrstatus)
        cmd.Parameters.AddWithValue("@UID", uid)
        cmd.Parameters.AddWithValue("@FName", paxfname)
        cmd.Parameters.AddWithValue("@LName", paxlname)
        cmd.Parameters.AddWithValue("@AirPNR", airpnr)
        cmd.Parameters.AddWithValue("@TFee", tfee)
        cmd.Parameters.AddWithValue("@Stax", stax)
        cmd.Parameters.AddWithValue("@Exec", exec)
        cmd.Parameters.AddWithValue("@AgName", agencyname)
        cmd.Parameters.AddWithValue("@Reissuecharge", reisscharge)
        cmd.Parameters.AddWithValue("@servicecharge", servicecharge)
        cmd.Parameters.AddWithValue("@Farediff", farediff)
        cmd.Parameters.AddWithValue("@VC", vc)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateRejectionStatusWithComment(ByVal comment As String, ByVal id As String, ByVal tktno As String, ByVal pnr As String) As Integer
        Dim i As Integer = 0
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()
            cmd = New SqlCommand("update ReIssue set status='Rejected',RejectionComment=@comment,executiveid=@id,UpdatedDate='" & System.DateTime.Now & "' where tkt_no=@tktno and pnr_locator=@pnr", con)
            cmd.Parameters.AddWithValue("@comment", comment)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.Parameters.AddWithValue("@tktno", tktno)
            cmd.Parameters.AddWithValue("@pnr", pnr)
            i = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try
        Return i
    End Function

    Public Function UpdateReissuePaxInfo(ByVal tktno As String, ByVal pnr As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.pax_information set pnr_locator='Reiss'+@pnr where tkt_no=@tktno", con)
        cmd.Parameters.AddWithValue("@tktno", tktno)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function StatusProcess(ByVal tktno As String) As DataSet
        adap = New SqlDataAdapter("select Status,ExecutiveID from ReIssue where tkt_no='" & tktno & "'", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function NoofPax(ByVal pnr As String) As DataSet
        Dim ds As New DataSet
        adap = New SqlDataAdapter("CountPax", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@pnr", pnr)
        adap.Fill(ds)
        Return ds
    End Function
    Public Function Updateflightbooking(ByVal deptdate As String, ByVal depttime As String, ByVal GdsPnr As String, ByVal airlinepnr As String, ByVal charge As String, ByVal servicecharge As String, ByVal farediff As String)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("UpdateFlightBook", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@deptdate", deptdate)
        cmd.Parameters.AddWithValue("@depttime", depttime)
        cmd.Parameters.AddWithValue("@pnr", GdsPnr)
        cmd.Parameters.AddWithValue("@airlinepnr", airlinepnr)
        cmd.Parameters.AddWithValue("@totalR", charge)
        cmd.Parameters.AddWithValue("@totalS", servicecharge)
        cmd.Parameters.AddWithValue("@totalF", farediff)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdatePaxInfo(ByVal Ntktno As String, ByVal pnr As String, ByVal tktno As String)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update pax_information set tkt_no='" & Ntktno & "' where pnr_locator='" & pnr & "' and tkt_no='" & tktno & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function countpnr(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT COUNT(*) AS cnt FROM ReIssue where pnr_locator = '" & pnr & "' AND (Status = 'Ticketed')", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateCredirLimit(ByVal userid As String, ByVal limit As String) As Double
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("SubtractFromLimit", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", userid)
        cmd.Parameters.Add("@Amount", limit)
        Dim Amt As Double = 0
        Amt = cmd.ExecuteScalar()
        Return Amt
    End Function
    Public Function UpdateRemark(ByVal remark As String, ByVal tktno As String, ByVal pnr As String)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("update ReIssue set UpdateRemark='" & remark & "' where Tkt_No='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
    End Function
    Public Function GetAgency() As DataSet
        adap = New SqlDataAdapter("select user_id,agency_name from New_Regs order by agency_name asc", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetAgencyDetails(ByVal agentid As String) As DataSet
        adap = New SqlDataAdapter("select * from ReIssue where UserID='" & agentid & "' order by  SubmitDate Desc", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
End Class