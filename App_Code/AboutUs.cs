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
/// Summary description for AboutUs
/// </summary>
public class AboutUs : ConnectSql
{
    public int AboutId;
    public int SiteId;
    public int AbouOrdPos;
    public string AbouTitle;
    public string AbouSubTitle;
    public string AbouImage;
    public string AbouImageAlt;
    public string AbouLink;
    public string AbouLink2;
    public string AbouContent;
    public bool AbouState;

    #region constructor
    public AboutUs(int aboutid, int siteid,int aboutord,string abouttitle,string abouSub,string aboutImage,string aboutImageAlt,string aboutlink,string aboutlink2,string aboutcontent, bool aboutstate)
    {
        this.AboutId = aboutid;
        this.SiteId = siteid;
        this.AbouOrdPos = aboutord;
        this.AbouTitle = abouttitle;
        this.AbouSubTitle = abouSub;
        this.AbouImage = aboutImage;
        this.AbouImageAlt = aboutImageAlt;
        this.AbouLink = aboutlink;
        this.AbouLink2 = aboutlink2;
        this.AbouContent = aboutcontent;
        this.AbouState = aboutstate;
    }

    ~AboutUs()
    {

    }

	public AboutUs()
	{

    }
    #endregion

    #region Querys
    //-----------------------------------------------
    protected DataSet getAllAboutUS(int siteId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_AboutUS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteId;
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
    //--------------------------------------------------
    protected DataSet getAboutUS_by_ID(int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "get_AboutUs_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = id;
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
    //--------------------------------------------------
    protected void addAboutUs(AboutUs about)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "add_New_About_US";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AbouOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@AbouTitle", SqlDbType.VarChar,250,"AbouTile");
            cmd.Parameters.Add("@AbouSubTitle", SqlDbType.VarChar,250,"AbouSubTitle");
            cmd.Parameters.Add("@AbouImage", SqlDbType.VarChar,250,"AbouImage");
            cmd.Parameters.Add("@AbouImageAlt", SqlDbType.VarChar,150,"AbouImageAlt");
            cmd.Parameters.Add("@AbouLink", SqlDbType.VarChar,250,"AbouLink");
            cmd.Parameters.Add("@AboutLink2", SqlDbType.VarChar,250,"AboutLink2");
            cmd.Parameters.Add("@AbouContent", SqlDbType.VarChar,20000,"AbouContent");
            cmd.Parameters.Add("@AbouState", SqlDbType.Bit);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = about.SiteId;
            cmd.Parameters[1].Value = about.AbouOrdPos;
            cmd.Parameters[2].Value = about.AbouTitle;
            cmd.Parameters[3].Value = about.AbouSubTitle;
            cmd.Parameters[4].Value = about.AbouImage;
            cmd.Parameters[5].Value = about.AbouImageAlt;
            cmd.Parameters[6].Value = about.AbouLink;
            cmd.Parameters[7].Value = about.AbouLink2;
            cmd.Parameters[8].Value = about.AbouContent;
            cmd.Parameters[9].Value = about.AbouState;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);

        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);

        }
    }
    //--------------------------------------------------
    protected string updateAboutUs(AboutUs about)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_AboutUs";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@AbouId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AbouOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@AbouTitle", SqlDbType.VarChar, 250, "AbouTile");
            cmd.Parameters.Add("@AbouSubTitle", SqlDbType.VarChar, 250, "AbouSubTitle");
            cmd.Parameters.Add("@AbouImage", SqlDbType.VarChar, 250, "AbouImage");
            cmd.Parameters.Add("@AbouImageAlt", SqlDbType.VarChar, 150, "AbouImageAlt");
            cmd.Parameters.Add("@AbouLink", SqlDbType.VarChar, 250, "AbouLink");
            cmd.Parameters.Add("@AboutLink2", SqlDbType.VarChar, 250, "AboutLink2");
            cmd.Parameters.Add("@AbouContent", SqlDbType.VarChar, 20000, "AbouContent");
            cmd.Parameters.Add("@AbouState", SqlDbType.Bit);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = about.AboutId;
            cmd.Parameters[1].Value = about.SiteId;
            cmd.Parameters[2].Value = about.AbouOrdPos;
            cmd.Parameters[3].Value = about.AbouTitle;
            cmd.Parameters[4].Value = about.AbouSubTitle;
            cmd.Parameters[5].Value = about.AbouImage;
            cmd.Parameters[6].Value = about.AbouImageAlt;
            cmd.Parameters[7].Value = about.AbouLink;
            cmd.Parameters[8].Value = about.AbouLink2;
            cmd.Parameters[9].Value = about.AbouContent;
            cmd.Parameters[10].Value = about.AbouState;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { ids = dr["id"].ToString(); }
            }
            dr.Close();
            dr.Dispose();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);

        }
        return ids;
    }
    #endregion
}
