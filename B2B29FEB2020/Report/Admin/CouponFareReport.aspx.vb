Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Partial Class SprReports_Admin_CouponFareReport
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim adp As SqlDataAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then
                LoadBind()
            End If
        End If
    End Sub
    Private Function GetCouponFareReport(ByVal fromdate As String, ByVal todate As String, ByVal trackid As String, ByVal st As String) As DataTable
        Dim dt As New DataTable()

        Try
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            adp = New SqlDataAdapter("SP_COU_CouponFareReport", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate)
            adp.SelectCommand.Parameters.AddWithValue("@to", todate)
            adp.SelectCommand.Parameters.AddWithValue("@trackid", trackid)
            adp.SelectCommand.Parameters.AddWithValue("@st", st)
            adp.Fill(dt)
        Catch ex As Exception
            Dim col As New DataColumn()
            col.ColumnName = "Error Massage"
            col.DefaultValue = ex.Message
            dt.Columns.Add(col)

            Return dt
        End Try
        Return dt
    End Function

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Dim FromDate As String
        Dim ToDate As String
        Dim trackid As String = ""
        Dim st As String = ""
        Try
            
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If

            If txt_tid.Text <> "" AndAlso txt_tid.Text IsNot Nothing Then
                trackid = txt_tid.Text
            Else
                trackid = ""
            End If

            If (ddlstatus.SelectedIndex > 0) Then
                st = ddlstatus.SelectedItem.Text
            End If

            Dim dt As New DataTable
            dt = GetCouponFareReport(FromDate, ToDate, trackid, st)
            If dt.Rows.Count > 0 Then
                grdCoupon.DataSource = dt
                grdCoupon.DataBind()
            lblprft.Text = "INR " & dt.Rows(0)("totProfit")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadBind()
        Try
            Dim curr_date As String = System.DateTime.Now
            Dim curr_date1 As String = System.DateTime.Now
            Dim status As String = ""
            Dim dt As New DataTable
            If Session("USER_TYPE").ToString.ToUpper = "ADMIN" Then
                dt = GetCouponFareReport(curr_date.Split(" ")(0) & " " & "12:00:00 AM", curr_date1.Split(" ")(0) & " " & "11:59:59 PM", "", "")
                If dt.Rows.Count > 0 Then
                    grdCoupon.DataSource = dt
                    grdCoupon.DataBind()
                    lblprft.Text = "INR " & dt.Rows(0)("totProfit")
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    
End Class
