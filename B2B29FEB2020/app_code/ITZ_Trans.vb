Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Configuration

Public Class Itz_Trans_Dal
    
    Public Sub Itz_Trans_Dal()
        
    End Sub

    Public Function InsertItzTrans(ByVal objItzTrans As ITZ_Trans) As Boolean
        Dim inserted As Boolean = False
        Dim rows As Integer = 0
        Dim conStr As String
        Dim conn As New SqlConnection()
        Try
            conStr = ConfigurationManager.ConnectionStrings("myAmdDB").ConnectionString
            conn.ConnectionString = conStr
            Dim cmd As New SqlCommand("USP_INSERT_ITZ_TRANS", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ORDERID", objItzTrans.ORDERID)
            cmd.Parameters.AddWithValue("@AMT_TO_DED", objItzTrans.AMT_TO_DED)
            cmd.Parameters.AddWithValue("@AMT_TO_CRE", objItzTrans.AMT_TO_CRE)
            cmd.Parameters.AddWithValue("@COMMI_ITZ", objItzTrans.COMMI_ITZ)
            cmd.Parameters.AddWithValue("@CONV_FEE_ITZ", objItzTrans.CONVFEE_ITZ)
            cmd.Parameters.AddWithValue("@EASY_ORDID_ITZ", objItzTrans.EASY_ORDID_ITZ)
            cmd.Parameters.AddWithValue("@EASY_TRANCODE_ITZ", objItzTrans.EASY_TRANCODE_ITZ)
            cmd.Parameters.AddWithValue("@DECODE_ITZ", objItzTrans.DECODE_ITZ)
            cmd.Parameters.AddWithValue("@B2C_MBLNO_ITZ", objItzTrans.B2C_MBLNO_ITZ)
            cmd.Parameters.AddWithValue("@RATE_GROUP_ITZ", objItzTrans.RATE_GROUP_ITZ)
            cmd.Parameters.AddWithValue("@SERIAL_NO_FROM", objItzTrans.SERIAL_NO_FROM)
            cmd.Parameters.AddWithValue("@SERIAL_NO_TO", objItzTrans.SERIAL_NO_TO)
            cmd.Parameters.AddWithValue("@SVC_TAX_ITZ", objItzTrans.SVC_TAX_ITZ)
            cmd.Parameters.AddWithValue("@TDS_ITZ", objItzTrans.TDS_ITZ)
            cmd.Parameters.AddWithValue("@TTL_AMT_DED_ITZ", objItzTrans.TOTAL_AMT_DED_ITZ)
            cmd.Parameters.AddWithValue("@USER_NAME_ITZ", objItzTrans.USER_NAME_ITZ)
            cmd.Parameters.AddWithValue("@REFUND_TYPE_ITZ", objItzTrans.REFUND_TYPE_ITZ.Trim())
            cmd.Parameters.AddWithValue("@TRANS_TYPE", objItzTrans.TRANS_TYPE)
            cmd.Parameters.AddWithValue("@AVAIL_BAL_ITZ", objItzTrans.AVAIL_BAL_ITZ)
            cmd.Parameters.AddWithValue("@ACCTYPE_NAME_ITZ", objItzTrans.ACCTYPE_NAME_ITZ)
            cmd.Parameters.AddWithValue("@MESSAGE_ITZ", objItzTrans.MESSAGE_ITZ)
            cmd.Parameters.AddWithValue("@MERCHANT_KEY_ITZ", objItzTrans.MERCHANT_KEY_ITZ)
            cmd.Parameters.AddWithValue("@ERROR_CODE", objItzTrans.ERROR_CODE)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            rows = cmd.ExecuteNonQuery()
            If rows > 0 Then
                inserted = True
            End If
            cmd.Dispose()
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
        Return inserted
    End Function

End Class

Public Class ITZ_Trans
    Public Property ORDERID As String
    Public Property AMT_TO_DED As String
    Public Property AMT_TO_CRE As String
    Public Property COMMI_ITZ As String
    Public Property CONVFEE_ITZ As String
    Public Property EASY_ORDID_ITZ As String
    Public Property EASY_TRANCODE_ITZ As String
    Public Property DECODE_ITZ As String
    Public Property B2C_MBLNO_ITZ As String
    Public Property RATE_GROUP_ITZ As String
    Public Property SERIAL_NO_FROM As String
    Public Property SERIAL_NO_TO As String
    Public Property SVC_TAX_ITZ As String
    Public Property TDS_ITZ As String
    Public Property TOTAL_AMT_DED_ITZ As String
    Public Property USER_NAME_ITZ As String
    Public Property REFUND_TYPE_ITZ As String
    Public Property TRANS_TYPE As String
    Public Property AVAIL_BAL_ITZ As String
    Public Property ACCTYPE_NAME_ITZ As String
    Public Property MESSAGE_ITZ As String
    Public Property MERCHANT_KEY_ITZ As String
    Public Property ERROR_CODE As String
End Class
