using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Content
/// </summary>
public class ContentNavigation: ContentGroup
{
	public ContentNavigation()
	{

	}

    public DataSet getAllContent(int siteId)
    {
        return getAllContentGroups(siteId);
    }
}
