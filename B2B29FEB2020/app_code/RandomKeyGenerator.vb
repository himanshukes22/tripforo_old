Option Strict On
Imports System.Text
Imports System.Security.Cryptography


Public Class RandomKeyGenerator
    Private Const LEN As Integer = 1
    Function Generate() As String
        Dim strRndKey As String = ""
        strRndKey = UsingGuid() & RNGCharacterMask() ' & "-" & UsingTicks() & "-" & UsingDateTime()
        Return strRndKey
    End Function

    Private Function UsingGuid() As String
        Dim result As String = Guid.NewGuid().ToString().GetHashCode().ToString("x")
        Return result
    End Function

    Private Function UsingTicks() As String
        Dim val As String = DateTime.Now.Ticks.ToString("x")
        Return val
    End Function

    Private Function RNGCharacterMask() As String
        Dim maxSize As Integer = 8
        Dim minSize As Integer = 8
        Dim chars As Char() = New Char(61) {}
        Dim a As String
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        chars = a.ToCharArray()
        Dim size As Integer = maxSize
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data)
        size = maxSize
        data = New Byte(size - 1) {}
        crypto.GetNonZeroBytes(data)
        Dim result As New StringBuilder(size)
        For Each b As Byte In data
            result.Append(chars(b Mod (chars.Length - 1)))
        Next
        Return result.ToString()
    End Function


    Private Function UsingDateTime() As String
        Return DateTime.Now.ToString().GetHashCode().ToString("x")
    End Function
End Class
