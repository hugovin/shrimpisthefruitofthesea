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


public partial class PublisherList : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string pubId = "";
    private string strFolder = SiteConstants.imagesPathTb;

    //Paginación
    public int _pg = 0, _pgg = 0, _ds_nr = 0;
    private int PageSize, PageNum, FirstRow;
    private string _sb = "1";

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
            CurrentChilPage = "publisherlist.aspx";
        //--
       

        string s_pg = Request["pg"];
        if (s_pg != null && s_pg != "")
        {
            _pg = Convert.ToInt32(s_pg);
        }
        else
        {
            _pg = 1;
        }
        string s_pgg = Request["pgg"];
        if (s_pgg != null && s_pgg != "")
        {
            _pgg = Convert.ToInt32(s_pgg);
        }
        else
        {
            _pgg = 1;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "publisherlist.aspx";
        
        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        pubId = Request["idP"];

        LoadPubHead();
        LoadPubContent();
        ////this.URLRedirect();
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li class=\"last\"><a href=\"#\" onClick=\"location.reload(true);\"><strong>" + "Publisher List" + "</strong></a></li>";
    }

    protected void LoadPubHead()
    {
        SiteProduct publisherList = new SiteProduct();
        DataSet dspublisherList = new DataSet();
        StringBuilder sb = new StringBuilder();

        dspublisherList = publisherList.Get_Title_Content_Publisher(Convert.ToInt32(pubId));
        if (dspublisherList != null)
        {
	        foreach (DataTable table in dspublisherList.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                sb.AppendLine("<div class=\"contHead\">");
	                sb.AppendLine("<h3>" + row["Description"].ToString() + "</h3>");
	                sb.AppendLine("<h4>" + row["PubName"].ToString() + "</h4>");
	                sb.AppendLine(row["Contenido"].ToString());
	                sb.AppendLine("</div>");
	                PlaceHolder_PublisherList_Head.Controls.Add(new LiteralControl(sb.ToString()));
	            }
	        }
        }
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Cleaner c = new Cleaner();
            Response.Redirect("publisher.grouppage"+_pgg+"_page-"+_pg+"_p-"+pubId+".plhtml");

        }
    }
    protected void LoadPubContent()
    {
        //--
        PageSize = 5;
        PageNum = _pg;
        FirstRow = PageNum * PageSize - PageSize;
        int CurrentRow = 0;
        //--

        SiteProduct publisherList = new SiteProduct();
        DataSet dspublisherList = new DataSet();
        StringBuilder sb = new StringBuilder();
        string pubName = ""; 
        dspublisherList = publisherList.Get_All_Publisher_By_Id(Convert.ToInt32(pubId));
        _ds_nr = dspublisherList.Tables[0].Rows.Count;
        LoadFinderPagination(_ds_nr, pubId);

        foreach (DataTable table in dspublisherList.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if ((FirstRow <= CurrentRow) && (CurrentRow < (PageNum * PageSize)))
                {
                    if (row["New_Flag"].ToString() == "1") { sb.AppendLine("<div class=\"prodContNew\">"); }
                    else { sb.AppendLine("<div class=\"prodCont\">"); }

                    sb.AppendLine("<div class=\"prodImg\">");
                        sb.AppendLine("<div id=\"boxContImage\"  style=\"width:102px; height:127px;\">");
                        sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" style=\"width: 102px;height:127px;\" src=\"" + strFolder + "tn_" + row["imageTN"].ToString() + "\" ></a>"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                    pubName = row["pubname"].ToString();
                    sb.AppendLine("<div class=\"prodDesc\">");
                    sb.AppendLine("<h2><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                    sb.AppendLine("<p><em>by: </em><a href=\"PublisherList.aspx?idP=" + row["PubId"].ToString() + "\">" + row["pubname"].ToString() + "</a></p>");
                    sb.AppendLine("<h3> $" + Convert.ToDouble(row["er_price"].ToString()) + "</h3>");
                    
                    if (Convert.ToDouble(row["youShave"].ToString()) > 0)
                    {
                        sb.AppendLine("<h4>Your DISCOUNTED price</h4>");
                        sb.AppendLine("<h5>You Save: $" + Convert.ToDouble(row["youShave"].ToString()) + "</h5>");
                    }

                    sb.AppendLine("<a href=\"#\" id=\"A2\" class=\"mb\" title=\"\" rel=\"type:element\">" + Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/addToCart.jpg\" width=\"109\" height=\"26\" />", row["titleid"].ToString(), row["defaultsku"].ToString(), 1, 0) + " </a>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"prodNumber\">");
                    sb.AppendLine("<div class=\"prodNumCont\">");

                    if ((row["Plat_Win_Flag"].ToString() == "1") && (row["Plat_Mac_Flag"].ToString() == "2"))
                    { sb.AppendLine("<p>Mac / Windows</p>"); }
                    else
                    { sb.AppendLine("<p>" + (row["Plat_Win_Flag"].ToString() == "1" ? "Windows" : "") + "" + (row["Plat_Mac_Flag"].ToString() == "2" ? "Mac" : "") + "</p>"); }

                    sb.AppendLine("<div class=\"numBG\">Grades<br /><span>" + row["grades"].ToString() + "</span></div>");
                    sb.AppendLine("<p>Item #: " + row["defaultSKU"].ToString() + "</p>");
                    sb.AppendLine("</div>");

                    //--Login Validate
                    if ((bool)Session[SiteConstants.UserValidLogin])
                    {
                        sb.AppendLine(" <a href=\"addWish.aspx?p=" + row["TitleId"].ToString() + "\" rel=\"width:800,height:300,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add Product\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                    }
                    else
                    {
                        sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                    }

                    

                    sb.AppendLine("<a href=\"requestaquote.aspx?title=" + row["Title"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/quote.jpg\" border=\"0\"></a>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"prodMore\"><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">+  learn more</a></div>");
                    sb.AppendLine("</div>");
                }
                if (CurrentRow > (PageNum * PageSize)) break;
                CurrentRow++;
            }
        }

        PlaceHolder_PublisherList_Content.Controls.Add(new LiteralControl(sb.ToString()));
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Publisher List - " + pubName + " - " + main.pageTitleBar;
        sb = null;
        publisherList = null;
        dspublisherList = null;
    }

    private void LoadFinderPagination(int dsNumRec, string pubId)
    {
        int numpagesrest = dsNumRec % 5;
        int numpages = dsNumRec / 5;
        if (numpagesrest >= 1) numpages++;
        int contrec = 0;

        StringBuilder sba = new StringBuilder();
        StringBuilder sbb = new StringBuilder();
        StringBuilder sbc = new StringBuilder();
        //--
      
        //--
        sbc.AppendLine("<div id=\"pagResult\">");
        sbc.AppendLine("<div class=\"pagination\">");
        if (_pg >= 2)
        {
            sbc.AppendLine("<div id=\"pagArrowPreviuos\">");
            if (_pg == (_pgg * PageSize - 4))
            {
                sbc.AppendLine("<a href=\"publisherList.aspx?pgg=" + (_pgg - 1).ToString() + "&pg=" + (_pg - 1).ToString() + "&idP=" + pubId + "\">PREVIOUS</a>");
            }
            else
            {
                sbc.AppendLine("<a href=\"publisherList.aspx?pgg=" + (_pgg).ToString() + "&pg=" + (_pg - 1).ToString() + "&idP=" + pubId + "\">PREVIOUS</a>");
            }
            sbc.AppendLine("</div>");
        }
        sbc.AppendLine("<div class=\"pagIndex\">");
        sbc.AppendLine("<ul>");
        for (
            
            int i = (_pgg * PageSize - 4); i <= numpages; i++)
        {
            contrec++;
            if (i > ((_pgg * PageSize))) { break; }
            sbc.AppendLine("<li><a href=\"publisherList.aspx?pgg=" + (_pgg).ToString() + "&pg=" + i.ToString() + "&idP=" + pubId + "\">");
            if (_pg == i)
            {
                sbc.AppendLine("<span class=\"current\">" + i.ToString() + "</span>");
            }
            else { sbc.AppendLine("" + i.ToString() + ""); }
            sbc.AppendLine("</a></li>");

        }
        sbc.AppendLine("</ul>");
        sbc.AppendLine("</div>");
        if (numpages > _pg)
        {
            if (_pg == (_pgg * PageSize))
            {
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"publisherList.aspx?pgg=" + (_pgg + 1).ToString() + "&pg=" + (_pg + 1).ToString() + "&idP=" + pubId + "\">NEXT</a></div>");
            }
            else
            {
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"publisherList.aspx?pgg=" + (_pgg).ToString() + "&pg=" + (_pg + 1).ToString() + "&idP=" + pubId + "\">NEXT</a></div>");
            }
        }
        sbc.AppendLine("<div id=\"pagDivider\">|</div>");
        if (numpages > 5)
        {
            sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"publisherList.aspx?pgg=" + (_pgg + 1).ToString() + "&pg=" + ((_pgg * PageSize) + 1).ToString() + "&idP=" + pubId + "\">Next 5</a></div>");
        }
        sbc.AppendLine("</div>");
        sbc.AppendLine("</div>");

        PlaceHolder_resultControls_Head.Controls.Add(new LiteralControl(sba.ToString() + sbc.ToString()));
        PlaceHolder_resultControls_Foot.Controls.Add(new LiteralControl(sba.ToString() + sbc.ToString()));

        sba = null;
        sbb = null;
        sbc = null;

    }
}
