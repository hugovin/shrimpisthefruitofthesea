<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true"
    CodeFile="CMS_MainSite.aspx.cs" Inherits="CMS_MainSite" %>

<%@ PreviousPageType VirtualPath="~/CMS/CMS_GroupSelection.aspx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="MainContainer">
        <div class="Boxes">
            <a href="mnt_Generics.aspx?Generic=1">
                <img src="imagesCss/box-resourcecenter.jpg" alt="Resource Center" border="0" /></a>
            <a href="mnt_AddSc.aspx" style="display:none;">
                <img src="imagesCss/box-checkout.jpg" alt="Checkout" border="0" /></a>
        </div>
        <div class="buttBoxes">
            <a href="mnt_Site.aspx">
                <img src="images/site_info.jpg" alt="Site Information" border="0" /></a> <a href="mnt_Generics.aspx?Generic=2">
                    <img src="imagesCss/box-about.jpg" alt="About" border="0" /></a> <a href="mnt_Home.aspx">
                        <img src="imagesCss/box-home.jpg" alt="Home" border="0" /></a> <a href="mnt_ResourCenter.aspx">
                            <img src="images/RC_mainpage.jpg" alt="Resource Center Main Page" border="0" /></a>
        </div>
    </div>
</asp:Content>
