using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sha256De : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] args =null;
        //Main(args);

        string value="";
        sha256_hash(value);
    }


  //public  static string ComputeSHA256(string plainText)
  //  {
  //      SHA256Managed sha256Managed = new SHA256Managed();
  //      Encoding u16LE = Encoding.Unicode;
  //      string hash = String.Empty;
  //      byte[] hashed = sha256Managed.ComputeHash(u16LE.GetBytes(plainText), 0, u16LE.GetByteCount(plainText));
  //      return Convert.ToBase64String(hashed);
  //  }


    //static void Main(string[] args)
    //{
    //    string hashed_password = "A312E15E76F64FE7276F1F743104E6DF5B9B6B82E5DEC4191AE5615B07E2B79E"; //"YOSGtSkJ41KX7K80FEmg+vme4ioLsp3qr28XU8nDQ9c=";
    //    int index;

    //    for (index = 0; index <= 9999; index++)
    //    {
    //        if (hashed_password.Equals(sha256_hash(index.ToString("0000"))))
    //            break;
    //    }
    //    string decr = index.ToString("0000");

    //    //Console.WriteLine("Password is: " + index.ToString("0000"));

    //    //Console.ReadLine();
    //}

    public static String sha256_hash(String value)
    {
        value = "A312E15E76F64FE7276F1F743104E6DF5B9B6B82E5DEC4191AE5615B07E2B79E";
        string mmm = "";

        try
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                value = "A312E15E76F64FE7276F1F743104E6DF5B9B6B82E5DEC4191AE5615B07E2B79E";
                mmm = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(value.ToCharArray())));

                
            }
        }
        catch(Exception ex)
        {

        }
        return mmm;
        
    }
}