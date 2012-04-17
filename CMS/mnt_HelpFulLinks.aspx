<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_HelpFulLinks.aspx.cs" Inherits="CMS_mn_HelpFullLinks" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!-- TinyMCE -->
<script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <% Response.Write(str_TinyMCE);%>
<!-- /TinyMCE -->
<h1>HelpFull Links</h1>
    <div id="div_helpfullLinks" runat="server">
        <div id="div_list" runat="server">
        </div>
        <asp:Button ID="btn_Cancellist" runat="server" Text="Cancel" />
        <asp:Button ID="btn_new" runat="server" Text="New" Visible="false" 
            onclick="btn_new_Click" />
        <hr />     
    </div>
    <div id="div_Edit" runat="server" visible="false">
        <div id="div_description" runat="server">
        </div>
        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return confirm('Do you want to leave without save?');" 
            onclick="btn_cancel_Click" />
        <asp:Button ID="btn_Save" runat="server" Text="Save" OnClientClick="return confirm('Do you want to save?');" onclick="btn_Save_Click" />
    </div>    
</asp:Content>