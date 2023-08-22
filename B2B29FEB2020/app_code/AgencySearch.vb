Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ScriptService()> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AgencySearch
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
 Public Function FetchAgencyList(ByVal city As String) As List(Of City)
        Dim AgencyDS As DataSet
        Dim objSqlTrans As New SqlTransactionNew
        AgencyDS = objSqlTrans.GetAgencyList(city, HttpContext.Current.Session("UserType"), HttpContext.Current.Session("UID"))
        Dim Ag = New City()
        Dim fetchAgency = Ag.GetAgencyList(AgencyDS.Tables(0))
        Return fetchAgency.ToList()
        ' Return fetchAgency.ToArray()
    End Function
    <WebMethod()> _
   Public Function GetFlightDetails(ByVal agentID As String, ByVal date1 As String) As List(Of FltDetails)
        Dim AgencyDT As DataTable

        Dim objSqlTrans As New SqlTransactionDom
        Return objSqlTrans.GetFlightDetailsByAgentId(agentID, date1)

    End Function


    <WebMethod()> _
  Public Function GetFlightDetailsByDateForCal(ByVal agentID As String, ByVal date1 As String) As List(Of FltDetails)
        Dim AgencyDT As DataTable

        Dim objSqlTrans As New SqlTransactionDom
        Return objSqlTrans.GetFlightDetailsByDateForCal(agentID, date1)

    End Function

End Class
