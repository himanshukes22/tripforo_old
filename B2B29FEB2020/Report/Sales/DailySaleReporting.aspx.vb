Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Partial Class SprReports_Sales_DailySaleReporting
    Inherits System.Web.UI.Page
    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim cmd As SqlCommand
    Private STDom As New SqlTransactionDom
    Dim dt As DataTable
    Dim dtsales As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtsales = STDom.GetSalesRef().Tables(0)
        If Not IsPostBack Then
            Try
                If Session("User_Type") = "SALES" Then
                    td_salesref.Visible = False
                End If


                Sales_DDL.AppendDataBoundItems = True
                Sales_DDL.Items.Clear()
                Sales_DDL.Items.Insert(0, "--Select--")
                Sales_DDL.DataSource = dtsales
                Sales_DDL.DataTextField = "Name"
                Sales_DDL.DataValueField = "EmailId"
                Sales_DDL.DataBind()


            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If


    End Sub
    Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            gridbind()
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        End Try
    End Sub
    Public Sub gridbind()
        Try
            Dim FromDate As String
            Dim ToDate As String
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
            Dim salesref As String = ""
            If (Sales_DDL.SelectedIndex > 0) Then
                salesref = Sales_DDL.SelectedValue
            End If

            Dim adap As New SqlDataAdapter("sp_get_salesreport", cn)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@Fromdate", FromDate)
            adap.SelectCommand.Parameters.AddWithValue("@ToDate", ToDate)
            adap.SelectCommand.Parameters.AddWithValue("@SalesRef", salesref)
            adap.SelectCommand.Parameters.AddWithValue("@Type", Session("User_type"))
            adap.SelectCommand.Parameters.AddWithValue("@LoginID", Session("UID"))
            Dim dt As New DataTable
            adap.Fill(dt)
            Session("griddt") = dt
            gvDSL.DataSource = dt

            gvDSL.DataBind()
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        End Try
    End Sub
    Protected Sub gvDSL_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDSL.RowEditing
        gvDSL.EditIndex = e.NewEditIndex
        gvDSL.DataSource = Session("griddt")
        gvDSL.DataBind()
        'bind()
    End Sub
    Public Sub bind()
        Dim adap As New SqlDataAdapter("select * from tbl_DailySalesRpt", cn)
        Dim dt As New DataTable
        adap.Fill(dt)
        gvDSL.DataSource = dt
        gvDSL.DataBind()

    End Sub

    Protected Sub gvDSL_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvDSL.RowCancelingEdit
        gvDSL.EditIndex = -1
        gvDSL.DataSource = Session("griddt")
        gvDSL.DataBind()
    End Sub

    Protected Sub gvDSL_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDSL.RowUpdating
        Try
            cn.Open()

            Dim gvr As GridViewRow = DirectCast(gvDSL.Rows(e.RowIndex), GridViewRow)
            Dim lblcounter As Label = DirectCast(gvr.FindControl("lblcounter"), Label)
            Dim lbldate As Label = DirectCast(gvr.FindControl("lbldate"), Label)
            Dim txtagencycity As TextBox = DirectCast(gvr.FindControl("txtagencycity"), TextBox)
            Dim txtagencyname As TextBox = DirectCast(gvr.FindControl("txtagencyname"), TextBox)
            Dim txtctcperson As TextBox = DirectCast(gvr.FindControl("txtctcperson"), TextBox)
            Dim txtctcpersonno As TextBox = DirectCast(gvr.FindControl("txtctcpersonno"), TextBox)
            Dim txtremark As TextBox = DirectCast(gvr.FindControl("txtremark"), TextBox)
            Dim cmd As New SqlCommand("sp_updatesalesRpt", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@counter", lblcounter.Text)
            cmd.Parameters.AddWithValue("@date", lbldate.Text)
            cmd.Parameters.AddWithValue("@agencycity", txtagencycity.Text)
            cmd.Parameters.AddWithValue("@agencyname", txtagencyname.Text)
            cmd.Parameters.AddWithValue("@ctcperson", txtctcperson.Text)
            cmd.Parameters.AddWithValue("@ctcpersonno", txtctcpersonno.Text)
            cmd.Parameters.AddWithValue("@remark", txtremark.Text)
            cmd.ExecuteNonQuery()
            cn.Close()
            gvDSL.EditIndex = -1
            gridbind()

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub gvDSL_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDSL.RowDeleting
        Try
            cn.Open()
            Dim gvr As GridViewRow = DirectCast(gvDSL.Rows(e.RowIndex), GridViewRow)
            Dim lblcounter As Label = DirectCast(gvr.FindControl("lblcounter"), Label)
            Dim cmd As New SqlCommand("delete tbl_DailySalesRpt where counter='" + lblcounter.Text + "'", cn)
            cmd.ExecuteNonQuery()
            cn.Close()
            gridbind()
        Catch ex As Exception

        End Try

    End Sub
End Class
