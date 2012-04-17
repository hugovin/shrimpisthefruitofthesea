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
/// Summary description for UserCMS
/// </summary>
public class UserCMS:ConnectSql
{
    public string strUserId;
    public string strUserPwd;
    public string strUserFullName ;
    public int intUserType;
    public bool blUserSatet;

    protected string getStrUserId()
    {
        return this.strUserId;
    }

    //------------------------------------------------------
	public UserCMS()
	{
        strUserId = "";
        strUserPwd ="";
        strUserPwd = "";
        intUserType = 0;
        blUserSatet = false;
	}
    //------------------------------------------------------
    public UserCMS(string userid, string userpwd, string userfull,int type, bool userstate)
    {
        this.strUserId = userid;
        this.strUserPwd = userpwd;
        this.strUserFullName = userfull;
        this.intUserType = type;
        this.blUserSatet = userstate;
    }
    //------------------------------------------------------
    protected DataSet VerifyUserPwd(string user, string pwd)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_UserCms_Login";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar,50,"UserId");
            cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50, "UserPassword");            
            //Setting values to Parameters.
            cmd.Parameters[0].Value = user;
            cmd.Parameters[1].Value = pwd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = cmd;
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
    //------------------------------------------------------
    protected int AddUser(string user,string pwd,string name, int state)
    { int save = 0;
    DataSet dataset = new DataSet();
    try
    {
        Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = DataBase;
        //Procedure Name.
        cmd.CommandText = "Add_New_User";
        cmd.CommandType = CommandType.StoredProcedure;
        //Procedure Parameters.
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50, "UserId");
        cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50, "UserPassword");
        cmd.Parameters.Add("@UserFullName", SqlDbType.VarChar, 500, "UserFullName");
        cmd.Parameters.Add("@UserType", SqlDbType.Int);
        //Setting values to Parameters.
        cmd.Parameters[0].Value = user;
        cmd.Parameters[1].Value = pwd;
        cmd.Parameters[2].Value = name;
        cmd.Parameters[3].Value = state;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(dataset);
        cmd.Dispose();
        Close();
    }
    catch
    {
        save = 1;
    }
    if (dataset != null)
    {
        foreach (DataTable table in dataset.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row.ItemArray[0]) !=0)
                {
                    save = 1;
                }
            }
        }
    }

    return save;
    }
    //------------------------------------------------------
    protected DataSet Get_All_Users()
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_Users";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = cmd;
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
    //------------------------------------------------------
    protected DataSet Get_User_By_Id(int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_User_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            //Setting values to Parameters.
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
    //-----------------------------------------------/------
    protected void Upd_UserCMS(int userId,string user, string pwd, string name)
    {

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_UserCMS";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50, "UserId");
            cmd.Parameters.Add("@UserNumber", SqlDbType.Int);           
            cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50, "UserPassword");
            cmd.Parameters.Add("@UserFullName", SqlDbType.VarChar, 500, "UserFullName"); 
            //Setting values to Parameters.
            cmd.Parameters[0].Value = user;
            cmd.Parameters[1].Value = userId;
            cmd.Parameters[2].Value = pwd;
            cmd.Parameters[3].Value = name;
            cmd.ExecuteNonQuery();
            Close();
        }        
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
           // return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            //return null;
        }
    }
    //------------------------------------------------------
    protected void Del_UserCms(int userNunmber)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_UserCms";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@UserNumber", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = userNunmber;
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
    }
    //------------------------------------------------------
         


}
