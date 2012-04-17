using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class mnt_Generics : Generics
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["DeleteId"] != null)
        {
            Del_Generic_By_Id(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["DeleteId"]));
            Response.Redirect("mnt_Generics.aspx?Generic=" + Request["Type"]);
        }
        if (Request["RenameId"] != null)
        {
            string nameGene = "";
            DataSet update = new DataSet();
            update = Get_Generic_Name(Convert.ToInt32(Request["RenameId"]));
            foreach (DataTable table1 in update.Tables)
            {
                foreach (DataRow row1 in table1.Rows)
                {
                    nameGene = row1["GeneTitle"].ToString();
                }
            }
            Response.Redirect("mnt_Generics.aspx?Generic="+Request["Type"]+"&Rename="+nameGene+"&Id="+Convert.ToInt32(Request["RenameId"]));
        }

        if (Request["NewName"] != null)
        {
            Upd_GenericTitle(Convert.ToInt32(Request["GeneId"]), Request["NewName"].ToString());
            Response.Redirect("mnt_Generics.aspx?Generic=" + Request["Type"]);

        }
    }

}
