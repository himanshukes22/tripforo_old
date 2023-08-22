Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports IRC_BAL
Imports IRC_DAL

Imports EBLBill
Imports IRC_SHARED

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AutoComplete
    Inherits System.Web.Services.WebService
    <WebMethod()> _
    Public Function GETCITYSTATE(ByVal INPUT As String, ByVal SEARCH As String) As List(Of String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable
        Dim adap As SqlDataAdapter
        adap = New SqlDataAdapter("SP_GET_STATECITY_v1", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@INPUT", INPUT)
        adap.SelectCommand.Parameters.AddWithValue("@SEARCH", "STATE")
        adap.SelectCommand.Parameters.AddWithValue("@Prefix", SEARCH)
        adap.Fill(dt)

        Dim CityList As New List(Of String)()
        For i As Integer = 0 To dt.Rows.Count - 1
            CityList.Add(Convert.ToString(dt.Rows(i)("CITY")))
        Next
        Return CityList
    End Function

    '   <WebMethod()> _
    'Public Function GetCityList(ByVal city As String) As List(Of FetchRecord)
    '       Dim dt As DataTable
    '       Dim con As New SqlConnection(Common.cs)
    '       Dim cmd As SqlCommand = Nothing
    '       cmd = New SqlCommand("SP_EBL_GETCITYLIST", con)
    '       con.Open()
    '       cmd.CommandText = "SP_EBL_GETCITYLIST"
    '       cmd.CommandType = CommandType.StoredProcedure
    '       cmd.Parameters.AddWithValue("@City", city)
    '       Dim da As New SqlDataAdapter(cmd)
    '       dt = New DataTable()
    '       da.Fill(dt)
    '       con.Close()

    '       Dim obj As New FetchRecord()
    '       Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchCityList(dt)

    '       Return list

    '   End Function

    '   <WebMethod()> _
    '   Public Function GetServiceList(ByVal service As String, ByVal cityId As Integer, ByVal utility As String) As List(Of FetchRecord)
    '       Dim dt As DataTable
    '       Dim con As New SqlConnection(Common.cs)
    '       con.Open()
    '       Dim cmd As New SqlCommand("SP_EBL_GETSERVICELIST", con)
    '       cmd.CommandText = "SP_EBL_GETSERVICELIST"
    '       cmd.CommandType = CommandType.StoredProcedure
    '       cmd.Parameters.AddWithValue("@Service", service)
    '       cmd.Parameters.AddWithValue("@CityId", cityId)
    '       cmd.Parameters.AddWithValue("@Utility", utility)
    '       Dim da As New SqlDataAdapter(cmd)
    '       dt = New DataTable()
    '       da.Fill(dt)
    '       con.Close()
    '       Dim obj As New FetchRecord()
    '       Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchServiceList(dt)

    '       'if (dr.HasRows)
    '       '{
    '       '    dr.Read();
    '       '    list.Add(dr["CityName"].ToString().Trim());

    '       '}
    '       Return list

    '   End Function

    '   <WebMethod()> _
    '   Public Function GetServiceInputs(ByVal cityId As Integer, ByVal serviceId As Integer) As List(Of FetchRecord)
    '       Dim dt As DataTable
    '       Dim con As New SqlConnection(Common.cs)
    '       con.Open()
    '       Dim cmd As New SqlCommand("SP_EBL_GETSERVICEINPUTS", con)
    '       cmd.CommandText = "SP_EBL_GETSERVICEINPUTS"
    '       cmd.CommandType = CommandType.StoredProcedure

    '       cmd.Parameters.AddWithValue("@CityId", cityId)
    '       cmd.Parameters.AddWithValue("@ServiceId", serviceId)
    '       Dim da As New SqlDataAdapter(cmd)
    '       dt = New DataTable()
    '       da.Fill(dt)
    '       con.Close()
    '       Dim obj As New FetchRecord()
    '       If dt.Rows.Count = 0 Then
    '           ' Code to update inputlist in DB
    '           Dim objCommon As New Common()

    '           Dim response As String = objCommon.objEnquiry.objEnquiryServices.GetServiceDetails(serviceId, cityId, Common.Format)
    '           Dim dsResponse As New DataSet()
    '           dsResponse.ReadXml(New System.IO.StringReader(response))
    '           con = New SqlConnection(Common.cs)
    '           con.Open()
    '           Dim mandate As Integer
    '           Dim billPresent As Integer
    '           cmd = New SqlCommand("SP_EBL_UPDATESERVICEINPUTS", con)
    '           cmd.CommandText = "SP_EBL_UPDATESERVICEINPUTS"
    '           cmd.CommandType = CommandType.StoredProcedure
    '           For i As Integer = 0 To dsResponse.Tables(0).Rows.Count - 1
    '               If dsResponse.Tables(0).Rows(i)("Mandatory").ToString().ToUpper() = "TRUE" Then
    '                   mandate = 1
    '               Else
    '                   mandate = 0
    '               End If
    '               If dsResponse.Tables(0).Rows(i)("BillPresent").ToString().ToUpper() = "TRUE" Then
    '                   billPresent = 1
    '               Else
    '                   billPresent = 0
    '               End If
    '               Try
    '                   cmd.Parameters.Clear()
    '                   cmd.Parameters.AddWithValue("@InputName", dsResponse.Tables(0).Rows(i)("ServiceInputName").ToString())
    '                   cmd.Parameters.AddWithValue("@InputType", dsResponse.Tables(0).Rows(i)("ServiceInputType").ToString())
    '                   cmd.Parameters.AddWithValue("@InputLength", dsResponse.Tables(0).Rows(i)("ServiceInputLength").ToString())
    '                   cmd.Parameters.AddWithValue("@Mandatory", mandate)
    '                   cmd.Parameters.AddWithValue("@BillPresent", billPresent)
    '                   cmd.Parameters.AddWithValue("@CityId", cityId)
    '                   cmd.Parameters.AddWithValue("@ServiceId", serviceId)
    '                   cmd.ExecuteNonQuery()
    '               Catch ex As Exception
    '               End Try
    '           Next
    '           'Code End

    '           Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchServiceInputs(dsResponse.Tables(0))
    '           Return list
    '       Else
    '           Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchServiceInputs(dt)
    '           Return list
    '       End If
    '   End Function


    '<WebMethod()> _
    'Public Function GetAllUtility() As List(Of FetchRecord)
    '    Dim dt As DataTable
    '    Dim con As New SqlConnection(Common.cs)
    '    con.Open()
    '    Dim cmd As New SqlCommand("SP_EBL_GETALLUTILITY", con)
    '    cmd.CommandText = "SP_EBL_GETALLUTILITY"
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Dim da As New SqlDataAdapter(cmd)
    '    dt = New DataTable()
    '    da.Fill(dt)
    '    con.Close()
    '    Dim obj As New FetchRecord()
    '    Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchAllUtility(dt)

    '    'if (dr.HasRows)
    '    '{
    '    '    dr.Read();
    '    '    list.Add(dr["CityName"].ToString().Trim());

    '    '}
    '    Return list

    'End Function

    '<WebMethod()> _
    'Public Function GetAgentList(ByVal agency As String) As List(Of FetchRecord)
    '    Dim dt As DataTable
    '    Dim con As New SqlConnection(Common.cs)
    '    con.Open()
    '    Dim cmd As New SqlCommand("SP_EBL_GETAGENTLIST", con)
    '    cmd.CommandText = "SP_EBL_GETAGENTLIST"
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@agent", agency)
    '    Dim da As New SqlDataAdapter(cmd)
    '    dt = New DataTable()
    '    Try
    '        da.Fill(dt)
    '    Catch ex As Exception
    '    End Try
    '    con.Close()
    '    Dim obj As New FetchRecord()
    '    Dim list As System.Collections.Generic.List(Of FetchRecord) = obj.FetchAgentList(dt)

    '    'if (dr.HasRows)
    '    '{
    '    '    dr.Read();
    '    '    list.Add(dr["CityName"].ToString().Trim());

    '    '}
    '    Return list

    'End Function
    '    <WebMethod()> _
    'Public Function GetStationList(ByVal stname As String) As List(Of IRCSearch)
    '        Dim dset As DataSet = Nothing
    '        Dim objDAL As New IRCTCDAL()
    '        dset = objDAL.GetStationList(stname)
    '        Dim getStationList__1 As List(Of IRCSearch) = objDAL.getStList(dset.Tables(0))
    '        Return getStationList__1
    '    End Function
    '<WebMethod()> _
    '    Public Function GETSOURCEDESTINATION(ByVal prefixText As String) As List(Of abc)
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDBIRCTC").ConnectionString)
    '    Dim da As New SqlDataAdapter("SP_IRC_SOURCE_DESTINATION_AUTO_SEARCH", con)
    '    da.SelectCommand.CommandType = CommandType.StoredProcedure
    '    da.SelectCommand.Parameters.AddWithValue("@Param", prefixText)
    '    Dim dt As New DataTable()
    '    da.Fill(dt)
    '    Dim result As New List(Of abc)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        result.Add(New abc() With {.Station_Name = dt.Rows(i)("STATION_NAME").ToString().Trim(), .Station_Code = dt.Rows(i)("STATION_CODE").ToString().Trim()})
    '    Next
    '    Return result
    'End Function

    '#Region "[Following function has been created for getting all rail booking journey dates with no of bookings having in the same dates,created by pawan on 5th feb 2014]"
    '    <WebMethod()> _
    '    Public Function GetNoOfBkgsWithDates(ByVal type As String, ByVal month As String, ByVal year As String, ByVal [date] As String, ByVal agtID As String) As List(Of IRC_SHARED.RailBookingInfo)
    '        Dim lstBkgsInfo As List(Of IRC_SHARED.RailBookingInfo) = New List(Of RailBookingInfo)()
    '        Dim dr As SqlDataReader
    '        Dim conbkginfo As SqlConnection
    '        Try
    '            conbkginfo = New SqlConnection(ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString)
    '            conbkginfo.Open()
    '            Dim cmd As New SqlCommand("USP_RAIL_BKG_WITH_JDATE", conbkginfo)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("@TYPE", type)
    '            cmd.Parameters.AddWithValue("@YEAR", year)
    '            cmd.Parameters.AddWithValue("@MONTH", month)
    '            cmd.Parameters.AddWithValue("@JDATE", [date])
    '            cmd.Parameters.AddWithValue("@AGTID", agtID)
    '            dr = cmd.ExecuteReader()
    '            If dr.HasRows Then
    '                If type = "GET_BKG_DATES" Then
    '                    While dr.Read()
    '                        lstBkgsInfo.Add(New IRC_SHARED.RailBookingInfo() With { _
    '                            .NoOfBooking = dr("No_Of_Bookings").ToString(), _
    '                            .JourneyDates = dr("JourneyDate").ToString() _
    '                        })
    '                    End While
    '                ElseIf type = "GET_PNR_DETAILS" Then
    '                    While dr.Read()
    '                        lstBkgsInfo.Add(New IRC_SHARED.RailBookingInfo() With { _
    '                            .PNR = dr("PNR").ToString(), _
    '                            .Source = dr("SOURCE").ToString(), _
    '                            .Destination = dr("DESTINATION").ToString(), _
    '                            .MobileNo = dr("MOBILE").ToString(), _
    '                            .RefNo = dr("ReferenceNo").ToString(), _
    '                            .Id = dr("ID").ToString(), _
    '                            .TransactionID = dr("TRANSACTIONID").ToString() _
    '                        })
    '                    End While
    '                End If
    '            End If
    '            dr.Close()
    '            cmd.Dispose()
    '            conbkginfo.Close()
    '            Return lstBkgsInfo
    '        Catch ex As Exception
    '            Return lstBkgsInfo
    '        End Try
    '    End Function
    '#End Region

    '#Region "[Following function has been created for getting no of bookings booked on current date and based on different statuses, created by pawan on 8th feb 2014]"
    '    <WebMethod()> _
    '    Public Function GetVariousStatus(ByVal todaydate As String, ByVal calltype As String) As List(Of IRC_SHARED.RailBookingInfo)
    '        Dim lstNums As List(Of IRC_SHARED.RailBookingInfo) = New List(Of RailBookingInfo)()
    '        Try
    '            Dim connum As New SqlConnection(ConfigurationManager.ConnectionStrings("IRCTC").ConnectionString)
    '            Dim cmd As New SqlCommand("USP_GET_ALL_BOOKING_STATUS_NUMBERS", connum)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("@CURRDATE", todaydate)
    '            cmd.Parameters.AddWithValue("@TYPE", calltype)
    '            connum.Open()
    '            Dim dr As SqlDataReader = cmd.ExecuteReader()
    '            If dr.HasRows Then
    '                While dr.Read()
    '                    lstNums.Add(New IRC_SHARED.RailBookingInfo() With { _
    '                        .ProcessNums = dr("PROCESS").ToString(), _
    '                        .CancelNums = dr("BOOKED").ToString(), _
    '                        .BookedNums = dr("CANCEL").ToString() _
    '                    })
    '                End While
    '            End If
    '            dr.Close()
    '            cmd.Dispose()
    '            connum.Close()
    '            Return lstNums
    '        Catch ex As Exception
    '            Return lstNums
    '        End Try
    '    End Function
    '#End Region


End Class
Public Class abc
    Public Property Station_Name() As String
        Get
            Return m_Station_Name
        End Get
        Set(ByVal value As String)
            m_Station_Name = value
        End Set
    End Property
    Private m_Station_Name As String
    Private m_Station_Code As String

    Public Property Station_Code() As String
        Get
            Return m_Station_Code
        End Get
        Set(ByVal value As String)
            m_Station_Code = value
        End Set
    End Property
End Class