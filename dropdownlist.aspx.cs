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
using System.Data.SqlClient;


public partial class dropdownlist : System.Web.UI.Page
{
    protected static string connectionString = ConfigurationManager.AppSettings["conStringSQL"];
    protected SqlConnection DataBase;
    protected void Page_Load(object sender, EventArgs e){
        string SiteId = "1";
        string ContId = "1";
        if (Session["SiteId"] != null) {
            SiteId = Session["SiteId"].ToString();
        }
        if(Session["ContId"] != null){
            ContId=Session["ContId"].ToString();
        }
        itemInformation.Text = "";
        if(Request["word"] != null){
            //mean that if has a word for the request.
            if(Request["word"]!=""){
                //All good to go.
                string findWord = Request["word"];
                
                //Page vars
                int PageSize = 5;
                int PageNum = 1;
                int FirstRow = PageNum * PageSize - PageSize;
                int CurrentRow = 0;
                //
                Result resultTitle = new Result();
                DataSet dsfinder = new DataSet();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //---
                dsfinder.Clear();
                //dsfinder = resultTitle.Find_TitlebyDesc(findWord,null,null,null,null,null);


                // <New embed sql contrl>
                try
                {
                    DataBase = new SqlConnection(connectionString);
                    DataBase.Open();
                    DataSet datasetRes = new DataSet();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DataBase;
                    cmd.CommandText = "Find_TitlebyDesc";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SiteId", SqlDbType.Int);
                    cmd.Parameters.Add("@ContId ", SqlDbType.Int);
                    cmd.Parameters.Add("@TextFinder", SqlDbType.VarChar, 250);
                    //Setting values to Parameters.
                    cmd.Parameters[0].Value = SiteId;
                    cmd.Parameters[1].Value = ContId;
                    cmd.Parameters[2].Value = findWord;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    datasetRes.Clear();
                    adapter.Fill(datasetRes);
                    cmd.Dispose();
                    DataBase.Close();
                    dsfinder =  datasetRes;
                }
                catch (SqlException oSqlExp)
                {
                    //Console.WriteLine("" + oSqlExp.Message);
                    dsfinder =  null;
                }
                catch (Exception oEx)
                {
                    //Console.WriteLine("" + oEx.Message);
                    dsfinder =  null;
                }
                // </New embed sql contrl>



                itemInformation.Text = "";
                itemInformation.Text += "<div class=\"boxSearchInt\">";
                itemInformation.Text += "    <div class=\"searchTopInt\">";
                itemInformation.Text += "        <div class=\"searchTopIntTitle\"><p>Products</p></div>";
                itemInformation.Text += "    </div>";
                foreach (DataTable table in dsfinder.Tables)
                {
                    int cant = table.Rows.Count;
                    if (cant > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            string imageThumbnail = row["imagetn"].ToString();
                            string productId = row["titleid"].ToString();
                            string productTitle = row["title"].ToString();
                            string originalTitle = productTitle;
                            //string productDescription = row["titledesc"].ToString();
                            string pubName = row["pubname"].ToString();
                            string pubId = row["pubid"].ToString();
                            string sku = row["defaultSKU"].ToString();
                            if (productTitle.Length > 66)
                            {
                                productTitle = productTitle.Substring(0, 63) + "...";
                            }
                            /*if (productDescription.Length > 72)
                            {
                                productDescription = productDescription.Substring(0, 69) + "...";
                            }*/
                            itemInformation.Text += "    <div class=\"searchMainInt\">";
                            itemInformation.Text += "        <div class=\"searchProductInt\">";
                            itemInformation.Text += "            <div class=\"searchImageDescrip\"><a href=\"product.aspx?p=" + productId + "\" onClick=\"document.getElementById('topSearch').value = '" + originalTitle + "'\"><img src=\"" + SiteConstants.imagesPathTb + "tn_" + imageThumbnail + "\" width=\"52\" height=\"65\"/></a></div>";
                            itemInformation.Text += "            <div class=\"searchInfoDescrip\">";
                            itemInformation.Text += "                <h2><a href=\"product.aspx?p=" + productId + "\" onClick=\"document.getElementById('topSearch').value = '" + originalTitle + "'\">" + productTitle + "</a></h2>";
                            itemInformation.Text += "                <p><em>by: </em><a href=\"PublisherList.aspx?idP=" + pubId + "\">" + pubName + "</a></p>";
                            itemInformation.Text += "                <p>Item# " + sku + "</p>";
                            itemInformation.Text += "           </div>";
                            itemInformation.Text += "        </div>";
                            itemInformation.Text += "        <div class=\"clear\"></div>";
                            itemInformation.Text += "    </div>";

                            /*itemInformation.Text += "<div class=\"oneItem\" style=\"margin: 7px 0px 7px 0px;\">";
                            itemInformation.Text += "   <div class=\"itemImage\" style=\"float:left;\">";
                            itemInformation.Text += "       <img src=\"" + Global.globalSiteImagesPath + "/" + imageThumbnail  + "\" border=\"0\" width=\"45\"/>";
                            itemInformation.Text += "   </div>";
                            itemInformation.Text += "   <div class=\"itemInformation\" style=\"margin-left:3px;\">";
                            itemInformation.Text += "       <div class=\"itemTitle\" align=\"center\">";
                            itemInformation.Text += "           <a href=\"product.aspx?p=" + productId + "\" onClick=\"document.getElementById('topSearch').value = '" + originalTitle + "'\">" + productTitle + "</a>";
                            itemInformation.Text += "       </div>";
                            itemInformation.Text += "       <div class=\"itemDescription\" align=\"left\">";
                            itemInformation.Text += "           " + productDescription;
                            itemInformation.Text += "       </div>";
                            itemInformation.Text += "   </div>";
                            itemInformation.Text += "</div>";
                            itemInformation.Text += "<div style=\"clear:both;;\"></div>";*/
                        }
                    }
                    else {
                        itemInformation.Text += "    <div class=\"searchMainInt\">";
                        itemInformation.Text += "        <div class=\"searchProductInt\">";
                        itemInformation.Text += "            <div class=\"searchInfoDescrip\">";
                        itemInformation.Text += "                <h2>Click the Search Button.</h2>";
                        itemInformation.Text += "           </div>";
                        itemInformation.Text += "        </div>";
                        itemInformation.Text += "        <div class=\"clear\"></div>";
                        itemInformation.Text += "    </div>";
                    }
                }
                resultTitle.Dispose();
                resultTitle = null;
                dsfinder.Clear();
                dsfinder = null;
                itemInformation.Text += "    <div class=\"searchBottonInt\">";
                itemInformation.Text += "        <a href=\"result.aspx?txtadv=" + findWord + "\">";
                itemInformation.Text += "        <div class=\"contBottonInt\">";
                itemInformation.Text += "            <div class=\"labelInt\"><p>Advanced Search</p></div>";
                itemInformation.Text += "            <div><img src=\"" + Global.globalSiteImagesPath + "/arrowSearch.jpg\"/></div>";
                itemInformation.Text += "        </div></a>";
                itemInformation.Text += "        <div class=\"clear\"></div>";
                itemInformation.Text += "    </div>";
                itemInformation.Text += "</div>";
            }
        }

    }

}
