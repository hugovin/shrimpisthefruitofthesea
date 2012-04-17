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

public partial class mnt_Finder : Finder
{
    #region properties
    private int contcat = 0;
    private int contcatDef = 0;
    private int contSub = 0;
    private int contSearch = 0;
    private int catID = 0;
    private int contcatSubCat = 0;
    DataSet data = new DataSet();
    DataSet data2 = new DataSet();
    DataSet data3 = new DataSet();
    DataSet data5 = new DataSet();
    int[] vector = new int[50];
    public bool FinderDefaultItemList = false;
    public bool FinderSubCategory = false;
    public bool MoreDefaultItems = false;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_Finder.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        //----------------------------------------------------------------      
        #region Load category Items
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllFinderCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]));
        bool fila = false;
        sb.AppendLine(" <table  border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>");
        sb.AppendLine("<td height=\"10\">&nbsp;</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td  class=\"class_LineaVTabla\">&nbsp;</td></tr>");
        sb.AppendLine("<tr> <td>&nbsp;</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\"></td></tr>");
        #region for each
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (fila == true)
                {
                    sb.AppendLine("<tr>");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\"> ");
                }

                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\">"+row["FindcateTitle"].ToString()+"</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href= mnt_Finder.aspx?CategoryId=" + row["FindCateId"] + ">Select SubItems</a></td>"); 

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

            }

        }
        #endregion

        #region Load Default Category Items
        data2 = getAllFinderDefault(Convert.ToString(Session["siteId"]), Convert.ToString(Session["contId"]));
        #region for each
        foreach (DataTable table2 in data2.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                if (fila == true)
                {
                    sb.AppendLine("<tr>");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\"> ");
                }

                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\">" + row2["FindTitle"].ToString() + "</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href= mnt_Finder.aspx?DefaultCat=" + row2["FindDefaId"] + ">Select SubItems</a></td>");

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
            }
        }
        #endregion
        sb.AppendLine("</TR>");
        sb.AppendLine("</TABLE>");
        sb.AppendLine("<input name=\"defCat\" type=\"hidden\" value=\"" + contcatDef + "\" />");
        sb.AppendLine("<input name=\"CatIt\" type=\"hidden\" value=\"" + contcat + "\" />");
        div_CategoryItems.InnerHtml = sb.ToString();
        
        #endregion
        
        #endregion
        //------------------------------------------------------------------------
        
        //----------------------------------------------------------------   
        #region Load SubCategory Items
        if (Request["CategoryId"] != null)
        {
            FinderSubCategory = true;
            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();
            data3 = getAllFinderSubCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["CategoryId"]));
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
                    sb3.AppendLine("<input type=\"checkbox\" value=\"" + row3["subid"] + "\" name=\"chkSubCaIt" + contcatSubCat + "\" " + act + " /> ");
                    sb3.AppendLine("</TD>");
                    sb3.AppendLine("<TD>");
                    sb3.AppendLine("" + row3["subid"].ToString() + "");
                    sb3.AppendLine("</TD>");
                    sb3.AppendLine("<TD>");
                    sb3.AppendLine("" + row3["subcat"].ToString() + "");
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
            div_FinderSubCategory.InnerHtml = sb3.ToString();
        }
        #endregion
        //------------------------------------------------------------------------
        #region Load SubDefaultCategory Items
        if (Request["DefaultCat"] != null)
        {
            FinderDefaultItemList = true;
            System.Text.StringBuilder sb4 = new System.Text.StringBuilder();
            data3 = getAllFinderSubDefault(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["DefaultCat"]));
            sb4.AppendLine("<TABLE>");
            sb4.AppendLine("<TR>");
            sb4.AppendLine("<TH>ID</TH>");
            sb4.AppendLine("<TH>NAME</TH>");
            sb4.AppendLine("</TR>");
            sb4.AppendLine("<TR>");
            #region for each
            foreach (DataTable table3 in data3.Tables)
            {
                foreach (DataRow row3 in table3.Rows)
                {                 
                    
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("" + row3["Id"].ToString() + "");
                    sb4.AppendLine("</TD>");
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("" + row3["Title"].ToString() + "");
                    sb4.AppendLine("</TD>");
                    sb4.AppendLine("<TD>");
                    sb4.AppendLine("<a href= mnt_Finder.aspx?DeleteSubDefCat=" + row3["Id"] + "&DefaultCat=" + Request["DefaultCat"] + "&acc=delete&press=send'  onclick=\"return confirm('  Are you sure you want to delete Feature Product?');\"> Remove</a>");
                    sb4.AppendLine("</TD></TR>");
                    contcatSubCat++;
                }
            }
            #endregion
            sb4.AppendLine("</TR>");
            sb4.AppendLine("</TABLE>");
            sb4.AppendLine("<a href= mnt_Finder.aspx?DefaultCat=" + Request["DefaultCat"] + "&AddMore=" + true + "> AddMore</a>");
            sb4.AppendLine("<input name=\"SubDefaulCat\" type=\"hidden\" value=\"" + contcatSubCat + "\" />");
            div_saveSubDefault.Visible = true;
            div_FinderDefaultItemList.InnerHtml = sb4.ToString();
        }
        #endregion
        //------------------------------------------------------------------------
        if (Request["AddMore"] != null)
        {
            MoreDefaultItems = true;
            div_MoreDefaultItems.Visible = true;
            div_saveSubDefault.Visible = false;
        }
        //-----------------------------------------------------------------------
        if (Request["DeleteSubDefCat"] != null)
        {
            deleteSubDefault(Convert.ToInt32(Request["DeleteSubDefCat"]));
            Response.Redirect("mnt_Finder.aspx?DefaultCat=" + Request["DefaultCat"]);
        }
        //------------------------------------------------------------------------
        #region Search Results
        if (Request["type"] != null)
        {
            MoreDefaultItems = true;
            btn_CancelSearch.Visible = true;
            btn_SaveSearch.Visible = true;
            System.Text.StringBuilder sb5 = new System.Text.StringBuilder();
            data5 = getSubDefaultByTitle(Convert.ToInt32(Request["DefaultCat"]), Convert.ToString(Request["SearchId"]));
            sb5.AppendLine("<TABLE>");
            sb5.AppendLine("<TR>");
            sb5.AppendLine("<TH>Select</TH>");
            sb5.AppendLine("<TH>ID</TH>");
            sb5.AppendLine("<TH>NAME</TH>");
            sb5.AppendLine("</TR>");
            sb5.AppendLine("<TR>");
            if (data5.Tables["table"].Rows.Count != 0)
            {
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
            else {
                div_MoreDefaultItems.Visible = true;
                btn_CancelSearch.Visible = false;
                btn_SaveSearch.Visible = false;
            }

        }
        #endregion
    }
    //-------------------------------------------------------------------------
    protected void SaveSubCategory(int count)
    {
        string vsubsubj;
        int isubsub = 0;
        Delete_FinderSubCategory();
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["SubCatIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {

                Add_FinderSubCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["CategoryId"]), isubsub, 5);
            }
        }

        Response.Redirect("mnt_Finder.aspx");
    }
    //-------------------------------------------------------------------------    
    protected void btn_CancelSubCategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Finder.aspx");
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

        Response.Redirect("mnt_Finder.aspx?DefaultCat=" + Request["DefaultCat"] + "&SearchId=" + search + "&type=text");

    }
    //---------------------------------------------------------------------------------------
    protected void btn_CancelSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Finder.aspx?DefaultCat=" + Request["DefaultCat"]);
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

                Add_FinderSubDefaultCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["DefaultCat"]), isubsub, 5);
            }
        }

        Response.Redirect("mnt_Finder.aspx?DefaultCat=" + Request["DefaultCat"]);
    }
    #endregion
    //---------------------------------------------------------------------------------------
    #region btn_SaveSubCategory
    protected void btn_SaveSubCategory_Click(object sender, EventArgs e)
    {
        DeleteALLSubcategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["CategoryId"]));
        string csub = Request["SubCatIt"];
        if (csub != null)
        {
            if (Convert.ToInt32(csub) > 0)
            {
                saveSubCategoryItems(Convert.ToInt32(csub));
            }
        }
    }
    protected void saveSubCategoryItems(int count)
    {
        string vsubsubj;
        int isubsub = 0;
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["chkSubCaIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {
                Add_FinderSubCategory(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["CategoryId"]), isubsub, 5);
            }
        }
        Response.Redirect("mnt_Finder.aspx");
    }
    #endregion
    //---------------------------------------------------------------------------------------
    #region btn_SaveFinder
    protected void btn_SaveFinder_Click(object sender, EventArgs e)
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
        Del_AllFinderCategory();
        string vsubsubj;
        int pos = 0;
        int isubsub = 0;
        string position;
        for (int i = 0; i <= countCategory - 1; i++)
        {
            vsubsubj = Request["chkCaIt" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            position = Request["txtCaIt" + i];
            if ((Convert.ToString(Request["txtCaIt" + i]) != "") || (Convert.ToBoolean(Request["txtCaIt" + i] != null)))
            {
                if ((validationpos(Convert.ToString(Request["txtCaIt" + i])) != false))
                {
                    pos = Convert.ToInt32(position);
                }
                if (vsubsubj != null)
                {
                    Add_FinderCategory(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), isubsub, "", pos, 1);
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

                Upd_FinderDefault(isubsub, true, true);
            }
            else
            {
                Upd_FinderDefault(i, false, true);
            }
        }
        Response.Redirect("mnt_Finder.aspx");

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
        Response.Redirect("mnt_Finder.aspx");
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