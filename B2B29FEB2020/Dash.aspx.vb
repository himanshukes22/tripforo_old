Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Partial Class Dash
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim AgentID As String = ""
        Dim dstval As DataSet = New DataSet
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not IsPostBack Then
                If Session("User_Type") = "AGENT" Then
                    AgentID = Session("UID").ToString()
                    dstval = Checkallreport(Session("User_Type"))
                    BindGrid(dstval)
                End If
                Dim trip As String = ""


                

                Dim Db As String = ""
                Dim sum As Double = 0

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_result_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_result.Click
        Try
            Dim dstval As DataSet = New DataSet
            dstval = Checkallreport(Session("User_Type"))
            BindGrid(dstval)

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Private Sub BindGrid(ByVal dstval As DataSet)
        Try
            Dim fltheaderDT As DataTable = dstval.Tables(0)
            Dim RefundDT As DataTable = dstval.Tables(1)
            Dim PGDT As DataTable = dstval.Tables(2)
            Dim FltsaleDT As DataTable = dstval.Tables(3)
            Dim cashinflowDT As DataTable = dstval.Tables(4)
            Dim cashoutflowDT As DataTable = dstval.Tables(5)
            If (fltheaderDT.Rows.Count > 0) Then
                lbltktcost.Text = fltheaderDT.Rows(0)("TotalBookingCost")
                lbltktcount.Text = fltheaderDT.Rows(0)("Totaltkt")

            End If
            If (RefundDT.Rows.Count > 0) Then
                lblrefcost.Text = RefundDT.Rows(0)("Totprice")
                lblrefcount.Text = RefundDT.Rows(0)("Totalnumber")

            End If
            If (PGDT.Rows.Count > 0) Then
                lblpgcost.Text = PGDT.Rows(0)("totalpgamount")
                lblpgcount.Text = PGDT.Rows(0)("totalcount")

            End If
            If (FltsaleDT.Rows.Count > 0) Then
                Dim Saletable As String = ""
                For i As Integer = 1 To FltsaleDT.Rows.Count
                    Saletable += "<tr>"
                    Saletable += "<td>" + FltsaleDT.Rows(0)("AirlineName").ToString() + "</td>"
                    Saletable += "<td>" + FltsaleDT.Rows(0)("VC").ToString() + "</td>"
                    Saletable += "<td>" + FltsaleDT.Rows(0)("TotalBookingCost").ToString() + "</td>"
                    Saletable += "<td>" + FltsaleDT.Rows(0)("TotalAfterDis").ToString() + "</td>"
                    Saletable += "<td>" + FltsaleDT.Rows(0)("TotalCommission").ToString() + "</td>"
                    Saletable += "</tr>"
                Next
                tbbody.InnerHtml = Saletable
            End If
            If (cashinflowDT.Rows.Count > 0) Then
                lblcashinflow.Text = cashinflowDT.Rows(0)("cashinflow")
                lblcashinflowcount.Text = cashinflowDT.Rows(0)("cashinflowcount")

            End If
            If (cashoutflowDT.Rows.Count > 0) Then
                lblcashoutflow.Text = cashoutflowDT.Rows(0)("cashoutflow")
                lblcashoutflowcount.Text = cashoutflowDT.Rows(0)("cashoutflowcount")

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    Public Function Checkallreport(ByVal type As String) As DataSet
        Dim cmd As New SqlCommand()
        Dim ErrorMsg As String = ""

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
        Dim dst As DataSet = New DataSet()
        Try
            Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            cmd.CommandText = "Sp_dashboarddata"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@agentid", SqlDbType.VarChar).Value = Session("UID").ToString()
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type
            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = FromDate
            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = ToDate
            cmd.Connection = con1
            con1.Open()
            Dim sdr As SqlDataAdapter = New SqlDataAdapter(cmd)

            sdr.Fill(dst)


            ''ErrorMsg = cmd.ExecuteScalar()
            cmd.Dispose()
            con1.Close()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        Return dst
    End Function
End Class
