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
/// Summary description for LeftMenu
/// </summary>
namespace uc_Left_Menu
{
    public class LeftMenu : ConnectSql
    {
        public LeftMenu()
        {

            //Get_LeftMenu_All_ResourceCenter 1,1
        }
        public DataSet Get_LeftMenu_All_FinderCategory()
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_FinderCategory";
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
        public DataSet Get_LeftMenu_All_FinderSubCategory(int SiteId, int ContId, int FindCateId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_FinderSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@FindCateId", SqlDbType.Int);
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = FindCateId;
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
        public DataSet Get_LeftMenu_All_FinderDefault()
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_FinderDefault";
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
        public DataSet Get_LeftMenu_All_FinderSubDefault(int SiteId, int ContId, int FindDefaId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_FinderSubDefault";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@FindId", SqlDbType.Int);
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = FindDefaId;
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
       

        public DataSet Get_LeftMenu_All_Subjects(int SiteId, int ContId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_Subjects";
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
       public DataSet Get_LeftMenu_All_SubSubjects(int SiteId, int ContId,int SubjId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_SubSubjects";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SubjId", SqlDbType.Int);
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters[0].Value = SubjId;
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

        public DataSet Get_LeftMenu_All_BrowseCategory(int SiteId, int ContId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_BrowseCategory";
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
        public DataSet Get_LeftMenu_All_BrowseSubCategory(int SiteId, int ContId, int BrowCateId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_BrowseSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@BrowCateId", SqlDbType.Int);
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = BrowCateId;
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
        public DataSet Get_LeftMenu_All_BrowseDefault()
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_BrowseDefault";
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
        public DataSet Get_LeftMenu_All_BrowseSubDefault(int SiteId, int ContId, int BrowDefaId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_LeftMenu_All_BrowseSubDefault";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@BrowDefaId", SqlDbType.Int);
                cmd.Parameters[0].Value = SiteId;
                cmd.Parameters[1].Value = ContId;
                cmd.Parameters[2].Value = BrowDefaId;
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

        public DataSet Get_LeftMenu_All_ResourceCenter(int SiteId, int ContId)
        {
            Generics generics = new Generics();
            return generics.Get_Site_All_GenericByType(SiteId, ContId, 1);
            generics.Dispose();
        }

        public DataSet Get_LeftMenu_All_AboutUs(int SiteId)
        {

            Generics generics = new Generics();
            return generics.Get_Site_All_GenericByType(SiteId, 0, 2);//Loading  About Us only in Site Level
            generics.Dispose();
        }
        
        public void LoadFinder()
        {


        }
        public void LoadSubjects()
        {

        }

        public void LoadBrowse()
        {

        }

        public void LoadResourceCenter()
        {

        }
    }
}