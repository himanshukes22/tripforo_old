Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class UserControl_FltSearchFixDep
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            Try


                Dim constr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("SurceListProc")
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = con
                        con.Open()
                        Sector.DataSource = cmd.ExecuteReader()
                        Sector.DataTextField = "Dest"
                        Sector.DataValueField = "HDest"
                        Sector.DataBind()
                        con.Close()
                    End Using
                End Using
                Sector.Items.Insert(0, New ListItem("--Select Sector--", "0"))
            Catch ex As Exception

            End Try

        End If
    End Sub

End Class
