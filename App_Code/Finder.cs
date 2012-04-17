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

public class Finder : ConnectSql
{
    //----------------------------------------
    public Finder()
    {
    }
    //-------------------------------------------------
    protected internal DataSet getAllFinderCategory(int siteid,int contid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_FinderCategory";
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
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
    //----------------------------------------
    protected internal DataSet getAllFinderDefault(string siteid, string contid)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_FinderDefault";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@ContId ", SqlDbType.VarChar, 5, "ContId ");
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
    ////-----------------------------------------
    protected internal DataSet getAllFinderSubCategory(int siteid,int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_FinderSubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@catId", SqlDbType.Int);
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
    //------------------------------------------------------------------------
    protected internal DataSet getAllFinderSubDefault(int siteid, int contid, int Findid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_FinderSubDefault";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@Findid", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = Findid;
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
    //-------------------------------------------------------------------------
    protected internal void Add_FinderSubCategory(int siteid,int contid,int Findcateid, int Findsubcateid, int Findsubcateordpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_FinderSubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@FindCateId", SqlDbType.Int);
            cmd.Parameters.Add("@FindSubCateId", SqlDbType.Int);
            cmd.Parameters.Add("@FindSubCateOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = Findcateid;
            cmd.Parameters[3].Value = Findsubcateid;
            cmd.Parameters[4].Value = Findsubcateordpos;

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
    //-------------------------------------------------------------------------
    protected internal void Delete_FinderSubCategory()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Del_FinderSubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
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
    //-------------------------------------------------------------------------
    protected internal void Add_FinderCategory(int siteid, int contid, int FindCateid, string Findcatitle, int Findcateord, int Findcatestate)
    {
        Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DataBase;
        //Procedure Name.
        cmd.CommandText = "Add_FinderCategory";
        cmd.CommandType = CommandType.StoredProcedure;
        //Procedure Parameters.
        cmd.Parameters.Add("@SiteId", SqlDbType.Int);
        cmd.Parameters.Add("@ContId", SqlDbType.Int);
        cmd.Parameters.Add("@FindCateId", SqlDbType.Int);
        cmd.Parameters.Add("@FindCateTitle", SqlDbType.VarChar, 50, "FindCateTitle");
        cmd.Parameters.Add("@FindCateOrdPos", SqlDbType.Int);
        cmd.Parameters.Add("@FindCateState", SqlDbType.Int);
        //Setting values to Parameters.
        cmd.Parameters[0].Value = siteid;
        cmd.Parameters[1].Value = contid;
        cmd.Parameters[2].Value = FindCateid;
        cmd.Parameters[3].Value = Findcatitle;
        cmd.Parameters[4].Value = Findcateord;
        cmd.Parameters[5].Value = Findcatestate;

        cmd.ExecuteNonQuery();
        Close();

    }
    //-------------------------------------------------------------------------
    protected DataSet getSubDefaultById(int titleID)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_FeatProduct_By_ID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleID;
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
    //---------------------------------//---------------------------------
    protected DataSet getSubDefaultByTitle(int Findid, string title)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SubFinder_By_Title ";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FindId", SqlDbType.Int);
            cmd.Parameters.Add("@title", SqlDbType.VarChar, 150, "title");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = Findid;
            cmd.Parameters[1].Value = title;
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
    //-------------------------------------------------------------------------
    protected internal void Add_FinderSubDefaultCategory(int siteid,int contid,int Findcateid, int Findsubcateid, int Findsubcateordpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_FinderSubDefaCatItems";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@FindDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@FindDefaItemId", SqlDbType.Int);
            cmd.Parameters.Add("@FindDefaItemOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = Findcateid;
            cmd.Parameters[3].Value = Findsubcateid;
            cmd.Parameters[4].Value = Findsubcateordpos;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);

        }
        catch (Exception oEx)
        {

        }
    }
    //-------------------------------------------------------------------------
    protected internal void deleteSubDefault(int id)
    {
        try
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_FinderDefaultItem";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@DefaId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = id;
            string ids = "";
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

        }
    }
    //------------------------------------------------------------------------
    protected internal void Del_AllFinderCategory()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Del_AllFinderCategory ";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
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
    //------------------------------------------------------------------------
    protected internal void Upd_FinderDefault(int Finddefaid, bool truefalsese, bool truefalsedb)
    {
        try
        {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_FinderDefault";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@FindDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@TrueFalsese", SqlDbType.Int);
            cmd.Parameters.Add("@TrueFalsedb", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = Finddefaid;
            cmd.Parameters[1].Value = truefalsese;
            cmd.Parameters[2].Value = truefalsedb;
            string ids = "";
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

        }

    }
    //------------------------------------------------------------------------
    protected void DeleteALLSubcategory(int siteid,int contid, int id)
    {
        try
        {
            Open();

        SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_All_SubCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@FindCateId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
            string ids = "";
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

        }
 
    }



}
