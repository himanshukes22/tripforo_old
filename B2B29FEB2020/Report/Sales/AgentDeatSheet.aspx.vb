
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Partial Class Reports_Sales_AgentDeatSheet
    Inherits System.Web.UI.Page
    Private S As New Sales()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        Try


            If Not IsPostBack Then


                dll_Agency.AppendDataBoundItems = True
                dll_Agency.Items.Clear()
                dll_Agency.Items.Insert(0, "--Select Agency--")
                dll_Agency.DataSource = S.SelectAgencyName(Session("UID").ToString)
                dll_Agency.DataTextField = "Agency_Name"
                dll_Agency.DataValueField = "Agency_Name"
                dll_Agency.DataBind()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub BindGrid()
        Try
            Grid_AgentDealSeat.DataSource = S.DealSeatDetails(Session("UID").ToString, dll_Agency.SelectedValue)
            Grid_AgentDealSeat.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub dll_Agency_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dll_Agency.SelectedIndexChanged
        Try
            BindGrid()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
End Class


