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
/// Summary description for Site
/// </summary>
public class Site:ConnectSql
{
    public int intSiteId;
    public string strSiteName;
    public string strSiteDesc;
    public string strSiteUrl;
    public string strPhone;
    public string strCopy ;
    public string strSiteKeyW;
    public bool blSiteStatus;

	public Site()
	{
	}
    //----------------------------------------------
    protected DataSet Get_MaxContentGroup(int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Max_Content_Group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
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
    //----------------------------------------------
    protected DataSet Get_UserCms_Site(string userId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_UserCms_Site";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50, "UserId");
            cmd.Parameters[0].Value = userId;
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
    //----------------------------------------------
    protected internal DataSet Get_Site_Information(int siteid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Information";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
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
    //----------------------------------------------
    public DataSet getSiteInfo()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Name_Id";
            cmd.CommandType = CommandType.StoredProcedure;
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
    //---------------------------------------------
    public DataSet Get_Site_Trials_Demos(int TitleId,int TitleResourceTypeID, int ContId, int SiteId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleResourceTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters[0].Value = TitleResourceTypeID;
            cmd.Parameters[1].Value = TitleId;
            cmd.Parameters[2].Value = ContId;
            cmd.Parameters[3].Value = SiteId;
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
    //---------------------------------------------
    public DataSet Get_Site_Trials_Demos_List(int TitleResourceTypeID, int ContId, int SiteId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleResourceTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters[0].Value = TitleResourceTypeID;
            cmd.Parameters[1].Value = ContId;
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
    //---------------------------------------------
    public DataSet Get_Site_Trials_Demos_List_Youtube(int SiteId, int ContId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos_List_Youtube";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters[0].Value = ContId;
            cmd.Parameters[1].Value = SiteId;
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
    //---------------------------------------------
    protected internal DataSet GetAllSiteContact(int SiteId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Contact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters[0].Value = SiteId;
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
    //---------------------------------------------
    protected internal DataSet Get_SiteContact(int siteId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Contact";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
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
    //----------------------------------------------
    protected void Upd_Site(int siteid, string sitetitle, string siteDescription,
                            string siteUrl, string sitePhone, string siteCopy, string sitetagLine,
                            string siteKey)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Site";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteName", SqlDbType.VarChar, 500, "SiteName");
            cmd.Parameters.Add("@SiteDescription", SqlDbType.VarChar, 500, "SiteDescription");
            cmd.Parameters.Add("@SiteURL", SqlDbType.VarChar, 500, "SiteURL");
            cmd.Parameters.Add("@SitePhone", SqlDbType.VarChar, 50, "SitePhone");
            cmd.Parameters.Add("@SiteCopy", SqlDbType.VarChar, 500, "SiteCopy");
            cmd.Parameters.Add("@SiteTagLine", SqlDbType.VarChar, 500, "SiteTagLine");
            cmd.Parameters.Add("@SiteKeyWords", SqlDbType.VarChar, 500, "SiteKeyWords");            
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = sitetitle;
            cmd.Parameters[2].Value = siteDescription;
            cmd.Parameters[3].Value = siteUrl;
            cmd.Parameters[4].Value = sitePhone;
            cmd.Parameters[5].Value = siteCopy;
            cmd.Parameters[6].Value = sitetagLine;
            cmd.Parameters[7].Value = siteKey;
      
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
    //---------------------------------------------
    protected internal DataSet GetAllSiteContactById(int SiteId,int sitecontid)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Contact_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContId", SqlDbType.Int);
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = sitecontid;
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
    //---------------------------------------------
    protected void Upd_SiteCont(int siteid, int sitecontid, string siteconttitle, string sitecontaddress,
                            string emailcus, string emailsal, string image)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_SiteContact";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContTitle", SqlDbType.VarChar, 250, "SiteContTitle");
            cmd.Parameters.Add("@SiteContAddress", SqlDbType.VarChar, 500, "SiteContAddress");
            cmd.Parameters.Add("@SiteContEmailCus", SqlDbType.VarChar, 150, "SiteContEmailCus");
            cmd.Parameters.Add("@SiteContEmailSal", SqlDbType.VarChar, 150, "SiteContEmailSal");
            cmd.Parameters.Add("@SiteContImage", SqlDbType.VarChar, 250, "SiteContImage");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = sitecontid;
            cmd.Parameters[2].Value = siteconttitle;
            cmd.Parameters[3].Value = sitecontaddress;
            cmd.Parameters[4].Value = emailcus;
            cmd.Parameters[5].Value = emailsal;
            cmd.Parameters[6].Value = image;
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
    //--------------------------------------------
    protected string delSiteContact(int siteid,int Sitecontid)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_SiteContact_By_Id ";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = Sitecontid;
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
    //--------------------------------------------
    protected void Add_SiteCont(int siteid, int contordpos, string siteconttitle, string sitecontaddress,
                            string emailcus, string emailsal, string image)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_SiteContact";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@SiteContTitle", SqlDbType.VarChar, 250, "SiteContTitle");
            cmd.Parameters.Add("@SiteContAddress", SqlDbType.VarChar, 500, "SiteContAddress");
            cmd.Parameters.Add("@SiteContEmailCus", SqlDbType.VarChar, 150, "SiteContEmailCus");
            cmd.Parameters.Add("@SiteContEmailSal", SqlDbType.VarChar, 150, "SiteContEmailSal");
            cmd.Parameters.Add("@SiteContImage", SqlDbType.VarChar, 250, "SiteContImage");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contordpos;
            cmd.Parameters[2].Value = siteconttitle;
            cmd.Parameters[3].Value = sitecontaddress;
            cmd.Parameters[4].Value = emailcus;
            cmd.Parameters[5].Value = emailsal;
            cmd.Parameters[6].Value = image;
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
    //--------------------------------------------
    protected DataSet getAdds(int siteid,int type)
    {
        Adds adds = new Adds();
        return adds.getAdds(siteid,1,type);

    }
    //-----------------------------------------------------------------------
    protected void Upd_Adds(int siteid, int addid, string addtitle,
                                string addImage, string addlink, string addalt, string addConte)
    {
        Adds adds = new Adds();
        adds.Upd_Adds(siteid, addid, addtitle, addImage, addlink, addalt, addConte);

    }
    //---------------------------------------------
    protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
    {
        Gen_X gen = new Gen_X();
        return gen.Add_Gen_x(siteid, contid, title, content, image, pos, genetypeId);
    }
    //--------------------------------------------------------------------
    protected DataSet Get_Adds_By_Id(int siteid, int AddsId)
    {
        Adds adds = new Adds();
        return adds.Get_Adds_By_Id(siteid, AddsId);
    }
    //------------------------------------------------------
    protected void addAdds(int siteid,int type,string image,string alt,string link)
    {
        Adds adds = new Adds();
       // adds.Add_Adds(siteid, type, image, alt, link);
    }
    //------------------------------------------------------
    public string getUserGUID() {
        Login login = new Login();
        if (login.getLoginGUID() != null)
        {
            return login.getLoginGUID().ToString();
        }
        else {
            return "";
        }
        
    }
    //----------------------------------------------------
    protected void Upd_TermOrPrivacy(int siteid, int type,int id)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_TermOrPrivacy";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@Type", SqlDbType.Int);
            cmd.Parameters.Add("@GeneXId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = type;
            cmd.Parameters[2].Value = id;
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

    }
    //----------------------------------------------------
    protected DataSet Get_GenericX_By_Id(int id)
    {
        Gen_X gen = new Gen_X();
        return gen.Get_GenericX_By_Id(id);
    }
    //-----------------------------------------------------
    protected void Upd_GenX(int geneid, string gentitle, string genecontent, string image)
    {
        Gen_X gen = new Gen_X();
        gen.Upd_GenX(geneid, gentitle, genecontent, image);
    }
}
