Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Security
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Xml
Imports IPTracker


Partial Class MasterforFlight
    Inherits System.Web.UI.MasterPage
    Private id As String
    Private usertype As String
    Private typeid As String
    Private ds As DataSet
    Private dsm As DataSet
    Private con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Private adap As SqlDataAdapter
    Private det As New Details()
    Private dtm As DataTable
    Private servtype As String
    Public AgencyName As String = ""
    Public AgencyId As String = ""
    Public LoginId As String = ""
    Public LoginType As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing Then
                'Check Login Type Staff or Agent
                Try
                    AgencyName = Session("AgencyName")
                    AgencyId = Session("AgencyId")
                    LoginId = Session("UID")
                    LoginType = "AGENT"
                    If (Not String.IsNullOrEmpty(Convert.ToString(Session("LoginByStaff"))) AndAlso Convert.ToString(Session("LoginByStaff")) = "true") Then
                        AgencyId = Session("StaffUserId")
                        LoginId = AgencyId
                        divAgentBalance.Visible = False
                        LoginType = "STAFF"
                    End If
                Catch ex As Exception

                End Try
                Dim State As New StateCollection()
                Dim objIP As New IPDetails()
                State.SessionID = Session.SessionID
                State.Path = Request.CurrentExecutionFilePath
                State.Username = Session("UID").ToString() 'Page.User.Identity.Name
                State.VISTING_TIME = DateTime.Now.ToString()
                Dim objST As New SessionTrack()
                objST.Add(State, Request.CurrentExecutionFilePath)
            End If

        Catch ex As Exception

        End Try
        Try
            If Not IsPostBack Then
                If Session("UID") <> "" AndAlso Session("UID") IsNot Nothing AndAlso Session("UserType") <> "" AndAlso Session("UserType") IsNot Nothing AndAlso Session("TypeID") <> "" AndAlso Session("TypeID") IsNot Nothing Then
                    id = Session("UID").ToString()
                    usertype = Session("UserType").ToString()
                    servtype = "Flight"
                    'div_Series.Visible = False
                    typeid = Session("TypeID").ToString()
                    If usertype = "AD" Then
                        lblagency.Text = Session("ADMINLogin")
                        crdrow.Visible = False
                        tr_AgencyID.Visible = False
                    ElseIf usertype = "AC" Then
                        lblagency.Text = "Accounts"
                        crdrow.Visible = False
                        tr_AgencyID.Visible = False
                    ElseIf usertype = "EC" Then
                        lblagency.Text = Session("UID").ToString()
                        crdrow.Visible = False
                        tr_AgencyID.Visible = False
                    ElseIf usertype = "SE" Then
                        lblagency.Text = Session("UID").ToString()
                        crdrow.Visible = False
                        tr_AgencyID.Visible = False
                    ElseIf usertype = "TA" Then
                        '' ds = det.AgencyInfo(id)
                        ''If ds.Tables(0).Rows.Count > 0 Then
                        lblagency.Text = Session("AgencyName")
                        lblCamt.Visible = True
                        ''''lblCamt.Text = " INR " & Convert.ToDouble(ds.Tables(0).Rows(0)("crd_limit").ToString())
                        ''td_AgencyID.InnerText = ds.Tables(0).Rows(0)("user_id").ToString()
                        td_AgencyID.InnerText = Session("UID")

                        lblagency.Text = Session("AgencyName")
                        'Session("AGTY") = ds.Tables(0).Rows(0)("Agent_Type").ToString()
                        'Session("agent_type") = ds.Tables(0).Rows(0)("Agent_Type").ToString()
                        'Session("MchntKeyITZ") = ds.Tables(0).Rows(0)("MerchantKey_ITZ").ToString().Trim()
                        ' ''''Session("ModeTypeITZ") = ds.Tables(0).Rows(0)("ModeType_ITZ").ToString().Trim()
                        'Session("_DCODE") = ds.Tables(0).Rows(0)("Decode_ITZ").ToString().Trim()
                        'Session("_SvcTypeITZ") = ds.Tables(0).Rows(0)("SvcType_ITZ").ToString().Trim()
                        'If ds.Tables(0).Rows(0)("Distr").ToString() <> "SPRING" Then
                        '    'div_ccpay.Visible = False
                        'End If
                        '' End If
                        'Marquee Message

                        'Try
                        '    dsm = det.GetMarquueemsg(servtype)
                        '    If dsm.Tables(0).Rows.Count > 0 Then
                        '        Dim msg As String = ""
                        '        For Each row As DataRow In dsm.Tables(0).Rows

                        '            msg += row("Message").ToString() & " ."
                        '        Next row

                        '        tdmarquee.InnerText = msg



                        '        'tdmarquee.InnerText = dsm.Tables(0).Rows(0)("Message").ToString()


                        '    End If
                        'Catch ex As Exception

                        'End Try





                        'BEGIN CHANGES FOR DISTR
                    ElseIf usertype = "DI" Then
                        'divflt.Visible = False
                        'divhtl.Visible = False
                        'div_Rail.Visible = False
                        'div_Bus.Visible = False
                        'div_Utility.Visible = False
                        'div_BillPayment.Visible = False
                        'div_Series.Visible = False
                        ds = det.AgencyInfo(id)
                        If ds.Tables(0).Rows.Count > 0 Then
                            lblagency.Text = ds.Tables(0).Rows(0)("Agency_Name").ToString()
                            lblCamt.Visible = True
                            lblCamt.Text = " INR " & Convert.ToDouble(ds.Tables(0).Rows(0)("crd_limit").ToString())
                            td_AgencyID.InnerText = ds.Tables(0).Rows(0)("user_id").ToString()
                            lblagency.Text = Session("AgencyName")
                            lblCamt.Visible = True
                            ''Session("AGTY") = ds.Tables(0).Rows(0)("Agent_Type").ToString()
                        End If
                        'END CHANGES FOR DISTR
                    End If

                    If Session("User_Type") = "ACC" Or Session("User_Type") = "SALES" Then
                        ' div_menu.Visible = False
                        hypdeal.Visible = False
                    End If
                    If Session("User_Type") = "EXEC" Then
                        'div_Rail.Visible = False
                        'div_Utility.Visible = False
                        'div_Series.Visible = False
                    End If

                    If Session("User_Type") = "ADMIN" Then
                        hypdeal.Visible = False
                    End If
                    If typeid = "TA2" Then
                        'div_Rail.Visible = False
                        'divflt.Visible = False
                        'divhtl.Visible = False
                        'div_Series.Visible = False
                        'div_Utility.Visible = False
                        hypdeal.Visible = False
                    End If

                    'If Session("User_Type") = "A" Or Session("User_Type") = "EXEC" Then
                    '    div_menu.Visible = False
                    'End If



                ElseIf Session("UID") Is Nothing AndAlso Session("UserType") Is Nothing AndAlso Session("TypeID") Is Nothing Then

                    Response.Redirect("~/Login.aspx?reason=Session TimeOut")
                End If
                ShowMenu()
                'RowMenu.Visible = False

                If (Request.UserAgent.IndexOf("AppleWebKit") > 0) Then
                    Request.Browser.Adapters.Clear()
                End If
                'If (usertype = "TA") Then
                '    ds = det.AgencyInfo(Session("UID").ToString())
                '    If ds.Tables(0).Rows.Count > 0 Then
                '        Dim GSTApplied As String = Convert.ToString(ds.Tables(0).Rows(0)("Is_GST_Apply"))
                '        If String.IsNullOrEmpty(GSTApplied) Then
                '            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "redirect", "alert('Please update first GST No in profile section.');window.location='SprReports/Agent/Agent_Profile.aspx'; ", True)
                '        End If

                '    End If
                'End If


            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Protected Sub lnklogout_Click1(ByVal sender As Object, ByVal e As EventArgs) Handles lnklogout.Click
        Try
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~/Login.aspx")
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub ShowMenu()
        Try
            Dim dset As New DataSet
            adap = New SqlDataAdapter("GetURL", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@typeid", typeid)
            adap.Fill(dset)
            adap.Dispose()
            Dim xmld As New XmlDataSource
            xmld.ID = "XmlDataSource1"
            xmld.EnableCaching = False
            dset.DataSetName = "Menus"
            dset.Tables(0).TableName = "abc"
            Dim relation As New DataRelation("ParentChild", dset.Tables("abc").Columns("Page_ID"), dset.Tables("abc").Columns("PageParent_ID"), True)
            relation.Nested = True
            dset.Relations.Add(relation)
            xmld.Data = dset.GetXml()
            xmld.TransformFile = Server.MapPath("~/Transform.xslt")
            xmld.XPath = "MenuItems/MenuItem"
            Menu1.DataSource = xmld
            Menu1.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub

    'Protected Sub lnkDash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDash.Click
    '    menu.Visible = True
    'End Sub

    'Protected Sub lnkDash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDash.Click
    '    RowMenu.Visible = True
    'End Sub

    'Protected Sub lnkflight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkflight.Click
    '    RowMenu.Visible = False
    '    Response.Redirect("~/IBEHome.aspx")
    'End Sub

End Class

