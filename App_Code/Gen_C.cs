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
/// Summary description for Gen_C
/// </summary>
public class Gen_C : ConnectSql
{
	public Gen_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected internal void Add_generic_C(int siteid, int contid, int geneid, int geneType, int GeneDefaId,
                                          string geneTitle, string genectitle, string geneccontent, int pos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Generic_C";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneCTitle", SqlDbType.VarChar, 1000, "GenCBTitle");
            cmd.Parameters.Add("@GeneCContent", SqlDbType.VarChar, 20000, "GeneCContent");
            cmd.Parameters.Add("@GeneCOrdPos", SqlDbType.Int);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            if (contid != 0)
            {
                cmd.Parameters[1].Value = contid;
            }
            else
            {
                cmd.Parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            cmd.Parameters[2].Value = geneid;
            cmd.Parameters[3].Value = geneType;
            cmd.Parameters[4].Value = GeneDefaId;
            cmd.Parameters[5].Value = geneTitle;
            cmd.Parameters[6].Value = genectitle;
            cmd.Parameters[7].Value = geneccontent;
            cmd.Parameters[8].Value = pos;
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
    //------------------------------------------------------------------
    protected internal DataSet Get_Site_Generic_C(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Generic_C";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
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
    //------------------------------------------------------------------
    protected internal DataSet Get_Generic_C_By_Type(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_C_By_Type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = id;
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
    //------------------------------------------------------------------
    protected internal DataSet Get_GenericC_By_ID(int siteId,int GeneCId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GenericC_By_ID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneCId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteId;
            cmd.Parameters[1].Value = GeneCId;
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
    //------------------------------------------------------------------       
    protected internal DataSet Get_Generic_Resources_Faqs(int siteid, int contid, int gencid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_Resources_Faqs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);            
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = gencid;            
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
    //---------------------------------------------------------------------
    protected void Upd_GenericC(int siteid, int geneCid, string genectitle, string geneccontent,int genecpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Gen_C";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GenecId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneCTitle", SqlDbType.VarChar, 100, "GeneCTitle");
            cmd.Parameters.Add("@GeneCContent", SqlDbType.VarChar, 20000, "GeneCContent");
            cmd.Parameters.Add("@GeneCOrdPos", SqlDbType.Int);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = geneCid;
            cmd.Parameters[2].Value = genectitle;
            cmd.Parameters[3].Value = geneccontent;
            cmd.Parameters[4].Value = genecpos;
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
    //------------------------------------------------------------------
    protected internal string Delete_Generic_C(int GeneBId)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Generic_C_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@GeneCId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = GeneBId;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { ids = dr["id"].ToString(); }
            }
            dr.Close();
            dr.Dispose();
            return ids;
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
    //------------------------------------------------------------------
}
