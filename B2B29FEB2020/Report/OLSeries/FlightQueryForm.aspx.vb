Imports System.Data
Partial Class SprReports_OLSeries_FlightQueryForm
    Inherits System.Web.UI.Page
    Dim series As New SeriesDepart
    Dim sqltran As New SqlTransaction


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                If (Session("User_Type") <> "AGENT") Then
                    Response.Redirect("~/Login.aspx")
                End If
            End If
            If Not IsPostBack Then
                If Request("Counter") <> "" AndAlso Request("Counter") IsNot Nothing AndAlso Session("UID").ToString() <> "" AndAlso Session("UID") IsNot Nothing Then
                    Try
                        tbl_query.Visible = True
                        tbl_msg.Visible = False
                        Dim counter As String = Request("Counter")
                        Dim AgentID As String = Session("UID").ToString()
                        Dim dt As New DataTable
                        dt = series.GetFlightDetails(counter, "")
                        lblairline.Text = dt.Rows(0)("AirlineName")
                        lblcode.Text = dt.Rows(0)("Airline_Code")
                        lbldeptdate.Text = dt.Rows(0)("Depart_Date")
                        lblretdate.Text = dt.Rows(0)("Ret_Date")
                        lblseats.Text = dt.Rows(0)("Available_Seat")
                        lblamt.Text = dt.Rows(0)("Amount")
                        lblsector.Text = dt.Rows(0)("Sector")
                        lblsid.Text = dt.Rows(0)("Counter")
                        lblTrip.Text = dt.Rows(0)("Trip")
                        lblexecid.Text = dt.Rows(0)("ExecId")
                    Catch ex As Exception

                    End Try


                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btn_post_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_post.Click
        Try
            Dim agencyname As DataSet
            Dim agName As String = ""
            agencyname = sqltran.GetAgencyDetails(Session("UID"))
            agName = agencyname.Tables(0).Rows(0)("Agency_Name").ToString()

            Dim counter As String = Request("Counter")
            Dim adt As Integer = 0
            Dim chd As Integer = 0
            Dim inf As Integer = 0
            If (Request("txtadult") <> "" AndAlso Request("txtadult") IsNot Nothing) Then
                adt = Convert.ToInt32(txtadult.Text)
            End If
            If (Request("txtchild") <> "" AndAlso Request("txtchild") IsNot Nothing) Then
                chd = Convert.ToInt32(txtchild.Text)
            End If
            If (Request("txtinfant") <> "" AndAlso Request("txtinfant") IsNot Nothing) Then
                inf = Convert.ToInt32(txtinfant.Text)
            End If
            Dim rmk As String = txtremark.Text.Trim
            Dim totpax As Integer = adt + chd + inf
            If (lblseats.Text >= totpax) Then
                Dim i As Integer = series.InsertSeriesRequest(lblairline.Text.Trim(), lblcode.Text.Trim(), lblsector.Text.Trim(), Convert.ToInt32(lblamt.Text.Trim()), Convert.ToInt32(lblseats.Text.Trim()), lbldeptdate.Text.Trim(), lblretdate.Text.Trim(), totpax, adt, chd, inf, rmk, Session("UID").ToString(), agName, lblsid.Text, lblTrip.Text, txtCntctPerson.Text.Trim, txtCntctPersonNo.Text.Trim, txtCntctEmailid.Text.Trim)
                series.Avl_HoldSeat(totpax, counter)
                Try


                    Dim mytable As String = ""
                    mytable += "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%;height: 200px; border-collapse: collapse'>"

                    mytable += "<tr>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "<td align='left'>"

                    mytable += "</td>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "</tr>"

                    mytable += "<tr>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "<td>"

                    mytable += "<table border='0' cellpadding='0' cellspacing='0' style='width: 70%; border-collapse: collapse'>"
                    mytable += "<tr valign='top'><td><br /><span style='font-family: Tahoma'><strong>"
                    mytable += " </strong></span>"
                    mytable += "</td>"
                    mytable += "<td> </td><td><br/></td></tr>"


                    mytable += "</table>"

                    mytable += "</td>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "</tr>"

                    mytable += "<tr>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "<td align='left'>"

                    mytable += "<table border='2' cellpadding='0' cellspacing='0' class='innerTEXT' style='width:70%;height: 200px;border-collapse: collapse'>"
                    mytable += "<tr><td colspan='2' align='center' valign='middle' style='color:Blue;'><strong>Series Departure</strong><br/><br/></td></tr>"


                    mytable += "<tr><td><b> Airline Name: </b></td><td>" & lblairline.Text & " </td></tr>"
                    mytable += "<tr><td><b> Sector : </b></td><td> " & lblsector.Text & " </td></tr>"

                    mytable += "<tr><td><b> Departure Date : </b></td><td>" & lbldeptdate.Text & " </td></tr>"


                    mytable += "<tr><td><b> Return Date  : </b></td><td>" & lblretdate.Text & " </td></tr>"
                    mytable += "<tr><td><b> Available Seats: </b></td><td>" & lblseats.Text & " </td></tr>"
                    mytable += "<tr><td><b> No Of Adult : </b></td><td>" & adt & " </td></tr>"
                    mytable += "<tr><td><b>  No Of Child : </b></td><td>" & chd & " </td></tr>"
                    mytable += "<tr><td><b>  No Of Infant : </b></td><td>" & inf & " </td></tr>"
                    mytable += "<tr><td><b>  Contact Name : </b></td><td>" & txtCntctPerson.Text.Trim & " </td></tr>"
                    mytable += "<tr><td><b>  Contact No : </b></td><td>" & txtCntctPersonNo.Text.Trim & " </td></tr>"
                    mytable += "<tr><td><b>  Contact EmailID : </b></td><td>" & txtCntctEmailid.Text.Trim & " </td></tr>"
                    mytable += "<tr><td><b>  Itinerary : </b></td><td>" & lblcode.Text & " </td></tr>"
                    mytable += "<tr><td><b>  Remark : </b></td><td>" & rmk & " </td></tr>"
                    mytable += "</table>"

                    mytable += "</td>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "</tr>"


                    mytable += "<tr>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "<td valign='top'>"



                    mytable += "</td>"

                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "</tr>"

                    mytable += "<tr>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "<td align='left'>"

                    mytable += "</td>"
                    mytable += "<td>"
                    mytable += "</td>"
                    mytable += "</tr>"
                    mytable += "</table>"

                    Dim STDom As New SqlTransactionDom
                    Dim MailDt As New DataTable
                    MailDt = STDom.GetMailingDetails(MAILING.OLSERIES_FQFORM.ToString().Trim(), Session("UID").ToString()).Tables(0)

                    Try
                        If (MailDt.Rows.Count > 0) Then
                            Dim Status As Boolean = False
                            Status = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())

                            If Status = True Then
                                Dim i1 As Integer = STDom.SendMail(lblexecid.Text, Request("txtCntctEmailid"), MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), mytable, MailDt.Rows(0)("SUBJECT").ToString(), "")

                            End If
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try


                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
                If i > 0 Then
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Details Submmitted Successfully');", True)
                    tbl_query.Visible = False
                    tbl_msg.Visible = True
                    tbl_msg.Height = "400px"

                End If
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('No  of Pax greater than greater than available seats ');", True)

            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
