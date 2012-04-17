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

    public class Specials:ConnectSql
    {
        public int intSiteid;
        public int intContid;
        public int intSubjid;
        public int intSpecid;
        public int intTitleID;
        public int intSpecOrdPos;
        public bool blSpecState;

        //-------------------------------------
        public Specials()
        {
            intSiteid = 0;
            intContid = 0;
            intSubjid = 0;
            intSpecid = 0;
            intTitleID = 0;
            intSpecOrdPos = 0;
             blSpecState = true;
           
        }
        //-------------------------------------
        public Specials(int siteid, int contid, int subjid, int specid, int spect, int specpos, bool specstate)
        {
            this.intSiteid = siteid;
            this.intContid = contid;
            this.intSubjid = subjid;
            this.intSpecid = specid;
            this.intTitleID = spect;
            this.intSpecOrdPos = specpos;
            this.blSpecState = specstate;
         }
        //---------------------------------
        protected DataSet getAllSpecials()
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Specials";
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
        //-------------------------------------
        protected string updateSpecials(Specials spec)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Specials";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SubjId", SqlDbType.Int);
            cmd.Parameters.Add("@SpecId", SqlDbType.Int);
            cmd.Parameters.Add("@SpecTitle", SqlDbType.VarChar, 150, "SpecTitle");
            cmd.Parameters.Add("@SpecContent", SqlDbType.VarChar, 500, "SpecContent");
                   

            //Setting values to Parameters.
            cmd.Parameters[0].Value = spec.intSiteid;
            cmd.Parameters[1].Value = spec.intContid;
            cmd.Parameters[2].Value = spec.intSubjid;
            cmd.Parameters[3].Value = spec.intSpecid;
            cmd.Parameters[4].Value = spec.intTitleID;
            cmd.Parameters[5].Value = spec.intSpecOrdPos;
           

            string ids = "";
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
        //---------------------------------

        public void addSpecial(Specials spec)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_Specials";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@SubjId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleID", SqlDbType.Int);
                cmd.Parameters.Add("@SpecOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = spec.intSiteid;
                cmd.Parameters[1].Value = spec.intContid;
                if (spec.intSubjid != 0)
                {
                    cmd.Parameters[2].Value = spec.intSubjid;
                }
                else
                {
                    cmd.Parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                cmd.Parameters[3].Value = spec.intTitleID;
                cmd.Parameters[4].Value = spec.intSpecOrdPos;


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
        protected internal DataSet getAllSubject(int siteid,int contid)
        {
            Subjects sub = new Subjects();
            return sub.getAllSubject(siteid, contid);    
        }
        //---------------------------------
        protected DataSet getAllSpecialsBySubjectID(string siteid, string contid, string id)
        {

            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Specials_By_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SITEID", SqlDbType.VarChar, 5, "SITEID");
                cmd.Parameters.Add("@CONTID", SqlDbType.VarChar, 5, "CONTID");
                cmd.Parameters.Add("@SUBJID", SqlDbType.VarChar, 5, "SUBJID");
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
        protected DataSet getSpecialByTitle(int siteid, int contid,string id)
        {
            FeaturedProducts title = new FeaturedProducts();
            return title.getFeatureProductByTitle(siteid,contid, id);

        }
        //---------------------------------
        protected internal DataSet getSpecialById(int siteid,int contid, int id)
        {
            FeaturedProducts title = new FeaturedProducts();
            return title.getFeatureProductById(siteid,contid,id); 
        }
        //---------------------------------     
        protected string deleteSpecials(int productid)
        {
            Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Special";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SpecId", SqlDbType.Int);
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


    }
