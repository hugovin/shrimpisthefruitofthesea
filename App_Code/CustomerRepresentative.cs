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
public class CustomerRepresentative: ConnectSql
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
	
	public CustomerRepresentative(int CostRepID)
	{
		// 
	}
    public static DataSet GetUserCustomerRepByZipCode(string RepClass,string UserZipCode,int SiteId) // one of : RE, SB, CA, ALL any othere returns all.  
	{
			// this is needed in order to hava a fully static method. It could be updated later on.
			string connectionSt = ConfigurationManager.AppSettings["conStringSQL"];
			SqlConnection SqlConnect;
			SqlConnect = new SqlConnection(connectionString);
            SqlConnect.Open();     
            // get the info   
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = SqlConnect;
            cmd.CommandText = "get_cust_rep_by_zip";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId ", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CustomerRepArea", SqlDbType.NVarChar);
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = UserZipCode;
            cmd.Parameters[2].Value = RepClass; 
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            // close and dispose.
            cmd.Dispose();
			SqlConnect.Close();
            SqlConnect.Dispose();
            return dataset;
     }
    public DataSet GetUserCustomerRepByZipCode(string RepClass,string UserZipCode){
		return GetUserCustomerRepByZipCode( RepClass, UserZipCode,1);
    }  
	
}
