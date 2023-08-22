
Partial Class FlightDom_FltResult
    Inherits System.Web.UI.Page
    Dim objUM As New FltSearch1()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        ''Dim sQStr = "TripType=" & Request.QueryString("TripType") & "&txtDepCity1=" & Request.QueryString("txtDepCity1") & "&txtArrCity1=" _
        ''            & Request.QueryString("txtArrCity1") & "&hidtxtDepCity1=" & Request.QueryString("hidtxtDepCity1") _
        ''            & "&hidtxtArrCity1=" & Request.QueryString("hidtxtArrCity1") & "&Adult=" & Request.QueryString("Adult") _
        ''            & "&Child=" & Request.QueryString("Child") & "&Infant=" & Request.QueryString("Infant") & "&Cabin=" & Request.QueryString("Cabin") _
        ''            & "&txtAirline=" & Request.QueryString("txtAirline") & "&hidtxtAirline=" & Request.QueryString("hidtxtAirline") _
        ''            & "&txtDepDate=" & Request.QueryString("txtDepDate") & "&txtRetDate=" & Request.QueryString("txtRetDate") _
        ''            & "&RTF=" & Request.QueryString("RTF") & "&NStop=" & Request.QueryString("NStop") & "&RTF=" & Request.QueryString("RTF") & "&Trip=" _
        ''            & Request.QueryString("Trip") & "&GRTF=" & Request.QueryString("GRTF")

        Session("SearchCriteriaUser") = Request.Url
        Session("BookIng") = "FALSE"
        Session("IntBookIng") = "FALSE"

        Try

        Catch ex As Exception

            Response.Write(ex.ToString())


        End Try
    End Sub
End Class
