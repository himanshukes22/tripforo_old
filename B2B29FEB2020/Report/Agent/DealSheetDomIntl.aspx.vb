Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient

Partial Class DealSheetDomIntl
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection
    Dim adp As SqlDataAdapter
    Public ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            Bind_Commission()
            'Bind_PLB()
            Bind_Domestic()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub Bind_Commission()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            ds.Clear()
            adp = New SqlDataAdapter("DealSheetDomIntl", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@Type", Session("AGTY"))
            adp.SelectCommand.Parameters.AddWithValue("@TableType", "CommissionPLB")
            adp.Fill(ds)
            GridView_Commission.DataSource = ds
            GridView_Commission.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub Bind_PLB()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            ds.Clear()
            adp = New SqlDataAdapter("DealSheetDomIntl", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@Type", Session("AGTY"))
            adp.SelectCommand.Parameters.AddWithValue("@TableType", "CommissionPLB")
            adp.Fill(ds)
            'GridView_PLB.DataSource = ds
            ' GridView_PLB.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Sub Bind_Domestic()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            ds.Clear()
            adp = New SqlDataAdapter("DealSheetDomIntl", con)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.AddWithValue("@Type", Session("AGTY"))
            adp.SelectCommand.Parameters.AddWithValue("@TableType", "Domestic")
            adp.Fill(ds)
            GridView_Domestic.DataSource = ds
            GridView_Domestic.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class
