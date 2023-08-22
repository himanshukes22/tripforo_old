<%@ WebService Language="VB" Class="TravelCalSrv" %>

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _

<ScriptService()> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
Public Class TravelCalSrv
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetFlightDetails_new_fltr(ByVal Agentid As String, ByVal loginid As String, ByVal user_Type As String, ByVal ddmonth As String, ByVal ddyear As String, ByVal VPNR As String, ByVal VPAXNAME As String, ByVal DEPCITY As String, ByVal ARRCITY As String, ByVal AIRLINE As String) As List(Of FltDetails)
        Dim AgencyDT As DataTable
        Return GetFlightDetailsByAgentId_New_fltr(Agentid, loginid, user_Type, ddmonth, ddyear, VPNR, VPAXNAME, DEPCITY, ARRCITY, AIRLINE)
    End Function

    Public Function GetFlightDetailsByAgentId_New_fltr(ByVal VAgencyName As String, ByVal loginid As String, ByVal user_Type As String, ByVal ddmonth As String, ByVal ddyear As String, ByVal VPNR As String, ByVal VPAXNAME As String, ByVal DEPCITY As String, ByVal ARRCITY As String, ByVal AIRLINE As String) As List(Of FltDetails)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Dim fltDList As New List(Of FltDetails)
        Try

            Dim vc As String = ""
            Dim SDEPCITY As String = ""
            Dim SARRCITY As String = ""
            Dim Agentid As String = ""

            If (Not String.IsNullOrEmpty(VAgencyName.Trim())) Then
                Agentid = VAgencyName.Split("(")(1).ToString().Replace(")", "")
            End If

            If (Not String.IsNullOrEmpty(AIRLINE)) Then
                vc = AIRLINE.Split(",")(1)
            End If
            If (Not String.IsNullOrEmpty(DEPCITY)) Then
                SDEPCITY = DEPCITY.Split(",")(0)
            End If
            If (Not String.IsNullOrEmpty(ARRCITY)) Then
                SARRCITY = ARRCITY.Split(",")(0)
            End If
            con.Open()
            Dim cmd As New SqlCommand("Sp_GetFlightDetailsForTravelCal_PNRCONTS_Filter1", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@usertype", user_Type)
            cmd.Parameters.AddWithValue("@LoginID", loginid)
            cmd.Parameters.AddWithValue("@PNR", VPNR.Trim())
            cmd.Parameters.AddWithValue("@AirlinePNR", vc)
            cmd.Parameters.AddWithValue("@PaxName", VPAXNAME.Trim())
            cmd.Parameters.AddWithValue("@AgentId", Agentid.Trim())
            cmd.Parameters.AddWithValue("@month", ddmonth.Trim())
            cmd.Parameters.AddWithValue("@year", ddyear.Trim())
            cmd.Parameters.AddWithValue("@Source", SDEPCITY.Trim())
            cmd.Parameters.AddWithValue("@destination", SARRCITY.Trim())

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            While dr.Read()
                fltDList.Add(New FltDetails() With {.DepDate = dr("DepDate").ToString().Trim(), .Pnr = dr("PnrCount").ToString().Trim()})
            End While

            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
            con.Dispose()
        End Try
        Return fltDList
    End Function




    <WebMethod()> _
    Public Function GetFlightDetails_new(ByVal loginid As String, ByVal user_Type As String, ByVal agentID As String, ByVal date1 As String) As List(Of FltDetails)
        Dim AgencyDT As DataTable
        Return GetFlightDetailsByAgentId_New(loginid, user_Type, agentID, date1)
    End Function

    Public Function GetFlightDetailsByAgentId_New(ByVal loginid As String, ByVal user_Type As String, ByVal agentID As String, ByVal date1 As String) As List(Of FltDetails)


        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Dim DateFarmate() As String
        DateFarmate = date1.Split("-")
        If (Convert.ToInt16(DateFarmate(1)) = 0) Then
            date1 = 1
            date1 = DateFarmate(0) + "-" + (Convert.ToInt16(DateFarmate(1)) + 1).ToString() + "-" + DateFarmate(2)
            date1 = Convert.ToDateTime(date1).ToString("ddMMyy")
            '   var validdate = validDate.split('-');
            '  if (parseInt(validdate[1]) == 0) {

            '      validDate = validdate[0] + "-" + (parseInt(validdate[1]) + 1).toString() + "-" + validdate[2];
            '}


        Else
            date1 = Convert.ToDateTime(date1).AddMonths(1).ToString("ddMMyy")
        End If
        Dim fltDList As New List(Of FltDetails)
        Try

            Dim cmd As New SqlCommand("Sp_GetFlightDetailsForTravelCal_PNRCONTS", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@LoginID", loginid.Trim())
            cmd.Parameters.AddWithValue("@usertype", user_Type.Trim())
            cmd.Parameters.AddWithValue("@AgentID", agentID.Trim())
            'cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date1).AddMonths(1).ToString("ddMMyy"))
            cmd.Parameters.AddWithValue("@date", date1)
            con.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()


            While dr.Read()
                fltDList.Add(New FltDetails() With {.DepDate = dr("DepDate").ToString().Trim(), .Pnr = dr("PnrCount").ToString().Trim()})
            End While
            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
            con.Dispose()
        End Try
        Return fltDList
    End Function


    <WebMethod()>
    Public Function GetFlightDetailsFare_new_fltr(ByVal Agentid As String, ByVal loginid As String, ByVal user_Type As String, ByVal ddmonth As String, ByVal ddyear As String, ByVal VPNR As String, ByVal VPAXNAME As String, ByVal DEPCITY As String, ByVal ARRCITY As String, ByVal AIRLINE As String) As List(Of FltDetails)
        Dim AgencyDT As DataTable
        Return GetFlightCalendorDetailsByAgentId_New_fltr(Agentid, loginid, user_Type, ddmonth, ddyear, VPNR, VPAXNAME, DEPCITY, ARRCITY, AIRLINE)
    End Function
    Public Function GetFlightCalendorDetailsByAgentId_New_fltr(ByVal VAgencyName As String, ByVal loginid As String, ByVal user_Type As String, ByVal ddmonth As String, ByVal ddyear As String, ByVal VPNR As String, ByVal VPAXNAME As String, ByVal DEPCITY As String, ByVal ARRCITY As String, ByVal AIRLINE As String) As List(Of FltDetails)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString())
        Dim i As Integer = 0
        Dim fltDList As New List(Of FltDetails)
        Try

            Dim vc As String = ""
            Dim SDEPCITY As String = ""
            Dim SARRCITY As String = ""
            Dim Agentid As String = ""

            If (Not String.IsNullOrEmpty(VAgencyName.Trim())) Then
                Agentid = VAgencyName.Split("(")(1).ToString().Replace(")", "")
            End If

            If (Not String.IsNullOrEmpty(AIRLINE)) Then
                vc = AIRLINE.Split(",")(1)
            End If
            If (Not String.IsNullOrEmpty(DEPCITY)) Then
                SDEPCITY = DEPCITY.Split(",")(0)
            End If
            If (Not String.IsNullOrEmpty(ARRCITY)) Then
                SARRCITY = ARRCITY.Split(",")(0)
            End If
            con.Open()
            Dim cmd As New SqlCommand("usp_farecolender", con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            While dr.Read()
                fltDList.Add(New FltDetails() With {.DepDate = dr("Departure_Date").ToString().Trim(), .Pnr = dr("Grand_Total").ToString().Trim(), .DepTime = dr("DepartureTime").ToString().Trim(), .Sector = dr("OrgDestFrom").ToString() & "#" & dr("OrgDestTo").ToString()})
            End While

            con.Close()
        Catch ex As SqlException
            'throw ex;
            ' ex.ToString();

        Finally
            con.Dispose()
        End Try
        Return fltDList
    End Function

End Class
