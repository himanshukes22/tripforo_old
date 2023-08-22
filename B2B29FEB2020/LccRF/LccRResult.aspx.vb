Imports System
Imports System.Data
Partial Class LccRF_LccRResult
    Inherits System.Web.UI.Page
    Dim objSpice As New NEWLCCRTF.RTFclsSpiceJet
    Dim objIndigo As New NEWLCCRTF.RTFclsIndigo
    Dim sCurrency As String = "INR"
    Dim Infant_basic As Double = 907
    Dim Infant_Tax As Double = 93
    Dim iTotalNo_Adt As Integer = 0
    Dim iTotalNo_Chd As Integer = 0
    Dim iTotalNo_Inf As Integer = 0
    Dim SearchQuery As New Hashtable

    Dim custinfo As New Hashtable

    Dim resDT As New Data.DataTable
    Dim resDTIndigo As New Data.DataTable
    Dim resBookingDT As New Data.DataTable
    Dim totFareO As Double = 0, totFareR As Double = 0
    Dim PickupDDMMYY() As String, DropDDMMYY() As String
    Dim depdate As String, retdate As String, origin As String, destination As String
    Dim tds_perct As String
    Dim ltn As Integer = 0, ltnR As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Session("UID") = "" Or Session("UID") Is Nothing Then
                Response.Redirect("../Login.aspx")
            End If
            Dim SearchValue As String = ""
            origin = Left(Request.QueryString("hidtxtDepCity1"), 3)
            destination = Left(Request.QueryString("hidtxtArrCity1"), 3)
            SearchValue = Left(origin(1), 3) & Left(destination(1), 3) & Now.ToString
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
            SearchQuery.Add("AgentId", Session("UID"))
            If Request.QueryString("TripType") = "rdbOneWay" Then
                SearchQuery.Add("tCnt", 1)
            Else
                SearchQuery.Add("tCnt", 2)
            End If
            SearchQuery.Add("FType", "RTF")
            SearchQuery.Add("TrackId", "")
            Session("AD") = Val(SearchQuery("Adult"))
            Session("CH") = Val(SearchQuery("Child"))
            PickupDDMMYY = Split(SearchQuery("Depdate"), "/")
            DropDDMMYY = Split(SearchQuery("retDate"), "/")
            depdate = PickupDDMMYY(2) & "-" & PickupDDMMYY(1) & "-" & PickupDDMMYY(0) & "T00:00:00Z"
            retdate = DropDDMMYY(2) & "-" & DropDDMMYY(1) & "-" & DropDDMMYY(0) & "T00:00:00Z"
            iTotalNo_Adt = SearchQuery("Adult")
            iTotalNo_Chd = SearchQuery("Child")
            iTotalNo_Inf = SearchQuery("Infant")

            If Not Page.IsPostBack Then
                '********spicejet availability*********
                resDT = objSpice.getAvailabilty("D", "Return", origin, destination, depdate, retdate, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, Infant_basic, Infant_Tax, Session("UID"), "SPRING", SearchValue, SearchQuery("tCnt"), "RTF", SearchQuery("TrackId"))
                '********Indigo availability*********
                resDTIndigo = objIndigo.getAvailabilty("D", "Return", origin, destination, depdate, retdate, iTotalNo_Adt, iTotalNo_Chd, iTotalNo_Inf, Infant_basic, Infant_Tax, Session("UID"), "SPRING", SearchValue, SearchQuery("tCnt"), "RTF", SearchQuery("TrackId"))

                If resDT.Rows.Count > 0 Then
                    ''ltn = Integer.Parse(resDT.Rows(resDT.Rows.Count - 1)("LineItemNumber"))
                    ltn = Integer.Parse((resDT.Select("TripType='O'", "LineItemNumber ASC")(resDT.Select("TripType='O'", "LineItemNumber ASC").Length - 1)("LineItemNumber")))
                    ltnR = Integer.Parse((resDT.Select("TripType='R'", "LineItemNumber ASC")(resDT.Select("TripType='R'", "LineItemNumber ASC").Length - 1)("LineItemNumber")))
                Else
                    ltn = 0
                    ltnR = 0
                End If
                If resDTIndigo.Rows.Count > 0 Then
                    For i As Integer = 0 To resDTIndigo.Select("TripType='O'", "LineItemNumber ASC").Length - 1
                        resDTIndigo.Rows(i)("LineItemNumber") = (Integer.Parse(resDTIndigo.Rows(i)("LineItemNumber")) + ltn)
                    Next
                    For i As Integer = resDTIndigo.Select("TripType='O'", "LineItemNumber ASC").Length To resDTIndigo.Rows.Count - 1
                        resDTIndigo.Rows(i)("LineItemNumber") = (Integer.Parse(resDTIndigo.Rows(i)("LineItemNumber")) + ltnR)
                    Next
                    resDT.Merge(resDTIndigo, False, MissingSchemaAction.Add)
                End If

                Dim tblSource As DataTable = CType(resDT, DataTable)
                Dim j As Integer = 1
                If tblSource.Rows.Count > 0 Then


                    Try

                        tblSource.DefaultView.RowFilter = "TripType='O'"
                        Dim depDV As DataTable = tblSource.DefaultView.ToTable
                        Session("depDV") = depDV
                        LCCResO.DataSource = tblSource.DefaultView.ToTable(True, "LineItemNumber")
                        LCCResO.DataBind()
                        DepLCCHdr.Text = depDV.Rows(0)("OrgDestFrom") & " To " & depDV.Rows(0)("OrgDestTo") & " On " & depDV.Rows(0)("Departure_Date")
                        For Each myItem As RepeaterItem In LCCResO.Items
                            Dim lccArray As Array = depDV.Select("LineItemNumber='" & j.ToString & "'", "")
                            Dim fltdetails As String = ""
                            If j = 1 Then DirectCast(myItem.FindControl("deplcc1"), HtmlInputRadioButton).Checked = True
                            fltdetails = "<img src=""../images/" & (lccArray(0)("MarketingCarrier")) & ".gif"" alt="""" /><br/>"
                            If (lccArray(0)("TotalFare")).ToString <> "" Then
                                For ii As Integer = 0 To lccArray.Length - 1
                                    'fltdetails = fltdetails & (lccArray(ii)("DepartureAirport")) & " To " & (lccArray(ii)("ArrivalAirport")) & " " & (lccArray(ii)("Airline")) & " " & (lccArray(ii)("CarrierCode")) & "-" & (lccArray(ii)("FlightNumber")) & " At " & Mid((lccArray(ii)("ScheduledDepartureTime")), 12, 5) & " Hrs.<br/>"
                                    fltdetails = fltdetails & (lccArray(ii)("DepartureLocation")) & " To " & (lccArray(ii)("ArrivalLocation")) & " " & (lccArray(ii)("MarketingCarrier")) & "-" & (lccArray(ii)("FlightIdentification")) & "<br/>"
                                    fltdetails = fltdetails & Left((lccArray(ii)("DepartureTime")), 5) & " Hrs. - " & Left((lccArray(ii)("ArrivalTime")), 5) & " Hrs.<br/>"
                                    If ii = 0 Then
                                        DirectCast(myItem.FindControl("deplcc1"), HtmlInputRadioButton).Value = j.ToString
                                        DirectCast(myItem.FindControl("deplcc1"), HtmlInputRadioButton).Attributes.Add("onclick", "return checkselectedflight('LCCResOTbl','" & j.ToString & "','O');")
                                        DirectCast(myItem.FindControl("airlogoO"), Image).ImageUrl = "../images/" & (lccArray(0)("MarketingCarrier")) & ".gif"
                                        DirectCast(myItem.FindControl("airlineO"), Label).Text = (lccArray(0)("MarketingCarrier")) & " - " & (lccArray(0)("FlightIdentification"))
                                        DirectCast(myItem.FindControl("departsO"), Label).Text = Left((lccArray(0)("DepartureTime")), 5)
                                        DirectCast(myItem.FindControl("arrivesO"), Label).Text = Left((lccArray(0)("ArrivalTime")), 5)
                                        DirectCast(myItem.FindControl("prcO"), HtmlAnchor).InnerText = "Rs. " & (lccArray(0)("TotalFare")).ToString & "/-"
                                        DirectCast(myItem.FindControl("prcO"), HtmlAnchor).Attributes.Add("onmouseover", "FareDtlsLccRTF('DEP','" & j.ToString & "');")
                                        DirectCast(myItem.FindControl("prcO"), HtmlAnchor).Attributes.Add("onmouseout", "HideContent('uniquename3'); return true;")
                                        DirectCast(myItem.FindControl("faretypeO"), Label).Text = "Refundable"
                                    End If
                                    If ii = lccArray.Length - 1 Then
                                        fltdetails = fltdetails & "Rs. " & (lccArray(0)("TotalFare")) & "/-<br/>"
                                        DirectCast(myItem.FindControl("fltdO"), HtmlInputHidden).Value = fltdetails
                                        DirectCast(myItem.FindControl("depfltdtls"), HtmlAnchor).Attributes.Add("onmouseover", "ShowContent('uniquename3','" & fltdetails & "'); return true;")
                                        DirectCast(myItem.FindControl("depfltdtls"), HtmlAnchor).Attributes.Add("onmouseout", "HideContent('uniquename3'); return true;")
                                    End If
                                Next
                                If j = 1 Then
                                    fltlblO.InnerHtml = fltdetails
                                    totFareO = (lccArray(0)("TotalFare"))
                                    selecteddep.Value = j.ToString
                                End If
                            Else
                                DirectCast(myItem.FindControl("OWTR"), HtmlTableRow).Style(HtmlTextWriterStyle.Display) = "none"
                            End If

                            j += 1
                        Next
                    Catch ex As Exception
                        Response.Redirect("../Domestic/NoResult.aspx")
                    End Try

                    Try
                        tblSource.DefaultView.RowFilter = "TripType='R'"
                        Dim retDV As DataTable = tblSource.DefaultView.ToTable
                        Session("retDV") = retDV
                        LCCResR.DataSource = tblSource.DefaultView.ToTable(True, "LineItemNumber")
                        LCCResR.DataBind()
                        RetLCCHdr.Text = retDV.Rows(0)("OrgDestFrom") & " To " & retDV.Rows(0)("OrgDestTo") & " On " & retDV.Rows(0)("Departure_Date")
                        j = 1
                        For Each myItem As RepeaterItem In LCCResR.Items
                            Dim lccArray As Array = retDV.Select("LineItemNumber='" & j.ToString & "'", "")
                            Dim fltdetails As String = ""
                            If j = 1 Then DirectCast(myItem.FindControl("retlcc1"), HtmlInputRadioButton).Checked = True
                            fltdetails = "<img src=""../images/" & (lccArray(0)("MarketingCarrier")) & ".gif"" alt="""" /><br/>"
                            If (lccArray(0)("TotalFare")).ToString <> "" Then
                                For ii As Integer = 0 To lccArray.Length - 1
                                    'fltdetails = fltdetails & (lccArray(ii)("DepartureAirport")) & " To " & (lccArray(ii)("ArrivalAirport")) & " " & (lccArray(ii)("Airline")) & " " & (lccArray(ii)("CarrierCode")) & "-" & (lccArray(ii)("FlightNumber")) & " At " & Mid((lccArray(ii)("ScheduledDepartureTime")), 12, 5) & " Hrs.<br/>"
                                    fltdetails = fltdetails & (lccArray(ii)("DepartureLocation")) & " To " & (lccArray(ii)("ArrivalLocation")) & " " & (lccArray(ii)("MarketingCarrier")) & "-" & (lccArray(ii)("FlightIdentification")) & "<br/>"
                                    fltdetails = fltdetails & Left((lccArray(ii)("DepartureTime")), 5) & " Hrs. - " & Left((lccArray(ii)("ArrivalTime")), 5) & " Hrs.<br/>"
                                    If ii = 0 Then
                                        DirectCast(myItem.FindControl("retlcc1"), HtmlInputRadioButton).Value = j.ToString
                                        DirectCast(myItem.FindControl("retlcc1"), HtmlInputRadioButton).Attributes.Add("onclick", "return checkselectedflight('LCCResRTbl','" & j.ToString & "','R');")
                                        DirectCast(myItem.FindControl("airlogoR"), Image).ImageUrl = "../images/" & (lccArray(0)("MarketingCarrier")) & ".gif"
                                        DirectCast(myItem.FindControl("airlineR"), Label).Text = (lccArray(0)("MarketingCarrier")) & " - " & (lccArray(0)("FlightIdentification"))
                                        DirectCast(myItem.FindControl("departsR"), Label).Text = Left((lccArray(0)("DepartureTime")), 5)
                                        DirectCast(myItem.FindControl("arrivesR"), Label).Text = Left((lccArray(0)("ArrivalTime")), 5)
                                        DirectCast(myItem.FindControl("prcR"), HtmlAnchor).InnerText = "Rs. " & (lccArray(0)("TotalFare")).ToString & "/-"
                                        DirectCast(myItem.FindControl("prcR"), HtmlAnchor).Attributes.Add("onmouseover", "FareDtlsLccRTF('RET','" & j.ToString & "');")
                                        DirectCast(myItem.FindControl("prcR"), HtmlAnchor).Attributes.Add("onmouseout", "HideContent('uniquename3'); return true;")
                                        DirectCast(myItem.FindControl("faretypeR"), Label).Text = "Refundable"
                                    End If
                                    If ii = lccArray.Length - 1 Then
                                        fltdetails = fltdetails & "Rs. " & (lccArray(0)("TotalFare")) & "/-<br/>"
                                        DirectCast(myItem.FindControl("fltdR"), HtmlInputHidden).Value = fltdetails
                                        DirectCast(myItem.FindControl("retfltdtls"), HtmlAnchor).Attributes.Add("onmouseover", "ShowContent('uniquename3','" & fltdetails & "'); return true;")
                                        DirectCast(myItem.FindControl("retfltdtls"), HtmlAnchor).Attributes.Add("onmouseout", "HideContent('uniquename3'); return true;")
                                    End If
                                Next
                                If j = 1 Then
                                    fltlblR.InnerHtml = fltdetails
                                    totFareR = (lccArray(0)("TotalFare"))
                                    selectedret.Value = j.ToString
                                End If
                            Else
                                DirectCast(myItem.FindControl("RTTR"), HtmlTableRow).Style(HtmlTextWriterStyle.Display) = "none"
                            End If

                            j += 1
                        Next
                    Catch ex As Exception
                        Response.Redirect("../Domestic/NoResult.aspx")
                    End Try
                    totFarelbl.InnerHtml = "Rs. " & (Math.Round((totFareO + totFareR), 0)).ToString & "/-"
                    'farebrekup.Attributes.Add("onmouseover", "lccRTFFare();")
                    'farebrekup.Attributes.Add("onmouseout", "HideContent('uniquename3'); return true;")
                Else
                    Response.Redirect("../Domestic/NoResult.aspx")
                End If
            End If
            Ajax.Utility.RegisterTypeForAjax(GetType(LccRF_LccRResult))
        Catch ex As Exception
            Response.Redirect("../Domestic/NoResult.aspx")
        End Try
    End Sub

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
    Public Function LccTotalFare(ByVal selectedFltO As String, ByVal selectedFltR As String) As Integer
        Dim depDV As DataTable = Session("depDV")
        Dim retDV As DataTable = Session("retDV")
        Try
            Dim lccArrayO As Array = depDV.Select("LineItemNumber='" & selectedFltO & "'", "")
            Dim lccArrayR As Array = retDV.Select("LineItemNumber='" & selectedFltR & "'", "")
            totFareO = (lccArrayO(0)("TotalFare"))
            totFareR = (lccArrayR(0)("TotalFare"))
            totFareR = Math.Round((totFareO + totFareR), 0)
        Catch ex As Exception
            totFareR = 0
        End Try
        Return totFareR
    End Function

    <Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)> _
     Public Function IsBookable(ByVal selectedFltO As String, ByVal selectedFltR As String) As Integer
        Dim depDV As DataTable = Session("depDV")
        Dim retDV As DataTable = Session("retDV")
        Dim depAirline As String = ""
        Dim retAirline As String = ""
        Dim retType As Integer = 0
        Try
            Dim lccArrayO As Array = depDV.Select("LineItemNumber='" & selectedFltO & "'", "")
            Dim lccArrayR As Array = retDV.Select("LineItemNumber='" & selectedFltR & "'", "")
            depAirline = (lccArrayO(0)("MarketingCarrier"))
            retAirline = (lccArrayR(0)("MarketingCarrier"))
            If depAirline.Trim = retAirline.Trim Then
                retType = 1
            Else
                retType = 0
            End If
        Catch ex As Exception
            retType = 0
        End Try
        Return retType
    End Function
End Class
