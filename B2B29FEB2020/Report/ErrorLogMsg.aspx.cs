using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SprReports_ErrorLogMsg : System.Web.UI.Page
{
    DirectoryInfo di;
    string Path = ConfigurationManager.AppSettings["FilePath"].ToString();
    string date = "";
    string RootPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            date = DateTime.Now.ToString("dd-MM-yyyy");
            di = new DirectoryInfo(Path + date);
            DirList(di);
            if (DirList(di).Count > 0)
            {
                Repeater1.DataSource = DirList(di);
                Repeater1.DataBind();
            }
            else
            {
                ltr_textread.Text = "<table> <tr style='font-family:verdana; font-size:14px ;font-weight: bold; color: #FF0000;'><td>No record found!!!<td></tr></table>";
            }
        }
    }
    protected void ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ButtonEvent")
        {
            ltr_textread.Text = "";
            RootPath = e.CommandArgument.ToString();
            FileList(RootPath);
            if (FileList(RootPath).Count > 0)
            {
                CreateRootFile(RootPath);
                Repeater2.DataSource = FileList(RootPath);
                Repeater2.DataBind();
            }
            else
            {
                ltr_textread.Text = "<table> <tr style='font-family:verdana; font-size:14px ;font-weight: bold; color: #FF0000;'><td>No error found!!!<td></tr></table>";
            }
        }
        if (e.CommandName == "ChildEvent")
        {
            ltr_textread.Text = "";
            RootPath = e.CommandArgument.ToString();
            CreateLog(RootPath);
        }
    }
    public List<string> DirList(DirectoryInfo dir)
    {
        List<string> rt = new List<string>();
        string FullDateDir = "";
        FullDateDir = dir.Root.Name + dir.Parent;
        DirectoryInfo OrderIdDatePath = new DirectoryInfo(FullDateDir);
        if (OrderIdDatePath.Exists == true)
        {
            foreach (DirectoryInfo d in OrderIdDatePath.GetDirectories())
            {
                if (d.FullName.Contains("#"))
                {
                    string FileName = d.Name.Substring(0, d.Name.IndexOf("#"));
                    if (FileName == txt_date.Text.Trim().ToString())
                    {
                        foreach (DirectoryInfo f in d.GetDirectories())
                        {
                            rt.Add(f.Name + "_" + f.FullName);
                        }
                    }
                }
                else if (d.Exists == true && d.Name == txt_date.Text.Trim().ToString())
                {
                    foreach (DirectoryInfo p in d.GetDirectories())
                    {
                        rt.Add(p.Name + "_" + p.FullName);
                    }
                }
            }
        }
        return rt;
    }

    public List<string> FileList(string FilePath)
    {
        List<string> rt = new List<string>();
        DirectoryInfo FileDir = new DirectoryInfo(FilePath);
        foreach (FileInfo d in FileDir.GetFiles())
        {
            string LinkFile = d.Name.Remove(d.Name.Length - 4);
            rt.Add(LinkFile + "_" + d.FullName);
        }
        return rt;
    }

    public void CreateLog(string FilePath)
    {
        string FileName = "";
        string tables = null;
        FileName = FilePath.Substring(FilePath.LastIndexOf("\\"), (FilePath.Length) - FilePath.LastIndexOf("\\"));
        FileName = FileName.Substring(1);
        ltr_textread.Text += "<table> <tr style='font-family:verdana; font-size:12px ;font-weight: bold;color: #FF6600;'><td>Module Error Page :" + FileName.Trim() + "<td></tr></table>";
        using (StreamReader sr = File.OpenText(FilePath))
        {
            while ((tables = sr.ReadLine()) != null)
            {
                ltr_textread.Text += "\n" + tables;
            }
        }
    }
    public void CreateRootFile(string FilePath)
    {
        DirectoryInfo FileDir = new DirectoryInfo(FilePath);
        string tables = null;
        foreach (FileInfo d in FileDir.GetFiles("*"))
        {
            ltr_textread.Text += "<table> <tr style='font-family:verdana; font-size:12px ;font-weight: bold;color: #FF6600;'><td>Module Error Page " + d.Name.Trim().ToString() + " :<td></tr></table>";
            using (StreamReader sr = File.OpenText(FileDir + "\\" + d.Name))
            {
                while ((tables = sr.ReadLine()) != null)
                {
                    ltr_textread.Text += "\n" + tables;
                }
            }
        }
    }
    protected void txt_date_TextChanged(object sender, EventArgs e)
    {
        ltr_textread.Text = "";
        if (txt_OrderID.Text.Trim().ToString() == "")
        {
            date = txt_date.Text.Trim().ToString();
            di = new DirectoryInfo(Path + date);
            DirList(di);
            Repeater1.DataSource = DirList(di);
            Repeater1.DataBind();
        }
        txt_date.Text = "";
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        ltr_textread.Text = "";
        Repeater1.Controls.Clear();
        Repeater2.Controls.Clear();
        if (txt_OrderID.Text.Trim().ToString() != "")
        {
            di = new DirectoryInfo(Path);
            OrderId(di, txt_OrderID.Text.Trim().ToString());
        }
        txt_OrderID.Text = "";
    }
    public void OrderId(DirectoryInfo dir, string OrderID)
    {
        OrderID = "_" + OrderID;
        List<string> rt = new List<string>();

        if (dir.Exists == true)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (d.FullName.Contains("#"))
                {
                    foreach (DirectoryInfo f in d.GetDirectories())
                    {
                        if (f.Exists == true)
                        {
                            string tables = null;
                            string PathName = f.FullName.ToString();
                            DirectoryInfo file = new DirectoryInfo(PathName);
                            foreach (FileInfo p in file.GetFiles("*"))
                            {
                                ltr_textread.Text += "<table> <tr style='font-family:verdana; font-size:12px ;font-weight: bold;color: #FF6600;'><td>Order ID : " + OrderID.Substring(1) + " Error Module : " + f.Name + "<td></tr></table>";
                                using (StreamReader sr = File.OpenText(file + "\\" + p.Name))
                                {
                                    while ((tables = sr.ReadLine()) != null)
                                    {
                                        ltr_textread.Text += "\n" + tables;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}