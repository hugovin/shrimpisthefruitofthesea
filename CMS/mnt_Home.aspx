<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMS/CMS_mntMasterPage.master" CodeFile="mnt_Home.aspx.cs" Inherits="CMS_mnt_Home" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
                   		<div class="titulos">Please Selecat an Area&nbsp;&nbsp;&nbsp;<img src="images/arrow1.png" /></div> 
                        <div class="newOpciones"> 
                            <a href="mnt_AddSc.aspx?pages=true" ><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Adds</div></a> 
                            <a href="mnt_FeaturedSpace.aspx"><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Theater</div></a> 
                            <a href="#" onmouseover="ver_mnu();">  </a>      
                            <a href="mnt_HighLights.aspx" ><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Highligths</div></a> 
                            <a href="mnt_FeaturedBrands.aspx" ><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Featured Brands</div></a> 
                        </div> 
</asp:Content>
