Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class clsDbSpiceJet
    Public Function CreateDataTable() As DataTable
        Dim dtTemp As New DataTable
        Dim DataColumn As DataColumn

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TransID"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TrackId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Origin"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Destination"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepDate"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalDateTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "BaseAmt"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "PassTypeCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FareGroup"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ToSell"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Cabin"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FBCode"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Stops"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AircraftType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "CarrierCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FlightNumber"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AircraftCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ServiceType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Capacity"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepartureAirport"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalAirport"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FlightDesignator"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ScheduledDepartureTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ScheduledArrivalTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Lid"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Sold"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "LegCnt"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TripType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Leg"
        dtTemp.Columns.Add(DataColumn)

        Return dtTemp
    End Function
    Public Function CreateFQTable() As DataTable
        Dim dtTemp As New DataTable
        Dim DataColumn As DataColumn

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TOurType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Origin"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Destination"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepDate"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalDateTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "PassTypeCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FBCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "CarrierCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FlightNumber"
        dtTemp.Columns.Add(DataColumn)
        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalAmt_Adt"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "BaseAmt_Adt"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TaxAmt_Adt"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "AdtFSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalAmt_Chd"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "BaseAmt_Chd"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TaxAmt_Chd"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "ChdFSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalAmt_Inf"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "BaseAmt_Inf"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TaxAmt_Inf"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "InfFSur"
        dtTemp.Columns.Add(DataColumn)

        Return dtTemp
    End Function
    Public Function CreateAvailTable() As DataTable
        Dim dtTemp As New DataTable
        Dim DataColumn As DataColumn

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "CarrierCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Airline"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FlightNumber"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Origin"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Destination"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepartureTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepartureDate"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalDate"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FareBasis"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "FareGroup"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "Adult"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "Child"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "Infant"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TotalBaseFare"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalTax"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalFare"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotalFuelSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "AdtBasic"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "AdtTax"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "AdtTotal"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "AdtFSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "ChdBasic"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "ChdTax"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "ChdTotal"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "ChdFSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "InfBasic"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "InfTax"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "InfTotal"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "InfFSur"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AvailableSeats"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Stops"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AircraftType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AircraftCode"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Class"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Capacity"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ScheduledDepartureTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ScheduledArrivalTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ArrivalAirport"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "DepartureAirport"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Lid"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Sold"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TripType"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TrackId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "LineItemNumber"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "SrvTax"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TrnFee"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotDis"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "Dis"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "CB"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotAdtMrk"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "TotChdMrk"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "admin_mrk"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "agent_mrk"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.Double")
        DataColumn.ColumnName = "OriginalTF"
        dtTemp.Columns.Add(DataColumn)

        Return dtTemp
    End Function
    Public Function CreateBookingDT() As DataTable
        Dim dtTemp As New DataTable
        Dim DataColumn As DataColumn

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TransID"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "SessionId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "TraceId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "CreateDateTime"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "Status"
        dtTemp.Columns.Add(DataColumn)


        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "PNRId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "AltPNRId"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "CreateUser"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ReqXml"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "ResXml"
        dtTemp.Columns.Add(DataColumn)

        DataColumn = New DataColumn()
        DataColumn.DataType = Type.GetType("System.String")
        DataColumn.ColumnName = "BKGStatus"
        dtTemp.Columns.Add(DataColumn)

        Return dtTemp
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
