Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Collections.Generic
Public Class SendEmailTemp
    Private STDom As New SqlTransactionDom
    Private ST As New SqlTransaction
    Private Distr As New Distributor
    Dim objDataAcess As New DataAccess(ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString)
    Dim paramHashtable As New Hashtable
    Public Function GetPaymentUploadEmailTemp(ByVal UserId As String, ByVal Amount As String, ByVal PaymentMode As String, ByVal Remark As String) As String
        Dim AgencyDetails As New DataTable
        Dim CompanyDetails As New DataTable
        AgencyDetails = ST.GetAgencyDetails(UserId).Tables(0)
        CompanyDetails = GetCompanyDetails().Tables(0)
        Dim AgencyName As String = AgencyDetails.Rows(0)("Agency_Name").ToString
        Dim EmailBody As String

        Try
            EmailBody = "<div>"
            EmailBody = EmailBody & "<table cellpadding='10' cellspacing='0' style='margin:auto;width:600px;background-color:#f3f3f3;border:1px solid #e9e9e9;line-height:20px;font-family:Arial, Helvetica, sans - serif;font-size:   1rem;color:#000'>"
            EmailBody = EmailBody & "<tbody>"
            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255, 255, 255, 0.3);color:#fff;border-bottom:4px solid #2a9928e8'>"
            EmailBody = EmailBody & "<img src='" + CompanyDetails.Rows(0)("Logo").ToString + "' alt='TBO Logo' style='width:150px;'"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255,255,255,0.3);color:#0c0c0c;border-bottom:4px solid #151616f0;text-align:right'><b><b>24x7 Helpdesk:</b> (+91) " + CompanyDetails.Rows(0)("MOBILENO").ToString + "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td colspan='2' style='text-align:center;'>"
            EmailBody = EmailBody & "<h4 style='margin: -3px -3px 8px 176px;text-align: center;font-size: 13px;width: 32.5%;background: rgb(40 151 22 / 68%);color: #fff;padding: 4px;border-radius: 10px;'>Payment Upload Request</h4>"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & "<td colspan='2' style='background:#efedf8;padding:1rem 2rem;font-size:.9rem'>"
            EmailBody = EmailBody & " Dear," + AgencyName + "(" + UserId + ")"

            EmailBody = EmailBody & "<br><br> Payment Upload Request Sent Sucessfully"
            EmailBody = EmailBody & "<br><br> Total Amount: " + Amount
            EmailBody = EmailBody & "<br> Payment Mode : " + PaymentMode
            EmailBody = EmailBody & "<br> Remark:" + Remark

            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"
            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & " <td style='background:rgba(255, 255, 255, 0.3);color:#0c0c0c;text-align:Right();font-size:.9rem'>"
            EmailBody = EmailBody & " <b> Customer Service:</b> <br>" + CompanyDetails.Rows(0)("MOBILENO").ToString + "<small style='font-size:12px'>(10 AM to 7:00 PM)</small><br>"
            EmailBody = EmailBody & " <a href='#' style='color:  #0c0c0c' target='_blank'> " + CompanyDetails.Rows(0)("EMAIL").ToString + "</a>"

            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td></td>"
            EmailBody = EmailBody & " </tr>"
            EmailBody = EmailBody & " <tr>"
            EmailBody = EmailBody & "<td colspan='2'></td>"
            EmailBody = EmailBody & "  </tr>"
            EmailBody = EmailBody & " </tbody></table>"
            EmailBody = EmailBody & "</div>"
            Return EmailBody

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Function

    Public Function GetRefundCancelEmailTemp(ByVal UserId As String, ByVal fltTD As DataTable, ByVal caclestatus As String, ByVal paxdt() As DataRow, ByVal Sector As String) As String
        Dim AgencyDetails As New DataTable
        Dim CompanyDetails As New DataTable
        AgencyDetails = ST.GetAgencyDetails(UserId).Tables(0)
        CompanyDetails = GetCompanyDetails().Tables(0)
        Dim AgencyName As String = AgencyDetails.Rows(0)("Agency_Name").ToString
        Try

            Dim EmailBody As String
            EmailBody = "<div>"
            EmailBody = EmailBody & "<table cellpadding='10' cellspacing='0' style='margin:auto;width:600px;background-color:#f3f3f3;border:1px solid #e9e9e9;line-height:20px;font-family:Arial, Helvetica, sans - serif;font-size:   1rem;color:#000'>"
            EmailBody = EmailBody & "<tbody>"
            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255, 255, 255, 0.3);color:#fff;border-bottom:4px solid #2a9928e8'>"
            EmailBody = EmailBody & "<img src='" + CompanyDetails.Rows(0)("Logo").ToString + "' alt='TBO Logo' style='width:150px;' class='CToWUd'>"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255,255,255,0.3);color:#0c0c0c;border-bottom:4px solid #151616f0;text-align:right'><b><b>24x7 Helpdesk:</b> (+91) " + CompanyDetails.Rows(0)("MOBILENO").ToString + "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td colspan='2' style='text-align:center;'>"
            EmailBody = EmailBody & "<h4 style='margin: -3px -3px 8px 176px;text-align: center;font-size: 13px;width: 32.5%;background: rgb(40 151 22 / 68%);color: #fff;padding: 4px;border-radius: 10px;'>Cancellation/Refund Request</h4>"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & "<td colspan='2' style='background:#efedf8;padding:1rem 2rem;font-size:.9rem'>"
            EmailBody = EmailBody & " Dear," + fltTD.Rows(0).Item("AgencyName") + "(" + UserId + ")"
            If caclestatus = "Cancelled" Then
                EmailBody = EmailBody & "<br><br>Cancel Status Flight booking has been canceled successfully. The refund will process up To 7 working days."
            Else
                EmailBody = EmailBody & "<br><br> cancellation / Refund request Is submitted successfully. The refund will process up To 7 working days."
            End If
            EmailBody = EmailBody & "<br><br> PNR: " + Convert.ToString(fltTD.Rows(0).Item("AirlinePnr"))

            For j As Integer = 0 To paxdt.Length - 1
                EmailBody = EmailBody & "<br> Passenger Information :<br> " + paxdt(j).Item("Title").ToString + " " + paxdt(j).Item("FName") + " " + paxdt(j).Item("LName") + "" + (paxdt(j).Item("PaxType"))

            Next
            EmailBody = EmailBody & "<br> Flight Details:<br>" + Sector
            EmailBody = EmailBody & "<br> Total Payable :" + Convert.ToString(fltTD.Rows(0).Item("TotalFareAfterDis"))

            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"
            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & " <td style='background:rgba(255, 255, 255, 0.3);color:#0c0c0c;text-align:Right();font-size:.9rem'>"
            EmailBody = EmailBody & " <b> Customer Service:</b> <br>" + CompanyDetails.Rows(0)("MOBILENO").ToString + "<small style='font-size:12px'>(10 AM to 7:00 PM)</small><br>"
            EmailBody = EmailBody & " <a href='#' style='color:  #0c0c0c' target='_blank'>" + CompanyDetails.Rows(0)("EMAIL").ToString + "</a>"

            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td></td>"
            EmailBody = EmailBody & " </tr>"
            EmailBody = EmailBody & " <tr>"
            EmailBody = EmailBody & "<td colspan='2'></td>"
            EmailBody = EmailBody & "  </tr>"
            EmailBody = EmailBody & " </tbody></table>"
            EmailBody = EmailBody & "</div>"
            Return EmailBody

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Function

    Public Function GetEmptyEmailTemp(ByVal UserId As String) As String
        Dim AgencyDetails As New DataTable
        Dim CompanyDetails As New DataTable
        AgencyDetails = ST.GetAgencyDetails(UserId).Tables(0)
        CompanyDetails = GetCompanyDetails().Tables(0)
        Dim AgencyName As String = AgencyDetails.Rows(0)("Agency_Name").ToString
        Dim EmailBody As String

        Try
            EmailBody = "<div>"
            EmailBody = EmailBody & "<table cellpadding='10' cellspacing='0' style='margin:auto;width:600px;background-color:#f3f3f3;border:1px solid #e9e9e9;line-height:20px;font-family:Arial, Helvetica, sans - serif;font-size:   1rem;color:#000'>"
            EmailBody = EmailBody & "<tbody>"
            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255, 255, 255, 0.3);color:#fff;border-bottom:4px solid #2a9928e8'>"
            EmailBody = EmailBody & "<img src='" + CompanyDetails.Rows(0)("Logo").ToString + "' alt='TBO Logo' style='width:150px;'"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td style='background-color:rgba(255,255,255,0.3);color:#0c0c0c;border-bottom:4px solid #151616f0;text-align:right'><b><b>24x7 Helpdesk:</b> (+91) " + CompanyDetails.Rows(0)("MOBILENO").ToString + "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"
            EmailBody = EmailBody & "<td colspan='2' style='text-align:center;'>"
            EmailBody = EmailBody & "<h4 style='margin: -3px -3px 8px 176px;text-align: center;font-size: 13px;width: 32.5%;background: rgb(40 151 22 / 68%);color: #fff;padding: 4px;border-radius: 10px;'>#bodyheader</h4>"
            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"

            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & "<td colspan='2' style='background:#efedf8;padding:1rem 2rem;font-size:.9rem'>"
            EmailBody = EmailBody & " Dear," + AgencyName + "(" + UserId + ")"


            EmailBody = EmailBody & "<br><br>#body"


            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "</tr>"
            EmailBody = EmailBody & "<tr>"

            EmailBody = EmailBody & " <td style='background:rgba(255, 255, 255, 0.3);color:#0c0c0c;text-align:Right();font-size:.9rem'>"
            EmailBody = EmailBody & " <b> Customer Service:</b> <br>" + CompanyDetails.Rows(0)("MOBILENO").ToString + "<small style='font-size:12px'>(10 AM to 7:00 PM)</small><br>"
            EmailBody = EmailBody & " <a href='#' style='color:  #0c0c0c' target='_blank'> " + CompanyDetails.Rows(0)("EMAIL").ToString + "</a>"

            EmailBody = EmailBody & "</td>"
            EmailBody = EmailBody & "<td></td>"
            EmailBody = EmailBody & " </tr>"
            EmailBody = EmailBody & " <tr>"
            EmailBody = EmailBody & "<td colspan='2'></td>"
            EmailBody = EmailBody & "  </tr>"
            EmailBody = EmailBody & " </tbody></table>"
            EmailBody = EmailBody & "</div>"
            Return EmailBody

        Catch ex As Exception
            clsErrorLog.LogInfo(ex)

        End Try
    End Function

    Public Function GetCompanyDetails() As DataSet
        paramHashtable.Clear()
        Return objDataAcess.ExecuteData(Of Object)(paramHashtable, True, "GetCompanyDetails", 3)
    End Function
End Class
