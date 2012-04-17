<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_Subjects.aspx.cs" Inherits="mnt_Subjects" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript" src="../js/mootools.js"></script> 
<script type="text/javascript" src="../js/overlay.js"></script> 
<script type="text/javascript" src="../js/multibox.js"></script> 
<script type="text/javascript" src="js/funciones.js"></script> 
    <script src="js/dhtmlapi.js" type="text/javascript"></script>

<script type="text/javascript"> 
 
	function showItems(){
	
		document.getElementById('htmlElement').style.visibility = 'visible';
	
	}
	
	function hiddenItems(){
	
		document.getElementById('htmlElement').style.visibility = 'hidden';
	
	}
 
</script>


 

     <div class="MainContainer2">
        <div  class="titulos2">Sections inside Subjects&nbsp;&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
            <div id="contCajas5">
                <div class="cdro_caja2">
                    <div id="div_category" runat="server"> No Results
                    </div>
                </div>
                       <div class="clear"></div> 
                <div id="div_Buttons" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnCancelSubj" runat="server" CssClass="class_Cancel" Text="" OnClientClick="return confirm('Do you want leave without save?')"
                                    OnClick="btnCancelSubj_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSavesubj" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Do you want to save?');"
                                    Text="" OnClick="btnSaveSubj_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
     </div>

     
<div id="htmlElement" style="width:320px; position:absolute; margin:-300px 0 0 45%; background-color:#FFFFFF; border:solid 5px #999999; visibility:hidden"> 
    <div style="width:315px; padding:5px;"> 
   <div id="div_sub_Subjects" runat="server" visible = "false">
       <div id="div_SubCategory" runat ="server"> </div>
       <div id="div_SubButtons" runat ="server"> 
       <table>
       <tr>
       <td>
       <asp:Button ID="btn_CancelSub" runat="server" CssClass="class_Cancel" OnClientClick="return confirm('Do you want leave without save?')" onclick="btn_CancelSub_Click"></asp:Button></td>
       <td>
       <asp:Button ID="btn_SaveSub" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Do you want save changes?')" onclick="btn_SaveSub_Click"></asp:Button></td>
       </tr>
       </table>
       </div>
       
    </div>  
    </div>
</div>
<%if (subItems == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.visibility = 'visible';</script>"); } %> 

    <script type="text/javascript">
    var maxRows=20;// max rows to sortorder
    function sortorder(){
  
    var j=1;//order number start in 1
    for(i=0; i<document.aspnetForm.elements.length; i++)
	    {
	    
		    if (document.aspnetForm.elements[i].name.substring(0,5)=="txt_o"){
			    document.aspnetForm.elements[i].value=j++;
			    }
	    }
	    }
	</script>
    <script language="Javascript" type="text/javascript" src="js/dnd2.js"></script>
</asp:Content>
