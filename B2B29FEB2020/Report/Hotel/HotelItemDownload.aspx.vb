Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.IO.File

Partial Class SprReports_Hotel_HotelItemDownload
    Inherits System.Web.UI.Page

    Protected Sub Downloads_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Downloads.Click
        'Dim SearchDetails As New HtlLibrary.Htl_Property
        'Dim GTADownlode As New HtlLibrary.GTA_ResponseXML
        'Try
        '    SearchDetails.Inventry = Request("douwnloadeType")
        '    If Request("douwnloadeType") = "rdbIncDownLoad" Or Request("douwnloadeType") = "" Then
        '        SearchDetails.CheckInDate = Request("From")
        '        SearchDetails.CheckOutDate = Request("To")
        '    End If
        '    Dim filedownloading As String = GTADownlode.ItemDownlode(SearchDetails)
        '    If filedownloading = "Downloaded" Then
        '        UnZipFile(ConfigurationManager.AppSettings("ExtractFilePath"), ConfigurationManager.AppSettings("ItemDownloadPath") & ".zip")
        '        downloadStatus.InnerText = "File Download Completed."
        '        'UnZipFile("D:\GTADownload", "D:\GTA_DATA_2013_01.rar")
        '    End If
        'Catch ex As Exception
        '    ' HtlLibrary.HtlLog.InsertLogDetails(ex)
        'End Try
    End Sub

    Protected Sub UnZipFile(ByVal filepath As String, ByVal ZipFilePath As String)
        Try
            Dim proc As New Process()
            proc.StartInfo.FileName = "C:\Program Files (x86)\WinRAR\WinRAR.EXE "
            proc.StartInfo.Arguments = " x -y " & ZipFilePath & " " & filepath   'D:\GTA_DATA_2013_01.zip D:\GTADownload"
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.Start()
            proc.WaitForExit()
        Catch ex As Exception
            ' HtlLibrary.HtlLog.InsertLogDetails(ex)
        End Try
    End Sub

    Private Sub UnRar(ByVal WorkingDirectory As String, ByVal filepath As String)
        Try
            ' Microsoft.Win32 and System.Diagnostics namespaces are imported
            Dim objRegKey As RegistryKey
            objRegKey = Registry.ClassesRoot.OpenSubKey("WinRAR\Shell\Open\Command")
            ' Windows 7 Registry entry for WinRAR Open Command
            Dim obj As Object = objRegKey.GetValue("")
            Dim objRarPath As String = obj.ToString()
            objRarPath = objRarPath.Substring(1, objRarPath.Length - 7)
            objRegKey.Close()
            Dim objArguments As String
            ' in the following format
            ' " X G:\Downloads\samplefile.rar G:\Downloads\sampleextractfolder\" 
            'objArguments = " X " & " " & filepath & " " + " " + WorkingDirectory
            objArguments = " X -y" & " " & filepath & " " + " " + WorkingDirectory
            Dim objStartInfo As New ProcessStartInfo()
            ' Set the UseShellExecute property of StartInfo object to FALSE
            ' Otherwise the we can get the following error message
            ' The Process object must have the UseShellExecute property set to false in order to use environment variables.
            objStartInfo.UseShellExecute = False
            objStartInfo.FileName = objRarPath
            objStartInfo.Arguments = objArguments
            'objStartInfo.Arguments = filepath
            objStartInfo.WindowStyle = ProcessWindowStyle.Hidden
            objStartInfo.WorkingDirectory = WorkingDirectory & "\"

            Dim objProcess As New Process()
            objProcess.StartInfo = objStartInfo
            objProcess.Start()

        Catch ex As Exception
            'at System.Diagnostics.Process.StartWithCreateProcess(ProcessStartInfo startInfo)
            'at(System.Diagnostics.Process.Start())
            'at SprReports_Hotel_HotelItemDownload.UnRar(String WorkingDirectory, String filepath) in D:\spring bakup 14-01-2013\GTAHOTEL_29Jan2013\SprReports\Hotel\HotelItemDownload.aspx.vb:line 52
        End Try
    End Sub
End Class
