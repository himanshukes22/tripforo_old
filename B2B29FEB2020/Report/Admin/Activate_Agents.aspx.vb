Imports System.Data
Imports System.Data.SqlClient
Partial Class Reports_Admin_Activate_Agents
    Inherits System.Web.UI.Page

    Dim dsag As New DataSet
    Dim adp As New SqlDataAdapter
    Dim con As New SqlConnection
    Dim ds As New DataSet
    Dim i
    Public total_Credit
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not Page.IsPostBack Then
                Agent_Credit_Info()
                Try

                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                End Try
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Public Sub Agent_Credit_Info(Optional ByVal str As String = "")
        Try
            total_Credit = 0
            dsag.Clear()
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            If str = "" Then
                adp = New SqlDataAdapter("SELECT * FROM New_Regs order by agency_name ", con)
            Else
                adp = New SqlDataAdapter("SELECT * from New_Regs where Agency_Name like '%" & str & "%' order by Agency_Name asc", con)
            End If

            adp.Fill(dsag)
            If dsag.Tables(0).Rows.Count > 0 Then

                For i = 0 To dsag.Tables(0).Rows.Count - 1
                    Try
                        total_Credit = total_Credit + CDbl(dsag.Tables(0).Rows(i)("crd_limit"))
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)

                    End Try
                Next


                GridView1.DataSource = dsag
                GridView1.DataBind()
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

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

    Protected Sub cmdcrd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcrd.Click
        'Dim Agent = cbo.SelectedItem.Text
        ' Session("Agent") = Agent
        'Response.Write("<script>window.open('Update_credit.aspx')</script>")
        ' Response.Write("<script>javascript:window.open('Update_credit.aspx?Agn=" & Agent & "','Update','scrollbars=yes,width=800,height=400,top=20,left=150')</script>")

    End Sub
    Protected Sub export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles export.Click

        Try
            Dim currtime = Now.TimeOfDay
            Dim filename = Session("Agent") & ".XLS"
            Export_rep(filename, GridView1)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim agent = CType(sender, LinkButton).CommandArgument.ToString
            Dim stat = UCase(CType(sender, LinkButton).Text)
            If stat = "ACTIVE" Then
                stat = "NOT ACTIVE"
            Else
                stat = "ACTIVE"
            End If
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()
            Dim str = "update New_Regs set  Agent_status='" & stat & "' where user_id='" & agent & "'"
            Dim com1 As SqlCommand = New SqlCommand(str, con)
            com1.ExecuteNonQuery()
            str = AgName.Value.ToString.Trim.Replace("'", "")
            Agent_Credit_Info(str)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim agent = CType(sender, LinkButton).CommandArgument.ToString
            Dim stat = UCase(CType(sender, LinkButton).Text)
            If stat = "ACTIVE" Then
                stat = "NOT ACTIVE"
            Else
                stat = "ACTIVE"
            End If
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            con.Open()
            Dim str = "update New_Regs set  Online_Tkt='" & stat & "' where user_id='" & agent & "'"
            Dim com1 As SqlCommand = New SqlCommand(str, con)
            com1.ExecuteNonQuery()
            str = AgName.Value.ToString.Trim.Replace("'", "")
            Agent_Credit_Info(str)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    'Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim agent = CType(sender, LinkButton).CommandArgument.ToString
    '    Response.Write("<script>javascript:window.open('Update_credit.aspx?Agn=" & agent & "','Update','scrollbars=yes,width=800,height=400,top=20,left=150')</script>")
    '    Agent_Credit_Info()
    'End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            Agent_Credit_Info()
            GridView1.DataSource = SortDataTable(CType(GridView1.DataSource, DataSet), True)
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Function SortDataTable(ByVal ds As DataSet, ByVal isPageIndexChanging As Boolean) As DataView

        Dim dt As New DataTable
        dt = ds.Tables(0)
        If Not dt Is Nothing Then

            Dim dv As New DataView(dt)
            If GridViewSortExpression <> String.Empty Then
                Try
                    If isPageIndexChanging Then
                        dv.Sort = String.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection)
                    Else
                        dv.Sort = String.Format("{0} {1}", GridViewSortExpression, GetSortDirection())
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)

                End Try
            End If
            Return dv
        Else
            Return New DataView()
        End If

    End Function
    Private Property GridViewSortDirection() As String

        Get
            Return IIf(ViewState("SortDirection") = Nothing, "ASC", ViewState("SortDirection"))
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    Private Property GridViewSortExpression() As String
        Get
            Return IIf(ViewState("SortExpression") = Nothing, String.Empty, ViewState("SortExpression"))
        End Get
        Set(ByVal value As String)
            ViewState("SortExpression") = value
        End Set
    End Property
    Private Function GetSortDirection() As String
        Select Case GridViewSortDirection
            Case "ASC"
                GridViewSortDirection = "DESC"
            Case "DESC"
                GridViewSortDirection = "ASC"
        End Select
        Return GridViewSortDirection
    End Function
    Protected Sub searchAgency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchAgency.Click
        Try
            Agent_Credit_Info(AgName.Value.ToString.Trim.Replace("'", ""))
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
End Class

