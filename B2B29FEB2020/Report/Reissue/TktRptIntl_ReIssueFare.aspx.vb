Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Partial Class TktRptIntl_ReIssueFare
    Inherits System.Web.UI.Page
    Private ST As New SqlTransaction()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If Request.QueryString("counter") <> "" AndAlso Request.QueryString IsNot Nothing Then
            Dim ds As New DataSet()
            ds = ST.GetReIssueCancelIntl(Convert.ToInt32(Request.QueryString("counter")), "R", StatusClass.InProcess)                'ST.GetReIssueedIntl(Request.QueryString("tktno"))
            Try
                'ds = bst.FareDetails(Request.QueryString("tktno").ToString())
                lblbasefare.Text = Convert.ToString(ds.Tables(0).Rows(0)("base_fare"))
                lbltax.Text = Convert.ToString(ds.Tables(0).Rows(0)("Tax"))
                lblyq.Text = Convert.ToString(ds.Tables(0).Rows(0)("YQ"))
                lblservicetax.Text = Convert.ToString(ds.Tables(0).Rows(0)("service_tax"))
                lbltranfee.Text = Convert.ToString(ds.Tables(0).Rows(0)("tran_fees"))
                lbldiscount.Text = Convert.ToString(ds.Tables(0).Rows(0)("Discount"))
                lblcb.Text = Convert.ToString(ds.Tables(0).Rows(0)("CB"))
                lbltds.Text = Convert.ToString(ds.Tables(0).Rows(0)("TDS"))
                lblTbc.Text = Convert.ToString(ds.Tables(0).Rows(0)("TotalFare"))
                lblTbcAftrDis.Text = Convert.ToString(ds.Tables(0).Rows(0)("TotalFareAfterDiscount"))
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)

            End Try
        End If
    End Sub
End Class
