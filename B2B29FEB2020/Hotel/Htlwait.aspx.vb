
Partial Class Htlwait
    Inherits System.Web.UI.Page

    Dim HtlSearchQuery As New Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim aa, ii As Integer
        Dim adult_per_rum As New ArrayList
        Dim child_per_rum As New ArrayList
        Dim adt As Integer = 0, chd As Integer = 0, TotalGuest As Integer = 0
        Try
            'Dim CheckInsDate() As String = Request("htlcheckin").Replace("/", "-").Split("-")
            'Dim CheckOutsDate() As String = Request("htlcheckout").Replace("/", "-").Split("-")
            'HtlSearchQuery.Add("CityDetails", Request("htlcitylist"))
            'HtlSearchQuery.Add("CheckIn", CheckInsDate(2) & "-" + CheckInsDate(1) & "-" + CheckInsDate(0))
            'HtlSearchQuery.Add("CheckOut", CheckOutsDate(2) & "-" + CheckOutsDate(1) & "-" + CheckOutsDate(0))
            HtlSearchQuery.Add("CityDetails", Request("htlcitylist"))
            HtlSearchQuery.Add("CheckIn", Request("htlcheckin"))
            HtlSearchQuery.Add("CheckOut", Request("htlcheckout"))
            HtlSearchQuery.Add("NoOfRooms", Request("rooms"))
            HtlSearchQuery.Add("Currency", "INR")
            Dim citydetail() As String = Request("htlcitylist").Split(",")
            HtlSearchQuery.Add("City", citydetail(0))
            HtlSearchQuery.Add("CountryName", citydetail(2))

            For aa = 0 To Request("rooms") - 1
                adult_per_rum.Add(Request("Room-" & aa & "-adult-total"))
                adt += Integer.Parse(Request("Room-" & aa & "-adult-total"))
                child_per_rum.Add(Request("Room-" & aa & "-child-total"))
                chd += Integer.Parse(Request("Room-" & aa & "-child-total"))
            Next
            TotalGuest = adt + chd
            HtlSearchQuery.Add("Adt_Per_Room", adult_per_rum)
            HtlSearchQuery.Add("Chd_Per_Room", child_per_rum)
            HtlSearchQuery.Add("TotAdt", adt.ToString)
            HtlSearchQuery.Add("TotChd", chd.ToString)
            HtlSearchQuery.Add("TotGuest", TotalGuest.ToString)
            Dim chld_age(Request("rooms"), 3) As Integer            'String   change by vipul

            Try
                For ii = 0 To Request("rooms") - 1
                    If child_per_rum(ii) = "" Then
                        child_per_rum(ii) = 0
                    End If
                    Dim agesArray1(3) As String
                    If child_per_rum(ii) <> 0 Then
                        For tot_chd = 0 To CInt(child_per_rum(ii)) - 1
                            If child_per_rum(0) = Nothing Then
                                Exit For
                            Else
                                agesArray1(tot_chd) = Request("Room-" & ii & "-child-" & tot_chd & "-age")
                                chld_age(ii, tot_chd) = CInt(agesArray1(tot_chd))
                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
                HotelDAL.HotelDA.InsertHotelErrorLog(ex, Session("UID").ToString())
            End Try

            HtlSearchQuery.Add("Chdage_Per_Room", chld_age)
            HtlSearchQuery.Add("htlname", Request("htlname"))
            HtlSearchQuery.Add("htlstar", Request("htlstar"))

        Catch ex As Exception
            HotelDAL.HotelDA.InsertHotelErrorLog(ex, Session("UID").ToString())
        End Try
        Session("HtlSearchQuery") = HtlSearchQuery
    End Sub

End Class
