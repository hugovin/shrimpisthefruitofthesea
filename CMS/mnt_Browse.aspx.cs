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

public partial class mnt_Browse : Browse
{
    #region properties
    private int contcat = 0;
    private int contcatDef = 0;
    private int contSearch =0;
    private int position = 1;
    private int contcatSubCat = 0;
    DataSet data = new DataSet();
    DataSet data2 = new DataSet();
    DataSet data3 = new DataSet();
    DataSet data5 = new DataSet();
    int[] vector = new int[50];
    #endregion
    public bool subItems = false;
    public bool BrowseDefaultItemList = false;
    public bool MoreDefault = false;
    public bool MoreDefaultItemsList = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_Browse.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        //----------------------------------------------------------------      
        #region Load category Items
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllBrowseCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]));
        bool fila = false;
        sb.AppendLine(" <table width=\"512\" id='canBeSorted' border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>");
        sb.AppendLine("<td width=\"4\" height=\"10\">&nbsp;</td>");
        sb.AppendLine("<td width=\"40\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"39\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"269\" class=\"class_LineaVTabla\">&nbsp;</td> ");
        sb.AppendLine("<td width=\"113\" class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td width=\"5\" align=\"center\">&nbsp;</td></tr>");
        sb.AppendLine("<tr> <td>&nbsp;</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Select</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Order</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td></tr>");
        #region for each
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                string act = "";
                string posi = "";
                if (Convert.ToInt32(row["active"]) == 1)
                {
                    act = "checked";
                } if (Convert.ToInt32(row["pos"]) == 0)
                {
                    posi = Convert.ToString(position);
                }
                else
                {
                    posi = Convert.ToString(row["pos"]);
                    vector[contcat] = Convert.ToInt32(posi);
                }
                if (fila == true)
                {
                    sb.AppendLine("<tr onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; ' >");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\" onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '> ");
                }

                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><input type=\"checkbox\" value=\"" + row["id"] + "\" name=\"chkCaIt" + contcat + "\"" + act + " /></td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><input type=\"text\" style=\"background-color:transparent; border:0px;width:15px\" value=\"" + position + "\"name=\"txtCaIt" + contcat + "\"  /></td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["cat"].ToString() + " &amp; Info </td>");
                if (Convert.ToInt32(row["BrowCatId"]) != 0)
                {
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href= mnt_Browse.aspx?CategoryId=" + row["id"] + "&BrowCateId=" + row["BrowCatId"] + ">Select SubItems</a></td>");
                }
                sb.AppendLine("<td align=\"left\">&nbsp;</td>");
                sb.AppendLine(" </tr>");
                if (fila == true)
                {
                    fila = false;
                }
                else
                {
                    fila = true;
                }
                contcat++;
                position++;

            }

        }
        #endregion
       
        
        //---------------------------------------------------------------
        #region Load Default Category Items
        data2 = getAllBrowseDefault();
        #region for each
        foreach (DataTable table2 in data2.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                string act = "";
                string posi = "";
                if (Convert.ToInt32(row2["BrowDefaState"]) == 1)
                {
                    act = "checked";
                } if (Convert.ToInt32(row2["BrowDefaOrdPos"]) == 0)
                {
                    posi = "";
                }
                else
                {
                    posi = Convert.ToString(row2["BrowDefaOrdPos"]);
                }
                if (fila == true)
                {
                    sb.AppendLine("<tr>");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\"> ");
                }

                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><input type=\"checkbox\" disabled value=\"" + row2["BrowDefaId"] + "\" name=\"chkDeCa" + row2["BrowDefaId"] + "\"" + act + " /></td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><input type=\"text\" style=\"background-color:transparent; border:0px;width:15px\" value=\"" + position + "\"name=\"txtDeCa" + contcatDef + "\"  /></td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row2["BrowDefaTitle"].ToString() + " &amp; Info </td>");

                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href= mnt_Browse.aspx?DefaultCat=" + row2["BrowDefaId"] + ">Select SubItems</a></td>");
                sb.AppendLine("<td align=\"left\">&nbsp;</td>");
                sb.AppendLine(" </tr>");
                if (fila == true)
                {
                    fila = false;
                }
                else
                {
                    fila = true;
                } 
                contcatDef++;
                position++;

            }

        }
        #endregion
        sb.AppendLine("</TABLE>");
        //sb.AppendLine("<a href= mnt_Browse.aspx?DefaultCat=" + Request["DefaultCat"] + "&AddMoreDefault=" + true + "> AddMore</a>");
        sb.AppendLine("<input name=\"CatIt\" type=\"hidden\" value=\"" + contcat + "\" />");
        sb.AppendLine("<input name=\"defCat\" type=\"hidden\" value=\"" + contcatDef + "\" />");
        div_CategoryItems.InnerHtml = sb.ToString();
        #endregion
        
#endregion
        //------------------------------------------------------------------------
        //----------------------------------------------------------------   
        #region Load SubCategory Items
        if(Request["CategoryId"]!= null)
        {
            subItems = true;
            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();
            data3 = getAllBrowseSubCategory(Convert.ToInt32(Request["BrowCateId"]));
            sb3.AppendLine("<TABLE>");
            sb3.AppendLine("<TR>");
            sb3.AppendLine("<TH>Select</TH>");
            sb3.AppendLine("<TH>ID</TH>");
            sb3.AppendLine("<TH>NAME</TH>");
            sb3.AppendLine("</TR>");
            sb3.AppendLine("<TR>");
            #region for each
            foreach (DataTable table3 in data3.Tables)
            {
                foreach (DataRow row3 in table3.Rows)
                {
                    string act = "";
                    string posi = "";
                    if (Convert.ToInt32(row3["active"]) == 1)
                    {
                        act = "checked";
                    } if (Convert.ToInt32(row3["pos"]) == 0)
                    {
                        posi = "";
                    }
                    else
                    {
                        posi = Convert.ToString(row3["pos"]);
                    }
                    sb3.AppendLine("<TR><TD>");
                    sb3.AppendLine("<input type=\"checkbox\" value=\"" + row3["subid"] + "\" name=\"chkSubCaIt" + contcatSubCat + "\" " + act +" /> ");
                    sb3.AppendLine("</TD>");
                    sb3.AppendLine("<TD>");
                    sb3.AppendLine("" + row3["subid"].ToString() + "");
                    sb3.AppendLine("</TD>");
                    sb3.AppendLine("<TD>");
                    sb3.AppendLine(""+ row3["subcat"].ToString() +"");
                    sb3.AppendLine("</TD></TR>");
                    contcatSubCat++;
                }
            }
            #endregion
            sb3.AppendLine("</TR>");
            sb3.AppendLine("</TABLE>");
            sb3.AppendLine("<input name=\"SubCatIt\" type=\"hidden\" value=\"" + contcatSubCat++ + "\" />");
          //  btn_SaveSubCategory.Visible = true;
            btn_CancelSubCategory.Visible = true;
            btn_SaveSubCategory.Visible = true;
            div_BrowseSubCategory.InnerHtml = sb3.ToString();
        }
        #endregion
        //------------------------------------------------------------------------
        #region Load SubDefaultCategory Items
        
        if (Request["DefaultCat"] != null)
        {
            BrowseDefaultItemList = true;
            System.Text.StringBuilder sb4 = new System.Text.StringBuilder();
            data3 = getAllBrowseSubDefault(Convert.ToInt32(Request["DefaultCat"]));
            sb4.AppendLine("<TABLE>");
            sb4.AppendLine("<TR>");
            sb4.AppendLine("<TH>Select</TH>");
            sb4.AppendLine("<TH>ID</TH>");
            sb4.AppendLine("<TH>NAME</TH>");
            sb4.AppendLine("</TR>");
            sb4.AppendLine("<TR>");
            #region for each
            foreach (DataTable table3 in data3.Tables)
            {
                foreach (DataRow row3 in table3.Rows)
                {
                    string act = "";
                    string posi = "";
                    if (Convert.ToInt32(row3["active"]) == 1)
                    {
                        act = "checked";
                    } 
                    sb4.AppendLine("<TR><TD>");
                    sb4.AppendLine("<input type=\"checkbox\" value=\"" + row3["Id"] + "\" name=\"chkSubDefaCaIt" + contcatSubCat + "\" " + act +" /> ");
                    sb4.AppendLine("</TD>");
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("" + row3["Id"].ToString() + "");
                    sb4.AppendLine("</TD>");
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("" + row3["Title"].ToString() + "");
                    sb4.AppendLine("</TD>");
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("<a href= mnt_Browse.aspx?DeleteSubDefCat=" + row3["Id"] +"&DefaultCat=" + Request["DefaultCat"]+ "&acc=delete&press=send'  onclick=\"return confirm('  Are you sure you want to delete Feature Product?');\"> Remove</a>");
                    sb4.AppendLine("</TD></TR>");
                    contcatSubCat++;
                }
            }
            #endregion
            sb4.AppendLine("</TR>");
            sb4.AppendLine("</TABLE>");
            sb4.AppendLine("<a href= mnt_Browse.aspx?DefaultCat=" + Request["DefaultCat"] + "&AddMore="+true+"> AddMore</a>");
            sb4.AppendLine("<input name=\"SubDefaulCat\" type=\"hidden\" value=\"" + contcatSubCat++ + "\" />");
            div_saveSubDefault.Visible = true;
            div_BrowseDefaultItemList.InnerHtml = sb4.ToString();
        }
 #endregion
        //------------------------------------------------------------------------
        if (Request["AddMore"] != null)
        {
            MoreDefault = true;
            div_MoreDefaultItems.Visible = true;
            div_saveSubDefault.Visible = false;
        }
        //-----------------------------------------------------------------------
        if (Request["DeleteSubDefCat"]!=null)
        {
            deleteSubDefault(Convert.ToInt32(Request["DeleteSubDefCat"]));
            Response.Redirect("mnt_Browse.aspx?DefaultCat="+ Request["DefaultCat"]);
        }
        //------------------------------------------------------------------------
        #region Search Results
        if (Request["type"] != null)
        {
            MoreDefaultItemsList = true;
            MoreDefault = true;
            btn_CancelSearch.Visible = true;
            btn_SaveSearch.Visible = true;
            System.Text.StringBuilder sb5 = new System.Text.StringBuilder();
            data5 = getSubDefaultByTitle(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["DefaultCat"]), Convert.ToString(Request["SearchId"]));
            sb5.AppendLine("<TABLE>");
            sb5.AppendLine("<TR>");
            sb5.AppendLine("<TH>Select</TH>");
            sb5.AppendLine("<TH>ID</TH>");
            sb5.AppendLine("<TH>NAME</TH>");
            sb5.AppendLine("</TR>");
            sb5.AppendLine("<TR>");
            #region for each
            foreach (DataTable table5 in data5.Tables)
            {
                foreach (DataRow row5 in table5.Rows)
                {

                    sb5.AppendLine("<TR><TD>");
                    sb5.AppendLine("<input type=\"checkbox\" value=\"" + row5["Id"] + "\" name=\"chkSubCaItSearch" + contSearch + "\" /> ");
                    sb5.AppendLine("</TD>");
                    sb5.AppendLine("<TD>");
                    sb5.AppendLine("" + row5["Id"].ToString() + "");
                    sb5.AppendLine("</TD>");
                    sb5.AppendLine("<TD>");
                    sb5.AppendLine("" + row5["Title"].ToString() + "");
                    sb5.AppendLine("</TD></TR>");
                    contSearch++;
                }
            }
            #endregion
            sb5.AppendLine("</TR>");
            sb5.AppendLine("</TABLE>");
            sb5.AppendLine("<input name=\"SearchCont\" type=\"hidden\" value=\"" + contSearch + "\" />");
           // btn_SubSubDefault.Visible = true;
            btn_CancelSubDefault.Visible = true;
            div_MoreDefaultItems.Visible = true;
            div_MoreDefaultItemsList.InnerHtml = sb5.ToString();
        }
        #endregion
    }
    //-------------------------------------------------------------------------
    protected void SaveSubCategory(int count)
    {
        string vsubsubj;
        int isubsub = 0;
        Delete_BrowseSubCategory();
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["SubCatIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {

                Add_BrowseSubCategory(Convert.ToInt32(Request["CategoryId"]), isubsub, 5);
            }
        }

        Response.Redirect("mnt_Browse.aspx");
    }
    //-------------------------------------------------------------------------    
    protected void btn_CancelSubCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Browse.aspx");
    }
    //-------------------------------------------------------------------------
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CMS_MainSite.aspx");
    }
    //-------------------------------------------------------------------------
    protected void btn_SearchDefaultItems_Click(object sender, EventArgs e)
    {
        string search = txCriteria.Value;
        Response.Redirect("mnt_Browse.aspx?DefaultCat=" + Request["DefaultCat"] + "&SearchId=" + search + "&type=text");

    }
    //---------------------------------------------------------------------------------------
    protected void btn_CancelSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Browse.aspx?DefaultCat=" + Request["DefaultCat"]);
    }
    //---------------------------------------------------------------------------------------
    #region btn_SaveSearch
    protected void btn_SaveSearch_Click(object sender, EventArgs e)
    {
        string csub = Request["SearchCont"];
        if (csub != null)
        {
            if (Convert.ToInt32(csub) > 0)
            {
                saveDefaultSubCategoryItems(Convert.ToInt32(csub));
            }
        }
    }
    //---------------------------------------------------------------------------------------
    protected void saveDefaultSubCategoryItems(int count)
    {
        string vsubsubj;
        int isubsub = 0;
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["chkSubCaItSearch" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {

                Add_BrowseSubDefaultCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["DefaultCat"]), isubsub, 5);
            }
        }

        Response.Redirect("mnt_Browse.aspx?DefaultCat=" + Request["DefaultCat"]);
    }
    #endregion
    //---------------------------------------------------------------------------------------
    #region btn_SaveSubCategory
    protected void btn_SaveSubCategory_Click(object sender, EventArgs e)
    {
        string csub = Request["SubCatIt"];
        if (csub != null)
        {
            if (Convert.ToInt32(csub) > 0)
            {
                saveSubCategoryItems(Convert.ToInt32(csub));
            }
        }
    }
    //---------------------------------------------------------------------------------------
    protected void saveSubCategoryItems(int count)
    {
        Del_All_BrowseSubCategory(Convert.ToInt32(Request["BrowCateId"]));
        string vsubsubj;
        int isubsub = 0;
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["chkSubCaIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {
                Add_BrowseSubCategory(Convert.ToInt32(Request["BrowCateId"]), isubsub, 5);
            }
        }
        Response.Redirect("mnt_Browse.aspx");
    }
    #endregion
    //---------------------------------------------------------------------------------------
    #region btn_SaveBrowse
    protected void btn_SaveBrowse_Click(object sender, EventArgs e)
    {
        string category = Request["CatIt"];
        string defaultcat = Request["defCat"];
        if ((category != null) && (defaultcat != null))
        {
            if (Convert.ToInt32(category) > 0)
            {
                SaveCategory(Convert.ToInt32(category), Convert.ToInt32(defaultcat));
            }
        }
    }
    //-------------------------------------------------------------------------
    protected void SaveCategory(int countCategory, int countDefaultCat)
    {
        Del_AllBrowseCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]));
        string vsubsubj;
        int pos = 0;
        int isubsub = 0;
        string position;
        for (int i = 0; i <= countCategory-1; i++)
        {
            vsubsubj = Request["chkCaIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            position = Request["txtCaIt" + i];
            if ((Convert.ToString(Request["txtCaIt" + i]) != "")||(Convert.ToBoolean(Request["txtCaIt" + i] != null)))
            {
                if ((validationpos(Convert.ToString(Request["txtCaIt" + i])) != false))
                {
                    pos = Convert.ToInt32(position);
                }
                if (vsubsubj != null)
                {
                    Add_BrowseCategory(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), isubsub, "", pos, 1);
                }
            }
        }

        vsubsubj = "";
        isubsub = 0;
        for (int i = 0; i <= countDefaultCat; i++)
        {
            vsubsubj = Request["chkDeCa" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {

                Upd_BrowseDefault(isubsub, true, true);
            }
            else
            {
                Upd_BrowseDefault(i, false, true);
            }
        }
        Response.Redirect("mnt_Browse.aspx");

    }
    #endregion
    //---------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------
    protected void btn_SubSubDefault_Click(object sender, EventArgs e)
    {
    }
    //-------------------------------------------------------------------------------
    protected void btn_CancelSubDefault_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Browse.aspx");
    }
    //-----------------------------------------------------------------------------
    protected bool validationpos(string search)
    {
        bool flag = false;
        int cont = 0;
        int position = 0;
        int leng = search.Length;
        for (int i = 0; i < search.Length; i++)
        {
            if (((search[i] == '0') || (search[i] == '1') || (search[i] == '2') || (search[i] == '3') || (search[i] == '4')
                || (search[i] == '5') || (search[i] == '6') || (search[i] == '7') || (search[i] == '8') || (search[i] == '9')) && (i > 0))
            {
                cont++;
            }
        }
        if (cont == (search.Length - 1))
        {
            flag = true;
        }
        return flag;
    }
        //--------------------------------------------------------------------------------------------------------------

}
