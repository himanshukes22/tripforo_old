Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Public Class SeriesDepart
    Private con As SqlConnection
    Private cmd As SqlCommand
    Private adap As SqlDataAdapter
    Public Sub New()
        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    'Using
    Public Function InsertSeriesDetails(ByVal airline As String, ByVal aircode As String, ByVal sector As String, ByVal amount As Integer, ByVal avlseat As Integer, ByVal deptdate As String, _
     ByVal retdate As String, ByVal trip As String, ByVal id As String) As Integer
        con.Open()
        cmd = New SqlCommand("InsertSeriesDetails", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@airline", airline)
        cmd.Parameters.AddWithValue("@aircode", aircode)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@amount", amount)
        cmd.Parameters.AddWithValue("@avlseats", avlseat)
        cmd.Parameters.AddWithValue("@deptdate", deptdate)
        cmd.Parameters.AddWithValue("@retdate", retdate)
        cmd.Parameters.AddWithValue("@trip", trip)
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@hold_seat", 0)
        cmd.Parameters.AddWithValue("@confirm_seat", 0)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    'Using
    Public Function GetFlightDetails(Optional ByVal counter As Integer = 0, Optional ByVal execid As String = "", Optional ByVal Trip As String = "") As DataTable
        If counter = 0 And execid <> "" Then
            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)
            adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)
            'Dim dt As New DataTable
            'adap.Fill(dt)
            'Return dt
        End If
        If counter <> 0 And execid = "" Then
            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)
            adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)
            'Dim dt As New DataTable
            'adap.Fill(dt)
            'Return dt
        End If
        If counter = 0 And execid = "" Then
            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)
            adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)

        End If
        If counter = 3 And execid = "" Then
            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)
            adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)

        End If
        If counter = 4 And execid <> "" Then
            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
            adap.SelectCommand.CommandType = CommandType.StoredProcedure
            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)
            adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)

        End If
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function
    'Using
    Public Function UpdateSeriseDeparture(ByVal airline As String, ByVal counter As Integer, ByVal sector As String, ByVal amount As Integer, ByVal avlseats As Integer, ByVal deptdate As String, ByVal retdate As String) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateSeriesDepart", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@airline", airline)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@amount", amount)
        cmd.Parameters.AddWithValue("@avlseats", avlseats)
        cmd.Parameters.AddWithValue("@deptdate", deptdate)
        cmd.Parameters.AddWithValue("@retdate", retdate)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    'Using
    Public Function DeleteSeriesDeparture(ByVal counter As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("DeleteSeriesDepart", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@counter", counter)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    'Using
    Public Function InsertSeriesRequest(ByVal airline As String, ByVal aircode As String, ByVal sector As String, ByVal amount As Integer, ByVal avlseat As Integer, ByVal deptdate As String, ByVal retdate As String, ByVal noofpax As Integer, ByVal noofadult As Integer, ByVal noofchild As Integer, ByVal noofinfant As Integer, ByVal remark As String, ByVal agentid As String, ByVal agencyname As String, ByVal SeriesID As Integer, ByVal Trip As String, ByVal ctcPersonName As String, ByVal ctcPersonno As String, ByVal ctcEmailId As String) As Integer
        con.Open()
        cmd = New SqlCommand("InsertSeriesRequest", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@airline", airline)
        cmd.Parameters.AddWithValue("@aircode", aircode)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@amount", amount)
        cmd.Parameters.AddWithValue("@avlseats", avlseat)
        cmd.Parameters.AddWithValue("@deptdate", deptdate)
        cmd.Parameters.AddWithValue("@retdate", retdate)
        cmd.Parameters.AddWithValue("@NoofPax", noofpax)
        cmd.Parameters.AddWithValue("@NoofAdult", noofadult)
        cmd.Parameters.AddWithValue("@NoofChild", noofchild)
        cmd.Parameters.AddWithValue("@NoofInfant", noofinfant)
        cmd.Parameters.AddWithValue("@remark", remark)
        cmd.Parameters.AddWithValue("@agentid", agentid)
        cmd.Parameters.AddWithValue("@agencyname", agencyname)
        cmd.Parameters.AddWithValue("@SeriesID", SeriesID)
        cmd.Parameters.AddWithValue("@Trip", Trip)
        cmd.Parameters.AddWithValue("@ctcPersonName", ctcPersonName)
        cmd.Parameters.AddWithValue("@ctcPersonNo", ctcPersonno)
        cmd.Parameters.AddWithValue("@ctcEmailid", ctcEmailId)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    'Using
    Public Function GetRequestSeriesDetails(ByVal tablename As String, Optional ByVal Trip As String = "") As DataTable
        adap = New SqlDataAdapter("GetRequestSeries", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
        adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function
    'Using
    Public Function UpdateSeriesPendingRequest(ByVal id As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, ByVal remark As String) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateSerieStatus", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@ip", ip)
        cmd.Parameters.AddWithValue("@status", status)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@rejremark", remark)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    'Using
    Public Function ReleaseSeriesRequest(ByVal id As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, ByVal remark As String) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateSerieStatus", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@ip", ip)
        cmd.Parameters.AddWithValue("@status", status)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@rejremark", remark)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    'Using
    Public Function SeriesFlightReport(ByVal fromdate As String, ByVal todate As String, ByVal uid As String, ByVal usertype As String, ByVal airline As String, ByVal agency As String, ByVal RType As String, ByVal TripType As String) As DataTable
        adap = New SqlDataAdapter("GetSeriseReport", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate)
        adap.SelectCommand.Parameters.AddWithValue("@todate", todate)
        adap.SelectCommand.Parameters.AddWithValue("@UserID", uid)
        adap.SelectCommand.Parameters.AddWithValue("@usertype", usertype)
        adap.SelectCommand.Parameters.AddWithValue("@airline", airline)
        adap.SelectCommand.Parameters.AddWithValue("@agencyname", agency)
        adap.SelectCommand.Parameters.AddWithValue("@RType", RType)
        adap.SelectCommand.Parameters.AddWithValue("@TripType", TripType)
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function

    'Using
    Public Function InserSeriesAccount(ByVal agencyname As String, ByVal AgentID As String, ByVal amount As Integer, ByVal remark As String, ByVal ID As String, ByVal Status As String) As Integer
        con.Open()
        cmd = New SqlCommand("InsertSeriesAccount", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@agencyname", agencyname)
        cmd.Parameters.AddWithValue("@AgentID", AgentID)
        cmd.Parameters.AddWithValue("@amount", amount)
        cmd.Parameters.AddWithValue("@execid", ID)
        cmd.Parameters.AddWithValue("@remark", remark)

        cmd.Parameters.AddWithValue("@Status", Status)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    'Using
    Public Function GetPendingAccountRequest(ByVal tablename As String, ByVal UID As String) As DataTable
        adap = New SqlDataAdapter("GetProcessStatus", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
        adap.SelectCommand.Parameters.AddWithValue("@uid", UID)
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function
    'Using
    Public Function UpdatePendingSeriesAcc(ByVal accid As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, Optional ByVal rm As String = "") As Integer
        con.Open()
        cmd = New SqlCommand("UpdateAcceptSeriseAccount", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", accid)
        cmd.Parameters.AddWithValue("@ip", ip)
        cmd.Parameters.AddWithValue("@status", status)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@rejrm", rm)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    'Using
    Public Function GetProcessSeries(ByVal tablename As String, ByVal uid As String) As DataTable
        adap = New SqlDataAdapter("GetProcessStatus", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
        adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function

    'Using
    Public Function UpdateSeriesRequest(ByVal avlseat As Integer, ByVal counter As Integer, ByVal type As String) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateSeriesRequestDepart", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@avlseat", avlseat)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@Type", type)
        'cmd.Parameters.AddWithValue("@msg", ParameterDirection.Output)

        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    'Using
    Public Function GetExecutiveTripType(ByVal id As String) As DataTable
        adap = New SqlDataAdapter("GetExecutiveTripType", con)
        adap.SelectCommand.CommandType = CommandType.StoredProcedure
        adap.SelectCommand.Parameters.AddWithValue("@id", id)
        Dim dt As New DataTable
        adap.Fill(dt)
        Return dt
    End Function
    'Using
    Public Function Avl_HoldSeat(ByVal holdseat As Integer, ByVal counter As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("Avl_HoldSeat", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@holdseat", holdseat)
        cmd.Parameters.AddWithValue("@counter", counter)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    'Using
    Public Function Update_ConfirmHoldSeat(ByVal NoofPax As Integer, ByVal counter As Integer, ByVal type As String) As Integer
        con.Open()
        ' If (type = "Request" And add = 0) Then
        cmd = New SqlCommand("UpdateRejectedSeats", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@type", type)
        'cmd.Parameters.AddWithValue("@Add", add)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
        'End If
        'If (type = "Inprocess" And add <> 0) Then
        '    cmd = New SqlCommand("UpdateRejectedSeats1", con)
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
        '    cmd.Parameters.AddWithValue("@counter", counter)
        '    cmd.Parameters.AddWithValue("@type", type)
        '    cmd.Parameters.AddWithValue("@Add", add)
        '    Dim i As Integer = cmd.ExecuteNonQuery()
        '    con.Close()
        '    Return i
        'End If
    End Function

    'Using
    Public Function Update_ConfirmHoldSeat1(ByVal NoofPax As Integer, ByVal counter As Integer, ByVal type As String, ByVal add As Integer) As Integer
        'If (type = "Inprocess" And add = 0) Then
        con.Open()
        cmd = New SqlCommand("UpdateRejectedSeats2", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@type", type)
        cmd.Parameters.AddWithValue("@Add", add)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
        'End If
    End Function
    'Using
    Public Function UpdateConfirmSeats(ByVal counter As Integer, ByVal noofpax As Integer) As Integer
        con.Open()
        cmd = New SqlCommand("UpdateConfirmSeats", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@Noofpax", noofpax)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
    Public Function InsertOfflineHold(ByVal airlinename As String, ByVal airlinecode As String, ByVal sector As String, ByVal amount As String, ByVal avlseat As Integer, ByVal depdate As String, ByVal retdate As String, ByVal Status As String, ByVal execid As String, ByVal seriesid As Integer, ByVal offlinehold As Integer, ByVal offlineconfirm As Integer, ByVal type As String, ByVal Remark As String) As Integer
        con.Open()
        cmd = New SqlCommand("Update_OfflineSeat", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@airlinename", airlinename)
        cmd.Parameters.AddWithValue("@airlinecode", airlinecode)
        cmd.Parameters.AddWithValue("@sector", sector)
        cmd.Parameters.AddWithValue("@Amount", amount)
        cmd.Parameters.AddWithValue("@Avl_seat", avlseat)
        cmd.Parameters.AddWithValue("@depdate", depdate)
        cmd.Parameters.AddWithValue("@retdate", retdate)
        cmd.Parameters.AddWithValue("@status", Status)
        cmd.Parameters.AddWithValue("@execID", execid)
        cmd.Parameters.AddWithValue("@Seriesid", seriesid)
        cmd.Parameters.AddWithValue("@offline_hold", offlinehold)
        cmd.Parameters.AddWithValue("@offline_confirm", offlineconfirm)
        cmd.Parameters.AddWithValue("@type", type)
        cmd.Parameters.AddWithValue("@ExecRm", Remark)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function UpdateOfflineHoldSeat(ByVal counter As Integer, ByVal offlineconfirm As Integer, ByVal add As Integer, ByVal seriesid As Integer, ByVal type As String) As Integer
        con.Open()
        cmd = New SqlCommand("OfflineConfirm", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@counter", counter)
        cmd.Parameters.AddWithValue("@offlineconfirm", offlineconfirm)
        cmd.Parameters.AddWithValue("@add", add)
        cmd.Parameters.AddWithValue("@seriesid", seriesid)
        cmd.Parameters.AddWithValue("@type", type)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function
End Class

'Imports Microsoft.VisualBasic
'Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Configuration
'Public Class SeriesDepart
'    Private con As SqlConnection
'    Private cmd As SqlCommand
'    Private adap As SqlDataAdapter
'    Public Sub New()
'        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
'    End Sub
'    'Using
'    Public Function InsertSeriesDetails(ByVal airline As String, ByVal aircode As String, ByVal sector As String, ByVal amount As Integer, ByVal avlseat As Integer, ByVal deptdate As String, _
'     ByVal retdate As String, ByVal trip As String, ByVal id As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("InsertSeriesDetails", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@airline", airline)
'        cmd.Parameters.AddWithValue("@aircode", aircode)
'        cmd.Parameters.AddWithValue("@sector", sector)
'        cmd.Parameters.AddWithValue("@amount", amount)
'        cmd.Parameters.AddWithValue("@avlseats", avlseat)
'        cmd.Parameters.AddWithValue("@deptdate", deptdate)
'        cmd.Parameters.AddWithValue("@retdate", retdate)
'        cmd.Parameters.AddWithValue("@trip", trip)
'        cmd.Parameters.AddWithValue("@id", id)
'        cmd.Parameters.AddWithValue("@hold_seat", 0)
'        cmd.Parameters.AddWithValue("@confirm_seat", 0)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    'Using
'    Public Function GetFlightDetails(Optional ByVal counter As Integer = 0, Optional ByVal execid As String = "") As DataTable
'        If counter = 0 And execid <> "" Then
'            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
'            adap.SelectCommand.CommandType = CommandType.StoredProcedure
'            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
'            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)

'            'Dim dt As New DataTable
'            'adap.Fill(dt)
'            'Return dt
'        End If
'        If counter <> 0 And execid = "" Then
'            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
'            adap.SelectCommand.CommandType = CommandType.StoredProcedure
'            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
'            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)

'            'Dim dt As New DataTable
'            'adap.Fill(dt)
'            'Return dt
'        End If
'        If counter = 0 And execid = "" Then
'            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
'            adap.SelectCommand.CommandType = CommandType.StoredProcedure
'            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
'            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)


'        End If
'        If counter = 3 And execid = "" Then
'            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
'            adap.SelectCommand.CommandType = CommandType.StoredProcedure
'            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
'            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)


'        End If
'        If counter = 4 And execid <> "" Then
'            adap = New SqlDataAdapter("GetSeriseFlightDetails", con)
'            adap.SelectCommand.CommandType = CommandType.StoredProcedure
'            adap.SelectCommand.Parameters.AddWithValue("@counter", counter)
'            adap.SelectCommand.Parameters.AddWithValue("@exec", execid)


'        End If
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function
'    'Using
'    Public Function UpdateSeriseDeparture(ByVal airline As String, ByVal counter As Integer, ByVal sector As String, ByVal amount As Integer, ByVal avlseats As Integer, ByVal deptdate As String, ByVal retdate As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateSeriesDepart", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@airline", airline)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@sector", sector)
'        cmd.Parameters.AddWithValue("@amount", amount)
'        cmd.Parameters.AddWithValue("@avlseats", avlseats)
'        cmd.Parameters.AddWithValue("@deptdate", deptdate)
'        cmd.Parameters.AddWithValue("@retdate", retdate)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function

'    'Using
'    Public Function DeleteSeriesDeparture(ByVal counter As Integer) As Integer
'        con.Open()
'        cmd = New SqlCommand("DeleteSeriesDepart", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@counter", counter)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function

'    'Using
'    Public Function InsertSeriesRequest(ByVal airline As String, ByVal aircode As String, ByVal sector As String, ByVal amount As Integer, ByVal avlseat As Integer, ByVal deptdate As String, ByVal retdate As String, ByVal noofpax As Integer, ByVal noofadult As Integer, ByVal noofchild As Integer, ByVal noofinfant As Integer, ByVal remark As String, ByVal agentid As String, ByVal agencyname As String, ByVal SeriesID As Integer, ByVal Trip As String, ByVal ctcPersonName As String, ByVal ctcPersonno As String, ByVal ctcEmailId As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("InsertSeriesRequest", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@airline", airline)
'        cmd.Parameters.AddWithValue("@aircode", aircode)
'        cmd.Parameters.AddWithValue("@sector", sector)
'        cmd.Parameters.AddWithValue("@amount", amount)
'        cmd.Parameters.AddWithValue("@avlseats", avlseat)
'        cmd.Parameters.AddWithValue("@deptdate", deptdate)
'        cmd.Parameters.AddWithValue("@retdate", retdate)
'        cmd.Parameters.AddWithValue("@NoofPax", noofpax)
'        cmd.Parameters.AddWithValue("@NoofAdult", noofadult)
'        cmd.Parameters.AddWithValue("@NoofChild", noofchild)
'        cmd.Parameters.AddWithValue("@NoofInfant", noofinfant)
'        cmd.Parameters.AddWithValue("@remark", remark)
'        cmd.Parameters.AddWithValue("@agentid", agentid)
'        cmd.Parameters.AddWithValue("@agencyname", agencyname)
'        cmd.Parameters.AddWithValue("@SeriesID", SeriesID)
'        cmd.Parameters.AddWithValue("@Trip", Trip)
'        cmd.Parameters.AddWithValue("@ctcPersonName", ctcPersonName)
'        cmd.Parameters.AddWithValue("@ctcPersonNo", ctcPersonno)
'        cmd.Parameters.AddWithValue("@ctcEmailid", ctcEmailId)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    'Using
'    Public Function GetRequestSeriesDetails(ByVal tablename As String, Optional ByVal Trip As String = "") As DataTable
'        adap = New SqlDataAdapter("GetRequestSeries", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
'        adap.SelectCommand.Parameters.AddWithValue("@Trip", Trip)
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function
'    'Using
'    Public Function UpdateSeriesPendingRequest(ByVal id As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, ByVal remark As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateSerieStatus", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@id", id)
'        cmd.Parameters.AddWithValue("@ip", ip)
'        cmd.Parameters.AddWithValue("@status", status)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@rejremark", remark)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function

'    'Using
'    Public Function ReleaseSeriesRequest(ByVal id As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, ByVal remark As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateSerieStatus", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@id", id)
'        cmd.Parameters.AddWithValue("@ip", ip)
'        cmd.Parameters.AddWithValue("@status", status)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@rejremark", remark)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function

'    'Using
'    Public Function SeriesFlightReport(ByVal fromdate As String, ByVal todate As String, ByVal uid As String, ByVal usertype As String, ByVal airline As String, ByVal agency As String, ByVal RType As String) As DataTable
'        adap = New SqlDataAdapter("GetSeriseReport", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@fromdate", fromdate)
'        adap.SelectCommand.Parameters.AddWithValue("@todate", todate)
'        adap.SelectCommand.Parameters.AddWithValue("@UserID", uid)
'        adap.SelectCommand.Parameters.AddWithValue("@usertype", usertype)
'        adap.SelectCommand.Parameters.AddWithValue("@airline", airline)
'        adap.SelectCommand.Parameters.AddWithValue("@agencyname", agency)
'        adap.SelectCommand.Parameters.AddWithValue("@RType", RType)
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function

'    'Using
'    Public Function InserSeriesAccount(ByVal agencyname As String, ByVal AgentID As String, ByVal amount As Integer, ByVal remark As String, ByVal ID As String, ByVal Status As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("InsertSeriesAccount", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@agencyname", agencyname)
'        cmd.Parameters.AddWithValue("@AgentID", AgentID)
'        cmd.Parameters.AddWithValue("@amount", amount)
'        cmd.Parameters.AddWithValue("@execid", ID)
'        cmd.Parameters.AddWithValue("@remark", remark)

'        cmd.Parameters.AddWithValue("@Status", Status)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    'Using
'    Public Function GetPendingAccountRequest(ByVal tablename As String, ByVal UID As String) As DataTable
'        adap = New SqlDataAdapter("GetProcessStatus", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
'        adap.SelectCommand.Parameters.AddWithValue("@uid", UID)
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function
'    'Using
'    Public Function UpdatePendingSeriesAcc(ByVal accid As String, ByVal ip As String, ByVal status As String, ByVal counter As Integer, Optional ByVal rm As String = "") As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateAcceptSeriseAccount", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@id", accid)
'        cmd.Parameters.AddWithValue("@ip", ip)
'        cmd.Parameters.AddWithValue("@status", status)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@rejrm", rm)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    'Using
'    Public Function GetProcessSeries(ByVal tablename As String, ByVal uid As String) As DataTable
'        adap = New SqlDataAdapter("GetProcessStatus", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@tableRef", tablename)
'        adap.SelectCommand.Parameters.AddWithValue("@uid", uid)
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function

'    'Using
'    Public Function UpdateSeriesRequest(ByVal avlseat As Integer, ByVal counter As Integer, ByVal type As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateSeriesRequestDepart", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@avlseat", avlseat)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@Type", type)
'        'cmd.Parameters.AddWithValue("@msg", ParameterDirection.Output)

'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function

'    'Using
'    Public Function GetExecutiveTripType(ByVal id As String) As DataTable
'        adap = New SqlDataAdapter("GetExecutiveTripType", con)
'        adap.SelectCommand.CommandType = CommandType.StoredProcedure
'        adap.SelectCommand.Parameters.AddWithValue("@id", id)
'        Dim dt As New DataTable
'        adap.Fill(dt)
'        Return dt
'    End Function
'    'Using
'    Public Function Avl_HoldSeat(ByVal holdseat As Integer, ByVal counter As Integer) As Integer
'        con.Open()
'        cmd = New SqlCommand("Avl_HoldSeat", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@holdseat", holdseat)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    'Using
'    Public Function Update_ConfirmHoldSeat(ByVal NoofPax As Integer, ByVal counter As Integer, ByVal type As String) As Integer
'        con.Open()
'        ' If (type = "Request" And add = 0) Then
'        cmd = New SqlCommand("UpdateRejectedSeats", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@type", type)
'        'cmd.Parameters.AddWithValue("@Add", add)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'        'End If
'        'If (type = "Inprocess" And add <> 0) Then
'        '    cmd = New SqlCommand("UpdateRejectedSeats1", con)
'        '    cmd.CommandType = CommandType.StoredProcedure
'        '    cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
'        '    cmd.Parameters.AddWithValue("@counter", counter)
'        '    cmd.Parameters.AddWithValue("@type", type)
'        '    cmd.Parameters.AddWithValue("@Add", add)
'        '    Dim i As Integer = cmd.ExecuteNonQuery()
'        '    con.Close()
'        '    Return i
'        'End If
'    End Function

'    'Using
'    Public Function Update_ConfirmHoldSeat1(ByVal NoofPax As Integer, ByVal counter As Integer, ByVal type As String, ByVal add As Integer) As Integer
'        'If (type = "Inprocess" And add = 0) Then
'        con.Open()
'        cmd = New SqlCommand("UpdateRejectedSeats2", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@Noofpax", NoofPax)
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@type", type)
'        cmd.Parameters.AddWithValue("@Add", add)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'        'End If
'    End Function
'    'Using
'    Public Function UpdateConfirmSeats(ByVal counter As Integer, ByVal noofpax As Integer) As Integer
'        con.Open()
'        cmd = New SqlCommand("UpdateConfirmSeats", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@Noofpax", noofpax)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'    Public Function InsertOfflineHold(ByVal airlinename As String, ByVal airlinecode As String, ByVal sector As String, ByVal amount As String, ByVal avlseat As Integer, ByVal depdate As String, ByVal retdate As String, ByVal Status As String, ByVal execid As String, ByVal seriesid As Integer, ByVal offlinehold As Integer, ByVal offlineconfirm As Integer, ByVal type As String, ByVal Remark As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("Update_OfflineSeat", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@airlinename", airlinename)
'        cmd.Parameters.AddWithValue("@airlinecode", airlinecode)
'        cmd.Parameters.AddWithValue("@sector", sector)
'        cmd.Parameters.AddWithValue("@Amount", amount)
'        cmd.Parameters.AddWithValue("@Avl_seat", avlseat)
'        cmd.Parameters.AddWithValue("@depdate", depdate)
'        cmd.Parameters.AddWithValue("@retdate", retdate)
'        cmd.Parameters.AddWithValue("@status", Status)
'        cmd.Parameters.AddWithValue("@execID", execid)
'        cmd.Parameters.AddWithValue("@Seriesid", seriesid)
'        cmd.Parameters.AddWithValue("@offline_hold", offlinehold)
'        cmd.Parameters.AddWithValue("@offline_confirm", offlineconfirm)
'        cmd.Parameters.AddWithValue("@type", type)
'        cmd.Parameters.AddWithValue("@ExecRm", Remark)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i

'    End Function
'    Public Function UpdateOfflineHoldSeat(ByVal counter As Integer, ByVal offlineconfirm As Integer, ByVal add As Integer, ByVal seriesid As Integer, ByVal type As String) As Integer
'        con.Open()
'        cmd = New SqlCommand("OfflineConfirm", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@counter", counter)
'        cmd.Parameters.AddWithValue("@offlineconfirm", offlineconfirm)
'        cmd.Parameters.AddWithValue("@add", add)
'        cmd.Parameters.AddWithValue("@seriesid", seriesid)
'        cmd.Parameters.AddWithValue("@type", type)
'        Dim i As Integer = cmd.ExecuteNonQuery()
'        con.Close()
'        Return i
'    End Function
'End Class
