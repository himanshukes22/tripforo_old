Imports System.Data
Partial Class SprReports_Admin_HtlDiscount
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID" Or Request("hidtxtAgencyName") = "ALL", "ALL", Request("hidtxtAgencyName"))
            Dim country, cityCode, HtlType As String
            Dim CityLists As String = Request("htlcitylist")

            If Request("htlcity") = "ALL" Then
                cityCode = "ALL"
                HtlType = "I"
            Else
                Dim citydetail() As String = CityLists.Split(",")
                If citydetail.Length > 0 Then
                    cityCode = citydetail(0)
                    country = citydetail(2)
                End If
                If country <> "IN" Then
                    HtlType = "I"
                Else
                    HtlType = "D"
                End If
            End If

            Dim i As Integer = ST.InsDiscount(HtlType, cityCode, AgentID, ddlStar.SelectedValue, txtAmt.Text.Trim(), 0, "InsDis")

            If i = 1 Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Hotel Discount Submitted succesfully'); ", True)
                Response.Redirect("~/Report/Admin/HtlDiscount.aspx", False)
            Else
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Data Allready In database. Please Insert Another Discount'); ", True)
            End If
        Catch ex As Exception
            ' HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Session("User_Type").ToString().ToUpper() <> "ADMIN" Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            BindGrid(ST.GETHtlDiscount())
        End If
    End Sub

    Protected Sub GrdMarkup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdMarkup.PageIndexChanging
        GrdMarkup.PageIndex = e.NewPageIndex
        BindGrid(Session("GrdDS"))
    End Sub

    Public Sub BindGrid(ByVal GrdDS As DataSet)
        Try
            Session("GrdDS") = GrdDS
            GrdMarkup.DataSource = GrdDS
            GrdMarkup.DataBind()
        Catch ex As Exception
            ' HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub
    Protected Sub GrdMarkup_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GrdMarkup.RowCancelingEdit
        GrdMarkup.EditIndex = -1
        BindGrid(Session("GrdDS"))
    End Sub

    'Protected Sub GrdMarkup_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdMarkup.RowCommand
    '    If e.CommandName = "Edit" Then
    '        Dim LB As LinkButton = TryCast(e.CommandSource, LinkButton)
    '        Dim gvr As GridViewRow = TryCast(LB.Parent.Parent, GridViewRow)
    '        ViewState("RowIndex") = gvr.RowIndex
    '    End If
    'End Sub

    Protected Sub GrdMarkup_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GrdMarkup.RowEditing
        GrdMarkup.EditIndex = e.NewEditIndex
        BindGrid(Session("GrdDS"))
    End Sub

    Protected Sub GrdMarkup_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GrdMarkup.RowUpdating
        Try
            Dim HtlType As String = DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtHtlType"), TextBox).Text
            Dim City As String = DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtCity"), TextBox).Text
            Dim Star As String = DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtStar"), TextBox).Text
            Dim txtAmt1 As String = DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtAmt"), TextBox).Text
            Dim counter As Integer = Convert.ToInt32(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblCounter"), Label).Text)
            Dim AgentID As String = DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblAgentID"), Label).Text

            If txtAmt1 = "" Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Please Provide Discount %'); ", True)
            ElseIf HtlType = "" Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Please Provide Holel Type'); ", True)
            ElseIf City = "" Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Please Provide City Code'); ", True)
            ElseIf Star = "" Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Please Provide Hotel Star'); ", True)
            Else
                Dim i As Integer = ST.InsDiscount(HtlType, City, AgentID, Star, txtAmt1.Trim(), counter, "UpdateDis")
                If (i = 1) Then
                    Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Hotel Discount Updated succesfully'); ", True)
                    GrdMarkup.EditIndex = -1
                    'BindGrid(ST.GETHtlDiscount())
                    Response.Redirect("~/Report/Admin/HtlDiscount.aspx", False)
                Else
                    Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Data Allready In database.'); ", True)
                End If
            End If
        Catch ex As Exception
            '            HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub

    'Public Sub BindCity(ByVal CountryName As String, ByVal type As String)
    '    Try
    '        Dim CityDS As New DataSet()
    '        CityDS = ST.GetCountry("", CountryName)
    '        If CityDS.Tables(0).Rows.Count > 0 And type = "DDL" Then
    '            ddlCity.AppendDataBoundItems = True
    '            ddlCity.Items.Clear()
    '            ddlCity.Items.Insert(0, "--Select City--")
    '            ddlCity.Items.Insert(1, "ALL")
    '            ddlCity.DataSource = CityDS
    '            ddlCity.DataTextField = "CityName"
    '            ddlCity.DataValueField = "AirportCode"
    '            ddlCity.DataBind()
    '        End If
    '        If CityDS.Tables(0).Rows.Count > 0 And type = "GRD" Then
    '            Dim City As DropDownList = DirectCast(GrdMarkup.Rows(ViewState("RowIndex")).FindControl("ddlCity"), DropDownList)
    '            City.AppendDataBoundItems = True
    '            City.Items.Clear()
    '            City.Items.Insert(0, "--Select City--")
    '            City.Items.Insert(1, "ALL")
    '            City.DataSource = CityDS
    '            City.DataTextField = "CityName"
    '            City.DataValueField = "AirportCode"
    '            City.DataBind()
    '        End If
    '    Catch ex As Exception
    '        HtlLog.InsertLogDetails(ex)
    '    End Try
    'End Sub
End Class
