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
/// Summary description for Result
/// </summary>
public class Result:ConnectSql
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
	public Result()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet Find_FinderTitle(string TextFinder, string SubjSubCateId, string TeacSubCateId, string Grade, string PlatformId, string PubId, string SortBy)
    {
        try
        {
            Open();
            DataSet datasetFinder = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Find_FinderTitle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@TextFinder", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@SubjSubCateId ", SqlDbType.Int);
            cmd.Parameters.Add("@TeacSubCateId ", SqlDbType.Int);
            cmd.Parameters.Add("@Grade ", SqlDbType.Int);
            cmd.Parameters.Add("@PlatformId ", SqlDbType.Int);
            cmd.Parameters.Add("@PubId ", SqlDbType.Int);
            cmd.Parameters.Add("@SortBy ", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TextFinder;
            cmd.Parameters[3].Value = (SubjSubCateId ==null?System.Data.SqlTypes.SqlInt32.Null:Convert.ToInt32(SubjSubCateId));
            cmd.Parameters[4].Value = (TeacSubCateId==null?System.Data.SqlTypes.SqlInt32.Null:Convert.ToInt32(TeacSubCateId));
            cmd.Parameters[5].Value = (Grade==null?System.Data.SqlTypes.SqlInt32.Null:Convert.ToInt32(Grade));
            cmd.Parameters[6].Value = (PlatformId==null?System.Data.SqlTypes.SqlInt32.Null:Convert.ToInt32(PlatformId));
            cmd.Parameters[7].Value = (PubId==null?System.Data.SqlTypes.SqlInt32.Null:Convert.ToInt32(PubId));
            cmd.Parameters[8].Value = (SortBy == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(SortBy));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            datasetFinder.Clear();
            adapter.Fill(datasetFinder);
            cmd.Dispose();
            Close();
            adapter.Dispose();
            return datasetFinder;
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
    public DataSet Find_FinderTitleCatGroup(string TextFinder, string SubjSubCateId, string TeacSubCateId, string Grade, string PlatformId, string PubId, string SortBy, string CategoryGroup)
    {
        try
        {
            Open();
            DataSet datasetFinder = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Find_FinderTitleCategoryGroup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@TextFinder", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@SubjSubCateId ", SqlDbType.Int);
            cmd.Parameters.Add("@TeacSubCateId ", SqlDbType.Int);
            cmd.Parameters.Add("@Grade ", SqlDbType.Int);
            cmd.Parameters.Add("@PlatformId ", SqlDbType.Int);
            cmd.Parameters.Add("@PubId ", SqlDbType.Int);
            cmd.Parameters.Add("@SortBy ", SqlDbType.Int);
            cmd.Parameters.Add("@CategoryGroup", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TextFinder;
            cmd.Parameters[3].Value = (SubjSubCateId == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(SubjSubCateId));
            cmd.Parameters[4].Value = (TeacSubCateId == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(TeacSubCateId));
            cmd.Parameters[5].Value = (Grade == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(Grade));
            cmd.Parameters[6].Value = (PlatformId == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(PlatformId));
            cmd.Parameters[7].Value = (PubId == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(PubId));
            cmd.Parameters[8].Value = (SortBy == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(SortBy));
            cmd.Parameters[9].Value = (CategoryGroup == null ? System.Data.SqlTypes.SqlInt32.Null : Convert.ToInt32(CategoryGroup));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            datasetFinder.Clear();
            adapter.Fill(datasetFinder);
            cmd.Dispose();
            Close();
            adapter.Dispose();
            return datasetFinder;
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

    public DataSet Find_TitlebyDesc(string TextFinder, string SubjSubCateId, string TeacSubCateId, string Grade, string PlatformId, string PubId)
    {

        try
        {
            Open();
            DataSet datasetRes = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Find_TitlebyDesc";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId ", SqlDbType.Int);
            cmd.Parameters.Add("@TextFinder", SqlDbType.VarChar, 250);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = ContId;
            cmd.Parameters[2].Value = TextFinder;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            datasetRes.Clear();
            adapter.Fill(datasetRes);
            cmd.Dispose();
            Close();
            return datasetRes;
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

    public DataSet Get_PubId(string pubname)
    {
        try
        {
            Open();
            DataSet datasetFinder = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_PubId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PubName", SqlDbType.VarChar, 50);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = pubname;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            datasetFinder.Clear();
            adapter.Fill(datasetFinder);
            cmd.Dispose();
            Close();
            adapter.Dispose();
            return datasetFinder;
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

    public string Get_Title_by_Student_Pricing(int TitleId)
    {
        Title title = new Title();
	return title.Get_Title_by_Student_Pricing(SiteId, ContId, TitleId);
    }
	
	

}
