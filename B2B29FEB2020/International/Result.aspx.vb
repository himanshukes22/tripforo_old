Imports System.Data

Partial Class FlightInt_FltResult
    Inherits System.Web.UI.Page

    Dim SearchValue As String
    Dim Origin() As String, Destination() As String
    Dim SearchQuery As New Hashtable
    Dim ObjIntFlt As New IntFltResult
    Dim str As String
    Dim IntAirDs As New DataSet
    Dim IntAirDt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            Else
                If Page.IsPostBack Then
                    Dim eventTarget As String = Me.Request("__EVENTTARGET")
                    Dim eventArgument As String = Me.Request("__EVENTARGUMENT")
                    Dim faredt As DataTable
                    Dim ds1 As New DataSet
                    Try
                        If eventArgument.Trim.Length = 2 Then
                            faredt = FilterDt(FilterByAirLine(Session("IntAirDt"), eventArgument), "All", True)
                        Else
                            If eventTarget.ToString.IndexOf("chkair") < 0 Then
                                faredt = FilterDt(Session("IntAirDt"), eventArgument, False)
                            End If
                        End If
                        If eventTarget.ToString.IndexOf("chkair") < 0 Then
                            ds1.Tables.Add(faredt.Copy)
                            Dim MasterPricerResponse = ds1.GetXml()
                            Dim strRowCount As String
                            If (eventArgument = "1-Stop") Then
                                strRowCount = (faredt.Rows.Count / 2).ToString
                            Else
                                strRowCount = faredt.Rows.Count.ToString
                            End If
                            lblTotal.Text = strRowCount & "  matching flights found"
                            Dim mpresponse As Object = MasterPricerResponse
                            xmlRes.DocumentContent = MasterPricerResponse
                            faredt.Clear()
                            faredt = FilterDt(Session("IntAirDt"), "All", False)
                            matrixTable(faredt)
                        End If
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                        Response.Redirect("NoResult.aspx")
                    End Try
                Else

                    Try
                        Origin = Split(Request.QueryString("txtDepCity1"), "(")
                        Destination = Split(Request.QueryString("txtArrCity1"), "(")
                        SearchValue = Left(Origin(1), 3) & Left(Destination(1), 3) & Now.ToString
                        SearchQuery.Add("TripType", Request.QueryString("TripType"))
                        SearchQuery.Add("Origin", Request.QueryString("txtDepCity1"))
                        SearchQuery.Add("hidOrigin", Request.QueryString("hidtxtDepCity1"))
                        SearchQuery.Add("Destination", Request.QueryString("txtArrCity1"))
                        SearchQuery.Add("hidDestination", Request.QueryString("hidtxtArrCity1"))
                        SearchQuery.Add("Depdate", Request.QueryString("txtDepDate"))
                        SearchQuery.Add("retDate", Request.QueryString("txtRetDate"))
                        SearchQuery.Add("Adult", Request.QueryString("Adult"))
                        SearchQuery.Add("Child", Request.QueryString("Child"))
                        SearchQuery.Add("Infant", Request.QueryString("Infant"))
                        SearchQuery.Add("Cabin", Request.QueryString("Cabin"))
                        SearchQuery.Add("Airline", Request.QueryString("txtAirline"))
                        SearchQuery.Add("hidAirline", Request.QueryString("hidtxtAirline"))
                        SearchQuery.Add("NStop", Request.QueryString("NStop"))
                        SearchQuery.Add("SearchValue", SearchValue)
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)

                    End Try
                    IntAirDt = ObjIntFlt.GetResults(SearchQuery)
                    If IntAirDt.Rows.Count < 1 Then
                    Else
                        'xmlRes.DocumentContent = str
                        'IntAirDs.ReadXml(New System.IO.StringReader(str))
                        'IntAirDt = IntAirDs.Tables(0)
                        Session("IntAirDt") = IntAirDt
                        Dim faredt As DataTable
                        faredt = FilterDtAllFlights(IntAirDt, "All", False)
                        Dim ds1 As New DataSet
                        ds1.Tables.Add(faredt.Copy)
                        Dim MasterPricerResponse As String = ds1.GetXml()
                        xmlRes.DocumentContent = MasterPricerResponse
                        lblTotal.Text = IntAirDt.Rows(IntAirDt.Rows.Count - 1)("LineItemNumber") & "  matching flights found"
                        matrixTable(faredt)
                        If Not Page.IsPostBack Then
                            If IntAirDt.Rows.Count <> 0 Then
                                CreateDynamicControl(IntAirDt)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub

    Public Function FilterDt(ByVal fltDt As DataTable, ByVal st As String, ByVal IsPostBack As Boolean) As DataTable
        Try
            Dim i As Integer
            Dim faredt As New DataTable
            faredt = fltDt.Clone
            Dim st1 As String = ""
            If st = "All" Then
            ElseIf st = "0-Stop" Or st = "1-Stop" Or st = "2-Stop" Then
                st1 = "Stops='" & st & "'"
            Else
                st1 = "TotalFare=" & st & ""
            End If
            Dim TF As Array = fltDt.Select(st1, "TotalFare ASC")
            Dim flg As Boolean = False
            If st = "0-Stop" Or st = "1-Stop" Or st = "2-Stop" Or st = "All" Then
                For i = 0 To TF.Length - 1
                    If i = 0 Then
                        faredt.ImportRow(TF(i))
                    Else
                        If Convert.ToInt32(TF(i)("LineItemNumber").ToString.Trim) = Convert.ToInt32(TF(i - 1)("LineItemNumber").ToString.Trim) Then
                            If flg = False Then
                                faredt.ImportRow(TF(i))
                            End If
                        Else
                            If Convert.ToDouble(TF(i)("TotalFare").ToString.Trim) = Convert.ToDouble(TF(i - 1)("TotalFare").ToString.Trim) Then
                                If TF(i)("MarketingCarrier").ToString.Trim = TF(i - 1)("MarketingCarrier").ToString.Trim Then
                                    If IsPostBack = True Then
                                        faredt.ImportRow(TF(i))
                                        flg = False
                                    Else
                                        flg = True
                                    End If
                                Else
                                    faredt.ImportRow(TF(i))
                                    flg = False
                                End If
                            Else
                                faredt.ImportRow(TF(i))
                                flg = False
                            End If
                        End If
                    End If
                Next
            Else
                For i = 0 To TF.Length - 1
                    faredt.ImportRow(TF(i))
                Next
            End If
            Return faredt
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)


        End Try

    End Function

    Public Function FilterDtAllFlights(ByVal fltDt As DataTable, ByVal st As String, ByVal IsPostBack As Boolean) As DataTable
        Dim i As Integer
        Dim faredt As New DataTable
        Try
            faredt = fltDt.Clone
            Dim st1 As String = ""
            If st = "All" Then
            ElseIf st = "0-Stop" Or st = "1-Stop" Or st = "2-Stop" Then
                st1 = "Stops='" & st & "'"
            Else
                st1 = "TotalFare=" & st & ""
            End If
            Dim TF As Array = fltDt.Select(st1, "TotalFare ASC")
            Dim flg As Boolean = False

            For i = 0 To TF.Length - 1
                faredt.ImportRow(TF(i))
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
        Return faredt
    End Function

    Public Sub matrixTable(ByVal fltDt As DataTable)
        Try
            Dim AirmtrxTbl As New HtmlTable
            Dim AirMtrxRow As HtmlTableRow
            Dim AirMtrxColumn As HtmlTableCell
            AirmtrxTbl.CellPadding = "0"
            AirmtrxTbl.CellSpacing = "0"
            AirmtrxTbl.Width = "100%"
            AirmtrxTbl.Border = "0"

            AirMtrxRow = New HtmlTableRow
            AirMtrxColumn = New HtmlTableCell
            AirMtrxColumn.Align = "left"
            AirMtrxColumn.VAlign = "middle"
            AirMtrxColumn.ColSpan = "2"
            ' AirMtrxColumn.Attributes.Add("class", "tablebk2")
            'AirMtrxColumn.InnerHtml = "<span style='font-size:12px; width:100%; color:black; font-weight:700;'>Flight Options</span>"
            AirMtrxRow.Cells.Add(AirMtrxColumn)
            AirmtrxTbl.Rows.Add(AirMtrxRow)

            AirMtrxRow = New HtmlTableRow
            AirMtrxColumn = New HtmlTableCell
            AirMtrxColumn.Align = "center"
            AirMtrxColumn.Width = "85"
            AirMtrxColumn.VAlign = "Top"
            AirMtrxColumn.InnerHtml = "<table cellspacing='0' cellpadding='0' rules='all' style='font-weight:600;'><tr><td  style='width:85px; height:70px;'><a href='javascript:;' class='far_det' onclick=SetHiddenVariable1('All');>Show All</a></td></tr><tr><td style='width:85px; height:30px; color:#fff;'><a href='javascript:;' class='far_det' onclick='SetHiddenVariable(0);'>0-Stop</a></td></tr><tr><td style='width:85px; height:30px;color:#fff;'><a href='javascript:;' class='far_det' onclick='SetHiddenVariable(1);'>1-Stop</a></td></tr><tr><td style='width:85px; height:30px;color:#fff;' rowspan='2'><a href='javascript:;' class='far_det' onclick='SetHiddenVariable(2);'>2-Stop</a></td></tr></table>"
            AirMtrxRow.Cells.Add(AirMtrxColumn)

            AirMtrxColumn = New HtmlTableCell
            AirMtrxColumn.Align = "center"
            AirMtrxColumn.Width = "665"
            'AirMtrxColumn.RowSpan = "5"
            AirMtrxColumn.VAlign = "top"
            Dim pgstr As String = ""
            pgstr = pgstr & "<div style='width:850px; height:190px; overflow-x:scroll; overflow-y:hidden;overflow-y:hidden;overflow-x:scroll; margin-left:0px; margin-top:0px;scrollbar-height:3px;'><table width='100%' border='1px' border-color='#eee' style='color:black; font-weight:300;' cellspacing='0' cellpadding='0'><tr>"
            Dim Al_C_N() As String
            Al_C_N = Split("MarketingCarrier,AirLineName", ",")
            Dim dt_MktCr As DataTable = fltDt.DefaultView.ToTable(True, Al_C_N)

            For i As Integer = 0 To dt_MktCr.Rows.Count - 1 ' width='76' height='16'
                'pgstr = pgstr & "<td style='width:80px; height:70px; text-align:center;'><a href='javascript:;' onclick=SetHiddenVariable1('" & dt_MktCr.Rows(i).Item(0).ToString.Trim & "');><img src='http://203.89.132.3/TBN/agents/airline/sm" & dt_MktCr.Rows(i).Item(0).ToString.Trim & ".gif'  alt ='" & dt_MktCr.Rows(i).Item(1).ToString.Trim & "' border ='0'/ ></a><br/><span class='alname'>" & dt_MktCr.Rows(i).Item(1).ToString.Trim & "</span></td>"
                pgstr = pgstr & "<td style='width:100px; height:70px; text-align:center;'><table width='80' border='0' cellspacing='0' cellpadding='0'><tr><td width='80'><a href='javascript:;' onclick=SetHiddenVariable1('" & dt_MktCr.Rows(i).Item(0).ToString.Trim & "');><img src='http://b2b.ITZ.com/airlogo/sm" & dt_MktCr.Rows(i).Item(0).ToString.Trim & ".gif'  alt ='" & dt_MktCr.Rows(i).Item(1).ToString.Trim & "' border ='0'/ ></a></td></tr><tr><td width='80'><span class='alname'>" & dt_MktCr.Rows(i).Item(1).ToString.Trim & "</span></td></tr></table>"
            Next
            pgstr = pgstr & "</tr><tr>"
            Dim Aprc As Array '= fltDt.Select("Stops='0-Stop'", "TotalFare ASC")
            Dim Airlinecode As String = ""
            Dim flg1 As String = ""
            Try
                For i As Integer = 0 To dt_MktCr.Rows.Count - 1
                    Aprc = fltDt.Select("Stops='0-Stop' and MarketingCarrier='" & dt_MktCr.Rows(i)("MarketingCarrier").ToString.Trim & "'", "TotalFare ASC")
                    If Aprc.Length > 0 Then
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'><a href='javascript:;' class='price' onclick='SetHiddenVariable(" & Aprc(0)("TotalFare") & ");'>Rs. " & Format(Convert.ToDouble(Aprc(0)("TotalFare").ToString.Trim), "##,###").ToString & "</a></td>"
                    Else
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'>-</td>"
                    End If
                Next
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
            pgstr = pgstr & "</tr><tr>"

            Airlinecode = ""
            flg1 = ""
            Try
                For i As Integer = 0 To dt_MktCr.Rows.Count - 1
                    Aprc = fltDt.Select("Stops='1-Stop' and MarketingCarrier='" & dt_MktCr.Rows(i)("MarketingCarrier").ToString.Trim & "'", "TotalFare ASC")
                    If Aprc.Length > 0 Then
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'><a href='javascript:;' class='price' onclick='SetHiddenVariable(" & Aprc(0)("TotalFare") & ");'>Rs. " & Format(Convert.ToDouble(Aprc(0)("TotalFare").ToString.Trim), "##,###").ToString & "</a></td>"
                    Else
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'>-</td>"
                    End If
                Next
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
            pgstr = pgstr & "</tr><tr>"


            Airlinecode = ""
            flg1 = ""
            Try
                For i As Integer = 0 To dt_MktCr.Rows.Count - 1
                    Aprc = fltDt.Select("Stops='2-Stop' and MarketingCarrier='" & dt_MktCr.Rows(i)("MarketingCarrier").ToString.Trim & "'", "TotalFare ASC")
                    If Aprc.Length > 0 Then
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'><a href='javascript:;' class='price' onclick='SetHiddenVariable(" & Aprc(0)("TotalFare") & ");'>Rs. " & Format(Convert.ToDouble(Aprc(0)("TotalFare").ToString.Trim), "##,###").ToString & "</a></td>"
                    Else
                        pgstr = pgstr & "<td style='width:80px; height:30px; text-align:center;'>-</td>"
                    End If
                Next
            Catch ex As Exception
                clsErrorLog.LogInfo(ex)
            End Try
            pgstr = pgstr & "</tr>"

            pgstr = pgstr & "</table></div>"
            AirMtrxColumn.InnerHtml = pgstr
            AirMtrxRow.Cells.Add(AirMtrxColumn)
            AirmtrxTbl.Rows.Add(AirMtrxRow)
            mtrxPL.Controls.Add(AirmtrxTbl)
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try


    End Sub
    Public Function FilterByAirLine(ByVal FltDt As DataTable, ByVal Airline As String) As DataTable
        Try
            Dim Flt_AR As Array = FltDt.Select("MarketingCarrier='" & Airline.Trim & "'", "LineItemNumber ASC")
            Dim LIN As String = ""
            Dim faredt As New DataTable
            faredt = FltDt.Clone
            For i As Integer = 0 To Flt_AR.Length - 1
                Dim Flt_AR1 As Array
                Flt_AR1 = FltDt.Select("LineItemNumber='" & Flt_AR(i)("LineItemNumber") & "'", "")
                If LIN <> Flt_AR(i)("LineItemNumber") Then
                    For ii As Integer = 0 To Flt_AR1.Length - 1
                        If Airline.Trim = Flt_AR1(ii)("MarketingCarrier").ToString.Trim Then
                            For iii As Integer = 0 To Flt_AR1.Length - 1
                                faredt.ImportRow(Flt_AR1(iii))
                            Next
                            Exit For
                        End If
                    Next
                    LIN = Flt_AR(i)("LineItemNumber")
                End If
            Next
            Return faredt
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Function
    Protected Sub chkair_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkair.SelectedIndexChanged
        Try
            Dim eventTarget As String = Me.Request("__EVENTTARGET")
            If eventTarget.ToString.IndexOf("chkair") < 0 Then
            Else
                Dim iCtr As Integer = 0
                Dim Carrier As String = ""
                Dim faredt As DataTable
                For iCtr = 0 To chkair.Items.Count - 1
                    If chkair.Items.Item(iCtr).Selected Then
                        If Carrier = "" Then
                            Carrier = "'" & chkair.Items.Item(iCtr).Value & "'"
                        Else
                            Carrier = Carrier & "," & "'" & chkair.Items.Item(iCtr).Value & "'"
                        End If
                    End If
                Next
                Carrier = "(" & Carrier & ")"
                faredt = FilterByAirln(Session("IntAirDt"), Carrier)
                Dim ds1 As New DataSet
                ds1.Tables.Add(faredt.Copy)
                Dim MasterPricerResponse = ds1.GetXml()
                Dim mpresponse As Object = MasterPricerResponse
                xmlRes.DocumentContent = MasterPricerResponse
                matrixTable(Session("IntAirDt"))
                lblTotal.Text = totalFlight(faredt) & "  matching flights found"
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Protected Function FilterByAirln(ByVal FltDt As DataTable, ByVal Airline As String) As DataTable
        Try
            Dim faredt As New DataTable
            Dim dv As DataView = FltDt.DefaultView
            If Airline <> "()" Then
                dv.RowFilter = "MarketingCarrier in " & Airline & " "
            Else
                dv.RowFilter = "MarketingCarrier ='' "
            End If
            faredt = dv.ToTable
            Return faredt
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Function
    Sub CreateDynamicControl(ByRef dtTable As DataTable)
        Try
            Dim Al_C_N() As String
            Al_C_N = Split("MarketingCarrier,AirLineName", ",")
            Dim dt_MktCr As DataTable = dtTable.DefaultView.ToTable(True, Al_C_N)
            chkair.DataSource = dt_MktCr
            chkair.DataValueField = "MarketingCarrier"
            chkair.DataTextField = "AirlineName"
            chkair.DataBind()
            Dim li As ListItem
            For Each li In chkair.Items
                li.Selected = True
            Next
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub

    Public Function totalFlight(ByVal dt As DataTable) As String
        Try
            Dim cnt As Integer
            Dim prevRecord As Integer = 0
            If dt.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To dt.Rows.Count - 1
                    If prevRecord = 0 Then
                        cnt = cnt + 1
                        prevRecord = Int16.Parse(dt.Rows(i)("LineItemNumber").ToString())
                    Else
                        If prevRecord <> dt.Rows(i)("LineItemNumber").ToString() Then
                            cnt = cnt + 1
                            prevRecord = Int16.Parse(dt.Rows(i)("LineItemNumber").ToString())
                        End If
                    End If
                Next
            End If
            Return cnt.ToString
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Function

    'Public Function blockAL(ByVal Org1 As String, ByVal Dst1 As String, ByVal Org2 As String, ByVal Dst2 As String, ByVal Org3 As String, ByVal Dst3 As String, ByVal TType As String, ByVal JType As String) As String
    '    Dim adpA As SqlDataAdapter
    '    Dim cmdA As New SqlCommand
    '    Dim alDT As New DataTable
    '    Dim strquery As String = "", BlockXml As String = ""
    '    Try
    '        consql.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
    '        consql.Open()
    '        cmdA.CommandType = CommandType.StoredProcedure
    '        cmdA.CommandText = "sp_blockCarrier"
    '        cmdA.Connection = consql
    '        cmdA.Parameters.Add("@org1", SqlDbType.Char).Value = Org1
    '        cmdA.Parameters.Add("@dst1", SqlDbType.Char).Value = Dst1
    '        cmdA.Parameters.Add("@org2", SqlDbType.Char).Value = Org2
    '        cmdA.Parameters.Add("@dst2", SqlDbType.Char).Value = Dst2
    '        cmdA.Parameters.Add("@org3", SqlDbType.Char).Value = Org3
    '        cmdA.Parameters.Add("@dst3", SqlDbType.Char).Value = Dst3
    '        cmdA.Parameters.Add("@ttype", SqlDbType.VarChar).Value = TType
    '        cmdA.Parameters.Add("@jtype", SqlDbType.VarChar).Value = JType
    '        adpA = New SqlDataAdapter(cmdA)
    '        adpA.Fill(alDT)
    '        If alDT.Rows.Count > 0 Then
    '            BlockXml = "<companyIdentity><carrierQualifier>X</carrierQualifier>"
    '            For i As Integer = 0 To alDT.Rows.Count - 1
    '                BlockXml = BlockXml & "<carrierId>" & alDT.Rows(i)("AirlineCode") & "</carrierId>"
    '            Next
    '            BlockXml = BlockXml & "</companyIdentity>"
    '        End If
    '        consql.Close()
    '    Catch ex As Exception
    '        BlockXml = ""
    '        consql.Close()
    '    End Try
    '    Return BlockXml
    'End Function
End Class

