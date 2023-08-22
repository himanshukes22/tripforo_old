Imports System.Data

Partial Class FlightInt_BookConfirmation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        'If Session("IntBookIng") = "TRUE" Then
        '    lblTkt.Text = Session("IntStrTktCopy")
        'End If



        If Session("IntBookIng") = "TRUE" Then
            Try
                lblTkt.Text = Session("IntStrTktCopy")

                Dim ObjIntDetails As New IntlDetails()
                Dim OrderId As String = Request.QueryString("OrderId")
                Dim dt As New DataTable()
                Dim ToEmail As String = ""
                Dim TKTCopyPath As String = ""
                dt = ObjIntDetails.Email_Credentilas(OrderId, "Ticket", "")
                For i As Integer = 0 To dt.Rows.Count - 1
                    ToEmail += "," + dt.Rows(i)(1).ToString()
                Next
                Dim STDOM As New SqlTransactionDom
                Dim MailDt As New DataTable
                MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)
                Try
                    If (MailDt.Rows.Count > 0) Then
                        TKTCopyPath = Session("strFileNmPdf")
                        STDOM.SendMail(ToEmail, "info@RWT.com", "", MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), "", "Ticket On Hold", TKTCopyPath)
                    End If
                Catch ex As Exception

                End Try


            Catch ex As Exception

            End Try
        End If


    End Sub
End Class
