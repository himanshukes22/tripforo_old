Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_Agent_Details
    Inherits System.Web.UI.Page

    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Session("User_Type").ToString().ToUpper() <> "ADMIN" And Session("User_Type").ToString().ToUpper() <> "ACC" And Session("User_Type").ToString().ToUpper() <> "DI" Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not IsPostBack Then

                Dim msg As String = ""
                DropDownListType.AppendDataBoundItems = True

                DropDownListType.DataSource = GroupTypeMGMT("", "", "MultipleSelect", msg)
                DropDownListType.DataTextField = "GroupType"
                DropDownListType.DataValueField = "GroupType"
                DropDownListType.DataBind()


                If Session("UserType") = "DI" Then
                    tr_AgentType.Visible = False
                    tr_GroupType.Visible = False
                    tr_SalesPerson.Visible = False
                    tr_ddlSalesPerson.Visible = False
                    If (Request("DIAG") = 1) Then
                        GridView1.Columns(0).Visible = False
                    Else
                        GridView1.Columns(0).Visible = True
                    End If
                    td_SBS.Visible = False
                    td_ddlSBS.Visible = False
                Else
                    tr_AgentType.Visible = True
                    tr_GroupType.Visible = True
                    tr_SalesPerson.Visible = True
                    tr_ddlSalesPerson.Visible = True
                End If
                DropDownListSalesPerson.DataSource = STDom.GetSalesRef().Tables(0)
                DropDownListSalesPerson.DataTextField = "EmailId"
                DropDownListSalesPerson.DataValueField = "EmailId"
                DropDownListSalesPerson.DataBind()
            End If

            Dim dtmodule As New DataTable
            dtmodule = STDom.GetModuleAccessDetails(Session("UID"), MODULENAME.AGENCYSEARCH.ToString()).Tables(0)
            If (dtmodule.Rows.Count > 0) Then
                If (Convert.ToBoolean(dtmodule.Rows(0)("STATUS").ToString()) = True) Then
                    export.Visible = False
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
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
    Protected Sub export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles export.Click
        Try
            Dim dtag As New DataSet

            Dim type As String = If(DropDownListType.SelectedValue = "Select", "", DropDownListType.SelectedValue)
            Dim saleId As String = If(DropDownListSalesPerson.SelectedValue = "Select", "", DropDownListSalesPerson.SelectedValue)
            Dim agencyId As String = If(Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName").Trim)

            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            Dim DiSearch As String = ""
            If (ddl_stock.SelectedValue = "ALL") Then
                DiSearch = "ALL"
            ElseIf (ddl_stock.SelectedValue = "STAG") Then
                DiSearch = "STAG"
            End If
            dtag = STDom.GetAgencyDetailsDynamic(agencyId, type, saleId, FromDate, ToDate, Session("UID"), Session("UserType"), DiSearch) 'ST.GetAgencyDetails(Request("hidtxtAgencyName")).Tables(0)
            
            STDom.ExportData(dtag)
        Catch ex As Exception

        End Try

    End Sub
    Public Sub Export_rep(ByVal fileName As String, ByVal gv As GridView)
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
            HttpContext.Current.Response.ContentType = "application/ms-excel"

            Using sw As New IO.StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    ' Create a form to contain the grid 
                    Dim table As New Table()

                    ' add the header row to the table 
                    If gv.HeaderRow IsNot Nothing Then
                        PrepareControlForExport(gv.HeaderRow)
                        table.Rows.Add(gv.HeaderRow)
                    End If

                    ' add each of the data rows to the table 
                    For Each row As GridViewRow In gv.Rows
                        PrepareControlForExport(row)
                        table.Rows.Add(row)
                    Next

                    ' add the footer row to the table 
                    If gv.FooterRow IsNot Nothing Then
                        PrepareControlForExport(gv.FooterRow)
                        table.Rows.Add(gv.FooterRow)
                    End If

                    ' render the table into the htmlwriter 
                    table.RenderControl(htw)

                    ' render the htmlwriter into the response 
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End Using
            End Using
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Shared Sub PrepareControlForExport(ByVal control As Control)
        Try
            For i As Integer = 0 To control.Controls.Count - 1
                Dim current As Control = control.Controls(i)
                If TypeOf current Is LinkButton Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))
                ElseIf TypeOf current Is ImageButton Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))
                ElseIf TypeOf current Is HyperLink Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))
                ElseIf TypeOf current Is DropDownList Then
                    control.Controls.Remove(current)
                    control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))
                ElseIf TypeOf current Is CheckBox Then
                    control.Controls.Remove(current)
                    ' control.Controls.AddAt(i, new LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False"))))
                End If
                If current.HasControls() Then
                    PrepareControlForExport(current)

                End If
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub



    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.DataSource = Session("DATAVIEW")
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataBind()
            Session("DATAVIEW") = DirectCast(GridView1.DataSource, DataView)
            'BindData()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub




    Protected Sub GridView1_Sorting1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting

        Try
            Dim sortBy As String = Nothing

            If Session("ORDERBY").ToString() = "ASC" Then
                sortBy = "DESC"

            ElseIf Session("ORDERBY").ToString() = "DESC" Then
                sortBy = "ASC"
            End If

            Session("ORDERBY") = sortBy
            Dim dv As DataView = DirectCast(Session("DATAVIEW"), DataView)
            dv.Sort = Convert.ToString(e.SortExpression) & " " & sortBy
            GridView1.DataSource = dv
            GridView1.DataBind()
            Session("DATAVIEW") = DirectCast(GridView1.DataSource, DataView)
        Catch ex As Exception

        End Try
        

    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Try
            Dim dtag As New DataTable

            Dim type As String = If(DropDownListType.SelectedValue = "Select", "", DropDownListType.SelectedValue)
            Dim saleId As String = If(DropDownListSalesPerson.SelectedValue = "Select", "", DropDownListSalesPerson.SelectedValue)
            Dim agencyId As String = If(Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName").Trim)

            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            Dim DiSearch As String = ""
            If (ddl_stock.SelectedValue = "ALL") Then
                DiSearch = "ALL"
            ElseIf (ddl_stock.SelectedValue = "STAG") Then
                DiSearch = "STAG"
            End If
            dtag = STDom.GetAgencyDetailsDynamic(agencyId, type, saleId, FromDate, ToDate, Session("UID"), Session("UserType"), DiSearch).Tables(0) 'ST.GetAgencyDetails(Request("hidtxtAgencyName")).Tables(0)

            If (dtag.Rows.Count > 0) Then

                TOTALCRD.Text = "Total Credit: " + Convert.ToString(dtag.Compute("Sum(Crd_Limit)", String.Empty))
            End If

            Dim dv As New DataView(dtag) '(ds.Tables(0))
            Session("DATAVIEW") = dv
            Session("ORDERBY") = "DESC"

            GridView1.DataSource = dtag
            GridView1.PageIndex = 0
            GridView1.DataBind()
            If Session("UserType") = "DI" Then
                If (Request("DIAG") = 1) Then
                    GridView1.Columns(0).Visible = False
                Else
                    GridView1.Columns(0).Visible = True
                End If
            End If

        
        Catch ex As Exception

        End Try
        

    End Sub
End Class

