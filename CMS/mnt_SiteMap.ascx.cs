using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using miniSitemap;

namespace miniSitemap
{

    public partial class CMS_mnt_SiteMap : System.Web.UI.UserControl
    {
        private int intSite;
        private int intCont;
        //--
        protected void Page_Load(object sender, EventArgs e)
        {
            intSite = Convert.ToInt32(HttpContext.Current.Session["siteId"].ToString());
            if (HttpContext.Current.Session["contId"] != null)
            {
                intCont = Convert.ToInt32(HttpContext.Current.Session["contId"].ToString());
            }
            else {
                intCont = 1;
            }
            LoadAboutUs();
            LoadResourceCenter();
            LoadHighlights();
            LoadBrands();
            LoadTheater();

        }
        
        protected void LoadAboutUs()
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet dsAboutUs = new DataSet();
            StringBuilder sb = new StringBuilder();
            dsAboutUs = us.Get_LeftMenu_All_AboutUs(intSite);
            foreach (DataTable table in dsAboutUs.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sb.AppendLine("<li style=\"cursor:hand\" onclick=\"getInternalPagelink('" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "');\">" + row["genetitle"].ToString() + "</li>");
                }
            }

            PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
            dsAboutUs = null;
            sb = null;
        }
        //--------------------------------------------------------------------------------------
        protected void LoadResourceCenter()
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet dsResource = new DataSet();
            StringBuilder sb = new StringBuilder();
            int asm = 1;
            dsResource = us.Get_LeftMenu_All_ResourceCenter(intSite,intCont);
            foreach (DataTable table in dsResource.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    sb.AppendLine("<li style=\"cursor:hand\"  onclick=\"getInternalPagelink('" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&TypeGen=1&am=2&asm=" + asm.ToString() + "');\">" + row["genetitle"].ToString() + "</li>");
                    asm++;
                }
            }

            PlaceHolder_ResourceCenter.Controls.Add(new LiteralControl(sb.ToString()));
            dsResource = null;
            sb = null;
        }
        //--------------------------------------------------------------------------------------
        protected void LoadHighlights()
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet dsResource = new DataSet();
            StringBuilder sb = new StringBuilder();
            dsResource = us.getallHiglights(intSite, intCont);
            foreach (DataTable table in dsResource.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    sb.AppendLine("<li style=\"cursor:hand\" onclick=\"getInternalPagelink('" + row["HighLink"].ToString() + "');\">" + row["HighTitle"].ToString() + "</li>");
                }
            }
            PlaceHolder_highlights.Controls.Add(new LiteralControl(sb.ToString()));
            dsResource = null;
            sb = null;
        }
        //--------------------------------------------------------------------------------------
        protected void LoadBrands()
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet dsResource = new DataSet();
            StringBuilder sb = new StringBuilder();
            dsResource = us.getallFeaturebrands(intSite, intCont);
            foreach (DataTable table in dsResource.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    sb.AppendLine("<li style=\"cursor:hand\" onclick=\"getInternalPagelink('PublisherList.aspx?idP=" + row["TitleId"] + "');\">" + searchTitlebrand(Convert.ToInt32(row["TitleId"])) + "</li>");
                }
            }
            PlaceHolder_Brands.Controls.Add(new LiteralControl(sb.ToString()));
            dsResource = null;
            sb = null;
        }
        //--------------------------------------------------------------------------------------
        protected void LoadTheater()
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet dsResource = new DataSet();
            StringBuilder sb = new StringBuilder();
            dsResource = us.getAlltheater(intSite, intCont);
            foreach (DataTable table in dsResource.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    sb.AppendLine("<li style=\"cursor:hand\" onclick=\"getInternalPagelink('" + row["FeatLink"].ToString() + "');\">" + row["FeatTitle"].ToString() + "</li>");
                }
            }
            PlaceHolder_theater.Controls.Add(new LiteralControl(sb.ToString()));
            dsResource = null;
            sb = null;
        }
        //-----------------------------------------
        protected string searchTitlebrand(int titleID)
        {
            ucCMSSitemap us = new ucCMSSitemap();
            DataSet title = new DataSet();
            string tittle = "";
            title = us.getbrandTitle(titleID);

            foreach (DataTable table in title.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    tittle = Convert.ToString(row["PubName"]);
                }
            }
            return tittle;

        }


    }


}
