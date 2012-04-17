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

    public class Featured:ConnectSql
    {
        #region attributes

        public int intSiteId;
        public int intContId;
        public int intFeatId;
        public int intFeatOrdPos;
        public string strFeatTitle;
        public string strFeatFile;
        public string strFeatAlt;
        public string strFeatLink;
        public DateTime dtFeatFrom;
        public DateTime dtFeatTo;
        public bool blFeatActive;
        public bool bltFeatState;
        #endregion 
        #region Constructors
        //---------------------------------
        public Featured()
        {
            intSiteId = 0;
            intContId = 0;
            intFeatId = 0;
            intFeatOrdPos = 0;
            strFeatTitle = "";
            strFeatFile = "";
            strFeatAlt = "";
            strFeatLink = "";

        }
        //---------------------------------
        public Featured(int siteid, int contid, int featid,int featPos,string feattitle, string featFile
                       ,string featalt,string featlink, DateTime featf, DateTime featt, bool featA,bool featS)
        {
            this.intSiteId = siteid;
            this.intContId = contid;
            this.intFeatId = featid;
            this.intFeatOrdPos = featPos;
            this.strFeatTitle = feattitle;
            this.strFeatFile = featFile;
            this.strFeatAlt = featalt;
            this.strFeatLink = featlink;
            this.dtFeatFrom = featf;
            this.dtFeatTo = featt;
            this.blFeatActive = featA;
            this.bltFeatState = featS;
        }
        #endregion
        
        #region  Methods
        //---------------------------------------
        protected internal DataSet getAllFeature(int siteId, int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Featured";
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
        
        protected internal DataSet getAllFeatureHome(int siteId, int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Featured_Home";
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
        protected string updatefeature(int siteid, int contid,int featId,string fftitle, string ffile,string alt, DateTime fFrom, DateTime fto, bool factive, bool fState,string link)
        {
            string ids = "";
            try
            {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Featured";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@FeatId", SqlDbType.Int);
            cmd.Parameters.Add("@FeatTitle", SqlDbType.VarChar, 250, "FeatTitle");
            cmd.Parameters.Add("@FeatFile", SqlDbType.VarChar, 250, "FeatFile");
            cmd.Parameters.Add("@FeatAlt", SqlDbType.VarChar, 250, "FeatAlt");
            cmd.Parameters.Add("@FeatFrom", SqlDbType.SmallDateTime);
            cmd.Parameters.Add("@FeatTo", SqlDbType.SmallDateTime);
            cmd.Parameters.Add("@FeatActive", SqlDbType.Int);
            cmd.Parameters.Add("@FeatState", SqlDbType.Int);
            cmd.Parameters.Add("@FeatLink", SqlDbType.VarChar, 250, "FeatLink");

            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = featId;
            cmd.Parameters[3].Value = fftitle;
            cmd.Parameters[4].Value = ffile;
            cmd.Parameters[5].Value = alt;
            cmd.Parameters[6].Value = fFrom;
            cmd.Parameters[7].Value = fto;
            cmd.Parameters[8].Value = factive;
            cmd.Parameters[9].Value = fState;
            cmd.Parameters[10].Value = link;

   
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
                return null;
            }
            catch (Exception oEx)
            {
                //Console.WriteLine("" + oEx.Message);
                return null;
            }
            return ids;
        }
        //---------------------------------
        protected string deleteFeature(int siteid,int contid,int featid)
        {
            string ids = "";
            try
            {
                Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Del_Featured";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@FeatId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = featid;
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
                return null;
            }
            catch (Exception oEx)
            {
                //Console.WriteLine("" + oEx.Message);
                return null;
            }
            return ids;
        }
        //---------------------------------
        protected void addFeature(int siteid, int contid, int featPos, string feattitle, string featFile
                       , string featalt, string featlink, DateTime featf, DateTime featt, bool featA, bool featS)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_Feature";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@FeatOrdPos", SqlDbType.Int);
                cmd.Parameters.Add("@FeatTitle", SqlDbType.VarChar, 250, "FeatTitle");
                cmd.Parameters.Add("@FeatFile", SqlDbType.VarChar, 250, "FeatFile");
                cmd.Parameters.Add("@FeatAlt", SqlDbType.VarChar, 250, "FeatAlt");
                cmd.Parameters.Add("@FeatLink", SqlDbType.VarChar, 250, "FeatLink");
                cmd.Parameters.Add("@FeatFrom", SqlDbType.SmallDateTime);
                cmd.Parameters.Add("@FeatTo", SqlDbType.SmallDateTime);
                cmd.Parameters.Add("@FeatActive", SqlDbType.Bit);
                cmd.Parameters.Add("@FeatState", SqlDbType.Bit);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = featPos;
                cmd.Parameters[3].Value = feattitle;
                cmd.Parameters[4].Value = featFile;
                cmd.Parameters[5].Value = featalt;
                cmd.Parameters[6].Value = featlink;
                cmd.Parameters[7].Value = featf;
                cmd.Parameters[8].Value = featt;
                cmd.Parameters[9].Value = featA;
                cmd.Parameters[10].Value = featS;
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
        //---------------------------------
        protected void browseImage()
        {
        }
        //--------------------------------
        protected DataSet getFeatureById(int id)
        {
           try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Feature_By_ID";
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

        //--------------------------------------------------------------------
        protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
        {
            Gen_X gen = new Gen_X();
            return gen.Add_Gen_x(siteid,contid,title, content, image, pos, genetypeId);
        }
        //--------------------------------------------------------------------
        protected DataSet Upd_TheaterPositions(int id,int pos)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Upd_TheaterPositions";
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
#endregion
    }



