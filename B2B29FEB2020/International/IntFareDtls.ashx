<%@ WebHandler Language="VB" Class="IntFareDtls" %>

Imports System
Imports System.Web

Public Class IntFareDtls : Implements IHttpHandler, IReadOnlySessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        context.Response.Write(intfarebreakup(context.Request("ln")))
    End Sub
    Public Function intfarebreakup(ByVal ln As String) As String
        Dim ObjCommPlb As New clsCalcCommAndPlb
        Dim IntFareDetails As Hashtable
        IntFareDetails = ObjCommPlb.getIntFareDetails(ln)
        Dim fbStr As String = ""
        Dim adt As Integer = 0, chd As Integer = 0, inf As Integer = 0        
        Dim AirArray As Array
        AirArray = HttpContext.Current.Session("IntAirDt").Select("LineItemNumber='" & ln & "'", "")
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
        fbStr = fbStr & "<td align='center'>" & IntFareDetails("AdtBFare") & "</td>"
        fbStr = fbStr & "<td align='center'>" & IntFareDetails("AdtTax") & "</td>"
        fbStr = fbStr & "<td align='center'>" & IntFareDetails("AdtTotal") & "</td>"
        fbStr = fbStr & "<td align='center'>x " & adt & " =</td>"
        fbStr = fbStr & "<td align='center'>" & IntFareDetails("AdtTotal") * adt & "</td>"
        fbStr = fbStr & "</tr>"
        If Val(chd) > 0 Then
            fbStr = fbStr & "<tr>"
            fbStr = fbStr & "<td align='center' style='font-weight:bold;'>Child</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("ChdBFare") & "</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("ChdTax") & "</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("ChdTotal") & "</td>"
            fbStr = fbStr & "<td align='center'>x " & chd & " =</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("ChdTotal") * chd & "</td>"
            fbStr = fbStr & "</tr>"
        End If
        
        If Val(inf) > 0 Then
            fbStr = fbStr & "<tr>"
            fbStr = fbStr & "<td align='center' style='font-weight:bold;'>Infant</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("InfBFare") & "</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("InfTax") & "</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("InfTotal") & "</td>"
            fbStr = fbStr & "<td align='center'>x " & inf & " =</td>"
            fbStr = fbStr & "<td align='center'>" & IntFareDetails("InfTotal") * inf & "</td>"
            fbStr = fbStr & "</tr>"
        End If
       
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='6'><table width='100%' border='0' cellspacing='2' cellpadding='2'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='14%'>&nbsp;</td>"
        fbStr = fbStr & "<td width='14%' style='font-weight:bold;'>Srv. Tax  </td>"
        fbStr = fbStr & "<td width='11%'>" & IntFareDetails("SrvTax") & "</td>"
        fbStr = fbStr & "<td width='14%' style='font-weight:bold;'>Tran. Fee</td>"
        fbStr = fbStr & "<td width='12%'>" & IntFareDetails("TFee") & "</td>"
        fbStr = fbStr & "<td width='19%' style='font-weight:bold;'>Tran. Charges </td>"
        fbStr = fbStr & "<td width='16%'>" & IntFareDetails("TC") & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "</table></td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='5' align='right' style='padding-right:10px; font-weight:bold;'>Total Fare</td>"
        fbStr = fbStr & "<td onmouseover='function ifd(){document.getElementById('tblcommint').style.display='block';}ifd();' onmouseout='function ifd1(){document.getElementById('tblcommint').style.display='none';}fd1();' style='font-weight:bold'>" & IntFareDetails("totFare") & "</td>"
        fbStr = fbStr & "</tr>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td colspan='6'><table width='100%' border='0' cellspacing='2' cellpadding='2' id='tblcommint' style='display:block'>"
        fbStr = fbStr & "<tr>"
        fbStr = fbStr & "<td width='17%'>Commission</td>"
        fbStr = fbStr & "<td width='14%'>" & IntFareDetails("totComm") & "</td>"
        fbStr = fbStr & "<td width='7%'>TDS</td>"
        fbStr = fbStr & "<td width='12%'>" & IntFareDetails("totTds") & "</td>"
        fbStr = fbStr & "<td width='29%' align='right'>Net Fare </td>"
        fbStr = fbStr & "<td width='21%'>" & IntFareDetails("netFare") & "</td>"
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