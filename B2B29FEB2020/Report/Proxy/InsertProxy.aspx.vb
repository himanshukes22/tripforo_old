Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports YatraBilling

Partial Public Class Reports_Proxy_InsertProxy
    Inherits System.Web.UI.Page
    Private con As New SqlConnection()
    Private adp As SqlDataAdapter
    Private ClsCorp As New ClsCorporate
    Private STDom As New SqlTransactionDom
    Private STYTR As New SqlTransactionYatra
    Private ST As New SqlTransaction
    Private objSql As New SqlTransactionNew
    Private CCAP As New clsCalcCommAndPlb
    Dim objSelectedfltCls As New clsInsertSelectedFlight
    Dim trackIdOneWay As String
    Dim trackIdRoundTrip As String
    Dim PxCD As String = ""
    Dim SpecialRT As Boolean = False
    'Added ProxyCode  Variable
    Private AFare As Double = 0, CFare As Double = 0, IFare As Double = 0, SerTax As Double = 0, AYQ As Double = 0, CYQ As Double = 0, TransFee As Double = 0, ReAFare As Double = 0, ReCFare As Double = 0, ReIFare As Double = 0, ReSerTax As Double = 0, ReAYQ As Double = 0, ReCYQ As Double = 0, ATax As Double = 0, CTax As Double = 0, ITax As Double = 0, IYQ As Double = 0, ReATax As Double = 0, ReCTax As Double = 0, ReITax As Double = 0, ReIYQ As Double = 0, Disc As Double = 0, DiscYQ As Double = 0, DiscBYQ As Double = 0, CB As Double = 0, ATotal As Double = 0, CTotal As Double = 0, ITotal As Double = 0, lblSTax As Double = 0, lblTFee As Double = 0, SerAFare As Double = 0, SerCFare As Double = 0, SerIFare As Double = 0, TraAFare As Double = 0, TraCFare As Double = 0, TraAYQ As Double = 0, TraCYQ As Double = 0, DAFare As Double = 0, DCFare As Double = 0, DIFare As Double = 0, DATax As Double = 0, DCTax As Double = 0, DITax As Double = 0, DIYQ As Double = 0, DAYQ As Double = 0, DCYQ As Double = 0, RDAFare As Double = 0, RDCFare As Double = 0, RDIFare As Double = 0, RDATax As Double = 0, RDCTax As Double = 0, RDITax As Double = 0, TransServiceTax As Double = 0, RDIYQ As Double = 0, RDAYQ As Double = 0, RDCYQ As Double = 0, RDDisc As Double = 0, RDDiscYQ As Double = 0, RDDiscBYQ As Double = 0, RDCB As Double = 0, TransTotalDiscount As Double = 0, TransCB As Double = 0, TransATotal As Double = 0, TransCtotal As Double = 0, TransITotal As Double = 0, TransTransFee As Double = 0, TransTDS As Double = 0, TransReTDS As Double = 0, TransABaseFare As Double = 0, TransCBaseFare As Double = 0, TransIBaseFare As Double = 0, TransATax As Double = 0, TransCTax As Double = 0, TransITax As Double = 0, TransAYQ As Double = 0, TransCYQ As Double = 0, TransIYQ As Double = 0, AYQ1 As Double = 0, CYQ1 As Double = 0, IYQ1 As Double = 0, ReAYQ1 As Double = 0, ReCYQ1 As Double = 0, ReIYQ1 As Double = 0, AYR As Double = 0, CYR As Double = 0, IYR As Double = 0, AWO As Double = 0, CWO As Double = 0, IWO As Double = 0, AOT As Double = 0, COT As Double = 0, IOT As Double = 0, REAYR As Double = 0, RECYR As Double = 0, REIYR As Double = 0, REAWO As Double = 0, RECWO As Double = 0, REIWO As Double = 0, REAOT As Double = 0, RECOT As Double = 0, REIOT As Double = 0
    Dim adtBF As Double = 0, adtTax As Double = 0, adtYQ As Double = 0, adtYR As Double = 0, adtWO As Double = 0, adtOT As Double = 0, adtTtl As Double = 0, chdBF As Double = 0, chdTax As Double = 0, chdYQ As Double = 0, chdYR As Double = 0, chdWO As Double = 0, chdOT As Double = 0, chdTtl As Double = 0, infBF As Double = 0, infTax As Double = 0, infYQ As Double = 0, infYR As Double = 0, infWO As Double = 0, infOT As Double = 0, infTtl As Double = 0, totFare As Double = 0, srvTax As Double = 0, tf As Double = 0, dis As Double = 0, disAdt As Double = 0, disChd As Double = 0, c_b As Double = 0, tfAfterDis As Double = 0, tds As Double = 0, ReadtBF As Double = 0, ReadtTax As Double = 0, ReadtYQ As Double = 0, ReadtYR As Double = 0, ReadtWO As Double = 0, ReadtOT As Double = 0, ReadtTtl As Double = 0, RechdBF As Double = 0, RechdTax As Double = 0, RechdYQ As Double = 0, RechdYR As Double = 0, RechdWO As Double = 0, RechdOT As Double = 0, RechdTtl As Double = 0, ReinfBF As Double = 0, ReinfTax As Double = 0, ReinfYQ As Double = 0, ReinfYR As Double = 0, ReinfWO As Double = 0, ReinfOT As Double = 0, ReinfTtl As Double = 0, RetotFare As Double = 0, ResrvTax As Double = 0, Retf As Double = 0, Redis As Double = 0, RedisAdt As Double = 0, RedisChd As Double = 0, Recb As Double = 0, RetfAfterDis As Double = 0, Retds As Double = 0
    'New Variable Lines Added 1st Line--> Transaction Fee ## 2nd Line -->Comission One Way ## 3rd line --> Round trip Comission
    Private TFeePerADT As Double = 0, TFeeADT As Double = 0, TFeePerCHD As Double = 0, TFeeCHD As Double = 0, TFee As Double = 0, CommADT As Double = 0, CommCHD As Double = 0, CommINF As Double = 0, TotalComm As Double = 0, CBChild As Double = 0, TotalCB As Double = 0, ReTotalCB As Double = 0
    Dim ReCommADT As Integer = 0, ReCommCHD As Integer = 0, ReCommINF As Integer = 0, ReTotalComm As Integer = 0, ReCBChild As Double = 0
    Private ReTFeePerADT As Double = 0, ReTFeeADT As Double = 0, ReTFeePerCHD As Double = 0, ReTFeeCHD As Double = 0, ReTFee As Double = 0
    Private dt As New DataTable(), dtcommADT As New DataTable, dtcommCHD, RedtcommADT As New DataTable, RedtcommCHD As New DataTable 'Added dt for Picking up data table
    Dim SrvchargOneWay As Double = 0, SrvchargTwoWay As Double = 0
    '*****
    Private CommADT1 As Double = 0, CommCHD1 As Double = 0, CommINF1 As Double = 0, TotalComm1 As Double = 0
    Dim ReCommADT1 As Integer = 0, ReCommCHD1 As Integer = 0, ReCommINF1 As Integer = 0, ReTotalComm1 As Integer = 0
    Dim STaxPerADT1 As Double = 0, STaxADT1 As Double = 0, STaxPerCHD1 As Double = 0, STaxCHD1 As Double = 0, STaxPerINF1 As Double = 0, STaxINF1 As Double = 0, STax1 As Double = 0
    Dim ReSTaxPerADT1 As Double = 0, ReSTaxADT1 As Double = 0, ReSTaxPerCHD1 As Double = 0, ReSTaxCHD1 As Double = 0, ReSTaxPerINF1 As Double = 0, ReSTaxINF1 As Double = 0, ReSTax1 As Double = 0

    'Special Fare Discount
    Dim SFTot As Double = 0, SFDis As Double = 0, ReSFDis As Double = 0
    Dim IsCorp As Boolean = False
    'We Will be Retriving D or I Value from Previous ProxyTicketDetail Page.
    'Need to Change D or I in BindProxyDetail Func and PxCD varaible on Top
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Session("UID") = "" Or Session("UID") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        End If
        If (Request("SpecialRT").ToUpper().Trim() = "YES") Then
            SpecialRT = True
        End If


        Try
            PxCD = Session("ExecTrip")
            IsCorp = Convert.ToBoolean(ViewState("IsCorp"))
            If Not IsPostBack Then
                Session("OneStatus") = "OneNotActive"
                Session("RoundStatus") = "RoundNotActive"
                BindProxyDetail()
                BindAdult()
                BindChild()
                BindInfrant()
                BindAdultDetail()
                If (PxCD = "I") Then
                    td_rbd.Visible = True
                    td_rerbd.Visible = True
                End If
                If td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True Then
                    pnl_Roundtrip.Visible = False
                    pnl_OneWay.Visible = True
                    pnl_onewaycal.Visible = True
                    pnl_roundtripcal.Visible = False
                    PanelRetrurn.Visible = False
                    trReturn.Visible = False
                    tr_OneWay.Visible = True
                    GridViewAdult.Columns(5).Visible = False
                    GridViewChild.Columns(5).Visible = False
                    GridViewInfrant.Columns(5).Visible = False
                    pnl_Depart.Visible = True
                    lbl_ProxyType.Text = "One Way"
                    If (SpecialRT = True) Then
                        td_SpecialRT.Visible = True
                        td_special_flight.Visible = True
                        td_special_flight1.Visible = True
                        lbl_ProxyType.Text = "Special Round Trip"
                    End If
                End If
                If td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False Then
                   
                        pnl_onewaycal.Visible = True
                        pnl_roundtripcal.Visible = True
                        pnl_Roundtrip.Visible = True
                        pnl_OneWay.Visible = True
                        PanelRetrurn.Visible = True
                        pnl_Depart.Visible = True
                        tr_OneWay.Visible = True
                        trReturn.Visible = True
                        GridViewAdult.Columns(5).Visible = True
                        GridViewChild.Columns(5).Visible = True
                        GridViewInfrant.Columns(5).Visible = True

                    lbl_ProxyType.Text = "Round Trip"

                    End If
                If td_Child.InnerText = "0" Then
                    tbl_Child.Visible = False
                    tr_Child.Visible = False
                    tbl_Rechild.Visible = False
                End If
                If td_Infrant.InnerText = "0" Then
                    tbl_Infrant.Visible = False
                    tr_Infrant.Visible = False
                    tbl_ReInfrant.Visible = False
                End If
            End If
        Catch ex As Exception

            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub Calc_BaseFare()
        Try
            If (td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) Then
                'Calculate One Way Fare
                If txt_ABaseFare.Text <> "" AndAlso txt_ABaseFare.Text IsNot Nothing Then
                    AFare = Convert.ToDouble(txt_ABaseFare.Text)
                End If
                If txt_CBaseFare.Text <> "" AndAlso txt_CBaseFare.Text IsNot Nothing Then
                    CFare = Convert.ToDouble(txt_CBaseFare.Text)
                End If

                If txt_IBaseFare.Text <> "" AndAlso txt_IBaseFare.Text IsNot Nothing Then
                    IFare = Convert.ToDouble(txt_IBaseFare.Text)
                End If
                If txt_AYQ.Text <> "" AndAlso txt_AYQ.Text IsNot Nothing Then
                    AYQ = Convert.ToDouble(txt_AYQ.Text)
                End If
                If txt_CYQ.Text <> "" AndAlso txt_CYQ.Text IsNot Nothing Then
                    CYQ = Convert.ToDouble(txt_CYQ.Text)
                End If
                If txt_IYQ.Text <> "" AndAlso txt_IYQ.Text IsNot Nothing Then
                    IYQ = Convert.ToDouble(txt_IYQ.Text)
                End If

                If txt_AYR.Text <> "" AndAlso txt_AYR.Text IsNot Nothing Then
                    AYR = Convert.ToDouble(txt_AYR.Text)
                End If
                If txt_CYR.Text <> "" AndAlso txt_CYR.Text IsNot Nothing Then
                    CYR = Convert.ToDouble(txt_CYR.Text)
                End If
                If txt_IYR.Text <> "" AndAlso txt_IYR.Text IsNot Nothing Then
                    IYR = Convert.ToDouble(txt_IYR.Text)
                End If

                If txt_AWO.Text <> "" AndAlso txt_AWO.Text IsNot Nothing Then
                    AWO = Convert.ToDouble(txt_AWO.Text)
                End If
                If txt_CWO.Text <> "" AndAlso txt_CWO.Text IsNot Nothing Then
                    CWO = Convert.ToDouble(txt_CWO.Text)
                End If
                If txt_IWO.Text <> "" AndAlso txt_IWO.Text IsNot Nothing Then
                    IWO = Convert.ToDouble(txt_IWO.Text)
                End If
                If txt_AOT.Text <> "" AndAlso txt_AOT.Text IsNot Nothing Then
                    AOT = Convert.ToDouble(txt_AOT.Text)
                End If
                If txt_COT.Text <> "" AndAlso txt_COT.Text IsNot Nothing Then
                    COT = Convert.ToDouble(txt_COT.Text)
                End If
                If txt_IOT.Text <> "" AndAlso txt_IOT.Text IsNot Nothing Then
                    IOT = Convert.ToDouble(txt_IYQ.Text)
                End If

                If td_Adult.InnerText > 0 Then ' No. of Adults
                    adtBF = Convert.ToDouble(txt_ABaseFare.Text) 'Per Adult base Fare
                    adtYQ = Convert.ToDouble(txt_AYQ.Text)
                    adtYR = Convert.ToDouble(txt_AYR.Text)
                    adtWO = Convert.ToDouble(txt_AWO.Text)
                    adtOT = Convert.ToDouble(txt_AOT.Text)
                    adtTtl = adtBF + adtYQ + adtYR + adtWO + adtOT
                    txt_ATotal.Text = adtTtl.ToString 'Per Adult total Fare
                Else
                    txt_ATotal.Text = 0
                End If
                If td_Child.InnerText > 0 Then ' No. of Childs
                    chdBF = Convert.ToDouble(txt_CBaseFare.Text)
                    chdYQ = Convert.ToDouble(txt_CYQ.Text)
                    chdYR = Convert.ToDouble(txt_CYR.Text)
                    chdWO = Convert.ToDouble(txt_CWO.Text)
                    chdOT = Convert.ToDouble(txt_COT.Text)
                    chdTtl = chdBF + chdYQ + chdYR + chdWO + chdOT
                    txt_Ctotal.Text = chdTtl.ToString
                Else
                    txt_Ctotal.Text = 0
                End If
                If td_Infrant.InnerText > 0 Then 'No. of Infant
                    infBF = Convert.ToDouble(txt_IBaseFare.Text)  'Per Infant base Fare
                    infYQ = Convert.ToDouble(txt_IYQ.Text)
                    infYR = Convert.ToDouble(txt_IYR.Text)
                    infWO = Convert.ToDouble(txt_IWO.Text)
                    infOT = Convert.ToDouble(txt_IOT.Text)
                    infTtl = infBF + infYQ + infYR + infWO + infOT
                    txt_ITotal.Text = infTtl.ToString 'Per infant total Fare
                Else
                    txt_ITotal.Text = 0
                End If

            End If

            If (td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False) Then
                'Calculate  One Way Fare AND Round Trip Fare BOTH

                'Calculate One Way Fare
                If txt_ABaseFare.Text <> "" AndAlso txt_ABaseFare.Text IsNot Nothing Then
                    AFare = Convert.ToDouble(txt_ABaseFare.Text)
                End If
                If txt_CBaseFare.Text <> "" AndAlso txt_CBaseFare.Text IsNot Nothing Then
                    CFare = Convert.ToDouble(txt_CBaseFare.Text)
                End If

                If txt_IBaseFare.Text <> "" AndAlso txt_IBaseFare.Text IsNot Nothing Then
                    IFare = Convert.ToDouble(txt_IBaseFare.Text)
                End If
                If txt_AYQ.Text <> "" AndAlso txt_AYQ.Text IsNot Nothing Then
                    AYQ = Convert.ToDouble(txt_AYQ.Text)
                End If
                If txt_CYQ.Text <> "" AndAlso txt_CYQ.Text IsNot Nothing Then
                    CYQ = Convert.ToDouble(txt_CYQ.Text)
                End If
                If txt_IYQ.Text <> "" AndAlso txt_IYQ.Text IsNot Nothing Then
                    IYQ = Convert.ToDouble(txt_IYQ.Text)
                End If

                If txt_AYR.Text <> "" AndAlso txt_AYR.Text IsNot Nothing Then
                    AYR = Convert.ToDouble(txt_AYR.Text)
                End If
                If txt_CYR.Text <> "" AndAlso txt_CYR.Text IsNot Nothing Then
                    CYR = Convert.ToDouble(txt_CYR.Text)
                End If
                If txt_IYR.Text <> "" AndAlso txt_IYR.Text IsNot Nothing Then
                    IYR = Convert.ToDouble(txt_IYR.Text)
                End If

                If txt_AWO.Text <> "" AndAlso txt_AWO.Text IsNot Nothing Then
                    AWO = Convert.ToDouble(txt_AWO.Text)
                End If
                If txt_CWO.Text <> "" AndAlso txt_CWO.Text IsNot Nothing Then
                    CWO = Convert.ToDouble(txt_CWO.Text)
                End If
                If txt_IWO.Text <> "" AndAlso txt_IWO.Text IsNot Nothing Then
                    IWO = Convert.ToDouble(txt_IWO.Text)
                End If
                If txt_AOT.Text <> "" AndAlso txt_AOT.Text IsNot Nothing Then
                    AOT = Convert.ToDouble(txt_AOT.Text)
                End If
                If txt_COT.Text <> "" AndAlso txt_COT.Text IsNot Nothing Then
                    COT = Convert.ToDouble(txt_COT.Text)
                End If
                If txt_IOT.Text <> "" AndAlso txt_IOT.Text IsNot Nothing Then
                    IOT = Convert.ToDouble(txt_IYQ.Text)
                End If

                If td_Adult.InnerText > 0 Then ' No. of Adults
                    adtBF = Convert.ToDouble(txt_ABaseFare.Text) 'Per Adult base Fare
                    adtYQ = Convert.ToDouble(txt_AYQ.Text)
                    adtYR = Convert.ToDouble(txt_AYR.Text)
                    adtWO = Convert.ToDouble(txt_AWO.Text)
                    adtOT = Convert.ToDouble(txt_AOT.Text)
                    adtTtl = adtBF + adtYQ + adtYR + adtWO + adtOT
                    txt_ATotal.Text = adtTtl.ToString 'Per Adult total Fare
                Else
                    txt_ATotal.Text = 0
                End If
                If td_Child.InnerText > 0 Then ' No. of Childs
                    chdBF = Convert.ToDouble(txt_CBaseFare.Text)
                    chdYQ = Convert.ToDouble(txt_CYQ.Text)
                    chdYR = Convert.ToDouble(txt_CYR.Text)
                    chdWO = Convert.ToDouble(txt_CWO.Text)
                    chdOT = Convert.ToDouble(txt_COT.Text)
                    chdTtl = chdBF + chdYQ + chdYR + chdWO + chdOT
                    txt_Ctotal.Text = chdTtl.ToString
                Else
                    txt_Ctotal.Text = 0
                End If
                If td_Infrant.InnerText > 0 Then 'No. of Infant
                    infBF = Convert.ToDouble(txt_IBaseFare.Text)  'Per Infant base Fare
                    infYQ = Convert.ToDouble(txt_IYQ.Text)
                    infYR = Convert.ToDouble(txt_IYR.Text)
                    infWO = Convert.ToDouble(txt_IWO.Text)
                    infOT = Convert.ToDouble(txt_IOT.Text)
                    infTtl = infBF + infYQ + infYR + infWO + infOT
                    txt_ITotal.Text = infTtl.ToString 'Per infant total Fare
                Else
                    txt_ITotal.Text = 0
                End If

                'Calculate  Round Trip  Fare
                If txt_ReABaseFare.Text <> "" AndAlso txt_ReABaseFare.Text IsNot Nothing Then
                    ReAFare = Convert.ToDouble(txt_ReABaseFare.Text)
                End If
                If txt_ReCBaseFare.Text <> "" AndAlso txt_ReCBaseFare.Text IsNot Nothing Then
                    ReCFare = Convert.ToDouble(txt_ReCBaseFare.Text)
                End If

                If txt_ReIBaseFare.Text <> "" AndAlso txt_ReIBaseFare.Text IsNot Nothing Then
                    ReIFare = Convert.ToDouble(txt_ReIBaseFare.Text)
                End If

                If txt_ReAYQ.Text <> "" AndAlso txt_ReAYQ.Text IsNot Nothing Then
                    ReAYQ = Convert.ToDouble(txt_ReAYQ.Text)
                End If
                If txt_ReCYQ.Text <> "" AndAlso txt_ReCYQ.Text IsNot Nothing Then
                    ReCYQ = Convert.ToDouble(txt_ReCYQ.Text)
                End If
                If txt_ReIYQ.Text <> "" AndAlso txt_ReIYQ.Text IsNot Nothing Then
                    ReIYQ = Convert.ToDouble(txt_ReIYQ.Text)
                End If

                If txt_ReAYR.Text <> "" AndAlso txt_ReAYR.Text IsNot Nothing Then
                    REAYR = Convert.ToDouble(txt_ReAYR.Text)
                End If
                If txt_ReCYR.Text <> "" AndAlso txt_ReCYR.Text IsNot Nothing Then
                    RECYR = Convert.ToDouble(txt_ReCYR.Text)
                End If
                If txt_ReIYR.Text <> "" AndAlso txt_ReIYR.Text IsNot Nothing Then
                    REIYR = Convert.ToDouble(txt_ReIYR.Text)
                End If

                If txt_ReAWO.Text <> "" AndAlso txt_ReAWO.Text IsNot Nothing Then
                    REAWO = Convert.ToDouble(txt_ReAWO.Text)
                End If
                If txt_ReCWO.Text <> "" AndAlso txt_ReCWO.Text IsNot Nothing Then
                    RECWO = Convert.ToDouble(txt_ReCWO.Text)
                End If
                If txt_ReIWO.Text <> "" AndAlso txt_ReIWO.Text IsNot Nothing Then
                    REIWO = Convert.ToDouble(txt_ReIWO.Text)
                End If

                If txt_ReAOT.Text <> "" AndAlso txt_ReAOT.Text IsNot Nothing Then
                    REAOT = Convert.ToDouble(txt_ReAOT.Text)
                End If
                If txt_ReCOT.Text <> "" AndAlso txt_ReCOT.Text IsNot Nothing Then
                    RECOT = Convert.ToDouble(txt_ReCOT.Text)
                End If
                If txt_ReIOT.Text <> "" AndAlso txt_ReIOT.Text IsNot Nothing Then
                    REIOT = Convert.ToDouble(txt_ReIYQ.Text)
                End If

                If td_Adult.InnerText > 0 Then
                    ReadtBF = Convert.ToDouble(txt_ReABaseFare.Text)
                    ReadtYQ = Convert.ToDouble(txt_ReAYQ.Text)
                    ReadtYR = Convert.ToDouble(txt_ReAYR.Text)
                    ReadtWO = Convert.ToDouble(txt_ReAWO.Text)
                    ReadtOT = Convert.ToDouble(txt_ReAOT.Text)
                    ReadtTtl = ReadtBF + ReadtYQ + ReadtYR + ReadtWO + ReadtOT
                    txt_ReATotal.Text = ReadtTtl.ToString
                Else
                    txt_ReATotal.Text = 0
                End If

                If td_Child.InnerText > 0 Then
                    RechdBF = Convert.ToDouble(txt_ReCBaseFare.Text)
                    RechdYQ = Convert.ToDouble(txt_ReCYQ.Text)
                    RechdYR = Convert.ToDouble(txt_ReCYR.Text)
                    RechdWO = Convert.ToDouble(txt_ReCWO.Text)
                    RechdOT = Convert.ToDouble(txt_ReCOT.Text)
                    RechdTtl = RechdBF + RechdYQ + RechdYR + RechdWO + RechdOT
                    txt_ReCtotal.Text = RechdTtl.ToString
                Else
                    txt_ReCtotal.Text = 0
                End If
                If td_Infrant.InnerText > 0 Then
                    ReinfBF = Convert.ToDouble(txt_ReIBaseFare.Text)
                    ReinfYQ = Convert.ToDouble(txt_ReIYQ.Text)
                    ReinfYR = Convert.ToDouble(txt_ReIYR.Text)
                    ReinfWO = Convert.ToDouble(txt_ReIWO.Text)
                    ReinfOT = Convert.ToDouble(txt_ReIOT.Text)
                    ReinfTtl = ReinfBF + ReinfYQ + ReinfYR + ReinfWO + ReinfOT
                    txt_ReITotal.Text = ReinfTtl.ToString
                Else
                    txt_ReITotal.Text = 0
                End If

            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub Calc_D_Comm(ByRef ADTTRFee As Double, ByRef CHDTRFee As Double, Optional ByRef ReADTTRFee As Double = 0, Optional ByRef ReCHDTRFee As Double = 0)
        Try
            If (td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) Then

                'Calculate Commission And Cash Back One Way
                Dim GroupType As String = ""
                Dim dsAType As New DataSet()
                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                Dim dtAType As New DataTable()
                dtAType = dsAType.Tables(0)
                GroupType = dtAType.Rows(0)("agent_type").ToString()
                Dim CBAdult As Double = 0
                Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0, STax As Double = 0
                CBChild = 0

                If (IsCorp = True) Then
                    '============================================FOR CORPORATE====================================
                    dtcommADT = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), "D", Convert.ToDouble(txt_ATotal.Text)).Tables(0)
                    CommADT = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    CBAdult = 0
                    Dim MgtFeeINF As Double = 0
                    Dim dtMgtFee As New DataTable
                    If (td_Child.InnerText > 0) Then
                        dtcommCHD = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), "D", Convert.ToDouble(txt_Ctotal.Text)).Tables(0)
                        CommCHD = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        CBChild = 0
                        'For child Cashback
                    End If
                    If (td_Infrant.InnerText > 0) Then
                        dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(IFare), Convert.ToDouble(IYQ), "D", Convert.ToDouble(txt_ITotal.Text)).Tables(0)
                        MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If

                    TotalComm = CommADT + CommCHD + MgtFeeINF
                    TotalCB = 0
                    TotalCB = 0
                    'End Calculate Commission
                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblTF.Text = "0"
                    If lblTF.Text <> "" AndAlso lblTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblTF.Text)
                    End If
                    lbl_TransFee.Text = "0"
                    lbl_STax.Text = "0"
                    If lbl_STax.Text <> "" AndAlso lbl_STax.Text IsNot Nothing Then
                        SerTax = Convert.ToDouble(lbl_STax.Text)
                    End If
                    STaxPerADT = Math.Round(((Convert.ToDouble(dtcommADT.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        STaxPerCHD = Math.Round(((Convert.ToDouble(dtcommCHD.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If

                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxINF = Math.Round((STaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If

                    STax = STaxADT + STaxCHD + STaxINF
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    'End Calculation ServiceTax
                    '===========================================END CORPORATE=====================================
                Else
                    dtcommADT = CCAP.calcCommDom(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), 1)
                    CommADT = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    CommADT1 = CommADT '*****
                    If txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then
                            CBAdult = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                        Else
                            CBAdult = 0
                        End If
                    Else
                        CBAdult = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    End If
                    If (td_Child.InnerText > 0) Then
                        dtcommCHD = CCAP.calcCommDom(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), 1)
                        CommCHD = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        CommCHD1 = CommCHD '*****
                        If txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then
                                CBChild = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                            Else
                                CBChild = 0
                            End If
                        Else
                            CBChild = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        End If
                        'For child Cashback
                    End If
                    TotalComm = CommADT + CommCHD
                    TotalComm1 = TotalComm
                    TotalCB = 0
                    TotalCB = CBAdult + CBChild
                    'End Calculate Commission
                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    dt = ST.calcServicecharge(txt_TktingAirline.Text, "D").Tables(0)
                    'Calculate Transaction Fee
                    If (dt.Rows.Count > 0) Then
                        lblTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblTF.Text = "0"
                    End If

                    If lblTF.Text <> "" AndAlso lblTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblTF.Text)
                    End If
                    Dim TFeePerADT As Double = ((AFare + AYQ) * TransFee) / 100
                    Dim TFeeADT As Double = Math.Round((TFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim TFeePerCHD As Double = ((CFare + CYQ) * TransFee) / 100
                    Dim TFeeCHD As Double = Math.Round((TFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim TFee As Double = TFeeADT + TFeeCHD
                    lbl_TransFee.Text = Convert.ToString(TFee)
                    'End Transaction Fee Calculation
                    If (dt.Rows.Count > 0) Then
                        lbl_STax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        lbl_STax.Text = "0"
                    End If
                    If lbl_STax.Text <> "" AndAlso lbl_STax.Text IsNot Nothing Then
                        SerTax = Convert.ToDouble(lbl_STax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        STaxPerADT = Math.Round(((((dtcommADT.Rows(0)("Dis").ToString()) - TFeePerADT) * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    Else
                        STaxPerADT = Math.Round(((dtcommADT.Rows(0)("Dis").ToString() * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT

                    End If
                    'Per Child SeviceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            STaxPerCHD = Math.Round((((dtcommCHD.Rows(0)("Dis").ToString() - TFeePerCHD) * SerTax)), 0) / 100
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        Else
                            STaxPerCHD = Math.Round(((dtcommCHD.Rows(0)("Dis").ToString() * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        End If
                    End If
                    STax = STaxADT + STaxCHD
                    TotalComm = TotalComm - STax
                    STax1 = STax
                    STax = 0
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    'End Calculation ServiceTax
                End If
                ADTTRFee = TFeePerADT 'Send Trans Fee value
                CHDTRFee = TFeePerCHD 'Send Trans Fee value
                '''''''''''''''''''''''''''''''''''''''''End Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
            If (td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False) Then
                ''''''''''''''''''''''''''''Calculate Commission And Cash Back For Both 1way and Round Trip'''''''''''''''''''''''''''''''''''''''''
                'Calculate Commission And Cash Back One Way
                Dim GroupType As String = ""
                Dim dsAType As New DataSet()
                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                Dim dtAType As New DataTable()
                dtAType = dsAType.Tables(0)
                GroupType = dtAType.Rows(0)("agent_type").ToString()
                Dim CBAdult As Double = 0
                Dim dtMgtFee As New DataTable
                If (IsCorp = True) Then
                    '=============================================FOR CORPORATE===========================================================
                    dtcommADT = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), "D", Convert.ToDouble(txt_ATotal.Text)).Tables(0)
                    CommADT = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    CBAdult = 0
                    Dim MgtFeeINF As Double = 0
                    'Dim dtcommCHD As New DataTable
                    If (td_Child.InnerText > 0) Then
                        dtcommCHD = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), "D", Convert.ToDouble(txt_Ctotal.Text)).Tables(0)
                        CommCHD = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        CBChild = 0
                        'For child Cashback
                    End If
                    If (td_Infrant.InnerText > 0) Then
                        dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(IFare), Convert.ToDouble(IYQ), "D", Convert.ToDouble(txt_ITotal.Text)).Tables(0)
                        MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If
                    TotalComm = CommADT + CommCHD + MgtFeeINF
                    TotalCB = 0
                    TotalCB = CBAdult + CBChild
                    'End Calculate Commission
                    '=============================================FOR CORPORATE===========================================================
                Else
                    dtcommADT = CCAP.calcCommDom(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), 1)
                    CommADT = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    CommADT1 = CommADT
                    'For ADULT Cashback
                    If txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then
                            CBAdult = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                        Else
                            CBAdult = 0
                        End If
                    Else
                        CBAdult = Math.Round(Convert.ToDouble(dtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    End If
                    'Dim dtcommCHD As New DataTable
                    If (td_Child.InnerText > 0) Then
                        dtcommCHD = CCAP.calcCommDom(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), 1)
                        CommCHD = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        CommCHD1 = CommCHD
                        If txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then
                                CBChild = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                            Else
                                CBChild = 0
                            End If
                        Else
                            CBChild = Math.Round(Convert.ToDouble(dtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        End If
                        'For child Cashback
                    End If
                    TotalComm = CommADT + CommCHD
                    TotalComm1 = TotalComm
                    TotalCB = 0
                    TotalCB = CBAdult + CBChild
                    'End Calculate Commission
                End If
                Dim ReGroupType As String = ""
                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                dtAType = dsAType.Tables(0)
                ReGroupType = dtAType.Rows(0)("agent_type").ToString()
                Dim ReCBAdult As Double = 0
                Dim ReTotalCB As Double = 0
                Dim RedtMgtFee As New DataTable
                Dim ReMgtFeeINF As Double = 0
                If (IsCorp = True) Then
                    RedtcommADT = ClsCorp.GetManagementFeeSrvTax(ReGroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReAFare), Convert.ToDouble(ReAYQ), "D", Convert.ToDouble(txt_ReATotal.Text)).Tables(0)
                    ReCommADT = Math.Round(Convert.ToDouble(RedtcommADT.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    ReCBAdult = 0
                    If (td_Child.InnerText > 0) Then
                        RedtcommCHD = ClsCorp.GetManagementFeeSrvTax(ReGroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReCFare), Convert.ToDouble(ReCYQ), "D", Convert.ToDouble(txt_ReCtotal.Text)).Tables(0)
                        ReCommCHD = Math.Round(Convert.ToDouble(RedtcommCHD.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        ReCBChild = 0
                    End If

                    If (td_Infrant.InnerText > 0) Then
                        RedtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReIFare), Convert.ToDouble(ReIYQ), "D", Convert.ToDouble(txt_ReITotal.Text)).Tables(0)
                        ReMgtFeeINF = Math.Round(Convert.ToDouble(RedtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If


                    ReTotalComm = ReCommADT + ReCommCHD + ReMgtFeeINF
                    ReTotalCB = ReCBAdult + ReCBChild
                    '''''''''''''''''''''''''''''End Calculate Commission''''''''''''''''''''''''''''''''''''
                Else
                    RedtcommADT = CCAP.calcCommDom(ReGroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReAFare), Convert.ToDouble(ReAYQ), 1)
                    ReCommADT = Math.Round(Convert.ToDouble(RedtcommADT.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    ReCommADT1 = ReCommADT

                    If txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                        If td_To.InnerText & ":" & td_From.InnerText <> "DEL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:PAT" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXC" And td_To.InnerText & ":" & td_From.InnerText <> "IXC:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:PAT" Then
                            ReCBAdult = Math.Round(Convert.ToDouble(RedtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                        Else
                            ReCBAdult = 0
                        End If
                    Else
                        ReCBAdult = Math.Round(Convert.ToDouble(RedtcommADT.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Adult.InnerText)
                    End If
                    'Dim RedtcommCHD As New DataTable
                    If (td_Child.InnerText > 0) Then
                        RedtcommCHD = CCAP.calcCommDom(ReGroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReCFare), Convert.ToDouble(ReCYQ), 1)
                        ReCommCHD = Math.Round(Convert.ToDouble(RedtcommCHD.Rows(0)("Dis").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        ReCommCHD1 = ReCommCHD
                        If txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                            If td_To.InnerText & ":" & td_From.InnerText <> "DEL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:PAT" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXC" And td_To.InnerText & ":" & td_From.InnerText <> "IXC:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:PAT" Then
                                ReCBChild = Math.Round(Convert.ToDouble(RedtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                            Else
                                ReCBChild = 0
                            End If
                        Else
                            ReCBChild = Math.Round(Convert.ToDouble(RedtcommCHD.Rows(0)("CB").ToString()), 0) * Convert.ToDouble(td_Child.InnerText)
                        End If
                    End If
                    ReTotalComm = ReCommADT + ReCommCHD
                    ReTotalComm1 = ReTotalComm
                    ReTotalCB = ReCBAdult + ReCBChild
                    '''''''''''''''''''''''''''''End Calculate Commission''''''''''''''''''''''''''''''''''''
                End If
                '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0, STax As Double = 0
                'Dim STaxPerADT1 As Double = 0, STaxADT1 As Double = 0, STaxPerCHD1 As Double = 0, STaxCHD1 As Double = 0, STaxPerINF1 As Double = 0, STaxINF1 As Double = 0, STax1 As Double = 0

                If (IsCorp = True) Then
                    lblTF.Text = "0"
                    TransFee = 0
                    lbl_TransFee.Text = 0
                    lbl_STax.Text = "0"
                    SerTax = 0 ' Convert.ToDouble(lbl_STax.Text)
                    STaxPerADT = Math.Round(((Convert.ToDouble(dtcommADT.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    'Per Child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        STaxPerCHD = Math.Round(((Convert.ToDouble(dtcommCHD.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If

                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxINF = Math.Round((STaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If

                    STax = STaxADT + STaxCHD + STaxINF
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    'End Calculation ServiceTax
                Else
                    dt = ST.calcServicecharge(txt_TktingAirline.Text, "D").Tables(0)
                    'Calculate Transaction Fee
                    If (dt.Rows.Count > 0) Then
                        lblTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblTF.Text = "0"
                    End If
                    If lblTF.Text <> "" AndAlso lblTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblTF.Text)
                    End If
                    Dim TFeePerADT As Double = ((AFare + AYQ) * TransFee) / 100
                    Dim TFeeADT As Double = Math.Round((TFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim TFeePerCHD As Double = ((CFare + CYQ) * TransFee) / 100
                    Dim TFeeCHD As Double = Math.Round((TFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim TFee As Double = TFeeADT + TFeeCHD
                    lbl_TransFee.Text = Convert.ToString(TFee)
                    'End Transaction Fee Calculation
                    If (dt.Rows.Count > 0) Then
                        lbl_STax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        lbl_STax.Text = "0"
                    End If

                    If lbl_STax.Text <> "" AndAlso lbl_STax.Text IsNot Nothing Then
                        SerTax = Convert.ToDouble(lbl_STax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        STaxPerADT = Math.Round(((((dtcommADT.Rows(0)("Dis").ToString()) - TFeePerADT) * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    Else
                        STaxPerADT = Math.Round(((dtcommADT.Rows(0)("Dis").ToString() * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    End If
                    'Per Child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            STaxPerCHD = Math.Round((((dtcommCHD.Rows(0)("Dis").ToString() - TFeePerCHD) * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        Else
                            STaxPerCHD = Math.Round(((dtcommCHD.Rows(0)("Dis").ToString() * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD

                        End If

                    End If

                    STax = STaxADT + STaxCHD
                    TotalComm = TotalComm - STax
                    STax1 = STax
                    STax = 0
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    'End Calculation ServiceTax
                End If
                ADTTRFee = TFeePerADT 'Send Trans Fee value
                CHDTRFee = TFeePerCHD 'Send Trans Fee value
                '''''''''''''''''''''''''''''''''''''''''End Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''Calculate For Round Trip'''''''''''''''''''''''''''''''''''''''''''''''
                'ReServiceTax for Round Trip
                Dim ReSTaxPerADT As Double = 0, ReSTaxADT As Double = 0, ReSTaxPerCHD As Double = 0, ReSTaxCHD As Double = 0, ReSTaxPerINF As Double = 0, ReSTaxINF As Double = 0, ReSTax As Double = 0
                If (IsCorp = True) Then
                    lblReTF.Text = "0"
                    TransFee = 0
                    lbl_ReTransFee.Text = 0
                    Lbl_ReSTax.Text = "0"
                    ReSerTax = 0
                    ReSTaxPerADT = Math.Round((((Convert.ToDouble(RedtcommADT.Rows(0)("MGTSRVTAX").ToString())))), 0)
                    ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    'Per Child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        ReSTaxPerCHD = Math.Round((((Convert.ToDouble(RedtcommCHD.Rows(0)("MGTSRVTAX").ToString())))), 0)
                        ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If
                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        ReSTaxPerINF = Math.Round(((Convert.ToDouble(RedtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        ReSTaxINF = Math.Round((ReSTaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If
                    ReSTax = ReSTaxADT + ReSTaxCHD + ReSTaxINF
                    lbl_ReServiceTax.Text = Convert.ToString(ReSTax)
                    'End Calculation ReServiceTax
                Else

                    dt = ST.calcServicecharge(txt_ReTktingAirline.Text, "D").Tables(0)
                    If (dt.Rows.Count > 0) Then
                        lblReTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblReTF.Text = "0"
                    End If

                    If lblReTF.Text <> "" AndAlso lblReTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblReTF.Text)
                    End If
                    Dim ReTFeePerADT As Double = ((ReAFare + ReAYQ) * TransFee) / 100
                    Dim ReTFeeADT As Double = Math.Round((ReTFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim ReTFeePerCHD As Double = ((ReCFare + ReCYQ) * TransFee) / 100
                    Dim ReTFeeCHD As Double = Math.Round((ReTFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim ReTFee As Double = ReTFeeADT + ReTFeeCHD
                    lbl_ReTransFee.Text = Convert.ToString(ReTFee)
                    If (dt.Rows.Count > 0) Then
                        Lbl_ReSTax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        Lbl_ReSTax.Text = "0"
                    End If

                    If Lbl_ReSTax.Text <> "" AndAlso Lbl_ReSTax.Text IsNot Nothing Then
                        ReSerTax = Convert.ToDouble(Lbl_ReSTax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                        ReSTaxPerADT = Math.Round(((((RedtcommADT.Rows(0)("Dis").ToString()) - ReTFeePerADT) * ReSerTax) / 100), 0)
                        ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        ReSTaxPerADT1 = ReSTaxPerADT
                        ReSTaxADT1 = ReSTaxADT
                    Else
                        ReSTaxPerADT = Math.Round(((RedtcommADT.Rows(0)("Dis").ToString() * ReSerTax) / 100), 0)
                        ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        ReSTaxPerADT1 = ReSTaxPerADT
                        ReSTaxADT1 = ReSTaxADT
                    End If
                    'Per Child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                            ReSTaxPerCHD = Math.Round((((RedtcommCHD.Rows(0)("Dis").ToString() - ReTFeePerCHD) * ReSerTax) / 100), 0)
                            ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            ReSTaxPerCHD1 = ReSTaxPerCHD
                            ReSTaxCHD1 = ReSTaxCHD
                        Else
                            ReSTaxPerCHD = Math.Round(((RedtcommCHD.Rows(0)("Dis").ToString() * ReSerTax) / 100), 0)
                            ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            ReSTaxPerCHD1 = ReSTaxPerCHD
                            ReSTaxCHD1 = ReSTaxCHD
                        End If
                    End If
                    ReSTax = ReSTaxADT + ReSTaxCHD
                    ReTotalComm = ReTotalComm - ReSTax
                    ReSTax1 = ReSTax
                    ReSTax = 0
                    lbl_ReServiceTax.Text = Convert.ToString(ReSTax)
                    'End Calculation ReServiceTax
                End If
                'Calculate ReTransaction Fee
                ReADTTRFee = ReTFeePerADT
                ReCHDTRFee = ReTFeePerCHD
                'End ReTransaction Fee Calculation
            End If
            '''''''''''''''''''''''''''''End Service Tax One Way and Round Trip Both'''''''''''''''''''''''
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub Calc_I_Comm(ByRef ADTComm As Double, ByRef CHDComm As Double, Optional ByRef ReADTComm As Double = 0, Optional ByRef ReCHDComm As Double = 0) 'need to call 
        'Calculate Commission And NO Cash Back in Intl
        Try
            Dim Adminmrkadt As Double = 0
            Dim Adminmrkchd As Double = 0
            Dim dtgetmrk As New DataTable
            If (td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) Then
                'One Way Comm and CashBack
                Dim Origin As String = "", Dest As String = "", cls As String = rbd.Text.ToUpper 'CLs for RBD
                Dim ADT As Double = Convert.ToDouble(td_Adult.InnerText)
                Dim CHD As Double = Convert.ToDouble(td_Child.InnerText)
                Dim GroupType As String = ""
                Dim dsAType As New DataSet()
                Dim dtAType As New DataTable()

                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                dtAType = dsAType.Tables(0)
                GroupType = dtAType.Rows(0)("agent_type").ToString()
                Origin = td_From.InnerText
                Dest = td_To.InnerText
                Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0, STax As Double = 0

                If (IsCorp = True) Then
                    '=========================================================CORPORATE INTL===========================================
                    Dim dtmgtcommadt As New DataTable
                    Dim dtmgtcommchd As New DataTable
                    dtmgtcommadt = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), "I", Convert.ToDouble(txt_ATotal.Text)).Tables(0)
                    CommADT = Convert.ToDouble(dtmgtcommadt.Rows(0)("MGTFEE").ToString())
                    'MARKUP
                    dtgetmrk = ClsCorp.GetMarkUp(dtAType.Rows(0)("User_Id").ToString(), "SPRING", "I", "AD").Tables(0)
                    If (dtgetmrk.Rows.Count > 0) Then
                        Adminmrkadt = ClsCorp.CalcMarkup(dtgetmrk, txt_TktingAirline.Text.Trim, AFare, "I")
                        ViewState("Adminmrkadt") = Adminmrkadt
                    End If
                    'END MARKUP

                    Dim MgtFeeINF As Double = 0
                    Dim dtMgtFee As New DataTable

                    If (CHD > 0) Then
                        dtmgtcommchd = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), "I", Convert.ToDouble(txt_Ctotal.Text)).Tables(0)
                        CommCHD = Convert.ToDouble(dtmgtcommchd.Rows(0)("MGTFEE").ToString())
                        If (dtgetmrk.Rows.Count > 0) Then
                            Adminmrkchd = ClsCorp.CalcMarkup(dtgetmrk, txt_TktingAirline.Text.Trim, CFare, "I")
                            ViewState("Adminmrkchd") = Adminmrkchd
                        End If
                    End If
                    If (td_Infrant.InnerText > 0) Then
                        dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(IFare), Convert.ToDouble(IYQ), "I", Convert.ToDouble(txt_ITotal.Text)).Tables(0)
                        MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If

                    TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD) + MgtFeeINF
                    'End Calculate Commission
                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblTF.Text = "0"
                    TransFee = 0
                    lbl_TransFee.Text = "0"
                    lbl_STax.Text = "0"
                    SerTax = 0
                    STaxPerADT = Math.Round(((Convert.ToDouble(dtmgtcommadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    'per child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        STaxPerCHD = Math.Round(((Convert.ToDouble(dtmgtcommchd.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If
                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxINF = Math.Round((STaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If
                    STax = STaxADT + STaxCHD + STaxINF
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    '''''''''''''''''''''''''''''''''''''''''End Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '=========================================================END CORPORATE INTL=======================================
                Else

                    CommADT = CCAP.calcComm(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), Origin, Dest, cls, 0, td_DepartDate.InnerText.Trim().Replace("/", ""), "")
                    CommADT1 = CommADT '*****
                    If (td_Child.InnerText > 0) Then
                        CommCHD = CCAP.calcComm(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), Origin, Dest, cls, 0, td_DepartDate.InnerText.Trim().Replace("/", ""), "")
                        CommCHD1 = CommCHD
                    End If

                    TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD)
                    TotalComm1 = TotalComm
                    'End Calculate Commission


                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    dt = ST.calcServicecharge(txt_TktingAirline.Text, "I").Tables(0)
                    'Calculate Transaction Fee
                    If (dt.Rows.Count > 0) Then
                        lblTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblTF.Text = "0"
                    End If

                    If lblTF.Text <> "" AndAlso lblTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblTF.Text)
                    End If
                    Dim TFeePerADT As Double = ((AFare + AYQ) * TransFee) / 100
                    Dim TFeeADT As Double = Math.Round((TFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim TFeePerCHD As Double = ((CFare + CYQ) * TransFee) / 100
                    Dim TFeeCHD As Double = Math.Round((TFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim TFee As Double = TFeeADT + TFeeCHD
                    lbl_TransFee.Text = Convert.ToString(TFee)
                    'End Transaction Fee Calculation
                    If (dt.Rows.Count > 0) Then
                        lbl_STax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        lbl_STax.Text = "0"
                    End If

                    If lbl_STax.Text <> "" AndAlso lbl_STax.Text IsNot Nothing Then
                        SerTax = Convert.ToDouble(lbl_STax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        STaxPerADT = Math.Round((((CommADT - TFeePerADT) * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    Else
                        STaxPerADT = Math.Round(((CommADT * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    End If
                    'per child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            STaxPerCHD = Math.Round((((CommCHD - TFeePerCHD) * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        Else
                            STaxPerCHD = Math.Round(((CommCHD * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        End If
                    End If
                    STax = STaxADT + STaxCHD
                    TotalComm = TotalComm - STax
                    STax1 = STax
                    STax = 0
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    '''''''''''''''''''''''''''''''''''''''''End Service Tax On the basis of Commission''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If
                'Assign to output Parameters
                CHDComm = CommCHD - STaxPerCHD
                ADTComm = CommADT - STaxPerADT
            End If
            If (td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False) Then
                'Calculate Commission And No Cash Back
                'One Way Comm and CashBack
                Dim Origin As String = "", Dest As String = "", cls As String = re_rbd.Text.ToUpper
                Dim ADT As Double = Convert.ToDouble(td_Adult.InnerText)
                Dim CHD As Double = Convert.ToDouble(td_Child.InnerText)
                Dim GroupType As String = ""
                Dim dsAType As New DataSet()
                Dim dtAType As New DataTable()

                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                dtAType = dsAType.Tables(0)
                GroupType = dtAType.Rows(0)("agent_type").ToString()
                Origin = td_From.InnerText
                Dest = td_To.InnerText
                Dim STaxPerADT As Double = 0, STaxADT As Double = 0, STaxPerCHD As Double = 0, STaxCHD As Double = 0, STaxPerINF As Double = 0, STaxINF As Double = 0, STax As Double = 0
                Dim dtMgtFee As New DataTable
                If (IsCorp = True) Then
                    Dim dtmgtcommadt As New DataTable
                    Dim dtmgtcommchd As New DataTable
                    dtmgtcommadt = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), "I", Convert.ToDouble(txt_ATotal.Text)).Tables(0)
                    CommADT = Convert.ToDouble(dtmgtcommadt.Rows(0)("MGTFEE").ToString())
                    'MARKUP
                    dtgetmrk = ClsCorp.GetMarkUp(dtAType.Rows(0)("User_Id").ToString(), "SPRING", "I", "AD").Tables(0)
                    If (dtgetmrk.Rows.Count > 0) Then
                        Adminmrkadt = ClsCorp.CalcMarkup(dtgetmrk, txt_TktingAirline.Text.Trim, AFare, "I")
                        ViewState("Adminmrkadt") = Adminmrkadt
                    End If
                    'END MARKUP
                    Dim MgtFeeINF As Double = 0
                    If (CHD > 0) Then
                        dtmgtcommchd = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), "I", Convert.ToDouble(txt_Ctotal.Text)).Tables(0)
                        CommCHD = Convert.ToDouble(dtmgtcommchd.Rows(0)("MGTFEE").ToString())
                        'MARKUP
                        dtgetmrk = ClsCorp.GetMarkUp(dtAType.Rows(0)("User_Id").ToString(), "SPRING", "I", "AD").Tables(0)
                        If (dtgetmrk.Rows.Count > 0) Then
                            Adminmrkchd = ClsCorp.CalcMarkup(dtgetmrk, txt_TktingAirline.Text.Trim, CFare, "I")
                            ViewState("Adminmrkchd") = Adminmrkchd
                        End If
                        'END MARKUP
                    End If
                    If (td_Infrant.InnerText > 0) Then
                        dtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_TktingAirline.Text.Trim, Convert.ToDouble(IFare), Convert.ToDouble(IYQ), "I", Convert.ToDouble(txt_ITotal.Text)).Tables(0)
                        MgtFeeINF = Math.Round(Convert.ToDouble(dtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If
                    TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD) + MgtFeeINF
                    'End Calculate Commission
                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission One Way''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblTF.Text = "0"
                    TransFee = 0 'Convert.ToDouble(lblTF.Text)
                    lbl_TransFee.Text = 0 ' Convert.ToString(TFee)
                    lbl_STax.Text = "0"
                    lbl_mrkadmin.Text = Adminmrkadt * ADT + Adminmrkchd * CHD
                    SerTax = 0 ' Convert.ToDouble(lbl_STax.Text)
                    STaxPerADT = Math.Round(((Convert.ToDouble(dtmgtcommadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    'per child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        STaxPerCHD = Math.Round(((Convert.ToDouble(dtmgtcommchd.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If
                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        STaxPerINF = Math.Round(((Convert.ToDouble(dtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        STaxINF = Math.Round((STaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If
                    STax = STaxADT + STaxCHD + STaxINF
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    'End Calculation ServiceTax
                Else
                    CommADT = CCAP.calcComm(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(AFare), Convert.ToDouble(AYQ), Origin, Dest, cls, 0, td_DepartDate.InnerText.Trim().Replace("/", ""), "")
                    CommADT1 = CommADT
                    CommCHD = CCAP.calcComm(GroupType, txt_TktingAirline.Text.Trim.ToUpper, Convert.ToDouble(CFare), Convert.ToDouble(CYQ), Origin, Dest, cls, 0, td_DepartDate.InnerText.Trim().Replace("/", ""), "")
                    CommCHD1 = CommCHD
                    TotalComm = (Math.Round((CommADT), 0) * ADT) + (Math.Round((CommCHD), 0) * CHD)
                    TotalComm1 = TotalComm
                    'End Calculate Commission
                    '''''''''''''''''''''''''''''''''''''''''Calculate Service Tax On the basis of Commission One Way''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    dt = ST.calcServicecharge(txt_TktingAirline.Text, "I").Tables(0)
                    'Calculate Transaction Fee
                    If (dt.Rows.Count > 0) Then
                        lblTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblTF.Text = "0"
                    End If

                    If lblTF.Text <> "" AndAlso lblTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblTF.Text)
                    End If
                    Dim TFeePerADT As Double = ((AFare + AYQ) * TransFee) / 100
                    Dim TFeeADT As Double = Math.Round((TFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim TFeePerCHD As Double = ((CFare + CYQ) * TransFee) / 100
                    Dim TFeeCHD As Double = Math.Round((TFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim TFee As Double = TFeeADT + TFeeCHD
                    lbl_TransFee.Text = Convert.ToString(TFee)
                    'End Transaction Fee Calculation
                    If (dt.Rows.Count > 0) Then
                        lbl_STax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        lbl_STax.Text = "0"
                    End If

                    If lbl_STax.Text <> "" AndAlso lbl_STax.Text IsNot Nothing Then
                        SerTax = Convert.ToDouble(lbl_STax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                        STaxPerADT = Math.Round((((CommADT - TFeePerADT) * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    Else
                        STaxPerADT = Math.Round(((CommADT * SerTax) / 100), 0)
                        STaxADT = Math.Round((STaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        STaxPerADT1 = STaxPerADT
                        STaxADT1 = STaxADT
                    End If
                    'per child ServiceTax
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                            STaxPerCHD = Math.Round((((CommCHD - TFeePerCHD) * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        Else
                            STaxPerCHD = Math.Round(((CommCHD * SerTax) / 100), 0)
                            STaxCHD = Math.Round((STaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            STaxPerCHD1 = STaxPerCHD
                            STaxCHD1 = STaxCHD
                        End If
                    End If
                    STax = STaxADT + STaxCHD
                    TotalComm = TotalComm - STax
                    STax1 = STax
                    STax = 0
                    lbl_ServiceTax.Text = Convert.ToString(STax)
                    lbl_mrkadmin.Text = Adminmrkadt * Convert.ToDouble(td_Adult.InnerHtml) + Adminmrkchd * Convert.ToDouble(td_Child.InnerHtml)
                    'End Calculation ServiceTax
                End If
                '''''''''''''''''''''''''''''''''''''''''End Service Tax On the basis of Commission Round Trip''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Assign to output Parameters
                CHDComm = CommCHD - STaxPerCHD
                ADTComm = CommADT - STaxPerADT

                'Round Trip Comm and Cash back
                Dim ReGroupType As String = ""
                Origin = ""
                Dest = ""
                cls = "RBD"
                dsAType = STDom.GetAgencyDetails(td_AgentID.InnerText)
                dtAType = dsAType.Tables(0)
                ReGroupType = dtAType.Rows(0)("agent_type").ToString()
                Origin = td_To.InnerText
                Dest = td_From.InnerText
                Dim ReSTaxPerADT As Double = 0, ReSTaxADT As Double = 0, ReSTaxPerCHD As Double = 0, ReSTaxCHD As Double = 0, ReSTaxPerINF As Double = 0, ReSTaxINF As Double = 0, ReSTax As Double = 0

                Dim ReAdminmrkadt As Double = 0
                Dim ReAdminmrkchd As Double = 0
                Dim redtgetmrk As New DataTable
                Dim RedtMgtFee As New DataTable
                Dim ReMgtFeeINF As Double = 0
                If (IsCorp = True) Then
                    Dim redtmgtcommadt As New DataTable
                    Dim redtmgtcommchd As New DataTable
                    redtmgtcommadt = ClsCorp.GetManagementFeeSrvTax(ReGroupType, txt_ReTktingAirline.Text.Trim.ToUpper, Convert.ToDouble(ReAFare), Convert.ToDouble(ReAYQ), "I", Convert.ToDouble(txt_ReATotal.Text)).Tables(0)
                    ReCommADT = Convert.ToDouble(redtmgtcommadt.Rows(0)("MGTFEE").ToString())

                    'MARKUP ROUND
                    redtgetmrk = ClsCorp.GetMarkUp(dtAType.Rows(0)("User_Id").ToString(), "SPRING", "I", "AD").Tables(0)
                    If (redtgetmrk.Rows.Count > 0) Then
                        ReAdminmrkadt = ClsCorp.CalcMarkup(redtgetmrk, txt_ReTktingAirline.Text.Trim, ReAFare, "I")
                        ViewState("ReAdminmrkadt") = ReAdminmrkadt
                    End If
                    'END MARKUP ROUND


                    If (CHD > 0) Then
                        redtmgtcommchd = ClsCorp.GetManagementFeeSrvTax(ReGroupType, txt_ReTktingAirline.Text.Trim.ToUpper, Convert.ToDouble(ReCFare), Convert.ToDouble(ReCYQ), "I", Convert.ToDouble(txt_ReCtotal.Text)).Tables(0)
                        ReCommCHD = Convert.ToDouble(redtmgtcommchd.Rows(0)("MGTFEE").ToString())
                        'MARKUP ROUND
                        redtgetmrk = ClsCorp.GetMarkUp(dtAType.Rows(0)("User_Id").ToString(), "SPRING", "I", "AD").Tables(0)
                        If (redtgetmrk.Rows.Count > 0) Then
                            ReAdminmrkchd = ClsCorp.CalcMarkup(redtgetmrk, txt_ReTktingAirline.Text.Trim, ReCFare, "I")
                            ViewState("ReAdminmrkchd") = ReAdminmrkchd
                        End If
                        'END MARKUP ROUND
                    End If
                    If (td_Infrant.InnerText > 0) Then
                        RedtMgtFee = ClsCorp.GetManagementFeeSrvTax(GroupType, txt_ReTktingAirline.Text.Trim, Convert.ToDouble(ReIFare), Convert.ToDouble(ReIYQ), "D", Convert.ToDouble(txt_ReITotal.Text)).Tables(0)
                        ReMgtFeeINF = Math.Round(Convert.ToDouble(RedtMgtFee.Rows(0)("MGTFEE").ToString()), 0) * Convert.ToDouble(td_Infrant.InnerText)
                    End If
                    ReTotalComm = (Math.Round((ReCommADT), 0) * ADT) + (Math.Round((ReCommCHD), 0) * CHD) + ReMgtFeeINF
                    lblReTF.Text = "0"
                    TransFee = 0
                    lbl_ReTransFee.Text = 0
                    Lbl_ReSTax.Text = "0"
                    lbl_readminmrk.Text = ReAdminmrkadt * ADT + ReAdminmrkchd * CHD
                    ReSerTax = 0
                    ReSTaxPerADT = Math.Round(((Convert.ToDouble(redtmgtcommadt.Rows(0)("MGTSRVTAX").ToString()))), 0)
                    ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    'per child commission
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        ReSTaxPerCHD = Math.Round(((Math.Round(((Convert.ToDouble(redtmgtcommchd.Rows(0)("MGTSRVTAX").ToString()))), 0))), 0)
                        ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    End If
                    If (Convert.ToDouble(td_Infrant.InnerHtml) > 0) Then
                        ReSTaxPerINF = Math.Round(((Convert.ToDouble(RedtMgtFee.Rows(0)("MGTSRVTAX").ToString()))), 0)
                        ReSTaxINF = Math.Round((ReSTaxPerINF), 0) * Convert.ToDouble(td_Infrant.InnerHtml)
                    End If
                    ReSTax = ReSTaxADT + ReSTaxCHD + ReSTaxINF
                    lbl_ReServiceTax.Text = Convert.ToString(ReSTax)
                    'End Calculation ReServiceTax
                Else
                    ReCommADT = CCAP.calcComm(ReGroupType, txt_ReTktingAirline.Text.Trim.ToUpper, Convert.ToDouble(ReAFare), Convert.ToDouble(ReAYQ), Origin, Dest, cls, 0, td_RetDate.InnerText.Trim().Replace("/", ""), "")
                    ReCommADT1 = ReCommADT
                    ReCommCHD = CCAP.calcComm(ReGroupType, txt_ReTktingAirline.Text.Trim.ToUpper, Convert.ToDouble(ReCFare), Convert.ToDouble(ReCYQ), Origin, Dest, cls, 0, td_RetDate.InnerText.Trim().Replace("/", ""), "")
                    ReCommCHD1 = ReCommCHD
                    ReTotalComm = (Math.Round((ReCommADT), 0) * ADT) + (Math.Round((ReCommCHD), 0) * CHD)
                    ReTotalComm1 = ReTotalComm
                    'End Calculate Commission
                    'ReServiceTax for Round Trip
                    dt = ST.calcServicecharge(txt_ReTktingAirline.Text, "I").Tables(0)
                    If (dt.Rows.Count > 0) Then
                        lblReTF.Text = dt.Rows(0)("TranFee").ToString()
                    Else
                        lblReTF.Text = "0"
                    End If
                    If lblReTF.Text <> "" AndAlso lblReTF.Text IsNot Nothing Then
                        TransFee = Convert.ToDouble(lblReTF.Text)
                    End If
                    Dim ReTFeePerADT As Double = ((ReAFare + ReAYQ) * TransFee) / 100
                    Dim ReTFeeADT As Double = Math.Round((ReTFeePerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                    Dim ReTFeePerCHD As Double = ((ReCFare + ReCYQ) * TransFee) / 100
                    Dim ReTFeeCHD As Double = Math.Round((ReTFeePerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                    Dim ReTFee As Double = ReTFeeADT + ReTFeeCHD
                    lbl_ReTransFee.Text = Convert.ToString(ReTFee)

                    If (dt.Rows.Count > 0) Then
                        Lbl_ReSTax.Text = dt.Rows(0)("SrvTax").ToString()
                    Else
                        Lbl_ReSTax.Text = "0"
                    End If

                    If Lbl_ReSTax.Text <> "" AndAlso Lbl_ReSTax.Text IsNot Nothing Then
                        ReSerTax = Convert.ToDouble(Lbl_ReSTax.Text)
                    End If
                    'Per Adult ServiceTax
                    If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                        ReSTaxPerADT = Math.Round((((ReCommADT - ReTFeePerADT) * ReSerTax) / 100), 0)
                        ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        ReSTaxPerADT1 = ReSTaxPerADT
                        ReSTaxADT1 = ReSTaxADT
                    Else
                        ReSTaxPerADT = Math.Round(((ReCommADT * ReSerTax) / 100), 0)
                        ReSTaxADT = Math.Round((ReSTaxPerADT), 0) * Convert.ToDouble(td_Adult.InnerHtml)
                        ReSTaxPerADT1 = ReSTaxPerADT
                        ReSTaxADT1 = ReSTaxADT
                    End If
                    'per child commission
                    If (Convert.ToDouble(td_Child.InnerHtml) > 0) Then
                        If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                            ReSTaxPerCHD = Math.Round((((ReCommCHD - ReTFeePerCHD) * ReSerTax) / 100), 0)
                            ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            ReSTaxPerCHD1 = ReSTaxPerCHD
                            ReSTaxCHD1 = ReSTaxCHD
                        Else
                            ReSTaxPerCHD = Math.Round(((ReCommCHD * ReSerTax) / 100), 0)
                            ReSTaxCHD = Math.Round((ReSTaxPerCHD), 0) * Convert.ToDouble(td_Child.InnerHtml)
                            ReSTaxPerCHD1 = ReSTaxPerCHD
                            ReSTaxCHD1 = ReSTaxCHD
                        End If
                    End If
                    ReSTax = ReSTaxADT + ReSTaxCHD
                    ReTotalComm = ReTotalComm - ReSTax
                    ReSTax1 = ReSTax
                    ReSTax = 0
                    lbl_ReServiceTax.Text = Convert.ToString(ReSTax)
                    'End Calculation ReServiceTax
                End If
                ''''''''''''''''''''''''''Calculate For Round Trip'''''''''''''''''''''''''''''''''''''''''''''''
                'Assign to output Parameters
                ReCHDComm = ReCommCHD - ReSTaxPerCHD
                ReADTComm = ReCommADT - ReSTaxPerADT
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub Calc_TDS_Dom()
        Try
            If (td_TravelType.InnerHtml = "One Way" And PxCD = "D" OrElse SpecialRT = True) Then
                'Calculate(tds)
                Dim tdsper As String
                Dim TdsOn As Integer = 0
                Dim TDSPerADT As Double = 0, ADTTRFee As Double
                Dim TDSADT As Double = 0
                Dim TDSPerCHD As Double = 0, CHDTRFee As Double
                Dim TDSCHD As Double = 0

                Dim TdsOn1 As Integer = 0
                Dim TDSPerADT1 As Double = 0
                Dim TDSADT1 As Double = 0
                Dim TDSPerCHD1 As Double = 0
                Dim TDSCHD1 As Double = 0


                Calc_D_Comm(ADTTRFee, CHDTRFee) 'get Adult and Child Tran fee
                TFeePerADT = ADTTRFee
                TFeePerCHD = CHDTRFee

                If (IsCorp = True) Then
                    TDSPerADT = 0
                    TDSADT = 0
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = 0
                        TDSCHD = 0
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = 0
                    lbl_mgtfee.Text = TotalComm
                    lbl_mrkadmin.Text = 0
                    'End Tds Calculation
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    'End
                    lbl_TBC.Text = Val(lbl_mgtfee.Text) + (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text)), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                Else
                    tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    TDSPerADT = ((Convert.ToDouble(dtcommADT.Rows(0)("Dis").ToString) - TFeePerADT - STaxPerADT1) * Convert.ToDouble(tdsper)) / 100
                    TDSADT = Math.Round((TDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = ((Convert.ToDouble(dtcommCHD.Rows(0)("Dis").ToString) - TFeePerCHD - STaxPerCHD1) * Convert.ToDouble(tdsper)) / 100
                        TDSCHD = Math.Round((TDSPerCHD), 0) * td_Child.InnerText
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = TotalComm

                    'End Tds Calculation
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If

                    'End
                    lbl_TBC.Text = (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text)), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                End If
            End If
            If (td_TravelType.InnerHtml = "Round Trip" And PxCD = "D" And SpecialRT = False) Then
                'Calculate Tds for Both
                'Calculate tds one way
                Dim tdsper As String
                Dim TdsOn As Integer = 0
                Dim TDSPerADT As Double = 0, ADTTRFee As Double
                Dim TDSADT As Double = 0
                Dim TDSPerCHD As Double = 0, CHDTRFee As Double
                Dim TDSCHD As Double = 0
                Dim Retdsper As String
                Dim ReTdsOn As Integer
                Dim ReTDSPerADT As Double, ReADTTRFee As Double
                Dim ReTDSADT As Double
                Dim ReTDSPerCHD As Double, ReCHDTRFee As Double
                Dim ReTDSCHD As Double
                Calc_D_Comm(ADTTRFee, CHDTRFee) 'get Adult and Child Tran fee
                TFeePerADT = ADTTRFee
                TFeePerCHD = CHDTRFee

                If (IsCorp = True) Then
                    'Calculate TDS
                    TDSPerADT = 0 '((Convert.ToDouble(dtcommADT.Rows(0)("Dis").ToString) - TFeePerADT) * Convert.ToDouble(tdsper)) / 100
                    TDSADT = 0 'Math.Round((TDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = 0 '((Convert.ToDouble(dtcommCHD.Rows(0)("Dis").ToString) - TFeePerCHD) * Convert.ToDouble(tdsper)) / 100
                        TDSCHD = 0 'Math.Round((TDSPerCHD), 0) * td_Child.InnerText
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = 0
                    lbl_mgtfee.Text = TotalComm
                    lbl_mrkadmin.Text = 0

                    'End Tds Calculation
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    'End
                    lbl_TBC.Text = Val(lbl_mgtfee.Text) + (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text)), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                    'Calculate TDS for Round Trip
                    Calc_D_Comm(ADTTRFee, CHDTRFee, ReADTTRFee, ReCHDTRFee) 'get Adult and Child Tran fee
                    ReTFeePerADT = ReADTTRFee
                    ReTFeePerCHD = ReCHDTRFee
                    'Calcualte TDS
                    ReTDSPerADT = 0 '((Convert.ToDouble(RedtcommADT.Rows(0)("Dis").ToString) - ReTFeePerADT) * Convert.ToDouble(Retdsper)) / 100
                    ReTDSADT = 0 'Math.Round((ReTDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        ReTDSPerCHD = 0 ' ((Convert.ToDouble(RedtcommCHD.Rows(0)("Dis").ToString) - ReTFeePerCHD) * Convert.ToDouble(Retdsper)) / 100
                        ReTDSCHD = 0 ' Math.Round((ReTDSPerCHD), 0) * td_Child.InnerText
                    End If
                    ReTdsOn = ReTDSADT + ReTDSCHD
                    lbl_ReTDS.Text = ReTdsOn
                    lbl_ReTotalDiscount.Text = 0 ' ReTotalComm
                    lbl_remgtfee.Text = ReTotalComm
                    lbl_readminmrk.Text = 0
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                        ReSFDis = Convert.ToDouble(txt_ReSFDis.Text.Trim())
                    End If
                    'End
                    lbl_ReTBC.Text = Math.Round((Val(lbl_remgtfee.Text)), 0) + (Math.Round((ReadtTtl * Val(td_Adult.InnerText) + RechdTtl * Val(td_Child.InnerText) + ReinfTtl * Val(td_Infrant.InnerText) + Val(lbl_ReServiceTax.Text) + Val(lbl_ReTransFee.Text)), 0)).ToString
                    lbl_ReTBCAFTRD.Text = (Math.Round((Val(lbl_ReTBC.Text) + Val(lbl_ReTDS.Text)) - Val(lbl_ReTotalDiscount.Text) - Val(ReSFDis) - Val(ReTotalCB), 0)).ToString
                    'End Calculate Total
                Else
                    tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    'Calculate TDS
                    TDSPerADT = ((Convert.ToDouble(dtcommADT.Rows(0)("Dis").ToString) - TFeePerADT - STaxPerADT1) * Convert.ToDouble(tdsper)) / 100
                    TDSADT = Math.Round((TDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = ((Convert.ToDouble(dtcommCHD.Rows(0)("Dis").ToString) - TFeePerCHD - STaxPerCHD1) * Convert.ToDouble(tdsper)) / 100
                        TDSCHD = Math.Round((TDSPerCHD), 0) * td_Child.InnerText
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = TotalComm
                    'End Tds Calculation
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    'End

                    lbl_TBC.Text = (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text)), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                    'Calculate TDS for Round Trip
                    Calc_D_Comm(ADTTRFee, CHDTRFee, ReADTTRFee, ReCHDTRFee) 'get Adult and Child Tran fee
                    ReTFeePerADT = ReADTTRFee
                    ReTFeePerCHD = ReCHDTRFee
                    Retdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    'Calcualte TDS
                    ReTDSPerADT = ((Convert.ToDouble(RedtcommADT.Rows(0)("Dis").ToString) - ReTFeePerADT - ReSTaxPerADT1) * Convert.ToDouble(Retdsper)) / 100
                    ReTDSADT = Math.Round((ReTDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        ReTDSPerCHD = ((Convert.ToDouble(RedtcommCHD.Rows(0)("Dis").ToString) - ReTFeePerCHD - ReSTaxPerCHD1) * Convert.ToDouble(Retdsper)) / 100
                        ReTDSCHD = Math.Round((ReTDSPerCHD), 0) * td_Child.InnerText
                    End If
                    ReTdsOn = ReTDSADT + ReTDSCHD
                    lbl_ReTDS.Text = ReTdsOn
                    lbl_ReTotalDiscount.Text = ReTotalComm
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                        ReSFDis = Convert.ToDouble(txt_ReSFDis.Text.Trim())
                    End If
                    'End
                    lbl_ReTBC.Text = (Math.Round((ReadtTtl * Val(td_Adult.InnerText) + RechdTtl * Val(td_Child.InnerText) + ReinfTtl * Val(td_Infrant.InnerText) + Val(lbl_ReServiceTax.Text) + Val(lbl_ReTransFee.Text)), 0)).ToString
                    lbl_ReTBCAFTRD.Text = (Math.Round((Val(lbl_ReTBC.Text) + Val(lbl_ReTDS.Text)) - Val(lbl_ReTotalDiscount.Text) - Val(ReSFDis) - Val(ReTotalCB), 0)).ToString
                    'End Calculate Total
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub Calc_TDS_Intl()
        'TDS is Calculated based on Adult Comm and Adult Trans Fee((Comm-TransFee)*Tdsper)/100
        Try
            If (td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) Then
                'Calculate(tds) ONE WAY 
                Dim tdsper As String
                Dim TdsOn As Integer = 0 'Total TDS 
                Dim TDSPerADT As Double = 0, ADTComm As Double, ADTTRFee As Double
                Dim TDSADT As Double = 0
                Dim TDSPerCHD As Double = 0, CHDComm As Double, CHDTRFee As Double
                Dim TDSCHD As Double = 0
                Calc_I_Comm(ADTComm, CHDComm) 'get Adult and Child Commision
                'Calc_SvTax_TransFee_Intl(ADTTRFee, CHDTRFee) 'get Adult and Child Tran fee

                If (IsCorp = True) Then
                    lbl_TDS.Text = 0
                    lbl_TotalDiscount.Text = 0
                    lbl_mgtfee.Text = TotalComm
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    If txt_srvcharge.Text <> "" AndAlso txt_srvcharge.Text IsNot Nothing Then
                        SrvchargOneWay = Convert.ToDouble(txt_srvcharge.Text.Trim())
                    End If
                    'End
                    'Total Booking Cost(TBC) and TBC after Discount
                    lbl_TBC.Text = (Math.Round((Val(lbl_mgtfee.Text) + Val(lbl_mrkadmin.Text) + adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text) + (SrvchargOneWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total


                Else
                    Dim dttfee As New DataTable
                    Dim tfee As Double = 0
                    dttfee = ST.calcServicecharge(txt_TktingAirline.Text, "I").Tables(0)
                    'Calculate Transaction Fee
                    If dttfee.Rows.Count > 0 Then
                        tfee = Convert.ToDouble(dttfee.Rows(0)("TranFee").ToString())
                    End If
                    ADTTRFee = ((AFare + AYQ) * tfee) / 100
                    If (td_Child.InnerText > 0) Then
                        CHDTRFee = ((CFare + CYQ) * tfee) / 100
                    End If


                    'TFeePerADT = ADTTRFee
                    'TFeePerCHD = CHDTRFee
                    tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    'Calculate TDS
                    TDSPerADT = ((ADTComm - ADTTRFee) * Convert.ToDouble(tdsper)) / 100 'Adult TDS
                    TDSADT = Math.Round((TDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = ((CHDComm - CHDTRFee) * Convert.ToDouble(tdsper)) / 100 'Per Child TDS
                        TDSCHD = Math.Round((TDSPerCHD), 0) * td_Child.InnerText
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = TotalComm
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    If txt_srvcharge.Text <> "" AndAlso txt_srvcharge.Text IsNot Nothing Then
                        SrvchargOneWay = Convert.ToDouble(txt_srvcharge.Text.Trim())
                    End If
                    'End
                    'Total Booking Cost(TBC) and TBC after Discount
                    lbl_TBC.Text = (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text) + (SrvchargOneWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                End If



            End If
            If (td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False) Then 'Calcualte Both TDS
                'Calculate tds 1 Way
                Dim tdsper As String
                Dim TdsOn As Integer = 0 'Total TDS 
                Dim TDSPerADT As Double = 0, ADTComm As Double, ADTTRFee As Double
                Dim TDSADT As Double = 0
                Dim TDSPerCHD As Double = 0, CHDComm As Double, CHDTRFee As Double
                Dim TDSCHD As Double = 0

                Dim Retdsper As String
                Dim ReTdsOn As Integer
                Dim ReTDSPerADT As Double, ReADTComm As Double, ReADTTRFee As Double
                Dim ReTDSADT As Double
                Dim ReTDSPerCHD As Double, ReCHDComm As Double, ReCHDTRFee As Double
                Dim ReTDSCHD As Double

                Calc_I_Comm(ADTComm, CHDComm, 0, 0) 'get Adult and Child Commision
                'Calc_SvTax_TransFee_Intl(ADTTRFee, CHDTRFee) 'get Adult and Child Tran fee
                'TFeePerADT = ADTTRFee
                'TFeePerCHD = CHDTRFee
                If (IsCorp = True) Then
                    '=================CORP

                    lbl_TDS.Text = 0
                    lbl_TotalDiscount.Text = 0
                    lbl_mgtfee.Text = TotalComm
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    If txt_srvcharge.Text <> "" AndAlso txt_srvcharge.Text IsNot Nothing Then
                        SrvchargOneWay = Convert.ToDouble(txt_srvcharge.Text.Trim())
                    End If

                    'End
                    'Total Booking Cost(TBC) and TBC after Discount
                    lbl_TBC.Text = (Math.Round((Val(lbl_mgtfee.Text) + Val(lbl_mrkadmin.Text) + adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text) + (SrvchargOneWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                    'End Tds Calculation


                    'Calculate Tds Round Trip

                    Calc_I_Comm(ADTComm, CHDComm, ReADTComm, ReCHDComm) 'get Adult and Child Commision
                    'Calc_SvTax_TransFee_Intl(ADTTRFee, CHDTRFee, ReADTTRFee, ReCHDTRFee) 'get Adult and Child Tran fee
                    'ReTFeePerADT = ReADTTRFee
                    'ReTFeePerCHD = ReCHDTRFee

                    'Calcualte TDS
                    lbl_ReTDS.Text = 0
                    lbl_ReTotalDiscount.Text = 0
                    lbl_remgtfee.Text = ReTotalComm
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                        ReSFDis = Convert.ToDouble(txt_ReSFDis.Text.Trim())

                    End If
                    If txt_resrvcharge.Text <> "" AndAlso txt_resrvcharge.Text IsNot Nothing Then
                        SrvchargTwoWay = Convert.ToDouble(txt_resrvcharge.Text.Trim())
                    End If
                    'End
                    lbl_ReTBC.Text = (Math.Round((Val(lbl_remgtfee.Text) + Val(lbl_readminmrk.Text) + ReadtTtl * Val(td_Adult.InnerText) + RechdTtl * Val(td_Child.InnerText) + ReinfTtl * Val(td_Infrant.InnerText) + Val(lbl_ReServiceTax.Text) + Val(lbl_ReTransFee.Text) + (SrvchargTwoWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_ReTBCAFTRD.Text = (Math.Round((Val(lbl_ReTBC.Text) + Val(lbl_ReTDS.Text)) - Val(lbl_ReTotalDiscount.Text) - Val(ReSFDis) - Val(ReTotalCB), 0)).ToString
                    'End Calculate Total

                    '=================END CORP




                Else

                    Dim dttfee As New DataTable
                    Dim tfee As Double = 0
                    dttfee = ST.calcServicecharge(txt_TktingAirline.Text, "I").Tables(0)
                    'Calculate Transaction Fee
                    If dttfee.Rows.Count > 0 Then
                        tfee = Convert.ToDouble(dttfee.Rows(0)("TranFee").ToString())
                    End If
                    ADTTRFee = ((AFare + AYQ) * tfee) / 100
                    If (td_Child.InnerText > 0) Then
                        CHDTRFee = ((CFare + CYQ) * tfee) / 100
                    End If

                    'Calculate TDS
                    tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    TDSPerADT = ((ADTComm - ADTTRFee) * Convert.ToDouble(tdsper)) / 100 'Adult TDS
                    TDSADT = Math.Round((TDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        TDSPerCHD = ((CHDComm - CHDTRFee) * Convert.ToDouble(tdsper)) / 100 'Per Child TDS
                        TDSCHD = Math.Round((TDSPerCHD), 0) * td_Child.InnerText
                    End If
                    TdsOn = TDSADT + TDSCHD
                    lbl_TDS.Text = TdsOn
                    lbl_TotalDiscount.Text = TotalComm
                    'For Special Fare Discount
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFDis = Convert.ToDouble(txt_SFDis.Text.Trim())
                    End If
                    If txt_srvcharge.Text <> "" AndAlso txt_srvcharge.Text IsNot Nothing Then
                        SrvchargOneWay = Convert.ToDouble(txt_srvcharge.Text.Trim())
                    End If

                    'End
                    'Total Booking Cost(TBC) and TBC after Discount
                    lbl_TBC.Text = (Math.Round((adtTtl * Val(td_Adult.InnerText) + chdTtl * Val(td_Child.InnerText) + infTtl * Val(td_Infrant.InnerText) + Val(lbl_ServiceTax.Text) + Val(lbl_TransFee.Text) + (SrvchargOneWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_TBCAFTRD.Text = (Math.Round((Val(lbl_TBC.Text) + Val(lbl_TDS.Text)) - Val(lbl_TotalDiscount.Text) - Val(SFDis) - Val(TotalCB), 0)).ToString
                    'End Calculate Total
                    'End Tds Calculation


                    'Calculate Tds Round Trip

                    Calc_I_Comm(ADTComm, CHDComm, ReADTComm, ReCHDComm) 'get Adult and Child Commision
                    'Calc_SvTax_TransFee_Intl(ADTTRFee, CHDTRFee, ReADTTRFee, ReCHDTRFee) 'get Adult and Child Tran fee
                    'ReTFeePerADT = ReADTTRFee
                    'ReTFeePerCHD = ReCHDTRFee

                    Dim Redttfee As New DataTable
                    Dim Retfee As Double = 0
                    Redttfee = ST.calcServicecharge(txt_ReTktingAirline.Text, "I").Tables(0)
                    'Calculate Transaction Fee
                    If Redttfee.Rows.Count > 0 Then
                        Retfee = Convert.ToDouble(Redttfee.Rows(0)("TranFee").ToString())
                    End If
                    ReADTTRFee = ((ReAFare + ReAYQ) * Retfee) / 100
                    If (td_Child.InnerText > 0) Then
                        ReCHDTRFee = ((ReCFare + ReCYQ) * Retfee) / 100
                    End If

                    'Calcualte TDS
                    Retdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                    ReTDSPerADT = ((ReADTComm - ReADTTRFee) * Convert.ToDouble(Retdsper)) / 100 'Adult TDS
                    ReTDSADT = Math.Round((ReTDSPerADT), 0) * td_Adult.InnerText
                    If (td_Child.InnerText > 0) Then
                        ReTDSPerCHD = ((ReCHDComm - ReCHDTRFee) * Convert.ToDouble(Retdsper)) / 100 'Per Child TDS
                        ReTDSCHD = Math.Round((ReTDSPerCHD), 0) * td_Child.InnerText
                    End If
                    ReTdsOn = ReTDSADT + ReTDSCHD
                    lbl_ReTDS.Text = ReTdsOn
                    lbl_ReTotalDiscount.Text = ReTotalComm
                    'End Tds Calculation

                    'For Special Fare Discount
                    If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                        ReSFDis = Convert.ToDouble(txt_ReSFDis.Text.Trim())
                    End If
                    If txt_resrvcharge.Text <> "" AndAlso txt_resrvcharge.Text IsNot Nothing Then
                        SrvchargTwoWay = Convert.ToDouble(txt_resrvcharge.Text.Trim())
                    End If
                    'End
                    lbl_ReTBC.Text = (Math.Round((ReadtTtl * Val(td_Adult.InnerText) + RechdTtl * Val(td_Child.InnerText) + ReinfTtl * Val(td_Infrant.InnerText) + Val(lbl_ReServiceTax.Text) + Val(lbl_ReTransFee.Text) + (SrvchargTwoWay * Val(Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText)))), 0)).ToString
                    lbl_ReTBCAFTRD.Text = (Math.Round((Val(lbl_ReTBC.Text) + Val(lbl_ReTDS.Text)) - Val(lbl_ReTotalDiscount.Text) - Val(ReSFDis) - Val(ReTotalCB), 0)).ToString
                    'End Calculate Total
                End If


            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try
    End Sub
    Public Sub BindProxyDetail() 'Need to Change D or I value
        Try

            Dim ProxyID As String = Request.QueryString("ProxyID")
            Dim ds As New DataSet()
            'Retrived Records From 
            ds = STDom.ProxyDetails("", PxCD, ProxyID) 'P.ShowProxyByID(Convert.ToInt32(ProxyID))
            Dim dt As New DataTable()
            dt = ds.Tables(0)
            td_AgentID.InnerText = dt.Rows(0)("AgentID").ToString()
            td_BookingType.InnerText = dt.Rows(0)("BookingType").ToString()
            td_TravelType.InnerText = dt.Rows(0)("TravelType").ToString()
            td_From.InnerText = dt.Rows(0)("ProxyFrom").ToString()
            td_To.InnerText = dt.Rows(0)("ProxyTo").ToString()

            td_Sector.InnerText = td_From.InnerText + "-"c + td_To.InnerText
            td_DepartDate.InnerText = dt.Rows(0)("DepartDate").ToString()
            td_RetDate.InnerText = dt.Rows(0)("ReturnDate").ToString()
            td_Adult.InnerText = dt.Rows(0)("Adult").ToString()
            td_Child.InnerText = dt.Rows(0)("Child").ToString()
            td_Infrant.InnerText = dt.Rows(0)("Infrant").ToString()

            td_Class.InnerText = dt.Rows(0)("Class").ToString()
            td_Airline.InnerText = dt.Rows(0)("Airlines").ToString()
            td_Classes.InnerText = dt.Rows(0)("Classes").ToString()
            td_PMode.InnerText = dt.Rows(0)("PaymentMode").ToString()
            td_Remark.InnerText = dt.Rows(0)("Remark").ToString()

            lbl_Oneway.Text = td_From.InnerText + "-"c + td_To.InnerText
            lbl_onewaydate.Text = dt.Rows(0)("DepartDate").ToString()
            lbl_Return.Text = td_To.InnerText + "-"c + td_From.InnerText
            lbl_ReturnDate.Text = InlineAssignHelper(td_RetDate.InnerText, dt.Rows(0)("ReturnDate").ToString())
            lbl_AgencyName.Text = dt.Rows(0)("Ag_Name").ToString()

            If (SpecialRT = True) Then
                td_Sector.InnerText = td_From.InnerText + "-"c + td_To.InnerText + "-"c + td_From.InnerText
                td_SpecialDep.InnerText = dt.Rows(0)("ProxyTo").ToString()
                td_SpecialDest.InnerText = dt.Rows(0)("ProxyFrom").ToString()
                td_SpecialRetDate.InnerText = dt.Rows(0)("ReturnDate").ToString()
            End If

            ViewState("ProjectId") = If(IsDBNull(dt.Rows(0)("ProjectID")), Nothing, dt.Rows(0)("ProjectID").ToString())
            ViewState("BookedBy") = If(IsDBNull(dt.Rows(0)("BookedBy")), Nothing, dt.Rows(0)("BookedBy").ToString())

            If ViewState("ProjectId") Is Nothing Then
                spn_Projects1.Visible = False
                spn_Projects.Visible = False
                Span_BookedBy.Visible = False
                Span_BookedBy1.Visible = False
            Else

                spn_Projects1.Visible = True
                spn_Projects.Visible = True
                Span_BookedBy.Visible = True
                Span_BookedBy1.Visible = True
                spn_Projects1.InnerText = ViewState("ProjectId")
                Span_BookedBy1.InnerText = ViewState("BookedBy")
            End If



            Dim ds1 As New DataSet()
            ds1 = STDom.GetAgencyDetails(td_AgentID.InnerText)
            Dim dt1 As New DataTable()
            dt1 = ds1.Tables(0)
            td_AgentName.InnerText = dt1.Rows(0)("Name").ToString()
            td_AgentAddress.InnerText = dt1.Rows(0)("Address").ToString()
            td_Street.InnerText = dt1.Rows(0)("city").ToString() & "," & dt1.Rows(0)("State").ToString() & "," & dt1.Rows(0)("country").ToString() & "," & dt1.Rows(0)("zipcode").ToString()
            td_AgentMobNo.InnerText = dt1.Rows(0)("Mobile").ToString()
            td_Email.InnerText = dt1.Rows(0)("Email").ToString()
            lbl_CrdLimit.Text = dt1.Rows(0)("Crd_Limit").ToString()
            td_AgentType.InnerText = dt1.Rows(0)("Agent_Type").ToString()
            If (dt1.Rows(0)("IsCorp").ToString() <> "" AndAlso dt1.Rows(0)("IsCorp").ToString() IsNot Nothing) Then
                ViewState("IsCorp") = dt1.Rows(0)("IsCorp").ToString()
            Else
                ViewState("IsCorp") = False
            End If


            IsCorp = Convert.ToBoolean(ViewState("IsCorp").ToString())
            If (IsCorp = True) Then
                tr_mgtfee.Visible = True
                tr_remgtfee.Visible = True
            Else
                tr_mgtfee.Visible = False
                tr_remgtfee.Visible = False
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub BindAdult()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewAdult.DataSource = STDom.ProxyPaxDetails(Convert.ToInt32(ProxyID), "ADT")
            GridViewAdult.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindChild()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewChild.DataSource = STDom.ProxyPaxDetails(Convert.ToInt32(ProxyID), "CHD")
            GridViewChild.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub BindInfrant()
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            GridViewInfrant.DataSource = STDom.ProxyPaxDetails(Convert.ToInt32(ProxyID), "INF")
            GridViewInfrant.DataBind()
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub BindAdultDetail() 'For Showing record on Labels on PAge 
        Try
            Dim ProxyID As String = Request.QueryString("ProxyID")
            Dim ds As New DataSet()
            ds = STDom.ProxyPaxDetails(Convert.ToInt32(ProxyID), "ADT")
            Dim dt As New DataTable()
            dt = ds.Tables(0)
            lbl_FName.Text = dt.Rows(0)("FirstName").ToString()
            lbl_LName.Text = dt.Rows(0)("LastName").ToString()
            lbl_Title.Text = dt.Rows(0)("SirName").ToString()

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            btn_UpdateProxy.Visible = True
            btn_UpdateProxy.Enabled = True
            If (td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) Then 'Added Condition 'And PxCD = "D"

                pnl_onewaycal.Visible = True
                pnl_roundtripcal.Visible = False
                CalculateOneWay()
            End If

            If (td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False) Then 'Added Condition 'And PxCD = "D"

                pnl_onewaycal.Visible = True
                pnl_roundtripcal.Visible = True
                CalculateRoundTrip()


            End If

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    ' Calculates Taxes and ALL for Showing after Calculate Button
    Public Sub CalculateOneWay()
        Try
            Calc_BaseFare()   'Common For All Pax
            If (PxCD = "D") Then
                'Calc_SvTax_TranFee_Dom()
                'Calc_D_Comm()
                Calc_TDS_Dom()
            End If
            If (PxCD = "I") Then
                Calc_TDS_Intl()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
        End Try

    End Sub
    ' Calculates Taxes and ALL for Showing after Calculate Button
    Public Sub CalculateRoundTrip() 'This is same as One Way Fair Just it is Calcualted for Round trip Panel
        Try
            Calc_BaseFare()
            If (PxCD = "D") Then
                'Calc_SvTax_TranFee_Dom()
                'Calc_D_Comm()
                Calc_TDS_Dom()
            End If
            If (PxCD = "I") Then
                Calc_TDS_Intl()
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    '  For Inserting after Update Button
    Public Sub InsertOneWayDetails()
        Try
            Dim Status As Boolean = False
            trackIdOneWay = objSelectedfltCls.getRndNum
            'Dim projectID As String = If(ViewState("ProjectId") Is Nothing, Nothing, ViewState("ProjectId").Trim())

            Try
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                con.Open()
                Dim cmd As SqlCommand
                cmd = New SqlCommand("SP_CheckBookingByOrderId", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@OrderId", trackIdOneWay)
                cmd.Parameters.AddWithValue("@TableName", "FLTHEADER")
                Status = cmd.ExecuteScalar()
                con.Close()
            Catch ex As Exception

            End Try
            If (Session("OneStatus") = "OneNotActive") AndAlso Status = False Then
                Session("OneStatus") = "OneActive"


                Dim CORPBILLNO As String = Nothing
                Dim CheckBalStatusPXC As Boolean = False
                If (IsCorp = True) Then
                    CORPBILLNO = ClsCorp.GenerateBillNoCorp(PxCD).ToString()
                End If
                'Insert Proxy Charge into Ledger
                If txt_ProxyChargeOW.Text.Trim <> "0" AndAlso txt_ProxyChargeOW.Text.Trim <> "" AndAlso txt_ProxyChargeOW.Text IsNot Nothing Then
                    Dim A_BalPXC As Double
                    A_BalPXC = ST.UpdateCrdLimit(td_AgentID.InnerText, Convert.ToDouble(txt_ProxyChargeOW.Text.Trim))

                    'Check for available balance
                    If (A_BalPXC = 0) Then
                        Dim dtavPXC As New DataTable()
                        dtavPXC = STDom.GetAgencyDetails(td_AgentID.InnerText).Tables(0)
                        Dim CurrAvlBalPXC As Double
                        CurrAvlBalPXC = Convert.ToDouble(dtavPXC.Rows(0)("crd_limit").ToString)
                        If (A_BalPXC <> CurrAvlBalPXC) Then
                            CheckBalStatusPXC = True
                        End If
                    End If
                    'End Check for available balance
                    If (CheckBalStatusPXC = False) Then
                        If (PxCD = "D") Then
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdOneWay, txt_GDSPNR.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ProxyChargeOW.Text.Trim, 0, A_BalPXC, "ExtraProxyChargeDom", "Proxy Charge with OrderId: " & trackIdOneWay & " and Pnr:" & txt_GDSPNR.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        Else
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdOneWay, txt_GDSPNR.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ProxyChargeOW.Text.Trim, 0, A_BalPXC, "ExtraChargeIntl", "Proxy Charge with OrderId: " & trackIdOneWay & " and Pnr:" & txt_GDSPNR.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        End If
                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                        Dim ds_cur As New DataSet
                        adp = New SqlDataAdapter("UpdateProxyImportCharge", con)
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure
                        adp.SelectCommand.Parameters.AddWithValue("@ID", Request.QueryString("ProxyID"))
                        adp.SelectCommand.Parameters.AddWithValue("@Charge", txt_ProxyChargeOW.Text.Trim)
                        adp.SelectCommand.Parameters.AddWithValue("@Type", "PROXYOW")
                        adp.Fill(ds_cur)

                    End If
                End If



                Dim CheckBalStatus As Boolean = False
                Dim SF As Double = 0
                Dim Aval_Bal As Double
                If (CheckBalStatusPXC = False) Then
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SF = Convert.ToDouble(txt_SFDis.Text)
                    End If
                    'Update Credit Limit in NewRegs Table
                    Aval_Bal = ST.UpdateCrdLimit(td_AgentID.InnerText, lbl_TBCAFTRD.Text)
                End If
                'Check for available balance
                If (Aval_Bal = 0) Then
                    Dim dtavl As New DataTable()
                    dtavl = STDom.GetAgencyDetails(td_AgentID.InnerText).Tables(0)
                    Dim CurrAvlBal As Double
                    CurrAvlBal = Convert.ToDouble(dtavl.Rows(0)("Crd_Limit").ToString)
                    If (Aval_Bal <> CurrAvlBal) Then
                        CheckBalStatus = True
                    End If
                End If
                'End Check for available balance
                If (CheckBalStatus = False AndAlso CheckBalStatusPXC = False) Then
                    Try
                        Dim TotalBookingCost As Double
                        TotalBookingCost = Convert.ToDouble(lbl_TBC.Text)
                        'Insert Header Details -FltHeader Table
                        Dim TripType As String = ""
                        Dim Sector As String = ""
                        If (SpecialRT = True) Then
                            TripType = "R"
                            Sector = td_From.InnerText & ":" & td_To.InnerText & ":" & td_From.InnerText
                        Else
                            TripType = "O"
                            Sector = td_From.InnerText & ":" & td_To.InnerText

                        End If
                        If (PxCD = "D") Then
                            ST.insertHeaderDetailsPnrImport(trackIdOneWay, Sector, "Ticketed", txt_GDSPNR.Text.Trim, txt_AirlinePNR.Text.Trim, txt_TktingAirline.Text.Trim, TripType, "D", TotalBookingCost, lbl_TBCAFTRD.Text, "0", td_Adult.InnerText, td_Child.InnerText, td_Infrant.InnerText, td_AgentID.InnerText, lbl_AgencyName.Text, "SPRING", Session("UID").ToString(), "CL", lbl_Title.Text, lbl_FName.Text, lbl_LName.Text, 0, SF, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        ElseIf (PxCD = "I") Then
                            ST.insertHeaderDetailsPnrImport(trackIdOneWay, Sector, "Ticketed", txt_GDSPNR.Text.Trim, txt_AirlinePNR.Text.Trim, txt_TktingAirline.Text.Trim, TripType, "I", TotalBookingCost, lbl_TBCAFTRD.Text, "0", td_Adult.InnerText, td_Child.InnerText, td_Infrant.InnerText, td_AgentID.InnerText, lbl_AgencyName.Text, "SPRING", Session("UID").ToString(), "CL", lbl_Title.Text, lbl_FName.Text, lbl_LName.Text, 0, SF, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        End If

                        'Insert Transaction Details for Both - Trans_Report table
                        ST.InsertTransReportPnrImport(td_AgentID.InnerText, txt_GDSPNR.Text.Trim, "Ticketed", Aval_Bal, TotalBookingCost, td_From.InnerText & ":" & td_To.InnerText, "Created By Proxy with OrderId: " & trackIdOneWay & "Pnr:" & txt_GDSPNR.Text.Trim, lbl_TBCAFTRD.Text, lbl_AgencyName.Text)

                        'Insert Fare Details


                        If (PxCD = "D") Then
                            If (td_Adult.InnerText > 0) Then
                                CalFareDetails(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "ADT", txt_ABaseFare.Text.Trim, txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AOT.Text.Trim, "ONE")
                            End If
                            If (td_Child.InnerText > 0) Then
                                CalFareDetails(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "CHD", txt_CBaseFare.Text.Trim, txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_COT.Text.Trim, "ONE")
                            End If
                            If (td_Infrant.InnerText > 0) Then
                                CalFareDetails(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IOT.Text.Trim, "ONE")
                            End If
                        ElseIf (PxCD = "I") Then
                            If txt_srvcharge.Text <> "" AndAlso txt_srvcharge.Text IsNot Nothing Then
                                SrvchargOneWay = Convert.ToDouble(txt_srvcharge.Text.Trim())
                            End If
                            If (IsCorp = True) Then


                                If (td_Adult.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "ADT", Convert.ToDouble(txt_ABaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkadt")), txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AOT.Text.Trim, SrvchargOneWay + Convert.ToDouble(ViewState("Adminmrkadt")), "ONE")
                                End If
                                If (td_Child.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "CHD", Convert.ToDouble(txt_CBaseFare.Text.Trim) + Convert.ToDouble(ViewState("Adminmrkchd")), txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_COT.Text.Trim, SrvchargOneWay + Convert.ToDouble(ViewState("Adminmrkchd")), "ONE")
                                End If
                                If (td_Infrant.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IOT.Text.Trim, SrvchargOneWay, "ONE")
                                End If
                            Else
                                If (td_Adult.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "ADT", txt_ABaseFare.Text.Trim, txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AOT.Text.Trim, SrvchargOneWay, "ONE")
                                End If
                                If (td_Child.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "CHD", txt_CBaseFare.Text.Trim, txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_COT.Text.Trim, SrvchargOneWay, "ONE")
                                End If
                                If (td_Infrant.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IOT.Text.Trim, SrvchargOneWay, "ONE")
                                End If
                            End If
                            'If (td_Adult.InnerText > 0) Then
                            '    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "ADT", txt_ABaseFare.Text.Trim, txt_AYQ.Text, txt_AYR.Text.Trim, txt_AWO.Text.Trim, txt_AOT.Text.Trim, SrvchargOneWay, "ONE")
                            'End If
                            'If (td_Child.InnerText > 0) Then
                            '    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "CHD", txt_CBaseFare.Text.Trim, txt_CYQ.Text, txt_CYR.Text.Trim, txt_CWO.Text.Trim, txt_COT.Text.Trim, SrvchargOneWay, "ONE")
                            'End If
                            'If (td_Infrant.InnerText > 0) Then
                            '    CalFareDetails_Intl(trackIdOneWay, txt_TktingAirline.Text.Trim.ToUpper, "INF", txt_IBaseFare.Text.Trim, txt_IYQ.Text, txt_IYR.Text.Trim, txt_IWO.Text.Trim, txt_IOT.Text.Trim, SrvchargOneWay, "ONE")
                            'End If

                        End If
                        'End Fare Details

                        'Insert Flight Details same for Both- FLTDetails  table
                        Dim dsAirNameDepart As New DataSet
                        Dim DtAirNameDepart As New DataTable
                        Dim dsAirNameDest As New DataSet
                        Dim DtAirNameDest As New DataTable
                        Dim dsAirName As New DataSet
                        Dim DtAirName As New DataTable
                        'dsAirNameDepart = ST.GetCityNameByCode(td_From.InnerText)
                        'DtAirNameDepart = dsAirNameDepart.Tables(0)
                        'dsAirNameDest = ST.GetCityNameByCode(td_To.InnerText)
                        'DtAirNameDest = dsAirNameDest.Tables(0)
                        'dsAirName = ST.GetAirlineNameByCode(txt_TktingAirline.Text.Trim)
                        'DtAirName = dsAirName.Tables(0)




                        Dim AirlineNameDepart As String = ""
                        Dim AirNameDest As String = ""
                        Dim AirlineName As String = ""
                        dsAirNameDepart = ST.GetCityNameByCode(td_From.InnerText)
                        DtAirNameDepart = dsAirNameDepart.Tables(0)
                        If DtAirNameDepart.Rows.Count > 0 Then
                            AirlineNameDepart = DtAirNameDepart.Rows(0)("city").ToString.Trim
                        End If

                        dsAirNameDest = ST.GetCityNameByCode(td_To.InnerText)
                        DtAirNameDest = dsAirNameDest.Tables(0)

                        If DtAirNameDest.Rows.Count > 0 Then
                            AirNameDest = DtAirNameDest.Rows(0)("city").ToString.Trim
                        End If

                        dsAirName = ST.GetAirlineNameByCode(txt_TktingAirline.Text.Trim)
                        DtAirName = dsAirName.Tables(0)
                        If DtAirName.Rows.Count > 0 Then
                            AirlineName = DtAirName.Rows(0)("AL_Name").ToString.Trim
                        End If
                        Dim departDate As String
                        Dim arrivalDate As String
                        departDate = td_DepartDate.InnerText.Trim().Replace("/", "")
                        arrivalDate = txt_ArivalDate.Text.Replace("/", "").Trim

                        ST.insertFlightDetailsPnrImport(trackIdOneWay, td_From.InnerText, AirlineNameDepart, td_To.InnerText, AirNameDest, departDate, txt_DeptTime.Text.Trim, arrivalDate, txt_ArivalTime.Text.Trim, txt_TktingAirline.Text.Trim.ToUpper, AirlineName, txt_Flight.Text.Trim, "", "", "", "", "", "", "")
                        If (SpecialRT = True) Then
                            Dim SpecialRetDate As String
                            Dim SpecialarrDate As String
                            SpecialRetDate = td_RetDate.InnerText.Trim().Replace("/", "")
                            SpecialarrDate = txt_SpecialArrDate.Text.Replace("/", "").Trim
                            ST.insertFlightDetailsPnrImport(trackIdOneWay, td_To.InnerText, AirNameDest, td_From.InnerText, AirlineNameDepart, SpecialRetDate, txt_SpecialRetTime.Text.Trim, SpecialarrDate, txt_SpecialArrTime.Text.Trim, txt_TktingAirline.Text.Trim.ToUpper, AirlineName, txt_SpecialFlight.Text.Trim, "", "", "", "", "", "", "")
                        End If
                        'End Insert Filght Deatils

                        'Ledger Same For Both 
                        Dim DebitADT As Double = 0, CreditADT As Double = 0, DebitCHD As Double = 0, CreditCHD As Double = 0, DebitINF As Double = 0, CreditINF As Double = 0
                        Dim DtFltFare As New DataTable
                        DtFltFare = ST.GetFltFareDtl(trackIdOneWay).Tables(0)
                        Dim DtFltHeaderADT As New DataTable
                        DtFltHeaderADT = ST.GetFltHeaderDetail(trackIdOneWay).Tables(0)
                        Dim AvalBalance As Double = Convert.ToDouble(DtFltHeaderADT.Rows(0)("TotalAfterDis")) + Aval_Bal
                        Dim IP As String = Request.UserHostAddress
                        'LedgerEnd

                        Dim tkt As Integer = 0
                        For Each row As GridViewRow In GridViewAdult.Rows
                            DebitADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalAfterDis").ToString())
                            CreditADT = Convert.ToDouble(DtFltFare.Rows(0)("TotalDiscount").ToString())
                            AvalBalance = AvalBalance - DebitADT
                            Dim lbltittle As Label = DirectCast(row.FindControl("lbl_SirName"), Label)
                            Dim lblfirstname As Label = DirectCast(row.FindControl("lbl_FirstName"), Label)
                            Dim lbllastname As Label = DirectCast(row.FindControl("lbl_LastName"), Label)
                            'Dim txtticket As TextBox = DirectCast(row.FindControl("txt_Ticket"), TextBox)
                            Dim txtticket As String = ""
                            If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                                tkt += 1
                                txtticket = DirectCast(row.FindControl("txt_Ticket"), TextBox).Text.Trim & (tkt).ToString
                            Else
                                txtticket = DirectCast(row.FindControl("txt_Ticket"), TextBox).Text.Trim
                            End If


                            STDom.InsertProxyPaxInfoIntl(trackIdOneWay, lbltittle.Text, lblfirstname.Text, "", lbllastname.Text, "ADT", txtticket, "D")
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdOneWay, txt_GDSPNR.Text.Trim, txtticket, txt_TktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, DebitADT, 0, AvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                        Next
                        If (td_Child.InnerText > 0) Then
                            DebitCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
                            CreditCHD = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
                            For Each rowchd As GridViewRow In GridViewChild.Rows

                                AvalBalance = AvalBalance - DebitCHD
                                Dim lblCtittle As Label = DirectCast(rowchd.FindControl("lbl_CSirName"), Label)
                                Dim lblCfirstname As Label = DirectCast(rowchd.FindControl("lbl_CFirstName"), Label)
                                Dim lblClastname As Label = DirectCast(rowchd.FindControl("lbl_CLastName"), Label)
                                'Dim txtCticket As TextBox = DirectCast(rowchd.FindControl("txt_CTktNo"), TextBox)

                                Dim txtCticket As String = ""
                                If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                                    tkt += 1
                                    txtCticket = DirectCast(rowchd.FindControl("txt_CTktNo"), TextBox).Text.Trim & (tkt).ToString
                                Else
                                    txtCticket = DirectCast(rowchd.FindControl("txt_CTktNo"), TextBox).Text.Trim
                                End If

                                'Same for Both D and I
                                STDom.InsertProxyPaxInfoIntl(trackIdOneWay, lblCtittle.Text, lblCfirstname.Text, "", lblClastname.Text, "CHD", txtCticket, "D")
                                STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdOneWay, txt_GDSPNR.Text.Trim, txtCticket, txt_TktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, DebitCHD, 0, AvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                            Next
                        End If
                        If (td_Infrant.InnerText > 0) Then
                            If (DtFltFare.Rows(1)("PaxType").ToString() = "INF") Then
                                DebitINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalAfterDis").ToString())
                                CreditINF = Convert.ToDouble(DtFltFare.Rows(1)("TotalDiscount").ToString())
                            Else
                                DebitINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalAfterDis").ToString())
                                CreditINF = Convert.ToDouble(DtFltFare.Rows(2)("TotalDiscount").ToString())
                            End If

                            For Each rowinf As GridViewRow In GridViewInfrant.Rows
                                AvalBalance = AvalBalance - DebitINF
                                Dim lblItittle As Label = DirectCast(rowinf.FindControl("lbl_ISirName"), Label)
                                Dim lblIfirstname As Label = DirectCast(rowinf.FindControl("lbl_IFirstName"), Label)
                                Dim lblIlastname As Label = DirectCast(rowinf.FindControl("lbl_ILastName"), Label)
                                'Dim txtIticket As TextBox = DirectCast(rowinf.FindControl("txt_ITktNo"), TextBox)
                                Dim txtIticket As String = ""
                                If txt_TktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_TktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_TktingAirline.Text.Trim.ToUpper = "G8" Then
                                    tkt += 1
                                    txtIticket = DirectCast(rowinf.FindControl("txt_ITktNo"), TextBox).Text.Trim & (tkt).ToString
                                Else
                                    txtIticket = DirectCast(rowinf.FindControl("txt_ITktNo"), TextBox).Text.Trim
                                End If


                                STDom.InsertProxyPaxInfoIntl(trackIdOneWay, lblItittle.Text, lblIfirstname.Text, "", lblIlastname.Text, "INF", txtIticket, "D")
                                STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdOneWay, txt_GDSPNR.Text.Trim, txtIticket, txt_TktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, DebitINF, 0, AvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), CORPBILLNO)
                            Next
                        End If
                        'Update Proxy
                        If ((td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) And PxCD = "I") Then

                            STDom.UpdateProxyDate("Ticketed", Request.QueryString("ProxyID"), SrvchargOneWay, 0, trackIdOneWay, "", rbd.Text.Trim, "")
                        End If

                        If ((td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True) And PxCD = "D") Then
                            STDom.UpdateProxyDate("Ticketed", Request.QueryString("ProxyID"), 0, 0, trackIdOneWay, "", "", "")
                        End If

                        'NAV METHOD CALL START
                        Try

                            'Dim objNav As New AirService.clsConnection(trackIdOneWay, "0", "0")
                            'objNav.airBookingNav(trackIdOneWay, "", 0)

                        Catch ex As Exception

                        End Try
                        'Nav METHOD END'

                        'Yatra Billing
                        'Online
                        Try
                            'Dim AirObj As New AIR_YATRA
                            'AirObj.ProcessYatra_Air(trackIdOneWay, txt_GDSPNR.Text.Trim, "B")
                        Catch ex As Exception

                        End Try
                        'online end
                        'offline
                        'Try
                        '    STYTR.InsertYatra_MIRHEADER(trackIdOneWay, txt_GDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_PAX(trackIdOneWay, txt_GDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_SEGMENT(trackIdOneWay, txt_GDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_FARE(trackIdOneWay, txt_GDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_DIFTLINES(trackIdOneWay, txt_GDSPNR.Text.Trim)
                        'Catch ex As Exception

                        'End Try
                        'ofline end
                        'yatra billing end
                        Try
                            Dim smsStatus As String = ""
                            Dim smsMsg As String = ""
                            Dim objSMSAPI As New SMSAPI.SMS
                            Dim SmsCrd As DataTable
                            Dim objDA As New SqlTransaction
                            SmsCrd = objDA.SmsCredential(SMS.PROXY.ToString()).Tables(0)
                            If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                smsStatus = objSMSAPI.sendSms(trackIdOneWay, td_AgentMobNo.InnerText, td_From.InnerText & ":" & td_To.InnerText, txt_TktingAirline.Text.Trim, "", td_DepartDate.InnerText, txt_GDSPNR.Text, smsMsg, SmsCrd)
                                objSql.SmsLogDetails(trackIdOneWay, td_AgentMobNo.InnerText, smsMsg, smsStatus)
                            End If

                        Catch ex As Exception

                        End Try
                    Catch ex As Exception
                        clsErrorLog.LogInfo(ex)
                    End Try
                Else
                    'Message For AvaL_Bal 
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to update proxy.Please try after some time.');", True)
                End If

            Else
                If (SpecialRT = True) Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Special round trip proxy already updated.');", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('One way proxy already updated.');", True)
                End If

            End If



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try

    End Sub
    Public Sub InsertRoundTripDetails()
        Try
            Dim ReStatus As Boolean = False
            trackIdRoundTrip = objSelectedfltCls.getRndNum
            ' Dim projectID As String = If(ViewState("ProjectId") Is Nothing, Nothing, ViewState("ProjectId").Trim())



            Try
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                con.Open()
                Dim cmd As SqlCommand
                cmd = New SqlCommand("SP_CheckBookingByOrderId", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@OrderId", trackIdRoundTrip)
                cmd.Parameters.AddWithValue("@TableName", "FLTHEADER")
                ReStatus = cmd.ExecuteScalar()
                con.Close()
            Catch ex As Exception

            End Try
            If (Session("RoundStatus") = "RoundNotActive") AndAlso ReStatus = False Then
                Session("RoundStatus") = "RoundActive"

                Dim RECORPBILLNO As String = Nothing
                Dim ReCheckBalStatus As Boolean = False
                Dim ReCheckBalStatusPXC As Boolean = False
                Dim A_ReBalPXC As Double
                If (IsCorp = True) Then
                    RECORPBILLNO = ClsCorp.GenerateBillNoCorp(PxCD).ToString()
                End If
                If txt_ProxyChargeRT.Text.Trim <> "0" AndAlso txt_ProxyChargeRT.Text.Trim <> "" AndAlso txt_ProxyChargeRT.Text IsNot Nothing Then
                    A_ReBalPXC = ST.UpdateCrdLimit(td_AgentID.InnerText, Convert.ToDouble(txt_ProxyChargeRT.Text.Trim))
                    'Check for available balance
                    If (A_ReBalPXC = 0) Then
                        Dim redtavPXC As New DataTable()
                        redtavPXC = STDom.GetAgencyDetails(td_AgentID.InnerText).Tables(0)
                        Dim ReCurrAvlBalPXC As Double
                        ReCurrAvlBalPXC = Convert.ToDouble(redtavPXC.Rows(0)("Crd_Limit").ToString)
                        If (A_ReBalPXC <> ReCurrAvlBalPXC) Then
                            ReCheckBalStatusPXC = True
                        End If
                    End If
                    'End Check for available balance
                    If (ReCheckBalStatusPXC = False) Then
                        If (PxCD = "D") Then
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ProxyChargeRT.Text.Trim, 0, A_ReBalPXC, "ExtraProxyChargeDom", "Proxy Charge with OrderId: " & trackIdRoundTrip & " and Pnr:" & txt_ReGDSPNR.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                        Else
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, "", "", "", "", Session("UID").ToString, Request.UserHostAddress, txt_ProxyChargeRT.Text.Trim, 0, A_ReBalPXC, "ExtraProxyChargeIntl", "Proxy Charge with OrderId: " & trackIdRoundTrip & " and Pnr:" & txt_ReGDSPNR.Text.Trim, 0, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                        End If

                        If con.State = ConnectionState.Open Then
                            con.Close()
                        End If
                        con.ConnectionString = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
                        Dim ds_cur As New DataSet
                        adp = New SqlDataAdapter("UpdateProxyImportCharge", con)
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure
                        adp.SelectCommand.Parameters.AddWithValue("@ID", Request.QueryString("ProxyID"))
                        adp.SelectCommand.Parameters.AddWithValue("@Charge", txt_ProxyChargeRT.Text.Trim)
                        adp.SelectCommand.Parameters.AddWithValue("@Type", "PROXYRT")
                        adp.Fill(ds_cur)
                    End If
                End If




                Dim ReAval_Bal As Double
                'Update Credit Limit
                ReAval_Bal = ST.UpdateCrdLimit(td_AgentID.InnerText, lbl_ReTBCAFTRD.Text)

                'Check for available balance
                If (ReAval_Bal = 0) Then
                    Dim redtavl As New DataTable()
                    redtavl = STDom.GetAgencyDetails(td_AgentID.InnerText).Tables(0)
                    Dim ReCurrAvlBal As Double
                    ReCurrAvlBal = Convert.ToDouble(redtavl.Rows(0)("Crd_Limit").ToString)
                    If (ReAval_Bal <> ReCurrAvlBal) Then
                        ReCheckBalStatus = True
                    End If
                End If
                'End Check for available balance

                If (ReCheckBalStatus = False AndAlso ReCheckBalStatusPXC = False) Then
                    Try
                        Dim ReTotalBookingCost As Double
                        ReTotalBookingCost = Convert.ToDouble(lbl_ReTBC.Text)
                        Dim ReSF As Double = 0
                        If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                            ReSF = Convert.ToDouble(txt_ReSFDis.Text)
                        End If
                        'Insert Header Details
                        If (PxCD = "D") Then
                            ST.insertHeaderDetailsPnrImport(trackIdRoundTrip, td_To.InnerText & ":" & td_From.InnerText, "Ticketed", txt_ReGDSPNR.Text.Trim, txt_ReAirlinePNR.Text.Trim, txt_ReTktingAirline.Text.Trim, "O", "D", ReTotalBookingCost, lbl_ReTBCAFTRD.Text, "0", td_Adult.InnerText, td_Child.InnerText, td_Infrant.InnerText, td_AgentID.InnerText, lbl_AgencyName.Text, "SPRING", Session("UID").ToString(), "CL", lbl_Title.Text, lbl_FName.Text, lbl_LName.Text, 0, ReSF, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                        ElseIf (PxCD = "I") Then
                            ST.insertHeaderDetailsPnrImport(trackIdRoundTrip, td_To.InnerText & ":" & td_From.InnerText, "Ticketed", txt_ReGDSPNR.Text.Trim, txt_ReAirlinePNR.Text.Trim, txt_ReTktingAirline.Text.Trim, "O", "I", ReTotalBookingCost, lbl_ReTBCAFTRD.Text, "0", td_Adult.InnerText, td_Child.InnerText, td_Infrant.InnerText, td_AgentID.InnerText, lbl_AgencyName.Text, "SPRING", Session("UID").ToString(), "CL", lbl_Title.Text, lbl_FName.Text, lbl_LName.Text, 0, ReSF, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                        End If

                        'Insert Transaction Details Same for Both
                        ST.InsertTransReportPnrImport(td_AgentID.InnerText, txt_ReGDSPNR.Text.Trim, "Ticketed", ReAval_Bal, ReTotalBookingCost, td_To.InnerText & ":" & td_From.InnerText, "Created By Proxy with OrderId: " & trackIdRoundTrip & "Pnr:" & txt_ReGDSPNR.Text.Trim, lbl_ReTBCAFTRD.Text, lbl_AgencyName.Text)
                        'Insert FltFareDetails Details in -FltFareDetails Table for each Pax
                        If (PxCD = "D") Then
                            If (td_Adult.InnerText > 0) Then
                                CalFareDetails(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "ADT", txt_ReABaseFare.Text.Trim, txt_ReAYQ.Text, txt_ReAYR.Text.Trim, txt_ReAWO.Text.Trim, txt_ReAOT.Text.Trim, "ROUND")
                            End If
                            If (td_Child.InnerText > 0) Then
                                CalFareDetails(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "CHD", txt_ReCBaseFare.Text.Trim, txt_ReCYQ.Text, txt_ReCYR.Text.Trim, txt_ReCWO.Text.Trim, txt_ReCOT.Text.Trim, "ROUND")
                            End If
                            If (td_Infrant.InnerText > 0) Then
                                CalFareDetails(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "INF", txt_ReIBaseFare.Text.Trim, txt_ReIYQ.Text, txt_ReIYR.Text.Trim, txt_ReIWO.Text.Trim, txt_ReIOT.Text.Trim, "ROUND")
                            End If
                        End If
                        If (PxCD = "I") Then
                            If txt_resrvcharge.Text <> "" AndAlso txt_resrvcharge.Text IsNot Nothing Then
                                SrvchargTwoWay = Convert.ToDouble(txt_resrvcharge.Text.Trim())
                            End If
                            If (IsCorp = True) Then
                                If (td_Adult.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "ADT", Convert.ToDouble(txt_ReABaseFare.Text.Trim) + Convert.ToDouble(ViewState("ReAdminmrkadt")), txt_ReAYQ.Text, txt_ReAYR.Text.Trim, txt_ReAWO.Text.Trim, txt_ReAOT.Text.Trim, SrvchargTwoWay + Convert.ToDouble(ViewState("ReAdminmrkadt")), "ROUND")
                                End If
                                If (td_Child.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "CHD", Convert.ToDouble(txt_ReCBaseFare.Text.Trim) + Convert.ToDouble(ViewState("ReAdminmrkchd")), txt_ReCYQ.Text, txt_ReCYR.Text.Trim, txt_ReCWO.Text.Trim, txt_ReCOT.Text.Trim, SrvchargTwoWay + Convert.ToDouble(ViewState("ReAdminmrkchd")), "ROUND")
                                End If
                                If (td_Infrant.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "INF", txt_ReIBaseFare.Text.Trim, txt_ReIYQ.Text, txt_ReIYR.Text.Trim, txt_ReIWO.Text.Trim, txt_ReIOT.Text.Trim, SrvchargTwoWay, "ROUND")
                                End If
                            Else
                                If (td_Adult.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "ADT", txt_ReABaseFare.Text.Trim, txt_ReAYQ.Text, txt_ReAYR.Text.Trim, txt_ReAWO.Text.Trim, txt_ReAOT.Text.Trim, SrvchargTwoWay, "ROUND")
                                End If
                                If (td_Child.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "CHD", txt_ReCBaseFare.Text.Trim, txt_ReCYQ.Text, txt_ReCYR.Text.Trim, txt_ReCWO.Text.Trim, txt_ReCOT.Text.Trim, SrvchargTwoWay, "ROUND")
                                End If
                                If (td_Infrant.InnerText > 0) Then
                                    CalFareDetails_Intl(trackIdRoundTrip, txt_ReTktingAirline.Text.Trim, "INF", txt_ReIBaseFare.Text.Trim, txt_ReIYQ.Text, txt_ReIYR.Text.Trim, txt_ReIWO.Text.Trim, txt_ReIOT.Text.Trim, SrvchargTwoWay, "ROUND")
                                End If

                            End If

                        End If

                        'Insert Flight Details same for Both
                        Dim dsAirNameDepart As New DataSet
                        Dim DtAirNameDepart As New DataTable

                        Dim dsAirNameDest As New DataSet
                        Dim DtAirNameDest As New DataTable
                        Dim dsAirName As New DataSet
                        Dim DtAirName As New DataTable
                        Dim AirlineNameDepart As String = ""
                        Dim AirNameDest As String = ""
                        Dim AirlineName As String = ""
                        dsAirNameDepart = ST.GetCityNameByCode(td_To.InnerText)
                        DtAirNameDepart = dsAirNameDepart.Tables(0)
                        If DtAirNameDepart.Rows.Count > 0 Then
                            AirlineNameDepart = DtAirNameDepart.Rows(0)("city").ToString.Trim
                        End If

                        dsAirNameDest = ST.GetCityNameByCode(td_From.InnerText)
                        DtAirNameDest = dsAirNameDest.Tables(0)

                        If DtAirNameDest.Rows.Count > 0 Then
                            AirNameDest = DtAirNameDest.Rows(0)("city").ToString.Trim
                        End If

                        dsAirName = ST.GetAirlineNameByCode(txt_ReTktingAirline.Text.Trim)
                        DtAirName = dsAirName.Tables(0)
                        If DtAirName.Rows.Count > 0 Then
                            AirlineName = DtAirName.Rows(0)("AL_Name").ToString.Trim
                        End If
                        Dim RedepartDate As String
                        Dim RearrivalDate As String
                        RedepartDate = td_RetDate.InnerText.Trim().Replace("/", "")
                        RearrivalDate = txt_ReADate.Text.Replace("/", "").Trim




                        ST.insertFlightDetailsPnrImport(trackIdRoundTrip, td_To.InnerText, AirlineNameDepart, td_From.InnerText, AirNameDest, RedepartDate, txt_RetTime.Text.Trim, RearrivalDate, txt_ReATime.Text.Trim, txt_ReTktingAirline.Text.Trim, AirlineName, txt_ReFlight.Text.Trim, "", "", "", "", "", "", "")
                        'Insert Pax Deatils

                        'Ledger
                        Dim ReDebitADT As Double = 0, ReCreditADT As Double = 0, ReDebitCHD As Double = 0, ReCreditCHD As Double = 0, ReDebitINF As Double = 0, ReCreditINF As Double = 0
                        Dim ReDtFltFare As New DataTable
                        ReDtFltFare = ST.GetFltFareDtl(trackIdRoundTrip).Tables(0)
                        Dim ReDtFltHeaderADT As New DataTable
                        ReDtFltHeaderADT = ST.GetFltHeaderDetail(trackIdRoundTrip).Tables(0)
                        Dim ReAvalBalance As Double = Convert.ToDouble(ReDtFltHeaderADT.Rows(0)("TotalAfterDis")) + ReAval_Bal
                        Dim IP As String = Request.UserHostAddress
                        'LedgerEnd

                        Dim tkt1 As Integer = 0
                        For Each Rerow As GridViewRow In GridViewAdult.Rows
                            ReDebitADT = Convert.ToDouble(ReDtFltFare.Rows(0)("TotalAfterDis").ToString())
                            ReCreditADT = Convert.ToDouble(ReDtFltFare.Rows(0)("TotalDiscount").ToString())
                            ReAvalBalance = ReAvalBalance - ReDebitADT
                            Dim Relbltittle As Label = DirectCast(Rerow.FindControl("lbl_SirName"), Label)
                            Dim Relblfirstname As Label = DirectCast(Rerow.FindControl("lbl_FirstName"), Label)
                            Dim Relbllastname As Label = DirectCast(Rerow.FindControl("lbl_LastName"), Label)
                            'Dim Retxtticket As TextBox = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox)
                            'Dim Retxtticket As String = ""
                            'If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                            '    tkt1 += 1
                            '    Retxtticket = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim & (tkt1).ToString
                            'Else
                            '    Retxtticket = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim
                            'End If
                            Dim Retxtticket As String = ""
                            If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                                tkt1 += 1
                                If (InStr(DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim.ToUpper(), "_R")) Then
                                    Retxtticket = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim.ToUpper & (tkt1).ToString
                                Else
                                    Retxtticket = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim.ToUpper & "_R" & (tkt1).ToString
                                End If


                            Else
                                Retxtticket = DirectCast(Rerow.FindControl("txt_ReTicket"), TextBox).Text.Trim
                            End If

                            STDom.InsertProxyPaxInfoIntl(trackIdRoundTrip, Relbltittle.Text, Relblfirstname.Text, "", Relbllastname.Text, "ADT", Retxtticket, "I")
                            STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, Retxtticket, txt_ReTktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, ReDebitADT, 0, ReAvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                        Next
                        If (td_Child.InnerText > 0) Then
                            ReDebitCHD = Convert.ToDouble(ReDtFltFare.Rows(1)("TotalAfterDis").ToString())
                            ReCreditCHD = Convert.ToDouble(ReDtFltFare.Rows(1)("TotalDiscount").ToString())
                            For Each Rerowchd As GridViewRow In GridViewChild.Rows
                                ReAvalBalance = ReAvalBalance - ReDebitCHD
                                Dim RelblCtittle As Label = DirectCast(Rerowchd.FindControl("lbl_CSirName"), Label)
                                Dim RelblCfirstname As Label = DirectCast(Rerowchd.FindControl("lbl_CFirstName"), Label)
                                Dim RelblClastname As Label = DirectCast(Rerowchd.FindControl("lbl_CLastName"), Label)
                                'Dim RetxtCticket As TextBox = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox)
                                Dim RetxtCticket As String = ""
                                'If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                                '    tkt1 += 1
                                '    RetxtCticket = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim & (tkt1).ToString
                                'Else
                                '    RetxtCticket = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim
                                'End If

                                If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                                    tkt1 += 1
                                    If (InStr(DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim.ToUpper(), "_R")) Then
                                        RetxtCticket = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim.ToUpper & (tkt1).ToString
                                    Else
                                        RetxtCticket = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim.ToUpper & "_R" & (tkt1).ToString
                                    End If

                                Else
                                    RetxtCticket = DirectCast(Rerowchd.FindControl("txt_ReCTktNo"), TextBox).Text.Trim
                                End If


                                STDom.InsertProxyPaxInfoIntl(trackIdRoundTrip, RelblCtittle.Text, RelblCfirstname.Text, "", RelblClastname.Text, "CHD", RetxtCticket, "I")
                                STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, RetxtCticket, txt_ReTktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, ReDebitCHD, 0, ReAvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                            Next
                        End If
                        If (td_Infrant.InnerText > 0) Then

                            If (ReDtFltFare.Rows(1)("PaxType").ToString() = "INF") Then
                                ReDebitINF = Convert.ToDouble(ReDtFltFare.Rows(1)("TotalAfterDis").ToString())
                                ReCreditINF = Convert.ToDouble(ReDtFltFare.Rows(1)("TotalDiscount").ToString())
                            Else

                                ReDebitINF = Convert.ToDouble(ReDtFltFare.Rows(2)("TotalAfterDis").ToString())
                                ReCreditINF = Convert.ToDouble(ReDtFltFare.Rows(2)("TotalDiscount").ToString())

                            End If



                            For Each Rerowinf As GridViewRow In GridViewInfrant.Rows
                                ReAvalBalance = ReAvalBalance - ReDebitINF
                                Dim RelblItittle As Label = DirectCast(Rerowinf.FindControl("lbl_ISirName"), Label)
                                Dim RelblIfirstname As Label = DirectCast(Rerowinf.FindControl("lbl_IFirstName"), Label)
                                Dim RelblIlastname As Label = DirectCast(Rerowinf.FindControl("lbl_ILastName"), Label)
                                'Dim RetxtIticket As TextBox = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox)
                                Dim RetxtIticket As String = ""
                                'If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                                '    tkt1 += 1
                                '    RetxtIticket = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim & (tkt1).ToString
                                'Else
                                '    RetxtIticket = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim
                                'End If
                                If txt_ReTktingAirline.Text.Trim.ToUpper = "6E" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "SG" OrElse txt_ReTktingAirline.Text.Trim.ToUpper = "G8" Then
                                    tkt1 += 1
                                    If (InStr(DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim.ToUpper(), "_R")) Then
                                        RetxtIticket = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim.ToUpper & (tkt1).ToString
                                    Else
                                        RetxtIticket = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim.ToUpper & "_R" & (tkt1).ToString
                                    End If

                                Else
                                    RetxtIticket = DirectCast(Rerowinf.FindControl("txt_ReITktNo"), TextBox).Text.Trim
                                End If

                                STDom.InsertProxyPaxInfoIntl(trackIdRoundTrip, RelblItittle.Text, RelblIfirstname.Text, "", RelblIlastname.Text, "INF", RetxtIticket, "I")
                                STDom.insertLedgerDetails(td_AgentID.InnerText, lbl_AgencyName.Text, trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, RetxtIticket, txt_ReTktingAirline.Text.Trim, "", "", Session("UID").ToString, IP, ReDebitINF, 0, ReAvalBalance, "PROXY", "Created By Proxy", 0, ViewState("ProjectId"), ViewState("BookedBy"), RECORPBILLNO)
                            Next
                        End If
                        'NAV METHOD CALL START
                        Try

                            'Dim objNav As New AirService.clsConnection(trackIdRoundTrip, "0", "0")
                            'objNav.airBookingNav(trackIdRoundTrip, "", 0)

                        Catch ex As Exception

                        End Try
                        'Nav METHOD END'
                        'Yatra Billing 
                        'Online
                        Try
                            'Dim AirObj As New AIR_YATRA
                            'AirObj.ProcessYatra_Air(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim, "B")
                        Catch ex As Exception

                        End Try
                        'online end
                        'Offline
                        'Try
                        '    STYTR.InsertYatra_MIRHEADER(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_PAX(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_SEGMENT(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_FARE(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim)
                        '    STYTR.InsertYatra_DIFTLINES(trackIdRoundTrip, txt_ReGDSPNR.Text.Trim)
                        'Catch ex As Exception
                        'End Try
                        'offline end
                        'Yatra Billing end
                        Try
                            Dim ResmsStatus As String = ""
                            Dim ResmsMsg As String = ""
                            Dim ReobjSMSAPI As New SMSAPI.SMS

                            Dim SmsCrd As DataTable
                            Dim objDA As New SqlTransaction
                            SmsCrd = objDA.SmsCredential(SMS.PROXY.ToString()).Tables(0)
                            If SmsCrd.Rows.Count > 0 AndAlso SmsCrd.Rows(0)("Status") = True Then
                                ResmsStatus = ReobjSMSAPI.sendSms(trackIdRoundTrip, td_AgentMobNo.InnerText, td_To.InnerText & ":" & td_From.InnerText, txt_ReTktingAirline.Text.Trim, "", td_RetDate.InnerText, txt_ReGDSPNR.Text, ResmsMsg, SmsCrd)
                                objSql.SmsLogDetails(trackIdRoundTrip, td_AgentMobNo.InnerText, ResmsMsg, ResmsStatus)
                            End If
                            
                        Catch ex As Exception

                        End Try
                        'Update Proxy
                        If (td_TravelType.InnerHtml = "Round Trip" And PxCD = "I" And SpecialRT = False) Then

                            STDom.UpdateProxyDate("Ticketed", Request.QueryString("ProxyID"), SrvchargOneWay, SrvchargTwoWay, trackIdOneWay, trackIdRoundTrip, rbd.Text.Trim, re_rbd.Text.Trim)
                        End If
                        If (td_TravelType.InnerHtml = "Round Trip" And PxCD = "D" And SpecialRT = False) Then
                            STDom.UpdateProxyDate("Ticketed", Request.QueryString("ProxyID"), 0, 0, trackIdOneWay, trackIdRoundTrip, "", "")
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Unable to update proxy.Please try after some time.');", True)
                End If

            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Round Trip proxy already updated.');", True)
            End If



        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub CalFareDetails(ByVal Orderid As String, ByVal VC As String, ByVal PaxType As String, ByVal BaseFare As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal OT As Integer, Optional ByVal LAST As String = "ONE")
        Try
            Dim Tax As String = ""
            Tax = "YQ:" & YQ.ToString() & "#YR:" & YR.ToString() & "#OT:" & OT.ToString() & "#WO:" & WO.ToString() & "#"
            'Calculate Commission
            Dim cls As String = ""
            Dim Origin As String = ""
            Dim Dest As String = ""
            Dim GroupType As String = ""
            Dim ds As New DataSet
            Dim dsG As New DataSet
            Dim DtG As New DataTable
            Dim Comm As Double = 0
            Dim tdsper As String
            Dim Tds As Integer = 0
            Dim Commcal As Double = 0
            Dim CBCal As Double = 0
            Dim TFee As Double = 0
            Dim dtTfee As New DataTable()
            Dim TFeePer As Double = 0
            Dim dtcom As New DataTable
            Dim srvtax As Double = 0
            If (IsCorp = True) Then
                '''''''''''''''''''''CORPORATE================================

                ' dtTfee = ST.calcServicecharge(txt_TktingAirline.Text, "D").Tables(0)
                ' If dtTfee.Rows.Count > 0 Then
                TFee = 0 'Convert.ToDouble(dtTfee.Rows(0)("TranFee").ToString())
                ' End If
                TFeePer = 0

                If (td_TravelType.InnerText = "One Way" orelse SpecialRT=True ) Then
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    'For Special Fare Cashback
                    If LAST = "ONE" Then
                        Dim SF As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SF = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SF) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    End If
                    ' If PaxType <> "INF" Then

                    dtcom = ClsCorp.GetManagementFeeSrvTax(GroupType, VC, BaseFare, YQ, "D", (BaseFare + YQ + YR + WO + OT)).Tables(0)
                    If dtcom.Rows.Count > 0 Then
                        Commcal = Convert.ToDouble(dtcom.Rows(0)("MGTFEE").ToString())
                        srvtax = Convert.ToDouble(dtcom.Rows(0)("MGTSRVTAX").ToString())
                    End If
                    CBCal = 0
                    Comm = Commcal + CBCal
                    tdsper = 0
                    Tds = 0
                    'End If

                    'Added Specail Fare in Commission For Adult and Child
                    '  If PaxType <> "INF" Then
                    ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, srvtax, 0, 0, VC, "D")
                    'Else
                    '    ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, 0, 0, 0, 0, 0, VC, "D")
                    'End If




                End If
                If (td_TravelType.InnerText = "Round Trip" And SpecialRT = False) Then
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString()
                    'For Special Fare Cashback
                    If LAST = "ONE" Then
                        Dim SFONE As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SFONE = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    ElseIf LAST = "ROUND" Then
                        Dim SFROUND As Double = 0
                        If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                            SFROUND = Convert.ToDouble(txt_ReSFDis.Text)
                        End If
                        SFTot = (SFROUND) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    End If

                    '  If PaxType <> "INF" Then
                    dtcom = ClsCorp.GetManagementFeeSrvTax(GroupType, VC, BaseFare, YQ, "D", (BaseFare + YQ + YR + WO + OT)).Tables(0)
                    If dtcom.Rows.Count > 0 Then
                        Commcal = dtcom.Rows(0)("MGTFEE").ToString()
                        srvtax = dtcom.Rows(0)("MGTSRVTAX").ToString()
                    End If

                    'CBCal = dtcom.Rows(0)("CB").ToString()

                    CBCal = 0
                    Comm = Commcal + CBCal

                    'Cal TDS
                    tdsper = 0
                    Tds = 0
                    ' 'End If
                    'If PaxType <> "INF" Then
                    ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, srvtax, 0, 0, VC, "D")
                    'Else
                    'ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, 0, 0, 0, 0, 0, VC, "D")
                    ' End If
                End If



                '''''''''''''''''''''END CORPORATE============================
            Else
                dtTfee = ST.calcServicecharge(txt_TktingAirline.Text, "D").Tables(0)
                If dtTfee.Rows.Count > 0 Then
                    TFee = Convert.ToDouble(dtTfee.Rows(0)("TranFee").ToString())
                End If
                TFeePer = ((BaseFare + YQ) * TFee) / 100

                Dim srvtax1 As String, STax As Double = 0, Comm1 As Double = 0
                srvtax1 = dtTfee.Rows(0)("SrvTax").ToString
                

                If (td_TravelType.InnerText = "One Way" OrElse SpecialRT = True) Then
                    Origin = td_From.InnerText
                    Dest = td_To.InnerText
                    cls = td_Classes.InnerText
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    'For Special Fare Cashback
                    If LAST = "ONE" Then
                        Dim SF As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SF = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SF) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    End If
                    If PaxType <> "INF" Then

                        dtcom = CCAP.calcCommDom(GroupType, VC, BaseFare, YQ, 1)
                        If dtcom.Rows.Count > 0 Then
                            Commcal = dtcom.Rows(0)("Dis").ToString()
                        End If

                        'CBCal = dtcom.Rows(0)("CB").ToString()
                        If txt_TktingAirline.Text.Trim.ToUpper = "G8" And LAST = "ONE" Then
                            If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then
                                If dtcom.Rows.Count > 0 Then
                                    CBCal = dtcom.Rows(0)("CB").ToString()
                                End If

                            Else
                                CBCal = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
                            End If
                        ElseIf txt_ReTktingAirline.Text.Trim.ToUpper = "G8" And LAST = "ROUND" Then
                            If td_To.InnerText & ":" & td_From.InnerText <> "DEL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:PAT" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXC" And td_To.InnerText & ":" & td_From.InnerText <> "IXC:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:PAT" Then
                                If dtcom.Rows.Count > 0 Then
                                    CBCal = dtcom.Rows(0)("CB").ToString()
                                End If
                            Else
                                CBCal = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
                            End If
                        Else
                            If dtcom.Rows.Count > 0 Then
                                CBCal = dtcom.Rows(0)("CB").ToString()
                            End If
                        End If
                        If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
                            STax = Math.Round(((Commcal * srvtax1) / 100), 0)
                        Else
                            STax = 0
                        End If
                        Comm1 = Commcal + CBCal
                        Commcal = Commcal - STax
                        Comm = Commcal + CBCal
                        'Cal TDS
                        tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                        Tds = Math.Round((((Commcal - TFeePer) * Convert.ToDouble(tdsper)) / 100), 0)
                    End If

                    'Added Specail Fare in Commission For Adult and Child
                    If PaxType <> "INF" Then
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm + SFTot, CBCal + SFTot, Tds, VC, "D", Comm1, "", 0)
                    Else
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, Tds, VC, "D", Comm1, "", 0)
                    End If




                End If
                If (td_TravelType.InnerText = "Round Trip" And SpecialRT = False) Then
                    Dest = td_From.InnerText
                    Origin = td_To.InnerText
                    cls = td_Classes.InnerText
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString()
                    'For Special Fare Cashback
                    If LAST = "ONE" Then
                        Dim SFONE As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SFONE = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    ElseIf LAST = "ROUND" Then
                        Dim SFROUND As Double = 0
                        If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                            SFROUND = Convert.ToDouble(txt_ReSFDis.Text)
                        End If
                        SFTot = (SFROUND) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    End If

                    If PaxType <> "INF" Then
                        dtcom = CCAP.calcCommDom(GroupType, VC, BaseFare, YQ, 1)
                        If dtcom.Rows.Count > 0 Then
                            Commcal = dtcom.Rows(0)("Dis").ToString()
                        End If

                        'CBCal = dtcom.Rows(0)("CB").ToString()

                        If txt_TktingAirline.Text.Trim.ToUpper = "G8" And LAST = "ONE" Then
                            If td_From.InnerText & ":" & td_To.InnerText <> "DEL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "DEL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:DEL" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:PAT" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "BOM:IXC" And td_From.InnerText & ":" & td_To.InnerText <> "IXC:BOM" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "SXR:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:SXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXJ:IXL" And td_From.InnerText & ":" & td_To.InnerText <> "IXL:IXJ" And td_From.InnerText & ":" & td_To.InnerText <> "PAT:IXR" And td_From.InnerText & ":" & td_To.InnerText <> "IXR:PAT" Then

                                If dtcom.Rows.Count > 0 Then
                                    CBCal = dtcom.Rows(0)("CB").ToString()
                                End If
                            Else
                                CBCal = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
                            End If
                        ElseIf txt_ReTktingAirline.Text.Trim.ToUpper = "G8" And LAST = "ROUND" Then
                            If td_To.InnerText & ":" & td_From.InnerText <> "DEL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "DEL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:DEL" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:PAT" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "BOM:IXC" And td_To.InnerText & ":" & td_From.InnerText <> "IXC:BOM" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "SXR:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:SXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXJ:IXL" And td_To.InnerText & ":" & td_From.InnerText <> "IXL:IXJ" And td_To.InnerText & ":" & td_From.InnerText <> "PAT:IXR" And td_To.InnerText & ":" & td_From.InnerText <> "IXR:PAT" Then
                                If dtcom.Rows.Count > 0 Then
                                    CBCal = dtcom.Rows(0)("CB").ToString()
                                End If
                            Else
                                CBCal = 0 'Math.Round(commDt.Rows(0)("CB"), 0)
                            End If
                        Else
                            If dtcom.Rows.Count > 0 Then
                                CBCal = dtcom.Rows(0)("CB").ToString()
                            End If
                        End If

                        If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
                            STax = Math.Round(((Commcal * srvtax1) / 100), 0)
                        Else
                            STax = 0
                        End If
                        Comm1 = Commcal + CBCal
                        Commcal = Commcal - STax
                        Comm = Commcal + CBCal

                        'Comm = Commcal + CBCal

                        'Cal TDS
                        tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                        Tds = Math.Round((((Commcal - TFeePer) * Convert.ToDouble(tdsper)) / 100), 0)
                    End If
                    If PaxType <> "INF" Then
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm + SFTot, CBCal + SFTot, Tds, VC, "D", Comm1, "", 0)
                    Else
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, 0, Tds, VC, "D", Comm1, "", 0)
                    End If
                End If
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Public Sub CalFareDetails_Intl(ByVal Orderid As String, ByVal VC As String, ByVal PaxType As String, ByVal BaseFare As Integer, ByVal YQ As Integer, ByVal YR As Integer, ByVal WO As Integer, ByVal OT As Integer, ByVal AdmMrk As Double, Optional ByVal LAST As String = "ROUND")
        'This Function calculates Fare details per Pax
        Try
            Dim Tax As String = ""
            Tax = "YQ:" & YQ.ToString() & "#YR:" & YR.ToString() & "#OT:" & OT.ToString() & "#WO:" & WO.ToString() & "#"
            'Calculate Commission
            Dim cls As String = ""
            Dim Origin As String = ""
            Dim Dest As String = ""
            Dim GroupType As String = ""
            Dim ds As New DataSet
            Dim dsG As New DataSet
            Dim DtG As New DataTable
            Dim Comm As Double = 0
            Dim tdsper As String = ""
            Dim Tds As Integer = 0
            Dim Commcal As Double = 0 'Added comm for per Pax
            Dim CBCal As Double = 0   'Cash Back 0
            Dim TFeeIntl As Double = 0
            Dim dtTfee As New DataTable()
            Dim TFeePer As Double = 0
            Dim srvtax As Double = 0
            Dim resrvtax As Double = 0
            If (IsCorp = True) Then
                '================================================================CORPORATE--------------------------------------------------

                'Service tax per Pax
                TFeeIntl = 0
                'For Special Fare
                If LAST = "ONE" Then
                    Dim SFONE As Double = 0
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFONE = Convert.ToDouble(txt_SFDis.Text)
                    End If
                    SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                End If
                If (td_TravelType.InnerText = "One Way" OrElse SpecialRT = True) Then
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    ' If PaxType <> "INF" Then
                    'Dim dtcom As New DataTable
                    Dim mgtfee As New DataTable
                    mgtfee = ClsCorp.GetManagementFeeSrvTax(GroupType, VC.ToUpper, Convert.ToDouble(BaseFare), YQ, "I", (BaseFare + YQ + YR + WO + OT)).Tables(0)
                    Commcal = Convert.ToDouble(mgtfee.Rows(0)("MGTFEE").ToString())
                    Comm = Commcal
                    srvtax = Convert.ToDouble(mgtfee.Rows(0)("MGTSRVTAX").ToString())
                    'Cal TDS
                    tdsper = 0
                    Tds = 0
                    'End If
                    'Added Specail Fare in Commission For Adult and Child
                    'If PaxType <> "INF" Then
                    ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, AdmMrk, 0, 0, Comm, 0, srvtax, 0, 0, VC, "I")
                    'Else
                    ' ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, 0, 0, 0, 0, 0, VC, "I")
                    'End If
                End If

                If (td_TravelType.InnerText = "Round Trip" And SpecialRT = False) Then
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    If LAST = "ONE" Then
                        Dim SFONE As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SFONE = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    ElseIf LAST = "ROUND" Then
                        Dim SFROUND As Double = 0
                        If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                            SFROUND = Convert.ToDouble(txt_ReSFDis.Text)
                        End If
                        SFTot = (SFROUND) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                    End If
                    ' If PaxType <> "INF" Then
                    Dim remgtfee As New DataTable
                    remgtfee = ClsCorp.GetManagementFeeSrvTax(GroupType, VC.ToUpper, Convert.ToDouble(BaseFare), YQ, "I", (BaseFare + YQ + YR + WO + OT)).Tables(0)
                    Commcal = Convert.ToDouble(remgtfee.Rows(0)("MGTFEE").ToString())
                    resrvtax = Convert.ToDouble(remgtfee.Rows(0)("MGTSRVTAX").ToString())
                    Comm = Commcal
                    'Cal TDS
                    tdsper = 0
                    Tds = 0
                    'End If
                    'If PaxType <> "INF" Then
                    ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, AdmMrk, 0, 0, Comm, 0, resrvtax, 0, 0, VC, "I")
                    'Else
                    '  ClsCorp.calcFareCorp(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, 0, 0, 0, 0, 0, VC, "I")
                    'End If

                End If



                '==================================================END CORPORATE===========================================================================

            Else

                'Service tax per Pax
                dtTfee = ST.calcServicecharge(txt_TktingAirline.Text, "I").Tables(0)
                If dtTfee.Rows.Count > 0 Then
                    TFeeIntl = Convert.ToDouble(dtTfee.Rows(0)("TranFee").ToString())
                End If
                TFeePer = ((BaseFare + YQ) * TFeeIntl) / 100
                Dim srvtax1 As String, STax As Double = 0, Comm1 As Double = 0
                srvtax1 = dtTfee.Rows(0)("SrvTax").ToString

                'For Special Fare
                If LAST = "ONE" Then
                    Dim SFONE As Double = 0
                    If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                        SFONE = Convert.ToDouble(txt_SFDis.Text)
                    End If
                    SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                End If
                If (td_TravelType.InnerText = "One Way" OrElse SpecialRT = True) Then
                    Origin = td_From.InnerText
                    Dest = td_To.InnerText
                    'cls = td_Classes.InnerText
                    cls = rbd.Text.Trim.ToUpper
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    If PaxType <> "INF" Then
                        
                        'Dim dtcom As New DataTable
                        'Commcal = CCAP.calcComm(GroupType, VC.ToUpper, Convert.ToDouble(BaseFare), YQ, Origin, Dest, cls)
                        Commcal = CCAP.calcComm(GroupType, VC.ToUpper, Convert.ToDouble(BaseFare), YQ, Origin, Dest, cls, 0, td_DepartDate.InnerText.Trim().Replace("/", ""), "")
                        'Comm = Commcal + CBCal
                        If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
                            STax = Math.Round(((Commcal * srvtax1) / 100), 0)
                        Else
                            STax = 0
                        End If
                        Comm1 = Commcal + CBCal
                        Commcal = Commcal - STax
                        Comm = Commcal + CBCal

                        'Cal TDS
                        tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                        Tds = Math.Round((((Commcal - TFeePer) * Convert.ToDouble(tdsper)) / 100), 0)
                    End If
                    'Added Specail Fare in Commission For Adult and Child
                    If PaxType = "INF" Then
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, CBCal, Tds, VC, "I", Comm1, "", 0)
                    ElseIf PaxType <> "INF" Then
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, AdmMrk, 0, 0, Comm + SFTot, CBCal, Tds, VC, "I", Comm1, "", 0)
                    End If
                End If

                If (td_TravelType.InnerText = "Round Trip" And SpecialRT = False) Then
                    Origin = td_To.InnerText 'Reverse Origin and dest
                    Dest = td_From.InnerText
                    cls = re_rbd.Text.Trim.ToUpper
                    dsG = ST.GetAgencyDetails(td_AgentID.InnerText)
                    DtG = dsG.Tables(0)
                    GroupType = DtG.Rows(0)("agent_type").ToString
                    Dim DepartDate As String = ""
                    Dim ReturnDate As String = ""
                    If LAST = "ONE" Then
                        Dim SFONE As Double = 0
                        If txt_SFDis.Text <> "" AndAlso txt_SFDis.Text IsNot Nothing Then
                            SFONE = Convert.ToDouble(txt_SFDis.Text)
                        End If
                        SFTot = (SFONE) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                        DepartDate = td_DepartDate.InnerText.Trim().Replace("/", "")
                        ReturnDate = ""
                    ElseIf LAST = "ROUND" Then
                        Dim SFROUND As Double = 0
                        If txt_ReSFDis.Text <> "" AndAlso txt_ReSFDis.Text IsNot Nothing Then
                            SFROUND = Convert.ToDouble(txt_ReSFDis.Text)
                        End If
                        SFTot = (SFROUND) / (Convert.ToDouble(td_Adult.InnerText) + Convert.ToDouble(td_Child.InnerText))
                        DepartDate = td_RetDate.InnerText.Trim().Replace("/", "")
                        ReturnDate = ""
                    End If
                    If PaxType <> "INF" Then

                        Commcal = CCAP.calcComm(GroupType, VC.ToUpper, Convert.ToDouble(BaseFare), YQ, Origin, Dest, cls, 0, DepartDate, ReturnDate)
                        ' Comm = Commcal + CBCal

                        If srvtax1 <> "" AndAlso srvtax1 IsNot Nothing Then
                            STax = Math.Round(((Commcal * srvtax1) / 100), 0)
                        Else
                            STax = 0
                        End If
                        Comm1 = Commcal + CBCal
                        Commcal = Commcal - STax
                        Comm = Commcal + CBCal
                        'Cal TDS
                        tdsper = CCAP.geTdsPercentagefromDb(td_AgentID.InnerText)
                        Tds = Math.Round((((Commcal - TFeePer) * Convert.ToDouble(tdsper)) / 100), 0)
                    End If
                    If PaxType = "INF" Then
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, 0, 0, 0, Comm, CBCal, Tds, VC, "I", Comm1, "", 0)
                    Else
                        ST.calcFare(Tax.Split("#"), Orderid, PaxType, BaseFare, AdmMrk, 0, 0, Comm + SFTot, CBCal + SFTot, Tds, VC, "I", Comm1, "", 0)
                    End If

                End If
            End If


        Catch ex As Exception
            clsErrorLog.LogInfo(ex)
            If txt_resrvcharge.Text <> "" AndAlso txt_resrvcharge.Text IsNot Nothing Then
                SrvchargTwoWay = Convert.ToDouble(txt_resrvcharge.Text.Trim())
            End If
        End Try
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_UpdateProxy.Click
        Try

            btn_UpdateProxy.Attributes.Add("onclick", "return confirm('are you sure you want to Update? ');")
            Dim dtAvlBal As New DataTable
            dtAvlBal = STDom.GetAgencyDetails(td_AgentID.InnerText).Tables(0)
            If td_TravelType.InnerHtml = "One Way" OrElse SpecialRT = True Then
                btn_UpdateProxy.Enabled = False

                Dim ProxyCharge As Double = 0
                If txt_ProxyChargeOW.Text <> "" AndAlso txt_ProxyChargeOW.Text IsNot Nothing Then
                    ProxyCharge = Convert.ToDouble(txt_ProxyChargeOW.Text.Trim())
                End If

                Dim Total As String = Convert.ToString(Convert.ToInt32(lbl_TBCAFTRD.Text) + ProxyCharge)
                If Convert.ToDouble(dtAvlBal.Rows(0)("Crd_Limit").ToString()) < Convert.ToDouble(Total) Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Check Balance');", True)
                Else
                    InsertOneWayDetails()
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Proxy Updated Sucessfully');;window.location='ProxyTicketDetail.aspx';", True)
                End If
            End If
            If td_TravelType.InnerHtml = "Round Trip" And SpecialRT = False Then
                btn_UpdateProxy.Enabled = False

                Dim ProxyChargeOW As Integer = 0
                Dim ProxyChargeRT As Integer = 0
                If txt_ProxyChargeOW.Text <> "" AndAlso txt_ProxyChargeOW.Text IsNot Nothing Then
                    ProxyChargeOW = Convert.ToDouble(txt_ProxyChargeOW.Text.Trim())
                End If
                If txt_ProxyChargeRT.Text <> "" AndAlso txt_ProxyChargeRT.Text IsNot Nothing Then
                    ProxyChargeRT = Convert.ToDouble(txt_ProxyChargeRT.Text.Trim())
                End If



                Dim Total As String = Convert.ToString(Convert.ToInt32(lbl_TBCAFTRD.Text) + Convert.ToInt32(lbl_ReTBCAFTRD.Text) + ProxyChargeOW + ProxyChargeRT)
                If Convert.ToDouble(dtAvlBal.Rows(0)("Crd_Limit").ToString()) < Convert.ToDouble(Total) Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Please Check Balance');", True)

                Else
                    InsertOneWayDetails()
                    InsertRoundTripDetails()
                    ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "alert('Proxy Updated Sucessfully');window.location='ProxyTicketDetail.aspx';", True)
                End If
            End If
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Sub
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        Try
            target = value
            Return value
        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Function
End Class
