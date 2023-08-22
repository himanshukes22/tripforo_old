Imports System.Data

Partial Class SprReports_Hotel_HotelBookingLog
    Inherits System.Web.UI.Page

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        Try
            Dim sqltrans As New HotelDAL.HotelDA()
            Dim logds As New DataSet()
            Dim noofrow As Integer = 1
            If String.IsNullOrEmpty(Request("NoofRow")) = False Then
                noofrow = Request("NoofRow")
            End If
            logds = sqltrans.SelectHotelLog(noofrow, Request("txtOrderId"), Request("txtAgentID"), Provider.SelectedValue, ddlReqType.SelectedValue)
            reqResp.Text = "No recard found"
            If logds.Tables(0).Rows.Count > 0 Then
                Dim str As String = "<table>"
                If ddlReqType.SelectedIndex = 9 Then
                    str = str & "<tr><td style='font-weight: bold;'>latest " & logds.Tables(0).Rows.Count & " Error Log Details</td></tr>"
                    str = str & "<tr><td style='font-weight: bold;'>Page Name</td><td style='font-weight: bold;'>Error Message</td><td style='font-weight: bold;'>Line Number</td><td style='font-weight: bold;'>Date</td></tr>"
                    For i As Integer = 0 To logds.Tables(0).Rows.Count - 1
                        str = str & "<tr><td>" & logds.Tables(0).Rows(i)("PageName").ToString() & "</td>"
                        str = str & "<td>" & logds.Tables(0).Rows(i)("ErrorMessage").ToString() & "</td>"
                        str = str & "<td>" & logds.Tables(0).Rows(i)("LineNumber").ToString() & "</td>"
                        str = str & "<td>" & logds.Tables(0).Rows(i)("CreatedDate").ToString() & "</td></tr>"
                    Next
                Else
                    For i As Integer = 0 To logds.Tables(0).Rows.Count - 1
                        If (logds.Tables(0).Rows(i)("req").ToString() <> "") Then
                            str = str & "<tr><td style='font-weight: bold;'>(" & logds.Tables(0).Rows(i)("HTLOrderID") & ") " & ddlReqType.SelectedValue & " Request " & i + 1 & " ( " & logds.Tables(0).Rows(i)("LoginID").ToString() & " )</td></tr>"
                            str = str & "<tr><td>" & HttpUtility.HtmlEncode(logds.Tables(0).Rows(i)("req").ToString()) & "</td></tr>"
                            str = str & "<tr><td style='font-weight: bold;'>" & ddlReqType.SelectedValue & " Response " & i + 1 & " ( Date: " & logds.Tables(0).Rows(i)("CreateDate").ToString() & " )</td></tr>"
                            str = str & "<tr><td>" & HttpUtility.HtmlEncode(logds.Tables(0).Rows(i)("resp").ToString()) & "</td></tr>"
                        End If
                    Next
                End If
                reqResp.Text = str & "</table>"
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    'Public Function SelectHotelLog(ByVal top As String, ByVal orederid As String, ByVal AgentID As String, ByVal ReqType As String) As DataSet
    '    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("HTLConnStr").ConnectionString)
    '    Dim adap As SqlDataAdapter
    '    Dim ds As New DataSet()
    '    Try
    '        adap = New SqlDataAdapter("SelectHotelLog", con)
    '        adap.SelectCommand.CommandType = CommandType.StoredProcedure
    '        adap.SelectCommand.Parameters.AddWithValue("@top", top)
    '        adap.SelectCommand.Parameters.AddWithValue("@orderid", orederid)
    '        adap.SelectCommand.Parameters.AddWithValue("@AgentID", AgentID)
    '        adap.SelectCommand.Parameters.AddWithValue("@ReqType", ReqType)
    '        adap.Fill(ds)
    '    Catch ex As Exception
    '        HtlLibrary.HtlLog.InsertLogDetails(ex)
    '    End Try
    '    Return ds
    'End Function
End Class
