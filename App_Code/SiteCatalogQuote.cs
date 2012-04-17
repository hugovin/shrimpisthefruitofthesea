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
/// Summary description for SiteCatalogQuote
/// </summary>
public class SiteCatalogQuote : ConnectSql
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
	public SiteCatalogQuote()
	{	}
    public void addQuoteDetails(string quoteId, string title, string platform, string quantity){
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            //
            cmd.Connection = DataBase;
            cmd.CommandText = "stp_QUOTE_Process_Quote_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@QuoteID", SqlDbType.Int);
            cmd.Parameters.Add("@Title", SqlDbType.VarChar);
            cmd.Parameters.Add("@Platform", SqlDbType.VarChar);
            cmd.Parameters.Add("@Qty", SqlDbType.VarChar);
            //OutPut 

            //Setting values to Parameters.
            cmd.Parameters[0].Value = int.Parse(quoteId);
            cmd.Parameters[1].Value = title;
            cmd.Parameters[2].Value = platform;
            cmd.Parameters[3].Value = quantity;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            //Console.WriteLine(cmd.Parameters["@NewQuoteID"].Value.ToString());
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
    public string addQuote(string fullName,string bldgName, string add1, string add2, string city, string state, string zip, string country, string phone, string email, string title, string purchaseFor, string notes){
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();


            //OutPut Params
            SqlParameter NewQuoteId = new SqlParameter("@NewQuoteID", SqlDbType.Int);
            NewQuoteId.Direction = ParameterDirection.Output;
            SqlParameter NewLoginId = new SqlParameter("@NewLoginID", SqlDbType.Int);
            NewLoginId.Direction = ParameterDirection.Output;
            //
            cmd.Connection = DataBase;
            cmd.CommandText = "stp_QUOTE_Process_Quote_Contact";
            cmd.CommandType = CommandType.StoredProcedure;
            // --> Outputs
            cmd.Parameters.Add(NewLoginId);
            cmd.Parameters.Add(NewQuoteId);
            //
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@FullName", SqlDbType.VarChar);
            cmd.Parameters.Add("@BldgName", SqlDbType.VarChar);
            cmd.Parameters.Add("@Address1", SqlDbType.VarChar);
            cmd.Parameters.Add("@Address2", SqlDbType.VarChar);
            cmd.Parameters.Add("@City", SqlDbType.VarChar);
            cmd.Parameters.Add("@State", SqlDbType.VarChar);
            cmd.Parameters.Add("@Zip", SqlDbType.VarChar);
            cmd.Parameters.Add("@Country", SqlDbType.VarChar);
            cmd.Parameters.Add("@Phone", SqlDbType.VarChar);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar);
            cmd.Parameters.Add("@Title", SqlDbType.VarChar);
            cmd.Parameters.Add("@ConPurchFor", SqlDbType.VarChar);
            cmd.Parameters.Add("@Notes", SqlDbType.VarChar);
            cmd.Parameters.Add("@ProdSiteID", SqlDbType.Int);
            //OutPut 
            cmd.Parameters[0].Direction = ParameterDirection.Output;
            cmd.Parameters[1].Direction = ParameterDirection.Output;
            //
            //Setting values to Parameters.
            cmd.Parameters[2].Value = SiteId;
            cmd.Parameters[3].Value = fullName;
            cmd.Parameters[4].Value = bldgName;
            cmd.Parameters[5].Value = add1;
            cmd.Parameters[6].Value = " ";
            cmd.Parameters[7].Value = city;
            cmd.Parameters[8].Value = state;
            cmd.Parameters[9].Value = zip;
            cmd.Parameters[10].Value = country;
            cmd.Parameters[11].Value = phone;
            cmd.Parameters[12].Value = email;
            cmd.Parameters[13].Value = title;
            cmd.Parameters[14].Value = purchaseFor;
            cmd.Parameters[15].Value = notes;
            cmd.Parameters[16].Value = SiteId;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            string QuoteObtain = cmd.Parameters["@NewQuoteID"].Value.ToString();
            string LoginObtain = cmd.Parameters["@NewLoginID"].Value.ToString();
            cmd.Dispose();
            Close();
            //Console.WriteLine(cmd.Parameters["@NewQuoteID"].Value.ToString());
            return QuoteObtain;
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

    public DataSet getUserIndo(string userGUID)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "stp_ORD_Select_UserInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LoginGUID", SqlDbType.VarChar);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = userGUID;
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
