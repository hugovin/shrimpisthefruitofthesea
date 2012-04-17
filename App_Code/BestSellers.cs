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

public class BestSellers:ConnectSql
 {
    private int intSiteid;
    private int intContid;
    private int intSubjid;
    private int intBestID;
    private int intTitleID;
    private int intBestOrdPos;
    private bool BestState;


        public BestSellers()
        {
            intSiteid = 0;
            intContid = 0;
            intSubjid = 0;
            intBestID = 0;
            intTitleID = 0;
            intBestOrdPos = 0;
            BestState = true;
            
        }
        //-----------------------------------
        public BestSellers(int siteid, int contid, int subjid,int bestid,int titleid,int bestordpos,bool beststate)
        {
            this.intSiteid = siteid;
            this.intContid = contid;
            this.intSubjid = subjid;
            this.intBestID = bestid;
            this.intTitleID = titleid;
            this.intBestOrdPos = bestordpos;
            this.BestState = beststate;
        }
        //-------------------------------
        protected DataSet Get_All_BestSellers_By_ID(int siteid, int contid, int id)
        {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_BestSellers_By_ID ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SITEID", SqlDbType.Int);
                cmd.Parameters.Add("@CONTID", SqlDbType.Int);
                cmd.Parameters.Add("@SUBJID", SqlDbType.Int);
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
        protected string deleteBestSellers(int siteid,int contid,int id)
        {
            string ids = "";
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Del_BestSellers";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@BestId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = id;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    { ids = dr["id"].ToString(); }
                }
                dr.Close();
                dr.Dispose();
                return ids;
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
        protected void addBestSellers(int siteid, int contid, int subjid, int titleid, bool Besthome, int pos)
        {
            try
            {
                Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_BestSeller";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@SubId", SqlDbType.Int);                
                cmd.Parameters.Add("@TitleID", SqlDbType.Int);
                cmd.Parameters.Add("@BestHome", SqlDbType.Int);
                cmd.Parameters.Add("@BestOrdPos", SqlDbType.Int);

                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                if (subjid!= 0)
                {
                    cmd.Parameters[2].Value = subjid;
                }
                else
                {
                    cmd.Parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                cmd.Parameters[3].Value = titleid;
                cmd.Parameters[4].Value = Besthome;
                cmd.Parameters[5].Value = pos;

                cmd.ExecuteNonQuery();

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
        //---------------------------------
        protected internal DataSet getProductByTitle(int siteid,int contid, string id)
        {
            FeaturedProducts feat = new FeaturedProducts();
            return feat.getFeatureProductByTitle(siteid, contid, id);

        }
        //---------------------------------
        protected internal DataSet getProductById(int id)
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
        //---------------------------------
    ///--------------------------------------------
        protected DataSet getAllSubject(int siteid,int contid)
        {
            Subjects sub = new Subjects();
            return sub.getAllSubject(siteid,contid);           
        }
     //-------------------------------------------
        //-------------------------------------------
        protected internal DataSet Get_Site_BestSellers(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Site_BestSellers";
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
        protected DataSet Get_BestSellers_Main(int siteid, int contid)
        {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_BestSellers_Main ";
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
        protected DataSet getProductTitle(int siteid,int contid,int id)
        {
            FeaturedProducts fea = new FeaturedProducts();
            return fea.getFeatureProductById(siteid,contid,id);
        }

}
