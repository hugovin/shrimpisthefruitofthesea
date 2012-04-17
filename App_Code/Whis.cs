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
/// Summary description for Whis
/// </summary>
public class Whis : ConnectSql
{
	public Whis()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //-----------------------------------------------
    protected internal void Add_Whish_Header(string title, string titleText, int idSession, int statusWish, int deleteFlag, int SiteId, int ContId)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Whish_Header";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.            
            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 251, "Title");
            cmd.Parameters.Add("@TitleText", SqlDbType.VarChar, 250, "TitleText");
            cmd.Parameters.Add("@idSession", SqlDbType.Int);
            cmd.Parameters.Add("@statusWish", SqlDbType.Int);
            cmd.Parameters.Add("@deleteFlag", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = title;
            cmd.Parameters[1].Value = titleText;
            cmd.Parameters[2].Value = idSession;
            cmd.Parameters[3].Value = statusWish;
            cmd.Parameters[4].Value = deleteFlag;
            cmd.Parameters[5].Value = SiteId;
            cmd.Parameters[6].Value = ContId;
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
    protected internal string Add_Whish_Detail(int wishListId, int titleId, string defaultSKU, string comments, int SiteId, int ContId)
    {
        //return "wishListId:" + Convert.ToString(wishListId) + "<br>titleId:" + Convert.ToString(titleId) + "<br>defaultSKU:" + Convert.ToString(defaultSKU) + "<br>comments:" + Convert.ToString(comments);
        try
        {
           
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Whish_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.            
            cmd.Parameters.Add("@wishListId", SqlDbType.Int);
            cmd.Parameters.Add("@titleId", SqlDbType.Int);
            cmd.Parameters.Add("@defaultSKU", SqlDbType.VarChar, 251, "defaultSKU");
            cmd.Parameters.Add("@comments", SqlDbType.VarChar, 250, "comments");
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);    
            //Setting values to Parameters.
            cmd.Parameters[0].Value = wishListId;
            cmd.Parameters[1].Value = titleId;
            cmd.Parameters[2].Value = defaultSKU;
            cmd.Parameters[3].Value =""+comments;
            cmd.Parameters[4].Value = SiteId;
            cmd.Parameters[5].Value = ContId;
            cmd.ExecuteNonQuery();
            return "This item has been added to your Wish List.";
            Close();
            //return "wishListId:" + Convert.ToString(wishListId) + "<br>titleId:" + Convert.ToString(titleId) + "<br>defaultSKU:" + Convert.ToString(defaultSKU) + "<br>comments:" + Convert.ToString(comments);
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return "Error trying to add a product.";
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return "Error tring to add a product.";
        }
    }
    //---------------------------------
    protected internal int Del_Whish_Header(int wishId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Del_Whish_Header";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.Add("@wishId", SqlDbType.Int);
            cmd.Parameters[0].Value = wishId;
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return 0;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return 1;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return 1;
        }
    }
    //---------------------------------
    protected internal int Del_Whish_Detail(int wishId,int id,string sku)
    {
        /*
         @wishId as int,
	    @titleid as int ,
	    @sku as varchar(50) 
         */
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Del_Whish_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.Add("@wishId", SqlDbType.Int);
            cmd.Parameters.Add("@titleid", SqlDbType.Int);
            cmd.Parameters.Add("@sku", SqlDbType.VarChar,50);
            cmd.Parameters[0].Value = wishId;
            cmd.Parameters[1].Value = id;
            cmd.Parameters[2].Value = sku;
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return 0;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return 1;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return 1;
        }
    }
    //---------------------------------
    //---------------------------------
    protected internal void Upd_Comment_Whish_Detail(int wishId, string comments)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Upd_Comment_Whish_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.Add("@idWishList", SqlDbType.Int);
            cmd.Parameters.Add("@commment", SqlDbType.VarChar, 3000, "commment");
            cmd.Parameters[0].Value = wishId;
            cmd.Parameters[1].Value = comments;
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
    //---------------------------------
    protected internal DataSet Get_Wish_by_IdSession(int idSession, int SiteId, int ContId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Wish_by_IdSession";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idSession", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);  
            cmd.Parameters[0].Value = idSession;
            cmd.Parameters[1].Value = SiteId;
            cmd.Parameters[2].Value = ContId;
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
    //---------------------------------
    protected internal DataSet Get_All_Wish_By_Session(int idSession, int SiteId, int ContId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Wish_By_Session";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idSession", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters[0].Value = idSession;
            cmd.Parameters[1].Value = SiteId;
            cmd.Parameters[2].Value = ContId;
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

    // Added by Jordan Sherer
    // re: Wishlist Sharing
    // Aug 28, 2009 
    protected internal DataSet Get_Wish_by_GUID(string id, int SiteId, int ContId)
    {
        try
        {
            return Get_Wish_by_GUID(new Guid(id), SiteId, ContId);
        }
        catch (Exception)
        {
            return null;
        }
    }
    protected internal DataSet Get_Wish_by_GUID(Guid id, int SiteId, int ContId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Wish_by_GUID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GUID", DbType.Guid);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters[0].Value = id;
            cmd.Parameters[1].Value = SiteId;
            cmd.Parameters[2].Value = ContId;
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
    protected internal DataSet Get_All_Wish_By_GUID(string id, int SiteId, int ContId)
    {
        try
        {
            return Get_All_Wish_By_GUID(new Guid(id), SiteId, ContId);
        }
        catch (Exception)
        {
            return null;
        }
    }
    protected internal DataSet Get_All_Wish_By_GUID(Guid id, int SiteId, int ContId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Wish_By_GUID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GUID", DbType.Guid);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters[0].Value = id;
            cmd.Parameters[1].Value = SiteId;
            cmd.Parameters[2].Value = ContId;
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
