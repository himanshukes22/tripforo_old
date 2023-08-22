<%@ WebHandler Language="VB" Class="DomFareDtls" %>

Imports System
Imports System.Web

Public Class DomFareDtls : Implements IHttpHandler, IReadOnlySessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        context.Response.Write(domfarebreakup(context.Request("ln"), context.Request("ft")))
    End Sub
    Public Function domfarebreakup(ByVal ln As String, ByVal ft As String) As String
        Dim ObjCommPlb As New clsCalcCommAndPlb
        Dim DomFareDetails As Hashtable
        DomFareDetails = ObjCommPlb.getDomFareDetails(ln, ft)
        Dim fbStr As String = ""
        Dim adt As Integer = 0, chd As Integer = 0, inf As Integer = 0
        Dim AirArray As Array
        AirArray = HttpContext.Current.Session("DomAirDt").Select("LineItemNumber='" & ln & "'", "")
        adt = (AirArray(0)("Adult"))
        chd = (AirArray(0)("Child"))
        inf = (AirArray(0)("Infant"))
        
        fbStr = fbStr & "<table width='500' cellspacing='0' cellpadding='0' style='border:3px #666666 solid; font-size:11px; background:#FFFFA8; font-family:verdana; padding-left:5px'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='84%' align='left' style='height:20px; font-weight:bold;'>Fare Summary</td>"
        fbStr = fbStr & "<td width='16%' align='center'><img src='../images/close.gif' onclick=javascript:HideContent('divfareDetails'); return true; /></td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table></td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td><table width='100%' border='0' cellspacing='2' cellpadding='2'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='9%' align='center'>&nbsp;</td>"
        fbStr = fbStr & "<td width='15%' align='center' style='font-weight:bold;'>Base Fare</td>"
        fbStr = fbStr & "<td width='35%' align='center' style='font-weight:bold;'>Tax</td>"
        fbStr = fbStr & "<td width='15%' align='center' style='font-weight:bold;'>Total</td>"
        fbStr = fbStr & "<td width='9%' align='center' style='font-weight:bold;'>Pax</td>"
        fbStr = fbStr & "<td width='17%' align='center' style='font-weight:bold;'>Total</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td align='center' style='font-weight:bold;'>Adult</td>"
        fbStr = fbStr & "<td align='center'>" & DomFareDetails("AdtBFare") & "</td>"
        fbStr = fbStr & "<td align='center'>" & DomFareDetails("AdtTax") & "</td>"
        fbStr = fbStr & "<td align='center'>" & DomFareDetails("AdtTotal") & "</td>"
        fbStr = fbStr & "<td align='center'>x " & adt & " =</td>"
        fbStr = fbStr & "<td align='center'>" & DomFareDetails("AdtTotal") * adt & "</td>"
        fbStr = fbStr & "</tr>"
        If Val(chd) > 0 Then
            fbStr = fbStr & "<tr>"
            fbStr = fbStr & "<td align='center' style='font-weight:bold;'>Child</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("ChdBFare") & "</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("ChdTax") & "</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("ChdTotal") & "</td>"
            fbStr = fbStr & "<td align='center'>x " & chd & " =</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("ChdTotal") * chd & "</td>"
            fbStr = fbStr & "</tr>"
        End If
        
        If Val(inf) > 0 Then
            fbStr = fbStr & "<tr>"
            fbStr = fbStr & "<td align='center' style='font-weight:bold;'>Infant</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("InfBFare") & "</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("InfTax") & "</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("InfTotal") & "</td>"
            fbStr = fbStr & "<td align='center'>x " & inf & " =</td>"
            fbStr = fbStr & "<td align='center'>" & DomFareDetails("InfTotal") * inf & "</td>"
            fbStr = fbStr & "</tr>"
        End If
       
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='6'><table width='100%' border='0' cellspacing='2' cellpadding='2'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='14%'>&nbsp;</td>"
        fbStr = fbStr & "<td width='14%' style='font-weight:bold;'>Srv. Tax  </td>"
        fbStr = fbStr & "<td width='11%'>" & DomFareDetails("SrvTax") & "</td>"
        fbStr = fbStr & "<td width='14%' style='font-weight:bold;'>Tran. Fee</td>"
        fbStr = fbStr & "<td width='12%'>" & DomFareDetails("TFee") & "</td>"
        fbStr = fbStr & "<td width='19%' style='font-weight:bold;'>Tran. Charges </td>"
        fbStr = fbStr & "<td width='16%'>" & DomFareDetails("TC") & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table></td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='5' align='right' style='padding-right:10px; font-weight:bold;'>Total Fare</td>"
        fbStr = fbStr & "<td onmouseover='function ifd(){document.getElementById('tblcommint').style.display='block';}ifd();' onmouseout='function ifd1(){document.getElementById('tblcommint').style.display='none';}fd1();' style='font-weight:bold'>" & DomFareDetails("totFare") & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='6'><table width='100%' border='0' cellspacing='2' cellpadding='2' id='tblcommint' style='display:block'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='18%'>Commission</td>"
        fbStr = fbStr & "<td width='12%'>" & DomFareDetails("totComm") & "</td>"
        fbStr = fbStr & "<td width='16%'>Cash Back</td><td width='10%'>" & DomFareDetails("totCB") & "</td>"
        fbStr = fbStr & "<td width='8%'>TDS</td>"
        fbStr = fbStr & "<td width='7%'>" & DomFareDetails("totTds") & "</td>"
        fbStr = fbStr & "<td width='12%' align='right'>Net Fare </td>"
        fbStr = fbStr & "<td width='17%'>" & DomFareDetails("netFare") & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table>"
        fbStr = fbStr & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table>"
        fbStr = fbStr & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table>"
        Return fbStr
    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class