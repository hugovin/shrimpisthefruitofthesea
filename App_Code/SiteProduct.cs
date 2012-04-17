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
/// Summary description for Product
/// </summary>
public class SiteProduct : ConnectSql
{
   private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
   private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
    Title title = new Title();
    public SiteProduct()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  
    public DataSet Get_Title_by_Id(int TitleId, string SkuId)
    {
        return title.Get_Title_by_Id(SiteId, ContId, TitleId, SkuId);        
    }
    public DataSet Get_Title_by_Id(int TitleId)
    {
        return title.Get_Title_by_Id(SiteId, ContId, TitleId, "");
    }

    public DataSet Get_SitePrice_by_Title(int TitleId)
    {
        return title.Get_SitePrice_by_Title(SiteId, ContId, TitleId);
    }

    public DataSet Get_Title_Content_Site_Classification(int classId)
    {
        return title.Get_Title_Content_Site_Classification(SiteId, classId);
    }

    public DataSet Get_Title_Content_Site_Free_Tools()
    {
        return title.Get_Title_Content_Site_Free_Tools(SiteId);
    }

    public DataSet Get_All_Products_By_Classification(int SubjId, int cp)
    {
        return title.Get_All_Products_By_Classification(SiteId, ContId, SubjId, cp);
    }
    
    public DataSet Get_Product_by_Id(int titleId)
    {
        return title.Get_Product_by_Id(SiteId, ContId, titleId);
    }

    public DataSet Get_All_Publisher_By_Id(int PubId)
    {
        return title.Get_All_Publisher_By_Id(SiteId, ContId, PubId);
    }

    public DataSet Get_Title_Content_Publisher(int PubId)
    {
        return title.Get_Title_Content_Publisher(PubId);
    }

    public DataSet Get_All_Freetools_By_Id(int ClassId, int titleResourceTypeIdDemos)
    {
        return title.Get_All_Freetools_By_Id(SiteId, ContId, ClassId, titleResourceTypeIdDemos);
    }

    public DataSet Get_Image_Product(int titleId)
    {
        return title.Get_Image_Product(titleId);
    }

    //-----------------------------------------------------------------------
    public void Add_Torch_Description(int orderid,string description,string sku)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Torch_Description";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@OrderId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.VarChar);
            cmd.Parameters.Add("@Sku", SqlDbType.VarChar);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = orderid;
            cmd.Parameters[1].Value = description;
            cmd.Parameters[2].Value = sku;
            SqlDataReader dr = cmd.ExecuteReader();
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
    }
    //------------------------------------------------------------------------
    public DataSet Get_TorchPrices()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Torch_Skus";
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
    //------------------------------------------------------------------------
    public DataSet Get_TorchDescription(int cartid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Torch_Configuration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@carid", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = cartid;
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
    public DataSet Get_SideFeaturedProducts(int siteid, int contid,int subjid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideFeaturedProducts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SubjId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = subjid;
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
    public DataSet Get_SideBestSellers(int siteid, int contid,int subjid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideBestSellers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SubjId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = subjid;
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
    public DataSet Get_SideWhatsNew(int siteid, int contid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideWhatsNew";
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
    //------------------------------------------------------------------------
    public DataSet Get_SideSpecials(int siteid, int contid, int subjid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideSpecials";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = subjid;
            cmd.Parameters[1].Value = siteid;
            cmd.Parameters[2].Value = contid;
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
    public DataSet Get_SideNewsNInfo(int siteid,int contid)
    {
                try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideNewsNInfo";
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
    //------------------------------------------------------------------------
    public DataSet Get_SideRelatedProducts(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideRelatedProducts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    //------------------------------------------------------------------------
    public DataSet Get_SideSAPricing(int siteid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SideSAPricing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            //Setting values to Parameters.
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
    //------------------------------------------------------------------------

    public DataSet Get_Title_RelatedProducts(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_RelatedProducts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    //------------------------------------------------------------------------

    public DataSet Get_Title_All_RelatedProducts(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Title_RelatedProducts";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    //------------------------------------------------------------------------
    public DataSet Get_Title_SysReq_OpeSys(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_SysReq_OpeSys";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    public DataSet Get_Title_SysReq_MemCPU(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_SysReq_MemCPU";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    public DataSet Get_Title_LicenseOptions(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_LicenseOptions";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = titleid;
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
    public DataSet Get_Title_Images(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Images";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    public DataSet Get_Title_Review(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Review";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    public DataSet Get_Title_Resources(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Resources";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = titleid;
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

    public DataSet Get_Title_AdditionalInfo(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_AdditionalInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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

    public DataSet Get_Title_Funding(int titleid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Title_Funding";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = titleid;
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
    //---- Procedurer For retrieve Videos and Trials of the products.
    //---- This are linked with the database procedures and functions.
    //------------------------------------------------------------------------
    public DataSet Get_Site_Trials_Demos(int TitleId, int TitleResourceTypeID)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleResourceTypeID", SqlDbType.Int);
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TitleId;
            cmd.Parameters[3].Value = TitleResourceTypeID;
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
    //---------------------------------------------
    public DataSet Get_Site_Trials_Demos_List(int TitleResourceTypeID)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos_List";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleResourceTypeID", SqlDbType.Int);
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TitleResourceTypeID;
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
    //---------------------------------------------
    public DataSet Get_Site_Trials_Demos_List_Youtube()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Trials_Demos_List_Youtube";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
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
}
