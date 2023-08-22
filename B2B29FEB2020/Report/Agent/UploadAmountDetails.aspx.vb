Imports System
Imports System.Collections.Generic

Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Web.Security
Partial Class Reports_Agent_UploadAmountDetails
    Inherits System.Web.UI.Page
    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_Type") = "AGENT" Then
                td_Agency.Visible = False
                'AgentID = Session("UID").ToString()
            End If
            If Session("User_Type") = "DI" Then
                tr_SearchType.Visible = True
            End If

            'Dim curr_date = Now.Date() & " " & "12:00:00 AM"
            'Dim curr_date1 = Now()
            'grd_deposit.DataSource = STDom.GetDepositDetails(curr_date, curr_date1, Session("User_Type").ToString, Session("UID"), "", "", "")
            'grd_deposit.DataBind()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub btn_showdetails_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_showdetails.Click
        'If txt_from.Text <> "" AndAlso txt_to.Text <> "" Then
        '    fromdate = txt_from.Text.Substring(0, 10)
        '    todate = txt_to.Text.Substring(0, 10)
        '    Dim ds As New DataSet()
        '    ds = upload.GetDepositDetailsWithdate(fromdate, todate)
        '    grd_deposit.DataSource = ds
        '    grd_deposit.DataBind()
        Try


            Dim FromDate As String
            Dim ToDate As String
            If [String].IsNullOrEmpty(Request("From")) Then
                FromDate = ""
            Else
                'FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + Strings.Left((Request("From")).Split(" ")(0), 2) + Strings.Right((Request("From")).Split(" ")(0), 4)

                FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
                FromDate = FromDate + " " + "12:00:00 AM"
            End If
            If [String].IsNullOrEmpty(Request("To")) Then
                ToDate = ""
            Else
                ToDate = Mid((Request("To")).Split(" ")(0), 4, 2) & "/" & Left((Request("To")).Split(" ")(0), 2) & "/" & Right((Request("To")).Split(" ")(0), 4)
                ToDate = ToDate & " " & "11:59:59 PM"
            End If
            Dim AgentID As String = If([String].IsNullOrEmpty(Request("hidtxtAgencyName")) Or Request("hidtxtAgencyName") = "Agency Name or ID", "", Request("hidtxtAgencyName"))
            Dim PaymentType As String = ""
            If (ddl_PType.SelectedIndex > 0) Then
                PaymentType = ddl_PType.SelectedValue
            Else
                PaymentType = ""
            End If
            Dim Status As String = ""
            If (ddl_status.SelectedIndex > 0) Then

                If DropDownListADJ.SelectedValue = "AD" Then
                    Status = "AD" + ddl_status.SelectedValue

                Else : Status = ddl_status.SelectedValue

                End If


            Else

                If DropDownListADJ.SelectedValue = "AD" Then
                    Status = "AD"
                ElseIf DropDownListADJ.SelectedValue = "FU" Then
                    Status = "FU"
                Else : Status = ""
                End If

            End If
            Dim SearchType As String = ""
            If (RB_Agent.Checked = True AndAlso Session("User_Type") = "DI") Then
                SearchType = RB_Agent.Text
            Else
                SearchType = RB_Distr.Text
            End If
            Dim DtSearch As New DataTable

            DtSearch = STDom.GetDepositDetails(FromDate, ToDate, Session("User_Type"), Session("UID"), PaymentType, AgentID, Status, SearchType).Tables(0)
            grd_deposit.DataSource = DtSearch
            grd_deposit.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
End Class
