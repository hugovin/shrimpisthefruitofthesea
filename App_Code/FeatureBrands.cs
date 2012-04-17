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

    public class FeatureBrands:ConnectSql
    {
        public int intSiteId;
        public int intContId;
        public int intTitleID;
        public string strFeatFile;
        public int intFeatOrdPos;
        public bool blFeatState;

        //---------------------------------
        public FeatureBrands()
        {
            intSiteId = 0;
            intContId = 0;
            intTitleID = 0;
            intFeatOrdPos = 0;
            blFeatState = true;
        }
        //---------------------------------
        public FeatureBrands(int siteid, int contid, int featti, string featfile, int featord, bool featstate)
        {
            this.intSiteId = siteid;
            this.intContId = contid;
            this.intTitleID = featti;
            this.strFeatFile = featfile;
            this.intFeatOrdPos = featord;
            this.blFeatState = featstate;
        }
        //---------------------------------
        protected internal DataSet getAllFeatureBrands(int siteId,int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_FeaturedBrands";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteId;
                cmd.Parameters[1].Value = contId;
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
        protected string updateFeatureBrands(FeatureBrands FeatB)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_FeaturedBrands";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@TitleID", SqlDbType.VarChar, 250, "FeatTitle");
            cmd.Parameters.Add("@FeatOrdPos", SqlDbType.VarChar, 250, "FeatFile");
            cmd.Parameters.Add("@FeatState", SqlDbType.VarChar, 250, "FeatLink");

            //Setting values to Parameters.
            cmd.Parameters[0].Value = FeatB.intSiteId;
            cmd.Parameters[1].Value = FeatB.intContId;
            cmd.Parameters[3].Value = FeatB.intTitleID;
            cmd.Parameters[4].Value = FeatB.intFeatOrdPos;
            cmd.Parameters[5].Value = FeatB.blFeatState;

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
         protected internal string deleteFeatureBrands(int featid)
        {
            string ids = "";
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Del_FeaturedBrands";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@FeatId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = featid;
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
            return ids;
        
        }
        //---------------------------------
         public void addFeatureB(FeatureBrands FeatB)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_FeaturedBrands";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleID", SqlDbType.Int);
                cmd.Parameters.Add("@FeatFile", SqlDbType.VarChar, 250, "FeatFile");
                cmd.Parameters.Add("@FeatOrdPos", SqlDbType.Int);
                cmd.Parameters.Add("@FeatState", SqlDbType.Bit);

                //Setting values to Parameters.
                cmd.Parameters[0].Value = FeatB.intSiteId;
                cmd.Parameters[1].Value = FeatB.intContId;
                cmd.Parameters[2].Value = FeatB.intTitleID;
                cmd.Parameters[3].Value = FeatB.strFeatFile;
                cmd.Parameters[4].Value = FeatB.intFeatOrdPos;
                cmd.Parameters[5].Value = FeatB.blFeatState;
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
        //-----------------------------------------------------------------
        protected internal DataSet getFeatureBrandsById(int titleID)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_FeatureBrand_By_Id";
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
        protected internal DataSet getFeatureBrandsByTitle(string id)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Brands_By_Title";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 150, "title");
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
        //-------------------------------------------------------
        protected internal DataSet getAllSubject(int siteid,int contId)
        {
            Subjects sub = new Subjects();
            return sub.getAllSubject(siteid,contId);
        }
        //-------------------------------------------
        protected internal DataSet Get_Site_FeaturedBrands(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Site_FeaturedBrands";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@siteid", SqlDbType.Int);
                cmd.Parameters.Add("@contid", SqlDbType.Int);
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
        //--------------------------------------------------------------------
        protected DataSet Upd_BrandsPositions(int id, int pos)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Upd_BrandsPositions";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FeatId", SqlDbType.Int);
                cmd.Parameters.Add("@FeatOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = id;
                cmd.Parameters[1].Value = pos;
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

        //--------------------------------------------------------------------
    }

