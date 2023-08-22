Imports System.Data
Partial Class ImportPnrDetailsIntl
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim pinfo As New pnrinfo
    Dim ST As New SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ds = ST.ImportPNRDetailsIntl("Pending", "I", Request("OrderId"))
            If ds.Tables.Count > 1 Then
                GridViewshow.DataSource = ds.Tables(0)
                GridViewshow.DataBind()
                If ds.Tables(1).Rows.Count > 0 Then
                    Dim paxStr As String = "Passenger Details : <br/>"
                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                        paxStr = paxStr & (i + 1).ToString & ". " & ds.Tables(1).Rows(i)("Title") & " " & ds.Tables(1).Rows(i)("FName") & " " & ds.Tables(1).Rows(i)("LName") & "<br/>"
                    Next
                    paxdiv.InnerHtml = paxStr
                Else
                End If
            Else
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
End Class