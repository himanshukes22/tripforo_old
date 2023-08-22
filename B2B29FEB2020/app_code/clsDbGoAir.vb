Imports Microsoft.VisualBasic
Imports System.Data
Class clsDbGoAir
    Dim dtTemp As DataTable
    Public Function CreateDataTable() As DataTable
        dtTemp = New DataTable("tblFareQuoteResponse")
        Dim Flt_cnt As DataColumn = New DataColumn("Flt_cnt")
        Flt_cnt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Flt_cnt)
        Dim Currency As DataColumn = New DataColumn("Currency")
        Currency.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Currency)
        Dim DepDate As DataColumn = New DataColumn("DepDate")
        DepDate.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(DepDate)
        Dim FareID_Adt As DataColumn = New DataColumn("FareID_Adt")
        FareID_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Adt)
        Dim FareID_Chd As DataColumn = New DataColumn("FareID_Chd")
        FareID_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Chd)
        Dim FareID_Inf As DataColumn = New DataColumn("FareID_Inf")
        FareID_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Inf)
        Dim FareTypeName As DataColumn = New DataColumn("FareTypeName")
        FareTypeName.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareTypeName)
        Dim FCCode_Adt As DataColumn = New DataColumn("FCCode_Adt")
        FCCode_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FCCode_Adt)
        Dim FBCode_Adt As DataColumn = New DataColumn("FBCode_Adt")
        FBCode_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FBCode_Adt)
        Dim FCCode_Chd As DataColumn = New DataColumn("FCCode_Chd")
        FCCode_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FCCode_Chd)
        Dim FBCode_Chd As DataColumn = New DataColumn("FBCode_Chd")
        FBCode_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FBCode_Chd)
        Dim FCCode_Inf As DataColumn = New DataColumn("FCCode_Inf")
        FCCode_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FCCode_Inf)
        Dim FBCode_Inf As DataColumn = New DataColumn("FBCode_Inf")
        FBCode_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FBCode_Inf)
        Dim BaseFareAmt_Adt As DataColumn = New DataColumn("BaseFareAmt_Adt")
        BaseFareAmt_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BaseFareAmt_Adt)
        Dim Total_Adt As DataColumn = New DataColumn("Total_Adt")
        Total_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Adt)
        Dim BaseFareAmt_Chd As DataColumn = New DataColumn("BaseFareAmt_Chd")
        BaseFareAmt_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BaseFareAmt_Chd)
        Dim Total_Chd As DataColumn = New DataColumn("Total_Chd")
        Total_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Chd)
        Dim BaseFareAmt_Inf As DataColumn = New DataColumn("BaseFareAmt_Inf")
        BaseFareAmt_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BaseFareAmt_Inf)
        Dim Total_Inf As DataColumn = New DataColumn("Total_Inf")
        Total_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Inf)
        Dim Total_Fare As DataColumn = New DataColumn("Total_Fare")
        Total_Fare.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Fare)
        Dim Total_Tax As DataColumn = New DataColumn("Total_Tax")
        Total_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Tax)
        Dim Cabin As DataColumn = New DataColumn("Cabin")
        Cabin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Cabin)
        Dim SeatAvail As DataColumn = New DataColumn("SeatAvail")
        SeatAvail.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(SeatAvail)
        Dim PFID As DataColumn = New DataColumn("PFID")
        PFID.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PFID)
        Dim Origin As DataColumn = New DataColumn("Origin")
        Origin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Origin)
        Dim Destination As DataColumn = New DataColumn("Destination")
        Destination.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Destination)
        Dim CarrierCode As DataColumn = New DataColumn("CarrierCode")
        CarrierCode.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(CarrierCode)
        Dim ArrivalDate As DataColumn = New DataColumn("ArrivalDate")
        ArrivalDate.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ArrivalDate)
        Dim Stops As DataColumn = New DataColumn("Stops")
        Stops.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Stops)
        Dim FlightTime As DataColumn = New DataColumn("FlightTime")
        FlightTime.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FlightTime)
        Dim FlightNo As DataColumn = New DataColumn("FlightNo")
        FlightNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FlightNo)
        Dim FUEL_Adt As DataColumn = New DataColumn("FUEL_Adt")
        FUEL_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FUEL_Adt)
        Dim FUEL_Chd As DataColumn = New DataColumn("FUEL_Chd")
        FUEL_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FUEL_Chd)
        Dim FUEL_Inf As DataColumn = New DataColumn("FUEL_Inf")
        FUEL_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FUEL_Inf)
        Dim Chd_Tax As DataColumn = New DataColumn("Chd_Tax")
        Chd_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Chd_Tax)
        Dim Adt_Tax As DataColumn = New DataColumn("Adt_Tax")
        Adt_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Adt_Tax)
        Dim Inf_Tax As DataColumn = New DataColumn("Inf_Tax")
        Inf_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Inf_Tax)
        Dim TotalPay As DataColumn = New DataColumn("TotalPay")
        TotalPay.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(TotalPay)

        Dim Leg As DataColumn = New DataColumn("Leg")
        TotalPay.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Leg)
        CreateDataTable = dtTemp
    End Function
    Public Function CreateMainDataTable() As DataTable
        dtTemp = New DataTable("tblFareQuoteResponse_Main")
        Dim Mkt_Carrier As DataColumn = New DataColumn("Mkt_Carrier")
        Mkt_Carrier.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Mkt_Carrier)
        Dim FareTypeName As DataColumn = New DataColumn("FareTypeName")
        FareTypeName.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareTypeName)
        Dim Flight_No As DataColumn = New DataColumn("Flight_No")
        Flight_No.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Flight_No)
        Dim Origin As DataColumn = New DataColumn("Origin")
        Origin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Origin)
        Dim Destination As DataColumn = New DataColumn("Destination")
        Destination.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Destination)
        Dim Dept_Date As DataColumn = New DataColumn("Dept_Date")
        Dept_Date.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Dept_Date)
        Dim Dept_Time As DataColumn = New DataColumn("Dept_Time")
        Dept_Time.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Dept_Time)
        Dim Arr_Date As DataColumn = New DataColumn("Arr_Date")
        Arr_Date.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Arr_Date)
        Dim Arr_Time As DataColumn = New DataColumn("Arr_Time")
        Arr_Time.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Arr_Time)
        Dim Total_Fare As DataColumn = New DataColumn("Total_Fare")
        Total_Fare.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Fare)
        Dim Total_Tax As DataColumn = New DataColumn("Total_Tax")
        Total_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Tax)
        Dim Adt_Basic As DataColumn = New DataColumn("Adt_Basic")
        Adt_Basic.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Adt_Basic)
        Dim Adt_Tax As DataColumn = New DataColumn("Adt_Tax")
        Adt_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Adt_Tax)
        Dim Adt_Sur As DataColumn = New DataColumn("Adt_Sur")
        Adt_Sur.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Adt_Sur)
        Dim Adt_total As DataColumn = New DataColumn("Adt_total")
        Adt_total.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Adt_total)
        Dim Chd_Basic As DataColumn = New DataColumn("Chd_Basic")
        Chd_Basic.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Chd_Basic)
        Dim Chd_Tax As DataColumn = New DataColumn("Chd_Tax")
        Chd_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Chd_Tax)
        Dim Chd_Sur As DataColumn = New DataColumn("Chd_Sur")
        Chd_Sur.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Chd_Sur)
        Dim Chd_Total As DataColumn = New DataColumn("Chd_Total")
        Chd_Total.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Chd_Total)
        Dim Inf_Basic As DataColumn = New DataColumn("Inf_Basic")
        Inf_Basic.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Inf_Basic)
        Dim Inf_Tax As DataColumn = New DataColumn("Inf_Tax")
        Inf_Tax.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Inf_Tax)
        Dim Inf_Sur As DataColumn = New DataColumn("Inf_Sur")
        Inf_Sur.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Inf_Sur)
        Dim Inf_Total As DataColumn = New DataColumn("Inf_Total")
        Inf_Total.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Inf_Total)
        Dim FareID_Adt As DataColumn = New DataColumn("FareID_Adt")
        FareID_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Adt)
        Dim FareID_Chd As DataColumn = New DataColumn("FareID_Chd")
        FareID_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Chd)
        Dim FareID_Inf As DataColumn = New DataColumn("FareID_Inf")
        FareID_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareID_Inf)
        Dim ClassC_Adt As DataColumn = New DataColumn("ClassC_Adt")
        ClassC_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ClassC_Adt)
        Dim ClassC_Chd As DataColumn = New DataColumn("ClassC_Chd")
        ClassC_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ClassC_Chd)
        Dim ClassC_Inf As DataColumn = New DataColumn("ClassC_Inf")
        ClassC_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ClassC_Inf)
        Dim Fare_Basis_Adt As DataColumn = New DataColumn("Fare_Basis_Adt")
        Fare_Basis_Adt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Fare_Basis_Adt)
        Dim Fare_Basis_Chd As DataColumn = New DataColumn("Fare_Basis_Chd")
        Fare_Basis_Chd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Fare_Basis_Chd)
        Dim Fare_Basis_Inf As DataColumn = New DataColumn("Fare_Basis_Inf")
        Fare_Basis_Inf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Fare_Basis_Inf)
        Dim Flight_Duration As DataColumn = New DataColumn("Flight_Duration")
        Flight_Duration.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Flight_Duration)
        Dim Stops As DataColumn = New DataColumn("Stops")
        Stops.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Stops)
        Dim flt_cnt As DataColumn = New DataColumn("flt_cnt")
        flt_cnt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(flt_cnt)
        Dim Cabin As DataColumn = New DataColumn("Cabin")
        Cabin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Cabin)
        Dim SeatAvail As DataColumn = New DataColumn("SeatAvail")
        SeatAvail.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(SeatAvail)
        Dim sNoOfAdt As DataColumn = New DataColumn("sNoOfAdt")
        sNoOfAdt.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(sNoOfAdt)
        Dim sNoOfChd As DataColumn = New DataColumn("sNoOfChd")
        sNoOfChd.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(sNoOfChd)
        Dim sNoOfInf As DataColumn = New DataColumn("sNoOfInf")
        sNoOfInf.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(sNoOfInf)
        Dim TotalPay As DataColumn = New DataColumn("TotalPay")
        TotalPay.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(TotalPay)
        CreateMainDataTable = dtTemp
    End Function
    Public Function CreateSummeryDataTable() As DataTable
        dtTemp = New DataTable("tblSummery")
        Dim SerialNo As DataColumn = New DataColumn("SerialNo")
        SerialNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(SerialNo)
        Dim ConfirmationNo As DataColumn = New DataColumn("ConfirmationNo")
        ConfirmationNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ConfirmationNo)
        Dim PromotionalID As DataColumn = New DataColumn("PromotionalID")
        PromotionalID.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PromotionalID)
        Dim RecieptLanguageID As DataColumn = New DataColumn("RecieptLanguageID")
        RecieptLanguageID.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(RecieptLanguageID)
        Dim ProfileID As DataColumn = New DataColumn("ProfileID")
        ProfileID.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ProfileID)
        Dim FareAmount As DataColumn = New DataColumn("FareAmount")
        FareAmount.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(FareAmount)
        Dim VoucherNo As DataColumn = New DataColumn("VoucherNo")
        VoucherNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(VoucherNo)
        CreateSummeryDataTable = dtTemp
    End Function
    Public Function CreateSaveDataTable() As DataTable
        dtTemp = New DataTable("tblSave")
        Dim SerialNo As DataColumn = New DataColumn("SerialNo")
        SerialNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(SerialNo)
        Dim ConfirmationNo As DataColumn = New DataColumn("ConfirmationNo")
        ConfirmationNo.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ConfirmationNo)
        Dim BookingAgent As DataColumn = New DataColumn("BookingAgent")
        BookingAgent.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BookingAgent)
        Dim BookDate As DataColumn = New DataColumn("BookDate")
        BookDate.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BookDate)
        Dim PassengerTitle As DataColumn = New DataColumn("PassengerTitle")
        PassengerTitle.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PassengerTitle)
        Dim PassengerFName As DataColumn = New DataColumn("PassengerFName")
        PassengerFName.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PassengerFName)
        Dim PassengerLName As DataColumn = New DataColumn("PassengerLName")
        PassengerLName.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PassengerLName)
        Dim PassengerAge As DataColumn = New DataColumn("PassengerAge")
        PassengerAge.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(PassengerAge)
        Dim Cabin As DataColumn = New DataColumn("Cabin")
        Cabin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Cabin)
        Dim Origin As DataColumn = New DataColumn("Origin")
        Origin.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Origin)
        Dim Destination As DataColumn = New DataColumn("Destination")
        Destination.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Destination)
        Dim Dept_Date As DataColumn = New DataColumn("Dept_Date")
        Dept_Date.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Dept_Date)
        Dim Dept_Time As DataColumn = New DataColumn("Dept_Time")
        Dept_Time.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Dept_Time)
        Dim Arr_Date As DataColumn = New DataColumn("Arr_Date")
        Arr_Date.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Arr_Date)
        Dim Arr_Time As DataColumn = New DataColumn("Arr_Time")
        Arr_Time.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Arr_Time)
        Dim ReservationBalance As DataColumn = New DataColumn("ReservationBalance")
        ReservationBalance.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ReservationBalance)
        Dim Total_Fare As DataColumn = New DataColumn("Total_Fare")
        Total_Fare.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Total_Fare)
        Dim Fare_ID As DataColumn = New DataColumn("Fare_ID")
        Fare_ID.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Fare_ID)
        Dim Message As DataColumn = New DataColumn("Message")
        Message.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(Message)

        Dim BookReq As DataColumn = New DataColumn("BookReq")
        BookReq.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BookReq)

        Dim BookRes As DataColumn = New DataColumn("BookRes")
        BookRes.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(BookRes)

        Dim AddPayReq As DataColumn = New DataColumn("AddPayReq")
        AddPayReq.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(AddPayReq)

        Dim AddPayRes As DataColumn = New DataColumn("AddPayRes")
        AddPayRes.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(AddPayRes)

        Dim ConfirmPayRes As DataColumn = New DataColumn("ConfirmPayRes")
        ConfirmPayRes.DataType = System.Type.GetType("System.String")
        dtTemp.Columns.Add(ConfirmPayRes)

        CreateSaveDataTable = dtTemp


    End Function
    Public Function ResultTable() As DataTable
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
            AirDataColumn.DataType = Type.GetType("System.Double")
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
