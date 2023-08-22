using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
public partial class SprReports_Hotel_CheckErrorLogs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void btnAllfile_Click(object sender, EventArgs e)
    {
        string path = txtExtention.Text;
        try
        {
            if (Directory.Exists(path))
            {
                string[] array2 = Directory.GetFiles(path, "*");

                string strFileName = "";
                foreach (string dir in array2)
                {
                    strFileName += dir + "\n";
                }
                txtDirectoryfiles.Text = strFileName;
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            } 
        }
        catch (Exception ex)
        { }
}

    protected void btnShowtext_Click(object sender, EventArgs e)
    {
        try
        {
            string path = txtpath.Text;
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    string text = System.IO.File.ReadAllText(path);
                    string[] lines = System.IO.File.ReadAllLines(path);

                    StringBuilder strbuild = new StringBuilder();
                    foreach (string s in lines)
                    {
                        strbuild.Append(s);
                        strbuild.AppendLine();
                    }
                    txtFileData.Text = strbuild.ToString();
                }

            }
        }
        catch (Exception ex)
        { }
    }
   
}