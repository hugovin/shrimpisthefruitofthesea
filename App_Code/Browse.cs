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

    public class Browse:ConnectSql
    {
        //----------------------------------------
        public Browse()
        {
            Close();
        }
        //-------------------------------------------------
        protected internal DataSet getAllBrowseCategory(int siteid,int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_BrowseCategory";
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId ", SqlDbType.Int);
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
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
        //----------------------------------------
        protected internal DataSet getAllBrowseDefault()
        {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_BrowseDefault";

                cmd.CommandType = CommandType.StoredProcedure;
                //Setting values to Parameters.
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
                Close();
                return null;
            }
            catch (Exception oEx)
            {
                Close();
                //Console.WriteLine("" + oEx.Message);
                return null;
            }
        }
        ////-----------------------------------------
        protected internal DataSet getAllBrowseSubCategory(int id)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_BrowseSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BrowCateId", SqlDbType.Int);
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
        //------------------------------------------------------------------------
        protected internal DataSet getAllBrowseSubDefault(int browid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_BrowseSubDefault";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@browid", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = browid;
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
        //-------------------------------------------------------------------------
        protected internal void Add_BrowseSubCategory(int browcateid,int browsubcateid, int browsubcateordpos)
        {
             try
             {
                 Open();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = DataBase;
                 //Procedure Name.
                 cmd.CommandText = "Add_BrowseSubCategory";
                 cmd.CommandType = CommandType.StoredProcedure;
                 //Procedure Parameters.
                 cmd.Parameters.Add("@BrowCateId", SqlDbType.Int);
                 cmd.Parameters.Add("@BrowSubCateId", SqlDbType.Int);
                 cmd.Parameters.Add("@BrowSubCateOrdPos", SqlDbType.Int);    
                 //Setting values to Parameters.
                 cmd.Parameters[0].Value = browcateid;
                 cmd.Parameters[1].Value = browsubcateid;
                 cmd.Parameters[2].Value = browsubcateordpos;
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
        //-------------------------------------------------------------------------
        protected internal void Delete_BrowseSubCategory()
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Del_BrowseSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
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
        //-------------------------------------------------------------------------
        protected internal void Add_BrowseCategory(int siteid, int contid, int browCateid, string browcatitle, int browcateord, int browcatestate)
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_BrowseCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@BrowCategoryId", SqlDbType.Int);
            cmd.Parameters.Add("@BrowCateTitle", SqlDbType.VarChar, 50, "BrowCateTitle");
            cmd.Parameters.Add("@BrowCateOrdPos", SqlDbType.Int);
            cmd.Parameters.Add("@BrowCateState", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = browCateid;
            cmd.Parameters[3].Value = browcatitle;
            cmd.Parameters[4].Value = browcateord;
            cmd.Parameters[5].Value = browcatestate;

            cmd.ExecuteNonQuery();
            Close();
 
        }
        //-------------------------------------------------------------------------
        protected DataSet getSubDefaultById(int titleID)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_FeatProduct_By_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = titleID;
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
        //---------------------------------//---------------------------------
        protected DataSet getSubDefaultByTitle(int siteid,int contid,int browid,string title)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_SubDefault_By_Title";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FindId", SqlDbType.Int);
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 150, "title");
                //Setting values to Parameters.
                cmd.Parameters[0].Value = browid;
                cmd.Parameters[1].Value = title;
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
        //-------------------------------------------------------------------------
        protected internal void Add_BrowseSubDefaultCategory(int siteId,int contId,int browcateid, int browsubcateid, int browsubcateordpos)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_BrowseSubDefaCatItems";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int); 
                cmd.Parameters.Add("@BrowCateId", SqlDbType.Int);
                cmd.Parameters.Add("@BrowSubCateId", SqlDbType.Int);
                cmd.Parameters.Add("@BrowSubCateOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteId;
                cmd.Parameters[1].Value = contId;
                cmd.Parameters[2].Value = browcateid;
                cmd.Parameters[3].Value = browsubcateid;
                cmd.Parameters[4].Value = browsubcateordpos;
                cmd.ExecuteNonQuery();
                Close();
            }
            catch (SqlException oSqlExp)
            {
                //Console.WriteLine("" + oSqlExp.Message);

            }
            catch (Exception oEx)
            {
 
            }
        }
        //-------------------------------------------------------------------------
        protected internal void deleteSubDefault(int id)
        {
            try
            {
            Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_BrowseDefaultItem";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@DefaId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = id;
            string ids = "";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { ids = dr["id"].ToString(); }
            }
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

            }
        }
        //------------------------------------------------------------------------
        protected internal void Del_AllBrowseCategory(int siteid,int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Del_AllBrowseCategory ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int); 
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                SqlDataAdapter adapter = new SqlDataAdapter();
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
        //------------------------------------------------------------------------
        protected internal void Upd_BrowseDefault(int browdefaid,bool truefalsese, bool truefalsedb)
        {
            try
            {
                Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Upd_BrowseDefault";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@BrowDefaId", SqlDbType.Int);
                cmd.Parameters.Add("@TrueFalsese", SqlDbType.Int);
                cmd.Parameters.Add("@TrueFalsedb", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = browdefaid;
                cmd.Parameters[1].Value = truefalsese;
                cmd.Parameters[2].Value = truefalsedb;
                string ids = "";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    { ids = dr["id"].ToString(); }
                }
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

            }
 
        }
        //------------------------------------------------------------------------
        protected internal void Del_All_BrowseSubCategory(int BrowCateId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Del_AllBrowseCategory ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BrowCateId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = BrowCateId;
                SqlDataAdapter adapter = new SqlDataAdapter();
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



    }

