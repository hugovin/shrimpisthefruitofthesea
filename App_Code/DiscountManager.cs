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
/// Summary description  this handle the user representatives
/// </summary>
public class DiscountManager: ConnectSql
{

	private string SalesRepID;
	private string SalesRepTypeID;
	private string FullName;
	private string Title;
	private string Phone;
	private string PhoneExt;
	private string Fax;
	private string CreateDate;
	private string ActiveFlag;

	public DiscountManager(int CostRepID)
	{
		//   
	}
    public static bool NeedsProof(int ProductTitleId) 
	{
			bool needsProof;
			int flagValue;
			string connectionSt = ConfigurationManager.AppSettings["conStringSQL"];
			SqlConnection SqlConnect;
			SqlConnect = new SqlConnection(connectionString);
            SqlConnect.Open();     
            // get the info   
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = SqlConnect;
            cmd.CommandText = "product_needs_proof";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductTitleId", SqlDbType.Int);
            cmd.Parameters[0].Value = ProductTitleId; 
			flagValue=(int) cmd.ExecuteScalar();
	 
            // close and dispose.
            cmd.Dispose();
			SqlConnect.Close();
            SqlConnect.Dispose();
			       
           if(flagValue==1) return true;
           else return false;
     }
	
}
