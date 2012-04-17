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
/// Summary description for Generics
/// </summary>
public class Generics:ConnectSql
{
	public Generics()
	{
	}
    //-----------------------------------------------------
    public DataSet Get_All_GenericByType(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_GenericByType";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeid", SqlDbType.Int);
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
     //-----------------------------------------------------
    public DataSet Get_Site_All_GenericByType(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_All_GenericByType";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeid", SqlDbType.Int);
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
    //-----------------------------------------------------
    protected string Del_Generic_By_Id(int siteid,int contid,int geneid)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Generic_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
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
    //-----------------------------------------------------
    public DataSet Get_GeneDefault_By_Type(int genetypeid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GeneDefault_By_Type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = genetypeid;
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
    public DataSet Get_GeneDefault_By_Id(string _gentype)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GeneDefault_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = _gentype;
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
    public DataSet Get_Id_Generic(int siteid, int contid, int genDefaId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Id_Generic";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@genDefaId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = genDefaId;
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
    public DataSet Get_Generic_Name(int geneId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_Name";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = geneId;
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
    public DataSet Upd_GenericTitle(int geneId,string name)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Upd_GenericTitle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTitle", SqlDbType.VarChar, 50,"GeneTitle");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = geneId;
            cmd.Parameters[1].Value = name;
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
