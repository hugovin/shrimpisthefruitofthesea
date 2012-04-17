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

public partial class CheckoutControls_ItemSummary : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    private bool SubtotalOnly = false;
    public CheckoutControls_ItemSummary() { }
    public CheckoutControls_ItemSummary(Cart c) { this.CurrentCart = c; }
    public CheckoutControls_ItemSummary(Cart c, bool subtotal_only) { this.CurrentCart = c; this.SubtotalOnly = subtotal_only; }
    public ArrayList _ConfigurationList = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        BindData(CurrentCart.Items);     
        

        SubTotalLiteral.Text = Helper.Dollar(CurrentCart.SubTotal);
        TaxLiteral.Text = Helper.Dollar(CurrentCart.Tax);
        ShippingLiteral.Text = Helper.Dollar(CurrentCart.Shipping + CurrentCart.Handling);
        TotalPriceLiteral.Text = Helper.Dollar(CurrentCart.Total);

	if(SubtotalOnly)
        {
		TaxLiteral.Visible = ShippingLiteral.Visible = TotalPriceLiteral.Visible = false;
		TaxLabel.Visible = ShippingLabel.Visible = TotalLabel.Visible = false;
	}
    }

    #region DataTable
    private DataTable CreateDataSource()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Title", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("UnitPrice", typeof(string));
        dt.Columns.Add("SubTotal", typeof(string));
        return dt;
    }

    private DataTable BuildDataSource(CartItem[] items)
    {
        DataTable dt = CreateDataSource();
        CartDB db = new CartDB();
        DataSet data = new DataSet();
             

        foreach (CartItem item in items)
        {
            string title = "", pubname = "", puburl = "";
            string skuDesc = "";
            if (item.ProductTitleId == int.Parse(Resources.Resource.TorchProductId))
            {
                Session["TorchInCart"] = true;
            }
            DataSet ds = db.CartProductGetBySKU(item.ProductSKU);
            if (ds != null)
            {
                using (DataTableReader dtr = ds.CreateDataReader())
                {
                    while (dtr.Read())
                    {
                        title = Helper.IsString(dtr.GetValue(dtr.GetOrdinal("Title")), "");
                    }
                    dtr.Close();
                }
            }

            DataRow row = dt.NewRow();
            data = db.Get_SkuDesc_By_SKU(item.ProductSKU);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow rowSku in table.Rows)
                {
                    title = rowSku["SKUDesc"].ToString();
                }
            }

            row["Title"] = title;
            row["Quantity"] = item.Quantity.ToString();
            row["UnitPrice"] = Helper.Dollar(item.UnitPrice);
            row["SubTotal"] = Helper.Dollar(item.SubTotal);
            dt.Rows.Add(row);
        }

        dt.AcceptChanges();
        return dt;
    }

    private void BindData(CartItem[] items)
    {
        ItemsDataRepeater.DataSource = BuildDataSource(items);
        ItemsDataRepeater.DataBind();
        /*
        CartDisplayPanel.Visible = items.Length > 0;
        CartEmptyPanel.Visible = !CartDisplayPanel.Visible;

        TotalValueLiteral.Text = Helper.Dollar(CurrentCart.SubTotal);
        */
    }
    #endregion
}
