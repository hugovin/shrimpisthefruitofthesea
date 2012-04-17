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

using System.IO;

public partial class CheckoutStep_PO : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_PO() { }
    public CheckoutStep_PO(Cart c) { this.CurrentCart = c; }
    public int OrId = 0;
    
    string valid_extensions = "jpg,jpeg,tiff,doc,docx,pdf";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");
        OrId = Convert.ToInt32(Session["orderpoid"]);
        string po_email = Helper.IsString(ConfigurationManager.AppSettings["PO_EMAIL"], "");
        string po_email_var = String.Format("var po_email = '{0}';", po_email);

        Page.RegisterStartupScript("RegisterControl", String.Format("<script type='text/javascript'>{1}{0}</script>", Helper.GenerateScriptToAccessControls(Page.Controls), po_email_var));
    }

    protected void MoveToThankYou()
    {
        CurrentCart.MoveToStep((int)Constants.CheckoutStep.ThankYou);
        Response.Redirect(Constants.Pages.CHECKOUT);
    }

    protected bool UpdateOrderPOPayment(string po_number, string po_filename, byte[] po_file_upload,string potype)
    {
        if(CurrentCart.Payment.ORD_PAYMENT_ID != 0)
        {
            CartDB db = new CartDB();
            return db.Order_Update_POPayment(CurrentCart.Payment.ORD_PAYMENT_ID, po_number, po_filename, po_file_upload, po_file_upload != null && po_file_upload.Length > 0 ? 1 : 0, potype);
        }
        return false;
    }

    protected void UploadPOButton_Clicked(object sender, EventArgs e)
    {
        string filename = "";
        string ponumber = PONumberTextbox.Text;
        string potype = "";
        

        byte[] content = DoUpload(out filename);

        if(filename.Contains(".jpg"))
        {
            potype = "jpg";
        }

                if(filename.Contains(".tiff"))
        {
            potype = "tiff";
        }
                if(filename.Contains(".pdf"))
        {
            potype = "pdf";
        }
                if(filename.Contains(".doc"))
        {
            potype = "doc";
        }
               if(filename.Contains(".docx"))
        {
            potype = "docx";
        }
        
        if (content != null)
        {
            CartDB db = new CartDB();
            if (db.CartUpdatePayment(CurrentCart.PaymentId, CurrentCart.CartId, PaymentType.PO, "", "", "", "", "", ponumber, filename, content))
            {
                //Response.Write(String.Format("SUCCESS UPLOADING FILE {0} OF {1} BYTES LENGTH", filename, content.Length));
                UpdateOrderPOPayment(ponumber, filename, content,potype);
                MoveToThankYou();
            }
        }
    }

    protected void EmailPOButton_Clicked(object sender, EventArgs e)
    {
        string ponumber = PONumberTextbox.Text;
        CartDB db = new CartDB();
        if (db.CartUpdatePayment(CurrentCart.PaymentId, CurrentCart.CartId, PaymentType.PO, "", "", "", "", "", ponumber, "[EMAIL]", null))
        {
            UpdateOrderPOPayment(ponumber, "[EMAIL]", null,"");
            MoveToThankYou();
        }
    }

    protected void PrintPOButton_Clicked(object sender, EventArgs e)
    {
        string ponumber = PONumberTextbox.Text;
        CartDB db = new CartDB();
        if (db.CartUpdatePayment(CurrentCart.PaymentId, CurrentCart.CartId, PaymentType.PO, "", "", "", "", "", ponumber, "[FAX]", null))
        {
            UpdateOrderPOPayment(ponumber, "[FAX]", null,"");
            MoveToThankYou();
        }
    }

    private byte[] DoUpload(out string filename)
    {
        int maxFileSize = 5242880;

        string sFileName = "", path = "";
        if ((POFileUpload.PostedFile != null) && (POFileUpload.PostedFile.ContentLength > 0))
        {
            sFileName = Path.GetFileName(POFileUpload.PostedFile.FileName);
            filename = POFileUpload.PostedFile.FileName;
            try
            {
                if (POFileUpload.PostedFile.ContentLength <= maxFileSize) //i.e., 5MB
                {
                    string ext = sFileName.Substring(sFileName.LastIndexOf('.') + 1, sFileName.Length - sFileName.LastIndexOf('.') - 1).ToLower();

                    if (CheckExtension(ext))
                    {
                        try
                        {
                            int sizeInBytes = POFileUpload.PostedFile.ContentLength;
                            try
                            {
                                BinaryReader reader = new BinaryReader(POFileUpload.PostedFile.InputStream);
                                byte[] content = reader.ReadBytes(sizeInBytes);

                                if (content.Length == sizeInBytes)
                                {
                                    return content;
                                }
                            }
                            catch (Exception e)
                            {
                                throw new Exception(String.Format("{0}", e.Message));
                            }
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            DisplayMessage("We are sorry, but permissions are not set to upload to this directory. (1) " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("We are sorry, but there was an error uploading your file.  (" + path + ") (1) " + ex.Message);
                        }
                    }
                    else
                    {
                        DisplayMessage("We are sorry, but the filetype you are uploading is restricted. (2)");
                    }
                }
                else
                {
                    DisplayMessage("We are sorry, but your file you are uploading is too large. Please limit your file size to 5MB in size. (3)");
                }
            }
            catch
            {
                DisplayMessage("We are sorry, but there was an error uploading your file. (4)");
            }
        }
        else
        {
            DisplayMessage("We are sorry, but there you must select a file to upload (5)");
        }

        filename = "";
        return null;
    }

    private void DisplayMessage(string message)
    {
        MessageLiteral.Text = message;
    }

    private bool CheckExtension(string ext)
    {
        if (valid_extensions == "all")
        {
            return true;
        }
        else
        {
            ArrayList validexts = new ArrayList();
            string[] exts = valid_extensions.Split(',');
            foreach (string sext in exts)
            {
                validexts.Add(sext);
            }

            return validexts.Contains(ext.ToLower());
        }
    }




}
