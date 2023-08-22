Imports System.Data
Partial Class HtlRefundInProcces
    Inherits System.Web.UI.Page
    Private ST As New HotelDAL.HotelDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx", False)
            End If
            If Not IsPostBack Then
                BindGrid()
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim TripType As String = ""
            If Session("TripExec").ToString() = "D" Then
                TripType = "Domestic"
            ElseIf Session("TripExec").ToString() = "I" Then
                TripType = "International"
            End If
            InproccesGrd.DataSource = ST.HtlRefundDetail(StatusClass.InProcess.ToString(), "", Session("UID").ToString, "InproceGET", TripType)
            InproccesGrd.DataBind()
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try


    End Sub
    Protected Sub btnRemark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemark.Click
        Try
            'Update Status and Remark in CancellationIntl Table after Reject
            Dim i As Integer = ST.HtlRefundUpdates(StatusClass.Rejected.ToString(), Request("OrderIDS"), Session("UID").ToString, "RejInproce", Request("txtRemark").Trim())
            If i > 0 Then
                Page.ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Reject succesfully'); ", True)
                BindGrid()
            End If
        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, "")
        End Try
    End Sub

    Protected Sub lnkupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnkupdate As String = CType(sender, LinkButton).CommandArgument.ToString()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "window.open('HtlRefundUpdate.aspx?OrderID=" & lnkupdate & "','Print','scrollbars=yes,width=1100px,height=490,top=20,left=150');", True)
        BindGrid()
    End Sub
End Class
