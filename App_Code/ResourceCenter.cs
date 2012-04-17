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

    public class ResourceCenter:ConnectSql
    {
        public int intResoid;
        public int intSiteid;
        public int intContId;
        public string strResoTitle;
        public string strResoDescription;
        public string strResoLink;
        public int intTempId;
        public int intResoOrPos;

        //---------------------------
         public ResourceCenter()
        {
            intResoid = 0;
            intSiteid = 0;
            intContId = 0;
            strResoTitle = "";
            strResoDescription = "";
            strResoLink = "";
            intTempId = 0;
            intResoOrPos = 0;
        }
        //---------------------------
       public  ResourceCenter(int siteid, int contid, string resotitle, string resodescri, string resolink, int tempid,int Resoordpos)
        {
            this.intSiteid = siteid;
            this.intContId = contid;
            this.strResoTitle = resotitle;
            this.strResoDescription = resodescri;
            this.strResoLink = resolink;
            this.intTempId = tempid;
            this.intResoOrPos = Resoordpos;
        }
       //---------------------------------
       protected internal void add_ResourceCenter(int siteid, int contid, string mainimage, string title1, string content1, string email1, string contact1, string title2,
           string content2, string email2, string contact2, int typelink1, string link1, string image, int typelink2, string link2, string image2,
           int typeMore, string linkMore, int typeMore2, string linkMore2)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_ResourceCenter";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@ResoMainImage", SqlDbType.VarChar, 500, "ResoMainImage");
                cmd.Parameters.Add("@ResoTitle1", SqlDbType.VarChar, 250, "ResoTitle1");
                cmd.Parameters.Add("@ResoContent1", SqlDbType.VarChar, 500, "ResoContent1");
                cmd.Parameters.Add("@ResoEmail1", SqlDbType.VarChar, 250, "ResoEmail1");
                cmd.Parameters.Add("@ResoContact1", SqlDbType.VarChar, 50, "ResoContact1");
                cmd.Parameters.Add("@ResoTitle2", SqlDbType.VarChar, 250, "ResoTitle2");
                cmd.Parameters.Add("@ResoContent2", SqlDbType.VarChar, 500, "ResoContent2");
                cmd.Parameters.Add("@ResoEmail2", SqlDbType.VarChar, 250, "ResoEmail2");
                cmd.Parameters.Add("@ResoContact2", SqlDbType.VarChar, 50, "ResoContact2");
                cmd.Parameters.Add("@Typelink1", SqlDbType.Int);
                cmd.Parameters.Add("@ResoLink1", SqlDbType.VarChar, 500, "ResoLink1");
                cmd.Parameters.Add("@ResoImage", SqlDbType.VarChar, 500, "ResoImage");
                cmd.Parameters.Add("@Typelink2", SqlDbType.Int);
                cmd.Parameters.Add("@ResoLink2", SqlDbType.VarChar, 500, "ResoLink2");
                cmd.Parameters.Add("@ResoImage2", SqlDbType.VarChar, 500, "ResoImage2");
                cmd.Parameters.Add("@ResoMoreLink", SqlDbType.VarChar, 500, "ResoMoreLink");
                cmd.Parameters.Add("@ResoMoreLinkType", SqlDbType.Int);
                cmd.Parameters.Add("@ResoMoreLink2", SqlDbType.VarChar, 500, "ResoMoreLink2");
                cmd.Parameters.Add("@ResoMoreLinkType2", SqlDbType.Int);

                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                cmd.Parameters[2].Value = mainimage;
                cmd.Parameters[3].Value = title1;
                cmd.Parameters[4].Value = content1;
                cmd.Parameters[5].Value = email1;
                cmd.Parameters[6].Value = contact1;
                cmd.Parameters[7].Value = title2;
                cmd.Parameters[8].Value = content2;
                cmd.Parameters[9].Value = email2;
                cmd.Parameters[10].Value = contact2;
                cmd.Parameters[11].Value = typelink1;
                cmd.Parameters[12].Value = link1;
                cmd.Parameters[13].Value = image;
                cmd.Parameters[14].Value = typelink2;
                cmd.Parameters[15].Value = link2;
                cmd.Parameters[16].Value = image2;
                cmd.Parameters[17].Value = linkMore;
                cmd.Parameters[18].Value = typeMore;
                cmd.Parameters[19].Value = linkMore2;
                cmd.Parameters[20].Value = typeMore2;
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
       protected internal DataSet Get_ResourceCenter_MP(int siteid,int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_ResourceCenter_MP ";
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
       protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
        {
            Gen_X gen = new Gen_X();
            return gen.Add_Gen_x(siteid,contid,title, content, image, pos, genetypeId);
        }
        //---------------------------------
        protected DataSet getLandPageById(int id)
        {
            Gen_X gen = new Gen_X();
            return gen.Get_GenericX_By_Id(id);
        }
        //----------------------------------
        protected void Upd_ResourceCenter(int siteid, int contid, string mainimage, string title1, string content1, string email1, string contact1, string title2,
           string content2, string email2, string contact2, int typelink1, string link1, string image, int typelink2, string link2, string image2
            ,int typeMore, string linkMore, int typeMore2, string linkMore2)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Upd_ResourceCenter";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@ResoMainImage", SqlDbType.VarChar, 500, "ResoMainImage");
                cmd.Parameters.Add("@ResoTitle1", SqlDbType.VarChar, 250, "ResoTitle1");
                cmd.Parameters.Add("@ResoContent1", SqlDbType.VarChar, 20000, "ResoContent1");
                cmd.Parameters.Add("@ResoEmail1", SqlDbType.VarChar, 250, "ResoEmail1");
                cmd.Parameters.Add("@ResoContact1", SqlDbType.VarChar, 50, "ResoContact1");
                cmd.Parameters.Add("@ResoTitle2", SqlDbType.VarChar, 250, "ResoTitle2");
                cmd.Parameters.Add("@ResoContent2", SqlDbType.VarChar, 20000, "ResoContent2");
                cmd.Parameters.Add("@ResoEmail2", SqlDbType.VarChar, 250, "ResoEmail2");
                cmd.Parameters.Add("@ResoContact2", SqlDbType.VarChar, 50, "ResoContact2");
                cmd.Parameters.Add("@Typelink1", SqlDbType.Int);
                cmd.Parameters.Add("@ResoLink1", SqlDbType.VarChar, 500, "ResoLink1");
                cmd.Parameters.Add("@ResoImage", SqlDbType.VarChar, 500, "ResoImage");
                cmd.Parameters.Add("@Typelink2", SqlDbType.Int);
                cmd.Parameters.Add("@ResoLink2", SqlDbType.VarChar, 500, "ResoLink2");
                cmd.Parameters.Add("@ResoImage2", SqlDbType.VarChar, 500, "ResoImage2");
                cmd.Parameters.Add("@ResoMoreLink", SqlDbType.VarChar, 500, "ResoMoreLink");
                cmd.Parameters.Add("@ResoMoreLinkType", SqlDbType.Int);
                cmd.Parameters.Add("@ResoMoreLink2", SqlDbType.VarChar, 500, "ResoMoreLink2");
                cmd.Parameters.Add("@ResoMoreLinkType2", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = siteid;
                cmd.Parameters[1].Value = contid;
                if (mainimage == null)
                { cmd.Parameters[2].Value = ""; }
                else { cmd.Parameters[2].Value = mainimage; }
                cmd.Parameters[3].Value = title1;
                cmd.Parameters[4].Value = content1;
                cmd.Parameters[5].Value = email1;
                cmd.Parameters[6].Value = contact1;
                cmd.Parameters[7].Value = title2;
                cmd.Parameters[8].Value = content2;
                cmd.Parameters[9].Value = email2;
                cmd.Parameters[10].Value = contact2;
                cmd.Parameters[11].Value = typelink1;
                cmd.Parameters[12].Value = link1;
                if(image==null)
                {cmd.Parameters[13].Value = "";}else{cmd.Parameters[13].Value = image;}                
                cmd.Parameters[14].Value = typelink2;
                cmd.Parameters[15].Value = link2;
                if (image2 == null)
                {cmd.Parameters[16].Value = "";}else{cmd.Parameters[16].Value = image2;}
                cmd.Parameters[17].Value = linkMore;
                cmd.Parameters[18].Value = typeMore;
                cmd.Parameters[19].Value = linkMore2;
                cmd.Parameters[20].Value = typeMore2;                
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
        //----------------------------------

    }


