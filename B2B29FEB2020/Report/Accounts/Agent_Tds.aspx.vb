Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Accounts_Agent_Tds
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet
    Public Agent
    Public dst As New DataSet
    Private ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Session("User_Type").ToString().ToUpper() <> "ACC" Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            Try
                'BindData()
                'If con.State = ConnectionState.Open Then con.Close()
                'con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                'adp = New SqlDataAdapter("SELECT user_id,Agency_Name FROM New_Regs", con)
                'adp.Fill(ds)
                'If ds.Tables(0).Rows.Count > 0 Then
                '    cbo.AppendDataBoundItems = True
                '    cbo.Items.Clear()
                '    cbo.Items.Insert(0, "--Select Agency Name--")
                '    cbo.DataSource = ds
                '    cbo.DataTextField = "Agency_Name"
                '    cbo.DataValueField = "user_id"
                '    cbo.DataBind()
                'Else
                'End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If
    End Sub
    Private Sub BindData()
        Try
            dst.Clear()
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim da As New SqlDataAdapter("SELECT counter,Agency_Name,user_id,Agent_Type,TDS,ExmptTDS,ExmptTdsLimit FROM New_Regs", con)
            da.Fill(dst)
            ViewState("dst") = dst
            GridView1.DataSource = dst
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        Try
            GridView1.EditIndex = e.NewEditIndex
            ' Bind_Data()
            GridView1.DataSource = ViewState("dst")
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            GridView1.EditIndex = -1
            ' Bind_Data()
            GridView1.DataSource = ViewState("dst")
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()

            cmd.CommandText = "UPDATE New_Regs SET TDS=@TD , ExmptTDS=@ExmptTDS,ExmptTdsLimit=@ExmptTdsLimit WHERE counter=@cat_id"
            cmd.Parameters.Add("@TD", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(5).Controls(0), TextBox).Text)
            cmd.Parameters.Add("@ExmptTDS", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(6).Controls(0), TextBox).Text)
            cmd.Parameters.Add("@ExmptTdsLimit", SqlDbType.VarChar).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(7).Controls(0), TextBox).Text)
            cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(GridView1.Rows(e.RowIndex).Cells(1).Text)
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            GridView1.EditIndex = -1
            'Bind_Data()

            GridView1.DataSource = ViewState("dst")
            GridView1.DataBind()
            Response.Redirect("Agent_Tds.aspx")
            'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Updated Sucessfully');window.location='Agent_Tds.aspx';", True)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "DELETE FROM Airline_Pa WHERE counter=@cat_id"
            cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(GridView1.Rows(e.RowIndex).Cells(1).Text)
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            ' Bind_Data()
            GridView1.DataSource = ViewState("dst")
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Private Sub Bind_Data()
        Try
            dst.Clear()
            Agent = Request("hidtxtAgencyName")
            Session("Agent") = Agent
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim da As New SqlDataAdapter("SELECT counter,Agency_Name,user_id,Agent_Type,TDS,ExmptTDS,ExmptTdsLimit FROM New_Regs where user_id='" & Agent & "'", con)
            'Dim dt As New DataTable()
            da.Fill(dst)
            ViewState("dst") = dst
            GridView1.DataSource = dst
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    
    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Dim dtag As New DataTable
        dtag = ST.GetAgencyDetails(Request("hidtxtAgencyName")).Tables(0)
        GridView1.DataSource = dtag
        GridView1.DataBind()
    End Sub
End Class

