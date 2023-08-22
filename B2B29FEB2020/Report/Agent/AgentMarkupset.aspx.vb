Imports System.Data
Imports System.Data.SqlClient
Partial Class Transfer_AgentMarkupset
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
            uid.Text = Session("UID").ToString.Trim
            mk.Attributes.Add("onkeypress", "return phone_vali()")

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub BindData()

        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myCon1").ConnectionString)
            Dim da As New SqlDataAdapter("SELECT counter,USERID,MARKUP_TYPE,MARKUP_VALUE FROM TBL_TRANSFER_AGENT_MARKUP where USERID='" & Session("UID").ToString.Trim & "'", con)

            da.Fill(dst)
            GridView1.DataSource = dst
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    
    
    
    
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim da As New SqlDataAdapter("Select USERID from TBL_TRANSFER_AGENT_MARKUP where (USERID='" & uid.Text.Trim & "')", con)
            Dim dt As New DataTable()
            con.Open()
            da.Fill(dt)
            Dim cmd As New SqlCommand()
            If dt.Rows.Count > 0 Then
                cmd.CommandText = "UPDATE TBL_TRANSFER_AGENT_MARKUP SET MARKUP_VALUE= @MRK,MARKUP_TYPE= @MKTYP WHERE USERID=@UID"
                cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = uid.Text.Trim
                cmd.Parameters.Add("@MRK", SqlDbType.Money).Value = mk.Text.Trim
                cmd.Parameters.Add("@MKTYP", SqlDbType.VarChar).Value = ddl_mktyp.SelectedValue
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                lbl.Text = "MarkUp Updated Sucessfully"
            Else

                cmd.CommandText = "INSERT INTO TBL_TRANSFER_AGENT_MARKUP (USERID,MARKUP_VALUE,MARKUP_TYPE) VALUES(@UID,@MRK,@MKTYP)"
                cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = uid.Text.Trim
                cmd.Parameters.Add("@MRK", SqlDbType.Money).Value = mk.Text.Trim
                cmd.Parameters.Add("@MKTYP", SqlDbType.VarChar).Value = ddl_mktyp.SelectedValue
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                lbl.Text = "Markup Submitted Sucessfully"
            End If
            con.Close()
            BindData()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
