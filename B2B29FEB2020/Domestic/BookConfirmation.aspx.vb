Imports System.Data

Partial Class Domestic_BookConfirmation
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        'If Session("BookIng") = "TRUE" Then
        '    lblTkt.Text = Session("DomStrTktCopy")
        'End If
        Try



            If Session("BookIng") = "TRUE" Then
                Dim OrderId As String = Request.QueryString("OrderId")

                lblTkt.Text = Session("DomStrTktCopy")
                Dim STDOM As New SqlTransactionDom
                Dim MailDt As New DataTable
                MailDt = STDOM.GetMailingDetails(MAILING.AIR_PNRSUMMARY.ToString(), Session("UID").ToString()).Tables(0)
                Dim ToEmail As String = ""

                Dim strMailMsgHold As String = ""
                Dim status1 As String = "1"

                Try

                    Dim ObjIntDetails As New IntlDetails()
                    Dim dt As New DataTable()

                    Dim TKTCopyPath As String = ""
                    dt = ObjIntDetails.Email_Credentilas(OrderId, "Ticket", "")
                    status1 = "2"
                    'If ToEmail.Length > 5 Then
                    '    ToEmail = ToEmail.Substring(2)

                    'Else
                    '    ToEmail = ".singh@galileo.co.in"
                    'End If



                    Try
                        If (MailDt.Rows.Count > 0) Then
                            status1 = "3"
                            Dim newDepDate As String = ""
                            'newDepDate = FltDs.Tables(0).Rows(0)("DepartureDate").ToString()
                            'newDepDate = newDepDate.Insert(4, "/")
                            'newDepDate = newDepDate.Insert(7, "/")
                            strMailMsgHold = "<table>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><h2>Domestic Failed Air Booking Details... </h2>"
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><b>Customer ID: </b>" + Session("UID").ToString()
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><b>Departure Date: </b>"
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><b>Pnr No: </b>"
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><b>Order ID: </b>" + OrderId
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "<tr>"
                            strMailMsgHold = strMailMsgHold & "<td><b>Fare: </b>"
                            strMailMsgHold = strMailMsgHold & "</td>"
                            strMailMsgHold = strMailMsgHold & "</tr>"
                            strMailMsgHold = strMailMsgHold & "</table>"
                            '' STDOM.SendMail(ToEmail, "info@RWT.com", "", "", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, " Domestic Ticket on Hold", "")

                            Try

                                For i As Integer = 0 To dt.Rows.Count - 1

                                    STDOM.SendMail(dt.Rows(i)(1).ToString(), "info@RWT.com", "", "", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, " Mail for Domestic Ticket on Hold", "")

                                Next

                            Catch exjj As Exception

                            End Try
                            status1 = "4"
                            '' STDOM.SendMail(".singh@galileo.co.in", "info@RWT.com", "", "", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), strMailMsgHold, " Mail for Domestic Ticket on Hold", "")

                        End If
                    Catch ex1 As Exception
                        Dim msg As String = ex1.Message & ex1.StackTrace.ToString() ''Response.Write(ex1.Message)


                        '' STDOM.SendMail(".singh@galileo.co.in", "info@RWT.com", "", "", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), msg, " Error mail for Domestic Ticket on Hold", "")
                    End Try

                Catch ex As Exception
                    Try
                        Dim msg As String = ex.Message & ex.StackTrace.ToString() ''Response.Write(ex1.Message)
                        '' STDOM.SendMail(".singh@galileo.co.in", " info@RWT.com", "", "", MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), msg, " Error mail for Domestic Ticket on Hold", "")
                    Catch ex44 As Exception

                    End Try

                End Try

            End If
        Catch ex2 As Exception

        End Try


    End Sub
End Class
