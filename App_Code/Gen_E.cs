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
/// Summary description for Gen_E
/// </summary>
public class Gen_E:ConnectSql
{
	public Gen_E()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //--------------------------------------------------------------------------------------------------
    protected void addGenericE(int siteid, int contid, int geneType,int genedefaid, string geneTitle,
       string geneEtitle,string geneElocation, string geneEContent, int linktype, string geneElinktitle, string geneElink
       , int geneordpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Generic_E";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneETitle", SqlDbType.VarChar, 100, "GeneETitle");
            cmd.Parameters.Add("@GeneELocation", SqlDbType.VarChar, 500, "GeneELocation");
            cmd.Parameters.Add("@GeneEContent", SqlDbType.VarChar, 20000, "GeneEContent");
            cmd.Parameters.Add("@LinkType", SqlDbType.Int);
            cmd.Parameters.Add("@GeneELinkTitle", SqlDbType.VarChar, 100, "GeneELinkTitle");
            cmd.Parameters.Add("@GeneELink", SqlDbType.VarChar, 250, "GeneELink");
            cmd.Parameters.Add("@GeneEOrdPos", SqlDbType.Int);

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
            cmd.Parameters[2].Value = geneType;
            cmd.Parameters[3].Value = genedefaid;
            cmd.Parameters[4].Value = geneTitle;
            cmd.Parameters[5].Value = geneEtitle;
            cmd.Parameters[6].Value = geneElocation;
            cmd.Parameters[7].Value = geneEContent;
            cmd.Parameters[8].Value = linktype;
            cmd.Parameters[9].Value = geneElinktitle;
            cmd.Parameters[10].Value = geneElink;
            cmd.Parameters[11].Value = geneordpos;
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
    //--------------------------------------------------------------------
    protected int addLandPage(int siteid,int contid,string title, string content, string image, int pos, int genetypeId)
    {
        Gen_X gen = new Gen_X();
        return gen.Add_Gen_x(siteid,contid,title, content, image, pos, genetypeId);
    }
    //------------------------------------------------------------------
    protected internal DataSet Get_Site_Generic_E(int siteid, int contid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Site_Generic_E";
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
    //--------------------------------------------------------------------
    protected DataSet Get_GenericE_By_ID(int siteid,int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_GenericE_By_ID";
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
    protected internal DataSet Get_Generic_E_By_Type(int siteid, int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_E_By_Type";
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
    //--------------------------------------------------------------------------------------------------
    protected void Upd_GenericE(int genericEid,string geneEtitle, string geneElocation, string geneEContent, int linktype, string geneElinktitle, string geneElink)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenE";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneETitle", SqlDbType.VarChar, 100, "GeneETitle");
            cmd.Parameters.Add("@GeneELocation", SqlDbType.VarChar, 500, "GeneELocation");
            cmd.Parameters.Add("@GeneEContent", SqlDbType.VarChar, 20000, "GeneEContent");
            cmd.Parameters.Add("@LinkType", SqlDbType.Int);
            cmd.Parameters.Add("@GeneELinkTitle", SqlDbType.VarChar, 100, "GeneELinkTitle");
            cmd.Parameters.Add("@GeneELink", SqlDbType.VarChar, 250, "GeneELink");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = genericEid;
            cmd.Parameters[1].Value = geneEtitle;
            cmd.Parameters[2].Value = geneElocation;
            cmd.Parameters[3].Value = geneEContent;
            cmd.Parameters[4].Value = linktype;
            cmd.Parameters[5].Value = geneElinktitle;
            cmd.Parameters[6].Value = geneElink;
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
    //--------------------------------------------------------------------

    //Updates the landing page.
    protected internal void updLandPage(int siteid, string title, string content, string image)
    {
        Gen_X gen = new Gen_X();
        gen.Upd_GenX_By_GeneId(siteid, title, content, image);
    }
    //-----------------------------------------------
}
