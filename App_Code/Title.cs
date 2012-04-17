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
/// Summary description for Title
/// </summary>
public class Title : ConnectSql
{
    public Title()
    {
    
    }
    protected internal DataSet Get_Title_Content_Site_Classification(int SiteId, int classsId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Content_Site_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@classId", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = classsId;           
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
    protected internal DataSet Get_Title_Content_Site_Free_Tools(int SiteId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Content_Site_Free_Tools";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            ////Setting values to Parameters.
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
    //---------------------------------   
    protected internal DataSet Get_Image_Product(int titleId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Image_Product";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@titleId", SqlDbType.Int);        
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = titleId;      
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

    protected internal string Get_Title_by_Student_Pricing(int SiteId, int ContId, int TitleId)
    {
	string ids = "";
        try
        {
            Open();   
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Title_by_Student_Pricing"; 
                cmd.CommandType = CommandType.StoredProcedure;
                ////Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleId", SqlDbType.Int);
 
                ////Setting values to Parameters.
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = TitleId;
 
                SqlDataReader dr = cmd.ExecuteReader();
            	if (dr.HasRows)
           	 {
                	while (dr.Read())
                	{ ids = dr["sap"].ToString(); }
            	}
            	dr.Close();
            	dr.Dispose();
		
                cmd.Dispose();
            Close();
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

///---------------------------------------------------------------------
    protected internal DataSet Get_Title_by_Id(int SiteId, int ContId, int TitleId, string SkuId)
    {

        try
        {
            Open();   
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Title_by_Id"; 
                cmd.CommandType = CommandType.StoredProcedure;
                ////Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleId", SqlDbType.Int);
                cmd.Parameters.Add("@SKUId", SqlDbType.VarChar,20);
                ////Setting values to Parameters.
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = TitleId;
                cmd.Parameters[3].Value = SkuId;
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
    protected internal DataSet Get_All_Classification(int SiteId, int ContId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@SITEID", SqlDbType.Int);
            cmd.Parameters.Add("@CONTID", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
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
    protected internal DataSet Get_Title_Content_Classification_Product(int classId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Content_Classification_Product";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@classId", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = classId;
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
    protected internal DataSet Get_Title_Content_Publisher(int pubId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Content_Publisher";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@pubId", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = pubId;
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
    protected internal DataSet Get_All_Freetools_By_Id(int SiteId, int ContId, int ClassId, int titleResourceTypeIdDemos)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Freetools_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@ClassId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleResourceTypeID", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = ClassId;
            cmd.Parameters[3].Value = titleResourceTypeIdDemos;
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
    protected internal DataSet Get_All_Products_By_Classification(int siteid, int contid, int id, int classId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Products_By_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@ContId", SqlDbType.VarChar, 5, "ContId");
            cmd.Parameters.Add("@SubjId", SqlDbType.VarChar, 5, "SubjId");
            cmd.Parameters.Add("@cp", SqlDbType.VarChar, 5, "cp");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
            cmd.Parameters[3].Value = classId;
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
    protected internal DataSet Get_Product_by_Id(int siteid, int contid, int titleId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Product_by_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@ContId", SqlDbType.VarChar, 5, "ContId");
            cmd.Parameters.Add("@TitleId", SqlDbType.VarChar, 5, "TitleId");  
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = titleId;
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
    protected internal DataSet Get_All_Publisher_By_Id(int siteid, int contid, int pubId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Publisher_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@ContId", SqlDbType.VarChar, 5, "ContId");
            cmd.Parameters.Add("@pubId", SqlDbType.VarChar, 5, "pubId");            
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = pubId;
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
    protected internal DataSet Get_SitePrice_by_Title(int SiteId, int ContId, int TitleId)
    {

        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SitePrice_by_Title";
            cmd.CommandType = CommandType.StoredProcedure;
            ////Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            ////Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TitleId;
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

    //--------------------------------------------------------------------------------------------------
    protected void addClassification(int SiteId, int ContId, string description, string content)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 150, "description");
            cmd.Parameters.Add("@content", SqlDbType.VarChar, 150, "content"); 

            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = description;
            cmd.Parameters[3].Value = content;
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
    //---------------------------------
    protected string delClassification(int siteid, int contid, int id)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@ClassId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = id;
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

    //---------------------------------------------------------------------
    protected void updClassification(int siteid, int contid, int classId, string title, string comments)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Classification";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@ClassId", SqlDbType.Int);
            cmd.Parameters.Add("@title", SqlDbType.VarChar, 150, "title");
            cmd.Parameters.Add("@comments", SqlDbType.VarChar, 150, "comments");

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = classId;
            cmd.Parameters[3].Value = title;
            cmd.Parameters[4].Value = comments;
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
