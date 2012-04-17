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

public class WhatsNew:ConnectSql
{
    public int intSiteId;
    public int intContId;
    public int intWhatId;
    public int intTitleID;
    public int intWhatOrdPos;
    public bool blWhatState;
    public int intwhatHome;
      
        
        #region constructor
        public WhatsNew()
        {
            intSiteId = 0;
            intContId = 0;
            intWhatId = 0;
            intTitleID  = 0;
            intWhatOrdPos= 0;
            blWhatState  = false;
            intwhatHome = 0;
            
        }
        //-----------------------------------
        public WhatsNew(int siteid, int contid, int whatt,int whathome, int whato, bool whats)
        {
            this.intSiteId = siteid;
            this.intContId = contid;
            this.intTitleID = whatt;
            this.intWhatOrdPos = whato;
            this.blWhatState = whats;
            this.intwhatHome = whathome;
        }
        #endregion

        protected DataSet getAllWhatsNew(int siteId, int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_WhatsNew";
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
        //--------------------------------------------------------   
        protected DataSet getWhatsNewById(int siteid,int contid,int titleID)
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
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = titleID;
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
        protected DataSet getWhatsNewByTitle(int siteid,int contid,string id)
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
       //--------------------------------------
        protected string deleteWhatsNew(int featid)
        {
            string ids = "";
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Del_WhatsNew";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@WhatId", SqlDbType.Int);
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
        public void addWhatsNew(WhatsNew WN)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_WhatsNew";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@TitleID", SqlDbType.Int);
                cmd.Parameters.Add("@WhatHome", SqlDbType.Int);
                cmd.Parameters.Add("@WhatOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = WN.intSiteId;
                cmd.Parameters[1].Value = WN.intContId;
                cmd.Parameters[2].Value = WN.intTitleID;
                cmd.Parameters[3].Value = WN.intwhatHome;
                cmd.Parameters[4].Value = WN.intWhatOrdPos;
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
    //---------------------------------
        protected void updateHomeWhatsNew(int siteid,int contid,int whatid,bool Home)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Upd_WhatsNew";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@WhatId", SqlDbType.Int);
                cmd.Parameters.Add("@WhatHome", SqlDbType.Bit);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = whatid;
                cmd.Parameters[3].Value = Home;
                cmd.ExecuteNonQuery();
                Close();
            }
            catch (SqlException oSqlExp)
            {
                //Console.WriteLine("" + oSqlExp.Message);
                //return null;
            }
            catch (Exception oEx)
            {
                //Console.WriteLine("" + oEx.Message);
                //return null;
            }
        }
        //---------------------------------
        protected internal DataSet Get_Site_WhatsNew(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_Site_WhatsNew";
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
    //--------------------------------------------------------
        protected DataSet Get_WhatsNewSide(int siteId, int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_WhatsNewSide";
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
        //--------------------------------------------------------
        protected internal DataSet Get_All_Whats_New_Product(int siteId, int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Whats_New_Product";
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
}


