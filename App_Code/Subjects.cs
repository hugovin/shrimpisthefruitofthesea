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


public class Subjects : ConnectSql
{
    //public int intSiteId { get; set; }
    //public int intContId { get; set; }
    //public int intSubjId { get; set; }
    //public int intCategoryId { get; set; }

    public int intSiteId;// { get; set; }
    public int intContId;// { get; set; }
    public int intSubjId;// { get; set; }
    public int intCategoryId;// { get; set; }
        
        #region Constructor
        public Subjects()
        {
            intSiteId = 0;
            intContId = 0;
            intSubjId = 0;
            intCategoryId = 0;
        }
        public Subjects(int siteid, int contid, int subid, int catid)
        {
            this.intSiteId = siteid;
            this.intContId = contid;
            this.intSubjId = subid;
            this.intCategoryId = catid;
        }
        #endregion

        //---------------------------------
        protected internal DataSet getAllSubject(int siteId,int contId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_Subject";
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
        //-------------------------------------------
        protected string updateSubject(Subjects subj)
        {
       
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_Subject";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@SubjId", SqlDbType.Int);
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = subj.intSiteId;
            cmd.Parameters[1].Value = subj.intContId;
            cmd.Parameters[2].Value = subj.intSubjId;
            cmd.Parameters[3].Value = subj.intCategoryId;

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
        //-------------------------------------------------
        protected string deleteSubjects(int SubjId)
        {

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_Subject";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@FeatId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SubjId;
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
        protected void addSubject(int site, int cont, int sub, int pos)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_Subject";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                cmd.Parameters.Add("@SubCategoryId", SqlDbType.Int);
                cmd.Parameters.Add("@SubjOrdPos", SqlDbType.Int);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = site;
                cmd.Parameters[1].Value = cont;
                cmd.Parameters[2].Value = sub;
                cmd.Parameters[3].Value = pos;
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
        protected DataSet getAllSubSubjects(int id)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Get_All_SubjectSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@subjid", SqlDbType.Int);
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
        //--------------------------------
        protected void addSubSubject(int subjid,int subsubcategory,bool substate)
        {
            try
            {
                Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                //Procedure Name.
                cmd.CommandText = "Add_SubSubject_Category ";
                cmd.CommandType = CommandType.StoredProcedure;
                //Procedure Parameters.
                cmd.Parameters.Add("@SubjId", SqlDbType.Int);
                cmd.Parameters.Add("@SubjSubCategoryId", SqlDbType.Int);
                cmd.Parameters.Add("@SubjSubCateState", SqlDbType.Bit);
                //Setting values to Parameters.
                cmd.Parameters[0].Value = subjid;
                cmd.Parameters[1].Value = subsubcategory;
                cmd.Parameters[2].Value = substate;

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
        protected void Del_All_SubjectSubCategory(int SubjId)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Del_All_SubjectSubCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.Parameters.Add("@SubjId", SqlDbType.Int);
                cmd.Parameters[0].Value = SubjId;
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
        //--------------------------------------
        protected internal void deleteAllSubjects(int siteid, int contid)
        {
            try
            {
                Open();
                DataSet dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = DataBase;
                cmd.CommandText = "Del_All_Subjects";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                cmd.Parameters.Add("@ContId", SqlDbType.Int);
                //Setting values to Parameters.
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
 }