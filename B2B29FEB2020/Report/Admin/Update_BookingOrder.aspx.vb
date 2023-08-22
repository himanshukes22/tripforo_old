Imports System.Data
Imports System.Data.SqlClient

Partial Class Update_BookingOrder
    Inherits System.Web.UI.Page

    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Private ObjIntDetails As New IntlDetails()
    Dim objTktCopy As New clsTicketCopy
    Dim SelectDetails As New IntlDetails



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Dim OrderId As String = Request.QueryString("OrderId")
                Dim TransTD As String = Request.QueryString("TransID")
                tdRefNo.InnerText = OrderId.ToString()
                'Getting Header Details
                BindFltHeader()

                'Getting Pax Details
                BindTravellerInformation()

                'Getting Flight Details
                BindFlightDetails()

                'Getting Fare Details
                lblFareInformation.Text = objTktCopy.FareDetail(OrderId, TransTD)

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindFltHeader()
        Try
            Dim OrderId As String = Request.QueryString("OrderId")
            Dim dtHeader As New DataTable
            dtHeader = SelectHeaderDetail() 'SelectDetails.SelectHeaderDetail(OrderId)
            GvFlightHeader.DataSource = dtHeader
            GvFlightHeader.DataBind()

        Catch ex As Exception

        End Try
    End Sub

   


    Public Sub BindTravellerInformation()
        Try
            ' Dim adap As New SqlDataAdapter("select * from fltPaxDetails where OrderId = '" & OrderId & "' ", con)
            'Dim dtPaxDetails As New DataTable
            'adap.Fill(dtPaxDetails)
            Dim OrderId As String = Request.QueryString("OrderId")
            'Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim adap As New SqlDataAdapter("usp_FltPaxDetails_lookup", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@orderid", OrderId.Trim)
            Dim dt2 As New DataTable()
            adap.Fill(dt2)
            GvTravellerInformation.DataSource = dt2
            GvTravellerInformation.DataBind()

        Catch ex As Exception

        End Try

    End Sub


    Public Function SelectHeaderDetail() As DataTable
        Dim dt2 As New DataTable()
        Try
            ' Dim adap As New SqlDataAdapter("select * from fltPaxDetails where OrderId = '" & OrderId & "' ", con)
            'Dim dtPaxDetails As New DataTable
            'adap.Fill(dtPaxDetails)
            Dim OrderId As String = Request.QueryString("OrderId")
            'Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim adap As New SqlDataAdapter("usp_Get_FltHeaderDetailsWithPnrMessage", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@orderid", OrderId.Trim)

            adap.Fill(dt2)


        Catch ex As Exception

        End Try
        Return dt2
    End Function


    Public Sub BindFlightDetails()
        Try
            Dim OrderId As String = Request.QueryString("OrderId")
            Dim adapFlightDetails As New SqlDataAdapter("select * from FltDetails where OrderId = '" & OrderId & "' ", con)
            Dim dtFlightDetails As New DataTable
            adapFlightDetails.Fill(dtFlightDetails)
            GvFlightDetails.DataSource = dtFlightDetails
            GvFlightDetails.DataBind()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub GvFlightHeader_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GvFlightHeader.RowCancelingEdit
        Try
            GvFlightHeader.EditIndex = -1
            GvFlightHeader.Columns(4).Visible = False
            BindFltHeader()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvFlightHeader_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GvFlightHeader.RowEditing
        Try
            GvFlightHeader.EditIndex = e.NewEditIndex
            GvFlightHeader.Columns(4).Visible = True
            BindFltHeader()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvFlightHeader_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GvFlightHeader.RowUpdating

        Try
            Dim txtRemark As TextBox = TryCast(DirectCast(GvFlightHeader.Rows(e.RowIndex).FindControl("txtRemark"), TextBox), TextBox)
            If txtRemark.Text = "" Then
                ShowAlertMessage("Please Enter Remark")
            Else
                Dim OrderId As String = Request.QueryString("OrderId")
                Dim txtGdsPnr As TextBox = TryCast(DirectCast(GvFlightHeader.Rows(e.RowIndex).FindControl("txtGdsPnr"), TextBox), TextBox)
                Dim txtAirlinePnr As TextBox = TryCast(DirectCast(GvFlightHeader.Rows(e.RowIndex).FindControl("txtAirlinePnr"), TextBox), TextBox)
                Dim txtStatus As TextBox = TryCast(DirectCast(GvFlightHeader.Rows(e.RowIndex).FindControl("txtStatus"), TextBox), TextBox)

                SelectDetails.UpdateFlightHeader(txtGdsPnr.Text.Trim(), txtAirlinePnr.Text.Trim(), txtStatus.Text.Trim(), OrderId)
                SelectDetails.InsertQCFlightHeader(OrderId, "FltHeader", Session("UID").ToString(), txtRemark.Text)

                lblUpdateFltHeader.Visible = True
                lblUpdateFltHeader.Text = "Updated Successfully"
                GvFlightHeader.Columns(4).Visible = False
                GvFlightHeader.EditIndex = -1
                BindFltHeader()

            End If

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub GvTravellerInformation_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GvTravellerInformation.RowCancelingEdit
        Try

            GvTravellerInformation.EditIndex = -1
            GvTravellerInformation.Columns(6).Visible = False
            BindTravellerInformation()


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvTravellerInformation_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GvTravellerInformation.RowEditing
        Try
            GvTravellerInformation.EditIndex = e.NewEditIndex
            GvTravellerInformation.Columns(6).Visible = True
            BindTravellerInformation()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvTravellerInformation_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GvTravellerInformation.RowUpdating
        Try
            Dim txtRemark As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtRemark"), TextBox), TextBox)
            If txtRemark.Text = "" Then
                ShowAlertMessage("Please Enter Remark")
            Else

                Dim OrderId As String = Request.QueryString("OrderId")
                Dim lblPaxId As Label = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("lblPaxId"), Label), Label)
                Dim txtTitle As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtTitle"), TextBox), TextBox)
                Dim txtFname As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtFname"), TextBox), TextBox)
                Dim txtLname As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtLname"), TextBox), TextBox)
                Dim txtType As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtType"), TextBox), TextBox)
                Dim txtTktNo As TextBox = TryCast(DirectCast(GvTravellerInformation.Rows(e.RowIndex).FindControl("txtTktNo"), TextBox), TextBox)

                SelectDetails.UpdatePaxInformation(txtTitle.Text.Trim(), txtFname.Text.Trim(), txtLname.Text.Trim(), txtType.Text.Trim(), txtTktNo.Text.Trim(), OrderId, lblPaxId.Text.Trim())
                SelectDetails.InsertQCFlightHeader(OrderId, "FltPaxDetails", Session("UID").ToString(), txtRemark.Text)
                lblUpdatePax.Visible = True
                lblUpdatePax.Text = "Updated Successfully"
                GvTravellerInformation.Columns(6).Visible = False
                GvTravellerInformation.EditIndex = -1
                BindTravellerInformation()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvFlightDetails_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GvFlightDetails.RowEditing
        Try

            GvFlightDetails.EditIndex = e.NewEditIndex
            BindFlightDetails()
            GvFlightDetails.Columns(12).Visible = True


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvFlightDetails_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GvFlightDetails.RowCancelingEdit
        Try

            GvFlightDetails.EditIndex = -1
            GvFlightDetails.Columns(12).Visible = False
            BindFlightDetails()

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub GvFlightDetails_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GvFlightDetails.RowUpdating

        Try
            Dim txtRemark As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtRemark"), TextBox), TextBox)
            If txtRemark.Text = "" Then
                ShowAlertMessage("Please Enter Remark")
            Else
                Dim OrderId As String = Request.QueryString("OrderId")
                Dim lblFltId As Label = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("lblFltId"), Label), Label)
                Dim txtDepcityName As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtDepcityName"), TextBox), TextBox)
                Dim txtDepcityCode As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtDepcityCode"), TextBox), TextBox)
                Dim txtArrcityName As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtArrcityName"), TextBox), TextBox)
                Dim txtArrcityCode As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtArrcityCode"), TextBox), TextBox)
                Dim txtAirlineName As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtAirlineName"), TextBox), TextBox)
                Dim txtAirlineCode As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtAirlineCode"), TextBox), TextBox)
                Dim txtFltNo As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtFltNo"), TextBox), TextBox)
                Dim txtDepDate As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtDepDate"), TextBox), TextBox)
                Dim txtDepTime As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtDepTime"), TextBox), TextBox)
                Dim txtArrTime As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtArrTime"), TextBox), TextBox)
                Dim txtAirCraft As TextBox = TryCast(DirectCast(GvFlightDetails.Rows(e.RowIndex).FindControl("txtAirCraft"), TextBox), TextBox)

                SelectDetails.UpdateFlightDetails(txtDepcityName.Text.Trim(), txtDepcityCode.Text.Trim(), txtArrcityName.Text.Trim(), txtArrcityCode.Text.Trim(), txtAirlineName.Text.Trim(), txtAirlineCode.Text.Trim(), txtFltNo.Text.Trim(), txtDepDate.Text.Trim(), txtDepTime.Text.Trim(), txtArrTime.Text.Trim(), txtAirCraft.Text.Trim(), OrderId, lblFltId.Text)
                SelectDetails.InsertQCFlightHeader(OrderId, "FltDetails", Session("UID").ToString(), txtRemark.Text)

                lblUpdateFlight.Visible = True
                lblUpdateFlight.Text = "Updated Successfully"
                GvFlightDetails.Columns(12).Visible = False
                GvFlightDetails.EditIndex = -1
                BindFlightDetails()


            End If



        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
End Class
