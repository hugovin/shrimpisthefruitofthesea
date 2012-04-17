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
public partial class trials : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    public string _ba = "";
    public int _pg = 0, _pgg = 0;
    public int _startRecord = 0, _maxRedord = 0, _ds_nr = 0;
    private int PageSize, PageNum, FirstRow;
    private string _sb = "1";
    public string[] title = new string[3];
    public string[] subTitle = new string[3];
    public string[] content = new string[3];
    public string pagePrintTitle = "";
    public string pagePrintSubTitle = "";
    public string pagePrintContent = "";
    //--
    Addins addins = new Addins();
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
            CurrentChilPage = "trials.aspx";
        string s_pg = Request["pg"];

        if (s_pg != null && s_pg != ""){_pg = Convert.ToInt32(s_pg);}
        else{ _pg = 1; }

        string s_pgg = Request["pgg"];

        if (s_pgg != null && s_pgg != "")
        {
            try
            {
                _pgg = Convert.ToInt32(s_pgg);
            }
            catch (Exception ex) { _pgg = 1; }
        }
        else
        {
            _pgg = 1;
        }
        _sb = Request["pgg"];
        if (_sb != null && _sb != "")
        {
            _sb = _sb;
        }
        else
        {
            _sb = "1";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "trials.aspx";
        FindTrials();
        ////this.URLRedirect();
        Main_MasterPage m = (Main_MasterPage)Page.Master;
        m.pageTitleBar = "Trials Download - " + m.pageTitleBar;
        //--Bread
        Main_MasterPage main = (Main_MasterPage)Page.Master;
            main._site_breadLink += "<li><a href=resourcecenter.aspx?id=0>" + "Resource Center" + "</a></li>";
            main._site_breadLink += "<li class=\"last\"><a href=#><strong>Trials</strong></a></li>";
        //--
            LoadTitSubConFreeTools();
    }
    protected void FindTrials()
    {
        PageSize = 5;
        PageNum = _pg;
        FirstRow = PageNum * PageSize - PageSize;
        int CurrentRow = 0;
        SiteProduct trialsData = new SiteProduct();
        DataSet dsfinder = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        dsfinder = trialsData.Get_Site_Trials_Demos_List(52);
        if (dsfinder != null)
        {
            _ds_nr = dsfinder.Tables[0].Rows.Count;
        }
        else
        {
            _ds_nr = 0;
        }
        if (_ds_nr > 0)
        {
            LoadFinderPagination(_ds_nr);
            foreach (DataTable table in dsfinder.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if ((FirstRow <= CurrentRow) && (CurrentRow < (PageNum * PageSize)))
                    {
                        sb.AppendLine("<div class=\"trialProduct\"><p>");
                        if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
                        {
                            sb.AppendLine("<a href=\"" + "#" + "\" onClick=\"downloadTrial(" + row["TitleID"] + "); return false;\">" + row["Title"] + "</a></p>");
                        }
                        else
                        {
                            sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\">" + row["Title"] + "</a>");
                        }
                        sb.AppendLine("<p>" + row["TitleText"] + "</p><img src=\"" + Global.globalSiteImagesPath + "/lineDotted.jpg\" alt=\"\" width=\"721\" height=\"1\"/></div>");
                    }
                    if (CurrentRow > (PageNum * PageSize)) break;
                    CurrentRow++;
                }
            }
            pagesContent.Text = sb.ToString();
        }

    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("TrialsDownloads_pagegroup-"+_pgg+"_page-"+_pg+".thtml");
        }
    }
    private void LoadFinderPagination(int dsNumRec)
    {
        int numpagesrest = dsNumRec % 5;
        int numpages = dsNumRec / 5;
        if (numpagesrest >= 1) numpages++;
        int contrec = 0;

        StringBuilder sba = new StringBuilder();
        StringBuilder sbb = new StringBuilder();
        StringBuilder sbc = new StringBuilder();
        sbc.AppendLine("<div class=\"pagination\">");
        if (_pg >= 2)
        {
            sbc.AppendLine("<div id=\"pagArrowPreviuos\">");
            if (_pg == (_pgg * PageSize - 4))
            {
                sbc.AppendLine("<a href=\"trials.aspx?pgg=" + (_pgg - 1).ToString() + "&pg=" + (_pg - 1).ToString() + "\">PREVIOUS</a>");
            }
            else
            {
                sbc.AppendLine("<a href=\"trials.aspx?pgg=" + (_pgg).ToString() + "&pg=" + (_pg - 1).ToString() + "\">PREVIOUS</a>");
            }
            sbc.AppendLine("</div>");
        }
        sbc.AppendLine("<div class=\"pagIndex\">");
        sbc.AppendLine("<ul>");
        for (int i = (_pgg * PageSize - 4); i <= numpages; i++)
        {
            contrec++;
            if (i > ((_pgg * PageSize))) { break; }
            sbc.AppendLine("<li><a href=\"trials.aspx?pgg=" + (_pgg).ToString() + "&pg=" + i.ToString() + "\">");
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
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"trials.aspx?pgg=" + (_pgg + 1).ToString() + "&pg=" + (_pg + 1).ToString() + "\">NEXT</a></div>");
            }
            else
            {
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"trials.aspx?pgg=" + (_pgg).ToString() + "&pg=" + (_pg + 1).ToString() + "\">NEXT</a></div>");
            }
        }
        sbc.AppendLine("<div id=\"pagDivider\">|</div>");
        if (numpages > 5)
        {
            sbc.AppendLine("<div id=\"pagNext5\"><a href=\"trials.aspx?pgg=" + (_pgg + 1).ToString() + "&pg=" + ((_pgg * PageSize) + 1).ToString() + "\">Next 5</a></div>");
            
        }
        sbc.AppendLine("</div>");
        paginationDown.Text = paginationUp.Text = sba.ToString() + sbb.ToString() + sbc.ToString();
        sba = null;
        sbb = null;
        sbc = null;

    }
    protected void LoadTitSubConFreeTools()
    {
        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();
        int cont = 0;

        dsclassificationProducts = classificationProducts.Get_Title_Content_Site_Free_Tools();
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                title[cont] = row["FreeTitle1"].ToString();
                subTitle[cont] = row["FreeSubTitle1"].ToString();
                content[cont] = row["FreeContent1"].ToString();

                cont++;

                title[cont] = row["FreeTitle2"].ToString();
                subTitle[cont] = row["FreeSubTitle2"].ToString();
                content[cont] = row["FreeContent2"].ToString();

                cont++;

                title[cont] = row["FreeTitle3"].ToString();
                subTitle[cont] = row["FreeSubTitle3"].ToString();
                content[cont] = row["FreeContent3"].ToString();
            }
        }
        pagePrintTitle= title[0].ToString();
        pagePrintSubTitle = subTitle[0].ToString();
        pagePrintContent =  content[0].ToString();
    }
}
