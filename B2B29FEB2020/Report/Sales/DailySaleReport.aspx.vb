Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class SprReports_Sales_DailySaleReport
    Inherits System.Web.UI.Page
    Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim cmd As SqlCommand
    Dim dt As DataTable
    Protected Sub btninsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btninsert.Click
        Try
            Dim FromDate As String
            FromDate = Strings.Mid((Request("From")).Split(" ")(0), 4, 2) + "/" + Strings.Left((Request("From")).Split(" ")(0), 2) + "/" + Strings.Right((Request("From")).Split(" ")(0), 4)
            Dim AgentID As String = Request("txtAgencyName1")
            'Dim Ag As String() = Request("txtAgencyName").Split("("c)
            'Dim agencyname As String = Ag(0).ToString
            cn.Open()

            Dim cmd As New SqlCommand("sp_InsertDSL", cn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@date", FromDate)
            cmd.Parameters.AddWithValue("@agencycity", txtagencycity.Text)
            cmd.Parameters.AddWithValue("@agencyname", AgentID)
            cmd.Parameters.AddWithValue("@ctcperson", txtctcperson.Text)
            cmd.Parameters.AddWithValue("@ctcpersonno", txtctcno.Text)
            cmd.Parameters.AddWithValue("@remark", txtremark.Text)
            cmd.Parameters.AddWithValue("@userid", Session("UID"))
            cmd.ExecuteNonQuery()
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Report Submmitted Successfully');", True)
            cn.Close()
            'txtdate.Text = ""
            txtagencycity.Text = ""
            'txtagencyname.Text = ""
            txtctcno.Text = ""
            txtctcperson.Text = ""
            txtremark.Text = ""


        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        End Try
    End Sub
End Class
