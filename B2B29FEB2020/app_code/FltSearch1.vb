Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq.Expressions
Imports System.Linq
Imports System.Data
Imports System.Diagnostics
Imports STD.DAL
Imports STD.BAL
Imports STD.Shared
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Runtime.Serialization
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Xml.Linq
Imports ITZLib
Imports Newtonsoft.Json
Imports System.Dynamic
Imports STD.BAL._6ENAV420
Imports STD.BAL.SGNAV420
'test

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.ComponentModel.ToolboxItem(False)>
<System.Web.Script.Services.ScriptService()>
Public Class FltSearch1
    Inherits System.Web.Services.WebService
    Private UID As String, D_ID As String, OwnerId As String, GroupType As String
    Private UserType As String
    Private TypeId As String
    Private ReturnList As ArrayList
    Private fbStr As String = ""
    Private adt As Integer = 0
    Private chd As Integer = 0
    Private inf As Integer = 0
    Private sql As New SqlTransaction
    'Shared.SelectedFlightDetail objSFD = new Shared.SelectedFlightDetail();
    Private Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Private AirResult As New Hashtable()
    ''' This Fucntion Takes Paramter list from FlightSearch JS.
    ''' Assign Remaining Values from Session and Get Flight List from Webservice
    ''' <param name="a"></param>
    ''' <returns></returns>
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Search_Flight(ByVal obj As FlightSearch) As ArrayList
        Try
            'Note Data from jason is a Table(List) of Dictonary.Each column in table is a dictonary
            UID = HttpContext.Current.Session("UID").ToString() '"MML1";
            UserType = Session("UserType").ToString() '"AGENT";
            TypeId = Session("TypeId").ToString() ' //"TA1";
            OwnerId = UID
            'Session["OwnerId"].ToString();
            If (UID <> "" OrElse UID IsNot Nothing) And (UserType <> "" OrElse UserType IsNot Nothing) Then
                Dim objInputs As New STD.Shared.FlightSearch()
                objInputs.DepartureCity = obj.DepartureCity
                objInputs.ArrivalCity = obj.ArrivalCity
                objInputs.HidTxtDepCity = obj.HidTxtDepCity
                objInputs.HidTxtArrCity = obj.HidTxtArrCity
                ' "BOM,IN";
                objInputs.DepDate = obj.DepDate
                ' "20/07/2012";
                objInputs.RetDate = obj.RetDate
                objInputs.RTF = obj.RTF
                objInputs.GDSRTF = obj.GDSRTF
                'false;
                'objInputs.Trip = obj.;
                'objInputs.TripType = obj.TripType;
                If obj.Trip1 = "D" Then
                    objInputs.Trip = Trip.D
                ElseIf obj.Trip1 = "I" Then
                    objInputs.Trip = Trip.I
                End If
                If obj.TripType1 = "rdbOneWay" Then
                    objInputs.TripType = TripType.OneWay
                ElseIf obj.TripType1 = "rdbRoundTrip" Then
                    objInputs.TripType = TripType.RoundTrip
                End If
                objInputs.AirLine = obj.AirLine
                objInputs.HidTxtAirLine = obj.HidTxtAirLine
                objInputs.Adult = obj.Adult
                objInputs.Child = obj.Child
                objInputs.Infant = obj.Infant
                objInputs.NStop = obj.NStop
                ' false;
                objInputs.Cabin = obj.Cabin
                'Session Parameters
                objInputs.DISTRID = "SPRING"
                objInputs.UID = UID
                objInputs.TypeId = TypeId
                ' "FinalResult";
                objInputs.UserType = UserType
                '""; 
                objInputs.TDS = Data.Calc_TDS(Con.ConnectionString, UID)
                objInputs.IsCorp = HttpContext.Current.Session("IsCorp")
                objInputs.AgentType = HttpContext.Current.Session("agent_type")
                objInputs.OwnerId = OwnerId
                Dim objI As IFlt = New FltService()
                ReturnList = DirectCast(objI.FltSearchResult(objInputs), ArrayList)
            Else
                'Redirect to Login
                Return Nothing
            End If
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_002", ex, "FlightSearch");
        Catch ex As Exception
        Finally
        End Try
        Return ReturnList
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_Selected_FltDetails_LZCmp(ByVal a As String) As STD.BAL.CacheRereshRespList
        'BAL is RECIVING EACH LINENO.'s RECORD
        Dim Tracks As String() = Nothing
        Dim changeFareList = New STD.BAL.CacheRereshRespList()

        Try
            'Dim strarray As String = LZCompression.LZString.DecompressFromUTF16(a)
            'Dim arrNew As ArrayList = Newtonsoft.Json.Linq.JArray.Parse(strarray).ToObject(Of ArrayList)()
            'Dim newArr As ArrayList = New ArrayList()

            'For index As Integer = 0 To arrNew.Count - 1
            '    newArr.Add(Newtonsoft.Json.Linq.JObject.Parse(arrNew(index).ToString()).ToObject(Of Dictionary(Of String, Object))())
            'Next

            '' Dim strarray As String = LZCompression.LZString.DecompressFromUTF16(a)
            Dim arrNew As ArrayList = Newtonsoft.Json.Linq.JArray.Parse(a).ToObject(Of ArrayList)()
            Dim newArr As ArrayList = New ArrayList()
            Dim newArrMain As ArrayList = New ArrayList()

            For ind As Integer = 0 To arrNew.Count - 1
                Dim aa As Object() = New Object(arrNew(ind).Count - 1) {}
                For index As Integer = 0 To arrNew(ind).Count - 1
                    Dim bb = Newtonsoft.Json.Linq.JObject.Parse(arrNew(ind)(index).ToString()).ToObject(Of Dictionary(Of String, Object))() '' Newtonsoft.Json.Linq.JObject.Parse(arrNew(0)(index).ToString()).ToObject(Of Dictionary(Of String,Object))()                                                                                                             )()
                    aa(index) = bb
                Next
                newArrMain.Add(aa)
            Next


            changeFareList = Insert_Selected_FltDetails(newArrMain)

            If (changeFareList.ChangeFareO.NewNetFare = "-1") Then
                changeFareList.ChangeFareO.TrackId = "0"
            End If




            'Insert_Selected_FltDetails(ByVal a As ArrayList)

        Catch ex As Exception
            changeFareList = New STD.BAL.CacheRereshRespList()
            changeFareList.ChangeFareO = New CacheRereshResp()
            changeFareList.ChangeFareO.TrackId = "0"


            'Return changeFareList
        Finally
        End Try
        Return changeFareList
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_Selected_FltDetails(ByVal a As ArrayList) As STD.BAL.CacheRereshRespList
        'BAL is RECIVING EACH LINENO.'s RECORD
        Dim Tracks As String() = Nothing
        Dim diff As String = "0"
        Dim diffz As String = "0"
        Dim changeFareList = New STD.BAL.CacheRereshRespList()
        changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp()
        changeFareList.ChangeFareR = New STD.BAL.CacheRereshResp()
        Try
            Dim l1 As Integer
            Dim l2 As Integer
            Dim ListOW As Object()
            Dim ListRT As Object()
            Dim Trip As Integer = a.Count

            'For 2 Trips Calculate Results 
            If Trip = 2 Then
                Tracks = New String(1) {}
                'INSERT FOR BOTH
                'LIST-1
                ListOW = DirectCast(a(0), Object())
                l1 = ListOW.Length
                Tracks(0) = Insert_REC_Details(ListOW, l1, "D", a, "", diff)
                'LIST-2
                ListRT = DirectCast(a(1), Object())
                l2 = ListRT.Length
                diffz = diff
                diff = "0"
                Tracks(1) = Insert_REC_Details(ListRT, l2, "D", a, "", diff)
            Else
                Tracks = New String(0) {}
                'INSER FOR ONE ONLY
                ListOW = DirectCast(a(0), Object())
                l1 = ListOW.Length
                Tracks(0) = Insert_REC_Details(ListOW, l1, "D", a, "", diff)

            End If
            'If CheckFareAvailability(Tracks) Then
            '    Return Tracks
            'Else
            '    Tracks(0) = "0"
            '    Return Tracks
            'End If
            Dim trId As String() = New String(1) {}

            ' trId = CheckFareAvailability(Tracks, changeFareList)
            trId = CheckFareAvailabilityNew(Tracks, changeFareList, a, "D")
            If trId(0).Trim() = "Error" Then
                changeFareList.ChangeFareO.TrackId = "0"
            Else
                changeFareList.ChangeFareO.TrackId = trId(0)
            End If

            If (diff <> "0") Then
                changeFareList.ChangeFareO.NewNetFare = "-1"
                changeFareList.ChangeFareO.NewTotFare = "-1"
            End If

            'changeFareList.ChangeFareO.TrackId = trId(0)
            If Trip = 2 Then
                If changeFareList.ChangeFareR Is Nothing Then
                    changeFareList.ChangeFareR = New STD.BAL.CacheRereshResp()
                End If

                If trId(1).Trim() = "Error" Then
                    changeFareList.ChangeFareO.TrackId = "0"
                Else
                    changeFareList.ChangeFareR.TrackId = trId(1)
                End If

                If (diffz <> "0" OrElse diff <> "0") Then
                    changeFareList.ChangeFareO.NewNetFare = "-1"
                    changeFareList.ChangeFareO.NewTotFare = "-1"
                    changeFareList.ChangeFareR.NewNetFare = "-1"
                    changeFareList.ChangeFareR.NewTotFare = "-1"
                End If


            End If

            Return changeFareList
            '' ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightSearch");
        Catch ex As Exception
            changeFareList.ChangeFareO.TrackId = "0"
            Return changeFareList
        Finally
        End Try
        'Return Tracks
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_Selected_FltDetails_live(ByVal a As ArrayList, ByVal tracks_old As String(), ByVal changeFareList As STD.BAL.CacheRereshRespList) As String()
        'BAL is RECIVING EACH LINENO.'s RECORD
        Dim Tracks As String() = Nothing
        Dim diff As String = "0"
        Try
            Dim l1 As Integer
            Dim l2 As Integer
            Dim ListOW As Object()
            Dim ListRT As Object()
            Dim Trip As Integer = a.Count
            'For 2 Trips Calculate Results 
            If Trip = 2 Then
                Tracks = New String(1) {}
                'INSERT FOR BOTH
                'LIST-1
                If changeFareList.ChangeFareO.NewNetFare <> changeFareList.ChangeFareO.CacheNetFare Then

                    ListOW = DirectCast(a(0), Object())
                    l1 = ListOW.Length
                    Tracks(0) = Insert_REC_Details(ListOW, l1, "D", a, tracks_old(0), diff)

                Else
                    Tracks(0) = tracks_old(0)
                End If


                'LIST-2

                If changeFareList.ChangeFareR.NewNetFare <> changeFareList.ChangeFareR.CacheNetFare Then
                    ListRT = DirectCast(a(1), Object())
                    l2 = ListRT.Length
                    Tracks(1) = Insert_REC_Details(ListRT, l2, "D", a, tracks_old(1), diff)

                Else
                    Tracks(1) = tracks_old(1)
                End If


            Else

                Tracks = New String(0) {}
                'INSER FOR ONE ONLY
                If changeFareList.ChangeFareO.NewNetFare <> changeFareList.ChangeFareO.CacheNetFare Then

                    ListOW = DirectCast(a(0), Object())
                    l1 = ListOW.Length
                    Tracks(0) = Insert_REC_Details(ListOW, l1, "D", a, tracks_old(0), diff)

                Else
                    Tracks(0) = tracks_old(0)

                End If


            End If
            'If CheckFareAvailability(Tracks) Then
            '    Return Tracks
            'Else
            '    Tracks(0) = "0"
            '    Return Tracks
            'End If
            ' Tracks = CheckFareAvailability(Tracks)
            Return Tracks
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightSearch");
        Catch ex As Exception
            Tracks(0) = "0"
            Return Tracks
        Finally
        End Try
        'Return Tracks
    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_International_FltDetails_LZCmp(ByVal a As String) As STD.BAL.CacheRereshRespList

        Dim Tracks As String() = New String(0) {}
        Dim changeFareList = New STD.BAL.CacheRereshRespList
        'Public Function FareBreakupGAL1(ByVal AirArray As String, ByVal Trip As String) As ArrayList
        Try
            ''Dim strarray As String = LZCompression.LZString.DecompressFromUTF16(a)
            Dim arrNew As ArrayList = Newtonsoft.Json.Linq.JArray.Parse(a).ToObject(Of ArrayList)()
            Dim newArr As ArrayList = New ArrayList()
            Dim newArrMain As ArrayList = New ArrayList()
            Dim aa As Object() = New Object(arrNew(0).Count - 1) {}

            For index As Integer = 0 To arrNew(0).Count - 1

                Dim bb = Newtonsoft.Json.Linq.JObject.Parse(arrNew(0)(index).ToString()).ToObject(Of Dictionary(Of String, Object))() '' Newtonsoft.Json.Linq.JObject.Parse(arrNew(0)(index).ToString()).ToObject(Of Dictionary(Of String,Object))()                                                                                                             )()
                aa(index) = bb
            Next
            newArrMain.Add(aa)
            'Dim dict = New Dictionary(Of String, Object) From {{"Property", "foo"}}
            changeFareList = Insert_International_FltDetails(newArrMain)

        Catch ex As Exception
            changeFareList.ChangeFareO.TrackId = "0"
            'Return Tracks
        Finally
        End Try
        Return changeFareList
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_International_FltDetails(ByVal a As ArrayList) As STD.BAL.CacheRereshRespList
        'BAL is RECIVING EACH LINENO.'s RECORD
        Dim diff As String = "0"
        Dim Tracks As String() = New String(0) {}
        Dim changeFareList = New STD.BAL.CacheRereshRespList
        changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp()
        changeFareList.ChangeFareR = New STD.BAL.CacheRereshResp()
        'Single Track Id in International
        '' Session("agent_type") = "type1"
        Dim Rec As New Dictionary(Of String, Object)()
        Dim ListOW As Object()
        Dim l As Integer = 0
        Try
            ListOW = DirectCast(a(0), Object())
            l = ListOW.Length
            If a.Count > 0 Then
                Tracks(0) = Insert_REC_Details(ListOW, l, "I", a, "", diff)
            End If


            ''CheckFareAvailability(Tracks, changeFareList)

            Dim trId As String() = New String(1) {}
            trId = CheckFareAvailabilityNew(Tracks, changeFareList, a, "I")
            If trId(0).Trim() = "Error" Then
                changeFareList.ChangeFareO.TrackId = "0"
            Else
                changeFareList.ChangeFareO.TrackId = trId(0)
            End If

            ''changeFareList.ChangeFareO.TrackId = Tracks(0)
            'If CheckFareAvailability(Tracks) Then
            '    Return Tracks
            'Else
            '    Tracks(0) = "0"
            '    Return Tracks
            'End If
        Catch ex As Exception
            changeFareList.ChangeFareO.TrackId = "0"
            'Return Tracks
        Finally
        End Try
        Return changeFareList
    End Function





    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CheckFareAvailability(ByVal TrackId As String(), ByRef changeFareList As STD.BAL.CacheRereshRespList) As String()

        Try

            changeFareList = New STD.BAL.CacheRereshRespList()
            changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp()
            changeFareList.ChangeFareR = New STD.BAL.CacheRereshResp()

            'NO. OF RECORDS LIST FOR MULTI STOPS
            Dim obj As New STD.BAL.GALTransanctions()
            Dim OBRes As Boolean = False, IBRes As Boolean = False
            obj.connectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString()
            'Call Function
            Dim objDA As New SqlTransaction
            Dim OBFltDs, IBFltDs As DataSet
            Dim ckCacheDs As DataSet

            Dim trackIdO As String = TrackId(0)
            Dim trackIdR As String = ""
            OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            If (TrackId.Length = 2) Then
                trackIdR = TrackId(1)
                IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
            End If
            If OBFltDs.Tables(0).Rows(0)("Trip").ToString() = "D" Then
                Dim result As New ArrayList()

                ckCacheDs = objDA.GetCacheStatusForOrderIds(trackIdO, trackIdR)
                Dim rowO As DataRow() = ckCacheDs.Tables(0).Select("orderID='" & trackIdO & "'")
                Dim rowR As DataRow() = If(trackIdR <> "", ckCacheDs.Tables(0).Select("orderID='" & trackIdR & "'"), Nothing)
                Dim objCache As New STD.BAL.FltSearchForCacheResult(obj.connectionString)
                Dim isNRMLRndTRip As Boolean = False

                If rowO(0)("IsCache") = True AndAlso (rowR IsNot Nothing AndAlso rowR(0)("IsCache") = True) Then
                    isNRMLRndTRip = True
                    result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, isNRMLRndTRip, changeFareList)
                    TrackId = Insert_Selected_FltDetails_live(result, TrackId, changeFareList)

                ElseIf rowO(0)("IsCache") = True Then

                    Dim tackid_old As String() = New String(1) {}
                    tackid_old(0) = TrackId(0)
                    tackid_old(1) = TrackId(0)
                    result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, isNRMLRndTRip, changeFareList)
                    TrackId(0) = Insert_Selected_FltDetails_live(result, tackid_old, changeFareList)(0)

                ElseIf rowR IsNot Nothing AndAlso rowR(0)("IsCache") = True Then
                    Dim tackid_old As String() = New String(1) {}
                    tackid_old(0) = TrackId(1)
                    tackid_old(1) = TrackId(1)
                    result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), IBFltDs, OBFltDs, isNRMLRndTRip, changeFareList)
                    changeFareList.ChangeFareR = changeFareList.ChangeFareO
                    changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp
                    TrackId(1) = Insert_Selected_FltDetails_live(result, tackid_old, changeFareList)(0)

                Else


                    If (TrackId.Length = 2) Then
                        OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
                        OBRes = obj.FairAvailablity(OBFltDs)
                        IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
                        IBRes = obj.FairAvailablity(IBFltDs)
                        If OBRes = True And IBRes = True Then
                            Return TrackId
                        Else
                            TrackId(0) = "0"
                            Return TrackId
                        End If
                    ElseIf (TrackId.Length = 1) Then
                        OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
                        OBRes = obj.FairAvailablity(OBFltDs)

                        If OBRes = False Then
                            TrackId(0) = "0"
                        End If
                        Return TrackId
                    End If



                End If
                Return TrackId
            Else


                If (TrackId.Length = 2) Then
                    OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
                    OBRes = obj.FairAvailablity(OBFltDs)
                    IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
                    IBRes = obj.FairAvailablity(IBFltDs)
                    If OBRes = True And IBRes = True Then
                        Return TrackId
                    Else
                        TrackId(0) = "0"
                        Return TrackId
                    End If
                ElseIf (TrackId.Length = 1) Then
                    OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
                    OBRes = obj.FairAvailablity(OBFltDs)

                    If OBRes = False Then
                        TrackId(0) = "0"
                    End If
                    Return TrackId
                End If

            End If

        Catch ex As Exception
            TrackId(0) = "0"
            Return TrackId
        Finally
        End Try

    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CheckFareAvailabilityNew(ByVal TrackId As String(), ByRef changeFareList As STD.BAL.CacheRereshRespList, ByVal a As ArrayList, ByVal SegTrip As String) As String()

        Try

            changeFareList = New STD.BAL.CacheRereshRespList()
            changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp()
            changeFareList.ChangeFareR = New STD.BAL.CacheRereshResp()

            'NO. OF RECORDS LIST FOR MULTI STOPS
            Dim obj As New STD.BAL.GALTransanctions()
            Dim OBRes As Boolean = False, IBRes As Boolean = False
            obj.connectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString()
            'Call Function
            Dim objDA As New SqlTransaction
            Dim OBFltDs, IBFltDs As DataSet
            Dim ckCacheDs As DataSet
            Dim Objfcross As New FareCrossCheckForAllProvider(obj.connectionString)

            Dim trackIdO As String = TrackId(0)
            Dim trackIdR As String = ""
            OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            If (TrackId.Length = 2) Then
                trackIdR = TrackId(1)
                IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
            End If

            Dim result As New ArrayList()
            Dim resultRT As New ArrayList()

            '#Region "Check provider LCC or other Provider"
            Dim l1P As Integer
            Dim l2P As Integer
            Dim ListOWP As Object()
            Dim ListRTP As Object()
            Dim Trip As Integer = a.Count
            Dim CheckLccOW As String = "false"
            Dim CheckLccRT As String = "false"
            Dim Provider As String = "", Ftype As String = "", VC As String = ""
            Dim ProviderRT As String = "", FtypeRT As String = "", VCRT As String = ""

            If Trip = 2 Then
                'LIST-1
                ListOWP = DirectCast(a(0), Object())
                l1P = ListOWP.Length
                'For i As Integer = 0 To l1 - 1
                Dim Rec As New Dictionary(Of String, Object)()
                'Rec = DirectCast(ListOW(i), Dictionary(Of String, Object))
                Rec = DirectCast(ListOWP(0), Dictionary(Of String, Object))
                Provider = Rec("Provider")
                VC = Rec("ValiDatingCarrier")
                Ftype = Rec("FType")
                'Next
                If (Provider = "LCC" And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG")) Then
                    CheckLccOW = "true"
                Else
                    CheckLccOW = "false"
                End If

                'LIST-2
                ListRTP = DirectCast(a(1), Object())
                l2P = ListRTP.Length

                ' For j As Integer = 0 To l2 - 1
                Dim RecRT As New Dictionary(Of String, Object)()
                'RecRT = DirectCast(ListRT(j), Dictionary(Of String, Object))
                RecRT = DirectCast(ListRTP(0), Dictionary(Of String, Object))
                ProviderRT = RecRT("Provider")
                VCRT = RecRT("ValiDatingCarrier")
                FtypeRT = RecRT("FType")
                'Next

                If (ProviderRT = "LCC" And (VCRT = "G8" OrElse VCRT = "6E" OrElse VCRT = "SG")) Then
                    CheckLccRT = "true"
                Else
                    CheckLccRT = "false"
                End If
            Else
                ListOWP = DirectCast(a(0), Object())
                Dim Rec As New Dictionary(Of String, Object)()
                'Rec = DirectCast(ListOW(i), Dictionary(Of String, Object))
                Rec = DirectCast(ListOWP(0), Dictionary(Of String, Object))
                Provider = Rec("Provider")
                VC = Rec("ValiDatingCarrier")
                Ftype = Rec("FType")
                'Next
                If (Provider = "LCC" And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG")) Then
                    CheckLccOW = "true"
                Else
                    CheckLccOW = "false"
                End If
            End If
            '#End Region
            'If (Session("Provider") <> "LCC") Then
            If (CheckLccOW = "false" And CheckLccRT = "false") Then
                If (TrackId.Length = 2) Then
                    result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, True, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0", False)
                Else
                    result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, False, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0", False)

                End If
                TrackId = Insert_Selected_FltDetails_live(result, TrackId, changeFareList)
            ElseIf (Provider = "LCC" And SegTrip.ToUpper = "I" And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX")) Then
                ' (Session("Provider") = "LCC" And Session("SegTrip") = "I")
                TrackId = Insert_Selected_FltDetails_live(result, TrackId, changeFareList)
            Else
                '' For Lcc Devesh
                '' Try
                Dim l1 As Integer
                Dim l2 As Integer
                Dim ListOW As Object()
                Dim ListRT As Object()
                'Dim Trip As Integer = a.Count
                Dim Tracks As String() = Nothing
                Dim diff As String = "0"
                Dim diffz As String = "0"
                Dim NewTrackId As String = "0"
                Dim CheckReprice As String = "false"

                'For 2 Trips Calculate Results 
                If Trip = 2 Then
                    Tracks = New String(1) {}
                    'INSERT FOR BOTH
                    'LIST-1
                    ListOW = DirectCast(a(0), Object())
                    l1 = ListOW.Length
                    ''Tracks(0) = Insert_REC_Details(ListOW, l1, "D", a, "", diff)
                    If (Provider = "LCC" And SegTrip.ToUpper = "D" And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX")) Then
                        'result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, True, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0")
                        result = CheckFareAvailabilityLCC(ListOW, l1, TrackId(0), diff, "false", changeFareList)   ' Use for New Fare Check
                    Else
                        result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, False, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0", False)
                    End If

                    'LIST-2
                    ListRT = DirectCast(a(1), Object())
                    l2 = ListRT.Length
                    diffz = diff
                    diff = "0"
                    If (ProviderRT = "LCC" And SegTrip.ToUpper = "D" And (VCRT = "G8" OrElse VCRT = "6E" OrElse VCRT = "SG" OrElse VCRT = "IX")) Then
                        'result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, True, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0")
                        resultRT = CheckFareAvailabilityLCC(ListRT, l2, TrackId(1), diff, "true", changeFareList)  ' Use for New Fare Check
                    Else
                        Dim changeFareListInbound As New STD.BAL.CacheRereshRespList()
                        'changeFareListInbound.ChangeFareO = New STD.BAL.CacheRereshResp()
                        'changeFareListInbound.ChangeFareR = New STD.BAL.CacheRereshResp()
                        result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, True, changeFareListInbound, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0", True)
                        Dim FareChangeO As CacheRereshResp = New CacheRereshResp()
                        Dim FareChangeR As CacheRereshResp = New CacheRereshResp()
                        FareChangeR.CacheNetFare = changeFareListInbound.ChangeFareR.CacheNetFare
                        FareChangeR.CacheTotFare = changeFareListInbound.ChangeFareR.CacheTotFare
                        FareChangeR.NewNetFare = changeFareListInbound.ChangeFareR.NewNetFare
                        FareChangeR.NewTotFare = changeFareListInbound.ChangeFareR.NewTotFare
                        changeFareList.ChangeFareR = FareChangeR
                    End If
                    TrackId = Insert_Selected_FltDetails_live(a, TrackId, changeFareList)                      ' Use for New Fare Check
                Else
                    Tracks = New String(0) {}
                    'INSER FOR ONE ONLY
                    ListOW = DirectCast(a(0), Object())
                    l1 = ListOW.Length
                    If (Provider = "LCC" And SegTrip.ToUpper = "D" And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX")) Then
                        result = CheckFareAvailabilityLCC(ListOW, l1, TrackId(0), diff, "false", changeFareList)
                        'Else
                        '    result = Objfcross.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, False, changeFareList, a, Session("UID"), "SPRING", Context.Session("agent_type").ToString(), "0")
                    End If
                    TrackId = Insert_Selected_FltDetails_live(a, TrackId, changeFareList)
                End If

                ''End Lcc


            End If


            'TrackId = Insert_Selected_FltDetails_live(result, TrackId, changeFareList)

            Return TrackId



            'If OBFltDs.Tables(0).Rows(0)("Trip").ToString() = "D" Then


            '    ckCacheDs = objDA.GetCacheStatusForOrderIds(trackIdO, trackIdR)
            '    Dim rowO As DataRow() = ckCacheDs.Tables(0).Select("orderID='" & trackIdO & "'")
            '    Dim rowR As DataRow() = If(trackIdR <> "", ckCacheDs.Tables(0).Select("orderID='" & trackIdR & "'"), Nothing)
            '    Dim objCache As New STD.BAL.FltSearchForCacheResult(obj.connectionString)
            '    Dim isNRMLRndTRip As Boolean = False

            '    If rowO(0)("IsCache") = True AndAlso (rowR IsNot Nothing AndAlso rowR(0)("IsCache") = True) Then
            '        isNRMLRndTRip = True
            '        result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, isNRMLRndTRip, changeFareList)
            '        TrackId = Insert_Selected_FltDetails_live(result, TrackId)

            '    ElseIf rowO(0)("IsCache") = True Then

            '        Dim tackid_old As String() = New String(1) {}
            '        tackid_old(0) = TrackId(0)
            '        tackid_old(1) = TrackId(0)
            '        result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), OBFltDs, IBFltDs, isNRMLRndTRip, changeFareList)
            '        TrackId(0) = Insert_Selected_FltDetails_live(result, tackid_old)(0)

            '    ElseIf rowR IsNot Nothing AndAlso rowR(0)("IsCache") = True Then
            '        Dim tackid_old As String() = New String(1) {}
            '        tackid_old(0) = TrackId(1)
            '        tackid_old(1) = TrackId(1)
            '        result = objCache.GetFltResult(Session("UID"), HttpContext.Current.Session("User_Type"), IBFltDs, OBFltDs, isNRMLRndTRip, changeFareList)
            '        changeFareList.ChangeFareR = changeFareList.ChangeFareO
            '        changeFareList.ChangeFareO = New STD.BAL.CacheRereshResp
            '        TrackId(1) = Insert_Selected_FltDetails_live(result, tackid_old)(0)

            '    Else


            '        If (TrackId.Length = 2) Then
            '            OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            '            OBRes = obj.FairAvailablity(OBFltDs)
            '            IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
            '            IBRes = obj.FairAvailablity(IBFltDs)
            '            If OBRes = True And IBRes = True Then
            '                Return TrackId
            '            Else
            '                TrackId(0) = "0"
            '                Return TrackId
            '            End If
            '        ElseIf (TrackId.Length = 1) Then
            '            OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            '            OBRes = obj.FairAvailablity(OBFltDs)

            '            If OBRes = False Then
            '                TrackId(0) = "0"
            '            End If
            '            Return TrackId
            '        End If



            '    End If
            '    Return TrackId
            'Else


            '    If (TrackId.Length = 2) Then
            '        OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            '        OBRes = obj.FairAvailablity(OBFltDs)
            '        IBFltDs = objDA.GetFltDtls(TrackId(1), Session("UID"))
            '        IBRes = obj.FairAvailablity(IBFltDs)
            '        If OBRes = True And IBRes = True Then
            '            Return TrackId
            '        Else
            '            TrackId(0) = "0"
            '            Return TrackId
            '        End If
            '    ElseIf (TrackId.Length = 1) Then
            '        OBFltDs = objDA.GetFltDtls(TrackId(0), Session("UID"))
            '        OBRes = obj.FairAvailablity(OBFltDs)

            '        If OBRes = False Then
            '            TrackId(0) = "0"
            '        End If
            '        Return TrackId
            '    End If

            'End If

        Catch ex As Exception
            TrackId(0) = "0"
            Return TrackId
        Finally
        End Try

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function Insert_REC_Details(ByVal List As Object(), ByVal l1 As Integer, ByVal Trip As String, ByVal a As ArrayList, ByVal TrackIdOld As String, ByRef diffAmount As String) As String

        Dim TID As String
        Dim Inserted As Boolean = False
        Dim TRANSID As String = "", RETVAL As String = ""
        Dim Provider As String = "", Ftype As String = "", VC As String = ""
        UID = HttpContext.Current.Session("UID").ToString()
        Try
            'STEP-1 FETCH DATA FROM EACH LIST FOR EACH RECORD
            'Each List List1 and List2 can Have Multiple Records(i.e Records for Multiple Stop)
            'GENRATE TRANSID -AND SEND SAME TRANSACTION ID FOR OUTBOUND(OR INBOUND) - RECORDS OF SAME SIDE WITH MULTIPLE STOPS
            Dim RK As New RandomKeyGenerator()
            Dim FareBreak_Row As New Hashtable()
            TRANSID = "TP" + DateTime.Now.ToString("yyMMddHHmmssff") 'RK.Generate()
            'TRANSID = "FW" + TRANSID
            Dim RecWrapper As New ArrayList()
            ' Dim FareData As New Hashtable()
            For i As Integer = 0 To l1 - 1
                'Fetch Each Record in the List
                Dim Rec As New Dictionary(Of String, Object)()
                Rec = DirectCast(List(i), Dictionary(Of String, Object))
                Provider = Rec("Provider")
                VC = Rec("ValiDatingCarrier")
                Session("Provider") = Provider
                Ftype = Rec("FType")
                Rec("User_id") = Session("UID").ToString()
                If (Rec.ContainsKey("Track_Old")) Then
                    Rec("Track_Old") = Convert.ToString(TrackIdOld)
                Else
                    Rec("Track_Old") = Convert.ToString(TrackIdOld)
                End If
                'Rec.Add("Track_Old", TrackIdOld)
                RecWrapper.Add(Rec)
            Next

            'Process Each List Separately
            'List 1 Starts - For One Way - Fetch Each Record  
            ' EACH RECORD IN LIST IS A DICTIONARY
            '' If Trip = "I" Or Trip = "D" Then

            Dim FinalList_Gal As New ArrayList()
            If (Provider = "FDD") Then

                FinalList_Gal = RecWrapper

                For i As Integer = 0 To FinalList_Gal.Count - 1
                    '   Fetch Each Record in the List
                    Dim Rec As New Dictionary(Of String, Object)()
                    Rec = DirectCast(List(i), Dictionary(Of String, Object))
                    '    'STEP -2 Send each Record Directly to BAL-then-DAL
                    Dim obj As New FlightSearchBAL()
                    Inserted = obj.ReturnTID(Rec, TRANSID, TID, Trip)
                Next

            Else
                Dim FareData As New Dictionary(Of String, Object)
                Dim ObjCommBal As New STD.BAL.FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)

                If Trip = "I" Then
                    FareData = ObjCommBal.GetIntlCommGal(Convert.ToString(HttpContext.Current.Session("agent_type")), RecWrapper, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))
                ElseIf Trip = "D" Then
                    FareData = ObjCommBal.GetDomCommGal(Convert.ToString(HttpContext.Current.Session("agent_type")), RecWrapper, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))

                End If
                FinalList_Gal = ObjCommBal.MereFare_GalIntl(RecWrapper, FareData)
                'SpicJet Code START
                Dim SellAmount As String = "0.0"
                If (Provider = "LCC" And Trip = "III") Then
                    If (VC = "SG") Then
                        FinalList_Gal.Clear()
                        FinalList_Gal = Spicejet_SellAmt(List, diffAmount)
                    ElseIf VC = "6E" Then
                        FinalList_Gal.Clear()
                        FinalList_Gal = Indigo_SellAmt(List, diffAmount)
                    End If
                End If
                'SpicJet Code END
                For i As Integer = 0 To FinalList_Gal.Count - 1
                    '   Fetch Each Record in the List
                    Dim Rec As New Dictionary(Of String, Object)()
                    Rec = DirectCast(List(i), Dictionary(Of String, Object))
                    '    'STEP -2 Send each Record Directly to BAL-then-DAL
                    Dim obj As New FlightSearchBAL()
                    Inserted = obj.ReturnTID(Rec, TRANSID, TID, Trip)
                Next
            End If
            'Else
            '    If (Provider = "LCC" And Ftype = "RTF") Then
            '        If (VC = "SG") Then
            '            Spicejet_SellAmt(List)
            '        ElseIf VC = "6E" Then
            '            Indigo_SellAmt(List)
            '        End If
            '    End If
            '    For i As Integer = 0 To l1 - 1
            '        Dim Rec As New Dictionary(Of String, Object)()
            '        'Fetch Each Record in the List
            '        Rec = DirectCast(List(i), Dictionary(Of String, Object))
            '        'STEP -2 Send each Record Directly to BAL-then-DAL
            '        Dim obj As New FlightSearchBAL()
            '        'DATA Inserted
            '        Inserted = obj.ReturnTID(Rec, TRANSID, TID, Trip)
            '    Next

            'End If

            If Inserted = True Then
                RETVAL = TRANSID
            Else
                RETVAL = "Error"
            End If
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightSearch");
        Catch ex As Exception
        End Try
        Return RETVAL
    End Function

    '<WebMethod(EnableSession:=True)> _
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    'Public Function Insert_REC_Details(ByVal List As Object(), ByVal l1 As Integer, ByVal Trip As String) As String
    '    Dim Rec As New Dictionary(Of String, Object)()
    '    Dim TID As String
    '    Dim Inserted As Boolean = False
    '    Dim TRANSID As String = "", RETVAL As String = ""
    '    Try
    '        'STEP-1 FETCH DATA FROM EACH LIST FOR EACH RECORD
    '        'Each List List1 and List2 can Have Multiple Records(i.e Records for Multiple Stop)
    '        'GENRATE TRANSID -AND SEND SAME TRANSACTION ID FOR OUTBOUND(OR INBOUND) - RECORDS OF SAME SIDE WITH MULTIPLE STOPS
    '        Dim RK As New RandomKeyGenerator()
    '        Dim FareBreak_Row As New Hashtable()
    '        TRANSID = RK.Generate()
    '        Dim RecWrapper As New ArrayList()
    '        Dim FareData As New Hashtable()
    '        For i As Integer = 0 To l1 - 1
    '            'Fetch Each Record in the List
    '            Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '            Rec.Add("User_id", Session("UID").ToString())
    '            RecWrapper.Add(Rec)
    '        Next
    '        'FareData = FareBreakup(RecWrapper, Trip);

    '        'Process Each List Separately
    '        'List 1 Starts - For One Way - Fetch Each Record  
    '        ' EACH RECORD IN LIST IS A DICTIONARY
    '        For i As Integer = 0 To l1 - 1
    '            'Fetch Each Record in the List
    '            Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '            'STEP -2 Send each Record Directly to BAL-then-DAL
    '            Dim obj As New FlightSearchBAL()
    '            'DATA Inserted
    '            Inserted = obj.ReturnTID(Rec, TRANSID, TID, FareData)
    '        Next
    '        If Inserted = True Then
    '            RETVAL = TRANSID
    '        Else
    '            RETVAL = "Error"
    '        End If
    '        'ExceptionLogger.FileHandling("FlightSearchService", "Err_001", ex, "FlightSearch");
    '    Catch ex As Exception
    '    End Try
    '    Return RETVAL
    'End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CalFareRule(ByVal AirArray As ArrayList, ByVal Trip As String) As Hashtable
        Try
            'NO. OF RECORDS LIST FOR MULTI STOPS
            Dim obj As New STD.BAL.GALTransanctions()
            Dim AdtFR As String, ChdFR As String, InfR As String
            Dim Rec As New Dictionary(Of String, Object)()
            obj.connectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString.ToString()
            Dim FR As New List(Of FareRule)()
            Rec = DirectCast(AirArray(0), Dictionary(Of String, Object))
            If Rec("ChdFar") Is Nothing Then
                ChdFR = ""
            Else
                ChdFR = Rec("ChdFar").ToString()
            End If
            If Rec("InfFar") Is Nothing Then
                InfR = ""
            Else
                InfR = Rec("InfFar").ToString()
            End If

            FR = obj.GetFareRule(Rec("AdtFar").ToString(), ChdFR, InfR, Convert.ToInt32(Rec("Adult")), Convert.ToInt32(Rec("Child")), Convert.ToInt32(Rec("Infant")))
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_003", ex, "FlightSearch");
        Catch ex As Exception
        Finally
        End Try
        Return AirResult
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FareBreakupGAL1(ByVal AirArray As String, ByVal Trip As String) As ArrayList

        Dim Air As New ArrayList()
        Try
            '' Dim strarray As String = LZCompression.LZString.DecompressFromUTF16(AirArray)
            Dim x As Object = JsonConvert.DeserializeObject(Of Object)(AirArray)
            Dim a As ArrayList = Newtonsoft.Json.Linq.JArray.Parse(AirArray).ToObject(Of ArrayList)()
            '' Dim y As ArrayList() = JsonConvert.DeserializeObject(strarray)
            'Dim list As List(Of MyObject) = New List(Of MyOjbect)
            'list.Add(MyObject)

            Dim newArr As ArrayList = New ArrayList()
            For index As Integer = 0 To a.Count - 1
                newArr.Add(Newtonsoft.Json.Linq.JObject.Parse(a(index).ToString()).ToObject(Of Dictionary(Of String, Object))())
            Next
            'Dim Rec As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            'Dim list As ArrayList = New ArrayList
            'list.Add(x)

            Dim PV As String = newArr(0)("Provider").ToString()
            If (PV = "FDD") Then
                Air = newArr
            Else

                Air = FareBreakupGAL(newArr, Trip)
            End If
            ' Dim objArry As ArrayList = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeObject(strarray), JSONArray)
            ' Air = FareBreakupGAL(objArry, Trip)

        Catch ex As Exception

        Finally
        End Try
        Return Air

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function FareBreakupGAL(ByVal AirArray As ArrayList, ByVal Trip As String) As ArrayList
        Dim fbStr As String = ""
        Dim Ln As String = AirArray(0)("LineNumber").ToString()
        Dim Air As New ArrayList()
        Try
            'NO. OF RECORDS LIST FOR MULTI STOPS

            UID = HttpContext.Current.Session("UID").ToString()
            UserType = Session("UserType").ToString()
            TypeId = Session("TypeId").ToString()
            GroupType = HttpContext.Current.Session("agent_type").ToString()
            Dim ObjCommBal As New STD.BAL.FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim IntFareDetails As New Dictionary(Of String, Object)
            If Trip = "I" Then
                IntFareDetails = ObjCommBal.GetIntlCommGal(GroupType, AirArray, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))

            ElseIf Trip = "D" Then
                IntFareDetails = ObjCommBal.GetDomCommGal(GroupType, AirArray, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))

            End If
            'Dim NetFare As String = GetAirlineNetFare(AirArray, Trip)     'Get Net Fare Devesh 

            Air = ObjCommBal.MereFare_GalIntl(AirArray, IntFareDetails)
            Dim adt As Integer = 0, chd As Integer = 0, inf As Integer = 0

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_003", ex, "FlightSearch")
        Finally
        End Try
        Return Air
        'Return fbStr
    End Function

    <WebMethod(EnableSession:=True)>
    Public Function sendEnqMailTo(ByVal emailFrom As String, ByVal emailTo As String, ByVal divhtml As String, ByVal subj As String, ByVal msg As String) As String
        'Dim uniqueOrderId As String = ""
        'Set page size as A4
        Dim status As Integer = 0
        Dim pdfDoc As New Document(PageSize.A4, 10, 10, 10, 10)
        Try

            Dim htmldivtodoc As String = Server.UrlDecode(divhtml)

            'Dim pdfPath As String = "E:\BookingDetails_" & DateTime.Now.ToString("dd_mm_yyyy") & ".pdf"
            'PdfWriter.GetInstance(pdfDoc, New FileStream(pdfPath, FileMode.Create))
            ''Open PDF Document to write data
            'pdfDoc.Open()
            ''string[] strValues = Request["hdndivPDF"].ToString().Split("");
            ''Read string contents using stream reader and convert html to parsed content. 
            'Dim parsedHtmlElements = HTMLWorker.ParseToList(New StringReader(htmldivtopdf), Nothing)
            ''Get each array values from parsed elements and add to the PDF document
            'For Each htmlElement In parsedHtmlElements
            '    pdfDoc.Add(TryCast(htmlElement, IElement))
            'Next
            ''Close your PDF
            'pdfDoc.Close()


            Dim strBuilder As New StringBuilder()
            strBuilder.Append(htmldivtodoc.ToString())
            'string path = @"C:\Test.doc";

            Dim strPath As String = "D:\FlightDetails" & DateAndTime.Now.ToString.Replace(":", "").Trim & ".doc"
            strPath = strPath.Replace("/", "-").Replace(" ", "")
            Dim thisfile As New FileInfo(strPath)
            If thisfile.Exists = True Then
                File.Delete(strPath)
            End If
            'string strTextToWrite = TextBox1.Text;
            Dim fStream As FileStream = File.Create(strPath)
            fStream.Close()
            Dim sWriter As New StreamWriter(strPath)
            sWriter.Write(strBuilder)
            sWriter.Close()

            Try
                Dim STDOM As New SqlTransactionDom
                Dim MailDt As New DataTable
                MailDt = STDOM.GetMailingDetails(MAILING.AIR_FLIGHTRESULT.ToString(), HttpContext.Current.Session("UID").ToString()).Tables(0)
                If (MailDt.Rows.Count > 0) Then
                    Dim Status_mail As Boolean = False
                    Status_mail = Convert.ToBoolean(MailDt.Rows(0)("Status").ToString())
                    If Status_mail = True Then
                        Dim i As Integer = STDOM.SendMail(emailTo, emailFrom, MailDt.Rows(0)("BCC").ToString(), MailDt.Rows(0)("CC").ToString(), MailDt.Rows(0)("SMTPCLIENT").ToString(), MailDt.Rows(0)("UserId").ToString(), MailDt.Rows(0)("Pass").ToString(), msg, subj, strPath)
                        If i = 1 Then
                            status = 1
                        Else
                            status = 0
                        End If
                    End If
                End If
            Catch ex As Exception
                status = 0
            End Try



            Dim thisfile1 As New FileInfo(strPath)
            If thisfile1.Exists = True Then
                File.Delete(strPath)
            End If
            'End If
        Catch ex As Exception
            'WriteToEventLog(ex)dt = sql.GetAgencyDetails(Session("UID").ToString()).Tables(0)

        End Try
        If status = 1 Then
            Return "Success"
        Else
            Return "Failure"
        End If

    End Function

    <WebMethod(EnableSession:=True)>
    Public Function getAgncyDet() As String
        Dim dt As New DataTable()
        dt = sql.GetAgencyDetails(Session("UID").ToString()).Tables(0)
        Dim agcStr As String = "<div style='width:600px'>"
        Dim filePath As String = Server.MapPath("~\\AgentLogo") + "\\" + Session("UID").ToString() + ".jpg"
        If File.Exists(filePath) Then

            agcStr = agcStr & "<div style='float:left;width:300px' align='left'><img id='imgAgcLogo' src='http://RWT.co/AgentLogo/" + Session("UID").ToString() + ".jpg' alt=''/></div>"
        Else
        End If
        agcStr = agcStr & "<div align='right' style='float:right;font-family:Times New Roman;width:300px'><b style='float:left'>" & dt.Rows(0)("Agency_Name") & "</b><br/><b style='font-size:12px;float:left'>" & dt.Rows(0)("Address") & "</b></div>"
        agcStr = agcStr & "</div>"
        Return agcStr
    End Function

    'Public Function Spicejet_SellAmt(ByVal List As Object()) As ArrayList
    '    Dim objSql As New SqlTransactionNew
    '    Dim dsCrd As DataSet = objSql.GetCredentials("SG")
    '    Dim objInputs As New STD.Shared.FlightSearch()
    '    'objInputs = DirectCast(Session("search"), STD.Shared.FlightSearch)
    '    Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "")
    '    Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = ""
    '    Dim Diff As Decimal = 0.0, DiffPax = 0.0
    '    Dim cnt As Short = 0

    '    'If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
    '    '    cnt = 1
    '    'End If
    '    Dim FinalList_Gal As New ArrayList()
    '    Try
    '        If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
    '        If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
    '            cnt = 1
    '        End If
    '        Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
    '        For i As Integer = 0 To List.Count - 1
    '            Dim Rec As New Dictionary(Of String, Object)()
    '            Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '            'If i = List.Count - 1 Then
    '            '    If Rec("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
    '            'End If

    '            If (i = 0) Then
    '                Org = Rec("OrgDestFrom").ToString()
    '                If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
    '                objInputs.Adult = Rec("Adult")
    '                objInputs.Child = Rec("Child")
    '                objInputs.Infant = Rec("Infant")
    '                VC = Rec("ValiDatingCarrier")
    '            End If
    '            If (Org = Rec("OrgDestFrom").ToString()) Then
    '                JSK(0) = Rec("sno")
    '                FSK(0) = Rec("Searchvalue")
    '                CC(0) = "SG"
    '                FNO(0) = Rec("FlightIdentification")
    '                DD(0) = Rec("depdatelcc")
    '                OriginalTF = Rec("OriginalTF")
    '            ElseIf (Org = Rec("OrgDestTo").ToString()) Then
    '                JSK(1) = Rec("sno")
    '                FSK(1) = Rec("Searchvalue")
    '                CC(1) = "SG"
    '                FNO(1) = Rec("FlightIdentification")
    '                DD(1) = Rec("depdatelcc")
    '            End If
    '            FinalList_Gal.Add(Rec)
    '        Next
    '        If VC = "SG" Then
    '            Signature = objSg.Spice_Login()
    '            Dim Req, Res As String
    '            Dim xml As New Dictionary(Of String, String)
    '            SellAmt = objSg.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml)
    '            If SellAmt <> "FAILURE" Then
    '                objSg.Spice_Logout(Signature)
    '            End If
    '            'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
    '        End If
    '    Catch ex As Exception

    '    End Try
    '    If (VC = "SG") Then
    '        Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
    '        DiffPax = Math.Round(Diff / (objInputs.Adult + objInputs.Child))
    '        If (Diff > 0) Then
    '            FinalList_Gal.Clear()
    '            For i As Integer = 0 To List.Count - 1
    '                Dim Rec As New Dictionary(Of String, Object)()
    '                Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '                Org = Rec("OrgDestFrom").ToString()
    '                If (Org = Rec("OrgDestFrom").ToString()) Then
    '                    If (objInputs.Adult > 0) Then
    '                        Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '                        Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
    '                    End If
    '                    If (objInputs.Child > 0) Then
    '                        Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
    '                        Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
    '                    End If
    '                    Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff
    '                    Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff
    '                    Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff
    '                    Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '                ElseIf (Org = Rec("OrgDestTo").ToString()) Then
    '                    If (objInputs.Adult > 0) Then
    '                        Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '                    End If
    '                    If (objInputs.Child > 0) Then
    '                        Rec("ChdTax") = Rec("ChdTax") + DiffPax
    '                    End If
    '                    Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff
    '                    Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff
    '                    Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff
    '                    Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '                End If
    '                FinalList_Gal.Add(Rec)
    '            Next
    '        End If
    '    End If
    '    Return FinalList_Gal
    'End Function
    'Public Function Spicejet_SellAmt(ByVal List As Object()) As ArrayList
    '    Dim objSql As New SqlTransactionNew
    '    Dim dsCrd As DataSet = objSql.GetCredentials("SG")
    '    Dim objInputs As New STD.Shared.FlightSearch()
    '    'objInputs = DirectCast(Session("search"), STD.Shared.FlightSearch)
    '    'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "")
    '    Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = ""
    '    Dim Diff As Decimal = 0.0, DiffPax = 0.0, Diff1 = 0.0
    '    Dim cnt As Short = 0

    '    'If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
    '    '    cnt = 1
    '    'End If
    '    Dim FinalList_Gal As New ArrayList()
    '    Try
    '        If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
    '        If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
    '            cnt = 1
    '        End If
    '        Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
    '        For i As Integer = 0 To List.Count - 1
    '            Dim Rec As New Dictionary(Of String, Object)()
    '            Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '            'If i = List.Count - 1 Then
    '            '    If Rec("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
    '            'End If

    '            If (i = 0) Then
    '                Org = Rec("OrgDestFrom").ToString()
    '                If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
    '                objInputs.Adult = Rec("Adult")
    '                objInputs.Child = Rec("Child")
    '                objInputs.Infant = Rec("Infant")
    '                VC = Rec("ValiDatingCarrier")
    '            End If
    '            If (Org = Rec("OrgDestFrom").ToString()) Then
    '                JSK(0) = Rec("sno")
    '                FSK(0) = Rec("Searchvalue")
    '                CC(0) = "SG"
    '                FNO(0) = Rec("FlightIdentification")
    '                DD(0) = Rec("depdatelcc")
    '                OriginalTF = Rec("OriginalTF")
    '            ElseIf (Org = Rec("OrgDestTo").ToString()) Then
    '                JSK(1) = Rec("sno")
    '                FSK(1) = Rec("Searchvalue")
    '                CC(1) = "SG"
    '                FNO(1) = Rec("FlightIdentification")
    '                DD(1) = Rec("depdatelcc")
    '            End If
    '            FinalList_Gal.Add(Rec)
    '        Next
    '        If VC = "SG" Then
    '            Signature = objSg.Spice_Login()
    '            Dim Req, Res As String
    '            Dim xml As New Dictionary(Of String, String)
    '            SellAmt = objSg.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml)
    '            If SellAmt <> "FAILURE" Then
    '                objSg.Spice_Logout(Signature)
    '            End If
    '            'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
    '        End If
    '    Catch ex As Exception

    '    End Try
    '    If (VC = "SG") Then
    '        Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
    '        Diff1 = 0 'Math.Round(Diff, 0)            
    '        DiffPax = Math.Round((Math.Round(Diff, 0) / (objInputs.Adult + objInputs.Child)), 0)
    '        Diff1 = Diff1 + (DiffPax * objInputs.Adult)
    '        Diff1 = Diff1 + (DiffPax * objInputs.Child)
    '        If (Diff > 0) Then
    '            FinalList_Gal.Clear()
    '            For i As Integer = 0 To List.Count - 1
    '                Dim Rec As New Dictionary(Of String, Object)()
    '                Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '                If (objInputs.Adult > 0) Then
    '                    Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '                    Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
    '                    Rec("AdtFare") = Convert.ToDecimal(Rec("AdtFare").ToString()) + DiffPax
    '                End If
    '                If (objInputs.Child > 0) Then
    '                    Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
    '                    Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
    '                    Rec("ChdFare") = Convert.ToDecimal(Rec("ChdFare").ToString()) + DiffPax
    '                End If
    '                Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
    '                Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
    '                Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
    '                Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '                'Org = Rec("OrgDestFrom").ToString()
    '                'If (Org = Rec("OrgDestFrom").ToString()) Then

    '                'ElseIf (Org = Rec("OrgDestTo").ToString()) Then
    '                '    If (objInputs.Adult > 0) Then
    '                '        Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '                '    End If
    '                '    If (objInputs.Child > 0) Then
    '                '        Rec("ChdTax") = Rec("ChdTax") + DiffPax
    '                '    End If
    '                '    Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
    '                '    Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
    '                '    Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
    '                '    Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '                'End If
    '                FinalList_Gal.Add(Rec)
    '            Next
    '            'Diff1 = Math.Round(Diff, 0)
    '            'DiffPax = Math.Round((Diff / (objInputs.Adult + objInputs.Child)), 0)
    '            'If (Diff > 0) Then
    '            '    FinalList_Gal.Clear()
    '            '    For i As Integer = 0 To List.Count - 1
    '            '        Dim Rec As New Dictionary(Of String, Object)()
    '            '        Rec = DirectCast(List(i), Dictionary(Of String, Object))
    '            '        Org = Rec("OrgDestFrom").ToString()
    '            '        If (Org = Rec("OrgDestFrom").ToString()) Then
    '            '            If (objInputs.Adult > 0) Then
    '            '                Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '            '                Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
    '            '            End If
    '            '            If (objInputs.Child > 0) Then
    '            '                Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
    '            '                Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
    '            '            End If
    '            '            Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
    '            '            Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
    '            '            Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
    '            '            Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '            '        ElseIf (Org = Rec("OrgDestTo").ToString()) Then
    '            '            If (objInputs.Adult > 0) Then
    '            '                Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
    '            '            End If
    '            '            If (objInputs.Child > 0) Then
    '            '                Rec("ChdTax") = Rec("ChdTax") + DiffPax
    '            '            End If
    '            '            Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
    '            '            Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
    '            '            Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
    '            '            Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
    '            '        End If
    '            '        FinalList_Gal.Add(Rec)
    '            '    Next
    '        End If
    '    End If
    '    Return FinalList_Gal
    'End Function

    Public Function Spicejet_SellAmt(ByVal List As Object(), ByRef diffamount As String) As ArrayList
        Dim a As Dictionary(Of String, Object) = DirectCast(List(0), Dictionary(Of String, Object))
        Dim objSql As New SqlTransactionNew
        Dim dsCrd As DataSet = objSql.GetCredentials("SG", a.Item("AdtFar"), a.Item("Trip"))
        Dim objInputs As New STD.Shared.FlightSearch()
        'objInputs = DirectCast(Session("search"), STD.Shared.FlightSearch)
        'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "")
        'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://sgr3xapi.navitaire.com/SessionManager.svc", "https://sgr3xapi.navitaire.com/BookingManager.svc", 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
        'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
        Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = ""
        Dim Diff As Decimal = 0.0, DiffPax = 0.0, Diff1 = 0.0
        Dim cnt As Short = 0
        ''Dim SFMCON1 As String = ""

        Dim FareTypeSettingsList As List(Of FareTypeSettings)
        Dim FT As String() = Nothing
        Dim PROMOCODE As String = ""
        Try
            If [String].IsNullOrEmpty(a.Item("ElectronicTicketing")) = False Then
                PROMOCODE = Split(a.Item("ElectronicTicketing").ToString(), "/")(0)
            End If
            Dim objfltBal As New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            FareTypeSettingsList = objfltBal.GetFareTypeSettings("", a.Item("Trip").ToString(), "")
            Try
                FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "SG" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim() AndAlso x.IsBagFare = Convert.ToBoolean(a.Item("IsBagFare")) AndAlso x.IsSMEFare = Convert.ToBoolean(a.Item("IsSMEFare"))).ToList()
            Catch ex As Exception
                FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "SG" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim()).ToList()
            End Try
            ' FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "SG" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim()).ToList()
            FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
        Catch ex As Exception

        End Try


        'If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
        '    cnt = 1
        'End If
        Dim FinalList_Gal As New ArrayList()
        Try
            If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                cnt = 1
            End If
            Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
            For i As Integer = 0 To List.Count - 1
                Dim Rec As New Dictionary(Of String, Object)()
                Rec = DirectCast(List(i), Dictionary(Of String, Object))
                'If i = List.Count - 1 Then
                '    If Rec("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
                'End If

                If (i = 0) Then
                    Org = Rec("OrgDestFrom").ToString()
                    If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                    objInputs.Adult = Rec("Adult")
                    objInputs.Child = Rec("Child")
                    objInputs.Infant = Rec("Infant")
                    VC = Rec("ValiDatingCarrier")
                    objInputs.HidTxtAirLine = VC
                End If
                If (Org = Rec("OrgDestFrom").ToString()) Then
                    JSK(0) = Rec("sno")
                    FSK(0) = Rec("Searchvalue")
                    CC(0) = "SG"
                    FNO(0) = Rec("FlightIdentification")
                    DD(0) = Rec("depdatelcc")
                    OriginalTF = Rec("OriginalTF")
                ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                    JSK(1) = Rec("sno")
                    FSK(1) = Rec("Searchvalue")
                    CC(1) = "SG"
                    FNO(1) = Rec("FlightIdentification")
                    DD(1) = Rec("depdatelcc")
                End If
                FinalList_Gal.Add(Rec)
            Next
            If VC = "SG" Then

                Dim RecNew As New Dictionary(Of String, Object)()
                RecNew = DirectCast(List(0), Dictionary(Of String, Object))

                '' If (RecNew("ProductDetailQualifier").ToString().Contains("SFM")) Then
                ''SFMCON1 = "0"
                '' Else
                '' SFMCON1 = "1"

                'Signature = objSg.Spice_Login()
                'Dim Req, Res As String
                'Dim xml As New Dictionary(Of String, String)
                'SellAmt = objSg.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml, FT, PROMOCODE, "BOTH")
                'If SellAmt <> "FAILURE" Then
                '    objSg.Spice_Logout(Signature)
                'End If

                Dim xml As New Dictionary(Of String, String)
                If dsCrd.Tables(0).Rows(0)("ServerIP") = "V4" Then
                    Dim objSGV4 As New SGNAV4(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    Signature = objSGV4.Spice_Login()
                    SellAmt = objSGV4.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml, FT, PROMOCODE, "BOTH")
                    If SellAmt <> "FAILURE" Then
                        objSGV4.Spice_Logout(Signature)
                    End If
                Else
                    Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 0) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                    Signature = objSg.Spice_Login()
                    SellAmt = objSg.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml, FT, PROMOCODE, "BOTH")
                    If SellAmt <> "FAILURE" Then
                        objSg.Spice_Logout(Signature)
                    End If
                End If

                If SellAmt = "FAILURE" Then
                    Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                    Dim Dest As String = RecNew("OrgDestTo").ToString()

                    Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                    Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                    If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                        SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                        SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                    Else
                        SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                    End If

                End If
                'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
            End If
            ''End If

        Catch ex As Exception

        End Try
        If (VC = "SG") Then

            ''If (SFMCON1 = "1") Then
            Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
            Diff1 = 0 'Math.Round(Diff, 0)            
            DiffPax = Math.Round((Math.Round(Diff, 0) / (objInputs.Adult + objInputs.Child)), 0)
            Diff1 = Diff1 + (DiffPax * objInputs.Adult)
            Diff1 = Diff1 + (DiffPax * objInputs.Child)
            If (Diff > 0 OrElse Diff < 0) Then

                Dim RecNew As New Dictionary(Of String, Object)()
                RecNew = DirectCast(List(0), Dictionary(Of String, Object))

                Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                Dim Dest As String = RecNew("OrgDestTo").ToString()

                Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))


                If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                    SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                    SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                Else
                    SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "SG")
                End If


                diffamount = Diff
                FinalList_Gal.Clear()
                For i As Integer = 0 To List.Count - 1
                    Dim Rec As New Dictionary(Of String, Object)()
                    Rec = DirectCast(List(i), Dictionary(Of String, Object))
                    If (objInputs.Adult > 0) Then
                        Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                        Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
                        Rec("AdtFare") = Convert.ToDecimal(Rec("AdtFare").ToString()) + DiffPax
                    End If
                    If (objInputs.Child > 0) Then
                        Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
                        Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
                        Rec("ChdFare") = Convert.ToDecimal(Rec("ChdFare").ToString()) + DiffPax
                    End If
                    Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                    Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                    Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                    Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff


                    ''CMT



                    'Org = Rec("OrgDestFrom").ToString()
                    'If (Org = Rec("OrgDestFrom").ToString()) Then

                    'ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                    '    If (objInputs.Adult > 0) Then
                    '        Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                    '    End If
                    '    If (objInputs.Child > 0) Then
                    '        Rec("ChdTax") = Rec("ChdTax") + DiffPax
                    '    End If
                    '    Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                    '    Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                    '    Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                    '    Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
                    'End If
                    FinalList_Gal.Add(Rec)
                Next
                'Diff1 = Math.Round(Diff, 0)
                'DiffPax = Math.Round((Diff / (objInputs.Adult + objInputs.Child)), 0)
                'If (Diff > 0) Then
                '    FinalList_Gal.Clear()
                '    For i As Integer = 0 To List.Count - 1
                '        Dim Rec As New Dictionary(Of String, Object)()
                '        Rec = DirectCast(List(i), Dictionary(Of String, Object))
                '        Org = Rec("OrgDestFrom").ToString()
                '        If (Org = Rec("OrgDestFrom").ToString()) Then
                '            If (objInputs.Adult > 0) Then
                '                Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                '                Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
                '            End If
                '            If (objInputs.Child > 0) Then
                '                Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
                '                Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
                '            End If
                '            Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                '            Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                '            Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                '            Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
                '        ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                '            If (objInputs.Adult > 0) Then
                '                Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                '            End If
                '            If (objInputs.Child > 0) Then
                '                Rec("ChdTax") = Rec("ChdTax") + DiffPax
                '            End If
                '            Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                '            Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                '            Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                '            Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
                '        End If
                '        FinalList_Gal.Add(Rec)
                '    Next
            End If
        End If
        ''End If

        Return FinalList_Gal
    End Function
    Public Function Indigo_SellAmt(ByVal List As Object(), ByRef diffamount As String) As ArrayList

        'Try
        '    Dim a As Dictionary(Of String, Object) = DirectCast(List(0), Dictionary(Of String, Object))
        'Catch ex As Exception

        'End Try
        Dim a As Dictionary(Of String, Object) = DirectCast(List(0), Dictionary(Of String, Object))
        Dim objSql As New SqlTransactionNew
        Dim dsCrd As DataSet = objSql.GetCredentials("6E", a.Item("AdtFar"), a.Item("Trip"))
        Dim objInputs As New STD.Shared.FlightSearch()
        'objInputs = DirectCast(Session("search"), STD.Shared.FlightSearch)
        'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "")
        'Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", "https://6etestr3xapi.navitaire.com/SessionManager.svc", "https://6etestr3xapi.navitaire.com/BookingManager.svc", 340) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))

        Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = ""
        Dim Diff As Decimal = 0.0, DiffPax = 0.0, Diff1 = 0.0
        Dim cnt As Short = 0
        Dim SFMCON As String = ""
        Dim FareTypeSettingsList As List(Of FareTypeSettings)
        Dim FT As String() = Nothing
        Dim PROMOCODE As String = ""
        Try
            If [String].IsNullOrEmpty(a.Item("ElectronicTicketing")) = False Then
                PROMOCODE = Split(a.Item("ElectronicTicketing").ToString(), "/")(0)
            End If
            Dim objfltBal As New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            FareTypeSettingsList = objfltBal.GetFareTypeSettings("", a.Item("Trip").ToString(), "")
            Try
                FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "6E" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim() AndAlso x.IsBagFare = Convert.ToBoolean(a.Item("IsBagFare")) AndAlso x.IsSMEFare = Convert.ToBoolean(a.Item("IsSMEFare"))).ToList()
            Catch ex As Exception
                FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "6E" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim()).ToList()
            End Try
            'FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = "6E" AndAlso x.Trip = a.Item("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = Convert.ToString(a.Item("AdtFar")).ToUpper().Trim()).ToList()
            FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
        Catch ex As Exception

        End Try

        'If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
        '    cnt = 1
        'End If
        Dim FinalList_Gal As New ArrayList()
        Try
            If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                cnt = 1
            End If
            Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
            For i As Integer = 0 To List.Count - 1
                Dim Rec As New Dictionary(Of String, Object)()
                Rec = DirectCast(List(i), Dictionary(Of String, Object))
                'If i = List.Count - 1 Then
                '    If Rec("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
                'End If

                If (i = 0) Then
                    Org = Rec("OrgDestFrom").ToString()
                    If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                    objInputs.Adult = Rec("Adult")
                    objInputs.Child = Rec("Child")
                    objInputs.Infant = Rec("Infant")
                    VC = Rec("ValiDatingCarrier")
                    objInputs.HidTxtAirLine = VC
                End If
                If (Org = Rec("OrgDestFrom").ToString()) Then
                    JSK(0) = Rec("sno")
                    FSK(0) = Rec("Searchvalue")
                    CC(0) = "6E"
                    FNO(0) = Rec("FlightIdentification")
                    DD(0) = Rec("depdatelcc")
                    OriginalTF = Rec("OriginalTF")
                ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                    JSK(1) = Rec("sno")
                    FSK(1) = Rec("Searchvalue")
                    CC(1) = "6E"
                    FNO(1) = Rec("FlightIdentification")
                    DD(1) = Rec("depdatelcc")
                End If
                FinalList_Gal.Add(Rec)
            Next
            If VC = "6E" Then

                Dim RecNew As New Dictionary(Of String, Object)()
                RecNew = DirectCast(List(0), Dictionary(Of String, Object))

                If (RecNew("ProductDetailQualifier").ToString().Contains("SFM")) Then
                    SFMCON = "0"
                Else
                    SFMCON = "1"
                    Dim Req, Res As String
                    Dim xml As New Dictionary(Of String, String)
                    If dsCrd.Tables(0).Rows(0)("ServerIP") = "V4" Then
                        Dim obj6EV4 As New _6ENAV(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 420) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = obj6EV4.Spice_Login()
                        SellAmt = obj6EV4.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml, FT, PROMOCODE, "BOTH")
                        If SellAmt <> "FAILURE" Then
                            obj6EV4.Spice_Logout(Signature)
                        End If
                    Else
                        Dim objSg As New SpiceAPI(dsCrd.Tables(0).Rows(0)("UserID"), dsCrd.Tables(0).Rows(0)("Password"), dsCrd.Tables(0).Rows(0)("BkgServerUrlOrIP"), ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, dsCrd.Tables(0).Rows(0)("CorporateID"), "", "", "", "BasicHttpBinding_ISessionManager", "BasicHttpBinding_IBookingManager", dsCrd.Tables(0).Rows(0)("LoginID"), dsCrd.Tables(0).Rows(0)("LoginPwd"), 340) ', Convert.ToString(dsCrd.Tables(0).Rows(0)("APISource"))
                        Signature = objSg.Spice_Login()
                        SellAmt = objSg.Spice_SellJourneyByKey(Signature, objInputs, JSK, FSK, xml, FT, PROMOCODE, "BOTH")
                        If SellAmt <> "FAILURE" Then
                            objSg.Spice_Logout(Signature)
                        End If
                    End If

                    If SellAmt = "FAILURE" Then
                        Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                        Dim Dest As String = RecNew("OrgDestTo").ToString()

                        Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                        Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                        If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                            SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                            SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                        Else
                            SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                        End If
                    End If

                End If

                'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
            End If


        Catch ex As Exception

        End Try
        If (VC = "6E") Then

            If (SFMCON = "1") Then
                Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
                Diff1 = 0 'Math.Round(Diff, 0)            
                DiffPax = Math.Round((Math.Round(Diff, 0) / (objInputs.Adult + objInputs.Child)), 0)
                Diff1 = Diff1 + (DiffPax * objInputs.Adult)
                Diff1 = Diff1 + (DiffPax * objInputs.Child)
                If (Diff > 0 OrElse Diff < 0) Then

                    Dim RecNew As New Dictionary(Of String, Object)()
                    RecNew = DirectCast(List(0), Dictionary(Of String, Object))


                    Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                    Dim Dest As String = RecNew("OrgDestTo").ToString()



                    Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                    Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                    If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                        SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                        SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                    Else
                        SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "6E")
                    End If


                    diffamount = Diff
                    FinalList_Gal.Clear()
                    For i As Integer = 0 To List.Count - 1
                        Dim Rec As New Dictionary(Of String, Object)()
                        Rec = DirectCast(List(i), Dictionary(Of String, Object))
                        If (objInputs.Adult > 0) Then
                            Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                            Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
                            Rec("AdtFare") = Convert.ToDecimal(Rec("AdtFare").ToString()) + DiffPax
                        End If
                        If (objInputs.Child > 0) Then
                            Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
                            Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
                            Rec("ChdFare") = Convert.ToDecimal(Rec("ChdFare").ToString()) + DiffPax
                        End If
                        Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                        Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                        Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                        Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
                        FinalList_Gal.Add(Rec)
                    Next
                End If
            End If
        End If
        Return FinalList_Gal
    End Function

    Public Function GoAir_AvailabilityCrossCheck(ByVal List As Object(), ByRef diffamount As String, ByRef changeFareList As STD.BAL.CacheRereshRespList, ByVal IsRoundTrip As String) As ArrayList


        '' changeFareList = New CacheRereshRespList()
        Dim FareChangeO As CacheRereshResp = New CacheRereshResp()
        Dim FareChangeR As CacheRereshResp = New CacheRereshResp()
        'FareChangeO.CacheNetFare = Single.Parse(dsO.Tables(0).Rows(0)("NetFare").ToString())
        'FareChangeO.CacheTotFare = Single.Parse(dsO.Tables(0).Rows(0)("TotFare").ToString())
        'FareChangeO.CacheNetFare = Single.Parse(Rec("NetFare").ToString())
        'FareChangeO.CacheTotFare = Single.Parse(Rec("TotalFare").ToString())
        'String TrackId
        Dim CacheTotFare As String = "0"
        Dim CacheNetFare As String = "0"
        Dim NewTotFare As String = "0"
        Dim NewNetFare As String = "0"


        Dim a As Dictionary(Of String, Object) = DirectCast(List(0), Dictionary(Of String, Object))
        Dim objSql As New SqlTransactionNew
        Dim objInputs As New STD.Shared.FlightSearch()
        Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = ""
        Dim Diff As Decimal = 0.0, DiffPax = 0.0, Diff1 = 0.0
        Dim cnt As Short = 0
        Dim SFMCON As String = ""
        Dim FT As String() = Nothing
        Dim FinalList_Gal As New ArrayList()
        Dim Air As New ArrayList()
        Try
            If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                cnt = 1
            End If
            Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
            For i As Integer = 0 To List.Count - 1
                Dim Rec As New Dictionary(Of String, Object)()
                Rec = DirectCast(List(i), Dictionary(Of String, Object))
                If (i = 0) Then
                    Org = Rec("OrgDestFrom").ToString()
                    If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                    objInputs.Adult = Rec("Adult")
                    objInputs.Child = Rec("Child")
                    objInputs.Infant = Rec("Infant")
                    VC = Rec("ValiDatingCarrier")
                    objInputs.HidTxtAirLine = VC
                End If
                If (Org = Rec("OrgDestFrom").ToString()) Then
                    JSK(0) = Rec("sno")
                    FSK(0) = Rec("Searchvalue")
                    CC(0) = "G8"
                    FNO(0) = Rec("FlightIdentification")
                    DD(0) = Rec("depdatelcc")
                    OriginalTF = Rec("OriginalTF")
                    CacheNetFare = Rec("NetFare").ToString()
                    CacheTotFare = Rec("TotalFare").ToString()
                ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                    JSK(1) = Rec("sno")
                    FSK(1) = Rec("Searchvalue")
                    CC(1) = "G8"
                    FNO(1) = Rec("FlightIdentification")
                    DD(1) = Rec("depdatelcc")
                End If
                FinalList_Gal.Add(Rec)
            Next
            If VC = "G8" Then

                Dim RecNew As New Dictionary(Of String, Object)()
                RecNew = DirectCast(List(0), Dictionary(Of String, Object))

                If (RecNew("ProductDetailQualifier").ToString().Contains("SFM")) Then
                    SFMCON = "0"
                Else
                    SFMCON = "1"
                    'Dim Req, Res As String
                    Dim xml As New Dictionary(Of String, String)
                    'New Code
                    Try
                        UID = HttpContext.Current.Session("UID").ToString()
                        UserType = Session("UserType").ToString()
                        TypeId = Session("TypeId").ToString()
                        GroupType = HttpContext.Current.Session("agent_type").ToString()
                        'Session("UserType") = UserType '' "TA"
                        'Session("TypeID") = TypeId ''"TA1"
                        Dim Obj As New GALWS.LCCFareCrossCheck(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                        Air = Obj.SearchResultAvilabilityLcc(FinalList_Gal, "G8", GroupType, UID, "", UserType, TypeId, JSK, FSK, HttpContext.Current)
                    Catch ex As Exception

                    Finally
                    End Try

                    'If SellAmt = "FAILURE" Then
                    If Air Is Nothing OrElse Air.Count < 1 Then
                        'End If
                        'If SellAmt = "FAILURE" Then
                        Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                        Dim Dest As String = RecNew("OrgDestTo").ToString()

                        Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                        Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                        If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                            SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                            SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                        Else
                            SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                        End If
                    End If

                End If

                'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
            End If


        Catch ex As Exception

        End Try
        If (VC = "G8" AndAlso Air IsNot Nothing AndAlso Air.Count > 0) Then
            SellAmt = Air(0)(0).OriginalTF
            If (SFMCON = "1") Then
                Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
                Diff1 = 0 'Math.Round(Diff, 0)            
                DiffPax = Math.Round((Math.Round(Diff, 0) / (objInputs.Adult + objInputs.Child)), 0)
                Diff1 = Diff1 + (DiffPax * objInputs.Adult)
                Diff1 = Diff1 + (DiffPax * objInputs.Child)
                If (Diff > 0 OrElse Diff < 0) Then
                    If (IsRoundTrip = "true") Then
                        NewTotFare = Air(0)(0).TotalFare
                        NewNetFare = Air(0)(0).NetFare
                        FareChangeR.CacheNetFare = Single.Parse(CacheNetFare)
                        FareChangeR.CacheTotFare = Single.Parse(CacheTotFare)
                        FareChangeR.NewNetFare = Single.Parse(NewNetFare) + Diff1 'Use for test, after test coment + Diff1
                        FareChangeR.NewTotFare = Single.Parse(NewTotFare) + Diff1 'Use for test, after test coment + Diff1
                        changeFareList.ChangeFareR = FareChangeR
                    Else
                        NewTotFare = Air(0)(0).TotalFare
                        NewNetFare = Air(0)(0).NetFare
                        FareChangeO.CacheNetFare = Single.Parse(CacheNetFare)
                        FareChangeO.CacheTotFare = Single.Parse(CacheTotFare)
                        FareChangeO.NewNetFare = Single.Parse(NewNetFare) + Diff1 'Use for test, after test coment + Diff1
                        FareChangeO.NewTotFare = Single.Parse(NewTotFare) + Diff1 'Use for test, after test coment + Diff1
                        changeFareList.ChangeFareO = FareChangeO
                    End If

                    Dim RecNew As New Dictionary(Of String, Object)()
                    RecNew = DirectCast(List(0), Dictionary(Of String, Object))


                    Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                    Dim Dest As String = RecNew("OrgDestTo").ToString()



                    Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                    Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                    If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                        SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                        SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                    Else
                        SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", "G8")
                    End If


                    diffamount = Diff
                    FinalList_Gal.Clear()
                    For i As Integer = 0 To List.Count - 1
                        Dim Rec As New Dictionary(Of String, Object)()
                        Rec = DirectCast(List(i), Dictionary(Of String, Object))
                        If (objInputs.Adult > 0) Then
                            Rec("AdtTax") = Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                            Rec("AdtOT") = Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
                            Rec("AdtFare") = Convert.ToDecimal(Rec("AdtFare").ToString()) + DiffPax
                        End If
                        If (objInputs.Child > 0) Then
                            Rec("ChdTax") = Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
                            Rec("ChdOT") = Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
                            Rec("ChdFare") = Convert.ToDecimal(Rec("ChdFare").ToString()) + DiffPax
                        End If
                        Rec("TotalTax") = Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1
                        Rec("TotalFare") = Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                        Rec("NetFare") = Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                        Rec("OriginalTF") = Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff
                        FinalList_Gal.Add(Rec)
                    Next
                End If
            End If
        End If
        Return FinalList_Gal
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetActiveAirlineProviders(ByVal org As String, ByVal dest As String, ByVal airline As String, ByVal rtfStatus As String, ByVal trip As String, ByVal DepDate As String, ByVal RetDate As String) As String
        'string org, string dest, string airline, string rtf, string trip, string agentID
        Dim airproviders As String = ""
        Try
            Dim agentID As String = HttpContext.Current.Session("UID").ToString()
            Dim depstra As String() = DepDate.Split("/")
            Dim depdatedd As Date = New Date(depstra(2), depstra(1), depstra(0))
            Dim retstra As String() = RetDate.Split("/")
            Dim retdatedd As Date = New Date(retstra(2), retstra(1), retstra(0))


            Dim fltbal As FlightCommonBAL = New FlightCommonBAL(Con.ConnectionString)
            airproviders = fltbal.GetActiveAirlineProvider(org, dest, airline, rtfStatus, trip, agentID, depdatedd, retdatedd)
            ''airproviders = "6E:TB"
        Catch ex As Exception
            Dim abc = ex.Message.ToString()
        End Try
        Return airproviders
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetActiveAirlineProvidersF(ByVal org As String, ByVal dest As String, ByVal airline As String, ByVal rtfStatus As String, ByVal trip As String, ByVal DepDate As String, ByVal RetDate As String) As String
        'string org, string dest, string airline, string rtf, string trip, string agentID
        Dim airproviders As String = ""
        Try
            Dim agentID As String = HttpContext.Current.Session("UID").ToString()
            Dim depstra As String() = DepDate.Split("/")
            Dim depdatedd As Date = New Date(depstra(2), depstra(1), depstra(0))
            Dim retstra As String() = RetDate.Split("/")
            Dim retdatedd As Date = New Date(retstra(2), retstra(1), retstra(0))


            Dim fltbal As FlightCommonBAL = New FlightCommonBAL(Con.ConnectionString)
            '' airproviders = fltbal.GetActiveAirlineProvider(org, dest, airline, rtfStatus, trip, agentID, depdatedd, retdatedd)
            airproviders = "SS:FDD"
        Catch ex As Exception
            Dim abc = ex.Message.ToString()
        End Try
        Return airproviders
    End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFlightChacheDataList(ByVal obj As FlightSearch) As ArrayList

        Try
            'Note Data from jason is a Table(List) of Dictonary.Each column in table is a dictonary
            UID = HttpContext.Current.Session("UID").ToString() '"MML1";
            UserType = Session("UserType").ToString() '"AGENT";
            TypeId = Session("TypeId").ToString() ' //"TA1";
            OwnerId = UID
            'Session["OwnerId"].ToString();
            If (UID <> "" OrElse UID IsNot Nothing) And (UserType <> "" OrElse UserType IsNot Nothing) Then
                Dim objInputs As New STD.Shared.FlightSearch()
                objInputs.DepartureCity = obj.DepartureCity
                objInputs.ArrivalCity = obj.ArrivalCity
                objInputs.HidTxtDepCity = obj.HidTxtDepCity
                objInputs.HidTxtArrCity = obj.HidTxtArrCity
                ' "BOM,IN";
                objInputs.DepDate = obj.DepDate
                ' "20/07/2012";
                objInputs.RetDate = obj.RetDate
                objInputs.RTF = obj.RTF
                objInputs.GDSRTF = obj.GDSRTF
                'false;
                'objInputs.Trip = obj.;
                'objInputs.TripType = obj.TripType;
                If obj.Trip1 = "D" Then
                    objInputs.Trip = Trip.D
                ElseIf obj.Trip1 = "I" Then
                    objInputs.Trip = Trip.I
                End If
                If obj.TripType1 = "rdbOneWay" Then
                    objInputs.TripType = TripType.OneWay
                    objInputs.RetDate = obj.DepDate
                ElseIf obj.TripType1 = "rdbRoundTrip" Then
                    objInputs.TripType = TripType.RoundTrip
                End If
                objInputs.AirLine = obj.AirLine
                objInputs.HidTxtAirLine = obj.HidTxtAirLine
                objInputs.Adult = obj.Adult
                objInputs.Child = obj.Child
                objInputs.Infant = obj.Infant
                objInputs.NStop = obj.NStop
                ' false;
                objInputs.Cabin = obj.Cabin
                'Session Parameters
                objInputs.DISTRID = "SPRING"
                objInputs.UID = UID
                objInputs.TypeId = TypeId
                ' "FinalResult";
                objInputs.UserType = UserType
                '""; 
                objInputs.TDS = Data.Calc_TDS(Con.ConnectionString, UID)
                objInputs.IsCorp = HttpContext.Current.Session("IsCorp")
                objInputs.AgentType = HttpContext.Current.Session("agent_type")
                objInputs.OwnerId = OwnerId

                Dim chache As New CacheFareUpdate(Con.ConnectionString)
                ReturnList = chache.GetCacheList(objInputs, "NRM")
            Else
                'Redirect to Login
                'Return Nothing
            End If
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_002", ex, "FlightSearch");
        Catch ex As Exception
        Finally
        End Try
        Return ReturnList

        'Return ReturnList
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetFlightChacheDataListNoth(ByVal obj As FlightSearch) As ArrayList

        Try
            'Note Data from jason is a Table(List) of Dictonary.Each column in table is a dictonary
            UID = HttpContext.Current.Session("UID").ToString() '"MML1";
            UserType = Session("UserType").ToString() '"AGENT";
            TypeId = Session("TypeId").ToString() ' //"TA1";
            OwnerId = UID
            'Session["OwnerId"].ToString();
            If (UID <> "" OrElse UID IsNot Nothing) And (UserType <> "" OrElse UserType IsNot Nothing) Then
                Dim objInputs As New STD.Shared.FlightSearch()
                objInputs.DepartureCity = obj.DepartureCity
                objInputs.ArrivalCity = obj.ArrivalCity
                objInputs.HidTxtDepCity = obj.HidTxtDepCity
                objInputs.HidTxtArrCity = obj.HidTxtArrCity
                ' "BOM,IN";
                objInputs.DepDate = obj.DepDate
                ' "20/07/2012";
                objInputs.RetDate = obj.RetDate
                objInputs.RTF = obj.RTF
                objInputs.GDSRTF = obj.GDSRTF
                'false;
                'objInputs.Trip = obj.;
                'objInputs.TripType = obj.TripType;
                If obj.Trip1 = "D" Then
                    objInputs.Trip = Trip.D
                ElseIf obj.Trip1 = "I" Then
                    objInputs.Trip = Trip.I
                End If
                If obj.TripType1 = "rdbOneWay" Then
                    objInputs.TripType = TripType.OneWay
                    objInputs.RetDate = obj.DepDate
                ElseIf obj.TripType1 = "rdbRoundTrip" Then
                    objInputs.TripType = TripType.RoundTrip
                End If
                objInputs.AirLine = obj.AirLine
                objInputs.HidTxtAirLine = obj.HidTxtAirLine
                objInputs.Adult = obj.Adult
                objInputs.Child = obj.Child
                objInputs.Infant = obj.Infant
                objInputs.NStop = obj.NStop
                ' false;
                objInputs.Cabin = obj.Cabin
                'Session Parameters
                objInputs.DISTRID = "SPRING"
                objInputs.UID = UID
                objInputs.TypeId = TypeId
                ' "FinalResult";
                objInputs.UserType = UserType
                '""; 
                objInputs.TDS = Data.Calc_TDS(Con.ConnectionString, UID)
                objInputs.IsCorp = HttpContext.Current.Session("IsCorp")
                objInputs.AgentType = HttpContext.Current.Session("agent_type")
                objInputs.OwnerId = OwnerId

                Dim chache As New CacheFareUpdate(Con.ConnectionString)
                ''ReturnList = chache.GetCacheListNoth(objInputs, "NRM")

            Else
                'Redirect to Login
                'Return Nothing
            End If
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_002", ex, "FlightSearch");
        Catch ex As Exception
        Finally
        End Try
        Return ReturnList

        'Return ReturnList
    End Function



#Region "Pawan"
#Region "Pawan Kumar"
    Dim objItzBal As New ITZGetbalance
    Dim objParamBal As New _GetBalance
    Dim objBalResp As New GetBalanceResponse
#End Region
    ''' <summary>
    ''' functin for getting the mapped url string for a given page
    ''' </summary>
    ''' <param name="org"></param>
    ''' <param name="dest"></param>
    ''' <param name="airline"></param>
    ''' <param name="rtfStatus"></param>
    ''' <param name="trip"></param>
    ''' <param name="DepDate"></param>
    ''' <param name="RetDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)>
    Public Function GetMUForPage(ByVal name As String) As String
        '''string org, string dest, string airline, string rtf, string trip, string agentID[''<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _]
        Dim um As String = ""
        Try
            Dim urlDoc = XDocument.Load(Context.Server.MapPath("~/PageMappedData.xml"), LoadOptions.None)
            Dim isSameVal = (From u In urlDoc.Root.Elements("MapUrlRow") Select u)
            um = (From urlmap In isSameVal Where urlmap.Element("PageName").Value.Trim() = name.Trim() Select urlmap.Element("MapUrl").Value.Trim()).FirstOrDefault()
        Catch ex As Exception
            ''Dim abc = ex.Message.ToString()
        End Try
        Return um
    End Function

    <WebMethod(EnableSession:=True)>
    Public Function GetAgencyBal() As String
        Dim mainBal As String = "0"
        Try
            Dim dt As New DataTable()
            dt = sql.GetAgencyDetails(Session("UID").ToString()).Tables(0)
            'mainBal = Convert.ToString(dt.Rows(0)("Crd_Limit"))
            mainBal = Convert.ToString(dt.Rows(0)("Crd_Limit")) & "~" & Convert.ToString(dt.Rows(0)("AgentLimit")) & "~" & Convert.ToString(dt.Rows(0)("DueAmount"))

            'objParamBal._DCODE = IIf(Session("_DCODE") <> Nothing, Session("_DCODE").ToString().Trim(), " ")
            'objParamBal._MERCHANT_KEY = IIf(Session("MchntKeyITZ") <> Nothing, Session("MchntKeyITZ").ToString().Trim(), " ") ''IIf(ConfigurationManager.AppSettings("MerchantKey") <> Nothing, ConfigurationManager.AppSettings("MerchantKey").Trim(), " ")
            'objParamBal._PASSWORD = IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " ")
            'objParamBal._USERNAME = IIf(Session("_USERNAME") <> Nothing, Session("_USERNAME").ToString().Trim(), " ")
            ' ''''objParamBal._PASSWORD = VGCheckSum.genPassHash(IIf(Session("_PASSWORD") <> Nothing, Session("_PASSWORD").ToString().Trim(), " "))
            'Dim reqxml As New GetBalance()
            'reqxml.DCODE = objParamBal._DCODE
            'reqxml.MERCHANT_KEY = objParamBal._MERCHANT_KEY
            'reqxml.PASSWORD = objParamBal._PASSWORD
            'reqxml.USERNAME = objParamBal._USERNAME
            ' ''''Dim balreqXml = ToXML(reqxml)
            'objBalResp = objItzBal.GetBalanceCustomer(objParamBal)
            ' ''''Dim balrespXml = ToXML(objBalResp)
            'If objBalResp.VAL_ACCOUNT_TYPE_DETAIL.Length > 0 Then
            '    mainBal = objBalResp.VAL_ACCOUNT_TYPE_DETAIL(0).VAL_ACCOUNT_BALANCE
            'End If
        Catch ex As Exception
        End Try
        Return mainBal
    End Function

    <WebMethod(EnableSession:=True)>
    Public Function GetFDDSectorDetails() As List(Of String)
        Dim strSecList As List(Of String) = New List(Of String)()
        Dim sbStr As StringBuilder = New StringBuilder()

        Try
            Dim ConnStr As String = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            Dim objFltComm As FlightCommonBAL = New FlightCommonBAL(ConnStr)
            'Dim SrvChargeList As List(Of FltSrvChargeList)
            Dim MiscList As List(Of MISCCharges) = New List(Of MISCCharges)()
            'Dim dtAgentMarkup As DataTable = New DataTable()
            'Dim MarkupDs As DataSet = New DataSet()
            'SrvChargeList = Data.GetSrvChargeInfo(searchInputs.Trip.ToString(), ConnStr)
            'dtAgentMarkup = Data.GetMarkup(ConnStr, searchInputs.UID.ToString(), searchInputs.DISTRID.ToString(), searchInputs.Trip.ToString(), "TA")
            'dtAgentMarkup.TableName = "AgentMarkUp"
            'MarkupDs.Tables.Add(dtAgentMarkup)



            Dim dtFdd As New DataTable()
            dtFdd = sql.GetFDDSectorDetails().Tables(0)
            If dtFdd.Rows.Count > 0 Then
                Dim LoopCount As Integer = 1
                For i As Integer = 0 To dtFdd.Rows.Count - 1

                    Try
                        MiscList = objFltComm.GetMiscCharges("D", "ALL", "", "ALL", dtFdd.Rows(i)("OrgDestFrom").ToString().Split(",")(1).Replace(")", "").Replace("(", "").Trim(), dtFdd.Rows(i)("OrgDestTo").ToString().Split(",")(1).Replace(")", "").Replace("(", "").Trim())
                    Catch ex2 As Exception
                    End Try

                    Dim sbTcStr As StringBuilder = New StringBuilder()
                    'strSecList.Add(dtFdd.Rows(i)("OrgDestFrom").ToString() & "-" + dtFdd.Rows(i)("OrgDestTo").ToString())
                    strSecList.Add(dtFdd.Rows(i)("DepAirportCode").ToString().Replace("(", "").Replace(")", "") & " - " + dtFdd.Rows(i)("ArrAirportCode").ToString().Replace("(", "").Replace(")", ""))

                    Dim dtFddTime As New DataTable()
                    dtFddTime = sql.GetFDDSectorDetails(dtFdd.Rows(i)("OrgDestFrom").ToString(), dtFdd.Rows(i)("OrgDestTo").ToString()).Tables(0)
                    Dim Airlinecode As String

                    sbStr.Append("<div class='tab__content-item tab'>")
                    sbStr.Append("<div class='alert_msg info_msg fl'><b class='status_info fl'><i class='icofont-airplane icofont-2x'></i></b><span class='status_cont'>Airlines avilable for selected sector.</span></div><div class='tab__list'>")
                    For t As Integer = 0 To dtFddTime.Rows.Count - 1
                        Airlinecode = dtFddTime.Rows(t)("MarketingCarrier").ToString().Replace("(", "").Replace(")", "")

                        ' If t Mod 3 = 0 Then
                            ' If t <> 0 Then
                                ' sbStr.Append("</div>")
                            ' End If
                            ' sbStr.Append("<div class='tab__list'>")
                        ' End If

                        sbStr.Append("<div class='tab__item'> <div class='row'><div class='col-md-6'><div class='row'><div class='col-sm-4'><span><img alt='' src='../Airlogo/sm" + Airlinecode + ".gif' style='width:25px;border-radius:50%;'></span></div><div class='col-sm-8'><p class='flight-ident' style='margin-left: -20px !important;margin-top: 5px !important;'>" + Airlinecode + "-" + (dtFddTime.Rows(t)("FlightNo").ToString()) + "</p></div></div></div><div class='col-md-3'><p class='dep-loc' style='margin: 0 0 0px;'><i class='icofont-ui-flight'></i></p><span class='dep-time'>" & (dtFddTime.Rows(t)("DepartureTime").ToString() & "</span></div> <div class='col-md-3'><p class='dep-loc' style='margin: 0 0 0px;transform: rotate(45deg);'><i class='icofont-ui-flight'></i></p><span class='dep-time'>" & dtFddTime.Rows(t)("ArrivalTime").ToString()) & "</span></div></div></div>")


                        'sbStr.Append("<div Class='theme-account-bookmarks-item-thumb'><a Class='theme-account-bookmarks-item-thumb-link' href='#'></a><div Class='row row-eq-height' data-gutter='none'><div Class='col-md-3'><div Class='banner theme-account-bookmarks-item-img banner-sqr banner-'><div Class='banner-bg' style='background-image:url(../Airlogo/sm" + Airlinecode + ".gif)'></div><a Class='banner-link' href='#'></a></div></div><div Class='col-md-9'><div Class='theme-account-bookmarks-item-thumb-body'><div Class='row'><div Class='col-xs-9'><p Class='theme-account-bookmarks-item-location'>" + Airlinecode + "-" + (dtFddTime.Rows(t)("FlightNo").ToString()) + "</p></div><div class='col-md-3'><p class='dep-loc' style='margin: 0 0 0px;'><i class='icofont-ui-flight'></i></p><span class='dep-time'>" & (dtFddTime.Rows(t)("DepartureTime").ToString() & "</span></div> <div class='col-md-3'><p class='dep-loc' style='margin: 0 0 0px;transform: rotate(45deg);'><i class='icofont-ui-flight'></i></p><span class='dep-time'>" & dtFddTime.Rows(t)("ArrivalTime").ToString()) & "</span></div></div></div></div></div></div>")

                        Dim dtFddTimeDate As New DataTable()
                        dtFddTimeDate = sql.GetFDDSectorDetails(dtFdd.Rows(i)("OrgDestFrom").ToString(), dtFdd.Rows(i)("OrgDestTo").ToString(), dtFddTime.Rows(t)("DepartureTime").ToString(), dtFddTime.Rows(t)("ArrivalTime").ToString(), dtFddTime.Rows(t)("MarketingCarrier").ToString()).Tables(0)

                        'sbTcStr.Append("<div class='tab__content'>")
                        sbTcStr.Append("<div class='tab__content-item'>")

                        sbTcStr.Append("<p><div class='row'>")
                        Dim objDepDateList As List(Of String) = New List(Of String)()
                        For d As Integer = 0 To dtFddTimeDate.Rows.Count - 1
                            If Not objDepDateList.Contains(dtFddTimeDate.Rows(d)("Departure_Date").ToString()) Then
                                objDepDateList.Add(dtFddTimeDate.Rows(d)("Departure_Date").ToString())

                                'Dim startDate As DateTime = DateTime.Parse(dtFddTimeDate.Rows(d)("Departure_Date").ToString().Split("/")(2) + "-" + dtFddTimeDate.Rows(d)("Departure_Date").ToString().Split("/")(1) + "-" + dtFddTimeDate.Rows(d)("Departure_Date").ToString().Split("/")(0))

                                'Dim strDate As String = startDate.ToString("dd MMM yyyy")
								Dim strDate As String = DateTime.Parse(dtFddTimeDate.Rows(d)("Departure_Date").ToString()).ToString("dd MMM yyyy") 'startDate.ToString("dd MMM yyyy")

								Dim strDateData As String = DateTime.Parse(dtFddTimeDate.Rows(d)("Departure_Date").ToString()).ToString("dd/MM/yyyy")

                                Dim strloop As String = LoopCount




                                Dim srvChargeAdt As Decimal = 0
                                Dim Grandtotalfare As Decimal = 0

                                Try
                                    srvChargeAdt = objFltComm.MISCServiceFee(MiscList, dtFddTime.Rows(t)("MarketingCarrier").ToString().Replace("(","").Replace(")","").Trim(), "FDD", dtFddTimeDate.Rows(d)("Basicfare").ToString(), dtFddTimeDate.Rows(d)("YQ").ToString()) ''GetMiscServiceCharge(searchInputs.Trip.ToString(), objFS.ValiDatingCarrier, searchInputs.UID, searchInputs.AgentType, Utility.Left(searchInputs.HidTxtDepCity, 3), Utility.Left(searchInputs.HidTxtArrCity, 3));
                                    Grandtotalfare = Convert.ToDecimal(dtFddTimeDate.Rows(d)("Grand_Total").ToString()) + srvChargeAdt
                                Catch ex As Exception
                                    srvChargeAdt = 0
                                    Grandtotalfare = Convert.ToDecimal(dtFddTimeDate.Rows(d)("Grand_Total").ToString()) + srvChargeAdt
                                End Try



                                If Convert.ToString(dtFddTimeDate.Rows(d)("FareDet")) <> "" Then
                                    sbTcStr.Append("<div class='col-md-2' style='margin-bottom: 11px;cursor: pointer;'><span data-strdstfrom='" + dtFdd.Rows(i)("OrgDestFrom").ToString() + "' data-strdstto='" + dtFdd.Rows(i)("OrgDestTo").ToString() + "' data-strdate='" + strDateData + "' data-strcarrier='" + dtFddTime.Rows(t)("MarketingCarrier").ToString() + "' data-loopcount='" + strloop + "' HideDep='" + dtFdd.Rows(i)("HideDep").ToString() + "' HideArr='" + dtFdd.Rows(i)("HideArr").ToString() + "' class='date-list BookSectorTicket' >" + strDate + "<span class='price-list'>" + Convert.ToString(dtFddTimeDate.Rows(d)("FareDet")) + Grandtotalfare.ToString() + "</span><span class='seat-list'>Seat Left : " + dtFddTimeDate.Rows(d)("Avl_Seat").ToString() + "</span></span></div>")
                                Else
                                    sbTcStr.Append("<div class='col-md-2' style='margin-bottom: 11px;cursor: pointer;'><span data-strdstfrom='" + dtFdd.Rows(i)("OrgDestFrom").ToString() + "' data-strdstto='" + dtFdd.Rows(i)("OrgDestTo").ToString() + "' data-strdate='" + strDateData + "' data-strcarrier='" + dtFddTime.Rows(t)("MarketingCarrier").ToString() + "' data-loopcount='" + strloop + "' HideDep='" + dtFdd.Rows(i)("HideDep").ToString() + "' HideArr='" + dtFdd.Rows(i)("HideArr").ToString() + "' class='date-list BookSectorTicket' >" + strDate + "<span class='price-list'>₹ " + Grandtotalfare.ToString() + "</span><span class='seat-list'>Seat Left : " + dtFddTimeDate.Rows(d)("Avl_Seat").ToString() + "</span></span></div>")
                                End If
                                'sbTcStr.Append("<div class='col-md-2' style='margin-bottom: 11px;cursor: pointer;'><span data-loopcount='" + LoopCount + "' class='date-list BookSectorTicket'>" + strDate + "<span class='price-list'>₹ " + dtFddTimeDate.Rows(d)("Grand_Total").ToString() + "</span><span class='seat-list'>Seat Left : " + dtFddTimeDate.Rows(d)("Total_Seats").ToString() + "</span></span></div>")

                                LoopCount = LoopCount + 1
                            End If
                        Next
                        sbTcStr.Append("</div></p>")

                        'sbTcStr.Append("</div>")
                        sbTcStr.Append("</div>")
                    Next
                    sbStr.Append("</div>")

                    sbStr.Append("<div class='alert_msg info_msg fl'><b class='status_info fl'><i class='icofont-ui-calendar icofont-2x'></i></b><span class='status_cont'>Avilable Date, Fare and Seats for select Airline.</span></div><div class='tab__content'>")
                    sbStr.Append(sbTcStr)
                    sbStr.Append("</div>")

                    sbStr.Append("</div>")
                Next
            End If
            strSecList.Add(sbStr.ToString())

        Catch ex As Exception
            Dim error_msg As String = ex.Message
        End Try
        Return strSecList
    End Function

    <WebMethod(EnableSession:=True)>
    Public Function GetFDDOfferExpireSoonDetails() As String
        Dim strSec As String = ""

        Try
            Dim dtFdd As New DataTable()
            dtFdd = sql.GetFDDOfferExpireSoon().Tables(0)
            If dtFdd.Rows.Count > 0 Then
                For i As Integer = 0 To dtFdd.Rows.Count - 1

                    Dim startDate As DateTime = DateTime.Parse(dtFdd.Rows(i)("valid_Till").ToString())
                    Dim expDate As String = startDate.ToString("dd MMM yyyy")

                    strSec = strSec + "<div class='item' href='#'>"

                    strSec = strSec + "<div class='row'>"
                    strSec = strSec + "<div class='col-md-3'><img src='../Airlogo/sm" + dtFdd.Rows(i)("MarketingCarrier").ToString().Replace("(", "").Replace(")", "") + ".gif' style='width: 44px;border-radius:50%;' /></div>"
                    strSec = strSec + "<div class='col-md-9'><p style='font-weight: 600; font-size: 19px;'>" + dtFdd.Rows(i)("DepAirportCode").ToString().Replace("(", "").Replace(")", "") + " // " + dtFdd.Rows(i)("ArrAirportCode").ToString().Replace("(", "").Replace(")", "") + "</p><p>" + expDate + "</p></div>"
                    strSec = strSec + "</div>"

                    strSec = strSec + "<div class='row' style='text-align: center;'>"
                    strSec = strSec + "<span style='color: red; font-size: 20px; font-weight: 600;'>₹ " + dtFdd.Rows(i)("Grand_Total").ToString() + "</span>"
                    strSec = strSec + "</div>"

                    strSec = strSec + "</div>"
                Next
            Else
                strSec = "No offer expire available!"
            End If
        Catch ex As Exception
            Dim error_msg As String = ex.Message
            strSec = error_msg
        End Try
        Return strSec
    End Function

    Public Function ToXML(objDeb As GetBalanceResponse) As String
        Dim stringwriter As New System.IO.StringWriter()
        Dim serializer As New System.Xml.Serialization.XmlSerializer(objDeb.GetType())
        serializer.Serialize(stringwriter, objDeb)
        Return stringwriter.ToString()
    End Function

    Public Function ToXML(objDeb As GetBalance) As String
        Dim stringwriter As New System.IO.StringWriter()
        Dim serializer As New System.Xml.Serialization.XmlSerializer(objDeb.GetType())
        serializer.Serialize(stringwriter, objDeb)
        Return stringwriter.ToString()
    End Function


#End Region

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetAirlineNetFare(ByVal AirArray As ArrayList, ByVal Trip As String) As String
        Dim fbStr As String = ""
        Dim NetFare As String = ""
        Dim Ln As String = AirArray(0)("LineNumber").ToString()
        Dim Air As New ArrayList()
        Try
            'NO. OF RECORDS LIST FOR MULTI STOPS

            UID = HttpContext.Current.Session("UID").ToString()
            UserType = Session("UserType").ToString()
            TypeId = Session("TypeId").ToString()
            GroupType = HttpContext.Current.Session("agent_type").ToString()
            Dim ObjCommBal As New STD.BAL.FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim IntFareDetails As New Dictionary(Of String, Object)
            If Trip = "I" Then
                IntFareDetails = ObjCommBal.GetIntlCommGal(GroupType, AirArray, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))

            ElseIf Trip = "D" Then
                IntFareDetails = ObjCommBal.GetDomCommGal(GroupType, AirArray, Data.Calc_TDS(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString, UID))

            End If

            If Not String.IsNullOrEmpty(Convert.ToString(IntFareDetails)) AndAlso IntFareDetails.Count > 0 Then
                NetFare = Convert.ToString(IntFareDetails("netFare"))
            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            'ExceptionLogger.FileHandling("FlightSearchService", "Err_003", ex, "FlightSearch")
        Finally
        End Try
        Return NetFare
        'Return fbStr
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function SRFPriceItinReq(ByVal AirArray As String, ByVal Trip As String) As ArrayList

        Dim Air As New ArrayList()
        Try
            ''Dim strarray As String = LZCompression.LZString.DecompressFromUTF16(AirArray)
            Dim a As ArrayList = Newtonsoft.Json.Linq.JArray.Parse(AirArray).ToObject(Of ArrayList)()

            Dim newArr As ArrayList = New ArrayList()
            For index As Integer = 0 To a.Count - 1
                newArr.Add(Newtonsoft.Json.Linq.JObject.Parse(a(index).ToString()).ToObject(Of Dictionary(Of String, Object))())
            Next

            Dim Ln As String = newArr(0)("LineNumber").ToString()
            UID = HttpContext.Current.Session("UID").ToString()
            UserType = Session("UserType").ToString()
            TypeId = Session("TypeId").ToString()
            GroupType = HttpContext.Current.Session("agent_type").ToString()

            ' Dim ObjCommBal As New STD.BAL.FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            'Dim Obj As New GALWS.SRFRepricing(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Dim Obj As New GALWS.SRFRepricing(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            Air = Obj.SpecailReturnFarePricing(newArr, "D", GroupType, UID, "6E")
            'Set Value

            'End Set Value





            'Air = FareBreakupGAL(newArr, Trip)
            ' Dim objArry As ArrayList = DirectCast(Newtonsoft.Json.JsonConvert.DeserializeObject(strarray), JSONArray)
            ' Air = FareBreakupGAL(objArry, Trip)

        Catch ex As Exception

        Finally
        End Try
        Return Air

    End Function

    Private Function SearchKey(ByVal Org As String, ByVal Dest As String, ByVal Depdate As String, ByVal RetDate As String, ByVal Adt As Integer, ByVal Chd As Integer, ByVal Inf As Integer, ByVal GdsRtf As Boolean, ByVal LccRtf As Boolean, ByVal Cabin As String, ByVal aircode As String) As String
        Dim SKey As String = ""
        If ((GdsRtf = True) _
                    OrElse (LccRtf = True)) Then
            SKey = (Org.Trim) + ("/" _
                        + (Dest.Trim) + ("/" _
                        + (Org.Trim) + ("/" _
                        + (Depdate.Trim + ("/" _
                        + (RetDate.Trim + ("/" _
                        + (Adt.ToString + ("/" _
                        + (Chd.ToString + ("/" _
                        + (Inf.ToString + ("/" _
                        + (GdsRtf.ToString + ("/" _
                        + (LccRtf.ToString + ("/" + Cabin.ToString)))))))))))))))))
        Else
            SKey = Org.Trim + "/" + Dest.Trim + "/" + Depdate.Trim + "/" + RetDate.Trim + "/" + Adt.ToString + "/" + Chd.ToString + "/" + Inf.ToString + "/" + GdsRtf.ToString + "/" + LccRtf.ToString + "/" + Cabin.ToString
        End If


        deletecache(SKey, aircode)

        Return SKey

    End Function

    Public Sub deletecache(ByVal skeyvalue As String, ByVal AirCode As String)
        Try
            Dim a As Integer = 0
            Dim cmd As New SqlCommand("Sp_DeleteCacheonfailure", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@skeyvalue", skeyvalue.Trim())
            cmd.Parameters.AddWithValue("@AirCode", AirCode.ToUpper().Trim())
            Con.Open()
            a = cmd.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception

        End Try


    End Sub

    Public Function CheckFareAvailabilityLCC(ByVal List As Object(), ByVal l1 As Integer, ByVal TrackIdOld As String, ByRef diffamount As String, ByRef IsRoundTrip As String, ByRef changeFareList As STD.BAL.CacheRereshRespList) As ArrayList

        Dim FinalList_Gal As New ArrayList()
        Dim Provider As String = "", Ftype As String = "", VC As String = ""
        UID = HttpContext.Current.Session("UID").ToString()
        Dim RecWrapper As New ArrayList()
        ' Dim FareData As New Hashtable()
        For i As Integer = 0 To l1 - 1
            'Fetch Each Record in the List
            Dim Rec As New Dictionary(Of String, Object)()
            Rec = DirectCast(List(i), Dictionary(Of String, Object))
            Provider = Rec("Provider")
            VC = Rec("ValiDatingCarrier")
            RecWrapper.Add(Rec)
        Next
        If ((Provider.ToUpper() = "LCC") And (VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX")) Then
            FinalList_Gal.Clear()
            FinalList_Gal = LCC_AvailabilityCrossCheck(List, diffamount, changeFareList, IsRoundTrip, VC)
        End If

        'If (Provider = "LCC") Then
        '    If (VC = "SG") Then
        '        FinalList_Gal.Clear()
        '        'FinalList_Gal = Spicejet_SellAmt(List, diffamount)
        '        FinalList_Gal = LCC_AvailabilityCrossCheck(List, diffamount, changeFareList, IsRoundTrip, VC)
        '    ElseIf VC = "6E" Then
        '        FinalList_Gal.Clear()
        '        'FinalList_Gal = Indigo_SellAmt(List, diffamount)
        '        FinalList_Gal = LCC_AvailabilityCrossCheck(List, diffamount, changeFareList, IsRoundTrip, VC)
        '    ElseIf VC = "G8" Then
        '        FinalList_Gal.Clear()
        '        FinalList_Gal = LCC_AvailabilityCrossCheck(List, diffamount, changeFareList, IsRoundTrip, VC)
        '    End If
        'End If


        Return FinalList_Gal
    End Function

    Public Function LCC_AvailabilityCrossCheck(ByVal List As Object(), ByRef diffamount As String, ByRef changeFareList As STD.BAL.CacheRereshRespList, ByVal IsRoundTrip As String, ByVal AirCode As String) As ArrayList

        Dim FareChangeO As CacheRereshResp = New CacheRereshResp()
        Dim FareChangeR As CacheRereshResp = New CacheRereshResp()
        Dim CacheTotFare As String = "0"
        Dim CacheNetFare As String = "0"
        Dim NewTotFare As String = "0"
        Dim NewNetFare As String = "0"


        Dim a As Dictionary(Of String, Object) = DirectCast(List(0), Dictionary(Of String, Object))
        Dim objSql As New SqlTransactionNew
        Dim objInputs As New STD.Shared.FlightSearch()
        Dim Org As String = "", SellAmt = "", Signature = "", OriginalTF = "", VC = "", CacheMicCharge = "0"
        Dim Diff As Decimal = 0.0, DiffPax = 0.0, Diff1 = 0.0, MicChargeDiff = 0.0
        Dim cnt As Short = 0
        Dim SFMCON As String = ""
        Dim FT As String() = Nothing
        Dim FinalList_Gal As New ArrayList()
        Dim Air As New ArrayList()
        Try
            If List(List.Count - 1)("TripType") = "R" Then objInputs.TripType = STD.Shared.TripType.RoundTrip Else objInputs.TripType = STD.Shared.TripType.OneWay
            If (objInputs.TripType = STD.Shared.TripType.RoundTrip) Then
                cnt = 1
            End If
            Dim JSK(cnt), FSK(cnt), CC(cnt), FNO(cnt), DD(cnt) As String
            For i As Integer = 0 To List.Count - 1
                Dim Rec As New Dictionary(Of String, Object)()
                Rec = DirectCast(List(i), Dictionary(Of String, Object))
                If (i = 0) Then
                    Org = Rec("OrgDestFrom").ToString()
                    If Rec("Trip") = "D" Then objInputs.Trip = STD.Shared.Trip.D Else objInputs.Trip = STD.Shared.Trip.I
                    objInputs.Adult = Rec("Adult")
                    objInputs.Child = Rec("Child")
                    objInputs.Infant = Rec("Infant")
                    VC = Rec("ValiDatingCarrier")
                    objInputs.HidTxtAirLine = VC
                End If
                If (Org = Rec("OrgDestFrom").ToString()) Then
                    JSK(0) = Rec("sno")
                    FSK(0) = Rec("Searchvalue")
                    'CC(0) = "G8"
                    CC(0) = Rec("ValiDatingCarrier")
                    FNO(0) = Rec("FlightIdentification")
                    DD(0) = Rec("depdatelcc")
                    OriginalTF = Rec("OriginalTF")
                    CacheNetFare = Rec("NetFare").ToString()
                    CacheTotFare = Rec("TotalFare").ToString()
                    CacheMicCharge = Convert.ToString(Rec("OriginalTT"))
                ElseIf (Org = Rec("OrgDestTo").ToString()) Then
                    JSK(1) = Rec("sno")
                    FSK(1) = Rec("Searchvalue")
                    CC(1) = Rec("ValiDatingCarrier")
                    FNO(1) = Rec("FlightIdentification")
                    DD(1) = Rec("depdatelcc")
                End If
                FinalList_Gal.Add(Rec)
            Next
            If VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX" Then

                Dim RecNew As New Dictionary(Of String, Object)()
                RecNew = DirectCast(List(0), Dictionary(Of String, Object))

                If (RecNew("ProductDetailQualifier").ToString().Contains("SFM")) Then
                    SFMCON = "0"
                Else
                    SFMCON = "1"
                    'Dim Req, Res As String
                    Dim xml As New Dictionary(Of String, String)
                    'New Code
                    Try
                        UID = HttpContext.Current.Session("UID").ToString()
                        UserType = Session("UserType").ToString()
                        TypeId = Session("TypeId").ToString()
                        GroupType = HttpContext.Current.Session("agent_type").ToString()
                        'Session("UserType") = UserType '' "TA"
                        'Session("TypeID") = TypeId ''"TA1"
                        Dim Obj As New GALWS.LCCFareCrossCheck(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
                        'Air = Obj.SearchResultAvilabilityLcc(FinalList_Gal, "G8", GroupType, UID, "FWU", UserType, TypeId, JSK, FSK, HttpContext.Current)
                        Air = Obj.SearchResultAvilabilityLcc(FinalList_Gal, VC.ToUpper(), GroupType, UID, "FWS", UserType, TypeId, JSK, FSK, HttpContext.Current)
                    Catch ex As Exception

                    Finally
                    End Try
                    Dim AirCnt As Integer = 0
                    Try
                        If (Air IsNot Nothing AndAlso Air.Count > 0 AndAlso Air(Air.Count - 1).Count > 0) Then
                            AirCnt = 1
                        End If
                    Catch ex As Exception
                        AirCnt = 0
                    End Try

                    'If SellAmt = "FAILURE" Then
                    If Air Is Nothing OrElse Air.Count < 1 OrElse AirCnt < 1 Then
                        'End If
                        'If SellAmt = "FAILURE" Then
                        Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                        Dim Dest As String = RecNew("OrgDestTo").ToString()

                        Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                        Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                        If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                            SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                            SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                        Else
                            SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                        End If
                    End If

                End If

                'SellAmt = objSg.Spice_Sell_SSR(Signature, objInputs, JSK, FSK, CC, FNO, DD, )
            End If


        Catch ex As Exception

        End Try
        If ((VC = "G8" OrElse VC = "6E" OrElse VC = "SG" OrElse VC = "IX") AndAlso Air IsNot Nothing AndAlso Air.Count > 0) Then
            SellAmt = Air(0)(0).OriginalTF
            If (SFMCON = "1") Then
                Diff = (Convert.ToDecimal(SellAmt) - Convert.ToDecimal(OriginalTF))
                MicChargeDiff = Convert.ToDecimal(Air(0)(0).OriginalTT) - Convert.ToDecimal(CacheMicCharge)
                'Diff1 = 0 'Math.Round(Diff, 0)            
                'DiffPax = Math.Round((Math.Round(Diff, 0) / (objInputs.Adult + objInputs.Child)), 0)
                'Diff1 = Diff1 + (DiffPax * objInputs.Adult)
                'Diff1 = Diff1 + (DiffPax * objInputs.Child)
                If (IsRoundTrip = "true") Then
                    NewTotFare = Air(0)(0).TotalFare
                    NewNetFare = Air(0)(0).NetFare
                    FareChangeR.CacheNetFare = Single.Parse(CacheNetFare)
                    FareChangeR.CacheTotFare = Single.Parse(CacheTotFare)
                    FareChangeR.NewNetFare = Single.Parse(NewNetFare) ' + Diff1 'Use for test, after test coment + Diff1
                    FareChangeR.NewTotFare = Single.Parse(NewTotFare) ' + Diff1 'Use for test, after test coment + Diff1
                    changeFareList.ChangeFareR = FareChangeR
                Else
                    NewTotFare = Air(0)(0).TotalFare
                    NewNetFare = Air(0)(0).NetFare
                    FareChangeO.CacheNetFare = Single.Parse(CacheNetFare)
                    FareChangeO.CacheTotFare = Single.Parse(CacheTotFare)
                    FareChangeO.NewNetFare = Single.Parse(NewNetFare) ' + Diff1 'Use for test, after test coment + Diff1
                    FareChangeO.NewTotFare = Single.Parse(NewTotFare) ' + Diff1 'Use for test, after test coment + Diff1
                    changeFareList.ChangeFareO = FareChangeO
                End If

                If (Diff > 0 OrElse Diff < 0 OrElse MicChargeDiff > 0 OrElse MicChargeDiff < 0) Then
                    'If (IsRoundTrip = "true") Then
                    '    NewTotFare = Air(0)(0).TotalFare
                    '    NewNetFare = Air(0)(0).NetFare
                    '    FareChangeR.CacheNetFare = Single.Parse(CacheNetFare)
                    '    FareChangeR.CacheTotFare = Single.Parse(CacheTotFare)
                    '    FareChangeR.NewNetFare = Single.Parse(NewNetFare) + Diff1 'Use for test, after test coment + Diff1
                    '    FareChangeR.NewTotFare = Single.Parse(NewTotFare) + Diff1 'Use for test, after test coment + Diff1
                    '    changeFareList.ChangeFareR = FareChangeR
                    'Else
                    '    NewTotFare = Air(0)(0).TotalFare
                    '    NewNetFare = Air(0)(0).NetFare
                    '    FareChangeO.CacheNetFare = Single.Parse(CacheNetFare)
                    '    FareChangeO.CacheTotFare = Single.Parse(CacheTotFare)
                    '    FareChangeO.NewNetFare = Single.Parse(NewNetFare) + Diff1 'Use for test, after test coment + Diff1
                    '    FareChangeO.NewTotFare = Single.Parse(NewTotFare) + Diff1 'Use for test, after test coment + Diff1
                    '    changeFareList.ChangeFareO = FareChangeO
                    'End If

                    Dim RecNew As New Dictionary(Of String, Object)()
                    RecNew = DirectCast(List(0), Dictionary(Of String, Object))


                    If (Diff > 0 OrElse Diff < 0) Then
                        Try
                            Dim Orgg As String = RecNew("OrgDestFrom").ToString()
                            Dim Dest As String = RecNew("OrgDestTo").ToString()

                            Dim DepDate As String = (Utility.Left(RecNew("DepartureDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("DepartureDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("DepartureDate").ToString().Trim, 2)))))
                            Dim RetDate As String = (Utility.Left(RecNew("ArrivalDate").ToString().Trim, 2) + ("/" + (Utility.Mid(RecNew("ArrivalDate").ToString().Trim, 2, 2) + ("/20" + Utility.Right(RecNew("ArrivalDate").ToString().Trim, 2)))))

                            If ((objInputs.TripType.ToString = "RoundTrip") AndAlso ((objInputs.Trip.ToString = "D") AndAlso ((objInputs.RTF = False) AndAlso (objInputs.GDSRTF = False)))) Then
                                SearchKey(Dest, Orgg, RetDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                                SearchKey(Orgg, Dest, DepDate, DepDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                            Else
                                SearchKey(Orgg, Dest, DepDate, RetDate, objInputs.Adult, objInputs.Child, objInputs.Infant, objInputs.GDSRTF, objInputs.RTF, "", VC.ToUpper())
                            End If
                        Catch ex As Exception

                        End Try
                    End If


                    diffamount = Diff
                    FinalList_Gal.Clear()
                    For i As Integer = 0 To List.Count - 1
                        Dim Rec As New Dictionary(Of String, Object)()
                        Rec = DirectCast(List(i), Dictionary(Of String, Object))
                        If (objInputs.Adult > 0) Then
                            Rec("AdtFSur") = Convert.ToDecimal(Air(0)(0).AdtFSur)
                            Rec("AdtYR") = Convert.ToDecimal(Air(0)(0).AdtYR)
                            Rec("AdtWO") = Convert.ToDecimal(Air(0)(0).AdtWO)
                            Rec("AdtIN") = Convert.ToDecimal(Air(0)(0).AdtIN)
                            Rec("AdtQ") = Convert.ToDecimal(Air(0)(0).AdtQ)
                            Rec("AdtJN") = Convert.ToDecimal(Air(0)(0).AdtJN)
                            Rec("AdtOT") = Convert.ToDecimal(Air(0)(0).AdtOT) 'Convert.ToDecimal(Rec("AdtOT").ToString()) + DiffPax
                            'Rec("ADT_TAX") = Convert.ToDecimal(Air(0)(0).ADT_TAX)
                            Rec("ADTSRVTAX") = Convert.ToDecimal(Air(0)(0).ADTSRVTAX)
                            Rec("ADTSRVTAX1") = Convert.ToDecimal(Air(0)(0).ADTSRVTAX1)
                            Rec("ADTTDS") = Convert.ToDecimal(Air(0)(0).ADTTDS)
                            Rec("AdtTax") = Convert.ToDecimal(Air(0)(0).AdtTax) ''Convert.ToDecimal(Rec("AdtTax").ToString()) + DiffPax
                            Rec("AdtFare") = Convert.ToDecimal(Air(0)(0).AdtFare) 'Convert.ToDecimal(Rec("AdtFare").ToString()) + DiffPax
                            Rec("AdtBfare") = Convert.ToDecimal(Air(0)(0).AdtBfare) 'objFS.AdtBfare = objFS.AdtBfare - PROMODISCHD;
                        End If
                        If (objInputs.Child > 0) Then
                            Rec("ChdFSur") = Convert.ToDecimal(Air(0)(0).ChdFSur)
                            Rec("ChdYR") = Convert.ToDecimal(Air(0)(0).ChdYR)
                            Rec("ChdWO") = Convert.ToDecimal(Air(0)(0).ChdWO)
                            Rec("ChdIN") = Convert.ToDecimal(Air(0)(0).ChdIN)
                            Rec("ChdQ") = Convert.ToDecimal(Air(0)(0).ChdQ)
                            Rec("ChdJN") = Convert.ToDecimal(Air(0)(0).ChdJN)
                            Rec("ChdOT") = Convert.ToDecimal(Air(0)(0).ChdOT) 'Convert.ToDecimal(Rec("ChdOT").ToString()) + DiffPax
                            'Rec("CHD_TAX") = Convert.ToDecimal(Air(0)(0).CHD_TAX)
                            Rec("CHDOT") = Convert.ToDecimal(Air(0)(0).CHDOT)
                            Rec("CHDSRVTAX") = Convert.ToDecimal(Air(0)(0).CHDSRVTAX)
                            Rec("CHDSRVTAX1") = Convert.ToDecimal(Air(0)(0).CHDSRVTAX1)
                            Rec("CHDTDS") = Convert.ToDecimal(Air(0)(0).CHDTDS)
                            Rec("ChdTax") = Convert.ToDecimal(Air(0)(0).ChdTax) 'Convert.ToDecimal(Rec("ChdTax").ToString()) + DiffPax
                            Rec("ChdFare") = Convert.ToDecimal(Air(0)(0).ChdFare) 'Convert.ToDecimal(Rec("ChdFare").ToString()) + DiffPax
                            Rec("ChdBFare") = Convert.ToDecimal(Air(0)(0).ChdBFare) 'objFS.ChdBFare = objFS.ChdBFare - PROMODISCHD;
                        End If
                        If (objInputs.Infant > 0) Then
                            Rec("InfFSur") = Convert.ToDecimal(Air(0)(0).InfFSur)
                            Rec("InfYR") = Convert.ToDecimal(Air(0)(0).InfYR)
                            Rec("InfWO") = Convert.ToDecimal(Air(0)(0).InfWO)
                            Rec("InfIN") = Convert.ToDecimal(Air(0)(0).InfIN)
                            Rec("InfQ") = Convert.ToDecimal(Air(0)(0).InfQ)
                            Rec("InfJN") = Convert.ToDecimal(Air(0)(0).InfJN)
                            Rec("InfOT") = Convert.ToDecimal(Air(0)(0).InfOT)  'objFS.InfOT = objFS.InfTax;
                            'Rec("INF_TAX") = Convert.ToDecimal(Air(0)(0).INF_TAX)
                            Rec("INFSRVTAX") = Convert.ToDecimal(Air(0)(0).CHDSRVTAX)
                            Rec("INFTDS") = Convert.ToDecimal(Air(0)(0).INFTDS)
                            Rec("InfTax") = Convert.ToDecimal(Air(0)(0).InfTax) 'objFS.InfTax = float.Parse(Math.Round(InfTax).ToString());
                            Rec("InfFare") = Convert.ToDecimal(Air(0)(0).InfFare) 'objFS.InfFare = float.Parse(Math.Round(InfantBFare + InfTax).ToString());
                            Rec("InfBfare") = Convert.ToDecimal(Air(0)(0).InfBfare) 'objFS.InfBfare = float.Parse(Math.Round(InfantBFare).ToString());
                        End If

                        '' 
                        ' ''obj.SRVTAX = Convert.ToDecimal(InsertRow["STax"].ToString());//Convert.ToDecimal(obj.ADTSRVTAX) + Convert.ToDecimal(obj.CHDSRVTAX) + Convert.ToDecimal(obj.INFSRVTAX);
                        'Rec("SRVTAX") = Convert.ToDecimal(Air(0)(0).SRVTAX)
                        ' '' obj.TF = Convert.ToDecimal(InsertRow["TFee"].ToString());
                        'Rec("TF") = Convert.ToDecimal(Air(0)(0).TF)
                        ' ''obj.TC = Convert.ToDecimal(InsertRow["TotMrkUp"].ToString());//((Convert.ToDecimal(obj.ADTADMINMRK) + Convert.ToDecimal(obj.ADTAGENTMRK)) * obj.ADULT) + ((Convert.ToDecimal(obj.CHDADMINMRK) + Convert.ToDecimal(obj.CHDAGENTMRK)) * obj.CHILD) + ((Convert.ToDecimal(obj.INFADMINMRK) + Convert.ToDecimal(obj.INFAGENTMRK)) * obj.INFANT);
                        'Rec("TC") = Convert.ToDecimal(Air(0)(0).TC)

                        Rec("OriginalTT") = Convert.ToDecimal(Air(0)(0).OriginalTT) 'objFS.OriginalTT = srvCharge;'MISC Markup Charges
                        Rec("TotalTax") = Convert.ToDecimal(Air(0)(0).TotalTax) 'Convert.ToDecimal(Rec("TotalTax").ToString()) + Diff1'objFS.TotalTax = (objFS.AdtTax * objFS.Adult) + (objFS.ChdTax * objFS.Child) + (objFS.InfTax * objFS.Infant);
                        Rec("TotalFare") = Convert.ToDecimal(Air(0)(0).TotalFare) 'Convert.ToDecimal(Rec("TotalFare").ToString()) + Diff1
                        'objFS.TotalFare = (objFS.AdtFare * objFS.Adult) + (objFS.ChdFare * objFS.Child) + (objFS.InfFare * objFS.Infant);
                        Rec("NetFare") = Convert.ToDecimal(Air(0)(0).NetFare) 'Convert.ToDecimal(Rec("NetFare").ToString()) + Diff1
                        Rec("OriginalTF") = Convert.ToDecimal(Air(0)(0).OriginalTF) 'Convert.ToDecimal(Rec("OriginalTF").ToString()) + Diff

                        Rec("TotBfare") = Convert.ToDecimal(Air(0)(0).TotBfare) 'objFS.TotBfare = (objFS.AdtBfare * objFS.Adult) + (objFS.ChdBFare * objFS.Child) + (objFS.InfBfare * objFS.Infant);
                        Rec("TotalFuelSur") = Convert.ToDecimal(Air(0)(0).TotalFuelSur) ' objFS.TotalFuelSur = (objFS.AdtFSur * objFS.Adult) + (objFS.ChdFSur * objFS.Child);
                        Rec("STax") = Convert.ToDecimal(Air(0)(0).STax) 'objFS.STax = (objFS.AdtSrvTax * objFS.Adult) + (objFS.ChdSrvTax * objFS.Child) + (objFS.InfSrvTax * objFS.Infant);
                        Rec("TFee") = Convert.ToDecimal(Air(0)(0).TFee) ' objFS.TFee = (objFS.AdtTF * objFS.Adult) + (objFS.ChdTF * objFS.Child) + (objFS.InfTF * objFS.Infant);
                        'objFS.TotDis = (objFS.AdtDiscount * objFS.Adult) + (objFS.ChdDiscount * objFS.Child);
                        'objFS.TotCB = (objFS.AdtCB * objFS.Adult) + (objFS.ChdCB * objFS.Child);
                        Rec("TotTds") = Convert.ToDecimal(Air(0)(0).TotTds) 'objFS.TotTds = (objFS.AdtTds * objFS.Adult) + (objFS.ChdTds * objFS.Child);// +objFS.InfTds;
                        Rec("TotMrkUp") = Convert.ToDecimal(Air(0)(0).TotMrkUp) 'objFS.TotMrkUp = (objFS.ADTAdminMrk * objFS.Adult) + (objFS.ADTAgentMrk * objFS.Adult) + (objFS.CHDAdminMrk * objFS.Child) + (objFS.CHDAgentMrk * objFS.Child);
                        Rec("TotMgtFee") = Convert.ToDecimal(Air(0)(0).TotMgtFee) 'objFS.TotMgtFee = (objFS.AdtMgtFee * objFS.Adult) + (objFS.ChdMgtFee * objFS.Child) + (objFS.InfMgtFee * objFS.Infant);


                        FinalList_Gal.Add(Rec)
                    Next
                End If
            End If
        End If
        Return FinalList_Gal
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ViewSeatDetails(ByVal Airline As String, ByVal OrderId As String) As String
        Dim AssignSeat As AssignSeat = New AssignSeat()
        Dim TDPaxInfo As FltPaxDetails = New FltPaxDetails()
        Dim OBFltDs, IBFltDs As DataSet
        Dim objDA As New SqlTransaction
        OBFltDs = objDA.GetFltDtls(OrderId, Session("UID"))
        Dim PaxDs As DataSet = objDA.GetPaxDetailsSeat(OrderId)
        Dim tcCode As String = "" ''TDPaxInfo.OBFlightList(0).CorpId
        Dim FareTypeSettingsList As List(Of FareTypeSettings)
        Dim objFltComm As FlightCommonBAL = New FlightCommonBAL(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        Dim FT As String() = Nothing
        Dim PROMOCODE As String = ""

        Try
            PROMOCODE = ""
            If String.IsNullOrEmpty(OBFltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString()) = False Then PROMOCODE = OBFltDs.Tables(0).Rows(0)("ElectronicTicketing").ToString().Split("/"c)(0)
            FareTypeSettingsList = objFltComm.GetFareTypeSettings("", OBFltDs.Tables(0).Rows(0)("Trip").ToString(), "")
            FareTypeSettingsList = FareTypeSettingsList.Where(Function(x) x.AirCode = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString() AndAlso x.Trip = OBFltDs.Tables(0).Rows(0)("Trip").ToString() AndAlso x.IdType.ToUpper().Trim() = OBFltDs.Tables(0).Rows(0)("RESULTTYPE").ToString().ToUpper().Trim() AndAlso x.IsBagFare = OBFltDs.Tables(0).Rows(0)("IsBagFare").ToString()).ToList()
            FT = FareTypeSettingsList(0).FareType.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
        Catch ex As Exception
        End Try

        Dim SeatMap As List(Of SeatMapFinal) = New List(Of SeatMapFinal)()
        Dim Provider As String = "LCC"
        Dim CrdList As List(Of CredentialList)
        Dim objCrd As Credentials = New Credentials(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
        CrdList = objCrd.GetGALBookingCredentialsSeat(OBFltDs.Tables(0).Rows(0)("Provider").ToString(), OBFltDs.Tables(0).Rows(0)("Trip").ToString(), OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), OBFltDs.Tables(0).Rows(0)("RESULTTYPE").ToString())
        ''    CrdList = objCrd.GetGALBookingCredentials(OBFltDs.Tables(0).Rows(0)("Trip").ToString(), tcCode, OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString(), OBFltDs.Tables(0).Rows(0)("TripCnt").ToString(), "LCC")

        If Airline.ToUpper() = "G8" Then
            SeatMap = G8NAV.G8NAV4.SeatBooking(OrderId, tcCode, FT, PROMOCODE, OBFltDs.Tables(0).Rows(0)("AdtCabin").ToString().Trim().ToUpper())
        ElseIf Airline.ToUpper() = "SG" Then

            If CrdList IsNot Nothing AndAlso CrdList.Count > 0 AndAlso CrdList(0).LoginID.ToUpper().Trim() = "V4" Then
                SeatMap = SGNAV4.SeatBooking(OrderId, tcCode, FT, PROMOCODE)
            Else
                SeatMap = SpiceAPI.SeatBooking(OrderId, tcCode, FT, PROMOCODE)
            End If
        ElseIf Airline.ToUpper() = "6E" Then

            If CrdList IsNot Nothing AndAlso CrdList.Count > 0 AndAlso CrdList(0).LoginID.ToUpper().Trim() = "V4" Then
                SeatMap = _6ENAV.SeatBooking(OrderId, tcCode, FT, PROMOCODE, OBFltDs.Tables(0).Rows(0)("AdtCabin").ToString().Trim().ToUpper())
            Else
                SeatMap = SpiceAPI.SeatBooking(OrderId, tcCode, FT, PROMOCODE)
            End If
        Else
            Provider = "GDS"
            Dim Seg As SeatRequest = New SeatRequest()
            Seg.Adult = OBFltDs.Tables(0).Rows(0)("Adult").ToString()
            Seg.Child = OBFltDs.Tables(0).Rows(0)("Child").ToString()
            Seg.Infant = OBFltDs.Tables(0).Rows(0)("Infant").ToString()
            Seg.Provider = OBFltDs.Tables(0).Rows(0)("Provider").ToString()
            Seg.ValidatingCarrier = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier").ToString()
            Seg.AdtFare = OBFltDs.Tables(0).Rows(0)("AdtFare").ToString()
            Seg.ChdFare = OBFltDs.Tables(0).Rows(0)("ChdFare").ToString()
            Seg.TotalFare = OBFltDs.Tables(0).Rows(0)("OriginalTF").ToString()

            'If OBFltDs.Tables(0).Rows(0)("Status").ToString().ToUpper() = "CP" Then
            '    Seg.TCCode = OBFltDs.Tables(0).Rows(0)("CorpId").ToString()
            'Else
            '    Seg.TCCode = OBFltDs.Tables(0).Rows(0)("EntityId").ToString()
            'End If

            Seg.Trip = OBFltDs.Tables(0).Rows(0)("Trip").ToString()
            Seg.AdtCabin = If(OBFltDs.Tables(0).Rows(0)("AdtCabin").ToString(), "")
            Seg.ChdCabin = If(OBFltDs.Tables(0).Rows(0)("AdtCabin").ToString(), "")
            Seg.InfCabin = If(OBFltDs.Tables(0).Rows(0)("AdtCabin").ToString(), "")
            Dim UapiSegList As List(Of SeatRequestSegment) = New List(Of SeatRequestSegment)()

            For i As Integer = 0 To OBFltDs.Tables(0).Rows.Count() - 1
                Dim UapiSeg As SeatRequestSegment = New SeatRequestSegment()

                If String.IsNullOrEmpty(OBFltDs.Tables(0).Rows(i)("sno")) = True Then
                    UapiSeg.Conx = "N"
                Else
                    Dim sno As String() = Utility.Split(OBFltDs.Tables(0).Rows(i)("sno"), "~")
                    Dim conx As String = Utility.Split(sno(2), "::")(1).ToString()
                    UapiSeg.Key = Utility.Split(sno(0), "::")(1).ToString()
                    UapiSeg.Group = Utility.Split(sno(1), "::")(1).ToString()

                    If Not String.IsNullOrEmpty(conx) Then

                        If conx.Contains(i.ToString()) = True Then
                            UapiSeg.Conx = "Y"
                        Else
                            UapiSeg.Conx = "N"
                        End If
                    Else
                        UapiSeg.Conx = "N"
                    End If
                End If







                UapiSeg.Carrier = OBFltDs.Tables(0).Rows(i)("MarketingCarrier")
                UapiSeg.FlightNumber = OBFltDs.Tables(0).Rows(i)("FlightIdentification")
                UapiSeg.Origin = OBFltDs.Tables(0).Rows(i)("DepartureLocation")
                UapiSeg.Destination = OBFltDs.Tables(0).Rows(i)("ArrivalLocation")
                UapiSeg.DepartureTime = OBFltDs.Tables(0).Rows(i)("DepartureDate")
                UapiSeg.ArrivalTime = OBFltDs.Tables(0).Rows(i)("ArrivalDate")
                UapiSeg.ETicketability = OBFltDs.Tables(0).Rows(i)("ElectronicTicketing")
                UapiSeg.Equipment = OBFltDs.Tables(0).Rows(i)("EQ")
                UapiSeg.Provider = OBFltDs.Tables(0).Rows(i)("Provider")
                UapiSeg.AdtRbd = OBFltDs.Tables(0).Rows(i)("AdtRbd")
                UapiSeg.AdtFBCode = OBFltDs.Tables(0).Rows(i)("AdtFarebasis")
                UapiSeg.ChdRbd = OBFltDs.Tables(0).Rows(i)("ChdRbd")
                UapiSeg.AChdFBCode = OBFltDs.Tables(0).Rows(i)("ChdFarebasis")
                UapiSeg.InfRbd = OBFltDs.Tables(0).Rows(i)("InfRbd")
                UapiSeg.InfFBCode = OBFltDs.Tables(0).Rows(i)("InfFarebasis")
                UapiSegList.Add(UapiSeg)
            Next

            Dim Upfc As UAPIPrvtFareCode = New UAPIPrvtFareCode()
            Dim UpfcList As List(Of UAPIPrvtFareCode) = New List(Of UAPIPrvtFareCode)()
            Upfc.AccountCode = If(OBFltDs.Tables(0).Rows(0)("RESULTTYPE") = "C", OBFltDs.Tables(0).Rows(0)("Searchvalue"))
            Upfc.Provider = OBFltDs.Tables(0).Rows(0)("Provider")
            Upfc.SupplierCode = OBFltDs.Tables(0).Rows(0)("ValiDatingCarrier")
            UpfcList.Add(Upfc)
            Seg.AirSegList = UapiSegList
            Seg.PrvtFareCodeList = UpfcList
            Seg.IDType = OBFltDs.Tables(0).Rows(0)("RESULTTYPE")
            Dim UPTrns As Result_1G = New Result_1G(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
            '' SeatMap = UPTrns.SeatBooking(Seg)
        End If
        Dim Action As String = String.Empty
        Try
            If SeatMap.Count > 0 Then
                AssignSeat.Provider = Provider
                AssignSeat.Airline = Airline
                AssignSeat.Columns = SeatMap(0).Columns
                AssignSeat.Rows = SeatMap(0).Rows
                AssignSeat.SeatMapAll = SeatMap
                AssignSeat.PaxListDetails = objFltComm.TDPaxInformation(PaxDs).Where(Function(x) x.PaxType <> "INF").OrderBy(Function(m) m.PaxType).ToList()
            Else
                AssignSeat.[Error] = "Error_Availability"
            End If
        Catch ex As Exception
            AssignSeat.[Error] = "Error_Availability"
        End Try
        Return JsonConvert.SerializeObject(AssignSeat)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetPgChargeByMode(ByVal paymode As String) As String
        Dim TransCharge As String = "0~P"
        Dim PgCharge As String = "0"
        Dim ChargeType As String = "0"
        Dim objP As PG.PaymentGateway = New PG.PaymentGateway()
        ''Dim UserID As String = Session("UID").ToString()
        Dim UserID As String = HttpContext.Current.Session("UID").ToString()
        Try
            Dim pgDT As DataTable = objP.GetPgTransChargesByModeByAgentWise(UserID, paymode, "GetPgCharges")

            If pgDT IsNot Nothing Then

                If pgDT.Rows.Count > 0 Then

                    If Not String.IsNullOrEmpty(Convert.ToString(pgDT.Rows(0)("Charges"))) Then
                        PgCharge = Convert.ToString(pgDT.Rows(0)("Charges")).Trim()
                    Else
                        PgCharge = "0"
                    End If

                    If Not String.IsNullOrEmpty(Convert.ToString(pgDT.Rows(0)("ChargesType"))) Then
                        ChargeType = Convert.ToString(pgDT.Rows(0)("ChargesType")).Trim()
                    Else
                        ChargeType = "P"
                    End If

                    TransCharge = PgCharge & "~" & ChargeType
                End If
            End If

        Catch ex As Exception
            TransCharge = "0~P"
        End Try

        Return TransCharge
    End Function

End Class
