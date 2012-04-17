<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_Administrator.aspx.cs" Inherits="CMS_mnt_Administrator" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Administartor Site</h3>

<div class="cdro_caja2">
<div id="div_ListOfUser" runat="server">

</div>
</div>
<div class="clear"></div>
<asp:Button ID="btnAddUser" runat="server" CssClass="class_addNew" Text="" 
        onclick="btnAddUser_Click" />

<div id="htmlElement" name="htmlElement"  style="width:530px; position:absolute; margin:-300px 0 0 45%; visibility:hidden; background-color:#FFFFFF; border:solid 5px #999999;  top: 615px; left: -87px;"> 
    <div  style="width:315px; padding:5px;"> 
        <div id="div_UserInfo" runat="server">        
        </div>
        <div class="clear"></div>
        <div id="btns">
        <div style="float:left">
        <asp:Button ID="btnCancelUser" runat="server" CssClass="class_Cancel" 
                onclick="btnCancelUser_Click" />        
        </div>
        <div style="float:left">
        <asp:Button ID="btnSaveUser" runat="server" CssClass="class_btnSave" Text="" 
                onclick="btnSaveUser_Click" />       
        </div>
         <div class="clear"></div><asp:Label ID="lbError" runat="server" Text=""></asp:Label>
        </div>
        </div>
</div>      

<%if (AddEdit == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.visibility = 'visible';</script>"); } %>      

</asp:Content>

