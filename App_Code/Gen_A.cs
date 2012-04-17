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
/// Summary description for Gen_A
/// </summary>
public class Gen_A : ConnectSql
{
    public Gen_A()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    protected DataSet get_GenA_By_Id(int id)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_A_byId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
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
    //protected internal DataSet get_GenA_By_Gene_Id(int siteid, int contid, int genid, int geneDefaId)
    protected internal DataSet get_GenA_By_Gene_Id(int siteid, int contid, int genid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_Generic_For_typeId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SiteId", SqlDbType.VarChar, 5, "SiteId");
            cmd.Parameters.Add("@ContId", SqlDbType.VarChar, 5, "ContId");
            cmd.Parameters.Add("@GeneId", SqlDbType.VarChar, 5, "GeneId");
            //cmd.Parameters.Add("@GeneDefaId", SqlDbType.VarChar, 5, "GeneDefaId");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = siteid;
            cmd.Parameters[1].Value = contid;
            cmd.Parameters[2].Value = genid;
            //cmd.Parameters[3].Value = geneDefaId;
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
    protected void Upd_GenaA(int geneID, string geneAtitle, string geneAContent,
                             string geneAimage, int linkTypeId, string geneAlinktitle, string geneAlink,
                             int linkTypeId2, string geneAlink2title, string geneAlink2, int geneordpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Upd_GenA";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@GeneId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneATitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneAContent", SqlDbType.VarChar, 20000, "GeneAContent");
            cmd.Parameters.Add("@GeneAImage", SqlDbType.VarChar, 250, "GeneAImage");
            cmd.Parameters.Add("@LinkTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneALinkTitle", SqlDbType.VarChar, 100, "GeneALinkTitle");
            cmd.Parameters.Add("@GeneALink", SqlDbType.VarChar, 250, "GeneALink");
            cmd.Parameters.Add("@LinkTypeId2", SqlDbType.Int);
            cmd.Parameters.Add("@GeneALink2Title", SqlDbType.VarChar, 100, "GeneALink2Title");
            cmd.Parameters.Add("@GeneAlink2", SqlDbType.VarChar, 250, "GeneAlink2");
            cmd.Parameters.Add("@GeneAOrdPos", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = geneID;
            cmd.Parameters[1].Value = geneAtitle;
            cmd.Parameters[2].Value = geneAContent;
            cmd.Parameters[3].Value = geneAimage;
            cmd.Parameters[4].Value = linkTypeId;
            cmd.Parameters[5].Value = geneAlinktitle;
            cmd.Parameters[6].Value = geneAlink;
            cmd.Parameters[7].Value = linkTypeId2;
            cmd.Parameters[8].Value = geneAlink2title;
            cmd.Parameters[9].Value = geneAlink2;
            cmd.Parameters[10].Value = geneordpos;

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
    //--------------------------------------------------------------------------------------------------
    protected void addGenericA(int siteid, int contid, int geneType, int genedefaid, string geneTitle,
       string geneAtitle, string geneAContent, string geneAimage, int linkTypeId, string geneAlinktitle, string geneAlink
       , int linkTypeId2, string geneAlink2title, string geneAlink2, int geneordpos)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Add_Generic_A";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@SiteId", SqlDbType.Int);
            cmd.Parameters.Add("@ContId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneDefaId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneTitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneATitle", SqlDbType.VarChar, 100, "GeneTitle");
            cmd.Parameters.Add("@GeneAContent", SqlDbType.VarChar, 20000, "GeneAContent");
            cmd.Parameters.Add("@GeneAImage", SqlDbType.VarChar, 250, "GeneAImage");
            cmd.Parameters.Add("@LinkTypeId", SqlDbType.Int);
            cmd.Parameters.Add("@GeneALinkTitle", SqlDbType.VarChar, 100, "GeneALinkTitle");
            cmd.Parameters.Add("@GeneALink", SqlDbType.VarChar, 250, "GeneALink");
            cmd.Parameters.Add("@LinkTypeId2", SqlDbType.Int);
            cmd.Parameters.Add("@GeneALink2Title", SqlDbType.VarChar, 100, "GeneALink2Title");
            cmd.Parameters.Add("@GeneAlink2", SqlDbType.VarChar, 250, "GeneAlink2");
            cmd.Parameters.Add("@GeneAOrdPos", SqlDbType.Int);

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
            cmd.Parameters[5].Value = geneAtitle;
            cmd.Parameters[6].Value = geneAContent;
            cmd.Parameters[7].Value = geneAimage;
            cmd.Parameters[8].Value = linkTypeId;
            cmd.Parameters[9].Value = geneAlinktitle;
            cmd.Parameters[10].Value = geneAlink;
            cmd.Parameters[11].Value = linkTypeId2;
            cmd.Parameters[12].Value = geneAlink2title;
            cmd.Parameters[13].Value = geneAlink2;
            cmd.Parameters[14].Value = geneordpos;
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
    protected int addLandPage(int siteid, int contid, string title, string content, string image, int pos, int genetypeId)
    {
        Gen_X gen = new Gen_X();
        return gen.Add_Gen_x(siteid, contid, title, content, image, pos, genetypeId);
    }
    //---------------------------------
    protected DataSet getLandPageById(int id)
    {
        Gen_X gen = new Gen_X();
        return gen.Get_GenericX_By_Id(id);
    }
    //----------------------------------

    //Updates the landing page.
    protected internal void updLandPage(int siteid, string title, string content, string image)
    {
        Gen_X gen = new Gen_X();
        gen.Upd_GenX_By_GeneId(siteid, title, content, image);
    }
    //-----------------------------------------------
}


