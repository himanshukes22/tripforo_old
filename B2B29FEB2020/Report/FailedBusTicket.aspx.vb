Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class BS_Admin_FailedBusTicket
    Inherits System.Web.UI.Page
    Dim adp As SqlDataAdapter
    Dim con As SqlConnection
    Dim dt As New DataTable
    Dim splitfromDate As String()
    Dim splittoDate As String()
    Dim fromdate As String
    Dim todate As String
    Dim orderid As String
    Dim agentid As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Try

                splitfromDate = DateTime.Now.ToString().Split("/")
                fromdate = splitfromDate(0).Trim() + "-" + splitfromDate(1).Trim() + "-" + splitfromDate(2).Substring(0, 4).Trim()
                splittoDate = DateTime.Now.ToString().Split("/")
                todate = splittoDate(0).Trim() + "-" + splittoDate(1).Trim() + "-" + splittoDate(2).Substring(0, 4).Trim()

                dt = getFailedBooking(fromdate, todate, Nothing, Nothing)
                If (dt.Rows.Count > 0) Then
                    grdfailed.DataSource = dt
                    grdfailed.DataBind()
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Function getFailedBooking(ByVal fromdate As String, ByVal todate As String, ByVal orderid As String, ByVal agentid As String) As DataTable

        Try
            Dim connection As String = ConfigurationManager.ConnectionStrings("myCon").ConnectionString.ToString()
            con = New SqlConnection(connection)
            adp = New SqlDataAdapter("SP_RB_GET_FAILED_TICKET_BOOKINGLOG", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate)
            adp.SelectCommand.Parameters.AddWithValue("@todate", todate)
            adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid)
            adp.SelectCommand.Parameters.AddWithValue("@agentid", agentid)
            adp.SelectCommand.Parameters.AddWithValue("@tblRef", "BookReqRes")
            adp.Fill(dt)
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        dt.Clear()
        Try
            If Request("From").ToString() <> Nothing Then
                splitfromDate = Request("From").ToString().Split("-")
                fromdate = splitfromDate(1).Trim() + "-" + splitfromDate(0).Trim() + "-" + splitfromDate(2)
            Else
                splitfromDate = DateTime.Now.ToString().Split("/")
                fromdate = splitfromDate(0).Trim() + "-" + splitfromDate(1).Trim() + "-" + splitfromDate(2).Substring(0, 4).Trim()
            End If
            If Request("To").ToString() <> Nothing Then
                splittoDate = Request("From").ToString().Split("-")
                todate = splittoDate(1).Trim() + "-" + splittoDate(0).Trim() + "-" + splittoDate(2)
            Else
                splittoDate = DateTime.Now.ToString().Split("/")
                todate = splittoDate(0).Trim() + "-" + splittoDate(1).Trim() + "-" + splittoDate(2).Substring(0, 4).Trim()
            End If
            If Request("hidtxtAgencyName").ToString() <> Nothing Then
                agentid = Request("hidtxtAgencyName").ToString()
            Else
                agentid = Nothing
            End If
            If (txtOrderID.Text <> Nothing) Then
                orderid = txtOrderID.Text()
            Else
                orderid = Nothing
            End If
            dt = getFailedBooking(fromdate, todate, orderid, agentid)
            If (dt.Rows.Count > 0) Then
                grdfailed.DataSource = dt
                grdfailed.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
