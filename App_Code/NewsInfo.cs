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

public class NewsInfo: ConnectSql
{
    public NewsInfo()
    { }


    protected internal DataSet GetAllNewsInfo()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_NewsInfo";
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
    //------------------------------------------------------
    protected internal DataSet Get_NewsInfo_By_ID(int siteId, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_NewsInfo_By_ID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@id", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteId;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
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
    //-----------------------------------------------------
    protected void addNewsInfo(int siteid, int contid,string newstitle,string newscontent,string newsfile,bool newsshare,int pos,bool newstate)
    { 
         try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_NewsInfo";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@NewsTitle", SqlDbType.VarChar, 50, "NewsTitle");
                cmd.Parameters.Add("@NewsContent", SqlDbType.VarChar, 20000, "NewsContent");
                cmd.Parameters.Add("@NewsFile", SqlDbType.VarChar, 50, "NewsFile");
                cmd.Parameters.Add("@NewsShare", SqlDbType.Bit);
                cmd.Parameters.Add("@NewsOrdPos", SqlDbType.Int);
                cmd.Parameters.Add("@NewsState", SqlDbType.Bit);

                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = newstitle;
                cmd.Parameters[3].Value = newscontent;
                cmd.Parameters[4].Value = newsfile;
                cmd.Parameters[5].Value = newsshare;
                cmd.Parameters[6].Value = pos;
                cmd.Parameters[7].Value = newstate;
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
    //--------------------------------------------------------------------------------
    protected void Upd_NewsInfo(int siteid, int contid,int id, string newstitle, string newscontent, string newsfile, bool newsshare, int pos, bool newstate)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_NewsInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@NewsId", SqlDbType.Int);
            cmd.Parameters.Add("@NewsTitle", SqlDbType.VarChar, 50, "NewsTitle");
            cmd.Parameters.Add("@NewsContent", SqlDbType.VarChar, 20000, "NewsContent");
            cmd.Parameters.Add("@NewsFile", SqlDbType.VarChar, 50, "NewsFile");
            cmd.Parameters.Add("@NewsShare", SqlDbType.Bit);
            cmd.Parameters.Add("@NewsOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@NewsState", SqlDbType.Bit);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
            cmd.Parameters[3].Value = newstitle;
            cmd.Parameters[4].Value = newscontent;
            cmd.Parameters[5].Value = newsfile;
            cmd.Parameters[6].Value = newsshare;
            cmd.Parameters[7].Value = pos;
            cmd.Parameters[8].Value = newstate;
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
    //-------------------------------------------------------------------------------
    protected string deleteNewsInfo(int id)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_NewsInfo_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@NewsId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = id;
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


}
