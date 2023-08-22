Imports Microsoft.VisualBasic
Imports System.Data

Public Class SpiceIndigoUTL

    Public Function Check_Via_Connecting(ByVal Dt As DataTable, ByVal Flight As String, ByVal VC As String) As String
        ' Check if Multiple FlightID's Exsists for Particualr Flight (1,2)
        Dim FTYPE As String = ""
        Dim Arrival As String = ""
        Try

            Dim dt1 = Dt.Select("Flight='" & Flight & "'", "")
            Dim FID As String = dt1(0)("FlightIdentification").ToString()
            If dt1.Length > 1 Then

                For jj As Integer = 1 To dt1.Length - 1
                    If (FID = dt1(jj)("FlightIdentification").ToString()) Then
                        FTYPE = "Via"
                    Else
                        FTYPE = "Con"
                    End If

                Next

            Else
                Arrival = dt1(0)("ArrivalLocation").ToString()
            End If
            If (Not (FTYPE) = "") Then
                If (FTYPE = "Via" And VC = "6E") Then
                    Arrival = dt1(0)("ArrivalLocation").ToString()
                Else
                    'Arrival = dt1(dt1.Length - 1)("ArrivalLocation").ToString()
                End If
            End If
        Catch ex As Exception

        End Try
        Return Arrival
    End Function

End Class
