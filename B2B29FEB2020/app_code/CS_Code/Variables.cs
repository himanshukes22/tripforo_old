using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Variables
/// </summary>
public class Variables
{
	public  Variables()
	{
	   
	}

    public static string Constr { get { return ConfigurationManager.ConnectionStrings["myAmdDB"].ConnectionString; } }
}