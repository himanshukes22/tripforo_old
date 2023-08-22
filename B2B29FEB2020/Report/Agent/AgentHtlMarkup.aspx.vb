Imports System.Data
Imports System.Data.SqlClient

Partial Class SprReports_Agent_AgentHtlMarkup
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            End If
            If Not Page.IsPostBack Then
                Bindgrid(ST.GetHtlMarkup(Session("UID").ToString, "", "", "", "", "", "", "", 0, "", "", "AgentMrkSearch"))
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Try
            BindGridMarkup()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub GrdMarkup_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdMarkup.RowDeleting
        Try
            Dim ReqType As String = ""
            If Session("User_Type").ToString() = "ADMIN" Then
                ReqType = "DeleteAdminMrk"
            End If
            If Session("User_Type").ToString() = "AGENT" Then
                ReqType = "DeleteAgentMrk"
            End If
            Dim Counter As Label = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label)
            Dim i As Integer = ST.InsHtlMarkup("", "", "", "", "", "", "", txtAmt.Text.Trim(), "", "", ReqType, Convert.ToInt32(Counter.Text))
            If i > 0 Then
                Dim DS As New DataSet
                DS = Session("Grdds")
                For Each strow In DS.Tables(0).Rows
                    If strow.Item("MarkupID").ToString = Counter.Text Then
                        strow.Delete()
                        'strow.Item("markupamt") = markupAmt.Text
                        DS.AcceptChanges()
                        Exit For
                    End If
                Next
                Bindgrid(DS)
                ShowAlertMessage("Markup Deleted Successfully")
            Else
                ShowAlertMessage("Please Try Again")
            End If

        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
        Try
            'Dim ReqType As String = "", AgentID As String = ""
            Dim City As String = If([String].IsNullOrEmpty(Request("htlCity")) Or Request("htlCity") = "City" Or Request("City") = "ALL", "ALL", Request("htlCity"))
            Dim Country As String = If([String].IsNullOrEmpty(Request("TR_Country")) Or Request("TR_Country") = "Country" Or Request("TR_Country") = "ALL", "ALL", Request("TR_Country"))
            Dim Star As String = If(ddlStar.SelectedIndex > 0, ddlStar.SelectedValue, "ALL")
            ' Dim HotelType As String = ddlhtl.SelectedValue
            Dim MrkupType As String = Request("mrktype")
            Dim i As Integer = ST.InsHtlMarkup(Session("UID").ToString, "", Country, City, "", MrkupType, Star, txtAmt.Text.Trim, "", "", "INSAgent", 0)
            If i > 0 Then
                Dim ds As New DataSet()
                ds = Session("Grdds")
                Dim addnewRow As DataRow = ds.Tables(0).NewRow
                addnewRow("MarkupID") = i
                addnewRow("AgentID") = Session("UID")
                addnewRow("Country") = Country
                addnewRow("City") = City
                addnewRow("Star") = Star

                addnewRow("MarkupType") = MrkupType
                addnewRow("MarkupAmount") = txtAmt.Text.Trim()
                ds.Tables(0).Rows.Add(addnewRow)
                Bindgrid(ds)
                ShowAlertMessage("Markup Inserted Successfully")
                txtAmt.Text = ""
                ddlStar.SelectedIndex = -1
            Else
                ShowAlertMessage("Markup Already Exists")
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub


    Protected Sub GrdMarkup_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GrdMarkup.RowUpdating
        Try
            Dim markupAmt As TextBox = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("txtAmt"), TextBox), TextBox)
            Dim Counter As Label = TryCast(DirectCast(GrdMarkup.Rows(e.RowIndex).FindControl("lblCounter"), Label), Label)
            Dim i As Integer = ST.InsHtlMarkup("", "", "", "", "", "", "", markupAmt.Text.Trim(), "", "", "UpdateAgent", Convert.ToInt32(Counter.Text))
            GrdMarkup.EditIndex = -1
            If i > 0 Then
                Dim DS As New DataSet
                DS = Session("Grdds")
                For Each strow In DS.Tables(0).Rows
                    If strow.Item("MarkupID").ToString = Counter.Text Then
                        strow.Item("MarkupAmount") = markupAmt.Text
                        DS.AcceptChanges()
                    End If
                Next
                Bindgrid(DS)
                ShowAlertMessage("Markup Updated Successfully")
                'Response.Redirect("~/Report/Admin/AdminHtlMarkup.aspx", False)
            Else
                ShowAlertMessage("This Record is Allready In Database. Please Insert Another Recard")
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try

    End Sub
    Public Sub BindGridMarkup()
        Try
            Dim HotelType As String = "", Star As String = ""
            
            If ddlStar.SelectedValue <> "--Select star--" Then
                Star = ddlStar.SelectedValue
            End If
            Dim Country As String = If([String].IsNullOrEmpty(Request("TR_Country")) Or Request("TR_Country") = "Country" Or Request("TR_Country") = "ALL", "ALL", Request("TR_Country"))
            Dim City As String = If([String].IsNullOrEmpty(Request("htlCity")) Or Request("htlCity") = "City" Or Request("City") = "ALL", "ALL", Request("htlCity"))
            Bindgrid(ST.GetHtlMarkup(Session("UID").ToString, "", Country, City, Star, HotelType, Request("mrktype"), "", 0, "", "", "AgentMrkSearch"))
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub
    Private Sub Bindgrid(ByVal ds As DataSet)
        Try
            Session("Grdds") = ds
            GrdMarkup.DataSource = ds
            GrdMarkup.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                NoRecard.Visible = False
            Else
                NoRecard.Visible = True
                NoRecard.InnerText = "No record found"
            End If

        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub GrdMarkup_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GrdMarkup.RowEditing
        GrdMarkup.EditIndex = e.NewEditIndex
        Bindgrid(Session("Grdds"))
    End Sub

    Protected Sub GrdMarkup_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GrdMarkup.RowCancelingEdit
        GrdMarkup.EditIndex = -1
        Bindgrid(Session("Grdds"))
    End Sub

    Protected Sub GrdMarkup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdMarkup.PageIndexChanging
        GrdMarkup.PageIndex = e.NewPageIndex
        Bindgrid(Session("Grdds"))
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

End Class
