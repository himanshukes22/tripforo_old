Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsInsertSelectedFlight
    Public Sub InsertFlightData(ByVal track_id As String, ByVal lineno As String, ByVal FlightDataTable As DataTable, ByVal strFile As String, ByVal Adt_Tax As String, ByVal Chd_Tax As String, ByVal Inf_Tax As String, ByVal SrvTax As Double, ByVal TF As Double, ByVal TC As Double, ByVal AdtTds As Double, ByVal ChdTds As Double, ByVal AdtComm As Double, ByVal ChdComm As Double, ByVal AdtCb As Integer, ByVal ChdCb As Integer, ByVal totFare As Double, ByVal netFare As Double, ByVal userid As String)
        Try
            Dim AirDataColumn As DataColumn
            Dim rows As DataRow() = FlightDataTable.Select("LineItemNumber='" & lineno & "'")
            Dim tempTable As DataTable
            tempTable = FlightDataTable.Clone
            tempTable.Clear() 
            For Each thisRow In rows
                Dim dtRow As DataRow = tempTable.NewRow()
                dtRow = thisRow
                tempTable.ImportRow(dtRow)
            Next
            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "FlightStatus"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Adt_Tax"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Chd_Tax"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "Inf_Tax"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "SrvTax"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TF"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TC"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtTds"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdTds"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtComm"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdComm"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtCB"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdCB"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "totFare"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "netFare"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "User_id"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.String")
            AirDataColumn.ColumnName = "counter"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Boolean")
            AirDataColumn.ColumnName = "IsCorp"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "AdtMgtFee"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "ChdMgtFee"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "InfMgtFee"
            tempTable.Columns.Add(AirDataColumn)

            AirDataColumn = New DataColumn
            AirDataColumn.DataType = Type.GetType("System.Double")
            AirDataColumn.ColumnName = "TotMgtFee"
            tempTable.Columns.Add(AirDataColumn)

            For i = 0 To tempTable.Rows.Count - 1

                tempTable.Rows(i).Item("Track_id") = track_id
                tempTable.Rows(i).Item("FlightStatus") = "OnRequest"
                tempTable.Rows(i).Item("Adt_Tax") = Adt_Tax
                tempTable.Rows(i).Item("Chd_Tax") = Chd_Tax
                tempTable.Rows(i).Item("Inf_Tax") = Inf_Tax
                tempTable.Rows(i).Item("SrvTax") = SrvTax
                tempTable.Rows(i).Item("TF") = TF
                tempTable.Rows(i).Item("TC") = TC
                tempTable.Rows(i).Item("AdtTds") = AdtTds
                tempTable.Rows(i).Item("ChdTds") = ChdTds
                tempTable.Rows(i).Item("AdtComm") = AdtComm
                tempTable.Rows(i).Item("ChdComm") = ChdComm
                tempTable.Rows(i).Item("AdtCB") = AdtCb
                tempTable.Rows(i).Item("ChdCB") = ChdCb
                tempTable.Rows(i).Item("totFare") = totFare
                tempTable.Rows(i).Item("netFare") = netFare
                tempTable.Rows(i).Item("User_id") = userid
                tempTable.Rows(i).Item("IsCorp") = False
                tempTable.Rows(i).Item("AdtMgtFee") = 0
                tempTable.Rows(i).Item("ChdMgtFee") = 0
                tempTable.Rows(i).Item("InfMgtFee") = 0
                tempTable.Rows(i).Item("TotMgtFee") = 0
            Next

            Dim Connection As New SqlConnection()
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Connection.Open()
            Dim da As New SqlDataAdapter("select * from SelectedFlightDetails", Connection)
            Dim cmd As New SqlCommandBuilder(da)
            da.Update(tempTable)
            Connection.Close()
        Catch ex As Exception
            ' Dim str1 = XmlLog.Info(ex.Message, "InsertFlightData", ConfigurationManager.AppSettings("Exception") & "\" & strFile & DateTime.Now.ToString("dd-MMM-yyyy") & "-" & DateTime.Now.Millisecond & ".txt", Boolean.Parse(ConfigurationManager.AppSettings("WriteLog")))
        End Try
    End Sub
    
    Public Sub updateFlight(ByVal strStatus As String, ByVal trackID As String)
        Try
            Dim Connection As New SqlConnection()
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Connection.Open()
            Dim str As String = "update SelectedFlightDetails set FlightStatus='" & strStatus & "' where Track_id='" & trackID & "'"
            Dim cmd = New SqlCommand(str, Connection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            Connection.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Function isRecordExist(ByVal strTableName As String, ByVal trackID As String) As Boolean
        Try
            Dim flag As Boolean = False
            Dim Connection As New SqlConnection()
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim ds As New DataSet
            Dim str As String = "select * from " & strTableName & " where Ref_Id='" & trackID & "'"
            Dim da As New SqlDataAdapter(str, Connection)
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                flag = True
            End If

            Return flag

        Catch ex As Exception

        End Try

    End Function
    Public Function uniqueID() As String
        Dim duration As System.TimeSpan
        duration = New TimeSpan(12, 30, 0)
        Dim localTimeIndia As DateTime
        ' localTimeIndia.UtcNow.Add(duration)
        Return  localTimeIndia.UtcNow.Add(duration).ToString("MMddyyyyHHmmssffffff")
    End Function

    Public Function getRndNum() As String
        Dim KeyGen As RandomKeyGenerator
        Dim RandomKey As String
        KeyGen = New RandomKeyGenerator
        RandomKey = KeyGen.Generate()
        Return RandomKey
    End Function

End Class
