Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Data

Public Class FareQuote
    Inherits System.Web.UI.Page
    Public XML
    Function FareQuoteRequest(ByVal FareXML_Request)
        XML = FareXML_Request
        Return Fare_Detail(CheckFareResponse())
    End Function

    Function CheckFareResponse() As String
        Dim FareResponse

        'Dim url = "http://directnettest.navitaire.com:8090/servlet/rpcrouter" 'test

        Dim url = System.Configuration.ConfigurationManager.AppSettings("6EURL") 'live

        Dim bytes As Byte() = Encoding.UTF8.GetBytes(XML)
        Dim strResult As String = String.Empty
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)

        request.Method = "POST"
        request.ContentLength = bytes.Length
        request.ContentType = "text/xml"
        request.KeepAlive = False

        Try
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(bytes, 0, bytes.Length)
            End Using

            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

                If response.StatusCode <> HttpStatusCode.OK Then
                    Dim message As String = [String].Format("POST failed. Received HTTP {0}", response.StatusCode)
                    Throw New ApplicationException(message)
                Else
                    Dim reader As StreamReader = Nothing
                    Dim responseStream As Stream = response.GetResponseStream()
                    reader = New StreamReader(responseStream)
                    strResult = reader.ReadToEnd()
                    response.Close()
                    responseStream.Close()
                    reader.Close()
                End If

            End Using

            FareResponse = strResult

        Catch ex As WebException
            Dim t = ex.Response()

            FareResponse = "Error"

        End Try
        If FareResponse = "Error" Then
            FareResponse = CheckFareResponse()
        End If

        Return FareResponse

    End Function

    Function Fare_Detail(ByVal FareQuote_Resp)

        Dim Aircomponent
        Dim pax_faredetail


        Dim readerDoc As New XmlDocument
        readerDoc.InnerXml = FareQuote_Resp
        Dim AvailabilityGroup As XmlNodeList = readerDoc.GetElementsByTagName("AirComponents")
        Dim Available As XmlNode
        Available = AvailabilityGroup(0)
        Dim Flights = "<xml>" & Available.InnerXml() & "</xml>"
        Dim fnode, fnode_B As XmlNode

        Dim RString = "<item xsi:type=" & Chr(34) & "ns1:AirComponent" & Chr(34) & " xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34) & ">"
        Dim RWString = "<item xsi:type=" & Chr(34) & "ns1:AirComponent" & Chr(34) & ">"

        Aircomponent = Replace("<AirComponents xsi:type='ns2:Vector'>" & Available.InnerXml & "</AirComponents>", RString, RWString)

        RString = "<item xsi:type=" & Chr(34) & "ns1:AirComponent" & Chr(34) & " xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34) & ">"

        ' Dim RString = "<item xsi:type='ns1:AirComponent' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>"

        Flights = Replace(Flights, RString, "<test>")
        Flights += "</test>"
        Dim sd = Split(Flights, "<test>")

        Dim i, j


        For i = 1 To UBound(sd)
            Dim strin1 = Split(sd(i), "</SelectedFareBasisCode>")
            Dim SelectedFareBasisCode = "<xml xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>" & strin1(0) & "</SelectedFareBasisCode></xml>"

            Dim strin2 = Split(strin1(1), "</Fares>")
            Dim FareDetail = "<xml>" & strin2(0) & "</Fares></xml>"
            FareDetail = Replace(FareDetail, "Vector", "Vector" & Chr(34) & " xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance")

            Dim fareList = New XmlDocument
            fareList.LoadXml(FareDetail)

            For Each fnode In fareList

                Dim BasicFare = fnode.ChildNodes(0).InnerText
                Dim Split_Val = "<item xsi:type=" & Chr(34) & "ns1:PassengerFare" & Chr(34) & " xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34) & ">"
                Dim fdetail = Split(fnode.ChildNodes(0).InnerXml, Split_Val)
                For j = 1 To UBound(fdetail)

                    Dim Pax_Fare_Detail = "<item  xmlns:xsi=" & Chr(34) & "http://www.w3.org/2001/XMLSchema-instance" & Chr(34) & ">" & fdetail(j)
                    Dim FareLag = New XmlDocument
                    FareLag.Loadxml(Pax_Fare_Detail)

                    For Each fnode_B In FareLag

                        pax_faredetail += fnode_B.ChildNodes(0).ChildNodes(0).InnerText + "," '     TotalAmount
                        'Session("Total_amount") = fnode_B.ChildNodes(0).ChildNodes(0).InnerText
                        pax_faredetail += fnode_B.ChildNodes(1).ChildNodes(0).InnerText + "," '     BaseAmount
                        'Session("BaseAmount") = fnode_B.ChildNodes(1).ChildNodes(0).InnerText
                        pax_faredetail += fnode_B.ChildNodes(2).InnerText + "," '   PassengerTypeCode
                        pax_faredetail += fnode_B.ChildNodes(3).InnerText + "," '   NumPaxTypes

                        Dim TaxDoc As New XmlDocument
                        TaxDoc.InnerXml = "<test>" & fnode_B.InnerXml & "</test>"
                        Dim Tnode, TaxNode As XmlNode
                        Dim TaxList As XmlNodeList = TaxDoc.GetElementsByTagName("Taxes")
                        Dim tax = ""

                        For Each Tnode In TaxList
                            Dim TaxDDoc As New XmlDocument
                            TaxDDoc.InnerXml = "<test>" & Tnode.InnerXml & "</test>"
                            Dim TaxDList As XmlNodeList = TaxDDoc.GetElementsByTagName("item")
                            For Each TaxNode In TaxDList
                                'tax += TaxNode.ChildNodes(0).InnerText & ";"
                                tax += TaxNode.ChildNodes(1).ChildNodes(0).InnerText & ","
                                'tax += TaxNode.ChildNodes(2).InnerText & "#"
                            Next
                        Next

                        pax_faredetail += tax + "$"

                    Next
                Next
            Next
        Next

        Return pax_faredetail & "@" & Aircomponent
    End Function

End Class
