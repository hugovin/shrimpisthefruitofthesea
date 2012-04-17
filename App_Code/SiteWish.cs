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
/// Summary description for SiteWish
/// </summary>
public class SiteWish
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
    Whis whis = new Whis();
    Login loginUser = new Login();

    private string _title = "";
    private string _titleText = "";
    private int _sessionId = 0;
    private int _statusWish = 0;
    private int _deleteFlag = 0;

    private int _titleId = 0;
    private string _defaultSKU = "";
    private int _wishListId = 0;
    private string _comments = "";

	public SiteWish()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //***********************************************************************************************************//
    //Get and Set
    public int _TitleId
    {
        get { return _titleId;  }
         set { _titleId = value; }
    }

    public string _Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public string _DefaultSKU
    {
        get { return _defaultSKU;  }
        set { _defaultSKU = value; }
    }

    public string _TitleText
    {
        get { return _titleText; }
        set { _titleText = value; }
    }

    public int _WishListId
    {
        get { return _wishListId; }
        set { _wishListId = value; }
    }

    //***********************************************************************************************************//

    public void Add_Whish_Header()
    {              
        whis.Add_Whish_Header(this._Title, this._TitleText, loginUser.getLoginId(), this._statusWish, this._deleteFlag,SiteId,ContId);
    }

    public string Add_Whish_Detail()
    {
        return whis.Add_Whish_Detail(this._WishListId, this._TitleId, this._DefaultSKU, this._comments, SiteId, ContId);
    }
    public int Del_Whish_Header(int wishId)
    {
        return whis.Del_Whish_Header(wishId);
    }

    public int Del_Whish_Detail(int wishId,int titleid,string sku)
    {
        return whis.Del_Whish_Detail(wishId,titleid,sku);
    }

    public void Upd_Comment_Whish_Detail(int wishId, string comments)
    {
        whis.Upd_Comment_Whish_Detail(wishId, comments);
    }

    public DataSet Get_Wish_by_IdSession()
    {
        return whis.Get_Wish_by_IdSession(loginUser.getLoginId(), SiteId, ContId);
    }

    public DataSet Get_All_Wish_By_Session()
    {
        return whis.Get_All_Wish_By_Session(loginUser.getLoginId(),SiteId,ContId);
    }

    // Added by Jordan Sherer
    // re: Wishlist Sharing
    // Aug 28, 2009
    public DataSet Get_Wish_by_GUID(string id)
    {
        return whis.Get_Wish_by_GUID(id,SiteId, ContId);
    }

    public DataSet Get_Wish_by_GUID(Guid id)
    {
        return whis.Get_Wish_by_GUID(id, SiteId, ContId);
    }

    public DataSet Get_All_Wish_By_GUID(string id)
    {
        return whis.Get_All_Wish_By_GUID(id, SiteId, ContId);
    }

    public DataSet Get_All_Wish_By_GUID(Guid id)
    {
        return whis.Get_All_Wish_By_GUID(id, SiteId, ContId);
    }
}
