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
/// Summary description for Adds
/// </summary>
public class Adds : ConnectSql
{
	public Adds()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //---------------------------------------------
    protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
    {
        Gen_X gen = new Gen_X();
        return gen.Add_Gen_x(siteid, contid, title, content, image, pos, genetypeId);
    }
    //--------------------------------------------------------------------
    protected internal DataSet GetAllAddsbyId(int SiteId, int AddsId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_AddsbyId";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsId", SqlDbType.Int);
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = AddsId;
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
    //--------------------------------------------------------------------
    protected internal DataSet getAdds(int siteid,int contid,int type)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Adds";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsType", SqlDbType.Int);
            cmd.Parameters[0].Value = siteid;
            if (contid != 0)
            {
                cmd.Parameters[1].Value = contid;
            }
            else
            {
                cmd.Parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }             
            cmd.Parameters[2].Value = type;            
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
    //--------------------------------------------
    protected internal DataSet Get_Adds_By_Id(int siteid,int AddsId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Adds_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsId ", SqlDbType.Int);
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = AddsId;
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
    protected internal void Upd_Adds(int siteid, int addid, string addtitle,
                            string addImage,string addlink, string addalt, string addConte)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Adds ";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsId", SqlDbType.VarChar, 250, "AddsId");
            cmd.Parameters.Add("@AddsTitle", SqlDbType.VarChar, 251, "AddsTitle");
            cmd.Parameters.Add("@AddsImage", SqlDbType.VarChar, 250, "AddsImage");
            cmd.Parameters.Add("@Addslink", SqlDbType.VarChar, 250, "AddsLink");
            cmd.Parameters.Add("@AddsAlt", SqlDbType.VarChar, 250, "AddsAlt");
            cmd.Parameters.Add("@AddsContent", SqlDbType.VarChar, 20000, "AddsContent");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = addid;
            cmd.Parameters[2].Value = addtitle;
            cmd.Parameters[3].Value = addImage;
            cmd.Parameters[4].Value = addlink;
            cmd.Parameters[5].Value = addalt;
            cmd.Parameters[6].Value = addConte;

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
    //-----------------------------------------------
    protected internal void Add_Adds(int siteid,int contid,int type,string addsTitle, string addImage, string addalt, string link)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Adds";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsType", SqlDbType.Int);
            cmd.Parameters.Add("@AddsTitle", SqlDbType.VarChar, 250, "AddsTitle");
            cmd.Parameters.Add("@AddsImage", SqlDbType.VarChar, 250, "AddsImage");
            cmd.Parameters.Add("@AddsAlt", SqlDbType.VarChar, 250, "AddsAlt");
            cmd.Parameters.Add("@AddsLink", SqlDbType.VarChar, 20000, "AddsLink");
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
            cmd.Parameters[2].Value = type;
            cmd.Parameters[3].Value = addsTitle;
            cmd.Parameters[4].Value = addImage;
            cmd.Parameters[5].Value = addalt;
            cmd.Parameters[6].Value = link;
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

    protected internal string Del_Adds(int siteid,int id)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Adds";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@AddsId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = id;
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
    //------------------------------------------------
    public DataSet Get_SideAdds(int siteid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideAdds";
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
    //------------------------------------------------

    protected internal void updLandPage(int siteid, string title, string content, string image)
    {
        Gen_X gen = new Gen_X();
        gen.Upd_GenX_By_GeneId(siteid, title, content, image);
    }
    //-----------------------------------------------
}
