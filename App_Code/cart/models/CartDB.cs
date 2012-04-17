using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class CartDB : ConnectSql
{
    private int orderidAdditem = 0;
    private int lineNumber = 1;
    private int paymentID = 0; 
    public CartDB() { }

    #region Static Methods
    public static void AddSetParameter(ref SqlCommand cmd, string param_name, SqlDbType param_type, object value)
    {
        CartDB.AddSetParameter(ref cmd, param_name, param_type, -1, value);
    }
    public static void AddSetParameter(ref SqlCommand cmd, string param_name, SqlDbType param_type, int size, object value)
    {
        if (size == -1)
        {
            cmd.Parameters.Add(param_name, param_type);
        }
        else
        {
            cmd.Parameters.Add(param_name, param_type, size);
        }

        cmd.Parameters[param_name].Value = value;
    }
    #endregion

    #region Cart DB Methods
    public bool CartCreate(int site_id, string user_id)
    {
        return this.CartCreate(site_id, user_id, 0, 0, DBNull.Value, DBNull.Value, DBNull.Value, 0, 0, 0, 0, 0, false);
    }

    public bool CartCreate(int site_id, string user_id, int checkout_step, int shipping_option, object payment_id, object 

shipping_location_id,
                           object billing_location_id, double tax, double shipping, double handling, double subtotal, double total, bool 

dirty)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;
            
            cmd.CommandText = "Cart_Add";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@SiteId", SqlDbType.Int, site_id);
            CartDB.AddSetParameter(ref cmd, "@UserId", SqlDbType.VarChar, 50, user_id);
            CartDB.AddSetParameter(ref cmd, "@CheckoutStep", SqlDbType.Int, checkout_step);
            CartDB.AddSetParameter(ref cmd, "@ShippingOption", SqlDbType.Int, shipping_option);
            CartDB.AddSetParameter(ref cmd, "@PaymentId", SqlDbType.Int, payment_id);
            CartDB.AddSetParameter(ref cmd, "@ShippingLocationId", SqlDbType.Int, shipping_location_id);
            CartDB.AddSetParameter(ref cmd, "@BillingLocationId", SqlDbType.Int, billing_location_id);
            CartDB.AddSetParameter(ref cmd, "@Tax", SqlDbType.Decimal, tax);
            CartDB.AddSetParameter(ref cmd, "@Shipping", SqlDbType.Decimal, shipping);
            CartDB.AddSetParameter(ref cmd, "@Handling", SqlDbType.Decimal, handling);
            CartDB.AddSetParameter(ref cmd, "@SubTotal", SqlDbType.Decimal, subtotal);
            CartDB.AddSetParameter(ref cmd, "@Total", SqlDbType.Decimal, total);
            CartDB.AddSetParameter(ref cmd, "@Dirty", SqlDbType.Bit, dirty);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            /*
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    throw new Exception(rdr.GetString(rdr.GetOrdinal("id")));
                }
                rdr.Close();
            }
            */

            if ((int)cmd.Parameters["@Return_Value"].Value != 1)
            {
                throw new CartException(String.Format("There was an error creating a cart: {0}", cmd.Parameters["@Return_Value"].Value));
            }
            
            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }



    public DataSet GetCartFromUserID(int site_id, string user_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_By_UserId";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@SiteId", SqlDbType.Int, site_id);
            CartDB.AddSetParameter(ref cmd, "@UserId", SqlDbType.VarChar, 50, user_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public DataSet GetCartFromCartID(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_By_CartId";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public DataSet GetCartFromGuid(Guid unique_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_By_Guid";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@UniqueId", SqlDbType.UniqueIdentifier, unique_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public bool UpdateCartCheckoutStep(int cart_id, int checkout_step)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_CheckoutStep";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@CheckoutStep", SqlDbType.Int, checkout_step);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdateTotals(int cart_id, decimal subtotal, decimal tax, decimal shipping, decimal handling, decimal total)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_CartTotals";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@SubTotal", SqlDbType.Decimal, subtotal);
            CartDB.AddSetParameter(ref cmd, "@Tax", SqlDbType.Decimal, tax);
            CartDB.AddSetParameter(ref cmd, "@Shipping", SqlDbType.Decimal, shipping);
            CartDB.AddSetParameter(ref cmd, "@Handling", SqlDbType.Decimal, handling);
            CartDB.AddSetParameter(ref cmd, "@Total", SqlDbType.Decimal, total);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdateShippingOption(int cart_id, int shipping_option)
    {
        return CartUpdateShipping(cart_id, shipping_option, 0);
    }

    public bool CartUpdateShipping(int cart_id, int shipping_option, decimal shipping)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_Shipping";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@ShippingOption", SqlDbType.Int, shipping_option);
            CartDB.AddSetParameter(ref cmd, "@Shipping", SqlDbType.Decimal, shipping);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdateDirty(int cart_id, bool dirty)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_CartDirty";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@Dirty", SqlDbType.Bit, dirty);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdateUserId(int cart_id, string user_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_UserId";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@UserId", SqlDbType.VarChar, 50, user_id);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdateCompleted(int cart_id, bool completed)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_Completed";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@Completed", SqlDbType.Bit, completed);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public decimal CalculateCartItemSubtotal(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Sum_ItemSubtotal";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            if (dataset != null)
            {
                using (DataTableReader dtr = dataset.CreateDataReader())
                {
                    while (dtr.Read())
                    {
                        return dtr.GetDecimal(dtr.GetOrdinal("CartSubtotal"));
                    }
                }
            }

            return (decimal)0;
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
            //throw new Exception(sqlex.Message);
            //return (decimal)0;
        }
        catch (Exception ex)
        {
            throw ex;
            //return (decimal)0;
        }
        finally
        {
            Close();
        }
    }

    public double CalculateCartItemsWeight(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Sum_ItemsWeight";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            if (dataset != null)
            {
                using (DataTableReader dtr = dataset.CreateDataReader())
                {
                    while (dtr.Read())
                    {
                        object obj_weight = dtr.GetValue(dtr.GetOrdinal("CartWeight"));
                       if (obj_weight == null || obj_weight == DBNull.Value)
                        {
                            return 1;
                        }
                        return decimal.ToDouble((decimal)obj_weight);
                    }
                }
            }

            return 0;
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
            //throw new Exception(sqlex.Message);
            //return (decimal)0;
        }
        catch (Exception ex)
        {
            throw ex;
            //return (decimal)0;
        }
        finally
        {
            Close();
        }
    }

    public decimal CartGetTaxFromLocation(int site_id, string country_code, string state_code, string zip_code)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "PublicPortal.dbo.stp_ORD_Find_TaxRate";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@SiteID", SqlDbType.Int, site_id);
            CartDB.AddSetParameter(ref cmd, "@CountryCode", SqlDbType.VarChar, 2, country_code);
            CartDB.AddSetParameter(ref cmd, "@StateCode", SqlDbType.VarChar, 2, state_code);
            CartDB.AddSetParameter(ref cmd, "@ZipCode", SqlDbType.VarChar, 20, zip_code);

            SqlParameter taxValue = new SqlParameter("@TaxRate", SqlDbType.Decimal);
            taxValue.Precision = 15;
            taxValue.Scale = 3;
            taxValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(taxValue);
            cmd.ExecuteNonQuery();

            return (decimal)cmd.Parameters["@TaxRate"].Value;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return 0;
        }
        catch (Exception ex)
        {
            //throw ex;
            return 0;
        }
        finally
        {
            Close();
        }
    }


    public decimal CalculateCartTaxRate(Cart CurrentCart)
    {
        CartDB db = new CartDB();
        if (CurrentCart.BillingLocation != null)
        {
            return db.CartGetTaxFromLocation(CurrentCart.SiteId, CurrentCart.BillingLocation.CountryCode,

CurrentCart.BillingLocation.StateCode, CurrentCart.BillingLocation.PostalCode);
        }
        else
        {
            return (decimal)0;
        }
    }

    public decimal CalculateCartShipping(Cart CurrentCart)
    {
        double weight = CurrentCart.ItemsWeight;
        foreach (ShippingOption option in ShippingOption.GetActiveShippingOptions())
        {
            if (option.ShippingOptionID == CurrentCart.ShippingOption)
            {
                double cost = 0;
                try
                {
                    if (UPS.CalculateShippingCost(CurrentCart.ShippingLocation, option.Code, weight, Constants.ShippingTypes.MIN_SHIPPING, 

out cost))
                    {
                        return (decimal)cost;
                    }
                }
                catch (UPSException ex)
                {
                    return (decimal)0;
                }
            }
        }

        return (decimal)0;
    }

    public decimal CalculateCartHandling(int cart_id)
    {
        return (decimal)0;
    }

    public void CleanCart(Cart cart)
    {
        decimal subtotal = CalculateCartItemSubtotal(cart.CartId);
        decimal taxrate = CalculateCartTaxRate(cart);
        decimal tax = subtotal * taxrate / 100;
        
        decimal shipping = CalculateCartShipping(cart);
        int count = 0;
        while (shipping == 0 && count < 5)
        {
            shipping = CalculateCartShipping(cart);
            count++;
        }
        if (shipping == 0) shipping = (decimal)Constants.ShippingTypes.MIN_SHIPPING;
        
        decimal handling = CalculateCartHandling(cart.CartId);

        decimal total = subtotal + tax + shipping + handling;

        bool updated = CartUpdateTotals(cart.CartId, subtotal, tax, shipping, handling, total);
        if (updated)
        {
            CartUpdateDirty(cart.CartId, false);
        }
    }


    #endregion

    #region Cart Item Methods
    public bool CartAddItem(int cart_id, object product_title_id, object product_sku, int quantity, decimal unit_price)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Add_Item";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@ProductTitleId", SqlDbType.Int, product_title_id);
            CartDB.AddSetParameter(ref cmd, "@ProductSKU", SqlDbType.VarChar, 20, product_sku);
            CartDB.AddSetParameter(ref cmd, "@Quantity", SqlDbType.Int, quantity);
            CartDB.AddSetParameter(ref cmd, "@UnitPrice", SqlDbType.Decimal, unit_price);
            CartDB.AddSetParameter(ref cmd, "@SubTotal", SqlDbType.Decimal, (decimal)quantity * unit_price);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            /*
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    throw new Exception(rdr.GetString(rdr.GetOrdinal("id")));
                }
                rdr.Close();
            }
            */

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();   
        }
    }

    public bool CartAddSKU(int cart_id, object product_sku, int quantity)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Add_ER_SKU";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@ER_SKU", SqlDbType.VarChar, 20, product_sku);
            CartDB.AddSetParameter(ref cmd, "@Quantity", SqlDbType.Int, quantity);
            
            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            /*
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    throw new Exception(rdr.GetString(rdr.GetOrdinal("id")));
                }
                rdr.Close();
            }
            */

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartDeleteItem(int cart_contents_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Del_Item";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartContentsId", SqlDbType.Int, cart_contents_id);
            
            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    //[Cart_Upd_CartItemQuantity]
    public bool CartUpdateItemQuantity(int cart_contents_id, int quantity, decimal subtotal)
    {
        try
        {
            if (quantity <= 0)
            {
                return this.CartDeleteItem(cart_contents_id);
            }

            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_CartItemQuantity";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartContentsId", SqlDbType.Int, cart_contents_id);
            CartDB.AddSetParameter(ref cmd, "@Quantity", SqlDbType.Int, quantity);
            CartDB.AddSetParameter(ref cmd, "@SubTotal", SqlDbType.Decimal, subtotal);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public DataSet CartGetAllItems(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_All_Items";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public DataSet CartProductGetBySKU(string sku)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Product_Get_By_SKU";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@SKU", SqlDbType.VarChar, 20, sku);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    #endregion

    #region Cart Location Methods
    public DataSet GetLocationFromLocationId(int location_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_Location_By_LocationId";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LocationId", SqlDbType.Int, location_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public DataSet GetLocationFromLocationType(int cart_id, int location_type)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_Location_By_LocationType";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@LocationType", SqlDbType.Int, location_type);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public enum LocationType
    {
        Billing=1, 
        Shipping=2
    }

    public bool CartAddLocation(int cartid, LocationType locationtype, string businessname, string fullname, string address1, string

address2, string city, string statecode, string postalcode, string countrycode, string phone, string email, string Title, string AreaCode, string PhoneExt, string Fax)
    {
        try
        {
            /*
            @CartId int
		   ,@LocationType int
           ,@BusinessName nvarchar(255)
           ,@FullName nvarchar(255)
           ,@Address1 nvarchar(255)
           ,@Address2 nvarchar(255)
           ,@City nvarchar(255)
           ,@StateCode nvarchar(4)
           ,@PostalCode nvarchar(20)
           ,@CountryCode nvarchar(4)
           ,@Phone nvarchar(30)*/
            
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Add_Location";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cartid);
            CartDB.AddSetParameter(ref cmd, "@LocationType", SqlDbType.Int, (int)locationtype);
            CartDB.AddSetParameter(ref cmd, "@BusinessName", SqlDbType.NVarChar, 255, businessname);
            CartDB.AddSetParameter(ref cmd, "@FullName", SqlDbType.NVarChar, 255, fullname);
            CartDB.AddSetParameter(ref cmd, "@Address1", SqlDbType.NVarChar, 255, address1);
            CartDB.AddSetParameter(ref cmd, "@Address2", SqlDbType.NVarChar, 255, address2);
            CartDB.AddSetParameter(ref cmd, "@City", SqlDbType.NVarChar, 255, city);
            CartDB.AddSetParameter(ref cmd, "@StateCode", SqlDbType.NVarChar, 4, statecode);
            CartDB.AddSetParameter(ref cmd, "@PostalCode", SqlDbType.NVarChar, 20, postalcode);
            CartDB.AddSetParameter(ref cmd, "@CountryCode", SqlDbType.NVarChar, 4, countrycode);
            CartDB.AddSetParameter(ref cmd, "@Phone", SqlDbType.NVarChar, 30, phone);
            CartDB.AddSetParameter(ref cmd, "@Email", SqlDbType.NVarChar, 255, email);
            CartDB.AddSetParameter(ref cmd, "@Title", SqlDbType.NVarChar, 255, Title);
            CartDB.AddSetParameter(ref cmd, "@AreaCode", SqlDbType.NVarChar, 50, AreaCode);
            CartDB.AddSetParameter(ref cmd, "@PhoneExt", SqlDbType.NVarChar, 50, PhoneExt);
            CartDB.AddSetParameter(ref cmd, "@Fax", SqlDbType.NVarChar, 50, Fax);
            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartDeleteAllByUserID(string userid)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Del_AllByUserID";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@UserID", SqlDbType.VarChar, userid);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();


            return ((int)cmd.Parameters["@Return_Value"].Value) > 0;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartDeleteAllLocations(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Del_AllLocations";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            
            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }


    #endregion

    #region Cart Payment Methods
    public bool CartAddPayment(int cart_id, PaymentType payment_type, string cctype, string last_four, string encrypted_cc, string 

encrypted_exp, string encrypted_cvv, string po_number, string file_name, byte[] file_contents)
    {
        try
        {
            /*
             @CartId int
            ,@PaymentType int
            ,@CCLastFourDigits nvarchar(4)
            ,@EncryptedCC nvarchar(512)
            ,@EncryptedCCExpiration nvarchar(512)
            ,@EncryptedCCCVV nvarchar(512)
            ,@POFileUpload varbinary(max)
            */

            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Add_Payment";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@PaymentType", SqlDbType.Int, (int)payment_type);
            CartDB.AddSetParameter(ref cmd, "@CCType", SqlDbType.NVarChar, 4, Helper.StringExists(cctype) ? (object)cctype : DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@CCLastFourDigits", SqlDbType.NVarChar, 4, Helper.StringExists(last_four) ? (object)last_four : 

DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCC", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_cc) ? (object)encrypted_cc 

: DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCCExpiration", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_exp) ? 

(object)encrypted_exp : DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCCCVV", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_cvv) ? 

(object)encrypted_cvv : DBNull.Value);

            CartDB.AddSetParameter(ref cmd, "@PONumber", SqlDbType.NVarChar, 50, Helper.StringExists(po_number) ? (object)po_number : 

DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@POFileName", SqlDbType.NVarChar, 255, Helper.StringExists(file_name) ? (object)file_name : 

DBNull.Value);

            if(file_contents == null)
            {
                CartDB.AddSetParameter(ref cmd, "@POFileUpload", SqlDbType.Image, DBNull.Value);
            }
            else
            {
                SqlParameter bin = new SqlParameter("@POFileUpload", SqlDbType.Image, file_contents.Length);
                cmd.Parameters.Add(bin);
                
                SqlBinary value = new SqlBinary(file_contents);
                cmd.Parameters["@POFileUpload"].SqlValue = value;
            }

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdatePayment(int payment_id, int cart_id, PaymentType payment_type, string cctype, string last_four, string 

encrypted_cc, string encrypted_exp, string encrypted_cvv, string po_number, string file_name, byte[] file_contents)
    {
        try
        {
            /*
             @CartId int
            ,@PaymentType int
            ,@CCLastFourDigits nvarchar(4)
            ,@EncryptedCC nvarchar(512)
            ,@EncryptedCCExpiration nvarchar(512)
            ,@EncryptedCCCVV nvarchar(512)
            ,@POFileUpload varbinary(max)
            */

            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Upd_Payment";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@PaymentId", SqlDbType.Int, payment_id);
            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);
            CartDB.AddSetParameter(ref cmd, "@PaymentType", SqlDbType.Int, (int)payment_type);
            CartDB.AddSetParameter(ref cmd, "@CCType", SqlDbType.NVarChar, 4, Helper.StringExists(cctype) ? (object)cctype : DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@CCLastFourDigits", SqlDbType.NVarChar, 4, Helper.StringExists(last_four) ? (object)last_four : 

DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCC", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_cc) ? (object)encrypted_cc 

: DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCCExpiration", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_exp) ? 

(object)encrypted_exp : DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@EncryptedCCCVV", SqlDbType.NVarChar, 512, Helper.StringExists(encrypted_cvv) ? 

(object)encrypted_cvv : DBNull.Value);

            CartDB.AddSetParameter(ref cmd, "@PONumber", SqlDbType.NVarChar, 50, Helper.StringExists(po_number) ? (object)po_number : 

DBNull.Value);
            CartDB.AddSetParameter(ref cmd, "@POFileName", SqlDbType.NVarChar, 255, Helper.StringExists(file_name) ? (object)file_name : 

DBNull.Value);

            if (file_contents == null)
            {
                CartDB.AddSetParameter(ref cmd, "@POFileUpload", SqlDbType.Image, DBNull.Value);
            }
            else
            {
                SqlParameter bin = new SqlParameter("@POFileUpload", SqlDbType.Image, file_contents.Length);
                cmd.Parameters.Add(bin);

                SqlBinary value = new SqlBinary(file_contents);
                cmd.Parameters["@POFileUpload"].SqlValue = value;
            }

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool CartUpdatePaymentOrderIDs(int payment_id, object order_id, object order_payment_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Payment_Upd_OrderPaymentIDs";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@PaymentId", SqlDbType.Int, payment_id);
            CartDB.AddSetParameter(ref cmd, "@ORD_ID", SqlDbType.Int, order_id);
            CartDB.AddSetParameter(ref cmd, "@ORD_Payment_ID", SqlDbType.Int, order_payment_id);
            
            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public DataSet GetPaymentFromPaymentId(int payment_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Get_Payment_By_PaymentId";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@PaymentId", SqlDbType.Int, payment_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public bool CartDeleteAllPayments(int cart_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Del_AllPayments";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@CartId", SqlDbType.Int, cart_id);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    #endregion

    #region ORDER Methods
    public bool Order_Post_Cart(Cart CompletedCart, int login_id, out int order_id, out int payment_id,string comments)
    {
        return Order_Post_Cart(CompletedCart, login_id, 0, out order_id, out payment_id,comments);
    }
    public bool Order_Post_Cart(Cart CompletedCart, int login_id, int sales_rep_helped, out int order_id, out int payment_id, string comments)
    {
       int orderR = 0;
        //orderstatusid = 1

        object orderdate = DateTime.Now;
        object completedate = DBNull.Value;
        object siteid = CompletedCart.SiteId;
        object orderstatusid = 1;
        object studentverifyflag = CompletedCart.HasStudentItems ? 1 : 0;
        object studentverifystatusid = 0;
        object salesrephelpedflag = sales_rep_helped;
        object paymentid = 0;
        object shipmethodid = CompletedCart.ShippingOption;
        object subtotal = CompletedCart.SubTotal;
        object shippingamt = CompletedCart.Shipping;
        object shippingweight = CompletedCart.ItemsWeight;
        object handlingamt = CompletedCart.Handling;
        object taxamt = CompletedCart.Tax;
        object ordertotal = CompletedCart.Total;
        object loginid = login_id;
        object firstname = CompletedCart.BillingLocation.FullName;
        object lastname = DBNull.Value;
        object email = CompletedCart.BillingLocation.Email; 
        object title = Session["UTitle"].ToString();
        object audienceid = Session["ContentId"].ToString();
        object areacode = Session["AreaCode"].ToString();
        object phone = CompletedCart.BillingLocation.Phone;
        object phoneext = Session["PhoneExt"].ToString();
        object fax = Session["Fax"].ToString();
        object billtologinaddressid = DBNull.Value;
        object billtobldgname = CompletedCart.BillingLocation.BusinessName;
        object billtoaddress1 = CompletedCart.BillingLocation.Address1;
        object billtoaddress2 = CompletedCart.BillingLocation.Address2;
        object billtocity = CompletedCart.BillingLocation.City;
        object billtostatecode = CompletedCart.BillingLocation.StateCode;
        object billtoprovince = DBNull.Value;
        object billtozip = CompletedCart.BillingLocation.PostalCode;
        object billtocountrycode = CompletedCart.BillingLocation.CountryCode;
        object shiptologinaddressid = DBNull.Value;
        object shiptobldgname = CompletedCart.ShippingLocation.BusinessName;
        object shiptofirstname = CompletedCart.ShippingLocation.FullName;
        object shiptolastname = DBNull.Value;
        object shiptoaddress1 = CompletedCart.ShippingLocation.Address1;
        object shiptoaddress2 = CompletedCart.ShippingLocation.Address2;
        object shiptocity = CompletedCart.ShippingLocation.City;
        object shiptostatecode = CompletedCart.ShippingLocation.StateCode;
        object shiptoprovince = DBNull.Value;
        object shiptozip = CompletedCart.ShippingLocation.PostalCode;
        object shiptocountrycode = CompletedCart.ShippingLocation.CountryCode;
        object shiptoareacode = DBNull.Value;
        object shiptophone = CompletedCart.ShippingLocation.Phone;
        object ordcomments = comments;
        object referenceid = 0;
        object forteordnum = DBNull.Value;
        object billtobldgcode = DBNull.Value;
        object billtocustnum = DBNull.Value;
        object shiptobldgcode = DBNull.Value;
        object shiptocustnum = DBNull.Value;
        object createdate = DateTime.Now;
        object modifieddate = DateTime.Now;
        object deleteflag = 0;

        payment_id = -1;

        orderR = Order_Add(orderdate, completedate, siteid, orderstatusid, studentverifyflag, studentverifystatusid, salesrephelpedflag, paymentid, 

shipmethodid, subtotal, shippingamt, shippingweight, handlingamt, taxamt, ordertotal, loginid, firstname, lastname, email, title, audienceid, 

areacode, phone, phoneext, fax, billtologinaddressid, billtobldgname, billtoaddress1, billtoaddress2, billtocity, billtostatecode, 

billtoprovince, billtozip, billtocountrycode, shiptologinaddressid, shiptobldgname, shiptofirstname, shiptolastname, shiptoaddress1, 

shiptoaddress2, shiptocity, shiptostatecode, shiptoprovince, shiptozip, shiptocountrycode, shiptoareacode, shiptophone, ordcomments, 

referenceid, forteordnum, billtobldgcode, billtocustnum, shiptobldgcode, shiptocustnum, createdate, modifieddate, deleteflag, out order_id);

        orderiInformation(login_id);
        if (Convert.ToInt32(Session["orderToaddI"]) != -1)
        {
            int item_id;
            int iten_Line = 1;
            foreach (CartItem item in CompletedCart.Items)
            {
                Order_Add_Item(Convert.ToInt32(Session["orderToaddI"]), iten_Line, 1, DBNull.Value, CompletedCart.ShippingOption, item.ProductTitleId, 

item.ProductSKU, DBNull.Value, item.Quantity, item.UnitPrice, item.SubTotal, DateTime.Now, 0, DateTime.Now, 0, 0, DBNull.Value, out item_id,login_id);
                iten_Line++;
            }
            

            Order_Add_Payment(CompletedCart.Payment.PaymentType, 0, CompletedCart.Payment.EncryptedCC, 

CompletedCart.Payment.EncryptedCCExpiration, CompletedCart.Payment.EncryptedCCCVV, "", CompletedCart.Payment.POFileName, 

CompletedCart.Payment.POFileUpload, CompletedCart.Payment.POFileUpload != null && CompletedCart.Payment.POFileUpload.Length > 0 ? 1 : 0, 0, 

DateTime.Now, 0, DateTime.Now, out payment_id,CompletedCart.Payment.CCType);

            Order_Update_PaymentId(paymentID, Convert.ToInt32(Session["orderToaddI"]));
            
            return true;
        }
        

        return false;
    }

    public int Order_Add(object orderdate, object completedate, object siteid, object orderstatusid, object studentverifyflag, object 

studentverifystatusid, object salesrephelpedflag, object paymentid, object shipmethodid, object subtotal, object shippingamt, object 

shippingweight, object handlingamt, object taxamt, object ordertotal, object loginid, object firstname, object lastname, object email, object 

title, object audienceid, object areacode, object phone, object phoneext, object fax, object billtologinaddressid, object billtobldgname, 

object billtoaddress1, object billtoaddress2, object billtocity, object billtostatecode, object billtoprovince, object billtozip, object 

billtocountrycode, object shiptologinaddressid, object shiptobldgname, object shiptofirstname, object shiptolastname, object shiptoaddress1, 

object shiptoaddress2, object shiptocity, object shiptostatecode, object shiptoprovince, object shiptozip, object shiptocountrycode, object 

shiptoareacode, object shiptophone, object ordcomments, object referenceid, object forteordnum, object billtobldgcode, object billtocustnum, 

object shiptobldgcode, object shiptocustnum, object createdate, object modifieddate, object deleteflag, out int order_id)
    {
        #region DB Schema
        /*
            @OrderDate smalldatetime
           ,@CompleteDate smalldatetime
           ,@SiteID int
           ,@OrderStatusID int
           ,@StudentVerifyFlag int
           ,@StudentVerifyStatusID int
           ,@SalesRepHelpedFlag int
           ,@PaymentID int
           ,@ShipMethodID int
           ,@SubTotal decimal(18,2)
           ,@ShippingAmt decimal(18,2)
           ,@ShippingWeight decimal(18,2)
           ,@HandlingAmt decimal(18,2)
           ,@TaxAmt decimal(18,2)
           ,@OrderTotal decimal(18,2)
           ,@LoginID int
           ,@FirstName varchar(48)
           ,@LastName varchar(50)
           ,@Email varchar(100)
           ,@Title varchar(50)
           ,@AudienceID int
           ,@AreaCode varchar(3)
           ,@Phone varchar(50)
           ,@PhoneExt varchar(10)
           ,@Fax varchar(50)
           ,@BillToLoginAddressID int
           ,@BillToBldgName varchar(100)
           ,@BillToAddress1 varchar(100)
           ,@BillToAddress2 varchar(100)
           ,@BillToCity varchar(100)
           ,@BillToStateCode varchar(2)
           ,@BillToProvince varchar(50)
           ,@BillToZip varchar(10)
           ,@BillToCountryCode varchar(2)
           ,@ShipToLoginAddressID int
           ,@ShipToBldgName varchar(100)
           ,@ShipToFirstName varchar(50)
           ,@ShipToLastName varchar(50)
           ,@ShipToAddress1 varchar(100)
           ,@ShipToAddress2 varchar(100)
           ,@ShipToCity varchar(100)
           ,@ShipToStateCode varchar(2)
           ,@ShipToProvince varchar(50)
           ,@ShipToZip varchar(10)
           ,@ShipToCountryCode varchar(2)
           ,@ShipToAreaCode varchar(3)
           ,@ShipToPhone varchar(50)
           ,@OrdComments varchar(2000)
           ,@ReferenceID int
           ,@ForteOrdNum varchar(20)
           ,@BillToBldgCode varchar(20)
           ,@BillToCustNum varchar(50)
           ,@ShipToBldgCode varchar(20)
           ,@ShipToCustNum varchar(20)
           ,@CreateDate smalldatetime
           ,@ModifiedDate smalldatetime
           ,@DeleteFlag int
        */
        #endregion
	Session["orderIdAdd"] = 0;
        order_id = -1;
        int result = 0;

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Order_Add";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter returnValue = new SqlParameter("@OrderID", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

             cmd.Parameters.Add(returnValue);
             cmd.Parameters.Add("@OrderDate", SqlDbType.SmallDateTime);
             cmd.Parameters.Add("@CompleteDate", SqlDbType.SmallDateTime);
             cmd.Parameters.Add("@SiteID", SqlDbType.Int);
             cmd.Parameters.Add("@OrderStatusID", SqlDbType.Int);
             cmd.Parameters.Add("@StudentVerifyFlag", SqlDbType.Int);
             cmd.Parameters.Add("@StudentVerifyStatusID", SqlDbType.Int);
             cmd.Parameters.Add("@SalesRepHelpedFlag", SqlDbType.Int);
             cmd.Parameters.Add("@PaymentID", SqlDbType.Int);
             cmd.Parameters.Add("@ShipMethodID", SqlDbType.Int);//10
             cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal);
             cmd.Parameters.Add("@ShippingAmt", SqlDbType.Decimal);
             cmd.Parameters.Add("@ShippingWeight", SqlDbType.Decimal);
             cmd.Parameters.Add("@HandlingAmt", SqlDbType.Decimal);
             cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal);
             cmd.Parameters.Add("@OrderTotal", SqlDbType.Decimal);
             cmd.Parameters.Add("@LoginID", SqlDbType.Int);
             cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 48);
             cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100);//20
             cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@AudienceID", SqlDbType.Int);
             cmd.Parameters.Add("@AreaCode", SqlDbType.VarChar, 3);
             cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@PhoneExt", SqlDbType.VarChar, 10);
             cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@BillToLoginAddressID", SqlDbType.Int);
             cmd.Parameters.Add("@BillToBldgName", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@BillToAddress1", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@BillToAddress2", SqlDbType.VarChar, 100);//30
             cmd.Parameters.Add("@BillToCity", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@BillToStateCode", SqlDbType.VarChar, 2);
             cmd.Parameters.Add("@BillToProvince", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@BillToZip", SqlDbType.VarChar, 10);
             cmd.Parameters.Add("@BillToCountryCode", SqlDbType.VarChar, 2);
             cmd.Parameters.Add("@ShipToLoginAddressID", SqlDbType.Int);
             cmd.Parameters.Add("@ShipToBldgName", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@ShipToFirstName", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@ShipToLastName", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@ShipToAddress1", SqlDbType.VarChar, 100);//40
             cmd.Parameters.Add("@ShipToAddress2", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@ShipToCity", SqlDbType.VarChar, 100);
             cmd.Parameters.Add("@ShipToStateCode", SqlDbType.VarChar, 2);
             cmd.Parameters.Add("@ShipToProvince", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@ShipToZip", SqlDbType.VarChar, 10);
             cmd.Parameters.Add("@ShipToCountryCode", SqlDbType.VarChar, 2);
             cmd.Parameters.Add("@ShipToAreaCode", SqlDbType.VarChar, 3);
             cmd.Parameters.Add("@ShipToPhone", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@OrdComments", SqlDbType.VarChar, 2000);
             cmd.Parameters.Add("@ReferenceID", SqlDbType.Int);//50
           //  cmd.Parameters.Add("@ForteOrdNum", SqlDbType.VarChar, 20, forteordnum);
             // cmd.Parameters.Add("@CreateDate", SqlDbType.SmallDateTime, createdate);
             // cmd.Parameters.Add("@ModifiedDate", SqlDbType.SmallDateTime, modifieddate);
             //  cmd.Parameters.Add("@DeleteFlag", SqlDbType.Int, deleteflag);
             cmd.Parameters.Add("@BillToBldgCode", SqlDbType.VarChar, 20);
             cmd.Parameters.Add("@BillToCustNum", SqlDbType.VarChar, 50);
             cmd.Parameters.Add("@ShipToBldgCode", SqlDbType.VarChar, 20);
             cmd.Parameters.Add("@ShipToCustNum", SqlDbType.VarChar, 20);//54
 
            cmd.Parameters[0].Direction = ParameterDirection.Output;
            cmd.Parameters[1].Value =  orderdate;
            cmd.Parameters[2].Value = completedate;
            cmd.Parameters[3].Value = siteid;
            cmd.Parameters[4].Value = orderstatusid;
            cmd.Parameters[5].Value = studentverifyflag;
            cmd.Parameters[6].Value = studentverifystatusid;
            cmd.Parameters[7].Value = salesrephelpedflag;
            cmd.Parameters[8].Value = paymentid;
            cmd.Parameters[9].Value = shipmethodid;
            cmd.Parameters[10].Value = subtotal;
            cmd.Parameters[11].Value = shippingamt;
            cmd.Parameters[12].Value = shippingweight;
            cmd.Parameters[13].Value = handlingamt;
            cmd.Parameters[14].Value = taxamt;
            cmd.Parameters[15].Value = ordertotal;
            cmd.Parameters[16].Value = loginid;
            cmd.Parameters[17].Value = firstname;
            cmd.Parameters[18].Value = lastname;
            cmd.Parameters[19].Value = email;
            cmd.Parameters[20].Value = title;
            cmd.Parameters[21].Value = audienceid;
            cmd.Parameters[22].Value = areacode;
            cmd.Parameters[23].Value = phone;
            cmd.Parameters[24].Value = phoneext;
            cmd.Parameters[25].Value = fax;
            cmd.Parameters[26].Value = billtologinaddressid;
            cmd.Parameters[27].Value = billtobldgname;
            cmd.Parameters[28].Value = billtoaddress1;
            cmd.Parameters[29].Value = billtoaddress2;
            cmd.Parameters[30].Value = billtocity;
            cmd.Parameters[31].Value = billtostatecode;
            cmd.Parameters[32].Value = billtoprovince;
            cmd.Parameters[33].Value = billtozip;
            cmd.Parameters[34].Value = billtocountrycode;
            cmd.Parameters[35].Value = shiptologinaddressid;
            cmd.Parameters[36].Value = shiptobldgname;
            cmd.Parameters[37].Value = shiptofirstname;
            cmd.Parameters[38].Value = shiptolastname;
            cmd.Parameters[39].Value = shiptoaddress1;
            cmd.Parameters[40].Value = shiptoaddress2;
            cmd.Parameters[41].Value = shiptocity;
            cmd.Parameters[42].Value = shiptostatecode;
            cmd.Parameters[43].Value = shiptoprovince;
            cmd.Parameters[44].Value = shiptozip;
            cmd.Parameters[45].Value = shiptocountrycode;
            cmd.Parameters[46].Value = shiptoareacode;
            cmd.Parameters[47].Value = shiptophone;
            cmd.Parameters[48].Value = ordcomments;
            cmd.Parameters[49].Value = referenceid;
            cmd.Parameters[50].Value = billtobldgcode;
            cmd.Parameters[51].Value = billtocustnum;
            cmd.Parameters[52].Value = shiptobldgcode;
            cmd.Parameters[53].Value = shiptocustnum;
            cmd.ExecuteNonQuery();
            
            int QuoteObtain = Convert.ToInt32(cmd.Parameters["@OrderID"].Value.ToString());
            Session["orderIdAdd"] = QuoteObtain;
            orderidAdditem = QuoteObtain;
            if (QuoteObtain != -1)
            {
                result = QuoteObtain;
            }
           
            return result;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return -1;
        }
        catch (Exception ex)
        {
            //throw ex;
            return -1;
        }
        finally
        {
            Close();
        }
    }

    public bool Order_Add_Item(object orderid, object linenum, object linestatusid, object shipdate, object shipmethodid, object titleid, 

object sku, object skudesc, object qty, object unitprice, object totalprice, object createdate, object createdbyid, object modifieddate, 

object modifiedbyid, object deleteflag, object forteinvnum, out int item_id, int userid)
    {
        #region DB Schema
        /*
        @OrderDetailID int
       ,@OrderID int
       ,@LineNum int
       ,@LineStatusID int
       ,@ShipDate smalldatetime
       ,@ShipMethodID int
       ,@TitleID int
       ,@SKU varchar(50)
       ,@SKUDesc varchar(50)
       ,@QTY int
       ,@UnitPrice decimal(18,2)
       ,@TotalPrice decimal(18,2)
       ,@CreateDate smalldatetime
       ,@CreatedByID int
       ,@ModifiedDate smalldatetime
       ,@ModifiedByID int
       ,@DeleteFlag int
       ,@ForteInvNum varchar(50)
        */
        #endregion


        Session["orderIdForTorch"] = orderid;
        item_id = -1;
        DataSet data = new DataSet();
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Order_Add_Item";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@OrderID", SqlDbType.Int, orderid);
            CartDB.AddSetParameter(ref cmd, "@LineNum", SqlDbType.Int, linenum);
            CartDB.AddSetParameter(ref cmd, "@LineStatusID", SqlDbType.Int, linestatusid);
            CartDB.AddSetParameter(ref cmd, "@ShipDate", SqlDbType.SmallDateTime,System.Data.SqlTypes.SqlDateTime.Null);
            CartDB.AddSetParameter(ref cmd, "@ShipMethodID", SqlDbType.Int, shipmethodid);
            CartDB.AddSetParameter(ref cmd, "@TitleID", SqlDbType.Int, titleid);
            CartDB.AddSetParameter(ref cmd, "@SKU", SqlDbType.VarChar, 50, sku);
            if (Convert.ToString(skudesc) != "")
            {
                CartDB.AddSetParameter(ref cmd, "@SKUDesc", SqlDbType.VarChar, 50, skudesc);
            }
            else
            { 
                
                CartDB.AddSetParameter(ref cmd, "@SKUDesc", SqlDbType.VarChar, 50,System.Data.SqlTypes.SqlString.Null);
            }
            CartDB.AddSetParameter(ref cmd, "@QTY", SqlDbType.Int, qty);
            CartDB.AddSetParameter(ref cmd, "@UnitPrice", SqlDbType.Decimal, unitprice);
            CartDB.AddSetParameter(ref cmd, "@TotalPrice", SqlDbType.Decimal, totalprice);
            CartDB.AddSetParameter(ref cmd, "@CreateDate", SqlDbType.SmallDateTime, createdate);
            CartDB.AddSetParameter(ref cmd, "@CreatedByID", SqlDbType.Int, createdbyid);
            CartDB.AddSetParameter(ref cmd, "@ModifiedDate", SqlDbType.SmallDateTime, modifieddate);
            CartDB.AddSetParameter(ref cmd, "@ModifiedByID", SqlDbType.Int, modifiedbyid);
            CartDB.AddSetParameter(ref cmd, "@LoginId", SqlDbType.Int, userid);


            cmd.ExecuteNonQuery();

           // item_id = (int)cmd.Parameters["@Return_Value"].Value;
	        lineNumber++;

            return true; //item_id != -1;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool Order_Add_Payment(object paymentmethodid, object paymentstatusid, object encryptedcc, object encryptedccexpiration, object 

encryptedcccvv, object ponum, object pofilename, byte[] pofileupload, object pouploadedflag, object createdbyid, object createdate, object 

modifiedbyid, object modifieddate, out int payment_id, string cctypeid)
    {
        #region DB Schema
        /*
        @PaymentID int
       ,@PaymentMethodID int
       ,@PaymentStatusID int
       ,@EncryptedCC nvarchar(512)
       ,@EncryptedCCExpiration nvarchar(512)
       ,@EncryptedCCCVV nvarchar(512)
       ,@PONum varchar(100)
       ,@POFileUpload text
       ,@POUploadedFlag int
       ,@CreatedByID int
       ,@CreateDate smalldatetime
       ,@ModifiedByID int
       ,@ModifiedDate smalldatetime
        */
        #endregion

        payment_id = -1;
	int intCCtypeid = 0;
        switch (cctypeid)
        {
            case "VISA":
                intCCtypeid = 1;
                break;
            case "MC":
                intCCtypeid = 2;
                break;
            case "AMEX":
                intCCtypeid = 3;
                break;
            case "DISC":
                intCCtypeid = 4;
                break;
            default:
                intCCtypeid = 0;
                break;
        }

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Order_Add_Payment";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CCTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@PaymentMethodID", SqlDbType.Int);
            cmd.Parameters.Add("@EncryptedCC", SqlDbType.NVarChar, 512);
            cmd.Parameters.Add("@EncryptedCCExpiration", SqlDbType.NVarChar, 512);
            cmd.Parameters.Add("@EncryptedCCCVV", SqlDbType.NVarChar, 512);
            cmd.Parameters.Add("@PONum", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@POFileName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@POFileUpload", SqlDbType.Image);
            cmd.Parameters.Add("@POUploadedFlag", SqlDbType.Int);
            cmd.Parameters[0].Value = intCCtypeid;
            cmd.Parameters[1].Value = paymentmethodid;
            cmd.Parameters[2].Value = encryptedcc;
            cmd.Parameters[3].Value = encryptedccexpiration;
            cmd.Parameters[4].Value = encryptedcccvv;
            cmd.Parameters[5].Value = ponum;
            cmd.Parameters[6].Value = pofilename;
            if (pofileupload == null)
            {
                cmd.Parameters[7].Value = DBNull.Value;
            }
            else
            {
                SqlBinary value1 = new SqlBinary(pofileupload);
                cmd.Parameters[7].Value = value1;
            }
            cmd.Parameters[8].Value = pouploadedflag;

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                { payment_id = Convert.ToInt32(dr["id"].ToString()); }
            }
            dr.Close();
            dr.Dispose();

            paymentID = payment_id;

            return payment_id != -1;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }
    public void Order_Update_PaymentId(int paymentid,int orderid)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "Cart_Order_Upd_PaymentId";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@PaymentId", SqlDbType.Int);
            cmd.Parameters.Add("@OrderId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = paymentid;
            cmd.Parameters[1].Value = orderid;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Close();
            dr.Dispose();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
        }
    }


    public bool Order_Update_POPayment(int payment_id, string po_number, string po_filename, byte[] po_file_upload, int po_uploaded,string potype)
    {
        #region DB Schema
        /*
        [dbo].[Cart_Order_Upd_POPayment] 
               @PaymentID int
			   ,@PONum varchar(100)
			   ,@POFileName nvarchar(512)
               ,@POFileUpload varbinary(max)
               ,@POUploadedFlag int
        */
        #endregion

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Order_Upd_POPayment";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@PaymentID", SqlDbType.Int, payment_id);
            CartDB.AddSetParameter(ref cmd, "@PONum", SqlDbType.VarChar, 100, po_number);
            CartDB.AddSetParameter(ref cmd, "@POFileName", SqlDbType.NVarChar, 512, po_filename);
            CartDB.AddSetParameter(ref cmd, "@POUploadedFlag", SqlDbType.Int, po_uploaded);
            CartDB.AddSetParameter(ref cmd, "@POFileType", SqlDbType.VarChar,50, potype);

            if (po_file_upload == null)
            {
                CartDB.AddSetParameter(ref cmd, "@POFileUpload", SqlDbType.Image, DBNull.Value);
            }
            else
            {
                SqlParameter bin = new SqlParameter("@POFileUpload", SqlDbType.Image, po_file_upload.Length);
                cmd.Parameters.Add(bin);

                SqlBinary value = new SqlBinary(po_file_upload);
                cmd.Parameters["@POFileUpload"].SqlValue = value;
            }

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            int result = (int)cmd.Parameters["@Return_Value"].Value;

            return result != -1;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }
    #endregion

    #region Login Addresses
    public bool LoginAddAddress(int login_id, string address_name, string building_name, string address1, string address2, string city, 

string state_code, string province, string zip, string country_code,string phone, out int login_address_id)
    {
        #region DB Schema
        /*            @LoginID int
           ,@AddressName varchar(100)
           ,@BldgName varchar(100)
           ,@Address1 varchar(100)
           ,@Address2 varchar(100)
           ,@City varchar(100)
           ,@StateCode varchar(2)
           ,@Province varchar(50)
           ,@Zip varchar(10)
           ,@CountryCode varchar(2)*/
        #endregion
        
        login_address_id = -1;

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Add_Address";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);
            CartDB.AddSetParameter(ref cmd, "@AddressName", SqlDbType.VarChar, 100, address_name);
            CartDB.AddSetParameter(ref cmd, "@BldgName", SqlDbType.VarChar, 100, building_name);
            CartDB.AddSetParameter(ref cmd, "@Address1", SqlDbType.VarChar, 100, address1);
            CartDB.AddSetParameter(ref cmd, "@Address2", SqlDbType.VarChar, 100, address2);
            CartDB.AddSetParameter(ref cmd, "@City", SqlDbType.VarChar, 100, city);
            CartDB.AddSetParameter(ref cmd, "@StateCode", SqlDbType.VarChar, 2, state_code);
            CartDB.AddSetParameter(ref cmd, "@Province", SqlDbType.VarChar, 50, province);
            CartDB.AddSetParameter(ref cmd, "@Zip", SqlDbType.VarChar, 10, zip);
            CartDB.AddSetParameter(ref cmd, "@CountryCode", SqlDbType.VarChar, 2, country_code);
            CartDB.AddSetParameter(ref cmd, "@Phone", SqlDbType.VarChar, 20, phone);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            int result = (int)cmd.Parameters["@Return_Value"].Value;
            login_address_id = result;
            return result != -1;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool LoginUpdAddress(int login_address_id, int login_id, string address_name, string building_name, string address1, string 

address2, string city, string state_code, string province, string zip, string country_code,string phone, int delete_flag)
    {
        #region DB Schema
        /*  @LoginID int
           ,@AddressName varchar(100)
           ,@BldgName varchar(100)
           ,@Address1 varchar(100)
           ,@Address2 varchar(100)
           ,@City varchar(100)
           ,@StateCode varchar(2)
           ,@Province varchar(50)
           ,@Zip varchar(10)
           ,@CountryCode varchar(2)
           ,@DeleteFlag int*/
        #endregion

        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Upd_Address";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginAddressID", SqlDbType.Int, login_address_id);
            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);
            CartDB.AddSetParameter(ref cmd, "@AddressName", SqlDbType.VarChar, 100, address_name);
            CartDB.AddSetParameter(ref cmd, "@BldgName", SqlDbType.VarChar, 100, building_name);
            CartDB.AddSetParameter(ref cmd, "@Address1", SqlDbType.VarChar, 100, address1);
            CartDB.AddSetParameter(ref cmd, "@Address2", SqlDbType.VarChar, 100, address2);
            CartDB.AddSetParameter(ref cmd, "@City", SqlDbType.VarChar, 100, city);
            CartDB.AddSetParameter(ref cmd, "@StateCode", SqlDbType.VarChar, 2, state_code);
            CartDB.AddSetParameter(ref cmd, "@Province", SqlDbType.VarChar, 50, province);
            CartDB.AddSetParameter(ref cmd, "@Zip", SqlDbType.VarChar, 10, zip);
            CartDB.AddSetParameter(ref cmd, "@CountryCode", SqlDbType.VarChar, 2, country_code);
            CartDB.AddSetParameter(ref cmd, "@DeleteFlag", SqlDbType.Int, delete_flag);
            CartDB.AddSetParameter(ref cmd, "@Phone", SqlDbType.VarChar, 20, phone);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            int result = (int)cmd.Parameters["@Return_Value"].Value;

            return result != -1;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public bool LoginDeleteAddress(int login_address_id)
    {
        if (1 == 1)//try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Delete_Address";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginAddressID", SqlDbType.Int, login_address_id);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            object v = cmd.Parameters["@Return_Value"].Value;

            return true;
        }
        /*
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {*/
            Close();
        //}
    }

    public bool LoginDeleteAddresses(int login_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Delete_Addresses";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public DataSet LoginGetAddresses(int login_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Get_Addresses";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public DataSet LoginGetAddress(int login_address_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Get_Address";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginAddressID", SqlDbType.Int, login_address_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }


    public DataSet LoginGetBillingAddress(int login_id)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Get_BillingAddress";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);

            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset, Constants.DataSetKeys.CART_TABLE);
            cmd.Dispose();

            return dataset;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return null;
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
        finally
        {
            Close();
        }
    }

    public bool LoginUpdBillingAddress(int login_id, string building_name, string address1, string address2, string city, string state_code, 

string zip, string country_code, string phone)
    {
        try
        {
            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.DataBase;

            cmd.CommandText = "Cart_Login_Upd_BillingAddress";
            cmd.CommandType = CommandType.StoredProcedure;

            CartDB.AddSetParameter(ref cmd, "@LoginID", SqlDbType.Int, login_id);
            CartDB.AddSetParameter(ref cmd, "@BldgName", SqlDbType.VarChar, 50, building_name);
            CartDB.AddSetParameter(ref cmd, "@Address1", SqlDbType.VarChar, 100, address1);
            CartDB.AddSetParameter(ref cmd, "@Address2", SqlDbType.VarChar, 100, address2);
            CartDB.AddSetParameter(ref cmd, "@City", SqlDbType.VarChar, 50, city);
            CartDB.AddSetParameter(ref cmd, "@State", SqlDbType.VarChar, 50, state_code);
            CartDB.AddSetParameter(ref cmd, "@Zip", SqlDbType.VarChar, 50, zip);
            CartDB.AddSetParameter(ref cmd, "@Country", SqlDbType.VarChar, 50, country_code);
            CartDB.AddSetParameter(ref cmd, "@Phone", SqlDbType.VarChar, 50, phone);

            SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (SqlException sqlex)
        {
            //throw sqlex;
            return false;
        }
        catch (Exception ex)
        {
            //throw ex;
            return false;
        }
        finally
        {
            Close();
        }
    }

    public DataSet Get_OrderId_By_Email(int loginid)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_OrderId_By_UserId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            //Setting values to Parameters.
            cmd.Parameters[0].Value = loginid;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return dataset;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return null;
        }
    }
    //-----------------------------------------------------------
    public DataSet Get_SkuDesc_By_SKU(string sku)
    {
        try
        {
            Open();
            DataSet dataset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            cmd.CommandText = "Get_SkuDesc_By_SKU";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SKU", SqlDbType.VarChar, 250, "SKU");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = sku;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataset);
            cmd.Dispose();
            Close();
            return dataset;
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
            return null;
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
            return null;
        }
    }

    protected void orderiInformation(int id)
    {
        int orderIdEmail = 0;
        DataSet data2 = new DataSet();
        data2 = Get_OrderId_By_Email(id); //de.Get_OrderId_By_Email(CurrentCart.BillingLocation.Email);
        foreach (DataTable table2 in data2.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                orderIdEmail = Convert.ToInt32(row2["OrderID"]);
            }
        }


        Session["orderToaddI"] = orderIdEmail;
    }

    #endregion

}
