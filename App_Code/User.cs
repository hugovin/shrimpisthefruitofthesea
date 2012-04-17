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
/// Summary description for User
/// </summary>
public class User: ConnectSql
{
    public readonly static string SESSION_USER_ID = "UserSite";
    public readonly static string SESSION_USER_FULLNAME = "UserFullName";
    public readonly static string SESSION_USER_ACTIVESESSION = "UserActiveSession";

	public User()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public DataSet Get_User_State(int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_User_State";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int);
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
}
