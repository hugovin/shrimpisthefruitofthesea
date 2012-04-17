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
using System.Text;

public partial class Purchasing_Information_Win : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = "images";
    private string GeneId = "17";// ID Generic correspondiente al Purchasing Information.
    private int GeneIdPurchasing = 0;// ID Generic correspondiente al Purchasing Information.
    private int GeneDefaIdPurchasing = 17;// ID Generic Default correspondiente al Purchasing Information.


    private void GetVars()
    {
        //---
        if (Session["SiteId"] != null)
            SiteId = Session["SiteId"].ToString();

        ContentId = Request["ci"];
        if (ContentId != null)
            Session["ContentId"] = ContentId;
        else
            ContentId = Session["ContentId"].ToString();
        //--
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "purchasing_information_win.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        //Session["CurrentChilPage"] = "purchasing_information_win.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        GeneIdPurchasing = Get_Id_Generic(Convert.ToInt32(GeneDefaIdPurchasing));

        LoadResoucesInfoPurchasing();
    }

    protected void LoadResoucesInfoPurchasing()
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();

        //---           
        dsresourcesinfo = resourcesinfo.getAllGenericDByTypeId(Convert.ToInt32(GeneIdPurchasing));
         
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {                
                sb.AppendLine("<div class=\"contChangePass\">");
                    sb.AppendLine("<div class=\"contPurInfo\">");
                        sb.AppendLine("<h1>" + row["GeneDTitle"].ToString() + "</h1>");
                        sb.AppendLine(row["GeneDContent"].ToString());

                        if (row["GeneDFile"].ToString() != "")
                        {
                            sb.AppendLine("<div id=\"boxContImage\">");
                            sb.AppendLine("<div class=\"imgPurInfo\" style=\"height:144px; text-align:center\"><img id=\"images\" style=\"height:100%;\" src=\""+ strFolder + "/" + row["GeneDFile"].ToString() + "\"/></div>"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                            sb.AppendLine("</div>");
                        }
                        if (row["GeneDLinkTitle"].ToString() != "") sb.AppendLine("<div class=\"mailCustomer\"><a href=\"" + row["GeneDLink"].ToString() + "\" target=\"_parent\">" + row["GeneDLinkTitle"].ToString() + "</a></div>");
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");                
            }
        }

        //sb.AppendLine("<div>*</div>");
        PlaceHolder_Resources_Purchasing.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
        resourcesinfo = null;
        dsresourcesinfo = null;
    }
    protected int Get_Id_Generic(int genDefaId)
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        int GeneIdG = 0;

        dsresourcesinfo = resourcesinfo.Get_Id_Generic(Convert.ToInt32(genDefaId));
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                GeneIdG = Convert.ToInt32(row["GeneId"].ToString());
            }
        }
        if (GeneIdG.ToString() == "") return 0;
        else return GeneIdG;
    }


}
