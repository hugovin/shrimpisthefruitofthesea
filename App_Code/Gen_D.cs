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

/// <summary>
/// Summary description for Gen_D
/// </summary>
public class Gen_D : ConnectSql
{
    public Gen_D()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //------------------------------------------------------------------
    protected internal DataSet Get_Site_Generic_D(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Generic_D";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
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
    //---------------------------------------------------------------------
    protected internal DataSet get_GenericD_ById(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GenericD_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@GeneDId", SqlDbType.VarChar, 5, "GeneAId");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = id;
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
    //---------------------------------------------------------------------
    protected internal DataSet get_All_genericsd(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_All_GeneD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@GeneId", SqlDbType.VarChar, 5, "GeneAId");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = id;
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
    //---------------------------------------------------------------------
    protected internal void Add_genericD(int siteid, int contid, int geneid, int geneType, int GeneDefaId, string geneTitle,
                                         string GeneDTitle, string GeneDContent, int linktype, string GeneDLinkTitle, string GeneDLink, string GeneDFile, int GeneDOrdPos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Generic_D";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneDTitle", SqlDbType.VarChar, 150, "GeneDTitle");
            cmd.Parameters.Add("@GeneDContent", SqlDbType.VarChar, 20000, "GeneAImage");
            cmd.Parameters.Add("@LinkTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDLinkTitle", SqlDbType.VarChar, 150, "GeneDLinkTitle");
            cmd.Parameters.Add("@GeneDLink", SqlDbType.VarChar, 250, " GeneDLink");
            cmd.Parameters.Add("@GeneDFile", SqlDbType.VarChar, 250, "GeneDFile");
            cmd.Parameters.Add("@GeneDOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            if (contid != 0)
            {
                cmd.Parameters[1].Value = contid;
            }
            else
            {
                cmd.Parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            cmd.Parameters[2].Value = geneid;
            cmd.Parameters[3].Value = geneType;
            cmd.Parameters[4].Value = GeneDefaId;
            cmd.Parameters[5].Value = geneTitle;
            cmd.Parameters[6].Value = GeneDTitle;
            cmd.Parameters[7].Value = GeneDContent;
            cmd.Parameters[8].Value = linktype;
            cmd.Parameters[9].Value = GeneDLinkTitle;
            cmd.Parameters[10].Value = GeneDLink;
            cmd.Parameters[11].Value = GeneDFile;
            cmd.Parameters[12].Value = GeneDOrdPos;
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
    //--------------------------------------------------------------------
    protected internal string deleteGenericDbyId(int siteid, int geneId, int genedid)
    {
        string ids = "";
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Del_GenD_By_Id";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = geneId;
            cmd.Parameters[2].Value = genedid;
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
    //--------------------------------------------------------------------
    protected internal void Upd_genericD(int SiteId, int GeneId, int GeneDID, string GeneDTitle, string GeneDContent, int linktype,
                             string GeneDLinkTitle, string GeneDLink, string GeneDFile, int GeneDOrdPos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenD";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDTitle", SqlDbType.VarChar, 150, "GeneDTitle");
            cmd.Parameters.Add("@GeneDContent", SqlDbType.VarChar, 20000, "GeneAImage");
            cmd.Parameters.Add("@LinkTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDLinkTitle", SqlDbType.VarChar, 150, "GeneDLinkTitle");
            cmd.Parameters.Add("@GeneDLink", SqlDbType.VarChar, 250, " GeneDLink");
            cmd.Parameters.Add("@GeneDFile", SqlDbType.VarChar, 250, "GeneDFile");
            cmd.Parameters.Add("@GeneDOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = SiteId;
            cmd.Parameters[1].Value = GeneId;
            cmd.Parameters[2].Value = GeneDID;
            cmd.Parameters[3].Value = GeneDTitle;
            cmd.Parameters[4].Value = GeneDContent;
            cmd.Parameters[5].Value = linktype;
            cmd.Parameters[6].Value = GeneDLinkTitle;
            cmd.Parameters[7].Value = GeneDLink;
            cmd.Parameters[8].Value = GeneDFile;
            cmd.Parameters[9].Value = GeneDOrdPos;
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
    //--------------------------------------------------------------------
    protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
    {
        Gen_X gen = new Gen_X();
        return gen.Add_Gen_x(siteid, contid, title, content, image, pos, genetypeId);
    }
    //--------------------------------------------------------------------
    protected DataSet getLandPageById(int id)
    {
        Gen_X gen = new Gen_X();
        return gen.Get_GenericX_By_Id(id);
    }
    //------------------------------------------------------------------
    protected internal DataSet Get_Generic_D_By_Type(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_D_By_Type";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = id;
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
    //---------------------------------------------------------------------    
    protected internal DataSet Get_Generic_Resources_Purchasing(int siteid, int contid, int gendid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_Resources_Purchasing";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);            
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = gendid;            
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
    //Updates the landing page.
    protected internal void updLandPage(int siteid, string title, string content, string image)
    {
        Gen_X gen = new Gen_X();
        gen.Upd_GenX_By_GeneId(siteid, title, content, image);
    }
    //-----------------------------------------------

    //Obtain generic section title, to add multiple templates to the same section
    protected internal DataSet Get_Generic_Name(int genericId)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_Name";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = genericId;
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



