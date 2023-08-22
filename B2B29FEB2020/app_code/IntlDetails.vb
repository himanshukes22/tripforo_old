Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration


''' <summary>
''' Summary description for IntlDetails
''' </summary>
Public Class IntlDetails
    Private con As SqlConnection
    Private adap As SqlDataAdapter
    Private ds As DataSet
    Public Sub New()

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    End Sub
    Public Function SelectTicketReport(ByVal AgentID As String, ByVal usertype As String) As DataSet
        Dim ds As New DataSet()
        If usertype = "ADMIN" Or usertype = "ACC" Then

            adap = New SqlDataAdapter("SELECT FltHeader.OrderId, FltHeader.TotalBookingCost, FltHeader.sector, FltHeader.AgentId, FltPaxDetails.PaxId,FltPaxDetails.PaxType, FltHeader.Status, FltHeader.CreateDate, FltHeader.GdsPnr FROM  FltHeader INNER JOIN FltPaxDetails ON FltHeader.OrderId = FltPaxDetails.OrderId where FltHeader.Status='Ticketed'", con)
            adap.Fill(ds)
            Return ds
        ElseIf usertype = "EXEC" Then
            adap = New SqlDataAdapter("SELECT FltHeader.OrderId, FltHeader.TotalBookingCost, FltHeader.sector, FltHeader.AgentId, FltPaxDetails.PaxId,FltPaxDetails.PaxType, FltHeader.Status, FltHeader.CreateDate, FltHeader.GdsPnr FROM  FltHeader INNER JOIN FltPaxDetails ON FltHeader.OrderId = FltPaxDetails.OrderId where FltHeader.Status='Ticketed' and FltHeader.ExecutiveId='" & AgentID & "' ", con)
            adap.Fill(ds)
        ElseIf usertype = "AGENT" Then
            adap = New SqlDataAdapter("SELECT FltHeader.OrderId, FltHeader.TotalBookingCost, FltHeader.sector, FltHeader.AgentId, FltPaxDetails.PaxId,FltPaxDetails.PaxType, FltHeader.Status, FltHeader.CreateDate, FltHeader.GdsPnr FROM  FltHeader INNER JOIN FltPaxDetails ON FltHeader.OrderId = FltPaxDetails.OrderId where FltHeader.Status='Ticketed' and FltHeader.AgentId='" & AgentID & "'", con)
            adap.Fill(ds)
        End If
        Return ds
    End Function
    Public Function SelectHeaderDetail(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SELECT * FROM  FltHeader WHERE OrderId = '" & OrderId & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function SelectPaxDetail(ByVal OrderId As String, ByVal TID As String) As DataTable
        If String.IsNullOrEmpty(TID) Then
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name,title, FName,MName,LName, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' ", con)
            adap.Fill(dt)

            Return dt
        Else
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name,title, FName,MName,LName, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            adap.Fill(dt)
            Return dt
        End If
    End Function
    Public Function SelectPaxDetailBOTH(ByVal OrderId As String, ByVal ROrderId As String, ByVal TID As String) As DataTable
        If String.IsNullOrEmpty(TID) Then
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name,title, FName,MName,LName, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' OR OrderId='" & ROrderId & "' ", con)
            adap.Fill(dt)

            Return dt
        Else
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT PaxId, OrderId, PaxId, Title + '  ' + FName + '  ' + MName + '  ' + LName AS Name,title, FName,MName,LName, PaxType, TicketNumber,DOB FROM   FltPaxDetails WHERE OrderId = '" & OrderId & "' and PaxId= '" & TID & "' ", con)
            adap.Fill(dt)
            Return dt
        End If
    End Function

    Public Function SelectFltheaderDetls(ByVal OrderId As String) As DataTable

        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select PgMobile,PgEmail from FltHeader WHERE OrderId = '" & OrderId & "'", con)
        adap.Fill(dt)
        Return dt
    End Function




    Public Function SelectAgent(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SELECT AgentId FROM   FltHeader WHERE OrderId = '" & OrderId & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function SelectAgencyDetail(ByVal AgentID As String) As DataTable
        Dim dt As New DataTable()
        ''adap = New SqlDataAdapter("SELECT Agency_Name,Email,Mobile,Address,Address,(City+' - '+State+' - '+Country) as Address1,IsCorp from New_Regs  WHERE User_Id = '" & AgentID & "' ", con)
        adap = New SqlDataAdapter("SELECT Agency_Name,Email,Mobile,Address,Address,(City+' - '+State+' - '+Country) as Address1,IsCorp from agent_register  WHERE User_Id = '" & AgentID & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function SelectFlightDetail(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SELECT FltId, OrderId, DepCityOrAirportCode AS DFrom, DepCityOrAirportName as DepAirName, ArrCityOrAirportCode AS ATo, ArrCityOrAirportName as ArrAirName, DepDate, DepTime, ArrDate, ArrTime, AirlineCode, AirlineName, FltNumber, AirCraft, CreateDate, UpdateDate,ISNULL(AdtRbd,'') as AdtRbd,ISNULL(ChdRbd,'') as ChdRbd FROM FltDetails  WHERE OrderId = '" & OrderId & "' Order by FltId ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function SelectFareDetail(ByVal OrderId As String, ByVal TID As String) As DataTable
        If String.IsNullOrEmpty(TID) Then
            Dim dt As New DataTable()
            'adap = New SqlDataAdapter("select PaxType, BaseFare, YQ as Fuel,(YR+WO+OT+ISNULL(ticketcopymarkupforTAX,0)+ISNULL(ticketcopymarkupforTC,0)) as Tax,ServiceTax,TranFee as TFee, (AdminMrk+AgentMrk+DistrMrk) as TCharge,(BaseFare+YQ+YR+WO+OT+ServiceTax+AdminMrk+AgentMrk+DistrMrk+TranFee+ISNULL(ticketcopymarkupforTAX,0)+ISNULL(ticketcopymarkupforTC,0)) as Total,TotalAfterDis , ISNULL(MgtFee,0) as MgtFee,ISNULL(FareType,'') as FareType,ISNULL(ticketcopymarkupforTAX,0) as ticketcopymarkupforTAX , ISNULL(ticketcopymarkupforTC,0) as ticketcopymarkupforTC  FROM FltFareDetails where OrderId='" & OrderId & "' ", con)
            adap = New SqlDataAdapter("select PaxType, BaseFare,Discount,Tds, YQ as Fuel,(TotalTax-YQ) as Tax,ServiceTax,TranFee as TFee, (AdminMrk+AgentMrk+DistrMrk) as TCharge,(BaseFare+TotalTax+ServiceTax+AdminMrk+AgentMrk+DistrMrk+TranFee+ISNULL(ticketcopymarkupforTAX,0)+ISNULL(ticketcopymarkupforTC,0)) as Total,TotalAfterDis , ISNULL(MgtFee,0) as MgtFee,ISNULL(FareType,'') as FareType,ISNULL(ticketcopymarkupforTAX,0) as ticketcopymarkupforTAX , ISNULL(ticketcopymarkupforTC,0) as ticketcopymarkupforTC  FROM FltFareDetails where OrderId='" & OrderId & "' ", con)
			adap.Fill(dt)
            Return dt
        Else
            Dim dt As New DataTable()
            adap = New SqlDataAdapter("SELECT  FltFareDetails.BaseFare as BaseFare, FltFareDetails.YQ as Fuel,  (FltFareDetails.YR+FltFareDetails.WO+FltFareDetails.OT+FltFareDetails.K3) as Tax,FltFareDetails.ServiceTax as ServiceTax,FltFareDetails.TranFee as TFee, FltFareDetails.AdminMrk+FltFareDetails.AgentMrk+FltFareDetails.DistrMrk as TCharge,TotalAfterDis , ISNULL(MgtFee,0) as MgtFee,ISNULL(FareType,'') as FareType  FROM FltPaxDetails INNER JOIN FltFareDetails ON FltPaxDetails.OrderId = FltFareDetails.OrderId AND FltPaxDetails.PaxType = FltFareDetails.PaxType WHERE FltPaxDetails.PaxId = '" & TID & "' ", con)
            adap.Fill(dt)
            Return dt
        End If
    End Function
    Public Function IntlHoldPNRAgentReport(ByVal Status As String, ByVal Trip As String, ByVal AGENTID As String, ByVal Multistatus As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from FltHeader where (Status=('" & Status & "') or  Status=('" & Multistatus & "')) and Trip='" & Trip & "' and AgentID='" & AGENTID & "'  order by CreateDate desc", con)
        adap.Fill(ds)
        Return ds
    End Function
    Public Function CountADT(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select COUNT(PaxType) counting from dbo.FltPaxDetails where PaxType='ADT' and OrderId = '" & OrderId & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function CountCHD(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select COUNT(PaxType) counting from dbo.FltPaxDetails where PaxType='CHD' and OrderId = '" & OrderId & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function CountINF(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select COUNT(PaxType) counting from dbo.FltPaxDetails where PaxType='INF' and OrderId = '" & OrderId & "' ", con)
        adap.Fill(dt)
        Return dt
    End Function

    Public Function AgentIDInfo(ByVal AgentID As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select * from agent_register where user_id='" & AgentID & "'", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function EmailID(ByVal orderid As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("select * from FltHeader where OrderId='" & orderid & "'", con)
        adap.Fill(dt)
        Return dt
    End Function
    'HoldPNRRequest Functions

    Public Function IntlConfirmHoldPNR(ByVal Status As String, ByVal Trip As String, Optional ByVal Multistatus As String = "") As DataSet
        Dim ds As New DataSet()
        If (Multistatus = "") Then

            adap = New SqlDataAdapter("Select * from FltHeader where Status='" & Status & "' and Trip='" & Trip & "' order by CreateDate desc", con)
            adap.Fill(ds)

        Else

            adap = New SqlDataAdapter("Select * from FltHeader where (Status=('" & Status & "') or  Status=('" & Multistatus & "')) and Trip='" & Trip & "'  order by CreateDate desc", con)
            adap.Fill(ds)

        End If

        Return ds
    End Function
    Public Function UpdateIntlFltHeaderExecutiveID(ByVal IDs As String, ByVal ExecID As String, ByVal Status As String, ByVal Comment As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE FltHeader set ExecutiveId='" & ExecID & "',Status = '" & Status & "',RejectedRemark = '" & Comment & "' where OrderId='" & IDs & "' ", con)
        adap.Fill(ds)
        Return ds
    End Function
    Public Function UpdateIntlFltHeaderExecutiveID(ByVal OrderId As String, ByVal ExecID As String, ByVal Status As String) As DataSet

        Dim ds As New DataSet()

        adap = New SqlDataAdapter("UPDATE FltHeader set ExecutiveId='" & ExecID & "',Status = '" & Status & "' where OrderId='" & OrderId & "' ", con)
        adap.Fill(ds)
        Return ds
    End Function
    'End HoldPNRRequest
    'HoldPNRRequest Functions

    Public Function IntlUpdateHoldPNR(ByVal Status As String, ByVal Trip As String, ByVal ExecID As String) As DataSet
        Dim ds As New DataSet()
        adap = New SqlDataAdapter("Select * from FltHeader where Status='" & Status & "' and Trip='" & Trip & "' and ExecutiveId='" & ExecID & "' order by CreateDate desc", con)
        adap.Fill(ds)
        Return ds

    End Function
    Public Function TravellerInfo(ByVal OrderId As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("SELECT  FltPaxDetails.PaxId, FltPaxDetails.OrderId, FltPaxDetails.Title + '  ' + FltPaxDetails.FName + '  ' + FltPaxDetails.MName + '  ' + FltPaxDetails.LName AS Name, FltPaxDetails.PaxType,FltPaxDetails.TicketNumber,FltFareDetails.TotalAfterDis,(case when FltPaxDetails.DOB='' then 'N/A' else FltPaxDetails.DOB end) as DOB  FROM FltPaxDetails inner join FltFareDetails on FltPaxDetails.OrderId=FltFareDetails.OrderId and FltPaxDetails.PaxType=FltFareDetails.PaxType  WHERE FltPaxDetails.OrderId = '" & OrderId & "'", con)
        adap.Fill(dt)
        Return dt
    End Function

    'UpdatePNR
    Public Function UpdatePNRIntl(ByVal OrderId As String, ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal Status As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("Update FltHeader set  GdsPnr='" & GdsPnr & "',AirlinePnr='" & AirlinePnr & "' ,Status='" & Status & "',UpdateDate='" & System.DateTime.Now() & "' where OrderId='" & OrderId & "'", con)
        adap.Fill(dt)
        Return dt
    End Function
    Public Function UpdateTicketIntl(ByVal OrderId As String, ByVal TID As String, ByVal tkt As String) As DataTable
        Dim dt As New DataTable()
        adap = New SqlDataAdapter("Update FltPaxDetails set  TicketNumber='" & tkt & "' where OrderId='" & OrderId & "' and PaxId='" & TID & "'", con)
        adap.Fill(dt)
        Return dt
    End Function


    'QC

    Public Function InsertQCFlightHeader(ByVal OrderID As String, ByVal TableName As String, ByVal ExecId As String, ByVal Remark As String) As Integer
        con.Open()
        Dim cmd As New SqlCommand("Insert into Tbl_QcChecklist(OrderId,  TableName, ExecutiveId, Remark) values('" & OrderID & "','" & TableName & "','" & ExecId & "','" & Remark & "') ", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i

    End Function
    Public Function UpdateFlightHeader(ByVal GdsPnr As String, ByVal AirlinePnr As String, ByVal Status As String, ByVal OrderId As String) As Integer
        con.Open()
        Dim cmd As New SqlCommand("Update fltHeader set  GdsPnr='" & GdsPnr & "', AirlinePnr='" & AirlinePnr & "',Status='" & Status & "' where OrderId='" & OrderId & "' ", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function



    Public Function UpdatePaxInformation(ByVal Title As String, ByVal FName As String, ByVal LName As String, ByVal Type As String, ByVal TicketNo As String, ByVal OrderId As String, ByVal PaxId As String) As Integer
        con.Open()
        Dim cmd As New SqlCommand("Update FltPaxDetails set  Title='" & Title & "', FName='" & FName & "',LName='" & LName & "', PaxType='" & Type & "',TicketNumber='" & TicketNo & "'  where OrderId='" & OrderId & "' and PaxId ='" & PaxId & "' ", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    Public Function UpdateFlightDetails(ByVal DepcityName As String, ByVal DepcityCode As String, ByVal ArrCityName As String, ByVal ArrCityCode As String, ByVal AirlineName As String, ByVal AirlineCode As String, ByVal FlightNo As String, ByVal DepDate As String, ByVal DepTime As String, ByVal ArrTime As String, ByVal AirCraft As String, ByVal OrderId As String, ByVal FltId As String) As Integer
        con.Open()
        Dim cmd As New SqlCommand("Update FltDetails set  DepCityOrAirportName='" & DepcityName & "', DepCityOrAirportCode='" & DepcityCode & "',ArrCityOrAirportName='" & ArrCityName & "', ArrCityOrAirportCode='" & ArrCityCode & "',AirlineName='" & AirlineName & "',AirlineCode='" & AirlineCode & "',FltNumber='" & FlightNo & "',DepDate='" & DepDate & "',DepTime='" & DepTime & "',ArrTime='" & ArrTime & "',AirCraft='" & AirCraft & "'  where OrderId='" & OrderId & "' and FltId='" & FltId & "'", con)
        Dim i As Integer = cmd.ExecuteNonQuery()
        con.Close()
        Return i
    End Function

    Public Function Email_Credentilas(ByVal Orderid As String, ByVal Cmd_Type As String, ByVal Counter As String) As DataTable
        con.Open()
        Dim dt As New DataTable()
        Dim cmd As New SqlCommand()
        cmd.CommandText = "USP_TICKETSTATUS"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = Orderid
        cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = Cmd_Type
        cmd.Parameters.Add("@COUNTER", SqlDbType.VarChar).Value = Counter
        cmd.Connection = con
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(dt)
        con.Close()
        Return dt
    End Function
    Public Function PaxName_OrderID(ByVal Orderid As String, ByVal Cmd_Type As String, ByVal Counter As String) As DataTable
        con.Open()
        Dim dt As New DataTable()
        Dim cmd As New SqlCommand()
        cmd.CommandText = "USP_OrderID_PaxName"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@ORDERID", SqlDbType.VarChar).Value = Orderid
        cmd.Parameters.Add("@CMD_TYPE", SqlDbType.VarChar).Value = Cmd_Type
        cmd.Parameters.Add("@COUNTER", SqlDbType.VarChar).Value = Counter
        cmd.Connection = con
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(dt)
        con.Close()
        Return dt
    End Function

    'QC

End Class
