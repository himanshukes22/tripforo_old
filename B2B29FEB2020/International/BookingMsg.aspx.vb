
Partial Class FlightInt_BookingMsg
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim msg As String = ""
        msg = Request.QueryString("msg")
        If msg = "CL" Then
            lblMsg.Text = "Insufficient Credit Limit. Please contact administrator."
        ElseIf msg = "NA" Then
            lblMsg.Text = "You are not authorised for booking. Please contact administrator."
        ElseIf msg = "PRC" Then
            lblMsg.Text = "System is unable to price this itinerary, Please select another."
        ElseIf msg = "ML" Then
            lblMsg.Text = "The Meal or Baggage you selected is not avialable, Please try another search."
            'ElseIf msg = "SSR" Then
            '    lblMsg.Text = "The Baggage you selected is not avialable, Please try another search."  dropped/bounced/captured/auth/failed/usercancelled/pending  ‘failure’ or ‘pending’
        ElseIf msg = "PG" Then
            lblMsg.Text = "Payment transaction Aborted"
        ElseIf msg = "failure" Then
            lblMsg.Text = "Payment transaction failure"
        ElseIf msg = "pending" Then
            lblMsg.Text = "Payment transaction pending,Please contact to customer care."
        ElseIf msg = "dropped" Then
            lblMsg.Text = "Payment transaction dropped"
        ElseIf msg = "bounced" Then
            lblMsg.Text = "Payment transaction bounced"
        ElseIf msg = "auth" Then
            lblMsg.Text = "Payment transaction not authenticated,lease contact to customer care"
        ElseIf msg = "usercancelled" Then
            lblMsg.Text = "Payment transaction usercancelled"
        ElseIf msg = "failed" Then
            lblMsg.Text = "Payment transaction failed"
        ElseIf msg = "pending" Then
            lblMsg.Text = "Payment transaction pending"
        ElseIf msg = "captured" Then
            lblMsg.Text = "Please contact to customer care"
        ElseIf msg = "Requested" Then
            lblMsg.Text = "Not getting response from payment gateway,Please contact to customer care"
        ElseIf msg = "1" Then
            lblMsg.Text = "Please make another search for new booking."
        ElseIf msg = "2" Then
            lblMsg.Text = "Corrently our database is too busy, Please try after sometime."
        ElseIf msg = "TXN_FAILURE" Then
            lblMsg.Text = "Paytm Transaction Failed."
        End If

    End Sub
End Class
