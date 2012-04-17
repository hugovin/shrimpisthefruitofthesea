<%@ Application Language="C#" %>

<script runat="server">
   
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex is HttpRequestValidationException)
        {
            Response.Write("Request Server with illegal character");
            Server.ClearError(); // ClearError() stop bubble up to Application_Error()。
        }
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
       
    }
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
    void Application_Error(object sender, EventArgs e)
    {

        

    }
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        string SiteId = "";
        string pageUrl = Request.Url.AbsoluteUri;
        
        
        if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
        {
        	Global.globalSiteNameAbr = SiteConstants.globalSiteSbNameAbr;
            Global.globalSiteId = SiteConstants.globalSiteSbSiteId;
            Global.globalSiteImagesPath = SiteConstants.globalSiteSbImages;
            Global.globalSiteStylePath = SiteConstants.globalSiteSbStyles;
        }
        else
        {
        	Global.globalSiteNameAbr = SiteConstants.globalSiteErNameAbr;
            Global.globalSiteId = SiteConstants.globalSiteErSiteId;
            Global.globalSiteImagesPath = SiteConstants.globalSiteErImages;
            Global.globalSiteStylePath = SiteConstants.globalSiteErStyles;
        }
        
        //string SiteId = ConfigurationManager.AppSettings["SiteId"];
        string SiteImagesPath = ConfigurationManager.AppSettings["SiteImagesPath"];
        string SiteImagesPathThumb = ConfigurationManager.AppSettings["SiteImagesPathThumb"];
        Session["SiteID"] = Global.globalSiteId;
        Session["ContentId"] = "1";
        Session["SiteImagesPath"] = SiteImagesPath;
        Session["SiteImagesPathThumb"] = SiteImagesPathThumb;
        Session["siteCustEmail"] = "";
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session[SiteConstants.UserId] = null;
    }
</script>
