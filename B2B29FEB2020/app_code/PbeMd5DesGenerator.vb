Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Namespace RedCorona.Cryptography
    Public Class PKCSKeyGenerator
        Private m_key As Byte() = New Byte(7) {}, m_iv As Byte() = New Byte(7) {}
        Private des As New DESCryptoServiceProvider()

        Public ReadOnly Property Key() As Byte()
            Get
                Return m_key
            End Get
        End Property
        Public ReadOnly Property IV() As Byte()
            Get
                Return IV
            End Get
        End Property
        Public ReadOnly Property Encryptor() As ICryptoTransform
            Get
                Return des.CreateEncryptor(m_key, m_iv)
            End Get
        End Property

        Public Sub New()
        End Sub
        Public Sub New(keystring As [String], salt As Byte(), md5iterations As Integer, segments As Integer)
            Generate(keystring, salt, md5iterations, segments)
        End Sub

        Public Function Generate(keystring As [String], salt As Byte(), md5iterations As Integer, segments As Integer) As ICryptoTransform
            Dim HASHLENGTH As Integer = 16
            'MD5 bytes
            Dim keymaterial As Byte() = New Byte(HASHLENGTH * segments - 1) {}
            'to store contatenated Mi hashed results
            ' --- get secret password bytes ----
            Dim psbytes As Byte()
            psbytes = Encoding.UTF8.GetBytes(keystring)

            ' --- contatenate salt and pswd bytes into fixed data array ---
            Dim data00 As Byte() = New Byte(psbytes.Length + (salt.Length - 1)) {}
            Array.Copy(psbytes, data00, psbytes.Length)
            'copy the pswd bytes
            Array.Copy(salt, 0, data00, psbytes.Length, salt.Length)
            'concatenate the salt bytes
            ' ---- do multi-hashing and contatenate results  D1, D2 ...  into keymaterial bytes ----
            Dim md5 As MD5 = New MD5CryptoServiceProvider()
            Dim result As Byte() = Nothing
            Dim hashtarget As Byte() = New Byte(HASHLENGTH + (data00.Length - 1)) {}
            'fixed length initial hashtarget
            For j As Integer = 0 To segments - 1
                ' ----  Now hash consecutively for md5iterations times ------
                If j = 0 Then
                    result = data00
                Else
                    'initialize
                    Array.Copy(result, hashtarget, result.Length)
                    Array.Copy(data00, 0, hashtarget, result.Length, data00.Length)
                    result = hashtarget
                End If

                For i As Integer = 0 To md5iterations - 1
                    result = md5.ComputeHash(result)
                Next

                'contatenate to keymaterial
                Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length)
            Next

            Array.Copy(keymaterial, 0, m_key, 0, 8)
            Array.Copy(keymaterial, 8, m_iv, 0, 8)

            Return Encryptor
        End Function


        Public Function EncryptString(plainText As String, ByVal keysalt As String, ByVal pwd As String) As String
            ' Instantiate a new RijndaelManaged object to perform string symmetric encryption
            Dim rijndaelCipher As New RijndaelManaged()

            ' Set key and IV
            rijndaelCipher.Key = Convert.FromBase64String(keysalt)
            rijndaelCipher.IV = Convert.FromBase64String(pwd)

            ' Instantiate a new MemoryStream object to contain the encrypted bytes
            Dim memoryStream As New MemoryStream()

            ' Instantiate a new encryptor from our RijndaelManaged object
            Dim rijndaelEncryptor As ICryptoTransform = rijndaelCipher.CreateEncryptor()

            ' Instantiate a new CryptoStream object to process the data and write it to the 
            ' memory stream
            Dim cryptoStream As New CryptoStream(memoryStream, rijndaelEncryptor, CryptoStreamMode.Write)

            ' Convert the plainText string into a byte array
            Dim plainBytes As Byte() = Encoding.ASCII.GetBytes(plainText)

            ' Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length)

            ' Complete the encryption process
            cryptoStream.FlushFinalBlock()

            ' Convert the encrypted data from a MemoryStream to a byte array
            Dim cipherBytes As Byte() = memoryStream.ToArray()

            ' Close both the MemoryStream and the CryptoStream
            memoryStream.Close()
            cryptoStream.Close()

            ' Convert the encrypted byte array to a base64 encoded string
            Dim cipherText As String = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length)

            ' Return the encrypted data as a string
            Return cipherText
        End Function




    End Class
End Namespace





'Imports Microsoft.VisualBasic
'Imports System.IO
'Imports System.Text
'Imports System.Security.Cryptography

'Public Class PbeMd5DesGenerator

'End Class



'Namespace RedCorona.Cryptography
'    Public Class PKCSKeyGenerator

'        Private m_key As Byte() = New Byte(7) {}, m_iv As Byte() = New Byte(7) {}
'        Private des As New DESCryptoServiceProvider()


'        Public ReadOnly Property Key() As Byte()
'            Get
'                Return m_key
'            End Get
'        End Property
'        Public ReadOnly Property IV() As Byte()
'            Get
'                Return m_iv
'            End Get
'        End Property
'        Public ReadOnly Property Encryptor() As ICryptoTransform
'            Get
'                Return des.CreateEncryptor(m_key, m_iv)
'            End Get
'        End Property

'        Public Sub New()
'        End Sub
'        Public Sub New(keystring As [String], salt As Byte(), md5iterations As Integer, segments As Integer)
'            Generate(keystring, salt, md5iterations, segments)
'        End Sub

'        Public Function Generate(keystring As [String], salt As Byte(), md5iterations As Integer, segments As Integer) As ICryptoTransform
'            Dim HASHLENGTH As Integer = 16
'            'MD5 bytes
'            Dim keymaterial As Byte() = New Byte(HASHLENGTH * segments - 1) {}
'            'to store concatenated Mi hashed results
'            ' --- get secret password bytes ----
'            Dim psbytes As Byte()
'            psbytes = Encoding.UTF8.GetBytes(keystring)

'            ' --- concatenate salt and pswd bytes into fixed data array ---
'            Dim data00 As Byte() = New Byte(psbytes.Length + (salt.Length - 1)) {}
'            Array.Copy(psbytes, data00, psbytes.Length)
'            'copy the pswd bytes
'            Array.Copy(salt, 0, data00, psbytes.Length, salt.Length)
'            'concatenate the salt bytes
'            ' ---- do multi-hashing and concatenate results  D1, D2 ...  
'            ' into keymaterial bytes ----
'            Dim md5 As MD5 = New MD5CryptoServiceProvider()
'            Dim result As Byte() = Nothing
'            Dim hashtarget As Byte() = New Byte(HASHLENGTH + (data00.Length - 1)) {}
'            'fixed length initial hashtarget
'            For j As Integer = 0 To segments - 1
'                ' ----  Now hash consecutively for md5iterations times ------
'                If j = 0 Then
'                    result = data00
'                Else
'                    'initialize
'                    Array.Copy(result, hashtarget, result.Length)
'                    Array.Copy(data00, 0, hashtarget, result.Length, data00.Length)
'                    result = hashtarget
'                End If

'                For i As Integer = 0 To md5iterations - 1
'                    result = md5.ComputeHash(result)
'                Next

'                'concatenate to keymaterial
'                Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length)
'            Next

'            Array.Copy(keymaterial, 0, m_key, 0, 8)
'            Array.Copy(keymaterial, 8, m_iv, 0, 8)

'            Return Encryptor
'        End Function
'    End Class
'End Namespace

