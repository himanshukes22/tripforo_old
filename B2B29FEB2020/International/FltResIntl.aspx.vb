Imports System.Data

Partial Class FltResIntl
    Inherits System.Web.UI.Page

    Dim SearchValue As String
    Dim Origin() As String, Destination() As String
    Dim SearchQuery As New Hashtable
    Dim ObjIntFlt As New IntFltResult
    Dim str As String
    Dim IntAirDs As New DataSet
    Dim IntAirDt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Try
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")

        End If
        Session("SearchCriteriaUser") = Request.Url
       Session("BookIng") = "FALSE"
        Session("IntBookIng") = "FALSE"
        'End Try
    End Sub
End Class
