<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_Browse.aspx.cs" Inherits="mnt_Browse" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script src="js/dhtmlapi.js" type="text/javascript"></script>
    
    <h1>Browse Navigation Area </h1>
    <div class="MainContainer2">
        <div  class="titulos2">Sections inside Browse&nbsp;&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
            <div id="contCajas5">
                <div class="cdro_caja2">
                    <div id="div_CategoryItems" runat="server"> </div>                
             
             <table>
             <tr><td>
                      <asp:Button ID="btn_Cancel" runat="server" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave?')"
                onclick="btn_Cancel_Click"></asp:Button></td><td>
            <asp:Button ID="btn_SaveBrowse" runat="server" CssClass="class_btnSave"
                onclick="btn_SaveBrowse_Click" ></asp:Button></td>
            </tr>
            </table>
            </div>
            </div>
    </div> 

        <div id="htmlElement" style="width:320px; position:absolute; margin:-300px 0 0 45%; background-color:#FFFFFF; border:solid 5px #999999; visibility:hidden; top: 562px; left: 172px;"> 
    <div style="width:315px; padding:5px;">  
            <div id="div_BrowseSubCategory" runat="server"></div> 
            <div id="div_btnsaveSubcategory">
            <table>
                <tr>
                    <td><asp:Button ID="btn_CancelSubCategory" runat="server" CssClass="class_Cancel" Visible ="false" OnClientClick="return confirm('Do you want to leave without save?')" onclick="btn_CancelSubCategory_Click"></asp:Button>      </td>
                    <td><asp:Button ID="btn_SaveSubCategory" runat="server" CssClass="class_btnSave" Visible ="false" OnClientClick="return confirm('Do you want to continue?')" onclick="btn_SaveSubCategory_Click"></asp:Button>  </td>
                </tr>
            </table>           
            
            </div>
              </div>
    </div>

    
   
    <div id="div_Defa" style="float:left">
        <div id="div_BrowseDefault" runat="server"></div>
        

                <div id="div_MoreDefault" runat="server">
                </div>

        
        <div id="div_BrowseDefaultItem" runat="server" >
        
   <div id="htmlElement1" style="width:320px; position:absolute; margin:-300px 0 0 45%; background-color:#FFFFFF; border:solid 5px #999999; visibility:hidden; top: 562px; left: 172px;"> 
    <div style="width:315px; padding:5px;"> 
                        <div id="div_BrowseDefaultItemList" runat="server"></div>
                        <div id="div_saveSubDefault" runat="server" visible = "false">
                            <asp:Button ID="btn_CancelSubDefault"  runat="server" Text="" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave without save?')"
                                onclick="btn_CancelSubDefault_Click"></asp:Button>
                        </div> 
                        </div>
                        </div> 
            </div>
                    <div id="htmlElement3" style="width: 320px; position: absolute; margin: -300px 0 0 45%;
            background-color: #FFFFFF; border: solid 5px #999999; visibility: hidden; top: 575px;
            left: 503px;">
            <div style="width: 315px; padding: 5px;">
        <div id="div_MoreDefaultItems" runat="server" style="float:left" visible ="false">
           Search:<br />
            <input id="txCriteria" type="text" runat="server" />
            <asp:ImageButton src="images/goBtnTable.jpg" ID="btn_SearchDefaultItems" runat="server" Text=""  onclick="btn_SearchDefaultItems_Click" />           
                <div id="div_MoreDefaultItemsList" runat="server"></div>
                <div class="clear"></div>
                <div style="float:left">
                <asp:Button ID="btn_CancelSearch" runat="server" Text="" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave without save?')" onclick="btn_CancelSearch_Click" Visible="false"></asp:Button></div>
                <div style="float:left">
                <asp:Button ID="btn_SaveSearch" runat="server" Text="" CssClass="class_btnSave" OnClientClick="return confirm('Do you want Continue?')" onclick="btn_SaveSearch_Click" Visible="false"></asp:Button></div>
                <div class="clear"></div>
        </div>
        </div>
        </div>
            

                      
  
  </div>
    
    <div id="div2" style="clear:both">
        

<%if (subItems == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.visibility = 'visible';</script>"); } %> 
<%if (BrowseDefaultItemList == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement1').style.visibility = 'visible';</script>"); } %> 
<%if (MoreDefault == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement3').style.visibility = 'visible';</script>"); } %> 

</div>

    <script type="text/javascript">
    var maxRows=71;// max rows to sortorder
    function sortorder(){
  
    var j=1;//order number start in 1
    for(i=0; i<document.aspnetForm.elements.length; i++)
	    {
	    
		    if (document.aspnetForm.elements[i].name.substring(0,7)== "txtCaIt"){
			    document.aspnetForm.elements[i].value=j++;
			    }
	    }
	    }
	</script>
    <script language="Javascript" type="text/javascript" src="js/dnd2.js"></script>
    
</asp:Content>