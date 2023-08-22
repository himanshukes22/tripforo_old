Imports System.Data
Imports System.IO
Imports System.Net
Imports Ionic.Zip
Imports System.Data.SqlClient

Partial Public Class Reports_Agent_Agent_Profile
    Inherits System.Web.UI.Page
    Dim STDom As New SqlTransactionDom
    Dim pCounty As String = ""
    Dim pState As String = ""
    Dim pStateCode As String = ""
    Dim pCity As String = ""


    Protected Sub LinkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkEdit.Click
        Try
            td_login.Visible = False
            td_login1.Visible = True

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnk_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Cancel.Click
        Try
            td_login1.Visible = False
            td_login.Visible = True
            lbl_msg.Text = ""

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            divTds.Visible = False
            'trLogin.Visible = False
            'trLoginDetails.Visible = False
            trAlternateEmailID.Visible = False
            tr_PanCard.Visible = False
            tr_Landline.Visible = False
            tr_Fax.Visible = False
            LinkPersonalEdit.Visible = False


            If IsPostBack = False Then
                Dim country As String = ddlCountry.SelectedValue
                BindState(country)
                BindStateGst("India")
            End If

            If ddlCountry.SelectedValue = "Other" Then
                ddlState.Visible = False
                ddlCity.Visible = False
                txt_state.Visible = True
                txt_city.Visible = True
            Else
                ddlState.Visible = True
                ddlCity.Visible = True
                txt_state.Visible = False
                txt_city.Visible = False
            End If
            Dim STDom As New SqlTransactionDom

            Dim dt As DataTable
            dt = STDom.GetAgencyDetails(Session("UID").ToString).Tables(0)
            Info()
            If Not IsPostBack Then
                'txt_aemail.Text = dt.Rows(0)("Alt_Email").ToString()
                'txt_fax.Text = dt.Rows(0)("Fax_no").ToString()
                'txt_landline.Text = dt.Rows(0)("Phone").ToString()
                txtAgencyName.Text = dt.Rows(0)("Agency_Name").ToString()
                txt_address.Text = dt.Rows(0)("Address").ToString()
                txt_city.Text = dt.Rows(0)("City").ToString()
                txt_state.Text = dt.Rows(0)("State").ToString()
                ' txt_country.Text = dt.Rows(0)("Country").ToString()
                ddlCountry.SelectedValue = dt.Rows(0)("Country").ToString()
                ddlState.SelectedValue = dt.Rows(0)("StateCode").ToString()
                'ddlState.SelectedItem.Text=dt.Rows(0)("State").ToString();                
                txtPincode.Text = dt.Rows(0)("zipcode").ToString()
                txtDistrict.Text = dt.Rows(0)("District").ToString()
                BindCity(dt.Rows(0)("StateCode").ToString())
                ddlCity.SelectedValue = dt.Rows(0)("City").ToString()
                oldpasshndfld.Value = dt.Rows(0)("PWD").ToString()
                'GST

                'RbtApplied.SelectedValue = Convert.ToString(dt.Rows(0)("Is_GST_Apply"))
                TxtGSTNo.Text = Convert.ToString(dt.Rows(0)("GSTNO"))
                TxtGSTCompanyName.Text = Convert.ToString(dt.Rows(0)("GST_Company_Name"))
                TxtGSTAddress.Text = Convert.ToString(dt.Rows(0)("GST_Company_Address"))
                TxtGSTPhoneNo.Text = Convert.ToString(dt.Rows(0)("GST_PhoneNo"))
                TxtGSTEmail.Text = Convert.ToString(dt.Rows(0)("GST_Email"))
                TxtGSTRemark.Text = Convert.ToString(dt.Rows(0)("GSTRemark"))
                txtPincodeGst.Text = Convert.ToString(dt.Rows(0)("GST_Pincode"))
                If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0)("GST_State"))) Then
                    ddlStateGst.SelectedValue = Convert.ToString(dt.Rows(0)("GST_State_Code"))
                    BindCityGst(ddlStateGst.SelectedValue)
                    If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0)("GST_City"))) Then
                        ddlCityGst.SelectedItem.Text = Convert.ToString(dt.Rows(0)("GST_City"))
                    End If
                End If

                If Not String.IsNullOrEmpty(Convert.ToString(dt.Rows(0)("Is_GST_Apply"))) Then
                    If Convert.ToString(dt.Rows(0)("Is_GST_Apply")).ToLower() = "true" Then
                        RbtApplied.SelectedValue = "True"
                        HGST9.Style.Add("display", "none")
                        GST8.Style.Add("display", "none") ''Remark

                        HGST1.Style.Remove("display")
                        HGST2.Style.Remove("display")
                        HGST3.Style.Remove("display")
                        HGST4.Style.Remove("display")
                        HGST5.Style.Remove("display")
                        HGST6.Style.Remove("display")
                        HGST7.Style.Remove("display")
                        HGST8.Style.Remove("display")

                        GST.Style.Remove("display")
                        GST1.Style.Remove("display")
                        GST2.Style.Remove("display")
                        GST3.Style.Remove("display")
                        GST4.Style.Remove("display")
                        GST5.Style.Remove("display")
                        GST6.Style.Remove("display")
                        GST7.Style.Remove("display")

                    Else
                        RbtApplied.SelectedValue = "False"
                        HGST1.Style.Add("display", "none")
                        HGST2.Style.Add("display", "none")
                        HGST3.Style.Add("display", "none")
                        HGST4.Style.Add("display", "none")
                        HGST5.Style.Add("display", "none")
                        HGST6.Style.Add("display", "none")
                        HGST7.Style.Add("display", "none")
                        HGST8.Style.Add("display", "none")
                        'HGST9.Style.Add("display", "none") ''Remark

                        GST.Style.Add("display", "none")
                        GST1.Style.Add("display", "none")
                        GST2.Style.Add("display", "none")
                        GST3.Style.Add("display", "none")
                        GST4.Style.Add("display", "none")
                        GST5.Style.Add("display", "none")
                        GST6.Style.Add("display", "none")
                        GST7.Style.Add("display", "none")
                        'GST8.Style.Add("display", "none") ''Remark
                        HGST9.Style.Remove("display")
                        GST8.Style.Remove("display")
                    End If
                Else
                    RbtApplied.SelectedValue = "True"
                    HGST9.Style.Add("display", "none")
                    GST8.Style.Add("display", "none") ''Remark

                    HGST1.Style.Remove("display")
                    HGST2.Style.Remove("display")
                    HGST3.Style.Remove("display")
                    HGST4.Style.Remove("display")
                    HGST5.Style.Remove("display")
                    HGST6.Style.Remove("display")
                    HGST7.Style.Remove("display")
                    HGST8.Style.Remove("display")

                    GST.Style.Remove("display")
                    GST1.Style.Remove("display")
                    GST2.Style.Remove("display")
                    GST3.Style.Remove("display")
                    GST4.Style.Remove("display")
                    GST5.Style.Remove("display")
                    GST6.Style.Remove("display")
                    GST7.Style.Remove("display")
                End If

            End If

            logo()


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub logo()
        Try
            Dim filepath As String = Server.MapPath("~\AgentLogo") + "\" + Session("UID") + ".jpg" 'Server.MapPath("~/AgentLogo/" + LogoName)
            If (System.IO.File.Exists(filepath)) Then
                Image111.ImageUrl = "~/AgentLogo/" & Session("UID") & ".jpg"
            Else
                Image111.ImageUrl = "~/images/nologo.png"
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub Info()
        Try


            Dim dtinfo As DataTable
            ' If Not IsPostBack Then
            dtinfo = STDom.GetAgencyDetails(Session("UID").ToString).Tables(0)
            td_username.InnerText = dtinfo.Rows(0)("user_id").ToString()
            td_Name.InnerText = dtinfo.Rows(0)("Title").ToString() & " " & dtinfo.Rows(0)("FName").ToString() & " " & dtinfo.Rows(0)("LName").ToString()
            td_EmailID.InnerText = dtinfo.Rows(0)("Email").ToString()
            td_Mobile.InnerText = dtinfo.Rows(0)("Mobile").ToString()
            td_Landline.InnerText = dtinfo.Rows(0)("Phone").ToString()
            td_Pan.InnerText = dtinfo.Rows(0)("PanNo").ToString()
            td_AlternateEmailID.InnerText = dtinfo.Rows(0)("Alt_Email").ToString()
            'td_Fax.InnerText = dtinfo.Rows(0)("Fax_no").ToString()
            tdAgencyName.InnerText = dtinfo.Rows(0)("Agency_Name").ToString()
            td_Add.InnerText = dtinfo.Rows(0)("Address").ToString()
            td_City.InnerText = dtinfo.Rows(0)("City").ToString()
            td_Country.InnerText = dtinfo.Rows(0)("Country").ToString()
            td_State.InnerText = dtinfo.Rows(0)("State").ToString()
            tdPinCode.InnerText = dtinfo.Rows(0)("zipcode").ToString()
            tdDistrict.InnerText = dtinfo.Rows(0)("District").ToString()

            td_Name1.InnerText = dtinfo.Rows(0)("Title").ToString() & " " & dtinfo.Rows(0)("FName").ToString() & " " & dtinfo.Rows(0)("LName").ToString()
            td_Email1.InnerText = dtinfo.Rows(0)("Email").ToString()
            td_Mobile1.InnerText = dtinfo.Rows(0)("Mobile").ToString()
            ''GST            

            tdGSTNO.InnerText = Convert.ToString(dtinfo.Rows(0)("GSTNO"))
            tdGST_Company_Name.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_Company_Name"))
            tdGST_Company_Address.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_Company_Address"))
            tdGST_PhoneNo.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_PhoneNo"))
            tdGST_Email.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_Email"))
            tdGSTRemark.InnerText = Convert.ToString(dtinfo.Rows(0)("GSTRemark"))

            tdCityGst.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_City"))
            tdStateGst.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_State"))
            tdPincodeGst.InnerText = Convert.ToString(dtinfo.Rows(0)("GST_Pincode"))


            If Not String.IsNullOrEmpty(Convert.ToString(dtinfo.Rows(0)("Is_GST_Apply"))) Then
                If Convert.ToString(dtinfo.Rows(0)("Is_GST_Apply")).ToLower() = "false" Then
                    tdGstApplied.InnerText = "Not Applied"
                    HGST1.Style.Add("display", "none")
                    HGST2.Style.Add("display", "none")
                    HGST3.Style.Add("display", "none")
                    HGST4.Style.Add("display", "none")
                    HGST5.Style.Add("display", "none")
                    HGST6.Style.Add("display", "none")
                    HGST7.Style.Add("display", "none")
                    HGST8.Style.Add("display", "none")
                    HGST9.Style.Remove("display")
                    GST8.Style.Remove("display")
                    ''HGST9.Style.Add("display", "block") ''Remark

                    GST.Style.Add("display", "none")
                    GST1.Style.Add("display", "none")
                    GST2.Style.Add("display", "none")
                    GST3.Style.Add("display", "none")
                    GST4.Style.Add("display", "none")
                    GST5.Style.Add("display", "none")
                    GST6.Style.Add("display", "none")
                    GST7.Style.Add("display", "none")
                    '' GST8.Style.Add("display", "block") ''Remark
                Else
                    tdGstApplied.InnerText = "Applied"
                    HGST9.Style.Add("display", "none")
                    GST8.Style.Add("display", "none") ''Remark



                    HGST1.Style.Remove("display")
                    HGST2.Style.Remove("display")
                    HGST3.Style.Remove("display")
                    HGST4.Style.Remove("display")
                    HGST5.Style.Remove("display")
                    HGST6.Style.Remove("display")
                    HGST7.Style.Remove("display")
                    HGST8.Style.Remove("display")

                    GST.Style.Remove("display")
                    GST1.Style.Remove("display")
                    GST2.Style.Remove("display")
                    GST3.Style.Remove("display")
                    GST4.Style.Remove("display")
                    GST5.Style.Remove("display")
                    GST6.Style.Remove("display")
                    GST7.Style.Remove("display")

                End If
            Else
                tdGstApplied.InnerText = "Not Applied"
            End If



            'GSTNO, GST_Company_Name, GST_Company_Address, GST_PhoneNo, GST_Email, Is_GST_Apply, GSTRemark
            'td_Pan1.InnerText = dtinfo.Rows(0)("PanNo").ToString()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub LinkEditAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkEditAdd.Click
        Try
            td_Address.Visible = False
            td_Address1.Visible = True
            'BindCity(ddlState.SelectedValue)


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnk_CancelAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_CancelAdd.Click
        Try
            td_Address.Visible = True
            td_Address1.Visible = False

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub LinkPersonalEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkPersonalEdit.Click
        Try
            td_PDetails.Visible = False
            td_PDetails1.Visible = True


            Dim dt As DataTable
            dt = STDom.GetAgencyDetails(Session("UID").ToString).Tables(0)
            BindCity(dt.Rows(0)("StateCode").ToString())
            'txt_aemail.Text = dt.Rows(0)("Alt_Email").ToString()
            'txt_fax.Text = dt.Rows(0)("Fax_no").ToString()
            'txt_landline.Text = dt.Rows(0)("Phone").ToString()
            txtAgencyName.Text = dt.Rows(0)("Agency_Name").ToString()
            txt_address.Text = dt.Rows(0)("Address").ToString()
            txt_city.Text = dt.Rows(0)("City").ToString()
            txt_state.Text = dt.Rows(0)("State").ToString()
            ' txt_country.Text = dt.Rows(0)("Country").ToString()
            ddlCountry.SelectedValue = dt.Rows(0)("Country").ToString()
            ddlState.SelectedValue = dt.Rows(0)("StateCode").ToString()
            'ddlState.SelectedItem.Text=dt.Rows(0)("State").ToString();
            ddlCity.SelectedValue = dt.Rows(0)("City").ToString()
            txtPincode.Text = dt.Rows(0)("zipcode").ToString()
            txtDistrict.Text = dt.Rows(0)("District").ToString()




        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub lnk_CancelPDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_CancelPDetails.Click
        Try
            td_PDetails.Visible = True
            td_PDetails1.Visible = False

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub button_upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles button_upload.Click
        Try
            Dim len As Integer = 0
            len = FileUpload1.PostedFile.ContentLength
            If len > 0 Then

                Dim finfo As New FileInfo(FileUpload1.FileName)
                Dim fileExtension As String = finfo.Extension.ToLower()
                If fileExtension <> ".jpg" Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
                Else

                    Dim file As String = ""
                    If FileUpload1.HasFile = True Then
                        Dim filepath As String = Server.MapPath("~/AgentLogo/" + Session("UID"))
                        filepath = filepath + ".jpg"
                        FileUpload1.SaveAs(filepath.ToString())
                        file = Path.GetFileName(Session("UID") + ".jpg")
                        logo()
                    End If

                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Select Image');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub


    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Try
            If ddlCountry.SelectedValue = "Other" Then
                pCounty = ddlCountry.SelectedValue
                pStateCode = txt_state.Text
                pState = txt_state.Text
                pCity = txt_city.Text
            Else
                pCounty = ddlCountry.SelectedValue
                pStateCode = ddlState.SelectedValue
                pState = ddlState.SelectedItem.Text
                pCity = ddlCity.SelectedValue

            End If

            If (txt_oldpassword.Text.Trim = oldpasshndfld.Value.Trim) Then

                If (txt_password.Text.Trim = txt_cpassword.Text.Trim) Then
                    If txt_password.Text <> "" AndAlso txt_password.Text IsNot Nothing Then
                        UpdateAgentProfileDetails("Login", Session("UID"), txt_password.Text.Trim, "", "", "", txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue, "", "", "", "", "", "", "", "")
                        'STDom.UpdateAgentProfile("Login", Session("UID"), txt_password.Text.Trim, "", "", "", txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue)
                        'STDom.UpdateAgentProfile("Login", Session("UID"), txt_password.Text.Trim, "", "", "", txt_address.Text.Trim, txt_city.Text.Trim, txt_state.Text.Trim, txt_country.Text.Trim)
                        'STDom.UpdateAgentProfile("Login", Session("UID"), txt_password.Text.Trim, txt_aemail.Text.Trim, txt_landline.Text.Trim, txt_fax.Text.Trim, txt_address.Text.Trim, txt_city.Text.Trim, txt_state.Text.Trim, txt_country.Text.Trim)
                        td_login.Visible = True
                        td_login1.Visible = False
                        lbl_msg.Text = ""
                    Else
                        lbl_msg.Text = "Enter Password"
                    End If

                Else
                    lbl_msg.Text = "Enter Same Password"
                End If
            Else
                lbl_msg.Text = "Enter old Password Correctly"

            End If



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_SavePDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SavePDetails.Click
        Try
            If ddlCountry.SelectedValue = "Other" Then
                pCounty = ddlCountry.SelectedValue
                pStateCode = txt_state.Text
                pState = txt_state.Text
                pCity = txt_city.Text
            Else
                pCounty = ddlCountry.SelectedValue
                pStateCode = ddlState.SelectedValue
                pState = ddlState.SelectedItem.Text
                pCity = ddlCity.SelectedValue

            End If

            STDom.UpdateAgentProfile("Details", Session("UID"), txt_password.Text.Trim, "", "", "", txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue)
            'STDom.UpdateAgentProfile("Details", Session("UID"), txt_password.Text.Trim, txt_aemail.Text.Trim, txt_landline.Text.Trim, txt_fax.Text.Trim, txt_address.Text.Trim, txt_city.Text.Trim, txt_state.Text.Trim, txt_country.Text.Trim)
            td_PDetails.Visible = True
            td_PDetails1.Visible = False
            Info()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub btn_Saveadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Saveadd.Click
        Try
            If ddlCountry.SelectedValue = "Other" Then
                pCounty = ddlCountry.SelectedValue
                pStateCode = txt_state.Text
                pState = txt_state.Text
                pCity = txt_city.Text
            Else
                pCounty = ddlCountry.SelectedValue
                pStateCode = ddlState.SelectedValue
                pState = ddlState.SelectedItem.Text
                pCity = ddlCity.SelectedValue

            End If
            UpdateAgentProfileDetails("Address", Session("UID"), txt_password.Text.Trim, txtAgencyName.Text, txtDistrict.Text, txtPincode.Text.Trim, txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue, "", "", "", "", "", "true", "", txtPincode.Text.Trim)
            'UpdateAgentProfileDetails("Address", Session("UID"), txt_password.Text.Trim, txtAgencyName.Text, txtDistrict.Text, txtPincode.Text.Trim, txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue)
            'UpdateAgentProfileDetails("Address", Session("UID"), txt_password.Text.Trim, txtAgencyName.Text, txtDistrict.Text, txtPincode.Text.Trim, txt_address.Text.Trim, pCity, pState, ddlCountry.SelectedValue)
            ' STDom.UpdateAgentProfile("Address", Session("UID"), txt_password.Text.Trim, txt_aemail.Text.Trim, txt_landline.Text.Trim, txt_fax.Text.Trim, txt_address.Text.Trim, txt_city.Text.Trim, txt_state.Text.Trim, txt_country.Text.Trim)
            td_Address.Visible = True
            td_Address1.Visible = False
            Info()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub lnk_tds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_tds.Click
        Try


            Dim Path As String
            Path = Server.MapPath("~/TDSPDF")
            'Dim contains As String() = Directory.GetFiles(Path, "*" & td_Pan1.InnerText.ToUpper & "*", SearchOption.TopDirectoryOnly) '.IndexOf(Path, ".pdf")
            Dim contains As String()
            Dim zip As New ZipFile()
            If (contains.Length > 0) Then
                'Dim ik As Integer = 0
                zip.AlternateEncodingUsage = ZipOption.AsNecessary
                zip.AddDirectoryByName("TDSPDF")
                'For ik = 0 To contains.Length
                For Each d As String In contains
                    'Dim i As Integer = contains(d).IndexOf("TDSPDF")
                    'Dim i1 As Integer = contains(d).IndexOf(".pdf")

                    Dim i As Integer = d.IndexOf("TDSPDF")
                    Dim i1 As Integer = d.IndexOf(".pdf")

                    Dim Filename As String = d.Substring(i + 7, (i1 + 4) - (i + 7))
                    'Dim filePath As String = Server.MapPath("~/SampleFiles/" & Filename)
                    'zip.AddFile(filePath, "files")


                    ' zip.AddFile(Filename, "TDSPDF")
                    zip.AddFile(Server.MapPath("~/TDSPDF/" & Filename), "TDSPDF")
                Next

                ' ik = ik + 1

                'Next
                Try
                    Response.Clear()
                    Response.BufferOutput = False
                    Dim zipName As String = [String].Format("TDSCertificate_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"))
                    Response.ContentType = "application/zip"
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName)
                    zip.Save(Response.OutputStream)
                    Response.[End]()
                    'Response.Clear()
                    'Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedFile.zip")
                    'Response.ContentType = "application/zip"
                    'zip.Save(Response.OutputStream)
                    'Response.[End]()


                    'Response.Clear()
                    'Response.ContentType = "application/pdf"
                    'Response.AppendHeader("Content-Disposition", "attachment; filename=TDSCertificate.pdf")
                    'Response.TransmitFile("~/TDSPDF/" & Filename & "")
                    '' Response.End()
                Catch ex As Exception

                End Try
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('TDS Certificate not found. Please contact to account department');", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub TheDownload(ByVal path As String)


        Dim toDownload As System.IO.FileInfo = New System.IO.FileInfo(HttpContext.Current.Server.MapPath(path))

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name)
        HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/octet-stream"
        HttpContext.Current.Response.WriteFile(path)
        HttpContext.Current.Response.End()

    End Sub

    'Devesh

    Private Sub BindState(countrycode As String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select STATEID as Code,STATE as Name from  [dbo].[Tbl_STATE] where COUNTRY='") & countrycode) + "'order by STATE", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlState.DataSource = dt
            ddlState.DataValueField = "Code"
            ddlState.DataTextField = "Name"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("--Select State--", "0"))

            BindCity(ddlState.SelectedValue)

        Catch ex As Exception
        End Try
        If countrycode = "Other" Then
            ddlState.Visible = False
            ddlCity.Visible = False
            txt_state.Visible = True
            txt_city.Visible = True
        Else
            ddlState.Visible = True
            ddlCity.Visible = True
            txt_state.Visible = False
            txt_city.Visible = False
        End If
    End Sub
    Private Sub BindCity(stateCode As String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select CITY, STATEID from  [dbo].[TBL_CITY]  where STATEID='") & stateCode) + "'order by CITY", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlCity.DataSource = dt
            ddlCity.DataValueField = "CITY"
            ddlCity.DataTextField = "CITY"
            ddlCity.DataBind()
            ddlCity.Items.Insert(0, New ListItem("--Select City --", "0"))
        Catch ex As Exception
        End Try
        If ddlCountry.SelectedValue = "Other" Then
            ddlState.Visible = False
            ddlCity.Visible = False
            txt_state.Visible = True
            txt_city.Visible = True
        Else
            ddlState.Visible = True
            ddlCity.Visible = True
            txt_state.Visible = False
            txt_city.Visible = False
        End If
    End Sub
    Protected Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim country As String = ddlCountry.SelectedValue
            BindState(country)
            GetAgecyDetails()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
        End Try

    End Sub
    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim code As String = ddlState.SelectedValue
            Dim name As String = ddlState.SelectedItem.Text
            BindCity(code)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
        End Try

    End Sub

    Public Function UpdateAgentProfileDetails(ByVal Type As String, ByVal UID As String, ByVal pwd As String, ByVal AEmail As String, ByVal Landline As String, ByVal Fax As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Country As String, ByVal GSTNO As String, ByVal GSTCompanyName As String, ByVal GSTCompanyAddress As String, ByVal GSTPhoneNo As String,
                                    ByVal GSTEmail As String, ByVal IsGSTApply As String, ByVal GSTRemark As String, ByVal GstPincode As String) As Integer
        If ddlCountry.SelectedValue = "Other" Then
            pStateCode = txt_state.Text
            pState = txt_state.Text
        Else
            pStateCode = ddlState.SelectedValue
            pState = ddlState.SelectedItem.Text
        End If

        If String.IsNullOrEmpty(IsGSTApply) Then
            IsGSTApply = "true"
        End If

        If Type = "GST" Then
            pStateCode = ddlStateGst.SelectedValue
            pState = ddlStateGst.SelectedItem.Text
        End If

        Dim paramHashtable As New Hashtable
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        paramHashtable.Clear()
        paramHashtable.Add("@Type", Type)
        paramHashtable.Add("@UID", UID)
        paramHashtable.Add("@pwd", pwd)
        paramHashtable.Add("@AEmail", AEmail)
        paramHashtable.Add("@Landline", Landline)
        paramHashtable.Add("@Fax", Fax)
        paramHashtable.Add("@Address", Address)
        paramHashtable.Add("@City", City)
        paramHashtable.Add("@State", State)
        paramHashtable.Add("@Country", Country)
        paramHashtable.Add("@StateCode", pStateCode)

        'GST
        paramHashtable.Add("@GSTNO", GSTNO)
        paramHashtable.Add("@GSTCompanyName", GSTCompanyName)
        paramHashtable.Add("@GSTCompanyAddress", GSTCompanyAddress)
        paramHashtable.Add("@GSTPhoneNo", GSTPhoneNo)
        paramHashtable.Add("@GSTEmail", GSTEmail)
        paramHashtable.Add("@IsGSTApply", Convert.ToBoolean(IsGSTApply))
        paramHashtable.Add("@GSTRemark", GSTRemark)
        paramHashtable.Add("@GST_Pincode", GstPincode)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "UpdateAgentProfile", 1)
    End Function
    Private Sub GetAgecyDetails()
        Try
            Dim dt As DataTable
            dt = STDom.GetAgencyDetails(Session("UID").ToString).Tables(0)
            'txt_aemail.Text = dt.Rows(0)("Alt_Email").ToString()
            'txt_fax.Text = dt.Rows(0)("Fax_no").ToString()
            'txt_landline.Text = dt.Rows(0)("Phone").ToString()
            txtAgencyName.Text = dt.Rows(0)("Agency_Name").ToString()
            txt_address.Text = dt.Rows(0)("Address").ToString()
            txt_city.Text = dt.Rows(0)("City").ToString()
            txt_state.Text = dt.Rows(0)("State").ToString()
            ' txt_country.Text = dt.Rows(0)("Country").ToString()
            ''ddlCountry.SelectedValue = dt.Rows(0)("Country").ToString()
            ddlState.SelectedValue = dt.Rows(0)("StateCode").ToString()
            'ddlState.SelectedItem.Text=dt.Rows(0)("State").ToString();                
            txtPincode.Text = dt.Rows(0)("zipcode").ToString()
            txtDistrict.Text = dt.Rows(0)("District").ToString()
            BindCity(dt.Rows(0)("StateCode").ToString())
            ddlCity.SelectedValue = dt.Rows(0)("City").ToString()


        Catch ex As Exception
        End Try
        If ddlCountry.SelectedValue = "Other" Then
            ddlState.Visible = False
            ddlCity.Visible = False
            txt_state.Visible = True
            txt_city.Visible = True
        Else
            ddlState.Visible = True
            ddlCity.Visible = True
            txt_state.Visible = False
            txt_city.Visible = False
        End If
    End Sub

    Protected Sub LinkBtnGstUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnGstUpdate.Click
        Try
            tdGst.Visible = False
            tdGstUpdate.Visible = True
            'BindCity(ddlState.SelectedValue)


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub BtnGstCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGstCancel.Click
        Try
            tdGst.Visible = True
            tdGstUpdate.Visible = False

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub BtnGstSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGstSave.Click
        Try
            Dim GSTNO As String = TxtGSTNo.Text
            Dim GSTCompanyName As String = TxtGSTCompanyName.Text
            Dim GSTCompanyAddress As String = TxtGSTAddress.Text
            Dim GSTPhoneNo As String = TxtGSTPhoneNo.Text
            Dim GSTEmail As String = TxtGSTEmail.Text
            Dim GSTRemark As String = TxtGSTRemark.Text
            Dim IsGSTApply As String = RbtApplied.SelectedValue

            Dim CityGST As String = ddlCityGst.SelectedItem.Text
            Dim StateGST As String = ddlStateGst.SelectedItem.Text
            Dim PincodeGST As String = txtPincodeGst.Text
            Dim StateCodeGST As String = ddlStateGst.SelectedValue

            UpdateAgentProfileDetails("GST", Session("UID"), txt_password.Text.Trim, "", "", "", txt_address.Text.Trim, CityGST, StateGST, ddlCountry.SelectedValue, GSTNO, GSTCompanyName, GSTCompanyAddress, GSTPhoneNo, GSTEmail, IsGSTApply, GSTRemark, PincodeGST)
            'STDom.UpdateAgentProfile("Details", Session("UID"), txt_password.Text.Trim, txt_aemail.Text.Trim, txt_landline.Text.Trim, txt_fax.Text.Trim, txt_address.Text.Trim, txt_city.Text.Trim, txt_state.Text.Trim, txt_country.Text.Trim)
            tdGst.Visible = True
            tdGstUpdate.Visible = False
            Info()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Protected Sub ddlStateGst_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim code As String = ddlStateGst.SelectedValue
            Dim name As String = ddlStateGst.SelectedItem.Text
            BindCityGst(code)


            If Convert.ToString(RbtApplied.SelectedValue).ToLower() = "true" Then
                HGST9.Style.Add("display", "none")
                GST8.Style.Add("display", "none") ''Remark

                HGST1.Style.Remove("display")
                HGST2.Style.Remove("display")
                HGST3.Style.Remove("display")
                HGST4.Style.Remove("display")
                HGST5.Style.Remove("display")
                HGST6.Style.Remove("display")
                HGST7.Style.Remove("display")
                HGST8.Style.Remove("display")

                GST.Style.Remove("display")
                GST1.Style.Remove("display")
                GST2.Style.Remove("display")
                GST3.Style.Remove("display")
                GST4.Style.Remove("display")
                GST5.Style.Remove("display")
                GST6.Style.Remove("display")
                GST7.Style.Remove("display")

            Else
                HGST1.Style.Add("display", "none")
                HGST2.Style.Add("display", "none")
                HGST3.Style.Add("display", "none")
                HGST4.Style.Add("display", "none")
                HGST5.Style.Add("display", "none")
                HGST6.Style.Add("display", "none")
                HGST7.Style.Add("display", "none")
                HGST8.Style.Add("display", "none")
                'HGST9.Style.Add("display", "none") ''Remark

                GST.Style.Add("display", "none")
                GST1.Style.Add("display", "none")
                GST2.Style.Add("display", "none")
                GST3.Style.Add("display", "none")
                GST4.Style.Add("display", "none")
                GST5.Style.Add("display", "none")
                GST6.Style.Add("display", "none")
                GST7.Style.Add("display", "none")
                'GST8.Style.Add("display", "none") ''Remark
                HGST9.Style.Remove("display")
                GST8.Style.Remove("display")
            End If
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.[GetType](), "Message", "alert('" + Convert.ToString(ex.Message) + "');", True)
        End Try

    End Sub

    Private Sub BindStateGst(countrycode As String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select STATEID as Code,STATE as Name from  [dbo].[Tbl_STATE] where COUNTRY='") & countrycode) + "'order by STATE", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlStateGst.DataSource = dt
            ddlStateGst.DataValueField = "Code"
            ddlStateGst.DataTextField = "Name"
            ddlStateGst.DataBind()
            ddlStateGst.Items.Insert(0, New ListItem("--Select State--", "select"))
            BindCityGst(ddlStateGst.SelectedValue)

        Catch ex As Exception
        End Try

    End Sub
    Private Sub BindCityGst(stateCode As String)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Try
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter((Convert.ToString("select CITY, STATEID from  [dbo].[TBL_CITY]  where STATEID='") & stateCode) + "'order by CITY", con)
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(dt)
            ddlCityGst.DataSource = dt
            ddlCityGst.DataValueField = "CITY"
            ddlCityGst.DataTextField = "CITY"
            ddlCityGst.DataBind()
            ddlCityGst.Items.Insert(0, New ListItem("--Select City --", "select"))
        Catch ex As Exception
        End Try
    End Sub

    'End Devesh
    'Protected Sub btn_uploadpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_uploadpan.Click
    '    Try
    '        Dim len As Integer = 0
    '        len = FileUpload_Pan.PostedFile.ContentLength
    '        If len > 0 Then

    '            Dim finfo As New FileInfo(FileUpload_Pan.FileName)
    '            Dim fileExtension As String = finfo.Extension.ToLower()
    '            If fileExtension <> ".jpg" Then
    '                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Upload JPG formate');", True)
    '            Else

    '                Dim file As String = ""
    '                If FileUpload_Pan.HasFile = True Then
    '                    Dim filepath As String = Server.MapPath("~/AgentPancard/" + "PAN_" + Session("UID"))
    '                    filepath = filepath + ".jpg"
    '                    FileUpload_Pan.SaveAs(filepath.ToString())
    '                    file = Path.GetFileName("PAN_" + Session("UID") + ".jpg")
    '                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Pancard image updated sucessfully');", True)
    '                End If

    '            End If
    '        Else
    '            ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Select Pancard Image');", True)
    '        End If
    '    Catch ex As Exception
    '        clsErrorLog.LogInfo(ex)

    '    End Try
    'End Sub
End Class
