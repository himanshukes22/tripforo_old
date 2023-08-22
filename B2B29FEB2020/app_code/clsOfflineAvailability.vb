Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class clsOfflineAvailability
    Dim OffLineDs As DataSet
    Dim objSql As New SqlTransactionNew
    Dim Conn As New SqlConnection
    Dim dtTemp As New DataTable
    Public Sub New(ByVal strCon As String)
        Conn.ConnectionString = strCon
    End Sub
    Public Function OfflineAvailability(ByVal Origin As String, ByVal Destination As String, ByVal Depdate As String, ByVal Airline As String, ByVal Trip As String, ByVal Adult As Integer, ByVal Child As Integer, ByVal Infant As Integer, ByVal AgentId As String, ByVal Distr As String, ByVal TourType As String, ByVal TCnt As Integer, ByVal FT As String, ByVal TrackId As String) As DataTable

        Try
            OffLineDs = objSql.GetCharteredFlightDetails(Origin, Destination, Depdate, Airline, Trip)
            If OffLineDs.Tables.Count > 0 Then
                If OffLineDs.Tables(0).Rows.Count > 0 Then

                    Dim row As DataRow
                    Conn.Open()
                    dtTemp = ResultTable()
                    'Calculation For AgentMarkUp'
                    Dim dtAgentMarkup As New DataTable
                    dtAgentMarkup = GetMarkUp(AgentId, Distr, Trip, "TA")
                    'Calculation For AdminMarkUp'
                    Dim dtAdminMarkup As New DataTable
                    dtAdminMarkup = GetMarkUp(AgentId, Distr, Trip, "AD")
                    For iCtr As Integer = 0 To OffLineDs.Tables(0).Rows.Count - 1
                        row = dtTemp.NewRow()

                        '//new entry
                        row("TripType") = "O"
                        row("OrgDestFrom") = Origin
                        row("OrgDestTo") = Destination
                        row("Sector") = Origin & ":" & Destination
                        row("DepartureLocation") = OffLineDs.Tables(0).Rows(iCtr).Item("DepartureLocation").ToString.Trim
                        row("ArrivalLocation") = OffLineDs.Tables(0).Rows(iCtr).Item("ArrivalLocation").ToString.Trim
                        row("DepartureCityName") = OffLineDs.Tables(0).Rows(iCtr).Item("DepartureCityName").ToString.Trim
                        row("ArrivalCityName") = OffLineDs.Tables(0).Rows(iCtr).Item("ArrivalCityName").ToString.Trim
                        row("depdatelcc") = ""
                        row("arrdatelcc") = ""
                        row("DepartureDate") = OffLineDs.Tables(0).Rows(iCtr).Item("DepartureDate").ToString.Trim
                        row("Departure_Date") = Left(row("DepartureDate"), 2) & " " & datecon(Mid(row("DepartureDate"), 3, 2))
                        row("DepartureTime") = OffLineDs.Tables(0).Rows(iCtr).Item("DepartureTime").ToString.Trim
                        row("ArrivalDate") = OffLineDs.Tables(0).Rows(iCtr).Item("ArrivalDate").ToString.Trim
                        row("Arrival_Date") = Left(row("ArrivalDate"), 2) & " " & datecon(Mid(row("ArrivalDate"), 3, 2))
                        row("ArrivalTime") = OffLineDs.Tables(0).Rows(iCtr).Item("ArrivalTime").ToString.Trim
                        row("MarketingCarrier") = OffLineDs.Tables(0).Rows(iCtr).Item("MarketingCarrier").ToString.Trim
                        row("AirLineName") = OffLineDs.Tables(0).Rows(iCtr).Item("AirlineName").ToString.Trim
                        row("FlightIdentification") = OffLineDs.Tables(0).Rows(iCtr).Item("FlightNo").ToString
                        row("RBD") = OffLineDs.Tables(0).Rows(iCtr).Item("RBD").ToString
                        row("AvailableSeats") = "0"
                        row("ValiDatingCarrier") = OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim
                        row("EQ") = ""

                        row("Stops") = OffLineDs.Tables(0).Rows(iCtr).Item("Stops").ToString & "-Stop"
                        row("fareBasis") = ""
                        row("FBPaxType") = ""
                        row("LineItemNumber") = iCtr + 1
                        row("Searchvalue") = ""
                        row("TotPax") = Adult + Child
                        row("Adult") = Adult
                        row("Child") = Child
                        row("Infant") = Infant
                        row("Leg") = iCtr + 1
                        row("Flight") = "1"
                        row("Tot_Dur") = ""
                        row("Trip") = TourType.ToString
                        row("TripCnt") = TCnt
                        row("Currency") = "INR"
                        row("CS") = "Rs."
                        row("sno") = "OFFLINE"


                        'Calculation of Transaction details
                        Dim totBFWInf As Double = 0
                        Dim totBFWOInf As Double = 0
                        Dim totFS As Double = 0
                        Dim totTax As Double = 0
                        Dim ADTAgentMrk As Double = 0, CHDAgentMrk As Double = 0, ADTAdminMrk As Double = 0, CHDAdminMrk As Double = 0
                        Dim AStax As Integer = 0, CStax As Integer = 0, IStax As Integer = 0
                        Dim ATF As Integer = 0, CTF As Integer = 0, ITF As Integer = 0

                        totBFWInf = (OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare") * Adult) + (OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare") * Child) '+ (OffLineDs.Tables(0).Rows(iCtr).Item("BaseFareAmt_Inf") * dTotalNo_inf)
                        totBFWOInf = totBFWInf '(OffLineDs.Tables(0).Rows(iCtr).Item("BaseFareAmt_Adt") * dTotalNo_Adt) + (OffLineDs.Tables(0).Rows(iCtr).Item("BaseFareAmt_Chd") * dTotalNo_Chd)
                        totFS = (OffLineDs.Tables(0).Rows(iCtr).Item("YQ") * Adult) + (OffLineDs.Tables(0).Rows(iCtr).Item("YQ") * Child)
                        totTax = (OffLineDs.Tables(0).Rows(iCtr).Item("OT") * Adult) + (OffLineDs.Tables(0).Rows(iCtr).Item("OT") * Child) ' + (OffLineDs.Tables(0).Rows(iCtr).Item("Inf_Tax") * Infant)
                        'Dim HsTblSTax As Hashtable = ServiceTax(OffLineDs.Tables(0).Rows(iCtr).Item("CarrierCode").ToString.Trim, totBFWInf, totBFWOInf, totFS, Trip)
                        Dim HsTblSTax As Hashtable = ServiceTax(OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare"), OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare"), OffLineDs.Tables(0).Rows(iCtr).Item("YQ"), Trip)
                        AStax = HsTblSTax("STax") * Adult
                        ATF = HsTblSTax("TF") * Adult
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare"), OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare"), OffLineDs.Tables(0).Rows(iCtr).Item("YQ"), Trip)
                        CStax = HsTblSTax("STax") * Child
                        CTF = HsTblSTax("TF") * Child
                        HsTblSTax.Clear()
                        HsTblSTax = ServiceTax(OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, 0, 0, 0, Trip)
                        IStax = HsTblSTax("STax") * Infant
                        ITF = HsTblSTax("TF") * Infant




                        ADTAgentMrk = CalcMarkup(dtAgentMarkup, OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare"), Trip)
                        If Child > 0 Then CHDAgentMrk = CalcMarkup(dtAgentMarkup, OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare"), Trip) Else CHDAgentMrk = 0
                        ADTAdminMrk = CalcMarkup(dtAdminMarkup, OffLineDs.Tables(0).Rows(iCtr).Item("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare"), Trip)
                        If Child > 0 Then CHDAdminMrk = CalcMarkup(dtAdminMarkup, OffLineDs.Tables(0).Rows(iCtr)("ValiDatingCarrier").ToString.Trim, OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare"), Trip) Else CHDAgentMrk = 0
                        Dim totMrk As Double = 0
                        totMrk = ADTAdminMrk * Adult
                        totMrk = totMrk + ADTAgentMrk * Adult
                        totMrk = totMrk + CHDAdminMrk * Child
                        totMrk = totMrk + CHDAgentMrk * Child
                        'Calculation of Transaction details end


                        row("AdtBfare") = OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare")
                        row("AdtFSur") = OffLineDs.Tables(0).Rows(iCtr).Item("YQ")
                        row("AdtTax") = OffLineDs.Tables(0).Rows(iCtr).Item("OT")
                        row("AdtFare") = OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare")
                        row("ChdBfare") = OffLineDs.Tables(0).Rows(iCtr).Item("BaseFare")
                        row("ChdFSur") = OffLineDs.Tables(0).Rows(iCtr).Item("YQ")
                        row("ChdTax") = OffLineDs.Tables(0).Rows(iCtr).Item("OT")
                        row("ChdFare") = OffLineDs.Tables(0).Rows(iCtr).Item("TotalFare")
                        row("InfBfare") = 0 'OffLineDs.Tables(0).Rows(iCtr).Item("BaseFareAmt_Inf")
                        row("InfFSur") = 0 'OffLineDs.Tables(0).Rows(iCtr).Item("FUEL_Inf")
                        row("InfTax") = 0 'OffLineDs.Tables(0).Rows(iCtr).Item("Inf_Tax")
                        row("InfFare") = 0 'OffLineDs.Tables(0).Rows(iCtr).Item("Total_Inf")
                        row("STax") = AStax + CStax + IStax 'HsTblSTax("STax")
                        row("TFee") = ATF + CTF + ITF 'HsTblSTax("TF")
                        row("IATAComm") = HsTblSTax("IATAComm")
                        row("ADTAgentMrk") = ADTAgentMrk
                        row("CHDAgentMrk") = CHDAgentMrk
                        row("ADTAdminMrk") = ADTAdminMrk
                        row("CHDAdminMrk") = CHDAdminMrk
                        row("TotalBfare") = totBFWInf
                        row("TotalFuelSur") = (OffLineDs.Tables(0).Rows(iCtr).Item("YQ") * Adult) + (OffLineDs.Tables(0).Rows(iCtr).Item("YQ") * Child) '+ (OffLineDs.Tables(0).Rows(iCtr).Item("FUEL_Inf") * dTotalNo_inf)
                        row("TotalTax") = totTax
                        row("TotalFare") = totBFWInf + totTax + row("STax") + row("TFee") + totMrk
                        row("DisCount") = "0"
                        row("OriginalTF") = row("TotalFare") 'OffLineDs.Tables(0).Rows(iCtr).Item("TotalPay") 'totBFWInf + totTax
                        row("OriginalTT") = totTax
                        row("Track_id") = TrackId
                        row("FType") = FT
                        '//
                        'row.Item("Cabin") = OffLineDs.Tables(0).Rows(iCtr).Item("Cabin")
                        dtTemp.Rows.Add(row)
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
        Return dtTemp
    End Function

    Private Function ServiceTax(ByVal VC As String, ByVal TotBFWI As Double, ByVal TotBFWOI As Double, ByVal FS As Double, ByVal Trip As String) As Hashtable
        Dim dtTax As New DataTable
        Dim AirlineCharges As New Hashtable
        Dim sqlcom As New SqlCommand
        Dim SqlQuery = "ServiceCharge"
        sqlcom = New SqlCommand(SqlQuery, Conn)
        sqlcom.Parameters.Add("@vc", SqlDbType.VarChar).Value = VC
        sqlcom.Parameters.Add("@trip", SqlDbType.VarChar).Value = Trip
        sqlcom.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter(sqlcom)
        da.Fill(dtTax)

        Try
            If dtTax.Rows.Count > 0 Then
                AirlineCharges.Add("STax", 0) 'Math.Round(((TotBFWI * dtTax.Rows(0)("SrvTax")) / 100), 0)
                AirlineCharges.Add("TF", Math.Round((((TotBFWOI + FS) * dtTax.Rows(0)("TranFee")) / 100), 0))
                AirlineCharges.Add("IATAComm", dtTax.Rows(0)("IATAComm"))
            Else
                AirlineCharges.Add("STax", 0)
                ' AirlineCharges.Add("STaxP", 0)
                AirlineCharges.Add("TF", 0)
                AirlineCharges.Add("IATAComm", 0)
            End If
        Catch ex As Exception
            AirlineCharges.Add("STax", 0)
            'AirlineCharges.Add("STaxP", 0)
            AirlineCharges.Add("TF", 0)
            AirlineCharges.Add("IATAComm", 0)
        End Try
        Return AirlineCharges
    End Function

    Private Function GetMarkUp(ByVal AgentID As String, ByVal distrubid As String, ByVal Trip As String, ByVal typeId As String) As DataTable
        Dim dt As New DataTable
        Dim sqlcom As New SqlCommand
        Dim SqlQuery = "GetMarkup"
        sqlcom = New SqlCommand(SqlQuery, Conn)
        sqlcom.Parameters.Add("@trip", SqlDbType.VarChar).Value = Trip
        sqlcom.Parameters.Add("@agid", SqlDbType.VarChar).Value = AgentID
        sqlcom.Parameters.Add("@distrid", SqlDbType.VarChar).Value = distrubid
        sqlcom.Parameters.Add("@idtype", SqlDbType.VarChar).Value = typeId
        sqlcom.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter(sqlcom)
        da.Fill(dt)
        Return dt
    End Function

    Private Function CalcMarkup(ByVal Mrkdt As DataTable, ByVal VC As String, ByVal fare As Double, ByVal Trip As String) As Double
        Dim airMrkArray As Array
        Dim mrkamt As Double = 0
        Try
            airMrkArray = Mrkdt.Select("AirlineCode='" & VC & "'", "")
            If airMrkArray.Length > 0 Then
                If Trip = "I" Then
                    If (airMrkArray(0))("MarkupType") = "P" Then
                        mrkamt = Math.Round((fare * (airMrkArray(0))("MarkupValue")) / 100, 0)
                    ElseIf (airMrkArray(0))("MarkupType") = "F" Then
                        mrkamt = (airMrkArray(0))("MarkupValue")
                    End If
                Else
                    mrkamt = (airMrkArray(0))("MarkUp")
                End If
            Else
                mrkamt = 0
            End If
        Catch ex As Exception
            mrkamt = 0
        End Try
        Return mrkamt
    End Function
    Private Function datecon(ByVal MM As String) As String
        Dim mm_str As String
        Select Case MM
            Case "01"
                mm_str = "JAN"
            Case "02"
                mm_str = "FEB"
            Case "03"
                mm_str = "MAR"
            Case "04"
                mm_str = "APR"
            Case "05"
                mm_str = "MAY"
            Case "06"
                mm_str = "JUN"
            Case "07"
                mm_str = "JUL"
            Case "08"
                mm_str = "AUG"
            Case "09"
                mm_str = "SEP"
            Case "10"
                mm_str = "OCT"
            Case "11"
                mm_str = "NOV"
            Case "12"
                mm_str = "DEC"
            Case Else
        End Select
        Return mm_str
    End Function
    Private Function ResultTable() As DataTable
        Dim AirDataTable As New DataTable
        Try

            Dim AirDataColumn As DataColumn

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "OrgDestFrom"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "OrgDestTo"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "Adult"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "Child"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "Infant"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotPax"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "DepartureLocation"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "DepartureCityName"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "ArrivalLocation"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "ArrivalCityName"
            AirDataTable.Columns.Add(AirDataColumn)


            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "DepartureDate"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Departure_Date"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "DepartureTime"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "ArrivalDate"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Arrival_Date"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "ArrivalTime"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "MarketingCarrier"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "FlightIdentification"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "RBD"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "AvailableSeats"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "ValiDatingCarrier"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtFare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtBfare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtTax"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdFare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdBfare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdTax"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "InfFare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "infBfare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "InfTax"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotalBfare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotalFare"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotalTax"
            AirDataTable.Columns.Add(AirDataColumn)



            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "LineItemNumber"
            AirDataTable.Columns.Add(AirDataColumn)


            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "STax"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TFee"
            AirDataTable.Columns.Add(AirDataColumn)


            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "DisCount"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Searchvalue"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Leg"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Flight"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Tot_Dur"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "TripType"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "EQ"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Stops"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "AirLineName"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Trip"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Sector"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "TripCnt"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Currency"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "CS"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ADTAdminMrk"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ADTAgentMrk"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "CHDAdminMrk"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "CHDAgentMrk"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "IATAComm"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "fareBasis"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "FBPaxType"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtFSur"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdFSur"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "InfFSur"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotalFuelSur"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "sno"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "depdatelcc"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "arrdatelcc"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "OriginalTF"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "OriginalTT"
            AirDataTable.Columns.Add(AirDataColumn)


            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Track_id"
            AirDataTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn()
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "FType"
            AirDataTable.Columns.Add(AirDataColumn)
        Catch ex As Exception

        End Try

        Return AirDataTable

    End Function
End Class
