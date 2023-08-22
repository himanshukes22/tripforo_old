Imports Microsoft.VisualBasic
Imports System.data
Imports System.Web.HttpContext
Imports System.Web.SessionState
Imports System.Math
Imports System.Data.SqlClient
Public Class markup
    Public Function AirlineMarkUp(ByVal type As String, ByVal airline As String, ByVal noofpax As Integer, ByVal agentid As String) As String
        Dim cmd As SqlCommand
        Dim con As New SqlConnection
        Dim agnt_type As String = ""
        agnt_type = Current.Session("agent_type")
        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
        Dim mkdt As New DataTable
        Dim mkadp As SqlDataAdapter
        Dim totalmark As String = ""
        Dim mrkup As Double = 0
        Try
            cmd = New SqlCommand(Agent_MRK(type, agentid, airline), con)
            mkadp = New SqlDataAdapter(cmd)
            mkadp.Fill(mkdt)
            If mkdt.Rows.Count > 0 Then
                Try
                    If mkdt.Rows(0)("markup") = "" Or mkdt.Rows(0)("markup") Is Nothing Or mkdt.Rows(0)("markup") = "0" Then
                        mrkup = 0
                    Else
                        mrkup = Convert.ToDouble(mkdt.Rows(0)("markup"))
                    End If
                Catch ex As Exception
                    mrkup = 0
                End Try
                totalmark = (Format((mrkup * noofpax), "####")).ToString
            Else
                totalmark = "0"
            End If
        Catch ex As Exception
            totalmark = "0"
        End Try
        Return totalmark
    End Function
    Private Function Agent_MRK(ByVal type As String, ByVal agentid As String, ByVal airline As String) As String
        Dim str As String = ""
        Try
            If type = "ADMIN" Then
                str = "select airline,markup from Mrk_admin WHERE (user_id='" & agentid.Trim & "') and airline='" & airline.Trim.ToUpper & "'"
            ElseIf type = "AGENT" Then
                str = "select airline,markup from airline_markup WHERE (user_id='" & agentid.Trim & "') and airline='" & airline.Trim.ToUpper & "'"
            End If
        Catch ex As Exception
        End Try
        Return str
    End Function
End Class
