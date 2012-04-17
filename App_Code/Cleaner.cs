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
/// Class made to clean all strings for use in URL, file names and others. Made by Danilo Ramirez.
/// </summary>
public class Cleaner
{
	public Cleaner()
	{
	}
    public string cleanURL(string url){
        //url = url.Replace(" ", "_");
        url = url.Replace("/", "_");
        url = url.Replace("%", "_");
        url = url.Replace("&", "_");
        url = url.Replace("#", "_");
        url = url.Replace("*", "_");
        url = url.Replace("?", "_");
        url = url.Replace("*", "_");
        url = url.Replace("=", "_");
        url = url.Replace("--", "_");
        url = url.Replace("{", "_");
        url = url.Replace("}", "_");
        url = url.Replace("+", "_");
        url = url.Replace("!", "_");
        url = url.Replace("[", "_");
        url = url.Replace("]", "_");
        url = url.Replace("\"", "_");
        //url = url.Replace(":", "-");
        //url = url.Replace("[^a-z0-9\\-]", "_");
        return url;
    }
}

