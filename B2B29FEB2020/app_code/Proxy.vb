Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
''' <summary>
''' Summary description for Proxy
''' </summary>
Public Class ProxyClass
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private cmd As SqlCommand
    Private ds As DataSet
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub

    Public Sub UpdateAdult(ByVal sn As Integer, ByVal empid As Integer, ByVal Sir As String, ByVal FName As String, ByVal LName As String, ByVal Age As String)
        ds = New DataSet()
        adap = New SqlDataAdapter("Update Adult set ProxyID='" & empid & "',SirName='" & Sir.Replace("'", "`").Trim() & "',FirstName='" & FName.Replace("'", "`").Trim() & "',LastName='" & LName.Replace("'", "`").Trim() & "',Age='" & Age & "' where EmpId='" & sn & "'", con)
        adap.Fill(ds)
    End Sub
    Public Sub UpdateChild(ByVal sn As Integer, ByVal empid As Integer, ByVal Sir As String, ByVal FName As String, ByVal LName As String, ByVal Age As Integer)
        ds = New DataSet()
        adap = New SqlDataAdapter("Update Child set ProxyID='" & empid & "',SirName='" & Sir.Replace("'", "`").Trim() & "',FirstName='" & FName.Replace("'", "`").Trim() & "',LastName='" & LName.Replace("'", "`").Trim() & "',Age='" & Age & "' where EmpId='" & sn & "'", con)
        adap.Fill(ds)
    End Sub
    Public Sub UpdateInfrant(ByVal sn As Integer, ByVal empid As Integer, ByVal Sir As String, ByVal FName As String, ByVal LName As String, ByVal Age As Integer)
        ds = New DataSet()
        adap = New SqlDataAdapter("Update Infrant set ProxyID='" & empid & "',SirName='" & Sir.Replace("'", "`").Trim() & "',FirstName='" & FName.Replace("'", "`").Trim() & "',LastName='" & LName.Replace("'", "`").Trim() & "',Age='" & Age & "' where EmpId='" & sn & "'", con)
        adap.Fill(ds)
    End Sub
    Public Sub InsertOneWayProxy(ByVal BookingType As String, ByVal TravelType As String, ByVal from As String, ByVal [to] As String, ByVal DDate As String, ByVal DTime As String, _
     ByVal Adult As String, ByVal Child As String, ByVal Infrant As String, ByVal [Class] As String, ByVal Airline As String, ByVal Classes As String, _
     ByVal PaymentMode As String, ByVal Remark As String, ByVal AgentID As String, ByVal Ag_name As String, ByVal Status As String, ByVal BlockProxy As Boolean)
        ds = New DataSet()
        adap = New SqlDataAdapter("INSERT INTO ProxyTicket(BookingType,TravelType,ProxyFrom,ProxyTo,DepartDate,DepartTime,Adult,Child,Infrant,Class,Airlines,Classes,PaymentMode,Remark,AgentID,Ag_Name,Status,BlockProxy) VALUES ('" & BookingType & "','" & TravelType & "','" & from & "','" & [to] & "','" & DDate & "','" & DTime & "','" & Adult & "','" & Child & "','" & Infrant & "','" & [Class] & "','" & Airline & "','" & Classes & "','" & PaymentMode & "','" & Remark & "','" & AgentID & "','" & Ag_name & "','" & Status & "','" & BlockProxy & "')", con)
        adap.Fill(ds)
    End Sub
    Public Sub InsertRoundTripProxy(ByVal BookingType As String, ByVal TravelType As String, ByVal from As String, ByVal [to] As String, ByVal DDate As String, ByVal RDate As String, _
     ByVal DTime As String, ByVal RTime As String, ByVal Adult As String, ByVal Child As String, ByVal Infrant As String, ByVal [Class] As String, _
     ByVal Airline As String, ByVal Classes As String, ByVal PaymentMode As String, ByVal Remark As String, ByVal AgentID As String, ByVal Ag_Name As String, _
     ByVal Status As String, ByVal BlockProxy As Boolean)
        ds = New DataSet()
        adap = New SqlDataAdapter("INSERT INTO ProxyTicket(BookingType,TravelType,ProxyFrom,ProxyTo,DepartDate,ReturnDate,DepartTime,ReturnTime,Adult,Child,Infrant,Class,Airlines,Classes,PaymentMode,Remark,AgentID,Ag_Name,Status,BlockProxy) VALUES ('" & BookingType & "','" & TravelType & "','" & from & "','" & [to] & "','" & DDate & "','" & RDate & "','" & DTime & "','" & RTime & "','" & Adult & "','" & Child & "','" & Infrant & "','" & [Class] & "','" & Airline & "','" & Classes & "','" & PaymentMode & "','" & Remark & "','" & AgentID & "','" & Ag_Name & "','" & Status & "','" & BlockProxy & "')", con)
        adap.Fill(ds)
    End Sub
    Public Function AdminLogin(ByVal user As String, ByVal pwd As String) As Integer
        Try

            ds = New DataSet()
            adap = New SqlDataAdapter("select * from sp_admin_login where Name='" & user.Replace("'", "`").Trim() & "' and Password='" & pwd.Replace("'", "`").Trim() & "'", con)
            adap.Fill(ds, "sp_admin_login")
            Dim temp As Integer = ds.Tables("sp_admin_login").Rows.Count
            If temp = 0 Then
                Return 0
            Else

                Return 1
            End If
            ' MessageBox.Show(e.Message);

        Catch e As Exception
        End Try
        Return 3
    End Function
    Public Function ProxyTicketDetail(ByVal id As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select * from ProxyTicket where Exec_ID='" & id & "' and Status='InProcess' order by ProxyID desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function ProxyPendingTicketDetail() As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select * from ProxyTicket where Status='Pending' order by ProxyID desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function ProxyAgentTicketDetail(ByVal AgentID As String, ByVal usertype As String) As DataSet
        Dim ds As New DataSet()
        If usertype = "ADMIN" Then
            adap = New SqlDataAdapter("Select * from ProxyTicket ORDER BY requestDateTime DESC", con)
        ElseIf usertype = "EXEC" Then
            adap = New SqlDataAdapter("Select * from ProxyTicket where  Exec_ID='" & AgentID & "' and Status='Ticketed' or Status='Rejected' ORDER BY requestDateTime DESC", con)
        ElseIf usertype = "AGENT" Then
            adap = New SqlDataAdapter("Select * from ProxyTicket where AgentID='" & AgentID & "' ORDER BY requestDateTime DESC", con)
        End If
        adap.Fill(ds)
        Return ds

    End Function

    Public Function ShowAdult(ByVal ProxyID As Integer) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from Adult where ProxyID=" & ProxyID & "", con)
        adap.Fill(ds, "Adult")
        Return ds

    End Function
    Public Function ShowChild(ByVal ProxyID As Integer) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from Child where ProxyID=" & ProxyID & "", con)
        adap.Fill(ds, "Child")
        Return ds

    End Function
    Public Function ShowInfrant(ByVal ProxyID As Integer) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from Infrant where ProxyID=" & ProxyID & "", con)
        adap.Fill(ds, "Infrant")
        Return ds

    End Function
    Public Function ShowProxyByID(ByVal ProxyID As Integer) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from ProxyTicket where ProxyID=" & ProxyID & "", con)
        adap.Fill(ds, "Adult")
        Return ds

    End Function
    Public Function ShowTopAdult(ByVal ProxyID As Integer) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select TOP 1 * from Adult where ProxyID=" & ProxyID & "", con)
        adap.Fill(ds, "Adult")
        Return ds

    End Function
    Public Function ServiceTax(ByVal Air As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select Code,STax from Airline_Pa where Code='" & Air & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function TransactionFee(ByVal Air As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select TF from Airline_Pa where Code='" & Air & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function Discount(ByVal Air As String, ByVal UserID As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("SELECT Agent_CD.Dis as Discount, Agent_CD.Airline as Airline, Agent_CD.CB as CB, Agent_CD.Dis_YQ as Dis_YQ, Agent_CD.Dis_B_YQ  as Dis_B_YQ, Agent_CD.Grade as Type, New_Regs.User_Id as UserID FROM Agent_CD INNER JOIN New_Regs ON Agent_CD.Grade = New_Regs.Agent_Type where Agent_CD.Airline='" & Air & "' and New_Regs.User_Id='" & UserID & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function

    Public Function TDS(ByVal AgentID As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select TDS from New_Regs where User_Id='" & AgentID & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function InsertAdult(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        'con.Open();
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        ' cmd.ExecuteNonQuery();
        'con.Close();

        adap.Fill(ds)
        Return ds


    End Function
    Public Function InsertReturnAdult(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        'con.Open();
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        ' cmd.ExecuteNonQuery();
        'con.Close();

        adap.Fill(ds)
        Return ds


    End Function

    Public Function InsertChild(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        adap.Fill(ds)


        Return ds


    End Function
    Public Function InsertReturnChild(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        adap.Fill(ds)


        Return ds


    End Function

    Public Function InsertInfrant(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        adap.Fill(ds)


        Return ds


    End Function

    Public Function InsertReturnInfrant(ByVal pnr As String, ByVal tittle As String, ByVal first As String, ByVal last As String, ByVal paxtype As String, ByVal tktno As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("insert into pax_information(pnr_locator,Title,first_name,last_name,paxtype,tkt_no) values(@pnr_locator,@Title,@first_name,@last_name,@paxtype,@tkt_no)", con)
        adap.SelectCommand.Parameters.AddWithValue("@pnr_locator", pnr)
        adap.SelectCommand.Parameters.AddWithValue("@Title", tittle)
        adap.SelectCommand.Parameters.AddWithValue("@first_name", first)
        adap.SelectCommand.Parameters.AddWithValue("@last_name", last)
        adap.SelectCommand.Parameters.AddWithValue("@paxtype", paxtype)
        adap.SelectCommand.Parameters.AddWithValue("@tkt_no", tktno)
        adap.Fill(ds)


        Return ds


    End Function

    Public Function UpdateProxyExecutiveID(ByVal ProxyID As String, ByVal ExecID As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket set Exec_ID='" & ExecID & "',AcceptedDate='" & System.DateTime.Now & "' where ProxyID=" & ProxyID & " ", con)
        adap.Fill(ds)
        Return ds


    End Function


    Public Function UpdateBlockProxy(ByVal ProxyID As String, ByVal Status As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket set Status='" & Status & "' where ProxyID=" & ProxyID & " ", con)
        adap.Fill(ds)
        Return ds


    End Function
    Public Function RejectBlockProxy(ByVal ProxyID As String, ByVal Status As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket set Status='" & Status & "',UpdatedDate='" & System.DateTime.Now & "' where ProxyID=" & ProxyID & " ", con)
        adap.Fill(ds)
        Return ds


    End Function
    Public Function CardLimit(ByVal UserID As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select Crd_Limit from New_Regs where User_Id='" & UserID & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function UpdateCardLimit(ByVal UserID As String, ByVal CardLimit As String) As Double
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        cmd = New SqlCommand("SubtractFromLimit", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@UserId", UserID)
        cmd.Parameters.AddWithValue("@Amount", CardLimit)
        Dim amt As Double = 0
        amt = cmd.ExecuteScalar()
        Return amt
    End Function
    Public Function SelectBlockProxy(ByVal AID As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("Select ProxyID,BlockProxy from ProxyTicket where AgentID='" & AID & "' ", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function InsertOneWayBooking_Details(ByVal pnr_locator As String, ByVal sector As String, ByVal dept_date As String, ByVal departure As String, ByVal destination As String, ByVal departure_date As String, _
     ByVal departure_time As String, ByVal arrival_date As String, ByVal arrival_time As String, ByVal flight_no As String, ByVal airline As String, ByVal booking_date As DateTime, _
     ByVal total_no_adult As String, ByVal total_no_child As String, ByVal total_no_infant As String, ByVal total_adult As String, ByVal adult_tax As String, ByVal total_child As String, _
     ByVal child_tax As String, ByVal total_infant As String, ByVal infant_tax As String, ByVal total_booking_cost As String, ByVal pnr_status As String, ByVal user_id As String, _
     ByVal pax_fname As String, ByVal pax_lname As String, ByVal MobNO As String, ByVal Email As String, ByVal Distr As String, ByVal Air_PNR As String, ByVal [Class] As String, _
     ByVal T_Fee As String, ByVal S_Tax As String, ByVal Exec As String, ByVal Ag_Name As String, ByVal Payment_Type As String, ByVal Agent_MRK As Integer, ByVal VC As String) As DataSet


        Dim ds As New DataSet()
        'con.Open();
        'adap = new SqlDataAdapter("INSERT INTO flight_booking(pnr_locator,sector,dept_date,ret_date,departure,destination,departure_date,departure_time,arrival_date,arrival_time,flight_no,airline,booking_date,total_no_adult,total_no_child,total_no_infant,total_adult,adult_tax,total_child,child_tax,total_infant,infant_tax,total_booking_cost,pnr_status,user_id,pax_fname,pax_lname,Air_PNR,class,T_Fee,S_Tax,Ag_Name,Payment_Type,Agent_MRK)values(@GDSPNR,@sector,@D_Date,@R_Date,@Dept,@Dest,@DepatDate,@DTime,@ADate,@Atime,@FlightNo,@Air,@BDate,@NOA,@NOC,@NOI,@ATotal,@ATax,@CTotal,@CTax,@ITotal,@ITax,@TotalBCost,@PNRStatus,@UID,@FName,@LName,@AirPNR,@class,@TFee,@Stax,@AgName,@PType,@AMRK) ", con);
        Dim adap As New SqlDataAdapter("InsertBookingDetailsOneWay", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@GDSPNR", pnr_locator)
        adap.SelectCommand.Parameters.AddWithValue("@sector", sector)
        adap.SelectCommand.Parameters.AddWithValue("@D_Date", dept_date)

        adap.SelectCommand.Parameters.AddWithValue("@Dept", departure)
        adap.SelectCommand.Parameters.AddWithValue("@Dest", destination)

        adap.SelectCommand.Parameters.AddWithValue("@DepatDate", departure_date)
        adap.SelectCommand.Parameters.AddWithValue("@DTime", departure_time)
        adap.SelectCommand.Parameters.AddWithValue("@ADate", arrival_date)
        adap.SelectCommand.Parameters.AddWithValue("@Atime", arrival_time)
        adap.SelectCommand.Parameters.AddWithValue("@FlightNo", flight_no)
        adap.SelectCommand.Parameters.AddWithValue("@Air", airline)


        adap.SelectCommand.Parameters.AddWithValue("@BDate", booking_date)
        adap.SelectCommand.Parameters.AddWithValue("@NOA", total_no_adult)
        adap.SelectCommand.Parameters.AddWithValue("@NOC", total_no_child)
        adap.SelectCommand.Parameters.AddWithValue("@NOI", total_no_infant)
        adap.SelectCommand.Parameters.AddWithValue("@ATotal", total_adult)
        adap.SelectCommand.Parameters.AddWithValue("@ATax", adult_tax)

        adap.SelectCommand.Parameters.AddWithValue("@CTotal", total_child)
        adap.SelectCommand.Parameters.AddWithValue("@CTax", child_tax)
        adap.SelectCommand.Parameters.AddWithValue("@ITotal", total_infant)
        adap.SelectCommand.Parameters.AddWithValue("@ITax", infant_tax)
        adap.SelectCommand.Parameters.AddWithValue("@TotalBCost", total_booking_cost)
        adap.SelectCommand.Parameters.AddWithValue("@PNRStatus", pnr_status)

        adap.SelectCommand.Parameters.AddWithValue("@UID", user_id)
        adap.SelectCommand.Parameters.AddWithValue("@FName", pax_fname)
        adap.SelectCommand.Parameters.AddWithValue("@LName", pax_lname)
        adap.SelectCommand.Parameters.AddWithValue("@MobNo", MobNO)
        adap.SelectCommand.Parameters.AddWithValue("@Email", Email)

        adap.SelectCommand.Parameters.AddWithValue("@Distr", Distr)

        adap.SelectCommand.Parameters.AddWithValue("@AirPNR", Air_PNR)
        adap.SelectCommand.Parameters.AddWithValue("@class", [Class])
        adap.SelectCommand.Parameters.AddWithValue("@TFee", T_Fee)

        adap.SelectCommand.Parameters.AddWithValue("@Stax", S_Tax)

        adap.SelectCommand.Parameters.AddWithValue("@Exec", Exec)

        adap.SelectCommand.Parameters.AddWithValue("@AgName", Ag_Name)
        adap.SelectCommand.Parameters.AddWithValue("@PType", Payment_Type)
        adap.SelectCommand.Parameters.AddWithValue("@AMRK", Agent_MRK)
        adap.SelectCommand.Parameters.AddWithValue("@VC", VC)

        adap.Fill(ds)
        Return ds

    End Function
    Public Function InsertRoundTripBooking_Details(ByVal pnr_locator As String, ByVal sector As String, ByVal dept_date As String, ByVal departure As String, ByVal destination As String, ByVal departure_date As String, _
     ByVal departure_time As String, ByVal arrival_date As String, ByVal arrival_time As String, ByVal flight_no As String, ByVal airline As String, ByVal booking_date As DateTime, _
     ByVal total_no_adult As String, ByVal total_no_child As String, ByVal total_no_infant As String, ByVal total_adult As String, ByVal adult_tax As String, ByVal total_child As String, _
     ByVal child_tax As String, ByVal total_infant As String, ByVal infant_tax As String, ByVal total_booking_cost As String, ByVal pnr_status As String, ByVal user_id As String, _
     ByVal pax_fname As String, ByVal pax_lname As String, ByVal MobNo As String, ByVal Email As String, ByVal Exec As String, ByVal Air_PNR As String, ByVal [Class] As String, _
     ByVal T_Fee As String, ByVal S_Tax As String, ByVal Distr As String, ByVal Ag_Name As String, ByVal Payment_Type As String, ByVal Agent_MRK As Integer, ByVal VC As String) As DataSet


        Dim ds As New DataSet()
        'con.Open();
        'adap = new SqlDataAdapter("INSERT INTO flight_booking(pnr_locator,sector,dept_date,ret_date,departure,destination,departure_date,departure_time,arrival_date,arrival_time,flight_no,airline,booking_date,total_no_adult,total_no_child,total_no_infant,total_adult,adult_tax,total_child,child_tax,total_infant,infant_tax,total_booking_cost,pnr_status,user_id,pax_fname,pax_lname,Air_PNR,class,T_Fee,S_Tax,Ag_Name,Payment_Type,Agent_MRK)values(@GDSPNR,@sector,@D_Date,@R_Date,@Dept,@Dest,@DepatDate,@DTime,@ADate,@Atime,@FlightNo,@Air,@BDate,@NOA,@NOC,@NOI,@ATotal,@ATax,@CTotal,@CTax,@ITotal,@ITax,@TotalBCost,@PNRStatus,@UID,@FName,@LName,@AirPNR,@class,@TFee,@Stax,@AgName,@PType,@AMRK) ", con);
        Dim adap As New SqlDataAdapter("InsertBookingDetailsRoundTrip", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@GDSPNR", pnr_locator)
        adap.SelectCommand.Parameters.AddWithValue("@sector", sector)
        adap.SelectCommand.Parameters.AddWithValue("@D_Date", dept_date)

        adap.SelectCommand.Parameters.AddWithValue("@Dept", departure)
        adap.SelectCommand.Parameters.AddWithValue("@Dest", destination)

        adap.SelectCommand.Parameters.AddWithValue("@DepatDate", departure_date)
        adap.SelectCommand.Parameters.AddWithValue("@DTime", departure_time)
        adap.SelectCommand.Parameters.AddWithValue("@ADate", arrival_date)
        adap.SelectCommand.Parameters.AddWithValue("@Atime", arrival_time)
        adap.SelectCommand.Parameters.AddWithValue("@FlightNo", flight_no)
        adap.SelectCommand.Parameters.AddWithValue("@Air", airline)


        adap.SelectCommand.Parameters.AddWithValue("@BDate", booking_date)
        adap.SelectCommand.Parameters.AddWithValue("@NOA", total_no_adult)
        adap.SelectCommand.Parameters.AddWithValue("@NOC", total_no_child)
        adap.SelectCommand.Parameters.AddWithValue("@NOI", total_no_infant)
        adap.SelectCommand.Parameters.AddWithValue("@ATotal", total_adult)
        adap.SelectCommand.Parameters.AddWithValue("@ATax", adult_tax)

        adap.SelectCommand.Parameters.AddWithValue("@CTotal", total_child)
        adap.SelectCommand.Parameters.AddWithValue("@CTax", child_tax)
        adap.SelectCommand.Parameters.AddWithValue("@ITotal", total_infant)
        adap.SelectCommand.Parameters.AddWithValue("@ITax", infant_tax)
        adap.SelectCommand.Parameters.AddWithValue("@TotalBCost", total_booking_cost)
        adap.SelectCommand.Parameters.AddWithValue("@PNRStatus", pnr_status)

        adap.SelectCommand.Parameters.AddWithValue("@UID", user_id)
        adap.SelectCommand.Parameters.AddWithValue("@FName", pax_fname)
        adap.SelectCommand.Parameters.AddWithValue("@MobNo", MobNo)
        adap.SelectCommand.Parameters.AddWithValue("@Email", Email)
        adap.SelectCommand.Parameters.AddWithValue("@Distr", Distr)
        adap.SelectCommand.Parameters.AddWithValue("@LName", pax_lname)
        adap.SelectCommand.Parameters.AddWithValue("@AirPNR", Air_PNR)
        adap.SelectCommand.Parameters.AddWithValue("@class", [Class])
        adap.SelectCommand.Parameters.AddWithValue("@TFee", T_Fee)

        adap.SelectCommand.Parameters.AddWithValue("@Stax", S_Tax)
        adap.SelectCommand.Parameters.AddWithValue("@Exec", Exec)
        adap.SelectCommand.Parameters.AddWithValue("@AgName", Ag_Name)
        adap.SelectCommand.Parameters.AddWithValue("@PType", Payment_Type)
        adap.SelectCommand.Parameters.AddWithValue("@AMRK", Agent_MRK)
        adap.SelectCommand.Parameters.AddWithValue("@VC", VC)

        adap.Fill(ds)
        Return ds

    End Function


    Public Function InsertOneWayTrans_Report(ByVal UID As String, ByVal GDSPNR As String, ByVal PNRStatus As String, ByVal BDate As DateTime, ByVal Credit As String, ByVal Discount As String, _
     ByVal AvalBal As String, ByVal CB As String, ByVal Debit As String, ByVal TDS As String, ByVal Sector As String, ByVal FName As String, _
     ByVal FlightNo As String, ByVal MobileNo As String, ByVal Email As String, ByVal Rm As String, ByVal PType As String, ByVal AgentMRK As String, _
     ByVal Dist As String, ByVal FareAFTDIS As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TotBF As String, _
     ByVal Ag_Name As String, ByVal SFDis As String) As DataSet


        Dim ds As New DataSet()
        'con.Open();
        'adap = new SqlDataAdapter("INSERT INTO flight_booking(pnr_locator,sector,dept_date,ret_date,departure,destination,departure_date,departure_time,arrival_date,arrival_time,flight_no,airline,booking_date,total_no_adult,total_no_child,total_no_infant,total_adult,adult_tax,total_child,child_tax,total_infant,infant_tax,total_booking_cost,pnr_status,user_id,pax_fname,pax_lname,Air_PNR,class,T_Fee,S_Tax,Ag_Name,Payment_Type,Agent_MRK)values(@GDSPNR,@sector,@D_Date,@R_Date,@Dept,@Dest,@DepatDate,@DTime,@ADate,@Atime,@FlightNo,@Air,@BDate,@NOA,@NOC,@NOI,@ATotal,@ATax,@CTotal,@CTax,@ITotal,@ITax,@TotalBCost,@PNRStatus,@UID,@FName,@LName,@AirPNR,@class,@TFee,@Stax,@AgName,@PType,@AMRK) ", con);
        Dim adap As New SqlDataAdapter("InsertTransReportOneWay", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@UID", UID)
        adap.SelectCommand.Parameters.AddWithValue("@GDSPNR", GDSPNR)
        adap.SelectCommand.Parameters.AddWithValue("@PNRStatus", PNRStatus)

        adap.SelectCommand.Parameters.AddWithValue("@BDate", BDate)
        adap.SelectCommand.Parameters.AddWithValue("@Credit", Credit)

        adap.SelectCommand.Parameters.AddWithValue("@Discount", Discount)
        adap.SelectCommand.Parameters.AddWithValue("@AvalBal", AvalBal)
        adap.SelectCommand.Parameters.AddWithValue("@CB", CB)
        adap.SelectCommand.Parameters.AddWithValue("@Debit", Debit)
        adap.SelectCommand.Parameters.AddWithValue("@TDS", TDS)
        adap.SelectCommand.Parameters.AddWithValue("@Sector", Sector)


        adap.SelectCommand.Parameters.AddWithValue("@FName", FName)
        adap.SelectCommand.Parameters.AddWithValue("@FlightNo", FlightNo)
        adap.SelectCommand.Parameters.AddWithValue("@MobileNo", MobileNo)
        adap.SelectCommand.Parameters.AddWithValue("@Email", Email)
        adap.SelectCommand.Parameters.AddWithValue("@Rm", Rm)
        adap.SelectCommand.Parameters.AddWithValue("@PType", PType)

        adap.SelectCommand.Parameters.AddWithValue("@AgentMRK", AgentMRK)
        adap.SelectCommand.Parameters.AddWithValue("@Dist", Dist)
        adap.SelectCommand.Parameters.AddWithValue("@FareAFTDIS", FareAFTDIS)
        adap.SelectCommand.Parameters.AddWithValue("@Tax", Tax)
        adap.SelectCommand.Parameters.AddWithValue("@YQ", YQ)
        adap.SelectCommand.Parameters.AddWithValue("@STax", STax)

        adap.SelectCommand.Parameters.AddWithValue("@TotBF", TotBF)
        adap.SelectCommand.Parameters.AddWithValue("@Ag_Name", Ag_Name)
        adap.SelectCommand.Parameters.AddWithValue("@SFDis", SFDis)

        adap.Fill(ds)
        Return ds

    End Function
    Public Function InsertRoundTripTrans_Report(ByVal UID As String, ByVal GDSPNR As String, ByVal PNRStatus As String, ByVal BDate As DateTime, ByVal Credit As String, ByVal Discount As String, _
     ByVal AvalBal As String, ByVal CB As String, ByVal Debit As String, ByVal TDS As String, ByVal Sector As String, ByVal FName As String, _
     ByVal FlightNo As String, ByVal MobileNo As String, ByVal Email As String, ByVal Rm As String, ByVal PType As String, ByVal AgentMRK As String, _
     ByVal Dist As String, ByVal FareAFTDIS As String, ByVal Tax As String, ByVal YQ As String, ByVal STax As String, ByVal TotBF As String, _
     ByVal Ag_Name As String, ByVal ReSFDis As String) As DataSet


        Dim ds As New DataSet()
        'con.Open();
        'adap = new SqlDataAdapter("INSERT INTO flight_booking(pnr_locator,sector,dept_date,ret_date,departure,destination,departure_date,departure_time,arrival_date,arrival_time,flight_no,airline,booking_date,total_no_adult,total_no_child,total_no_infant,total_adult,adult_tax,total_child,child_tax,total_infant,infant_tax,total_booking_cost,pnr_status,user_id,pax_fname,pax_lname,Air_PNR,class,T_Fee,S_Tax,Ag_Name,Payment_Type,Agent_MRK)values(@GDSPNR,@sector,@D_Date,@R_Date,@Dept,@Dest,@DepatDate,@DTime,@ADate,@Atime,@FlightNo,@Air,@BDate,@NOA,@NOC,@NOI,@ATotal,@ATax,@CTotal,@CTax,@ITotal,@ITax,@TotalBCost,@PNRStatus,@UID,@FName,@LName,@AirPNR,@class,@TFee,@Stax,@AgName,@PType,@AMRK) ", con);
        Dim adap As New SqlDataAdapter("InsertTransReportRoundTrip", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@UID", UID)
        adap.SelectCommand.Parameters.AddWithValue("@GDSPNR", GDSPNR)
        adap.SelectCommand.Parameters.AddWithValue("@PNRStatus", PNRStatus)

        adap.SelectCommand.Parameters.AddWithValue("@BDate", BDate)
        adap.SelectCommand.Parameters.AddWithValue("@Credit", Credit)

        adap.SelectCommand.Parameters.AddWithValue("@Discount", Discount)
        adap.SelectCommand.Parameters.AddWithValue("@AvalBal", AvalBal)
        adap.SelectCommand.Parameters.AddWithValue("@CB", CB)
        adap.SelectCommand.Parameters.AddWithValue("@Debit", Debit)
        adap.SelectCommand.Parameters.AddWithValue("@TDS", TDS)
        adap.SelectCommand.Parameters.AddWithValue("@Sector", Sector)


        adap.SelectCommand.Parameters.AddWithValue("@FName", FName)
        adap.SelectCommand.Parameters.AddWithValue("@FlightNo", FlightNo)
        adap.SelectCommand.Parameters.AddWithValue("@MobileNo", MobileNo)
        adap.SelectCommand.Parameters.AddWithValue("@Email", Email)
        adap.SelectCommand.Parameters.AddWithValue("@Rm", Rm)
        adap.SelectCommand.Parameters.AddWithValue("@PType", PType)

        adap.SelectCommand.Parameters.AddWithValue("@AgentMRK", AgentMRK)
        adap.SelectCommand.Parameters.AddWithValue("@Dist", Dist)
        adap.SelectCommand.Parameters.AddWithValue("@FareAFTDIS", FareAFTDIS)
        adap.SelectCommand.Parameters.AddWithValue("@Tax", Tax)
        adap.SelectCommand.Parameters.AddWithValue("@YQ", YQ)
        adap.SelectCommand.Parameters.AddWithValue("@STax", STax)

        adap.SelectCommand.Parameters.AddWithValue("@TotBF", TotBF)
        adap.SelectCommand.Parameters.AddWithValue("@Ag_Name", Ag_Name)
        adap.SelectCommand.Parameters.AddWithValue("@ReSFDis", ReSFDis)


        adap.Fill(ds)
        Return ds

    End Function


    Public Function UpdateBlockProxy(ByVal ProxyID As Integer) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket SET  Status='Ticketed' , BlockProxy='False' where ProxyID='" & ProxyID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
    Public Sub ResetFormControlValues(ByVal parent As Control)
        For Each c As Control In parent.Controls
            If c.Controls.Count > 0 Then
                ResetFormControlValues(c)
            Else
                Select Case c.[GetType]().ToString()
                    Case "System.Web.UI.WebControls.TextBox"
                        DirectCast(c, TextBox).Text = ""
                        Exit Select
                    Case "System.Web.UI.WebControls.CheckBox"
                        DirectCast(c, CheckBox).Checked = False
                        Exit Select
                    Case "System.Web.UI.WebControls.RadioButton"
                        DirectCast(c, RadioButton).Checked = False
                        Exit Select

                    Case "System.Web.UI.WebControls.Label"
                        DirectCast(c, Label).Text = ""
                        Exit Select

                End Select
            End If
        Next
    End Sub
    Public Function AgentCheckStatus(ByVal AgentId As String) As DataSet
        'con.Open();
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Usp_AgentTrueFalse", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentId)
        adap.Fill(ds)
        Return ds

    End Function

    Public Function FullAgentDetail(ByVal AgentID As String) As DataSet
        Dim ds As New DataSet()

        adap = New SqlDataAdapter("SELECT   Fname+Lname as Name, Address ,City+','+State+','+Country+','+zipcode as Addr, Phone, Mobile, Email, PanNo,Agency_Name,Crd_Limit FROM New_Regs where User_Id='" & AgentID & "'", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function InsertRejectComment(ByVal ProxyID As Integer, ByVal comment As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket SET  RejectComment='" & comment & "' where ProxyID='" & ProxyID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
    'New Code
    Public Function AgentComment(ByVal ProxyID As Integer, ByVal comment As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket SET  UpdateRemark='" & comment & "' where ProxyID='" & ProxyID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
    'Updated Date in proxy
    Public Function UpdatedDate(ByVal ProxyID As Integer) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE ProxyTicket SET  UpdatedDate='" & System.DateTime.Now & "' where ProxyID='" & ProxyID & "'  ", con)
        adap.Fill(ds)
        Return ds


    End Function
End Class