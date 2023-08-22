Imports Microsoft.VisualBasic

Public Class Status

    Public Function GetID(ByVal preChar As String) As String
        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim rnmd As String = GetRandomNumber(allowedChars)
        Return preChar & rnmd
    End Function

    Private Function GetRandomNumber(ByVal allowedChars1 As String) As String
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars1.Split(sep)
        Dim rndString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To 9
            temp = arr(rand.[Next](0, arr.Length))
            rndString += temp
        Next
        Return rndString
    End Function

End Class
Public Enum StatusClass
    Pending
    InProcess
    Ticketed
    Rejected
    Cancelled
    CancelRequest
    CancelInprocess
    CancelRejecRejectt
    ReIssueRequest
    ReIssueInProcess
    ReIssue
    ProxyRequest
    ProxyInProcess
    Confirm
    Hold
End Enum
Public Enum MAILING
    AIR_INVOICE
    HOTEL_INVOICE
    BUS_INVOICE
    RAIL_INVOICE
    UTILITY_INVOICE
    AIR_BOOKING
    HOTEL_BOOKING
    BUS_BOOKING
    RAIL_BOOKING
    UTILITY_BOOKING
    AIR_PNRSUMMARY
    REGISTRATION_AGENT
    REGISTRATION_SUBAGENT
    REGISTRATION_STOCKIEST
    SALESUPPORT
    AIR_MAILINGINFANT
    RAIL_ERSCOPY
    RAIL_CREDITNOTE
    OLSERIES_FQFORM
    AIR_FLIGHTRESULT
    FEEDBACK
    FORGOTPASSWORD
    ENQUIRY_D
    ENQUIRY_I

    RWT_REGISTRATION
    RWT_REGISTRATION_DISTR
    RWT_REGISTRATION_DEBITCREDIT
    RWT_REFUND
End Enum
Public Enum ADDRESS
    FWU
    CORP
    RAIL
End Enum
Public Enum MODULENAME
    AGENCYDETAILS
    AGENCYSEARCH
    DISCOUNT_D
    DISCOUNT_I
    EXECDETAILS
    MARKUP_D
    MARKUP_I
    MARKUP_H
    SALESEXECACTIVE
    SERVICETAX
End Enum
Public Enum MODULETYPE
    ACTIVE
    DELETE
    EXPORT
    INSERT
    PASSWORD
    UPDATE
End Enum
Public Enum SMS
    AIRBOOKINGDOM
    AIRBOOKINGINTL
    AIRBOOKINGHOLD
    BUSBOOKING
    BUSCANCEL
    HOTELBOOKING
    HOTELCANCEL
    IMPORT
    PROXY
    EMULATE
    UPLOADCREDIT
End Enum