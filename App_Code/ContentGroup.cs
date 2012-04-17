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
/// Summary description for ContentGroup
/// </summary>
public class ContentGroup:ConnectSql
{

    private int intContId;
    private int intContOrdPos;
    private string strConttitle;
    private string strContDescription;
    private bool blConState;

    public ContentGroup(int contid,int contordpos, string conttitle, string contdescrip, bool state)
    {
        this.intContId = contid;
        this.intContOrdPos = contordpos;
        this.strConttitle = conttitle;
        this.strContDescription = contdescrip;
        this.blConState = state;
    }

	public ContentGroup()
	{
	}
    //----------------------------------------------------
    protected void addContentGroup(int siteid,int posm, string title,string description, bool state)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_ContentGroup";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@ContTitle", SqlDbType.VarChar, 50, "ContTitle");
            cmd.Parameters.Add("@ContDescription", SqlDbType.VarChar, 50, "ContDescription");
            cmd.Parameters.Add("@ContState", SqlDbType.Bit);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = posm;
            cmd.Parameters[2].Value = title;
            cmd.Parameters[3].Value = description;
            cmd.Parameters[4].Value = state;
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
    //----------------------------------------------------
    protected internal DataSet getAllContentGroups(int SiteId)
    {
        Open();
        DataSet dataset = new DataSet();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DataBase;
        cmd.CommandText = "Get_All_ContentGroup";
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
    //----------------------------------------------------
    protected DataSet getContentGroupByID(int id)
    {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_ContentGroup_By_ID";
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
    //----------------------------------------------------
    protected string deleteTopNavigation(int contid)
    {
        string ids = "";
                try
        {
        Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DataBase;
        //Procedure Name.
        cmd.CommandText = "Del_ContentGroup";
        cmd.CommandType = CommandType.StoredProcedure;
        //Procedure Parameters.
        cmd.Parameters.Add("@ContId", SqlDbType.Int);
        //Setting values to Parameters.
        cmd.Parameters[0].Value = contid;

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            { ids = dr["id"].ToString(); }
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
        return ids;
    }
    //---------------------------------
    protected string updateContentGroup(int contid,int posm, string title, string description, bool state)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_ContentGroup";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@ContOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@ContTitle", SqlDbType.VarChar, 50, "ContTitle");
            cmd.Parameters.Add("@ContDescription", SqlDbType.VarChar, 500, "ContDescription");
            cmd.Parameters.Add("@ContState", SqlDbType.Bit);

            //Setting values to Parameters.
            cmd.Parameters[0].Value = contid;
            cmd.Parameters[1].Value = posm;
            cmd.Parameters[2].Value = title;
            cmd.Parameters[3].Value = description;
            cmd.Parameters[4].Value = state;
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
