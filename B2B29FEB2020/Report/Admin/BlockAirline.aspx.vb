
Partial Class SprReports_Admin_BlockAirline
    Inherits System.Web.UI.Page
    Dim SqlT As New SqlTransactionDom
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As String
        i = SqlT.AirlineEnableTrueFalse(txt_org.Text.Trim, txt_dest.Text.Trim, txt_flight.Text.Trim, txt_airline.Text.Trim, ddl_trip.SelectedValue)
        lblstatus.Text = i
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
