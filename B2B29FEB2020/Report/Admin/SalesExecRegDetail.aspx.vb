Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class Reports_Admin_SalesExecRegDetail
    Inherits System.Web.UI.Page
    Private S As New Sales()
    Private ds As New DataSet()
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private STDom As New SqlTransactionDom
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub BindGrid()

        Try
            Grid_SalesExecReg.DataSource = S.SalesExecDetail()
            Grid_SalesExecReg.DataBind()
            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.SALESEXECACTIVE.ToString()).Tables(0)
            If (dtmodule.Rows.Count > 0) Then
                For Each dr As DataRow In dtmodule.Rows
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.ACTIVE.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        Grid_SalesExecReg.Columns(8).Visible = False
                    End If
                    If (dr("MODULETYPE").ToString().ToUpper() = MODULETYPE.PASSWORD.ToString().ToUpper() AndAlso Convert.ToBoolean(dr("STATUS").ToString()) = True) Then
                        Grid_SalesExecReg.Columns(7).Visible = False
                    End If
                Next
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Protected Sub GridRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "Status" Then


                Dim ds As New DataSet()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub ITZ_Accept_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' string agent = "";
        Try
            Dim id As String = DirectCast(sender, LinkButton).CommandArgument.ToString()
            Dim stat As String = (DirectCast(sender, LinkButton).Text).ToUpper()
            If stat = "ACTIVE" Then
                stat = "NOT ACTIVE"
            Else
                stat = "ACTIVE"
            End If
            'if (con.State == ConnectionState.Open)
            '{
            '    con.Close();
            con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            con.Open()
            Dim str As String = "update SalesExecReg set  Status='" & stat & "' where ID='" & id & "'"
            Dim com1 As New SqlCommand(str, con)
            com1.ExecuteNonQuery()
            BindGrid()
            'str = AgName.Value.ToString().Trim.Replace("'", "");
            ' Agent_Credit_Info(str);
            '}
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

End Class

