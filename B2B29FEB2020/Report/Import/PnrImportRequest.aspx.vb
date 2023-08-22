Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports GetPnrDetails
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports PNRImport
Partial Class Reports_Import_PnrImportRequest
    Inherits System.Web.UI.Page
    Private PInfo As New pnrinfo()
    Private ST As New SqlTransaction()
    Private airline As String
    Private departure As String
    Private destination As String
    Private depatdate As String
    Private depattime As String
    Private arrvdate As String
    Private arrvtime As String
    Private flightno As String
    Private rdb As String
    Private Adtfrbas As String
    Private chdfrbas As String
    Private inffrbas As String
    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim trackId As String
    Dim trackIdDom As String
    Dim STDom As New SqlTransactionDom()
    Dim clsCorp As New ClsCorporate()
    Protected Sub Btn_find_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_find.Click
        'Dim pnd As New GetPnrDetails()
        Dim objDA As New SqlTransaction
        Dim AgencyDs As DataSet
        AgencyDs = objDA.GetAgencyDetails(Session("UID"))
        GridViewshow.Visible = True
        ' SetGridViewWidth()
        paxdiv.Style(HtmlTextWriterStyle.Display) = "block"
	lbl_Errormsg.Text =""
        If AgencyDs.Tables(0).Rows(0)("Agent_Status").ToString.ToUpper.Trim <> "NOT ACTIVE" And AgencyDs.Tables(0).Rows(0)("Online_tkt").ToString.ToUpper.Trim <> "NOT ACTIVE" Then
            If Imp_Source.SelectedValue = "1A" Then


                If rdb_import.SelectedValue = "Domestic" Then
                    If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
                        lbl_Errormsg.Text = "Pnr already exist in import queue."
                    Else
                        Dim ds As New DataSet
                        Dim AIMP As New IAPNRImport
                        'If rdb_import.SelectedValue = "Domestic" Then
                        '    DataSetIA = AIMP.PNRDetailsGAL(Txt_pnr.Text, "D")
                        'Else
                        '    DataSetGAL = GALIMP.PNRDetailsGAL(Txt_pnr.Text, "I")
                        'End If
                        ds = AIMP.PnrDetailsReply(Txt_pnr.Text.Trim())
                        '  Dim pnrno As String = pnd.PnrDetailsReply(Txt_pnr.Text, "DELVS384Q")
                        ' Dim ds As New DataSet()
                        Try
                            'ds.ReadXml(New StringReader(pnrno))
                            ViewState("pnrDs") = ds

                            ' Dim flag As Boolean = False
                            If ds.Tables.Count > 1 Then
                                'Try
                                '    For ii As Integer = 0 To ds.Tables.Count - 1
                                '        If ds.Tables(ii).TableName = "FareDetails" Then
                                '            flag = True
                                '            Exit For
                                '        Else
                                '            flag = False
                                '        End If
                                '    Next
                                'Catch ex As Exception
                                '    clsErrorLog.LogInfo(ex)
                                '    flag = False
                                'End Try
                                'If flag = True Then
                                GridViewshow.DataSource = ds.Tables("SegmentDetails")
                                GridViewshow.DataBind()
                                If ds.Tables("TktDetails").Rows.Count > 0 Then
                                    Dim paxStr As String = "Passenger Details : <br/>"
                                    'For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
                                    '    paxStr = paxStr & (i + 1).ToString & ". " & Split(ds.Tables("TktDetails").Rows(i)(0), "/")(0) & "<br/>"
                                    'Next
                                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                                        paxStr = paxStr & (i + 1).ToString & ". " & ds.Tables(1).Rows(i)("FNAME") & " " & ds.Tables(1).Rows(i)("LNAME") & " " & ds.Tables(1).Rows(i)("paxtype") & "<br/>"
                                    Next
                                    paxdiv.InnerHtml = paxStr
                                    btn_update.Visible = True
                                    'Else
                                    '    lbl_Errormsg.Text = "Unable to find passenger."
                                    'End If
                                Else
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                            Else
                                If ds.Tables.Count > 0 Then
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                            lbl_Errormsg.Text = "Invalid Pnr Number."
                        End Try
                    End If

                Else

                    If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
                        lbl_Errormsg.Text = "Pnr already exist in import queue."
                    Else
                        'Dim pnrno As String = pnd.PnrDetailsReply(Txt_pnr.Text, "DELVS384Q")
                        'Dim ds As New DataSet()
                        Dim ds As New DataSet
                        Dim AIMP As New IAPNRImport
                        ds = AIMP.PnrDetailsReply(Txt_pnr.Text.Trim())
                        Try
                            'ds.ReadXml(New StringReader(pnrno))
                            ViewState("pnrDs") = ds

                            'Dim flag As Boolean = False
                            If ds.Tables.Count > 1 Then
                                '    Try
                                '        For ii As Integer = 0 To ds.Tables.Count - 1
                                '            If ds.Tables(ii).TableName = "FareDetails" Then
                                '                flag = True
                                '                Exit For
                                '            Else
                                '                flag = False
                                '            End If
                                '        Next
                                '    Catch ex As Exception
                                '        clsErrorLog.LogInfo(ex)
                                '        flag = False
                                '    End Try
                                ' If flag = True Then
                                GridViewshow.DataSource = ds.Tables("SegmentDetails")
                                GridViewshow.DataBind()
                                If ds.Tables("TktDetails").Rows.Count > 0 Then
                                    Dim paxStr As String = "Passenger Details : <br/>"
                                    For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
                                        paxStr = paxStr & (i + 1).ToString & ". " & Split(ds.Tables("TktDetails").Rows(i)(0), "/")(0) & "<br/>"
                                    Next
                                    paxdiv.InnerHtml = paxStr
                                    btn_update.Visible = True
                                Else
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                                'Else
                                '    lbl_Errormsg.Text = "Please price the itinerary."
                                'End If
                            Else
                                If ds.Tables.Count > 0 Then
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                            lbl_Errormsg.Text = "Invalid Pnr Number."
                        End Try
                    End If


                End If
                'ABACUS IMPORT BEGIN
            ElseIf Imp_Source.SelectedValue = "1B" Then
                If rdb_import.SelectedValue = "Domestic" Or rdb_import.SelectedValue = "International" Then
                    If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
                        lbl_Errormsg.Text = "Pnr already exist in import queue."
                    Else
                        Dim ds As New DataSet
                        Dim Dtt As New DataTable
                        'Dim AIMP As New STD.BAL.A_PNRImport("9102", "tango50", "Default", "https://sws-crt.cert.sabre.com", "3HQD", Dtt)
                        'Dim AIMP As New STD.BAL.A_PNRImport("8102", "table25", "Default", "https://webservices.sabre.com/websvc", "KS38", Dtt)
                        'ds = AIMP.RetrvPnrImport(Txt_pnr.Text.Trim())
                        Try
                            ViewState("pnrDs") = ds
                            If ds.Tables.Count > 1 Then
                                GridViewshow.DataSource = ds.Tables("SegmentDetails")
                                GridViewshow.DataBind()
                                If ds.Tables("TktDetails").Rows.Count > 0 Then
                                    Dim paxStr As String = "Passenger Details : <br/>"
                                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                                        paxStr = paxStr & (i + 1).ToString & ". " & ds.Tables(1).Rows(i)("FNAME") & " " & ds.Tables(1).Rows(i)("LNAME") & "<br/>"
                                    Next
                                    paxdiv.InnerHtml = paxStr
                                    btn_update.Visible = True
                                Else
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                            Else
                                If ds.Tables.Count > 0 Then
                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)("CancelStatus")
                                End If
                            End If
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                            lbl_Errormsg.Text = "Invalid Pnr Number."
                        End Try
                    End If

                End If
                'ABACUS IMPORT END
            Else
                'GAL PNRIMPORT
                Try
                    If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
                        lbl_Errormsg.Text = "Pnr already exist in import queue."
                    Else

                        Dim DataSetGAL As New DataSet
                        Dim GALIMP As New GALIMPORTPNR
                        If rdb_import.SelectedValue = "Domestic" Then
                            DataSetGAL = GALIMP.PNRDetailsGAL(Txt_pnr.Text, "D", rdb_Triptype.SelectedValue)
                        Else
                            DataSetGAL = GALIMP.PNRDetailsGAL(Txt_pnr.Text, "I", rdb_Triptype.SelectedValue)
                        End If



                        If DataSetGAL.Tables.Count > 1 Then

                            Dim paxStr As String = "Passenger Details : <br/>"
                            GridViewshow.DataSource = DataSetGAL.Tables(0)
                            GridViewshow.DataBind()


                            GridView1.DataSource = DataSetGAL.Tables(2)
                            GridView1.DataBind()


                            GridView2.DataSource = DataSetGAL.Tables(3)
                            GridView2.DataBind()

                            GridView3.DataSource = DataSetGAL.Tables(4)
                            GridView3.DataBind()



                            ViewState("pnrDs") = DataSetGAL
                            For i As Integer = 0 To DataSetGAL.Tables(1).Rows.Count - 1
                                paxStr = paxStr & (i + 1).ToString & ". " & DataSetGAL.Tables(1).Rows(i)("FNAME") & " " & DataSetGAL.Tables(1).Rows(i)("LNAME") & "<br/>"
                            Next
                            paxdiv.InnerHtml = paxStr
                            btn_update.Visible = True


                        Else
                            lbl_Errormsg.Text = DataSetGAL.Tables(0).Rows(0)("CancelStatus")
                        End If


                    End If

                Catch ex As Exception

                End Try






            End If

            ''GAL IMPORT
            'Dim FltRq As New FltRequest
            'Dim FltComBal As New STD.BAL.FlightCommonBAL
            'FltRq()
            ''  Dim Req As String = FltComBal.PostXml(
        Else
            ShowAlertMessage("You are not authorized. Please contact to your sales person")

        End If
        




    End Sub

    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_update.Click
        Dim projectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
        Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)

        If Imp_Source.SelectedValue = "1A" Then
            'AAAAAAAAAAAAAAAAAA
            If rdb_import.SelectedValue = "Domestic" Then
                trackIdDom = objSelectedfltCls.getRndNum
                Dim ds As New DataSet
                Dim ds1 As New DataSet
                Try
                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "D")
                    If (ds1.Tables(0).Rows.Count > 0) Then
                        Response.Write("<script>alert('PNR Already Exist')</script>")
                    Else

                        ds = ViewState("pnrDs")
                        If ds.Tables.Count > 0 Then
                            Try

                                For i As Integer = 0 To ds.Tables("SegmentDetails").Rows.Count - 1
                                    airline = ds.Tables("SegmentDetails").Rows(i)("Airline").ToString()
                                    departure = ds.Tables("SegmentDetails").Rows(i)("loc1").ToString()
                                    destination = ds.Tables("SegmentDetails").Rows(i)("loc2").ToString()
                                    depatdate = ds.Tables("SegmentDetails").Rows(i)("Depdate").ToString()
                                    depattime = ds.Tables("SegmentDetails").Rows(i)("Deptime").ToString()
                                    arrvdate = ds.Tables("SegmentDetails").Rows(i)("ArrDate").ToString()
                                    arrvtime = ds.Tables("SegmentDetails").Rows(i)("Arrtime").ToString()
                                    flightno = ds.Tables("SegmentDetails").Rows(i)("FlightNumber").ToString()
                                    rdb = ds.Tables("SegmentDetails").Rows(i)("RBD").ToString()

                                    'PInfo.InsertImportPNR(Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                    ' arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString)
                                    ST.InsertImportPNRIntl(trackIdDom.Trim(), Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "D", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)

                                Next
                                For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
                                    'Dim str As String = ds.Tables("TktDetails").Rows(i)("PaxTktDetails").ToString()
                                    'Dim pax As String() = str.Split("/"c)
                                    'Dim pax1 As String() = pax(0).Split(" "c)
                                    'Dim pax2 As String = ""
                                    'For ii As Integer = 0 To pax1.Length - 1
                                    '    If pax1(ii).ToString <> "" And pax1(ii) IsNot Nothing Then
                                    '        If ii = pax1.Length - 1 Then
                                    '            pax2 = pax2 & pax1(ii).ToString
                                    '        Else
                                    '            pax2 = pax2 & pax1(ii).ToString & " "
                                    '        End If
                                    '    End If
                                    'Next
                                    'pax1 = pax2.Split(" "c)
                                    Dim Passengertittle As String = ""
                                    Dim Passengerfname As String = ds.Tables("TktDetails").Rows(i)("FName").ToString()
                                    Dim Passengermname As String = ""
                                    Dim Passengerlname As String = ds.Tables("TktDetails").Rows(i)("LName").ToString()
                                    'If pax1.Length = 5 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengermname = pax1(2).ToString() & " " & pax1(3).ToString()
                                    '    Passengerlname = pax1(4).ToString()
                                    'ElseIf pax1.Length = 4 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengermname = pax1(2).ToString()
                                    '    Passengerlname = pax1(3).ToString()
                                    'ElseIf pax1.Length = 3 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengerlname = pax1(2).ToString()
                                    'ElseIf pax1.Length = 2 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    'ElseIf pax1.Length = 1 Then
                                    '    Passengerfname = pax1(0).ToString()
                                    'Else
                                    '    For ii As Integer = 0 To pax1.Length - 1
                                    '        Passengerfname = Passengerfname & pax1(ii) & " "
                                    '    Next
                                    'End If
                                    ST.InsertPaxInfoIntl(trackIdDom.Trim(), Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
                                Next
                                lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
                                btn_update.Visible = False
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                                Dim [Error] As String
                                [Error] = ds.Tables("errormessage").Rows(0)("errorMessage_Text").ToString()
                                lbl_Errormsg.Text = [Error]
                            End Try

                            GridViewshow.Visible = False
                            paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
                        End If
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                    lbl_Errormsg.Text = ex.Message
                End Try
            Else
                'For Intenational
                trackId = objSelectedfltCls.getRndNum
                Dim ds As New DataSet
                Dim ds1 As New DataSet
                Try
                    ds1 = ST.PnrImportIntlDetails(trackId, "I")
                    If (ds1.Tables(0).Rows.Count > 0) Then
                        Response.Write("<script>alert('PNR Already Exist')</script>")
                    Else

                        ds = ViewState("pnrDs")


                      

                        If ds.Tables.Count > 0 Then
                            Try
                                For i As Integer = 0 To ds.Tables("SegmentDetails").Rows.Count - 1
                                    airline = ds.Tables("SegmentDetails").Rows(i)("Airline").ToString()
                                    departure = ds.Tables("SegmentDetails").Rows(i)("loc1").ToString()
                                    destination = ds.Tables("SegmentDetails").Rows(i)("loc2").ToString()
                                    depatdate = ds.Tables("SegmentDetails").Rows(i)("Depdate").ToString()
                                    depattime = ds.Tables("SegmentDetails").Rows(i)("Deptime").ToString()
                                    arrvdate = ds.Tables("SegmentDetails").Rows(i)("ArrDate").ToString()
                                    arrvtime = ds.Tables("SegmentDetails").Rows(i)("Arrtime").ToString()
                                    flightno = ds.Tables("SegmentDetails").Rows(i)("FlightNumber").ToString()
                                    rdb = ds.Tables("SegmentDetails").Rows(i)("RBD").ToString()
                                    ST.InsertImportPNRIntl(trackId, Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "I", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)
                                Next

                                For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
                                    'Dim str As String = ds.Tables("TktDetails").Rows(i)("PaxTktDetails").ToString()
                                    'Dim pax As String() = str.Split("/"c)
                                    'Dim pax1 As String() = pax(0).Split(" "c)
                                    'Dim pax2 As String = ""
                                    'For ii As Integer = 0 To pax1.Length - 1
                                    '    If pax1(ii).ToString <> "" And pax1(ii) IsNot Nothing Then
                                    '        If ii = pax1.Length - 1 Then
                                    '            pax2 = pax2 & pax1(ii).ToString
                                    '        Else
                                    '            pax2 = pax2 & pax1(ii).ToString & " "
                                    '        End If
                                    '    End If
                                    'Next
                                    'pax1 = pax2.Split(" "c)
                                    Dim Passengertittle As String = ""
                                    Dim Passengerfname As String = ds.Tables("TktDetails").Rows(i)("FName").ToString()
                                    Dim Passengermname As String = ""
                                    Dim Passengerlname As String = ds.Tables("TktDetails").Rows(i)("LName").ToString()
                                    'If pax1.Length = 5 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengermname = pax1(2).ToString() & " " & pax1(3).ToString()
                                    '    Passengerlname = pax1(4).ToString()
                                    'ElseIf pax1.Length = 4 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengermname = pax1(2).ToString()
                                    '    Passengerlname = pax1(3).ToString()
                                    'ElseIf pax1.Length = 3 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    '    Passengerlname = pax1(2).ToString()
                                    'ElseIf pax1.Length = 2 Then
                                    '    Passengertittle = pax1(0).ToString()
                                    '    Passengerfname = pax1(1).ToString()
                                    'ElseIf pax1.Length = 1 Then
                                    '    Passengerfname = pax1(0).ToString()
                                    'Else
                                    '    For ii As Integer = 0 To pax1.Length - 1
                                    '        Passengerfname = Passengerfname & pax1(ii) & " "
                                    '    Next
                                    'End If
                                    ST.InsertPaxInfoIntl(trackId, Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
                                Next
                                lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
                                btn_update.Visible = False
                            Catch ex As Exception
                                clsErrorLog.LogInfo(ex)
                                Dim [Error] As String
                                [Error] = ds.Tables("errormessage").Rows(0)("errorMessage_Text").ToString()
                                lbl_Errormsg.Text = [Error]
                            End Try

                            GridViewshow.Visible = False
                            paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
                        End If
                    End If
                Catch ex As Exception
                    clsErrorLog.LogInfo(ex)
                    lbl_Errormsg.Text = ex.Message
                End Try




            End If
            'ENDAAAAAAAAAAAAAAA
        Else
            'GALLLLLLLLLLLLLLLLLLL
            'If rdb_import.SelectedValue = "Domestic" Then
            trackIdDom = objSelectedfltCls.getRndNum
            Dim ds As New DataSet
            Dim ds1 As New DataSet
            Dim trackid_ As String = ""
            Dim FLTHADD As String = ""
            Try

                If rdb_import.SelectedValue = "Domestic" Then
                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "D")
                Else
                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "I")
                End If
                If (ds1.Tables(0).Rows.Count > 0) Then
                    Response.Write("<script>alert('PNR Already Exist')</script>")
                Else

                    ds = ViewState("pnrDs")
                    trackid_ = ds.Tables(4).Rows(0)("OrderId").ToString().Trim()
                    FLTHADD += ds.Tables(4).Rows(0)("AirlinePNR").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("VC").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("sector").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("GdsPnr").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("Adult").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("Child").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("Infant").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("PgTitle").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("PgFName").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("PgLName").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("PgMobile").ToString() + "#"
                    FLTHADD += ds.Tables(4).Rows(0)("PgEmail").ToString() + "#"

                    If ds.Tables.Count > 0 Then
                        Try
                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                airline = ds.Tables(0).Rows(i)("Airline").ToString()
                                departure = ds.Tables(0).Rows(i)("loc1").ToString()
                                destination = ds.Tables(0).Rows(i)("loc2").ToString()
                                depatdate = ds.Tables(0).Rows(i)("Depdate").ToString()
                                depattime = ds.Tables(0).Rows(i)("Deptime").ToString()
                                arrvdate = ds.Tables(0).Rows(i)("ArrDate").ToString()
                                arrvtime = ds.Tables(0).Rows(i)("Arrtime").ToString()
                                flightno = ds.Tables(0).Rows(i)("FlightNumber").ToString()
                                rdb = ds.Tables(0).Rows(i)("RBD").ToString()
                                Adtfrbas = ds.Tables(0).Rows(i)("AdtFareBasis").ToString()
                                chdfrbas = ds.Tables(0).Rows(i)("ChdFareBasis").ToString()
                                inffrbas = ds.Tables(0).Rows(i)("InfFareBasis").ToString()
                                'PInfo.InsertImportPNR(Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                ' arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString)
                                If rdb_import.SelectedValue = "Domestic" Then

                                    InsertImportPNRIntl(trackid_.Trim(), Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "D", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue, FLTHADD, Adtfrbas, chdfrbas, inffrbas)

                                Else
                                    InsertImportPNRIntl(trackid_.Trim(), Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "I", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue, FLTHADD, Adtfrbas, chdfrbas, inffrbas)

                                End If

                            Next
                            For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                                Dim Passengertittle As String = ""
                                Dim Passengerfname As String = ""
                                Dim Passengermname As String = ""
                                Dim Passengerlname As String = ""
                                Dim PaxType As String = ""
                                Dim Gender As String = ""
                                Dim Dob_ As String = ""
                                Passengertittle = ds.Tables(2).Rows(i)("Title")
                                Passengerfname = ds.Tables(2).Rows(i)("FName")
                                Passengerlname = ds.Tables(2).Rows(i)("LName")
                                PaxType = ds.Tables(2).Rows(i)("PaxType")
                                Gender = ds.Tables(2).Rows(i)("Gender")
                                Dob_ = ds.Tables(2).Rows(i)("DOB")

                                InsertPaxInfoIntl(trackid_, Dob_, Gender, PaxType, Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
                            Next



                            For k As Integer = 0 To ds.Tables(3).Rows.Count - 1
                                calcFare_Import(ds.Tables(3).Rows(k)("OrderId").ToString().Trim(), ds.Tables(3).Rows(k)("PaxType").ToString(), Convert.ToInt32(ds.Tables(3).Rows(k)("BaseFare")), Convert.ToInt32(ds.Tables(3).Rows(k)("GST")), Convert.ToInt32(ds.Tables(3).Rows(k)("YQ")), Convert.ToInt32(ds.Tables(3).Rows(k)("YR")), Convert.ToInt32(ds.Tables(3).Rows(k)("WO")), Convert.ToInt32(ds.Tables(3).Rows(k)("OT")), Convert.ToInt32(ds.Tables(3).Rows(k)("QTax")), Convert.ToInt32(ds.Tables(3).Rows(k)("TotalTax")), Convert.ToInt32(ds.Tables(3).Rows(k)("TotalFare")), "")
                            Next



                            lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
                            btn_update.Visible = False
                        Catch ex As Exception
                            clsErrorLog.LogInfo(ex)
                            Dim [Error] As String
                            [Error] = "Either The PNR Has Been Canceled Or Some Problem In The PNR"
                            lbl_Errormsg.Text = [Error]
                        End Try
                        paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
                    End If
                End If
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
                lbl_Errormsg.Text = ex.Message
            End Try
            'ENDGALLLLLLLLLLLLLLLLLL
        End If





    End Sub

    Public Sub calcFare_Import(ByVal trackid As String, ByVal paxtype As String, ByVal basefare As Integer, ByVal Gst As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal OT As Integer, ByVal QTax As Integer, ByVal totTax As Integer, ByVal totFare As Integer, ByVal FareType As String)

        Dim con1 As New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        con1.Open()
        Dim cmd As New SqlCommand("InsertFltFareDetails_Import", con1)
        Dim c As New Integer
        c = 0
        If (paxtype.ToUpper() = "CNN") Then
            paxtype = "CHD"
        End If
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@OrderID", trackid)
        cmd.Parameters.AddWithValue("@YQ", YQ)
        cmd.Parameters.AddWithValue("@BaseFare", basefare)
        cmd.Parameters.AddWithValue("@YR", YR)
        cmd.Parameters.AddWithValue("@WO", WO)
        cmd.Parameters.AddWithValue("@OT", OT)
        cmd.Parameters.AddWithValue("@GST", Gst)
        cmd.Parameters.AddWithValue("@Qtax", QTax)
        cmd.Parameters.AddWithValue("@TotalTax", totTax)
        cmd.Parameters.AddWithValue("@TotalFare", totFare)
        cmd.Parameters.AddWithValue("@ServiceTax", 0)
        cmd.Parameters.AddWithValue("@TranFee", 0)
        cmd.Parameters.AddWithValue("@AdminMrk", 0)
        cmd.Parameters.AddWithValue("@AgentMrk", 0)
        cmd.Parameters.AddWithValue("@DistrMrk", 0)
        cmd.Parameters.AddWithValue("@TotalDiscount", 0)
        cmd.Parameters.AddWithValue("@PLb", 0)
        cmd.Parameters.AddWithValue("@Discount", 0)
        cmd.Parameters.AddWithValue("@CashBack", 0)
        cmd.Parameters.AddWithValue("@Tds", 0)
        cmd.Parameters.AddWithValue("@TdsOn", 0)
        cmd.Parameters.AddWithValue("@TotalAfterDis", 0)
        cmd.Parameters.AddWithValue("@PaxType", paxtype)
        cmd.Parameters.AddWithValue("@ServiceTax1", 0)
        cmd.Parameters.AddWithValue("@Discount1", 0)
        cmd.Parameters.AddWithValue("@FareType", FareType)

        c = cmd.ExecuteNonQuery()
        con1.Close()
    End Sub

    Public Function InsertPaxInfoIntl(ByVal OrderId As String, ByVal dob As String, ByVal Gender As String, ByVal paxtype As String, ByVal tittle As String, ByVal fname As String, ByVal mname As String, ByVal lname As String, ByVal Tri As String) As Integer

        If (paxtype.ToUpper() = "CNN") Then
            paxtype = "CHD"
        End If

        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@tittle", tittle)
        paramHashtable.Add("@first_name", fname)
        paramHashtable.Add("@middle_name", mname)
        paramHashtable.Add("@last_name", lname)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@gender", Gender)
        paramHashtable.Add("@paxtype", paxtype)
        paramHashtable.Add("@dob", dob)
        paramHashtable.Add("@ticketno", "")

        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertPaxIntl_N", 1)
    End Function

    'Insert Intl Import PNR Details
    Public Function InsertImportPNRIntl(ByVal OrderId As String, ByVal PNR As String, ByVal Airline As String, ByVal Dept As String, ByVal dest As String, ByVal DDate As String, ByVal DTime As String, ByVal Adate As String, ByVal ATime As String, ByVal FNo As String, ByVal RBD As String, ByVal Status As String, ByVal BlockPNR As Boolean, ByVal userid As String, ByVal agency As String, ByVal Trip As String, ByVal TripType As String, ByVal Tri As String, ByVal projectID As String, ByVal BookedBy As String, ByVal Provider As String, ByVal FLHTR As String, ByVal ADTFRBAS As String, ByVal CHDFRBAS As String, ByVal INFFRBAS As String) As Integer
        Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim paramHashtable As New Hashtable
        paramHashtable.Clear()
        paramHashtable.Add("@OrderId", OrderId)
        paramHashtable.Add("@PNR", PNR)
        paramHashtable.Add("@Airline", Airline)
        paramHashtable.Add("@Dept", Dept)
        paramHashtable.Add("@dest", dest)
        paramHashtable.Add("@DDate", DDate)
        paramHashtable.Add("@DTime", DTime)
        paramHashtable.Add("@Adate", Adate)
        paramHashtable.Add("@ATime", ATime)
        paramHashtable.Add("@FNo", FNo)
        paramHashtable.Add("@RBD", RBD)
        paramHashtable.Add("@ADTFRBAS", ADTFRBAS)
        paramHashtable.Add("@CHDFRBAS", CHDFRBAS)
        paramHashtable.Add("@INFFRBAS", INFFRBAS)
        paramHashtable.Add("@userid", userid)
        paramHashtable.Add("@agency", agency)
        paramHashtable.Add("@Status", Status)
        paramHashtable.Add("@BlockPNR", False)
        paramHashtable.Add("@Trip", Trip)
        paramHashtable.Add("@TripType", TripType)
        paramHashtable.Add("@Tri", Tri)
        paramHashtable.Add("@ProjectID", projectID)
        paramHashtable.Add("@BookedBy", BookedBy)
        paramHashtable.Add("@Provider", Provider)
        paramHashtable.Add("@Fltheadr_column", FLHTR)
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "InsertImportPNRIntl_N", 1)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("~/Login.aspx")
            End If
            If Not Page.IsPostBack Then
                ViewState("pnrDs") = ""
                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
                If ds Is Nothing Then

                Else
                    If ds.Tables(0).Rows.Count > 0 Then
                        DropDownListProject.Items.Clear()
                        Dim item As New ListItem("Select")
                        DropDownListProject.AppendDataBoundItems = True
                        DropDownListProject.Items.Insert(0, item)
                        DropDownListProject.DataSource = ds.Tables(0)
                        DropDownListProject.DataTextField = "ProjectName"
                        DropDownListProject.DataValueField = "ProjectId"
                        DropDownListProject.DataBind()
                        Dim dtbooked As New DataTable
                        dtbooked = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "BB").Tables(0)
                        If ds.Tables(0).Rows.Count > 0 Then
                            DropDownListBookedBy.AppendDataBoundItems = True
                            DropDownListBookedBy.Items.Clear()
                            DropDownListBookedBy.Items.Insert(0, "Select")
                            DropDownListBookedBy.DataSource = dtbooked
                            DropDownListBookedBy.DataTextField = "BOOKEDBY"
                            DropDownListBookedBy.DataValueField = "BOOKEDBY"
                            DropDownListBookedBy.DataBind()
                        End If
                        TBL_Projects.Visible = True
                    Else
                        TBL_Projects.Visible = False

                    End If

                End If

            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try


            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "'")
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');window.location='PnrImportRequest.aspx';", True)
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    'Public Sub SetGridViewWidth()
    '    For i As Integer = 0 To GridView1.Columns.Count - 1
    '        GridView1.Columns(i).ItemStyle.Width = 200
    '    Next

    '    For i As Integer = 0 To GridView2.Columns.Count - 1
    '        GridView2.Columns(i).ItemStyle.Width = 200
    '    Next

    '    For i As Integer = 0 To GridView3.Columns.Count - 1
    '        GridView3.Columns(i).ItemStyle.Width = 200
    '    Next
    'End Sub

End Class

'Imports System.Web
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports GetPnrDetails
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.IO
'Imports System.Text
'Imports GALIMPORT
'Partial Class Reports_Import_PnrImportRequest
'    Inherits System.Web.UI.Page
'    Private PInfo As New pnrinfo()
'    Private ST As New SqlTransaction()
'    Private airline As String
'    Private departure As String
'    Private destination As String
'    Private depatdate As String
'    Private depattime As String
'    Private arrvdate As String
'    Private arrvtime As String
'    Private flightno As String
'    Private rdb As String
'    Dim objSelectedfltCls As New clsInsertSelectedFlight
'    Dim trackId As String
'    Dim trackIdDom As String
'    Dim STDom As New SqlTransactionDom()
'    Dim clsCorp As New ClsCorporate()
'    Protected Sub Btn_find_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_find.Click
'        Dim pnd As New GetPnrDetails()
'        If Imp_Source.SelectedValue = "1A" Then


'            If rdb_import.SelectedValue = "Domestic" Then
'                If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
'                    lbl_Errormsg.Text = "Pnr already exist in import queue."
'                Else
'                    Dim pnrno As String = pnd.PnrDetailsReply(Txt_pnr.Text, "DELVS384Q")
'                    Dim ds As New DataSet()
'                    Try
'                        ds.ReadXml(New StringReader(pnrno))
'                        ViewState("pnrDs") = ds

'                        Dim flag As Boolean = False
'                        If ds.Tables.Count > 1 Then
'                            Try
'                                For ii As Integer = 0 To ds.Tables.Count - 1
'                                    If ds.Tables(ii).TableName = "FareDetails" Then
'                                        flag = True
'                                        Exit For
'                                    Else
'                                        flag = False
'                                    End If
'                                Next
'                            Catch ex As Exception
'                                clsErrorLog.LogInfo(ex)
'                                flag = False
'                            End Try
'                            If flag = True Then
'                                GridViewshow.DataSource = ds.Tables("SegmentDetails")
'                                GridViewshow.DataBind()
'                                If ds.Tables("TktDetails").Rows.Count > 0 Then
'                                    Dim paxStr As String = "Passenger Details : <br/>"
'                                    For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
'                                        paxStr = paxStr & (i + 1).ToString & ". " & Split(ds.Tables("TktDetails").Rows(i)(0), "/")(0) & "<br/>"
'                                    Next
'                                    paxdiv.InnerHtml = paxStr
'                                    btn_update.Visible = True
'                                Else
'                                    lbl_Errormsg.Text = "Unable to find passenger."
'                                End If
'                            Else
'                                lbl_Errormsg.Text = "Please price the itinerary."
'                            End If
'                        Else
'                            If ds.Tables.Count > 0 Then
'                                If ds.Tables(0).TableName = "errorMessage" Then
'                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)(0).ToString
'                                End If
'                            End If
'                        End If
'                    Catch ex As Exception
'                        clsErrorLog.LogInfo(ex)
'                        lbl_Errormsg.Text = "Invalid Pnr Number."
'                    End Try
'                End If

'            Else

'                If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
'                    lbl_Errormsg.Text = "Pnr already exist in import queue."
'                Else
'                    Dim pnrno As String = pnd.PnrDetailsReply(Txt_pnr.Text, "DELVS384Q")
'                    Dim ds As New DataSet()
'                    Try
'                        ds.ReadXml(New StringReader(pnrno))
'                        ViewState("pnrDs") = ds

'                        Dim flag As Boolean = False
'                        If ds.Tables.Count > 1 Then
'                            Try
'                                For ii As Integer = 0 To ds.Tables.Count - 1
'                                    If ds.Tables(ii).TableName = "FareDetails" Then
'                                        flag = True
'                                        Exit For
'                                    Else
'                                        flag = False
'                                    End If
'                                Next
'                            Catch ex As Exception
'                                clsErrorLog.LogInfo(ex)
'                                flag = False
'                            End Try
'                            If flag = True Then
'                                GridViewshow.DataSource = ds.Tables("SegmentDetails")
'                                GridViewshow.DataBind()
'                                If ds.Tables("TktDetails").Rows.Count > 0 Then
'                                    Dim paxStr As String = "Passenger Details : <br/>"
'                                    For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
'                                        paxStr = paxStr & (i + 1).ToString & ". " & Split(ds.Tables("TktDetails").Rows(i)(0), "/")(0) & "<br/>"
'                                    Next
'                                    paxdiv.InnerHtml = paxStr
'                                    btn_update.Visible = True
'                                Else
'                                    lbl_Errormsg.Text = "Unable to find passenger."
'                                End If
'                            Else
'                                lbl_Errormsg.Text = "Please price the itinerary."
'                            End If
'                        Else
'                            If ds.Tables.Count > 0 Then
'                                If ds.Tables(0).TableName = "errorMessage" Then
'                                    lbl_Errormsg.Text = ds.Tables(0).Rows(0)(0).ToString
'                                End If
'                            End If
'                        End If
'                    Catch ex As Exception
'                        clsErrorLog.LogInfo(ex)
'                        lbl_Errormsg.Text = "Invalid Pnr Number."
'                    End Try
'                End If


'            End If
'        Else
'            'GAL PNRIMPORT
'            Try
'                If PInfo.cchkPnrIntl(Txt_pnr.Text) > 0 Then
'                    lbl_Errormsg.Text = "Pnr already exist in import queue."
'                Else

'                    Dim DataSetGAL As New DataSet
'                    Dim GALIMP As New GALIMPORTPNR
'                    If rdb_import.SelectedValue = "Domestic" Then
'                        DataSetGAL = GALIMP.PNRDetailsGAL(Txt_pnr.Text, "D")
'                    Else
'                        DataSetGAL = GALIMP.PNRDetailsGAL(Txt_pnr.Text, "I")
'                    End If



'                    If DataSetGAL.Tables.Count > 1 Then

'                        Dim paxStr As String = "Passenger Details : <br/>"
'                        GridViewshow.DataSource = DataSetGAL.Tables(0)
'                        GridViewshow.DataBind()
'                        ViewState("pnrDs") = DataSetGAL
'                        For i As Integer = 0 To DataSetGAL.Tables(1).Rows.Count - 1
'                            paxStr = paxStr & (i + 1).ToString & ". " & DataSetGAL.Tables(1).Rows(i)("FNAME") & " " & DataSetGAL.Tables(1).Rows(i)("LNAME") & "<br/>"
'                        Next
'                        paxdiv.InnerHtml = paxStr
'                        btn_update.Visible = True


'                    Else
'                        lbl_Errormsg.Text = DataSetGAL.Tables(0).Rows(0)("CancelStatus")
'                    End If


'                End If

'            Catch ex As Exception

'            End Try






'        End If

'        ''GAL IMPORT
'        'Dim FltRq As New FltRequest
'        'Dim FltComBal As New STD.BAL.FlightCommonBAL
'        'FltRq()
'        ''  Dim Req As String = FltComBal.PostXml(




'    End Sub

'    Protected Sub btn_update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_update.Click
'        Dim projectId As String = If(DropDownListProject.Visible = True, If(DropDownListProject.SelectedValue.ToLower() <> "select", DropDownListProject.SelectedValue, Nothing), Nothing)
'        Dim BookedBy As String = If(DropDownListBookedBy.Visible = True, If(DropDownListBookedBy.SelectedValue.ToLower() <> "select", DropDownListBookedBy.SelectedValue, Nothing), Nothing)

'        If Imp_Source.SelectedValue = "1A" Then
'            'AAAAAAAAAAAAAAAAAA
'            If rdb_import.SelectedValue = "Domestic" Then
'                trackIdDom = objSelectedfltCls.getRndNum
'                Dim ds As New DataSet
'                Dim ds1 As New DataSet
'                Try
'                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "D")
'                    If (ds1.Tables(0).Rows.Count > 0) Then
'                        Response.Write("<script>alert('PNR Already Exist')</script>")
'                    Else

'                        ds = ViewState("pnrDs")
'                        If ds.Tables.Count > 0 Then
'                            Try

'                                For i As Integer = 0 To ds.Tables("SegmentDetails").Rows.Count - 1
'                                    airline = ds.Tables("SegmentDetails").Rows(i)("Airline").ToString()
'                                    departure = ds.Tables("SegmentDetails").Rows(i)("loc1").ToString()
'                                    destination = ds.Tables("SegmentDetails").Rows(i)("loc2").ToString()
'                                    depatdate = ds.Tables("SegmentDetails").Rows(i)("Depdate").ToString()
'                                    depattime = ds.Tables("SegmentDetails").Rows(i)("Deptime").ToString()
'                                    arrvdate = ds.Tables("SegmentDetails").Rows(i)("ArrDate").ToString()
'                                    arrvtime = ds.Tables("SegmentDetails").Rows(i)("Arrtime").ToString()
'                                    flightno = ds.Tables("SegmentDetails").Rows(i)("FlightNumber").ToString()
'                                    rdb = ds.Tables("SegmentDetails").Rows(i)("RBD").ToString()
'                                    'PInfo.InsertImportPNR(Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                    ' arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString)
'                                    ST.InsertImportPNRIntl(trackIdDom, Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "D", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)

'                                Next
'                                For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
'                                    Dim str As String = ds.Tables("TktDetails").Rows(i)("PaxTktDetails").ToString()
'                                    Dim pax As String() = str.Split("/"c)
'                                    Dim pax1 As String() = pax(0).Split(" "c)
'                                    Dim pax2 As String = ""
'                                    For ii As Integer = 0 To pax1.Length - 1
'                                        If pax1(ii).ToString <> "" And pax1(ii) IsNot Nothing Then
'                                            If ii = pax1.Length - 1 Then
'                                                pax2 = pax2 & pax1(ii).ToString
'                                            Else
'                                                pax2 = pax2 & pax1(ii).ToString & " "
'                                            End If
'                                        End If
'                                    Next
'                                    pax1 = pax2.Split(" "c)
'                                    Dim Passengertittle As String = ""
'                                    Dim Passengerfname As String = ""
'                                    Dim Passengermname As String = ""
'                                    Dim Passengerlname As String = ""
'                                    If pax1.Length = 5 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengermname = pax1(2).ToString() & " " & pax1(3).ToString()
'                                        Passengerlname = pax1(4).ToString()
'                                    ElseIf pax1.Length = 4 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengermname = pax1(2).ToString()
'                                        Passengerlname = pax1(3).ToString()
'                                    ElseIf pax1.Length = 3 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengerlname = pax1(2).ToString()
'                                    ElseIf pax1.Length = 2 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                    ElseIf pax1.Length = 1 Then
'                                        Passengerfname = pax1(0).ToString()
'                                    Else
'                                        For ii As Integer = 0 To pax1.Length - 1
'                                            Passengerfname = Passengerfname & pax1(ii) & " "
'                                        Next
'                                    End If
'                                    ST.InsertPaxInfoIntl(trackIdDom, Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
'                                Next
'                                lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
'                                btn_update.Visible = False
'                            Catch ex As Exception
'                                clsErrorLog.LogInfo(ex)
'                                Dim [Error] As String
'                                [Error] = ds.Tables("errormessage").Rows(0)("errorMessage_Text").ToString()
'                                lbl_Errormsg.Text = [Error]
'                            End Try

'                            GridViewshow.Visible = False
'                            paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
'                        End If
'                    End If
'                Catch ex As Exception
'                    clsErrorLog.LogInfo(ex)
'                    lbl_Errormsg.Text = ex.Message
'                End Try
'            Else
'                'For Intenational
'                trackId = objSelectedfltCls.getRndNum
'                Dim ds As New DataSet
'                Dim ds1 As New DataSet
'                Try
'                    ds1 = ST.PnrImportIntlDetails(trackId, "I")
'                    If (ds1.Tables(0).Rows.Count > 0) Then
'                        Response.Write("<script>alert('PNR Already Exist')</script>")
'                    Else

'                        ds = ViewState("pnrDs")
'                        If ds.Tables.Count > 0 Then
'                            Try
'                                For i As Integer = 0 To ds.Tables("SegmentDetails").Rows.Count - 1
'                                    airline = ds.Tables("SegmentDetails").Rows(i)("Airline").ToString()
'                                    departure = ds.Tables("SegmentDetails").Rows(i)("loc1").ToString()
'                                    destination = ds.Tables("SegmentDetails").Rows(i)("loc2").ToString()
'                                    depatdate = ds.Tables("SegmentDetails").Rows(i)("Depdate").ToString()
'                                    depattime = ds.Tables("SegmentDetails").Rows(i)("Deptime").ToString()
'                                    arrvdate = ds.Tables("SegmentDetails").Rows(i)("ArrDate").ToString()
'                                    arrvtime = ds.Tables("SegmentDetails").Rows(i)("Arrtime").ToString()
'                                    flightno = ds.Tables("SegmentDetails").Rows(i)("FlightNumber").ToString()
'                                    rdb = ds.Tables("SegmentDetails").Rows(i)("RBD").ToString()
'                                    ST.InsertImportPNRIntl(trackId, Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "I", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)
'                                Next

'                                For i As Integer = 0 To ds.Tables("TktDetails").Rows.Count - 1
'                                    Dim str As String = ds.Tables("TktDetails").Rows(i)("PaxTktDetails").ToString()
'                                    Dim pax As String() = str.Split("/"c)
'                                    Dim pax1 As String() = pax(0).Split(" "c)
'                                    Dim pax2 As String = ""
'                                    For ii As Integer = 0 To pax1.Length - 1
'                                        If pax1(ii).ToString <> "" And pax1(ii) IsNot Nothing Then
'                                            If ii = pax1.Length - 1 Then
'                                                pax2 = pax2 & pax1(ii).ToString
'                                            Else
'                                                pax2 = pax2 & pax1(ii).ToString & " "
'                                            End If
'                                        End If
'                                    Next
'                                    pax1 = pax2.Split(" "c)
'                                    Dim Passengertittle As String = ""
'                                    Dim Passengerfname As String = ""
'                                    Dim Passengermname As String = ""
'                                    Dim Passengerlname As String = ""
'                                    If pax1.Length = 5 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengermname = pax1(2).ToString() & " " & pax1(3).ToString()
'                                        Passengerlname = pax1(4).ToString()
'                                    ElseIf pax1.Length = 4 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengermname = pax1(2).ToString()
'                                        Passengerlname = pax1(3).ToString()
'                                    ElseIf pax1.Length = 3 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                        Passengerlname = pax1(2).ToString()
'                                    ElseIf pax1.Length = 2 Then
'                                        Passengertittle = pax1(0).ToString()
'                                        Passengerfname = pax1(1).ToString()
'                                    ElseIf pax1.Length = 1 Then
'                                        Passengerfname = pax1(0).ToString()
'                                    Else
'                                        For ii As Integer = 0 To pax1.Length - 1
'                                            Passengerfname = Passengerfname & pax1(ii) & " "
'                                        Next
'                                    End If
'                                    ST.InsertPaxInfoIntl(trackId, Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
'                                Next
'                                lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
'                                btn_update.Visible = False
'                            Catch ex As Exception
'                                clsErrorLog.LogInfo(ex)
'                                Dim [Error] As String
'                                [Error] = ds.Tables("errormessage").Rows(0)("errorMessage_Text").ToString()
'                                lbl_Errormsg.Text = [Error]
'                            End Try

'                            GridViewshow.Visible = False
'                            paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
'                        End If
'                    End If
'                Catch ex As Exception
'                    clsErrorLog.LogInfo(ex)
'                    lbl_Errormsg.Text = ex.Message
'                End Try




'            End If
'            'ENDAAAAAAAAAAAAAAA
'        Else
'            'GALLLLLLLLLLLLLLLLLLL
'            'If rdb_import.SelectedValue = "Domestic" Then
'            trackIdDom = objSelectedfltCls.getRndNum
'            Dim ds As New DataSet
'            Dim ds1 As New DataSet
'            Try

'                If rdb_import.SelectedValue = "Domestic" Then
'                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "D")
'                Else
'                    ds1 = ST.PnrImportIntlDetails(trackIdDom, "I")
'                End If
'                If (ds1.Tables(0).Rows.Count > 0) Then
'                    Response.Write("<script>alert('PNR Already Exist')</script>")
'                Else

'                    ds = ViewState("pnrDs")
'                    If ds.Tables.Count > 0 Then
'                        Try
'                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'                                airline = ds.Tables(0).Rows(i)("Airline").ToString()
'                                departure = ds.Tables(0).Rows(i)("loc1").ToString()
'                                destination = ds.Tables(0).Rows(i)("loc2").ToString()
'                                depatdate = ds.Tables(0).Rows(i)("Depdate").ToString()
'                                depattime = ds.Tables(0).Rows(i)("Deptime").ToString()
'                                arrvdate = ds.Tables(0).Rows(i)("ArrDate").ToString()
'                                arrvtime = ds.Tables(0).Rows(i)("Arrtime").ToString()
'                                flightno = ds.Tables(0).Rows(i)("FlightNumber").ToString()
'                                rdb = ds.Tables(0).Rows(i)("RBD").ToString()
'                                'PInfo.InsertImportPNR(Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                ' arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString)
'                                If rdb_import.SelectedValue = "Domestic" Then

'                                    ST.InsertImportPNRIntl(trackIdDom, Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "D", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)

'                                Else
'                                    ST.InsertImportPNRIntl(trackIdDom, Txt_pnr.Text, airline, departure, destination, depatdate, depattime, _
'                                     arrvdate, arrvtime, flightno, rdb, "Pending", False, Session("UID").ToString, Session("AgencyName").ToString, "I", rdb_Triptype.SelectedValue, "I", projectId, BookedBy, Imp_Source.SelectedValue)

'                                End If

'                            Next
'                            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
'                                Dim Passengertittle As String = ""
'                                Dim Passengerfname As String = ""
'                                Dim Passengermname As String = ""
'                                Dim Passengerlname As String = ""
'                                Passengerfname = ds.Tables(1).Rows(i)("FName")
'                                Passengerlname = ds.Tables(1).Rows(i)("LName")
'                                ST.InsertPaxInfoIntl(trackIdDom, Passengertittle, Passengerfname, Passengermname, Passengerlname, "I")
'                            Next
'                            lbl_Errormsg.Text = "Pnr Updated Sucssessfully"
'                            btn_update.Visible = False
'                        Catch ex As Exception
'                            clsErrorLog.LogInfo(ex)
'                            Dim [Error] As String
'                            [Error] = "Either The PNR Has Been Canceled Or Some Problem In The PNR"
'                            lbl_Errormsg.Text = [Error]
'                        End Try

'                        GridViewshow.Visible = False
'                        paxdiv.Style(HtmlTextWriterStyle.Display) = "none"
'                    End If
'                End If
'            Catch ex As Exception
'                clsErrorLog.LogInfo(ex)
'                lbl_Errormsg.Text = ex.Message
'            End Try
'            'ENDGALLLLLLLLLLLLLLLLLL
'        End If





'    End Sub

'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Response.Cache.SetCacheability(HttpCacheability.NoCache)
'        Try
'            If Session("UID") = "" Or Session("UID") Is Nothing Then
'                Response.Redirect("~/Login.aspx")
'            End If
'            If Not Page.IsPostBack Then
'                ViewState("pnrDs") = ""
'                Dim ds As DataSet = clsCorp.Get_Corp_Project_Details_By_AgentID(Session("UID").ToString(), Session("User_Type"))
'                If ds Is Nothing Then

'                Else
'                    If ds.Tables(0).Rows.Count > 0 Then
'                        DropDownListProject.Items.Clear()
'                        Dim item As New ListItem("Select")
'                        DropDownListProject.AppendDataBoundItems = True
'                        DropDownListProject.Items.Insert(0, item)
'                        DropDownListProject.DataSource = ds.Tables(0)
'                        DropDownListProject.DataTextField = "ProjectName"
'                        DropDownListProject.DataValueField = "ProjectId"
'                        DropDownListProject.DataBind()
'                        Dim dtbooked As New DataTable
'                        dtbooked = clsCorp.Get_Corp_BookedBy(Session("UID").ToString(), "BB").Tables(0)
'                        If ds.Tables(0).Rows.Count > 0 Then
'                            DropDownListBookedBy.AppendDataBoundItems = True
'                            DropDownListBookedBy.Items.Clear()
'                            DropDownListBookedBy.Items.Insert(0, "Select")
'                            DropDownListBookedBy.DataSource = dtbooked
'                            DropDownListBookedBy.DataTextField = "BOOKEDBY"
'                            DropDownListBookedBy.DataValueField = "BOOKEDBY"
'                            DropDownListBookedBy.DataBind()
'                        End If
'                        TBL_Projects.Visible = True
'                    Else
'                        TBL_Projects.Visible = False

'                    End If

'                End If

'            End If
'        Catch ex As Exception
'            clsErrorLog.LogInfo(ex)

'        End Try
'    End Sub
'    Protected Sub IAImport()

'    End Sub
'  End Class
