Imports System.Collections.Generic
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Web.UI
''' <summary>
''' Summary description for Sales
''' </summary>
Public Class Sales
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private ds As DataSet
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    'Function for Sales Agent Registration
    Public Function InsertSalesRegistration(ByVal Fname As String, ByVal LName As String, ByVal Loc As String, ByVal MNo As String, ByVal EmailId As String, ByVal Password As String, _
     ByVal Status As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("InsertSalesExecReg", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure

        adap.SelectCommand.Parameters.AddWithValue("@Fname", Fname)
        adap.SelectCommand.Parameters.AddWithValue("@LName", LName)
        adap.SelectCommand.Parameters.AddWithValue("@Loc", Loc)

        adap.SelectCommand.Parameters.AddWithValue("@MNo", MNo)
        adap.SelectCommand.Parameters.AddWithValue("@EmailId", EmailId)
        adap.SelectCommand.Parameters.AddWithValue("@Password", Password)
        adap.SelectCommand.Parameters.AddWithValue("@Status", Status)
        adap.Fill(ds)
        Return ds

    End Function
    'End Function
    'Function for Sales Agent Deal Seat
    Public Function DealSeatDetails(ByVal SalesExecID As String, ByVal AName As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT DISTINCT Agent_CD.Dis, Agent_CD.Airline, Agent_CD.CB, Agent_CD.Dis_YQ, Agent_CD.Dis_B_YQ,New_Regs.Agency_Name, New_Regs.Fname + New_Regs.Lname AS Name FROM Agent_CD INNER JOIN New_Regs ON Agent_CD.Grade = New_Regs.Agent_Type WHERE (New_Regs.SalesExecID = '" & SalesExecID & "') and (New_Regs.Agency_Name =  '" & AName & "')ORDER BY Name", con)
        adap.Fill(ds)
        Return ds



    End Function
    'End Function
    'Functions for Sales Agency Name
    Public Function SelectAgencyName(ByVal Execname As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT Agency_Name,SalesExecID from New_Regs where SalesExecID='" & Execname & "'", con)
        adap.Fill(ds)
        Return ds



    End Function
    'Functions End

    'Functions for Sales Agent Profile
    Public Function SalesExecDetail() As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT * from SalesExecReg", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function SalesExecProfile(ByVal Execid As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT * from SalesExecReg where EmailId='" & Execid & "' ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function UpdateSalesExecProfile(ByVal id As String, ByVal Mno As String, ByVal pwd As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("UPDATE SalesExecReg set MobileNo='" & Mno & "',Password='" & pwd & "' where ID='" & id & "' ", con)
        adap.Fill(ds)
        Return ds



    End Function

    'Functions End
    'Functions for Sales Agent Ticket Report
    Public Function CurrentTicketReport(ByVal Execid As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "'   and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "') )   ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function TicketedTicketReport(ByVal Execid As String, ByVal tick As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "' and flight_booking.pnr_status='" & tick & "'   and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "') ) order by flight_booking.booking_date desc ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function RejectedTicketReport(ByVal Execid As String, ByVal tick As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "' and flight_booking.pnr_status='" & tick & "'   and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')     ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function CancelledTicketReport(ByVal Execid As String, ByVal tick As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "' and flight_booking.pnr_status='" & tick & "'   and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')     ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function TicketReportAgencyName(ByVal Execid As String, ByVal AgName As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "' and  flight_booking.Ag_Name='" & AgName & "' order by flight_booking.booking_date desc ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function TicketReportAgencyNameAndDate(ByVal Execid As String, ByVal AgName As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "' and  flight_booking.Ag_Name='" & AgName & "'  and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')) order by flight_booking.booking_date desc", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function TicketReportByDate(ByVal Execid As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT flight_booking.pnr_locator as PNR, flight_booking.booking_date as BookingDate, flight_booking.sector as Sector, flight_booking.total_booking_cost as TotalFare, flight_booking.pnr_status as Status, flight_booking.Ag_Name as AgencyName,flight_booking.user_id as UserID FROM flight_booking INNER JOIN New_Regs ON flight_booking.user_id = New_Regs.User_Id where New_Regs.SalesExecID='" & Execid & "'  and  (flight_booking.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "') order by flight_booking.booking_date desc", con)
        adap.Fill(ds)
        Return ds



    End Function
    'Function End
    'Function for Sales Agent Ledger Detail
    Public Function CurrentLedgerDetail(ByVal Execid As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT Trans_Report.* FROM New_Regs INNER JOIN Trans_Report ON New_Regs.User_Id = Trans_Report.user_id where New_Regs.SalesExecID='" & Execid & "' and  (Trans_Report.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')) ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function LedgerDetailByDate(ByVal Execid As String, ByVal From As String, ByVal [To] As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT Trans_Report.* FROM New_Regs INNER JOIN Trans_Report ON New_Regs.User_Id = Trans_Report.user_id where New_Regs.SalesExecID='" & Execid & "' and  (Trans_Report.booking_date Between CONVERT(datetime,'" & From & "') AND CONVERT(datetime,'" & [To] & "')) order by Trans_Report.booking_date desc ", con)
        adap.Fill(ds)
        Return ds



    End Function
    Public Function LedgerDetailByAgencyName(ByVal Execid As String, ByVal AgencyName As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("SELECT Trans_Report.* FROM New_Regs INNER JOIN Trans_Report ON New_Regs.User_Id = Trans_Report.user_id where New_Regs.SalesExecID='" & Execid & "' and New_Regs.Agency_Name='" & AgencyName & "'  ", con)
        adap.Fill(ds)
        Return ds



    End Function

    'Function End

    'Function to reset Controls
    Public Sub ResetFormControlValues(ByVal parent As Control)
        For Each c As Control In parent.Controls
            If c.Controls.Count > 0 Then
                ResetFormControlValues(c)
            Else
                Select Case c.[GetType]().ToString()
                    Case "System.Web.UI.WebControls.TextBox"
                        DirectCast(c, TextBox).Text = ""
                        Exit Select


                End Select
            End If
        Next
    End Sub
    'Function End
End Class
