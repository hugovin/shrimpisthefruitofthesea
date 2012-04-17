using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;

/// <summary>
/// Class of Connection.
/// </summary>
public class ConnectSql : System.Web.UI.Page
{
    //Get the connectionstring from the webconfig and declare a global SqlConnection "SqlConnection"    
    protected static string connectionString = ConfigurationManager.AppSettings["conStringSQL"];
    protected SqlConnection SqlConn;

    protected ConnectSql()
    {
        //SqlConnection.ClearAllPools();
        SqlConn = new SqlConnection(connectionString);
    }
    ~ConnectSql()
    {
        try
        {
            if (SqlConn.State == ConnectionState.Open)
            {
                SqlConn.Close();
            }
            SqlConn.Dispose();
        }
        catch { }

    }

    protected void Open()
    {
        if (SqlConn.State == ConnectionState.Open)
        {
            SqlConn.Close();
        }
        SqlConn.Open(); 
    }

    protected void Close()
    {
        if (SqlConn.State == ConnectionState.Open)
            SqlConn.Close();
        //SqlConn.Dispose();
    }
    
    internal SqlConnection DataBase
    {
        get { return SqlConn; }
    }
}
