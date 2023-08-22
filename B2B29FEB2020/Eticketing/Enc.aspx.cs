using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Eticketing_Enc_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string RandomNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        string ReferenceNo = "FR" + RandomNo.Substring(7, 13); //15 digit random  

        string str1 = "323332666366646332653262343736374E051A242D1FC1DDD5A4F3BCEF583C6E556B9464BBB368C52C180DE3555DA30490DB2AA7B2A9C5F07493E16DFF4C963B811AA9707C6B98955208BBB0E4C9BE2962F2FFCE6AEA544FA7F0FE196EC98F6B1703781CF98BABA4F0BF4795B2D7A77358043EC8F74F1838F207170E505A52D4E246F901AA11D5C5D1790BD82F5B4DC2CA56BF9D09D8B952036F80AB052578BDF0E9AE48B4584498F07976727A9BA3345425E712A9E315D5623BC32828398C0728625FE400085BE17DC21069CA4D561576B40D18DC24761F1C0C4DFCE855EA036F6FA71F91022C89BEA54E15C357B677ADC1B25E19D7F669973827D4DFDF589D9C0D607C18BCA9B29AE8055F5CF2136F0489BBD0E60EED439CBA64E49B519614580F4B5626E1248D78D8C2CC699D4098";
        string str2 = str1.Substring(32);


        string hexKey = Convert.ToString(ConfigurationManager.AppSettings["EncrypteRDSKEY"]);
        string str3 = hexKey + str2;
        //string str2 = str1.Substring(32); 
        //StartClient(string Host, string token, int userID, int port)
        StartClient("", "", 0, 0);



        string plainText = "157E25B601345071AE830CD57CC0D525CCD46906E5701FE45159F40D2745AF44";
        string dd = "";//ComputeSHA256("");
      //sha256_hash(plainText);

        string password=null;
      // dd= DecryptTTT(plainText,password);
//        key :- abfb7c8d48dfc4f1ce7ed92a44989f25 (Length of 32)
//IV    :- 232fcfdc2e2b4767 (Length of 16)

          string textp = @"merchantCode=IR_CRIS|reservationId=1100012345|txnAmount=458.00|currencyType=INR|appCode=BOOKING|pymtMode=Internet|txnDate=20111206|securityId=CRIS|RU=https://localhost:8080/eticketing/BankResponse|checkSum=1EF7C9AA4C9CF912C1539AE332E997E15019E40010A59363064C9C308EA138";
                byte[] key =  Encoding.ASCII.GetBytes("abfb7c8d48dfc4f1ce7ed92a44989f25");

           //  byte[] aa =   EncryptStringToBytes_Aes(textp, key);


           //string ddr=  DecryptStringFromBytes_Aes(aa, key);
           //  string encr = BitConverter.ToString(aa).Replace("-", "");


           //byte[] Eaa = StringToByteArray(encr);
           //string ddr1 = DecryptStringFromBytes_Aes(Eaa, key);



        //string text = "merchantCode=IR_CRIS|reservationId=1100012345|txnAmount=458.00|currencyType=INR|appCode=BOOKING|pymtMode=Internet|txnDate=20111206|securityId=CRIS|RU=https://localhost:8080/eticketing/BankResponse|checkSum=1EF7C9AA4C9CF912C1539AE332E997E15019E40010A59363064C9C308EA13873";
        ////string key = "abfb7c8d48dfc4f1ce7ed92a44989f25";
        //string iv = "232fcfdc2e2b4767";

        //var encrypted = BytesToHex(
        //    SimpleEncrypt(
        //        new RijndaelManaged(),
        //        CipherMode.CBC,
        //        HexToBytes(key),
        //        HexToBytes(iv),
        //        Encoding.UTF8.GetBytes(text)));

        //var decrypted = Encoding.UTF8.GetString(
        //    SimpleDecrypt(
        //        new RijndaelManaged(),
        //        CipherMode.CBC,
        //        HexToBytes(key),
        //        HexToBytes(iv),
        //        HexToBytes(encrypted))).TrimEnd('\0');

        //Main();

    }

    //public static void StartClient(string Host, string token, int userID, int port)
    public static void StartClient(string Host, string token, int userID, int port)
    {


        Host = "mytrip.mercurytravels.in";
        port = 82;
        string Datamsg = @"GET Hotel/FetchCityList?CityName=Delhi&Type=City";
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];
        try
        {

            //  uses port 80.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostEntry(Host).HostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());
                string isConnect = "Socket connected to {0}" + sender.RemoteEndPoint.ToString();

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes(Datamsg);

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.
                int bytesRec = sender.Receive(bytes);


                string res = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }







    public static byte[] HexToBytes(string str, string separator = " ")
    {
        separator = null;
        if (str == null)
        {
            throw new ArgumentNullException();
        }

        if (separator == null)
        {
            separator = string.Empty;
        }

        if (str == string.Empty)
        {
            return new byte[0];
        }

        int stride = 2 + separator.Length;

        if ((str.Length + separator.Length) % stride != 0)
        {
            throw new FormatException();
        }

        var bytes = new byte[(str.Length + separator.Length) / stride];

        for (int i = 0, j = 0; i < str.Length; i += stride)
        {
            bytes[j] = byte.Parse(str.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            j++;

            // There is no separator at the end!
            if (j != bytes.Length && separator != string.Empty)
            {
                if (string.CompareOrdinal(str, i + 2, separator, 0, separator.Length) != 0)
                {
                    throw new FormatException();
                }
            }
        }

        return bytes;
    }

    public static string BytesToHex(byte[] bytes, string separator = " ")
    {
        if (bytes == null)
        {
            throw new ArgumentNullException();
        }

        if (separator == null)
        {
            separator = string.Empty;
        }

        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder((bytes.Length * (2 + separator.Length)) - 1);

        for (int i = 0; i < bytes.Length; i++)
        {
            if (i != 0)
            {
                sb.Append(separator);
            }

            sb.Append(bytes[i].ToString("x2"));
        }

        return sb.ToString();
    }

    public static byte[] SimpleEncrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
    {
        algorithm.Mode = cipherMode;
        algorithm.Padding = PaddingMode.Zeros;
        algorithm.Key = key;
        algorithm.IV = iv;

        using (var encryptor = algorithm.CreateEncryptor())
        {
            return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        }
    }

    public static byte[] SimpleDecrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
    {
        algorithm.Mode = cipherMode;
        algorithm.Padding = PaddingMode.Zeros;
        algorithm.Key = key;
        algorithm.IV = iv;

        using (var encryptor = algorithm.CreateDecryptor())
        {
            return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        }
    }

    //#region 256
    ////public static void Main()
    ////{
    ////    try
    ////    {

    ////        string original = "Here is some data to encrypt!";

    ////        // Create a new instance of the Aes 
    ////        // class.  This generates a new key and initialization  
    ////        // vector (IV). 
    ////        using (var random = new RNGCryptoServiceProvider())
    ////        {
    ////            var key = new byte[16];
    ////            //random.GetBytes(key);

    ////            // Encrypt the string to an array of bytes. 
    ////            byte[] encrypted = EncryptStringToBytes_Aes(original, key);

    ////            // Decrypt the bytes to a string. 
    ////            //string roundtrip = DecryptStringFromBytes_Aes(encrypted, key);
    ////            string cipherText = "D5620509499F1C240C7FBBDDA4D4C1C22EB36C2C5C3D74B0AC919C18AD3A13EEBC1A968F9DC8EEB45F16BC59D256D02A7A48724E8291C015FCBA0BF3974AB3BD0515B648EC60A0DA893DBBA2C2935CFBBBD2E94ED4C5E0B2D094DF3861DB3903F5300EBFD0F2D502FCEE25F55DE66272CE50302585BD00CBBF66A390A623AF579830FA93549C5170CDDF64098E89A65C442C40E0FE10B711FCFA324AD3BF3CEAC166E51997D084A6DAB66E57A2A3B4843A4A2546183EDBAD7F5C40DF0B105013010FDF70E9C794D71A34C3C6C6F9BA965286208EEA5F92D041DE90CE9D677BB8356C583529151F647B1B380C68466212840F147A314485FD009239D6401AFC7F90B22686919BAECFDB44BD852AE1A189";

    ////            // encrypted = Convert.FromBase64String(cipherText);


    ////            //byte[] ByteIV = Encoding.UTF8.GetBytes(IV);
    ////            string EncryptionKey = "abfb7c8d48dfc4f1ce7ed92a44989f25";
    ////            key = Encoding.UTF8.GetBytes(EncryptionKey);

    ////            byte[] cipherBytes = Convert.FromBase64String(cipherText);

    ////            // byte[] BytesKey = Encoding.UTF8.GetBytes(password);
    ////            string IV = "232fcfdc2e2b4767";
    ////            byte[] ByteIV = Encoding.UTF8.GetBytes(IV);



    ////            string roundtrip = DecryptStringFromBytes_Aes(encrypted, key);

    ////            //Display the original data and the decrypted data.
    ////            Console.WriteLine("Original:   {0}", original);
    ////            Console.WriteLine("Encrypted (b64-encode): {0}", Convert.ToBase64String(encrypted));
    ////            Console.WriteLine("Round Trip: {0}", roundtrip);
    ////            Console.ReadKey();
    ////        }

    ////    }
    ////    catch (Exception e)
    ////    {
    ////        Console.WriteLine("Error: {0}", e.Message);
    ////    }
    ////}

    //public static void Main()
    //{
    //    try
    //    {

    //        //string original = "Devesh";

    //        string original = "merchantCode=IR_CRIS|reservationId=1100012345|txnAmount=458.00|currencyType=INR|appCode=BOOKING|pymtMode=Internet|txnDate=20111206|securityId=CRIS|RU=https://localhost:8080/eticketing/BankResponse|checkSum=1EF7C9AA4C9CF912C1539AE332E997E15019E40010A59363064C9C308EA13873";

    //        // Create a new instance of the Aes 
    //        // class.  This generates a new key and initialization  
    //        // vector (IV). 
    //        using (var random = new RNGCryptoServiceProvider())
    //        {
    //            var key = new byte[16];
    //            //random.GetBytes(key);
    //            string EncryptionKey = "abfb7c8d48dfc4f1ce7ed92a44989f25";
    //            byte[] BytesKey = Encoding.UTF8.GetBytes(EncryptionKey);
    //            key = BytesKey;

    //            // Encrypt the string to an array of bytes. 
    //            byte[] encrypted = EncryptStringToBytes_Aes(original, key);



    //            string srtEn = "D5620509499F1C240C7FBBDDA4D4C1C22EB36C2C5C3D74B0AC919C18AD3A13EEBC1A968F9DC8EEB45F16BC59D256D02A7A48724E8291C015FCBA0BF3974AB3BD0515B648EC60A0DA893DBBA2C2935CFBBBD2E94ED4C5E0B2D094DF3861DB3903F5300EBFD0F2D502FCEE25F55DE66272CE50302585BD00CBBF66A390A623AF579830FA93549C5170CDDF64098E89A65C442C40E0FE10B711FCFA324AD3BF3CEAC166E51997D084A6DAB66E57A2A3B4843A4A2546183EDBAD7F5C40DF0B105013010FDF70E9C794D71A34C3C6C6F9BA965286208EEA5F92D041DE90CE9D677BB8356C583529151F647B1B380C68466212840F147A314485FD009239D6401AFC7F90B22686919BAECFDB44BD852AE1A189";
    //            encrypted = Encoding.UTF8.GetBytes(srtEn);




    //            // Decrypt the bytes to a string. 
    //            string roundtrip = DecryptStringFromBytes_Aes(encrypted, key);

    //            //Display the original data and the decrypted data.
    //            Console.WriteLine("Original:   {0}", original);
    //            Console.WriteLine("Encrypted (b64-encode): {0}", Convert.ToBase64String(encrypted));
    //            Console.WriteLine("Round Trip: {0}", roundtrip);
    //            //Console.ReadKey();
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine("Error: {0}", e.Message);
    //    }
    //}

    //public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key)
    //{
    //    byte[] encrypted;
    //    byte[] IV;

    //    string plaintext1 = "";

    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = Key;

    //        aesAlg.GenerateIV();
    //        IV = aesAlg.IV;

    //        aesAlg.Mode = CipherMode.CBC;

    //        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

    //        // Create the streams used for encryption. 
    //        using (var msEncrypt = new MemoryStream())
    //        {
    //            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (var swEncrypt = new StreamWriter(csEncrypt))
    //                {
    //                    //Write all data to the stream.
    //                    swEncrypt.Write(plainText);
    //                    plaintext1 = swEncrypt.ToString();
    //                }
    //                encrypted = msEncrypt.ToArray();
    //            }
    //        }
    //    }

    //    var combinedIvCt = new byte[IV.Length + encrypted.Length];
    //    Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
    //    Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

    //    // Return the encrypted bytes from the memory stream. 
    //    return combinedIvCt;

    //}

    ////
    //public static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] Key)
    //{

    //    // Declare the string used to hold 
    //    // the decrypted text. 
    //    string plaintext = "";

    //    // Create an Aes object 
    //    // with the specified key and IV. 

    //    //byte[] BytesKey = Encoding.UTF8.GetBytes(password);
    //    //byte[] BytesIV = Encoding.UTF8.GetBytes(password);
    //    using (Aes aesAlg = Aes.Create())
    //    {
    //        aesAlg.Key = Key;

    //        byte[] IV = new byte[aesAlg.BlockSize / 8];
    //        byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

    //        Array.Copy(cipherTextCombined, IV, IV.Length);
    //        Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

    //        aesAlg.IV = IV;

    //        aesAlg.Mode = CipherMode.CBC;

    //        // Create a decrytor to perform the stream transform.
    //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

    //        // Create the streams used for decryption. 
    //        using (var msDecrypt = new MemoryStream(cipherText))
    //        {
    //            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //            {
    //                using (var srDecrypt = new StreamReader(csDecrypt))
    //                {

    //                    // Read the decrypted bytes from the decrypting stream
    //                    // and place them in a string.
    //                    plaintext = srDecrypt.ReadToEnd();
    //                }
    //            }
    //        }

    //    }

    //    return plaintext;

    //}
    ////
    //#endregion

    static string ComputeSHA256(string plainText)
    {
        plainText = "157E25B601345071AE830CD57CC0D525CCD46906E5701FE45159F40D2745AF44";
        SHA256Managed sha256Managed = new SHA256Managed();
        Encoding u16LE = Encoding.Unicode;
        string hash = String.Empty;
        byte[] hashed = sha256Managed.ComputeHash(u16LE.GetBytes(plainText), 0, u16LE.GetByteCount(plainText));
        return Convert.ToBase64String(hashed);
    }



    public static String sha256_hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }


    public string EncryptData(string strData, string strKey)
    {
        byte[] key = { }; //Encryption Key   
        byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        byte[] inputByteArray;

        try
        {
            key = Encoding.UTF8.GetBytes(strKey);
            // DESCryptoServiceProvider is a cryptography class defind in c#.  
            DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
            inputByteArray = Encoding.UTF8.GetBytes(strData);
            MemoryStream Objmst = new MemoryStream();
            CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            Objcs.Write(inputByteArray, 0, inputByteArray.Length);
            Objcs.FlushFinalBlock();

            return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public string DecryptData(string strData, string strKey)
    {
        byte[] key = { };// Key   
        byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        byte[] inputByteArray = new byte[strData.Length];

        try
        {
            key = Encoding.UTF8.GetBytes(strKey);
            DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strData);

            MemoryStream Objmst = new MemoryStream();
            CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            Objcs.Write(inputByteArray, 0, inputByteArray.Length);
            Objcs.FlushFinalBlock();

            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(Objmst.ToArray());
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }



  
    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key)
    {
        byte[] encrypted;
        byte[] IV;


        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;

            aesAlg.GenerateIV();
            IV = aesAlg.IV;

            aesAlg.Mode = CipherMode.CBC;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption. 
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        var combinedIvCt = new byte[IV.Length + encrypted.Length];
        Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
        Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

        // Return the encrypted bytes from the memory stream. 
        return combinedIvCt;

    }

    static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] Key)
    {

        // Declare the string used to hold 
        // the decrypted text. 
        string plaintext = null;

        // Create an Aes object 
        // with the specified key and IV. 
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;

            byte[] IV = new byte[aesAlg.BlockSize / 8];
            byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

            Array.Copy(cipherTextCombined, IV, IV.Length);
            Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

            aesAlg.IV = IV;

            aesAlg.Mode = CipherMode.CBC;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption. 
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;

    }

    public static string DecryptTTT(string encryptedText, string password)
    {
        if (encryptedText == null)
        {
            return null;
        }

        if (password == null)
        {
            password = String.Empty;
        }

        // Get the bytes of the string
        var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

        return Encoding.UTF8.GetString(bytesDecrypted);
    }



    private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
}