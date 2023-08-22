Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Public Class Details
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private ds As New DataSet()
    Private dsm As New DataSet()

    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    Public Function AgencyInfo(ByVal userid As String) As DataSet
        adap = New SqlDataAdapter("select * from agent_register where User_Id='" & userid & "'", con)
        adap.Fill(ds)
        Return ds

    End Function

    Public Function GetMarquueemsg(ByVal servtype As String) As DataSet
        Try
            adap = New SqlDataAdapter("Select Message from MarqueeDetails where ServiceType='" & servtype & "'", con)
            adap.Fill(dsm)
            'Return dsm
        Catch ex As Exception

        End Try
        Return dsm
    End Function

End Class