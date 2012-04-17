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

public partial class mnt_FeatureProducts : FeaturedProducts
{
    int contmain = 0;    
    int firstSubject = 0;
    private int contTitle = 0;
    DataSet data2 = new DataSet();
    int[] subsIds = new int[500];
    int[] vector = new int[500];
    int[] ids = new int[500];
    private int contpos = 1;
    public int contInde = 0;
    private string IndexPos = "";
    private string strFolder = SiteConstants.imagesPath;
    Addins addin = new Addins();

    protected void Page_Load(object sender, EventArgs e)
    {
        string posi = "";
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_FeatureProducts.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();

        #region First Load
        bool fila = false;
        if (Request["Home"] != null)
        {
            
            data = Get_FeatureProduct_Main(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
            WrapperProduct.Style.Value = "margin-left:80px; float:left";
            Selection_Area.Visible = true;
        }
        else {
            IndexPos = Request["SubjID"];
            ddlSubject_Load();
            data = getAllFeaturedProductsBySubjectID(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["SubjID"]));
            div_SubjectSelection.Visible = true;
        }

        sb.AppendLine("<table width=\"943\">");
        sb.AppendLine("<tr class=\"topTable\">");
        sb.AppendLine("<td width=\"50\" height=\"30\" class=\"tableSelect\">Select</td>");
        sb.AppendLine("<td width=\"95\"></td>");
        sb.AppendLine("<td width=\"230\" class=\"productName\">Product Name</td>");
        sb.AppendLine("<td width=\"78\" class=\"item\">Item #</td>");
        sb.AppendLine("<td width=\"387\" class=\"descriptionTable\">Description</td>");
        sb.AppendLine("<td width=\"87\" class=\"priceTable\">Price</td></tr>");

        int concat = 0;
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["FeatProOrdPos"]) == 0)
                {
                    posi = "";
                }
                else
                {
                    posi = Convert.ToString(row["FeatProOrdPos"]);
                    vector[concat] = Convert.ToInt32(posi);
                }
                if (fila == true)
                { sb.AppendLine("<tr>"); }
                else { sb.AppendLine("<tr class=\"whiteTable\">"); }
                if (Request["Home"] != null)
                {
                    sb.AppendLine("<td class=\"tableSelect\"><a href='mnt_FeatureProducts.aspx?Home=True&Delete=" + row["FeatPro"] + "&acc=delete&press=send' onclick=\"return confirm('Do you want to delete this Feature Product?');\"><img src=\"imagesCss/bulletTable.jpg\" /></a></td>");

                }
                else
                {
                    sb.AppendLine("<td class=\"tableSelect\"><a href='mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Delete=" + row["FeatPro"] + " &acc=delete&press=send'  onclick=\"return confirm('Do you want to delete this Feature Product?');\"><img src=\"imagesCss/bulletTable.jpg\"/></a></td>");
                }
                sb.AppendLine("<td><img src=\""+strFolder+row["ImageTN"].ToString() +"\" /></td>");
                sb.AppendLine("<td class=\"productName\"><p>" + searchTitle(Convert.ToInt32(row["TitleID"])) + "</p></td>");
                sb.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                sb.AppendLine("<td class=\"descriptionTable\"><p>" + addin.cutDescription(row["Titletext"].ToString(), 241) + "</p></td>");
                sb.AppendLine("<td class=\"priceTable\"><p>"+row["ER_Price"].ToString()+"</p></td></tr>");
                concat++;
                contmain++;
                contpos++;
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
            }
        }
        sb.AppendLine("</TABLE>");
        btnAddFP.Visible = true;
        div_FeatureProducts.InnerHtml = sb.ToString();
        
        #endregion
        //--------------------------------------------------------------------------------------------------------------
        #region New
        if (Request["AddNew"] != null)
        {
            if ((Request["Mainsite"] == "0") && contmain == 3)
            {
                lbErroMax.Text = "Only 3 elements can be added as Feature products on the main site";
            }
            else
            {
                System.Text.StringBuilder sbSearch = new System.Text.StringBuilder();
                sbSearch.AppendLine("Search <br>");
                if (Request["SearchId"] != null)
                {
                    sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" runat=\"server\"  value=\"" + Convert.ToString(Request["SearchID"]) + "\" />");
                }
                else
                {
                    sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" runat=\"server\" />");
                }
                btnSearch.Visible = true;
                div_AddFP.InnerHtml = sbSearch.ToString();
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------------------
        #region Search by criteria
        if (Request["SearchID"] != null)
        {
            System.Text.StringBuilder sbNew = new System.Text.StringBuilder();
            string a = Convert.ToString(Request["type"]);
            if ((Convert.ToString(Request["type"])) == "text")
            {
                data2 = getFeatureProductByTitle(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToString(Request["SearchID"]));
            }
            else
            {
                data2 = getFeatureProductById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["SearchID"]));
            }
            SiteConstants constants = new SiteConstants();
            
            sbNew.AppendLine("<input id=\"txtPosition\" readonly value=\"" + contpos + "\" type=\"hidden\" name=\"txtPosition\"/>");
            sbNew.AppendLine("<table width=\"943\">");
            sbNew.AppendLine("<tr class=\"topTable\">");
            sbNew.AppendLine("<td width=\"50\" height=\"30\" class=\"tableSelect\">Select</td>");
            sbNew.AppendLine("<td width=\"95\"></td>");
            sbNew.AppendLine("<td width=\"230\" class=\"productName\">Product Name</td>");
            sbNew.AppendLine("<td width=\"78\" class=\"item\">Item #</td>");
            sbNew.AppendLine("<td width=\"387\" class=\"descriptionTable\">Description</td>");
            sbNew.AppendLine("<td width=\"87\" class=\"priceTable\">Price</td></tr>");
            //if (data2.Tables["table"].Rows.Count != 0)
            //{
                foreach (DataTable table2 in data2.Tables)
                {
                    foreach (DataRow row in table2.Rows)
                    {

                        if (fila == true)
                        {
                            sbNew.AppendLine("<tr>");
                        }
                        else
                        {
                            sbNew.AppendLine("<tr class=\"whiteTable\"> ");
                        }
                        sbNew.AppendLine("<td class=\"tableSelect\"><input type=\"checkbox\" value=\"" + row["TitleID"].ToString() + "\" name=\"chk" + contTitle + "\" /> </td>");
                        sbNew.AppendLine("<td><img src=\""+strFolder+row["ImageTN"].ToString() +"\" /></td>");
                        sbNew.AppendLine("<td class=\"productName\"><p>" + row["Title"].ToString() + "</p></td>");
                        sbNew.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                        sbNew.AppendLine("<td class=\"descriptionTable\"><p>"+ addin.cutDescription(row["Titletext"].ToString(),241)+"</p></td>");
                        sbNew.AppendLine("<td class=\"priceTable\"><p>"+row["ER_Price"].ToString()+"</p></td></tr>");
                        if (contTitle < 499)
                        {
                            ids[contTitle] = Convert.ToInt32(row["TitleID"]);
                        }
                        contTitle++;
                        if (fila == true)
                        {
                            fila = false;
                        }
                        else
                        {
                            fila = true;
                        }    
                    }

                }
                sbNew.AppendLine("</TABLE>");
                btnCancelFeat.Visible = true;
                btnSaveFeat.Visible = true;
                sbNew.AppendLine("<input name=\"contTitle\" type=\"hidden\" value=\"" + contTitle + "\" />");
                div_SearchResult.InnerHtml = sbNew.ToString();
           // }
        }
        #endregion

        #region Delete
        if (Request["Delete"] != null)
        {

            deleteFeatureProducts(Convert.ToInt32(Request["Delete"]));
            if (Request["Home"] != null)
            {
                Response.Redirect("mnt_FeatureProducts.aspx?Home=True");
            }
            else
            {
                Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"]);
            }
        }
        #endregion             
    }
    #region Buttons methods
    //-------------------------------------------------------
    protected void btnAddFP_Click(object sender, EventArgs e)
    {
        if (Request["Home"] != null)
        {
            if (contmain == 3)
            {
                lbErroMax.Text = "Only 3 elements can be added as Feature products on the main site";
                
            }
            else {
                Response.Redirect("mnt_FeatureProducts.aspx?Home=true&AddNew=" + true);
            }
        }
        else
        {
            if (contmain == 2)
            {
                lbErroMax.Text = "Only 2 elements can be added as Feature products on the Sidebar";
            }
            else {
                Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&AddNew=" + true);
                
            }
        }
    }
    //-------------------------------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        bool flag = false;
        string search = "";
        int cont = 0;
        if (Convert.ToString(Request["txtCriteria"]) != "")
        {
            search = Convert.ToString(Request["txtCriteria"]);
            for (int i = 0; i < search.Length; i++)
            {
                if (((search[i] == '0') || (search[i] == '1') || (search[i] == '2') || (search[i] == '3') || (search[i] == '4')
                    || (search[i] == '5') || (search[i] == '6') || (search[i] == '7') || (search[i] == '8') || (search[i] == '9')) && (i > 0))
                {
                    cont++;
                }
            }
            if ((cont != 0) && (cont == (search.Length - 1)))
            {
                flag = true;
            }
               
           if (Request["Home"]!= null)
           {
               string criteria = Convert.ToString(Request["txtCriteria"]);
               if (flag == true)
               {
                   int id = Convert.ToInt32(Request["txtCriteria"].ToString());
                   Response.Redirect("mnt_FeatureProducts.aspx?Home=true&AddNew=" + true + "&SearchID=" + id + "&type=number");                  
               }
               else {
                   Response.Redirect("mnt_FeatureProducts.aspx?Home=true&AddNew=" + true + "&SearchID=" + search + "&type=text");
               }
            }
            else
            {
                if (flag == true)
                {
                    int id = Convert.ToInt32(Request["txtCriteria"].ToString());                    
                    Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&AddNew=" + true+ "&SearchID=" + id + "&type=number");
                }
                else
                {
                    Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&AddNew=" + true + "&SearchID=" + search + "&type=text");
                    
                }                
            }
        }
 
    }
    //-------------------------------------------------------
    protected void btnCancelFeat_Click(object sender, EventArgs e)
    {
        if (Request["Home"] != null)
        {
            Response.Redirect("mnt_FeatureProducts.aspx?Home=True");
        }
        else{
            Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"]);
        }

    }
    //-------------------------------------------------------
    protected void btnSaveFeat_Click(object sender, EventArgs e)
    {
        string csub = Request["contTitle"];
        if (contTitle != 0)
        {
            if (Convert.ToInt32(contTitle) > 0)
            {
                SaveFeatureProducts(Convert.ToInt32(contTitle));
            }
        }
    }
    //------------------------------------------------------------
    private void SaveFeatureProducts(int count)
    {
        int subjectid = 0;
        string vsub;
        int isub;
        string search = Convert.ToString(Request["txtPosition"]);
        if (validationpos(search) == true )
        {
            for (int i = 0; i <= count; i++)
            {
                vsub = Request["chk" + i];
                isub = Convert.ToInt32(vsub);
                if (vsub != null )
                {
                    //if (validateId(isub) == false)
                    //{
                        if ((Request["Mainsite"] == "1") && (Convert.ToInt32(Session["subjId"])) == 0)
                        {
                            subjectid = firstSubject;
                        }
                        else
                        {
                            subjectid = Convert.ToInt32(Session["subjId"]);
                        }
                        if (Request["Home"] != null)
                        {                            
                            addFeatureProduct(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), 0, isub, true, Convert.ToInt32(Request["txtPosition"])); ;
                            Response.Redirect("mnt_FeatureProducts.aspx?Home=True");
                        }
                        else {
                            addFeatureProduct(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["SubjID"]), isub, false, Convert.ToInt32(Request["txtPosition"])); ;
                            Response.Redirect("mnt_FeatureProducts.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"]);
                        }
                         
                    //} else {
                    //    lbPositionError.Text = "**This product is already feature product**";
                    //    lbPositionError.Visible = true;
                    //}
                }
            }
         }
    }
    //------------------------------------------------------------
    #endregion
      
    protected string searchTitle(int titleID)
    {

        DataSet title = new DataSet();
        string tittle = "";
        title = getFeatureProductById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),titleID);

        foreach (DataTable table in title.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                tittle = Convert.ToString(row["Title"]);
            }
        }
        return tittle;

    }
    //------------------------------------------------------------
    
    //------------------------------------------------------------
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
            position = Convert.ToInt32(search);
            if (position < 20)
            {
                flag = true;
                for (int j = 0; j < vector.Length; j++)
                {
                    if (vector[j] == position)
                    {
                        flag = false;
                    }
                }
            }
        }
        return flag;
    }
    //-----------------------------------
    //protected bool validateId(int id)
    //{
    //    bool flag = false;
    //    for (int i = 0; i < ids.Length; i++)
    //    {
    //        if (id == ids[i])
    //        {
    //            flag = true;

    //        }
    //    }
    //    return flag;
    //}
    //-----------------------------------

    protected void ddlSubject_Load()
    {
                DataSet Subject = new DataSet();
        Subject = getAllSubject(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(" option value=\"0\"selected=\"selected\">None</option>");               
        foreach (DataTable table in Subject.Tables)
        {
            foreach (DataRow row2 in table.Rows)
            {
                
                int task = Convert.ToInt32(row2["active"]);
                if (task != 0)
                {
                    sb.AppendLine("<option value=\"" + row2["SubjId"].ToString() + "\" " + (row2["SubjId"].ToString() == IndexPos ? "selected=\"selected\"" : "") + ">" + row2["cat"].ToString() + "</option>");               
                }

            }
        }
        PLaceHolder_Cmb_Test.Controls.Add(new LiteralControl(sb.ToString()));
    }
}
