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

public partial class mnt_generic_c : Gen_C
{
    public string str_TinyMCE = "";
    public string Answer = "";
    public string Question = "";
    private int cont = 0;
    public int position = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_generic_c.aspx";
        if (Request["GenericId"] != null)
        {
            Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
            Session["NewPageTemplate"] = false;
        }

        #region FirstLoad
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        bool fila = true;
        int positionsta = 1;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
        data = Get_Generic_C_By_Type(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["GenericId"]));
        sb.AppendLine("<TABLE width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        sb.AppendLine("<tr><td width=\"47\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"330\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"47\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td width=\"56\" align=\"center\">&nbsp;</td></tr>");
        sb.AppendLine("<tr class=\"class_LineaHTabla\">");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Position</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td></tr>");
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (fila == true)
                { sb.AppendLine("<tr class=\"fila\"> "); }
                else { sb.AppendLine("<tr> "); }
                sb.AppendLine("<TD align=\"center\" class=\"class_LineaVTabla\">");
                sb.AppendLine("" + positionsta + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD class=\"class_LineaVTabla\">");
                sb.AppendLine("" + row["GeneCTitle"].ToString() + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD align=\"center\">");
                sb.AppendLine("<a class=\"enlace\" href=mnt_generic_c.aspx?GeneCId=" + row["GeneCId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                sb.AppendLine("<TD>");
                sb.AppendLine("<a onclick=\"return confirm('Do you want to continue?  ');\" class=\"enlace\" href=mnt_generic_c.aspx?DeleteId=" + row["GeneCId"] + " ><img src=\"images/btn_delete.png\" border=\"0\">Delete</a></td>");
                sb.AppendLine("</TR>");
                cont++;
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
                positionsta++;
            }

        }
        sb.AppendLine("</TABLE>");
        if ( (cont != 0)&& (Convert.ToBoolean(Session["NewPageTemplate"])==false))
        {
            position = cont++; 
            div_FaqsList.InnerHtml = sb.ToString();
            if (Convert.ToInt32(Request["GenericId"]) == 0)
            {
                Session["newTemplate"] = true;
            }
        }
        else
        {
            div_FAQS.Visible = false;
            div_newEdit.Visible = true;
        }
        #endregion

        #region New GeneC
        if (Request["NewGeneC"] != null)
        {
            div_newEdit.Visible = true;
        }
        #endregion

        #region Edit
        if (Request["GeneCId"] != null)
        {
            DataSet data2 = new DataSet();
            data2 = Get_GenericC_By_ID(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["GeneCId"]));
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    Question = row2["GeneCTitle"].ToString();
                    Answer = row2["GeneCContent"].ToString();
                    lbDate.Text = "Creation Date: " + row2["GeneCDate"].ToString();
                }

            }
            div_newEdit.Visible = true;
        }
        #endregion

        if (Request["DeleteId"] != null)
        {
            Delete_Generic_C(Convert.ToInt32(Request["Deleteid"]));
            Response.Redirect("mnt_generic_c.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
        }
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {
        Session["intGenericId"] = Convert.ToInt32(Request["GenericId"]);
        Response.Redirect("mnt_generic_c.aspx?NewGeneC=" + true);
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_c.aspx");
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        int genedefaid = 0;

        if (Convert.ToString(Session["GeneDefaId"]) != "")
        {
            genedefaid = Convert.ToInt32(Session["GeneDefaId"]);
        }

        if (Convert.ToInt32(Session["GeneType"]) == 0)
        {
            if (Convert.ToBoolean(Session["About"]) == false)
            {
                Session["GeneType"] = 1;
            }
            else
            {
                Session["GeneType"] = 2;
            }

        }

        if (Convert.ToString(Request["txt_Title"]) != "")
        {
            if (Request["GeneCId"] == null)
            {
                if (Convert.ToBoolean(Session["NewPageTemplate"]) != false)
                {
                    if (Convert.ToInt32(Session["GeneType"]) != 0)
                    {
                        if (Convert.ToBoolean(Session["About"]) == false)
                        {
                            Add_generic_C(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), 0, Convert.ToInt32(Session["GeneType"]), genedefaid, Convert.ToString(Session["PageTitle"]),
                                Convert.ToString(Request["txt_TitleC"]), Convert.ToString(Request["elm1"]), 1);
                        }
                        else
                        {
                            Add_generic_C(Convert.ToInt32(Session["siteId"]), 0, 0, Convert.ToInt32(Session["GeneType"]), genedefaid, Convert.ToString(Session["PageTitle"]),
                                Convert.ToString(Request["txt_TitleC"]), Convert.ToString(Request["elm1"]), 1);                           
                        }

                    }
                    Session["PageTitle"] = "";
                    Session["TemplateChose"] = false;
                    Session["NewPageTemplate"] = false;
                    Response.Redirect("mnt_Generics.aspx?Generic=" + Convert.ToInt32(Session["GeneType"]));
                }
                else
                {
                    if (Convert.ToBoolean(Session["About"]) == false)
                    {
                        Add_generic_C(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Session["intGenericId"]), Convert.ToInt32(Session["GeneType"]), genedefaid, Convert.ToString(Session["PageTitle"]),
                               Convert.ToString(Request["txt_TitleC"]), Convert.ToString(Request["elm1"]), Convert.ToInt32(Request["hidepos"]));
                    
                    }
                    else
                    {
                        Add_generic_C(Convert.ToInt32(Session["siteId"]), 0, Convert.ToInt32(Session["intGenericId"]), Convert.ToInt32(Session["GeneType"]), genedefaid, Convert.ToString(Session["PageTitle"]),
                            Convert.ToString(Request["txt_TitleC"]), Convert.ToString(Request["elm1"]), Convert.ToInt32(Request["hidepos"]));
                        Response.Redirect("mnt_generic_c.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
                    }
                    Session["PageTitle"] = "";
                    Session["TemplateChose"] = false;
                    Session["NewPageTemplate"] = false;
                    Response.Redirect("mnt_generic_c.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
                }


            }
            else
            {
                Upd_GenericC(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["GeneCId"]), Convert.ToString(Request["txt_TitleC"]), Convert.ToString(Request["elm1"]), 1);
                Response.Redirect("mnt_generic_c.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
            }
        }
    }
    protected void btn_GoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Generics.aspx?Generic=1");

    }
}
