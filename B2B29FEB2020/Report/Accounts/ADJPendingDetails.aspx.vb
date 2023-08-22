Imports System
Imports System.Collections.Generic

Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class SprReports_Accounts_ADJPendingDetails
    Inherits System.Web.UI.Page


    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Or Session("User_Type").ToString() <> "ACC" Then
                Response.Redirect("~/Login.aspx")
            End If
        Catch ex As Exception

        End Try

        If Not IsPostBack Then
            Try


                If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then
                    grd_accdeposit.DataSource = STDom.DepositStatusDetails("ADPending") ' upload.GetDepositProcessDetails("ADPending")
                    grd_accdeposit.DataBind()
                    Dim DTUT As New DataTable
                    DTUT = STDom.GetUploadType().Tables(0)
                    RBL_Type.DataSource = DTUT
                    RBL_Type.DataTextField = "UploadCategoryText"
                    RBL_Type.DataValueField = "UploadCategory"
                    RBL_Type.DataBind()
                    RBL_Type.SelectedIndex = 0
                    Dim DTUC As New DataTable
                    DTUC = STDom.GetCategory("CA").Tables(0)
                    If DTUC.Rows.Count > 0 Then
                        ddl_Category.AppendDataBoundItems = True
                        ddl_Category.Items.Clear()
                        ddl_Category.Items.Insert(0, "--Select Category--")
                        ddl_Category.DataSource = DTUC
                        ddl_Category.DataTextField = "SubCategory"
                        ddl_Category.DataValueField = "GroupType"
                        ddl_Category.DataBind()

                    End If
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
        End If


    End Sub
    Protected Sub grd_accdeposit_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            id = e.CommandArgument.ToString()
            Dim DtDDetails As New DataTable
            DtDDetails = STDom.GetDepositDetailsByID(id).Tables(0)
            Dim Status As String = DtDDetails.Rows(0)("Status").ToString
            Dim IDS As String = DtDDetails.Rows(0)("Counter").ToString
            Dim rw As GridViewRow = DirectCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            Dim AgentID As Label = DirectCast(rw.FindControl("lbl_uid"), Label)

            If e.CommandName = "accept" Then
                id = e.CommandArgument.ToString()
                STDom.UpdateDepositDetails(ID, AgentID.Text, "ADInProcess", "Acc", Session("UID"), txt_Reject.Text.Replace("'", ""))
                'ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Sucessfully');", True)
                'td_Reject.Visible = False
                grd_accdeposit.DataSource = STDom.DepositStatusDetails("ADPending")
                grd_accdeposit.DataBind()
            End If

            If e.CommandName = "reject" Then

                If IDS = id AndAlso Status = "Confirm" Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Deposite amount is already confirm');", True)
                    grd_accdeposit.DataSource = STDom.DepositStatusDetails("ADPending")
                    grd_accdeposit.DataBind()
                Else
                    Dim lb As LinkButton = TryCast(e.CommandSource, LinkButton)
                    Dim gvr As GridViewRow = TryCast(lb.Parent.Parent, GridViewRow)
                    gvr.BackColor = System.Drawing.Color.Yellow

                    td_Reject.Visible = True
                    ViewState("ID") = id
                    ViewState("AgentID") = AgentID.Text
                End If



            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub btn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Try
            td_Reject.Visible = False
            grd_accdeposit.DataSource = STDom.DepositStatusDetails("ADPending")
            grd_accdeposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub grd_accdeposit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)


    End Sub
    Protected Sub ddl_Category_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Category.SelectedIndexChanged
        Try
            BindGrid()
            txtSearch.Text = ""
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Private Sub BindGrid()
        Try
            Dim dtgrid As New DataTable
            dtgrid = STDom.DepositProcessDetails("ADPending", ddl_Category.SelectedValue).Tables(0)
            grd_accdeposit.DataSource = dtgrid
            grd_accdeposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub RBL_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL_Type.SelectedIndexChanged
        Try
            Dim DTUC As New DataTable
            DTUC = STDom.GetCategory(RBL_Type.SelectedValue).Tables(0)
            If DTUC.Rows.Count > 0 Then
                ddl_Category.AppendDataBoundItems = True
                ddl_Category.Items.Clear()
                ddl_Category.Items.Insert(0, "--Select Category--")
                ddl_Category.DataSource = DTUC
                ddl_Category.DataTextField = "SubCategory"
                ddl_Category.DataValueField = "GroupType"
                ddl_Category.DataBind()
                BindGrid()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub



    Private Sub SearchText()
        Try
            If ddl_Category.SelectedValue = "--Select Category--" Then
                Dim dt As DataTable = STDom.DepositStatusDetails("ADPending").Tables(0)
                Dim dv As New DataView(dt)
                Session("dv") = dv
                Dim SearchExpression As String = Nothing
                If Not [String].IsNullOrEmpty(txtSearch.Text) Then

                    SearchExpression = String.Format("{0} '%{1}%'", grd_accdeposit.SortExpression, txtSearch.Text)
                End If
                dv.RowFilter = "AgencyName like" & SearchExpression
                grd_accdeposit.DataSource = dv
                grd_accdeposit.DataBind()
            Else
                Dim dt As DataTable = STDom.DepositProcessDetails("ADPending", ddl_Category.SelectedValue).Tables(0)
                Dim dv As New DataView(dt)
                Dim SearchExpression As String = Nothing
                If Not [String].IsNullOrEmpty(txtSearch.Text) Then

                    SearchExpression = String.Format("{0} '%{1}%'", grd_accdeposit.SortExpression, txtSearch.Text)
                End If
                dv.RowFilter = "AgencyName like" & SearchExpression
                grd_accdeposit.DataSource = dv

                grd_accdeposit.DataBind()
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try


    End Sub
    Public Function ReplaceKeyWords(ByVal m As Match) As String

        Return "<span class='highlight'>" & Convert.ToString(m.Value) & "</span>"

    End Function
    Public Function Highlight(ByVal InputTxt As String) As String
        Dim Search_Str As String = txtSearch.Text.ToString()
        ' Setup the regular expression and add the Or operator.
        Dim RegExp As New Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase)

        ' Highlight keywords by calling the 
        'delegate each time a keyword is found.
        Return RegExp.Replace(InputTxt, New MatchEvaluator(AddressOf ReplaceKeyWords))

        ' Set the RegExp to null.
        RegExp = Nothing

    End Function
    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            SearchText()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub

    Protected Sub btn_Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Submit.Click
        Try
            STDom.UpdateDepositDetails(ViewState("ID"), ViewState("AgentID"), "ADRejected", "Rej", Session("UID"), txt_Reject.Text.Replace("'", ""))
            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Rejected Sucessfully');", True)
            td_Reject.Visible = False
            grd_accdeposit.DataSource = STDom.DepositStatusDetails("ADPending")
            grd_accdeposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class
