Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data

Partial Public Class PassengerDetail
    Inherits System.Web.UI.Page
    Private P As New ProxyClass()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            BindAdult()
            BindChild()
            BindInfrant()
            BindProxyDetail()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindAdult()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewAdult.DataSource = P.ShowAdult(Convert.ToInt32(ProxyID))
            GridViewAdult.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindChild()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewChild.DataSource = P.ShowChild(Convert.ToInt32(ProxyID))
            GridViewChild.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindInfrant()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewInfrant.DataSource = P.ShowInfrant(Convert.ToInt32(ProxyID))
            GridViewInfrant.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindProxyDetail()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            Dim ds As New DataSet()
            ds = P.ShowProxyByID(Convert.ToInt32(ProxyID))
            Dim dt As New DataTable()
            dt = ds.Tables(0)
            td_AgentID.InnerText = dt.Rows(0)("AgentID").ToString()
            td_BookingType.InnerText = dt.Rows(0)("BookingType").ToString()
            td_TravelType.InnerText = dt.Rows(0)("TravelType").ToString()
            td_From.InnerText = dt.Rows(0)("ProxyFrom").ToString()
            td_To.InnerText = dt.Rows(0)("ProxyTo").ToString()
            td_DepartDate.InnerText = dt.Rows(0)("DepartDate").ToString()
            td_RetDate.InnerText = dt.Rows(0)("ReturnDate").ToString()
            td_DepartTime.InnerText = dt.Rows(0)("DepartTime").ToString()
            td_RetTime.InnerText = dt.Rows(0)("ReturnTime").ToString()
            td_Adult.InnerText = dt.Rows(0)("Adult").ToString()
            td_Child.InnerText = dt.Rows(0)("Child").ToString()
            td_Infrant.InnerText = dt.Rows(0)("Infrant").ToString()

            td_Class.InnerText = dt.Rows(0)("Class").ToString()
            td_Airline.InnerText = dt.Rows(0)("Airlines").ToString()
            td_Classes.InnerText = dt.Rows(0)("Classes").ToString()
            td_PMode.InnerText = dt.Rows(0)("PaymentMode").ToString()
            td_Remark.InnerText = dt.Rows(0)("Remark").ToString()
            td_Reject.InnerText = dt.Rows(0)("RejectComment").ToString()

            If td_Reject.InnerText = "" Then
                tr_reject.Visible = False
            Else
                tr_reject.Visible = True
            End If

            Dim ds1 As New DataSet()
            ds1 = P.FullAgentDetail(td_AgentID.InnerText)
            Dim dt1 As New DataTable()
            dt1 = ds1.Tables(0)
            td_AgentName.InnerText = dt1.Rows(0)("Name").ToString()
            td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString()
            td_Street.InnerText = dt1.Rows(0)("Addr").ToString()
            td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
            td_Email.InnerText = dt1.Rows(0)("Email").ToString()
            td_AgencyName.InnerText = dt1.Rows(0)("Agency_Name").ToString()
            td_CardLimit.InnerText = dt1.Rows(0)("Crd_Limit").ToString()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub RDBAdult(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim extraRow As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)
                Dim tc As TableCell = New DataControlFieldCell(DirectCast(e.Row.Cells(2), DataControlFieldCell).ContainingField)
                tc.Text = "<b>Adult Detail</b>"
                tc.ColumnSpan = e.Row.Cells.Count
                tc.BackColor = System.Drawing.Color.Gray
                extraRow.Cells.Add(tc)

                e.Row.Parent.Controls.AddAt(0, extraRow)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub RBDChild(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim extraRow As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)
                Dim tc As TableCell = New DataControlFieldCell(DirectCast(e.Row.Cells(2), DataControlFieldCell).ContainingField)
                'DataSet ds2 = new DataSet();
                'string c;
                'c = Session["agentcode"].ToString();
                'ds2 = Led.VendorInfo(c);
                'Label lbl = new Label();
                'lbl.Text = ds2.Tables[0].Rows[0][4].ToString();
                'tc.Text = "Opening balance:" + lbl.Text;
                tc.Text = "<b>Child Detail</b>"
                tc.BackColor = System.Drawing.Color.Gray
                tc.ColumnSpan = e.Row.Cells.Count
                extraRow.Cells.Add(tc)

                e.Row.Parent.Controls.AddAt(0, extraRow)

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub RDBInfrant(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim extraRow As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)
                Dim tc As TableCell = New DataControlFieldCell(DirectCast(e.Row.Cells(2), DataControlFieldCell).ContainingField)
                tc.Text = "<b>Infant Detail</b>"
                tc.BackColor = System.Drawing.Color.Gray
                tc.ColumnSpan = e.Row.Cells.Count
                extraRow.Cells.Add(tc)

                e.Row.Parent.Controls.AddAt(0, extraRow)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

End Class