<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master"  AutoEventWireup="true" CodeFile="mnt_Finder.aspx.cs" Inherits="mnt_Finder" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">  
	function showItems(){	
		document.getElementById('htmlElement').style.visibility = 'visible';	
	}	
	function hiddenItems(){	
		document.getElementById('htmlElement').style.visibility = 'hidden';	
	}
 
</script> 
    <h1>Product Finder</h1>
    <div id="div_Cat" style="float:left">
     <div class="cdro_caja2">
        <div id="div_CategoryItems" runat="server" style="float:left" > </div>
         </div>   
    </div>
    
   <div id="htmlElement" style="width:320px; position:absolute; margin:-300px 0 0 45%; background-color:#FFFFFF; border:solid 5px #999999; visibility:hidden; top: 574px; left: -258px;"> 
    <div style="width:315px; padding:5px;">  
        <div id="div_FinderSubCategory" runat="server" style="float:left"></div> 
           <div id="div_btnsaveSubcategory" runat="server" style="float:none">         
             <div style="float:left">
            <asp:Button ID="btn_CancelSubCategory" runat="server" Visible="false" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave without save?')" onclick="btn_CancelSubCategory_Click"></asp:Button></div>
            <div style="float:left" >
            <asp:Button ID="btn_SaveSubCategory" runat="server"  CssClass="class_btnSave" Visible ="false" OnClientClick="return confirm('Do you want to continue?')" onclick="btn_SaveSubCategory_Click"></asp:Button>  
            </div>
            <div class="clear"></div>
         </div>
       </div>
    </div>
    
    
    <div id="htmlElement2" style="width: 320px; position: absolute; margin: -300px 0 0 45%;
        background-color: #FFFFFF; border: solid 5px #999999; visibility: hidden; top: 574px;
        left: -258px;">
        <div style="width: 315px; padding: 5px;">
            <div id="div_FinderDefaultItem" runat="server" style="float: left">
                <div id="div_FinderDefaultItemList" runat="server">
                </div>
                <div id="div_saveSubDefault" runat="server" visible="false">
                    <asp:Button ID="btn_CancelSubDefault" runat="server" Text="" CssClass="class_Cancel" OnClick="btn_CancelSubDefault_Click">
                    </asp:Button>
                </div>
            </div>
        </div>
    </div>
    
    <div id="htmlElement3" style="width: 320px; position: absolute; margin: -300px 0 0 45%; background-color: #FFFFFF;
        border: solid 5px #999999; visibility: hidden; top: 572px; left: 93px;">
        <div style="width: 315px; padding: 5px;">
            <div id="div_MoreDefaultItems" runat="server" style="float: left" visible="false">
                Search:<br />
                <input id="txCriteria" type="text" runat="server" />
                <asp:ImageButton src="images/goBtnTable.jpg" ID="btn_SearchDefaultItems" runat="server" Text="Search" OnClick="btn_SearchDefaultItems_Click" />
                <div class="clear"></div>
                <div id="div_MoreDefaultItemsList" runat="server">
                </div>
                <div class="clear"></div>
                <div style="float:left">
                <asp:Button ID="btn_CancelSearch" runat="server" Text="" CssClass="class_Cancel" OnClick="btn_CancelSearch_Click"
                    Visible="false"></asp:Button></div>
                <div style="float:left">   
                <asp:Button ID="btn_SaveSearch" runat="server" Text="" CssClass="class_btnSave" OnClick="btn_SaveSearch_Click"
                    Visible="false" ></asp:Button></div> 
                    <div class="clear"></div>
                    <br />
            </div>
        </div>
    </div>
    
    
    <div id="div_Defa" style="float:left">
        <div id="div_FinderDefault" runat="server" style="float:left"></div>
        <div id="div_MoreDefault" runat="server" style="float:left"></div>         

                      
    </div>
    
    <div id="div2" style="clear:both"></div>
    <div id="div_buttons">
            <div style="float:left">
            <asp:Button ID="btn_Cancel" runat="server" Text="" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave?')"
                onclick="btn_Cancel_Click"></asp:Button></div>
            <div style="float:left">
            <asp:Button ID="btn_SaveFinder" runat="server" Text="" CssClass="class_btnSave"
                onclick="btn_SaveFinder_Click" ></asp:Button></div>
                <div class="clear"    ></div>
    </div>
    <%if (FinderSubCategory == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.visibility = 'visible';</script>"); } %> 
    <%if (FinderDefaultItemList == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement2').style.visibility = 'visible';</script>"); } %> 
    <%if (MoreDefaultItems == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement3').style.visibility = 'visible';</script>"); } %> 
</asp:Content>