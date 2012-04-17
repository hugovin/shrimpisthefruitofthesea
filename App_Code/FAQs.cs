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
/// Summary description for FAQs
/// </summary>
public class FAQs : ConnectSql
{
	public FAQs()
	{
	}

    //----------------------------------------------------------------------------------------
    protected internal DataSet getAllFAQs(int siteid,int contid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_FAQs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
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
    protected internal DataSet Get_FAQ_By_ID(int siteId, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_FAQ_By_Id";
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
    protected void addFaq(int siteid, int contid, string Faqtitle, string Faqcontent, int pos, bool Faqtate)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_New_FAQ";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@FaqTitle", SqlDbType.VarChar, 50, "FaqTitle");
            cmd.Parameters.Add("@FaqContent", SqlDbType.VarChar, 20000, "FaqContent");
            cmd.Parameters.Add("@FaqOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@FaqState", SqlDbType.Bit);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = Faqtitle;
            cmd.Parameters[3].Value = Faqcontent;
            cmd.Parameters[4].Value = pos;
            cmd.Parameters[5].Value = Faqtate;
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
    //--------------------------------------------------------------------------------
    protected void Upd_Faq(int siteid, int contid, int id, string Faqtitle, string Faqcontent, int pos, bool Faqtate)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Faq";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@FaqId", SqlDbType.Int);
            cmd.Parameters.Add("@FaqTitle", SqlDbType.VarChar, 50, "FaqTitle");
            cmd.Parameters.Add("@FaqContent", SqlDbType.VarChar, 20000, "FaqContent");
            cmd.Parameters.Add("@FaqOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@FaqState", SqlDbType.Bit);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
            cmd.Parameters[3].Value = Faqtitle;
            cmd.Parameters[4].Value = Faqcontent;
            cmd.Parameters[5].Value = pos;
            cmd.Parameters[6].Value = Faqtate;
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
    protected string deleteFaq(int id)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Faq_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@FaqId", SqlDbType.Int);
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
