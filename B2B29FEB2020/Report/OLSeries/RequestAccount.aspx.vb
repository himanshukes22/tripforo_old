Imports System.Data
Partial Class SprReports_OLSeries_RequestAccount
    Inherits System.Web.UI.Page
    Dim series As New SeriesDepart

    Protected Sub btn_post_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_post.Click
        Try
            Dim agentid As String = Request("hidtxtAgencyName")
            Dim Ag As String() = Request("txtAgencyName").Split("("c)
            Dim agencyname As String = Ag(0).ToString
            Dim amount As Integer = Convert.ToInt32(Request("txtamount"))
            Dim remark As String = Request("txtremark")
            Dim i As Integer = series.InserSeriesAccount(agencyname.Trim(), agentid, amount, remark.Trim(), Session("UID").ToString, "Pending")
            If i > 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Details Submmitted Successfully');", True)
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            If (Session("User_Type") <> "SALES") Then
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub
End Class
