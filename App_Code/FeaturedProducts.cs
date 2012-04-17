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


    public class FeaturedProducts:ConnectSql
    {
        public int intSiteid;
        public int intContid;
        public int intSubjid;
        public int intFeatPro;
        public int intTitleID;
        public bool blWhatHome;
        public int intFeatProOrdPos;
        public bool intFeatProState;
        
        //--------------------------------------
        public FeaturedProducts()
        {
            intSiteid = 0;
            intContid = 0;
            intSubjid = 0;
            intFeatPro = 0;
            intTitleID = 0;
            blWhatHome = true;
            intFeatProOrdPos = 0;
            intFeatProState = true;
        }
        //---------------------------------------

        public FeaturedProducts(int siteid, int contid, int subjid, int featPro, int title,bool whathome,int featproOrd, bool featProSta)
        {
            this.intSiteid = siteid;
            this.intContid = contid;
            this.intSubjid = subjid;
            this.intFeatPro = featPro;
            this.intTitleID = title;
            this.blWhatHome = whathome;
            this.intFeatProOrdPos = featproOrd;
            this.intFeatProState = featProSta;
        }
        //---------------------------------
        protected DataSet getAllFeaturedProductsBySubjectID(int siteid,int contid,int id)
        {
            
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_FeaturedProduct_By_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SITEID", SqlDbType.Int);
                cmd.Parameters.Add("@CONTID", SqlDbType.Int);
                cmd.Parameters.Add("@SUBJID", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                if (id != 0)
                {
                    cmd.Parameters[2].Value = id;
                }
                else
                {
                    cmd.Parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
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
        protected string deleteFeatureProducts(int productid)
        {
            Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_FeaturedProducts";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@FeatPro", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = productid;
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
            return ids;
        }
        //---------------------------------
        public void addFeatureProduct(int siteid,int contid,int subjid,int titleid,bool Feathome,int pos)
        {

            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_FeatureProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@SubjId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleID", SqlDbType.Int);
                cmd.Parameters.Add("@FeatHome", SqlDbType.Int);
                cmd.Parameters.Add("@FeatProOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                if (subjid != 0)
                {
                    cmd.Parameters[2].Value = subjid;
                }
                else
                {
                    cmd.Parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                cmd.Parameters[3].Value = titleid;
                cmd.Parameters[4].Value = Feathome;
                cmd.Parameters[5].Value = pos;
                cmd.ExecuteNonQuery();
                Close();
            }
            catch (SqlException oSqlExp)
            {
                //Console.WriteLine("" + oSqlExp.Message);
                //turn null;
            }
            catch (Exception oEx)
            {
                //Console.WriteLine("" + oEx.Message);
                //return null;
            }


        }
        //---------------------------------
        protected internal DataSet getFeatureProductByTitle(int siteid,int contid,string id)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_FeatureProduct_By_Title";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 150, "title");
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = id;
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
        protected internal DataSet getFeatureProductById(int siteid,int contid,int id)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_FeatProduct_By_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = id;
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
        //---------------------------------
        protected DataSet getAllSubject(int siteid,int contid)
        {
            Subjects sub = new Subjects();
            return sub.getAllSubject(siteid,contid);            
        }
        //-------------------------------------------
        protected internal DataSet Get_Site_FeaturedProduct(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Site_FeaturedProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@siteid", SqlDbType.Int);
                cmd.Parameters.Add("@contid", SqlDbType.Int);
                //cmd.Parameters.Add("@subjid", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                //cmd.Parameters[2].Value = subjid;
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
        protected DataSet Get_FeatureProduct_Main(int siteid, int contid)
        {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_FeatureProduct_Main";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SITEID", SqlDbType.Int);
                cmd.Parameters.Add("@CONTID", SqlDbType.Int);

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
        //---------------------------------     
    }
