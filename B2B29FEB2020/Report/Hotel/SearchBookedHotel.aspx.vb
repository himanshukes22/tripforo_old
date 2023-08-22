Imports System.Xml
Partial Class SprReports_Hotel_SearchBookedHotel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx", False)
        End If
    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Search.Click
        'Dim SearchDetails As New HtlLibrary.Htl_Property
        'Dim HtlRes As New HtlLibrary.GTA_ResponseXML
        'Try
        '    SearchDetails.CheckInDate = Request("From").Trim()
        '    SearchDetails.CheckOutDate = Request("To").Trim()
        '    SearchDetails.HBID = Request("orderidTxt").Trim()
        '    SearchDetails.bookingStatus = BookingStatus.SelectedValue
        '    SearchDetails.Inventry = ReqType.SelectedValue
        '    SearchDetails.AgentID = Session("UID").ToString()

        '    If ReqType.SelectedValue = "XMLtype" Then
        '        Dim doc As XmlDocument = New XmlDocument()
        '        doc.LoadXml(HtlRes.SearchBookingItem(SearchDetails))
        '        doc.Save(Server.MapPath("Booked-Hotel.xml"))
        '        Response.Write("<script> window.open('Booked-Hotel.xml','_blank') </script>")
        '    Else
        '        BookedHotel.InnerHtml = HtlRes.SearchBookingItem(SearchDetails)
        '    End If
        'Catch ex As Exception
        '    ''  HtlLibrary.HtlLog.InsertLogDetails(ex)
        'End Try
    End Sub
End Class
