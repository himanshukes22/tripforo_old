Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Partial Class SprReports_Admin_MISCSRVCHARGE
    Inherits System.Web.UI.Page
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)

    Private ds As New DataSet()
   
    Private dt As New DataTable()

 
    Public Function GroupTypeMGMT(ByVal type As String, ByVal desc As String, ByVal cmdType As String, ByRef msg As String) As DataTable
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim dt As New DataTable()
        Try
            con.Open()
            Dim cmd As New SqlCommand()
            cmd.CommandText = "usp_agentTypeMGMT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 200).Value = type
            cmd.Parameters.Add("@desc", SqlDbType.VarChar, 500).Value = desc
            cmd.Parameters.Add("@cmdType", SqlDbType.VarChar, 50).Value = cmdType
            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 500)
            cmd.Parameters("@msg").Direction = ParameterDirection.Output
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            msg = cmd.Parameters("@msg").Value.ToString().Trim()
            con.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            con.Close()
        End Try
        Return dt
    End Function
    Public Sub reset()
        ddl_airline.SelectedIndex = 0
        ddl_TripType.SelectedIndex = 0
        'TXTAgentId.Text = ""
        ddl_GroupType.SelectedIndex = 0
        TXTOrg.Text = ""
        TXTAmount.Text = ""
        TXTDest.Text = ""
    End Sub

    Protected Sub SAVE_Click(Sender As [Object], e As EventArgs)
        Try
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "ALL", "ALL", Request("hidtxtAgencyName"))
            'Checking for entry
            Dim cmd1 As New SqlCommand("SP_CHECK_MISCDETAILS", con)
            con.Open()
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Parameters.AddWithValue("@Airline", ddl_airline.SelectedValue)
            cmd1.Parameters.AddWithValue("@Trip", ddl_TripType.SelectedValue)
            cmd1.Parameters.AddWithValue("@AgentId", AgentID)
            cmd1.Parameters.AddWithValue("@GroupType", ddl_GroupType.SelectedValue)
            cmd1.Parameters.AddWithValue("@ORIGIN", TXTOrg.Text.Trim())
            cmd1.Parameters.AddWithValue("@DEST", TXTDest.Text)
            Dim st As Boolean = cmd1.ExecuteScalar()
            con.Close()
            'End Checking for entry
            If (st = False) Then
                Dim cmd As New SqlCommand("sp_INSERTMISCSRV", con)
                con.Open()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Airline", ddl_airline.SelectedValue)
                cmd.Parameters.AddWithValue("@Trip", ddl_TripType.SelectedValue)
                cmd.Parameters.AddWithValue("@AgentId", AgentID)
                cmd.Parameters.AddWithValue("@GroupType", ddl_GroupType.SelectedValue)
                cmd.Parameters.AddWithValue("@Org", TXTOrg.Text)
                cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(TXTAmount.Text))
                cmd.Parameters.AddWithValue("@Dest", TXTDest.Text)
                Dim i As Integer = cmd.ExecuteNonQuery()
                con.Close()
                If (i > 0) Then
                    ShowAlertMessage("MISC charges submitted sucessfully")
                Else
                    ShowAlertMessage("Unable to insert .Please try again ")
                End If
                getgv()
                reset()
            Else
                ShowAlertMessage("MISC charges already applied . Please edit.")
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btnreset_Click(sender As Object, e As EventArgs)
        reset()
    End Sub
    Private Sub getgv()
        Try
            Dim da As New SqlDataAdapter("sp_MISCSRV", con)
            da.Fill(ds)
            grdemp.DataSource = ds
            grdemp.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grdemp_RowEditing(sender As Object, e As GridViewEditEventArgs)
        Try
            grdemp.EditIndex = e.NewEditIndex
            getgv()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grdemp_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim lbtnCounter As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnCounter"), Label), Label)
            Dim txtAmount As TextBox = TryCast(grdemp.Rows(e.RowIndex).FindControl("txtAmount"), TextBox)
            con.Open()
            Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
            Dim cmd As New SqlCommand("sp_GetMISCUPDATE", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Counter", lbtnCounter.Text)
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text)
            cmd.Parameters.AddWithValue("@UserID", Session("UID").ToString)
            cmd.Parameters.AddWithValue("@IPAddress", IPAddress)
            cmd.ExecuteNonQuery()
            con.Close()
            grdemp.EditIndex = -1
            getgv()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grdemp_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        grdemp.EditIndex = -1
        getgv()
    End Sub
    Protected Sub grdemp_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim lbtnCounter As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnCounter"), Label), Label)
        Dim lbtnAirline As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnAirline"), Label), Label)
        Dim lbtnTrip As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnTrip"), Label), Label)
        Dim lbtnAgentId As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnAgentId"), Label), Label)
        Dim lbtnGroupType As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnGroupType"), Label), Label)
        Dim lbtnOrg As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnOrg"), Label), Label)
        Dim lbtdest As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtdest"), Label), Label)
        Dim lbtnAmount As Label = TryCast(DirectCast(grdemp.Rows(e.RowIndex).FindControl("lbtnAmount"), Label), Label)
        con.Open()
        Dim IPAddress As String = Request.ServerVariables("REMOTE_ADDR")
        Dim UserID As String = Session("UID").ToString()
        Dim cmd As New SqlCommand("sp_DELETEMISCSRV", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Counter", lbtnCounter.Text)
        cmd.Parameters.AddWithValue("@Airline", lbtnAirline.Text)
        cmd.Parameters.AddWithValue("@Trip", lbtnTrip.Text)
        cmd.Parameters.AddWithValue("@AgentId", lbtnAgentId.Text)
        cmd.Parameters.AddWithValue("@GroupType", lbtnGroupType.Text)
        cmd.Parameters.AddWithValue("@Origin", lbtnOrg.Text)
        cmd.Parameters.AddWithValue("@Destination", lbtdest.Text)
        cmd.Parameters.AddWithValue("@Amount", lbtnAmount.Text)
        cmd.Parameters.AddWithValue("@UserID", UserID)
        cmd.Parameters.AddWithValue("@IPAddress", IPAddress)
        cmd.ExecuteNonQuery()
        con.Close()
        grdemp.EditIndex = -1
        getgv()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Session("UID") = "" Or Session("UID") Is Nothing) Or Session("User_Type") <> "ADMIN" Then
            Response.Redirect("~/Login.aspx")
        End If

        If Not Page.IsPostBack Then
            'grdemp.DataSource = BindGridView();
            Dim msg As String = ""
            ddl_GroupType.AppendDataBoundItems = True
            ddl_GroupType.DataSource = GroupTypeMGMT("", "", "MultipleSelect", msg)
            ddl_GroupType.DataTextField = "GroupType"
            ddl_GroupType.DataValueField = "GroupType"
            ddl_GroupType.DataBind()
            grdemp.DataBind()
            getgv()
        End If
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
