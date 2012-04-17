using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FreeTools
/// </summary>
public class FreeTools : ConnectSql
{
	public FreeTools()
	{
	}
    //-------------------------------------------------
    protected DataSet Get_Site_FreeTools(int siteid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_FreeTools";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return dataset;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return null;
        }
    }
    //-------------------------------------------------
    protected void Add_FreeTools_Info(int siteid, string title, string subtitle, string content, string title2, string subtitle2,
        string content2, string title3, string subtitle3, string content3)
    {
         try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenA";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@FreeTitle1", SqlDbType.VarChar, 250, "FreeTitle1");
            cmd.Parameters.Add("@FreeSubTitle1", SqlDbType.VarChar, 250, "FreeSubTitle1");
            cmd.Parameters.Add("@FreeContent1", SqlDbType.VarChar, 20000, "FreeContent1");
            cmd.Parameters.Add("@FreeTitle2", SqlDbType.VarChar, 250, "FreeTitle2");
            cmd.Parameters.Add("@FreeSubTitle2", SqlDbType.VarChar, 250, "FreeSubTitle2");
            cmd.Parameters.Add("@FreeContent2", SqlDbType.VarChar, 20000, "FreeContent2");
            cmd.Parameters.Add("@FreeTitle3", SqlDbType.VarChar, 250, "FreeTitle3");
            cmd.Parameters.Add("@FreeSubTitle3", SqlDbType.VarChar, 250, "FreeSubTitle3");
            cmd.Parameters.Add("@FreeContent3", SqlDbType.VarChar, 20000, "FreeContent3");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = title;
            cmd.Parameters[2].Value = subtitle;
            cmd.Parameters[3].Value = content;
            cmd.Parameters[1].Value = title2;
            cmd.Parameters[2].Value = subtitle2;
            cmd.Parameters[3].Value = content2;
            cmd.Parameters[1].Value = title3;
            cmd.Parameters[2].Value = subtitle3;
            cmd.Parameters[3].Value = content3;


            cmd.ExecuteNonQuery();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            //return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            //return null;
        }


    }
    //-------------------------------------------------
    protected void Upd_FreeTools_Info(int siteid, string title, string subtitle, string content, string title2, string subtitle2,
    string content2, string title3, string subtitle3, string content3)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_FreeTools_Info ";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@FreeTitle1", SqlDbType.VarChar, 250, "FreeTitle1");
            cmd.Parameters.Add("@FreeSubTitle1", SqlDbType.VarChar, 250, "FreeSubTitle1");
            cmd.Parameters.Add("@FreeContent1", SqlDbType.VarChar, 20000, "FreeContent1");
            cmd.Parameters.Add("@FreeTitle2", SqlDbType.VarChar, 250, "FreeTitle2");
            cmd.Parameters.Add("@FreeSubTitle2", SqlDbType.VarChar, 250, "FreeSubTitle2");
            cmd.Parameters.Add("@FreeContent2", SqlDbType.VarChar, 20000, "FreeContent2");
            cmd.Parameters.Add("@FreeTitle3", SqlDbType.VarChar, 250, "FreeTitle3");
            cmd.Parameters.Add("@FreeSubTitle3", SqlDbType.VarChar, 250, "FreeSubTitle3");
            cmd.Parameters.Add("@FreeContent3", SqlDbType.VarChar, 20000, "FreeContent3");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = title;
            cmd.Parameters[2].Value = subtitle;
            cmd.Parameters[3].Value = content;
            cmd.Parameters[4].Value = title2;
            cmd.Parameters[5].Value = subtitle2;
            cmd.Parameters[6].Value = content2;
            cmd.Parameters[7].Value = title3;
            cmd.Parameters[8].Value = subtitle3;
            cmd.Parameters[9].Value = content3;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            //return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            //return null;
        }


    }
    //-------------------------------------------------
    public DataSet add_TitleResourceViewer(int TitleResourceID)
    {
        try
        {
            int SiteId = 0;
            if (HttpContext.Current.Session["SiteId"] != null)
            {
                SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            }
            else
            {
                SiteId = Convert.ToInt32(Global.globalSiteId);
            }

            Login login = new Login();
            int loginId = login.getLoginId();
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Add_TitleResourceViewer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginID", SqlDbType.Int);
            cmd.Parameters.Add("@TitleResourceID", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = loginId;
            cmd.Parameters[1].Value = TitleResourceID;
            cmd.Parameters[2].Value = SiteId;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return dataset;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return null;
        }
    }

}
