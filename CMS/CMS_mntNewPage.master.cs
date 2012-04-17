using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CMS_CMS_mntNewPage : System.Web.UI.MasterPage
{
    public string RCAbout = "";
    public bool tamplates = false;
    public bool Rename = false;
    private int intSite;
    private int intCont;
    public string nameGene = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        intSite = Convert.ToInt32(HttpContext.Current.Session["siteId"].ToString());
        if (HttpContext.Current.Session["contId"] != null)
        {
            intCont = Convert.ToInt32(HttpContext.Current.Session["contId"].ToString());
        }
        else
        {
            intCont = 1;
        }


        System.Text.StringBuilder sbUser = new System.Text.StringBuilder();
        if (Convert.ToInt32(Session["userType"]) == 1)
        {
            sbUser.AppendLine("<div class=\"LoginName\">");
            sbUser.AppendLine("<a href=mnt_administrator.aspx style=\"border:0px\"><img src=\"imagesCss/user-icon.gif\" style=\"border:0px\" alt=\"user\" align=\"absmiddle\" /></a> Administrator ");
            sbUser.AppendLine("</div>");
        }
        else
        {
            sbUser.AppendLine("<div class=\"LoginName\">");
            sbUser.AppendLine("<img src=\"imagesCss/user-icon.gif\"  style=\"border:0\" alt=\"user\" align=\"absmiddle\" />&nbsp;" + Convert.ToString(Session["userfullname"]) + "");
            sbUser.AppendLine("</div>");
        }
        sbUser.AppendLine("<div class=\"LoginName\" id=\"cms_documentation\"><a class=\"linkHeader\" href=\"documents/ER-CMSUserManual.doc\" target=\"_blank\"><img align=\"absmiddle\" alt=\"user\" style=\"border: 0px none;\" src=\"imagesCss/Word.gif\"> CMS Documentation</a></div>");
        admin_name.InnerHtml = sbUser.ToString();
                    
  
        #region Aut ifs
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        if (Request["Generic"] != null)
        {
            Session["CurrentPage"] = "mnt_Generics.aspx?Generic=" + Convert.ToInt32(Request["Generic"]);

        }
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
            Session["CurrentPage"] = "mnt_Generics.aspx?Generic=" + Convert.ToInt32(Request["Generic"]);
            Response.Redirect("mnt_Generics.aspx?Generic=" + Convert.ToInt32(Request["Generic"]));
        }
        #endregion
       
        #region Ifs
        if (Convert.ToBoolean(Session["TemplateChose"]) == false)
        {
            Session["GeneType"] = 0;
            if ((Request["Generic"]) != null)
            {
                if (Convert.ToInt32(Request["Generic"]) == 2)
                {
                    Session["About"] = true;
                }
                else
                {
                    Session["About"] = false;
                }
            }

            if ((Request["Generic"]) != null)
            {
                Session["GeneType"] = Convert.ToInt32(Request["Generic"]);
            }

        }
        else {
            loadDDL();
        }

        if (Convert.ToString(Session["PageTitle"]) != "")
        {
            tamplates = true; 
            txt_NewPageTitle.Value = Convert.ToString(Session["PageTitle"]);
        }

        #endregion

        #region Content group
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        System.Text.StringBuilder sbcont = new System.Text.StringBuilder();
        //if (Session["contId"] != null)
        //{
        ContentNavigation content = new ContentNavigation();
        DataSet data = new DataSet();
        string[] Vector = new string[6 + 1];
        data = content.getAllContent(intSite);
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                string link = "<a class=\"SelectAudience2\"  href=" + Convert.ToString(Session["CurrentPage"]) + "&ContentGroupId=" + Convert.ToInt32(row["ContId"]) + ">" + Convert.ToString(row["ContTitle"]) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                Vector[Convert.ToInt32(row["ContOrdPos"])] = link;
            }

        }
        for (int i = 1; i < Vector.Length; i++)
        {
            sbcont.AppendLine(Convert.ToString(Vector[i]));
        }

        sbcont.AppendLine("<br/>");
        div_TopNavigation.InnerHtml = sbcont.ToString();
        //}
        #endregion

        #region FirstLoad
        if (((Request["Generic"]) != null) || (Convert.ToInt32(Session["GeneType"]) != 0) || (Convert.ToBoolean(Session["NewPageTemplate"]) == true))
        {
            
            bool fila = true;
            Generics gen = new Generics();
            //DataSet data = new DataSet();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (Session["GeneType"] != null)
            {
                if (Convert.ToInt32(Session["GeneType"]) == 1)
                {
                    RCAbout = "Resource Center";
                    data = gen.Get_All_GenericByType(intSite, intCont, Convert.ToInt32(Session["GeneType"]));
                }
                else
                {
                    if (Convert.ToInt32(Session["GeneType"]) == 2)
                    {
                        RCAbout = "About Us";
                        data = gen.Get_All_GenericByType(intSite, 0, Convert.ToInt32(Session["GeneType"]));
                    }
                }

                sb.AppendLine("<TABLE  border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td width=\"9\" height=\"10\">&nbsp;</td> ");
                sb.AppendLine("<td width=\"44\">&nbsp;</td>");
                sb.AppendLine("<td width=\"150\" class=\"class_LineaVTabla\">&nbsp;</td>");
                sb.AppendLine("<td width=\"80\" align=\"center\">&nbsp;</td> ");
                sb.AppendLine("<td width=\"100\" align=\"center\">&nbsp;</td>  ");
                sb.AppendLine("<td width=\"58\">&nbsp;</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td>&nbsp;</td> ");
                sb.AppendLine("<td>Id</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td> ");
                sb.AppendLine("<td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td> ");
                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("</tr>");
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if(fila == true)
                        {sb.AppendLine("<tr class=\"fila\"> ");}else{sb.AppendLine("<tr> ");}
                        sb.AppendLine("<td>&nbsp;</td>"); 
                        sb.AppendLine("<TD align=\"center\">");
                        sb.AppendLine("" + row["GeneId"].ToString() + "");
                        sb.AppendLine("</TD>");
                        sb.AppendLine("<TD align=\"left\" class=\"class_LineaVTabla\">"); 
                        sb.AppendLine("" + row["GeneTitle"].ToString() + "");
                        sb.AppendLine("</TD>");
                        sb.AppendLine("<td align=\"center\"><a class=\"enlace\" href= mnt_Generics.aspx?RenameId=" + row["GeneId"] + "&Type=" + Session["GeneType"] + "><img src=\"images/edit.jpg\" border=\"0\"> Rename</A></td>");
                        sb.AppendLine("<TD align=\"center\"><a href=" + row["tempmntpage"].ToString() + "?GenericId=" + row["GeneId"] + " class=\"enlace\"><img src=\"images/btn_Edit.png\" border=\"0\" /> Edit Content</a></TD>");
                        sb.AppendLine("<td align=\"center\"><a class=\"enlace\" href= mnt_Generics.aspx?DeleteId=" + row["GeneId"] + "&Type=" + Session["GeneType"] + "><img src=\"images/btn_delete.png\" border=\"0\" onclick=\"return confirm('Do you want to continue?  ');\"> Delete</A></td>");                        
                        sb.AppendLine(" <td align=\"left\">&nbsp;</td></tr>");
                        if (fila == true)
                        { fila = false; }
                        else { fila = true; }
                    }
                }
                if (Convert.ToBoolean(Session["About"]) == false)
                {
                    if (fila == true)
                    { sb.AppendLine("<tr class=\"fila\"> "); }
                    else { sb.AppendLine("<tr> "); }
                    sb.AppendLine("<td>&nbsp;</td>");
                    sb.AppendLine("<TD align=\"center\">");
                    sb.AppendLine("</TD>");
                    sb.AppendLine("<TD align=\"left\" class=\"class_LineaVTabla\">Free Tools</TD>");
                    sb.AppendLine("<td align=\"center\"></td>");
                    sb.AppendLine("<TD align=\"center\"><a href=\"mnt_FreeTools.aspx\" class=\"enlace\"><img src=\"images/btn_Edit.png\" border=\"0\" /> Edit Content</a></TD>");
                    sb.AppendLine("<td align=\"center\"></td>");
                    sb.AppendLine(" <td align=\"left\">&nbsp;</td></tr>");
                }
                sb.AppendLine("<tr><td>&nbsp;</td><td align=\"center\">&nbsp;</td>"); 
                sb.AppendLine("<td align=\"left\"class=\"class_LineaVTabla\">&nbsp;</td>");
                sb.AppendLine("<td align=\"center\">&nbsp;</td>"); 
                sb.AppendLine("<td align=\"center\">&nbsp;</td>");
                sb.AppendLine("<td align=\"left\">&nbsp;</td></tr>"); 
                                  
                sb.AppendLine("</TABLE>");
                div_Generic.InnerHtml = sb.ToString();
                btn_New.Visible = true;
            }
            else
            {
                Session["NewPageTemplate"] = true;
                div_templates.Visible = true;
            }
            if ((Request["GenericId"] != null) || (Request["ContentGroupId"] != null))
            {
                div_templates.Visible = false;
            }
            if (Convert.ToBoolean(Session["NewPageTemplate"]) == true)
            {
                div_templates.Visible = true;
            }
           
        }
        #endregion

        if (Request["Rename"] != null)
        {
            nameGene = Request["Rename"].ToString();
            Rename = true;
        }
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {
        div_templates.Visible = true;
        Session["NewPageTemplate"] = true;
        loadDDL();

    }



    protected void btn_genericA_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_NewPageTitle.Value != "")
        {
            Session["PageTitle"] = txt_NewPageTitle.Value;
            Session["TemplateChose"] = true;
            Session["NewPageTemplate"] = true;
            Session["GeneDefaId"] = SelectGeneric.SelectedValue;
            Response.Redirect("mnt_generic_a.aspx");
        }
        else
        {
            LbTitle.Text = "*Please type a title";
        }
       
    }
    protected void btn_genericB_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_NewPageTitle.Value != "")
        {
            Session["PageTitle"] = txt_NewPageTitle.Value;
            Session["TemplateChose"] = true;
            Session["NewPageTemplate"] = true;
            Session["GeneDefaId"] = SelectGeneric.SelectedValue;
            LbTitle.Text = "";
            Response.Redirect("mnt_generic_b.aspx");
        }
        else
        {
            LbTitle.Text = "*Please type a title";
        }

    }
    protected void btn_genericC_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_NewPageTitle.Value != "")
        {
            Session["PageTitle"] = txt_NewPageTitle.Value;
            Session["TemplateChose"] = true;
            Session["NewPageTemplate"] = true;
            Session["GeneDefaId"] = SelectGeneric.SelectedValue;
            LbTitle.Text = "";
            Response.Redirect("mnt_generic_c.aspx");
        }
        else
        {
            LbTitle.Text = "*Please type a title";
        }
    }
    protected void btn_genericD_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_NewPageTitle.Value != "")
        {
            Session["PageTitle"] = txt_NewPageTitle.Value;
            Session["TemplateChose"] = true;
            Session["NewPageTemplate"] = true;
            Session["GeneDefaId"] = SelectGeneric.SelectedValue;
            LbTitle.Text = "";
            Response.Redirect("mnt_generic_d.aspx?NewGend=" + true);
        }
        else
        {
            LbTitle.Text = "*Please Type A title";
        }
        
    }

    protected void btn_genericE_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_NewPageTitle.Value != "")
        {
            Session["PageTitle"] = txt_NewPageTitle.Value;
            Session["TemplateChose"] = true;
            Session["NewPageTemplate"] = true;
            Session["GeneDefaId"] = SelectGeneric.SelectedValue;
            LbTitle.Text = "";
            Response.Redirect("mnt_generic_e.aspx?NewGend=" + true);
        }
        else
        {
            LbTitle.Text = "*Please Type A title";
        }
        
    }
    protected void loadDDL()
    {
        DataSet datanew = new DataSet();
        Generics genew = new Generics();
        SelectGeneric.Items.Add(new ListItem("None", "0"));
        datanew = genew.Get_GeneDefault_By_Type(Convert.ToInt32(Session["GeneType"]));
        foreach (DataTable table in datanew.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                SelectGeneric.Items.Add(new ListItem(Convert.ToString(row["GeneDefaDescription"]), Convert.ToString(row["GeneDefaId"])));
            }
        } 
    }
    protected void btn_SaveLand_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Generics.aspx?GeneId=" + Convert.ToString(Request.QueryString["Id"]) + "&NewName="+Request["txtRename"].ToString()+"&Type=" + Session["GeneType"]);
    }
    protected void btn_CancelLand_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Generics.aspx?Generic=" + Request.QueryString["Generic"]);
    }
}

