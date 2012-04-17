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
/// Summary description for Gen_X
/// </summary>
public class Gen_X:ConnectSql
{
	public Gen_X()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected internal int Add_Gen_x(int siteid,int contid,string gentitle, string genecontent, string image, int pos, int GeneTypeId)
    {
        int id = 0;
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Generic_X";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@TempId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneXTitle", SqlDbType.VarChar, 50, "GeneXTitle");
            cmd.Parameters.Add("@GeneXContent", SqlDbType.VarChar, 20000, "GeneXContent");
            cmd.Parameters.Add("@GeneXImage", SqlDbType.VarChar, 250, "GeneXImage");
            cmd.Parameters.Add("@GeneOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = GeneTypeId;
            cmd.Parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null; ;
            cmd.Parameters[4].Value = 6;
            cmd.Parameters[5].Value = gentitle;
            cmd.Parameters[6].Value = genecontent;
            cmd.Parameters[7].Value = image;
            cmd.Parameters[8].Value = pos;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { id = Convert.ToInt32(dr["id"].ToString()); }
            }
            dr.Close();
            dr.Dispose();
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
        return id;
    }
    //------------------------------------------------------------------
    protected internal DataSet Get_Site_Generic_X(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Generic_X";
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
    //------------------------------------------------------
    protected internal DataSet Get_GenericX_By_Id(int id)
    {
        try
        {
            Open(); DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GenericX_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
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
    //---------------------------------------------------------------------
    protected internal DataSet Get_Generic_X_By_Type(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_X_By_Type";
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
    //-----------------------------------------------------
    protected internal string Del_GeneX(int siteid,int contid,int geneid,int id)
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
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneXId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = geneid;
            cmd.Parameters[3].Value = id;
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
    //-------------------------------------------------------------------
    protected internal void Upd_GenX(int geneid, string gentitle, string genecontent, string image)
    {
        int id = 0;
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenX";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@GeneX", SqlDbType.Int);
            cmd.Parameters.Add("@GeneXTitle", SqlDbType.VarChar, 50, "GeneXTitle");
            cmd.Parameters.Add("@GeneXContent", SqlDbType.VarChar, 20000, "GeneXContent");
            cmd.Parameters.Add("@GeneXImage", SqlDbType.VarChar, 250, "GeneXImage");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = geneid;
            cmd.Parameters[1].Value = gentitle;
            cmd.Parameters[2].Value = genecontent;
            cmd.Parameters[3].Value = image;
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

    protected internal void Upd_GenX_By_GeneId(int geneid, string gentitle, string genecontent, string image)
    {
        int id = 0;
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenX_By_GeneId";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@GeneX", SqlDbType.Int);
            cmd.Parameters.Add("@GeneXTitle", SqlDbType.VarChar, 50, "GeneXTitle");
            cmd.Parameters.Add("@GeneXContent", SqlDbType.VarChar, 20000, "GeneXContent");
            cmd.Parameters.Add("@GeneXImage", SqlDbType.VarChar, 250, "GeneXImage");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = geneid;
            cmd.Parameters[1].Value = gentitle;
            cmd.Parameters[2].Value = genecontent;
            cmd.Parameters[3].Value = image;
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

}
