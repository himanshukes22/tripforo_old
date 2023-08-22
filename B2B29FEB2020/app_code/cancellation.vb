Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

''' <summary>
''' Summary description for cancellation
''' </summary>
Public Class cancellation
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private cmd As SqlCommand
    Private cmd1 As SqlCommand
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    Public Function WithPnrDetails(ByVal pnr As String, ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("GetPNRDetails", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@pnr", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@userid", userid)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function login(ByVal userid As String, ByVal password As String, ByVal type As String) As DataSet
        adap = New SqlDataAdapter("select UserID,Password,UserType from Login where UserID='" & userid & "' and Password='" & password & "' and UserType='" & type & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PnrDetails(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator,flight_booking.departure, flight_booking.destination, flight_booking.departure_date, flight_booking.arrival_time,flight_booking.flight_no,flight_booking.airline, flight_booking.booking_date, flight_booking.total_no_adult, flight_booking.total_no_child,flight_booking.total_no_infant, flight_booking.total_adult, flight_booking.adult_tax, flight_booking.total_child, flight_booking.child_tax,flight_booking.total_infant, flight_booking.infant_tax, flight_booking.total_booking_cost,flight_booking.s_tax,flight_booking.t_fee,flight_booking.VC,Trans_Report.Fare_AFTDIS,Trans_Report.YQ FROM flight_booking INNER JOIN Trans_Report ON flight_booking.pnr_locator = Trans_Report.pnr_locator AND flight_booking.pnr_status = Trans_Report.pnr_status where flight_booking.pnr_status='ticketed' and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PnrDetails2(ByVal airlinecode As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT Airline_Pa.TF, Airline_Pa.STax, New_Regs.Agent_Type,New_Regs.agency_name,New_Regs.User_Id from flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id INNER JOIN Airline_Pa ON flight_booking.VC = Airline_Pa.Code where flight_booking.vc='" & airlinecode & "' and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PnrDetails3(ByVal code As String, ByVal type As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("SELECT Agent_CD.CB, Agent_CD.Dis_YQ, Agent_CD.Dis_B_YQ,agent_cd.dis FROM Agent_CD INNER JOIN flight_booking ON Agent_CD.Airline = flight_booking.VC WHERE  Agent_CD.Airline = '" & code & "' and Agent_cd.grade='" & type & "'and flight_booking.pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxAdultInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select Title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='ADT' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxChildInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select Title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='CHD' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function PaxInfantInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select Title,first_name,last_name,paxtype,tkt_no from dbo.pax_information where paxtype='INF' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function InsertAdult(ByVal pnr As String, ByVal ticketno As String, ByVal sector As String, ByVal Source As String, ByVal Destination As String, ByVal tittle As String, ByVal PaxFname As String, ByVal PaxLname As String, _
     ByVal PaxType As String, ByVal bookingdate As String, ByVal DepartureDate As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BasicAdult As Double, ByVal TaxAdult As Double, _
     ByVal YQAdult As Double, ByVal ServiceTax As Double, ByVal Tranfee As Double, ByVal TotalDiscount As Double, ByVal CBAdult As Double, ByVal TdsAdult As Double, _
     ByVal TotalFareAdult As Double, ByVal TotalFareAfterDiscountAdult As Double, ByVal Regardcancel As String, ByVal vc As String) As Integer
        con.Open()
        Dim row As Integer = 0
        Dim cmd As New SqlCommand("CancelTkt", con)
        Try
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pnr", pnr)
            cmd.Parameters.AddWithValue("@ticketno", ticketno)
            cmd.Parameters.AddWithValue("@sector", sector)
            cmd.Parameters.AddWithValue("@depature", Source)
            cmd.Parameters.AddWithValue("@desination", Destination)
            cmd.Parameters.AddWithValue("@tittle", tittle)
            cmd.Parameters.AddWithValue("@paxfname", PaxFname)
            cmd.Parameters.AddWithValue("@paxlname", PaxLname)
            cmd.Parameters.AddWithValue("@paxtype", PaxType)
            cmd.Parameters.AddWithValue("@bookingdate", bookingdate)
            cmd.Parameters.AddWithValue("@departuredate", DepartureDate)
            cmd.Parameters.AddWithValue("@userid", UserID)
            cmd.Parameters.AddWithValue("@agencyname", AgencyName)
            cmd.Parameters.AddWithValue("@Block_Cancel", "False")
            cmd.Parameters.AddWithValue("@basefare", BasicAdult)
            cmd.Parameters.AddWithValue("@tax", TaxAdult)
            cmd.Parameters.AddWithValue("@yq", YQAdult)
            cmd.Parameters.AddWithValue("@servicetax", ServiceTax)
            cmd.Parameters.AddWithValue("@transfee", Tranfee)
            cmd.Parameters.AddWithValue("@discount", TotalDiscount)
            cmd.Parameters.AddWithValue("@cb", CBAdult)
            cmd.Parameters.AddWithValue("@tds", TdsAdult)
            cmd.Parameters.AddWithValue("@totalfare", TotalFareAdult)
            cmd.Parameters.AddWithValue("@totalfareafterdiscount", TotalFareAfterDiscountAdult)
            cmd.Parameters.AddWithValue("@RegardingCancel", Regardcancel)
            cmd.Parameters.AddWithValue("@Status", "Null")
            cmd.Parameters.AddWithValue("@VC", vc)
            row = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try
        Return row
    End Function
    Public Function InsertChild(ByVal pnr As String, ByVal ticketno As String, ByVal sector As String, ByVal Source As String, ByVal Destination As String, ByVal tittle As String, ByVal PaxFname As String, ByVal PaxLname As String, _
     ByVal PaxType As String, ByVal bookingdate As String, ByVal DepartureDate As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BasicChild As Double, ByVal TaxChild As Double, _
     ByVal YQChild As Double, ByVal ServiceTax As Double, ByVal Tranfee As Double, ByVal TotalDiscount As Double, ByVal CBChild As Double, ByVal TdsChild As Double, _
     ByVal TotalFareChild As Double, ByVal TotalFareAfterDiscountChild As Double, ByVal Regardcancel As String, ByVal vc As String) As Integer
        con.Open()
        Dim row As Integer = 0
        Dim cmd As New SqlCommand("CancelTkt", con)
        Try
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pnr", pnr)
            cmd.Parameters.AddWithValue("@ticketno", ticketno)
            cmd.Parameters.AddWithValue("@sector", sector)
            cmd.Parameters.AddWithValue("@depature", Source)
            cmd.Parameters.AddWithValue("@desination", Destination)
            cmd.Parameters.AddWithValue("@tittle", tittle)
            cmd.Parameters.AddWithValue("@paxfname", PaxFname)
            cmd.Parameters.AddWithValue("@paxlname", PaxLname)
            cmd.Parameters.AddWithValue("@paxtype", PaxType)
            cmd.Parameters.AddWithValue("@bookingdate", bookingdate)
            cmd.Parameters.AddWithValue("@departuredate", DepartureDate)
            cmd.Parameters.AddWithValue("@userid", UserID)
            cmd.Parameters.AddWithValue("@agencyname", AgencyName)
            cmd.Parameters.AddWithValue("@Block_Cancel", "False")
            cmd.Parameters.AddWithValue("@basefare", BasicChild)
            cmd.Parameters.AddWithValue("@tax", TaxChild)
            cmd.Parameters.AddWithValue("@yq", YQChild)
            cmd.Parameters.AddWithValue("@servicetax", ServiceTax)
            cmd.Parameters.AddWithValue("@transfee", Tranfee)
            cmd.Parameters.AddWithValue("@discount", TotalDiscount)
            cmd.Parameters.AddWithValue("@cb", CBChild)
            cmd.Parameters.AddWithValue("@tds", TdsChild)
            cmd.Parameters.AddWithValue("@totalfare", TotalFareChild)
            cmd.Parameters.AddWithValue("@totalfareafterdiscount", TotalFareAfterDiscountChild)
            cmd.Parameters.AddWithValue("@RegardingCancel", Regardcancel)
            cmd.Parameters.AddWithValue("@Status", "Null")
            cmd.Parameters.AddWithValue("@VC", vc)
            row = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try


        Return row

    End Function
    Public Function InsertInfant(ByVal pnr As String, ByVal ticketno As String, ByVal sector As String, ByVal Source As String, ByVal Destination As String, ByVal tittle As String, ByVal PaxFname As String, ByVal PaxLname As String, _
     ByVal PaxType As String, ByVal bookingdate As String, ByVal DepartureDate As String, ByVal UserID As String, ByVal AgencyName As String, ByVal BasicInfant As Double, ByVal TaxInfant As Double, _
     ByVal ServiceTax As Double, ByVal Tranfee As Double, ByVal TotalDiscount As Double, ByVal CBInfant As Double, ByVal TdsInfant As Double, ByVal TotalFareInfant As Double, _
     ByVal TotalFareAfterDiscountInfant As Double, ByVal Regardcancel As String, ByVal vc As String) As Integer
        con.Open()
        Dim row As Integer = 0
        Dim cmd As New SqlCommand("CancelTkt", con)
        Try
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pnr", pnr)
            cmd.Parameters.AddWithValue("@ticketno", ticketno)
            cmd.Parameters.AddWithValue("@sector", sector)
            cmd.Parameters.AddWithValue("@depature", Source)
            cmd.Parameters.AddWithValue("@desination", Destination)
            cmd.Parameters.AddWithValue("@tittle", tittle)
            cmd.Parameters.AddWithValue("@paxfname", PaxFname)
            cmd.Parameters.AddWithValue("@paxlname", PaxLname)
            cmd.Parameters.AddWithValue("@paxtype", PaxType)
            cmd.Parameters.AddWithValue("@bookingdate", bookingdate)
            cmd.Parameters.AddWithValue("@departuredate", DepartureDate)
            cmd.Parameters.AddWithValue("@userid", UserID)
            cmd.Parameters.AddWithValue("@agencyname", AgencyName)
            cmd.Parameters.AddWithValue("@Block_Cancel", "False")
            cmd.Parameters.AddWithValue("@basefare", BasicInfant)
            cmd.Parameters.AddWithValue("@tax", TaxInfant)
            cmd.Parameters.AddWithValue("@yq", "0")
            cmd.Parameters.AddWithValue("@servicetax", ServiceTax)
            cmd.Parameters.AddWithValue("@transfee", Tranfee)
            cmd.Parameters.AddWithValue("@discount", 0)
            cmd.Parameters.AddWithValue("@cb", 0)
            cmd.Parameters.AddWithValue("@tds", 0)
            cmd.Parameters.AddWithValue("@totalfare", TotalFareInfant)
            cmd.Parameters.AddWithValue("@totalfareafterdiscount", TotalFareAfterDiscountInfant)
            cmd.Parameters.AddWithValue("@RegardingCancel", Regardcancel)
            cmd.Parameters.AddWithValue("@Status", "Null")
            cmd.Parameters.AddWithValue("@VC", vc)
            row = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try


        Return row

    End Function

    Public Function PassengerInfo(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select pnr_locator,Tkt_No,Sector,pax_fname,pax_lname,pax_type,departure_date,UserID,Agency_Name,TotalFare,TotalFareAfterDiscount,Status from Cancellation where pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function CancellationPax(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from Cancellation where pnr_locator='" & pnr & "' and status='Null'", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function CanReqUpdate(ByVal remark As String, ByVal tktno As String, ByVal pnr As String) As Integer
        Dim i As Integer = 0
        Try
            con.Open()
            cmd = New SqlCommand("update dbo.Cancellation set Status='Pending',RegardingCancel=@CancelRemark where tkt_no=@tktno and pnr_locator=@pnr", con)
            cmd.Parameters.AddWithValue("@CancelRemark", remark)
            cmd.Parameters.AddWithValue("@tktno", tktno)
            cmd.Parameters.AddWithValue("@pnr", pnr)
            i = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try
        Return i

    End Function
    Public Function PassengerStatus(Optional ByVal pnr As String = "") As DataSet
        Dim str As String = ""
        If pnr = "" Then
            str = "select * from dbo.Cancellation where Status='Pending' order by SubmitDate desc"
        Else
            str = "select * from dbo.Cancellation where Status='Pending' and pnr_locator='" & pnr & "' order by SubmitDate desc"
        End If
        adap = New SqlDataAdapter(str, con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateAcceptStatus(ByVal userid As String, ByVal tktno As String, ByVal pnr As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.Cancellation set status='Process',executiveid=@executiveid,AcceptedDate='" & System.DateTime.Now & "' where status='Pending' and tkt_no=@tkt_no and pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@executiveid", userid)
        cmd.Parameters.AddWithValue("@tkt_no", tktno)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateCancelStatus(ByVal tktno As String, ByVal userid As String, ByVal pnr As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.Cancellation set status='Rejected',UpdatedDate='" & System.DateTime.Now & "' where tkt_no=@tkt_no and executiveid=@executiveid and pnr_locator=@pnr", con)
        cmd.Parameters.AddWithValue("@tkt_no", tktno)
        cmd.Parameters.AddWithValue("@executiveid", userid)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function UpdateCancelStatusWithComment(ByVal comment As String, ByVal id As String, ByVal tktno As String, ByVal pnr As String) As Integer
        Dim i As Integer = 0
        Try
            con.Open()
            cmd = New SqlCommand("update Cancellation set status='Rejected',rejectcomment=@comment,executiveid=@executive,UpdatedDate='" & System.DateTime.Now & "' where tkt_no=@tktno and pnr_locator=@pnr", con)
            cmd.Parameters.AddWithValue("@comment", comment)
            cmd.Parameters.AddWithValue("@executive", id)
            cmd.Parameters.AddWithValue("@tktno", tktno)
            cmd.Parameters.AddWithValue("@pnr", pnr)
            i = cmd.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            con.Close()
        End Try


        Return i
    End Function
    Public Function CancelProcess(ByVal UID As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.Cancellation where Status='Process' and ExecutiveID='" & UID & "' order by Submitdate desc", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function CancelStatus(ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.Cancellation where pnr_locator='" & pnr & "' order by Submitdate desc", con)
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
    Public Function CancellationProcess(ByVal tktno As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.Cancellation where Status='Process' and tkt_no='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateStatus(ByVal charge As String, ByVal refund As String, ByVal remark As String, ByVal tktno As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("update dbo.Cancellation set status='Cancelled',CancellationCharge='" & charge & "',RefundFare='" & refund & "',ExecutiveRemark='" & remark & "',UpdatedDate='" & System.DateTime.Now() & "' where status='Process' and tkt_no='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function FareDetails(ByVal tktno As String) As DataSet
        adap = New SqlDataAdapter("select base_fare,tax,yq,service_tax,tran_fees,discount,cb,tds,totalfareafterdiscount from dbo.Cancellation where tkt_no='" & tktno & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateCredirLimit(ByVal userid As String, ByVal limit As String) As Double
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("AddToLimit", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", userid)
        cmd.Parameters.Add("@Amount", limit)
        Dim Amt As Double = 0
        Amt = cmd.ExecuteScalar()
        con.Close()
        Return Amt
        End Function
    Public Function GetCreditLimit(ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("select crd_limit from dbo.New_Regs where user_id='" & userid & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetCharges(ByVal tktno As String) As DataSet
        adap = New SqlDataAdapter("select cancellationcharge as Charges from Cancellation where Tkt_No='" & tktno & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdatePaxInfo(ByVal tktno As String, ByVal pnr As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.pax_information set pnr_locator='Can'+@pnr where tkt_no=@tktno", con)
        cmd.Parameters.AddWithValue("@tktno", tktno)
        cmd.Parameters.AddWithValue("@pnr", pnr)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function GetCancelDetails(ByVal tktno As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select * from Cancellation where Tkt_No='" & tktno & "' and pnr_locator='" & pnr & "'", con)
        Dim ds As New DataSet()
        adap.Fill(ds)
        Return ds
    End Function
    Public Function InsertTransactionRepot(ByVal pnr As String, ByVal userid As String, ByVal credit As Double, ByVal avalbal As String, ByVal debit As String, ByVal sector As String, _
     ByVal paxname As String, ByVal agencyname As String, ByVal rm As String, ByVal status As String) As Integer
        con.Open()
        cmd = New SqlCommand("InsertTransReport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pnr", pnr)
        cmd.Parameters.AddWithValue("@userid", userid)
        cmd.Parameters.AddWithValue("@Credit", credit)
        cmd.Parameters.AddWithValue("@AvalBal", avalbal)
        cmd.Parameters.AddWithValue("@Debit", debit)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@paxname", paxname)
        cmd.Parameters.AddWithValue("@flightno", "")
        cmd.Parameters.AddWithValue("@agencyname", agencyname)
        cmd.Parameters.AddWithValue("@Rm", rm)
        cmd.Parameters.AddWithValue("@status", status)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    Public Function UpdateFlightBookingForAdult(ByVal noofadult As String, ByVal totaladult As Double, ByVal adulttax As Double, ByVal bookingcost As Double, ByVal tfee As String, ByVal servicetax As Double, ByVal pnr As String) As Integer
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
    Public Function CancelReportExec(ByVal id As String, ByVal fromdate As String, ByVal todate As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("select * from dbo.Cancellation where ExecutiveID='" & id & "' and status<>'NULL' and  SubmitDate >='" & fromdate & "' and SubmitDate <='" & todate & "' order by SubmitDate desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function CancelReportAdmin(ByVal fromdate As String, ByVal todate As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("select * from dbo.Cancellation where  status<>'NULL' and  SubmitDate >='" & fromdate & "' and SubmitDate <='" & todate & "' order by SubmitDate desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function CancelReportforAgent(ByVal id As String, ByVal fromdate As String, ByVal todate As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("select * from dbo.Cancellation where UserID='" & id & "' and status<>'NULL' and SubmitDate >='" & fromdate & "' and SubmitDate <='" & todate & "' order by SubmitDate desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function UpdateStatus(ByVal pnr As String) As Integer
        con.Open()
        cmd = New SqlCommand("update dbo.flight_booking set pnr_status='Cancelled' where pnr_locator='" & pnr & "'", con)
        cmd1 = New SqlCommand("update Trans_Report set pnr_status='Cancelled' where pnr_locator='" & pnr & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Dim j As Integer = cmd1.ExecuteNonQuery()
        con.Close()
        Return i
        Return j
    End Function
    Public Function StatusProcess(ByVal tktno As String, ByVal pnr As String) As DataSet
        adap = New SqlDataAdapter("select Status,ExecutiveID from Cancellation where tkt_no='" & tktno & "' and pnr_locator='" & pnr & "' ", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetAgency() As DataSet
        adap = New SqlDataAdapter("select user_id,agency_name from New_Regs order by agency_name asc", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
    Public Function GetAgencyDetails(ByVal agentid As String) As DataSet
        adap = New SqlDataAdapter("select * from dbo.Cancellation where status<>'NULL' and UserId='" & agentid & "'", con)
        Dim ds As New DataSet
        adap.Fill(ds)
        Return ds
    End Function
End Class