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


    public class Highlights:ConnectSql
    {
        public int intSiteid;
        public int intContId;
        public int intHighId;
        public string strHighTitle;
        public string strHighAlt;
        public string strHighFile;
        public string strHighLink;
        public int intHighOrdPos;
        public int intHighState;

         public Highlights()
        {
            intSiteid = 0;
            intContId = 0;
            intHighId = 0;
            strHighTitle = "";
            strHighAlt = "";
            strHighFile = "";
            strHighLink = "";
            intHighOrdPos = 0;
            intHighState = 0;

        }
        //-------------------------------
        public Highlights(int siteid, int contid, int highid,string hight,string highAlt, string highf, string highl,int highord, int highs)
        {
            this.intSiteid = siteid;
            this.intContId = contid;
            this.intHighId = highid;
            this.strHighTitle = hight;
            this.strHighAlt = highAlt;
            this.strHighFile = highf;
            this.strHighLink = highl;
            this.intHighOrdPos = highord;
            this.intHighState = highs;
        }
        //-------------------------------
        protected internal DataSet getAllHighlights(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Highlights";
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
        protected string updateHighlights(Highlights highl)
        {
            
            string ids = "";
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Upd_HighLights";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@HighId", SqlDbType.Int);
                cmd.Parameters.Add("@HighTitle", SqlDbType.VarChar, 150, "HighTitle");
                cmd.Parameters.Add("@HighAlt", SqlDbType.VarChar, 500, "HighAlt");
                cmd.Parameters.Add("@HighFile", SqlDbType.VarChar, 250, "HighFile");
                cmd.Parameters.Add("@HighLink", SqlDbType.VarChar, 250, "HighLink");
                cmd.Parameters.Add("@HighOrdPos", SqlDbType.Int);
                cmd.Parameters.Add("@HingState", SqlDbType.Int);

                //Setting values to Parameters.
                cmd.Parameters[0].Value = highl.intSiteid;
                cmd.Parameters[1].Value = highl.intContId;
                cmd.Parameters[2].Value = highl.intHighId;
                cmd.Parameters[3].Value = highl.strHighTitle;
                cmd.Parameters[4].Value = highl.strHighAlt;
                cmd.Parameters[5].Value = highl.strHighFile;
                cmd.Parameters[6].Value = highl.strHighLink;
                cmd.Parameters[7].Value = highl.intHighOrdPos;
                cmd.Parameters[8].Value = highl.intHighState;
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
        protected string deleteHighlights(int HighId)
        {
            string ids = "";
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Del_Highlights";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@HighId", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = HighId;
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
        public void addHighlights(Highlights highl)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_Highlights";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@HighTitle", SqlDbType.VarChar, 150, "HighTitle");
                cmd.Parameters.Add("@HighAlt", SqlDbType.VarChar, 500, "HighAlt");
                cmd.Parameters.Add("@HighFile", SqlDbType.VarChar, 250, "HighFile");
                cmd.Parameters.Add("@HighLink", SqlDbType.VarChar, 250, "HighLink");
                cmd.Parameters.Add("@HighOrdPos", SqlDbType.Int);
                cmd.Parameters.Add("@HingState", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = highl.intSiteid;
                cmd.Parameters[1].Value = highl.intContId;
                cmd.Parameters[2].Value = highl.strHighTitle;
                cmd.Parameters[3].Value = highl.strHighAlt;
                cmd.Parameters[4].Value = highl.strHighFile;
                cmd.Parameters[5].Value = highl.strHighLink;
                cmd.Parameters[6].Value = highl.intHighOrdPos;
                cmd.Parameters[7].Value = highl.intHighState;
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
        public void browseImage()
        {
        }
        //---------------------------------
        protected DataSet getHighLight_By_id(int id)
        {
            try
            {
                Open(); DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_HighLights_By_Id";
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
        protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
        {
            Gen_X gen = new Gen_X();
            return gen.Add_Gen_x(siteid,contid,title, content, image, pos,genetypeId);
        }

        protected DataSet getLandPageById(int id)
        {
            Gen_X gen = new Gen_X();
            return gen.Get_GenericX_By_Id(id);
        }
        //--------------------------------------------------------------------
        protected DataSet Upd_HighPosition(int id, int pos)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Upd_HighPosition";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HighId", SqlDbType.Int);
                cmd.Parameters.Add("@HighOrdPos", SqlDbType.Int);
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

        
      }
