Imports System.Data
Imports System.Data.SqlClient
Partial Class Transfer_AdminMarkupset
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet
    Public Agent
    Public dst As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not Page.IsPostBack Then
                Try
                    BindData()
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
                mk.Text = "0"
            End If
            ' uid.Text = Session("").ToString.Trim
            mk.Attributes.Add("onkeypress", "return phone_vali()")

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindData()

        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
            Dim da As New SqlDataAdapter("SELECT COUNTER,MARKUP_ON,MARKUP_VALUE,MARKUP_TYPE FROM TBL_TRANSFER_ADMIN_MARKUP ", con)
            da.Fill(dst)
            GridView1.DataSource = dst
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        Try
            GridView1.EditIndex = e.NewEditIndex
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            GridView1.EditIndex = -1
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
            Dim cmd As New SqlCommand()
            If DirectCast(GridView1.Rows(0).Cells(0).Controls(0), LinkButton).Text = "Insert" Then

            Else
                cmd.CommandText = "UPDATE TBL_TRANSFER_ADMIN_MARKUP SET MARKUP_VALUE=@MRK WHERE COUNTER=@cat_id"
                cmd.Parameters.Add("@MRK", SqlDbType.Money).Value = UCase(DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text.ToUpper)
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(GridView1.Rows(e.RowIndex).Cells(1).Text)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
            GridView1.EditIndex = -1
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
            Dim cmd As New SqlCommand()
            cmd.CommandText = "DELETE FROM TBL_TRANSFER_ADMIN_MARKUP WHERE counter=@cat_id"
            cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = Convert.ToInt64(GridView1.Rows(e.RowIndex).Cells(1).Text)
            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
            Dim da As New SqlDataAdapter("Select MARKUP_ON from TBL_TRANSFER_ADMIN_MARKUP  where (MARKUP_ON='" & uid.Text.Trim & "')", con)
            Dim dt As New DataTable()
            con.Open()
            da.Fill(dt)

            Dim cmd As New SqlCommand()
            If dt.Rows.Count > 0 Then
                lbl.Text = "MarkUp For Is Already Exist."
            Else
                cmd.CommandText = "INSERT INTO TBL_TRANSFER_ADMIN_MARKUP(MARKUP_ON,MARKUP_VALUE,MARKUP_TYPE) VALUES(@UID,@MRK,@MKTYP)"
                cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = uid.Text.Trim
                cmd.Parameters.Add("@MRK", SqlDbType.Money).Value = mk.Text.Trim
                cmd.Parameters.Add("@MKTYP", SqlDbType.VarChar).Value = ddl_mktyp.SelectedValue
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                lbl.Text = ""
            End If
            con.Close()
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
