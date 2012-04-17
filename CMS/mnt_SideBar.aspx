<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_SideBar.aspx.cs" Inherits="CMS_mnt_SideBar" %>

<asp:Content ID="contentSide" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="MainContainer"> 
     
     	<div class="Boxes"> 
   		  <div class="box-sidebar-title">Please Select Sidebar Box <img src="images/arrow-down.gif" /></div> 
            
            <div class="box-sidebar"> 
            
            	<a href="mnt_BestSellers.aspx?SubjID=0&Subject=None">BestSellers</a><br /> 
            	
                <a class="sidebarLink" href="#">Contact Expert</a><br /> 
               
                 <a href="mnt_FeatureProducts.aspx?SubjID=0&Subject=None">Feature Products</a><br /> 
                
                <a href="mnt_HelpFulLinks.aspx">HelpFul Links</a><br /> 
                
                <a class="sidebarLink" href="#">Related Links</a><br /> 
                
                <a class="sidebarLink" href="#">Related Products</a><br /> 
                
                 <a href="mnt_Specials.aspx">Specials</a><br /> 
                
                <a class="sidebarLink" href="#">Special Academic Pricing</a><br /> 
                
                <a href="mnt_WhatsNew.aspx">What's New</a><br />
                
                <a class="sidebarLink" href="#">White Papers</a><br /> 
                
          </div> 
            
     	</div>       
     
  </div> 

</asp:Content>
