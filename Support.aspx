<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Support.aspx.cs" Inherits="Support" Title="Untitled Page" %>
<%@ Reference Control="~/uc_WhatsNew.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <div id="cont">          
        <div id="print">
            <div id="main-content">
                <asp:PlaceHolder ID="PlaceHolder_Resources_Purchasing" runat="server"></asp:PlaceHolder>  
                <asp:PlaceHolder ID="PlaceHolder_Resources_FAQs" runat="server"></asp:PlaceHolder>    
            </div>
        </div> 
        <div id="sidebar-content">		       
                  	
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="PlaceHolder_uc_WhatsNew" runat="server"></asp:PlaceHolder>			       	

	    </div>
    </div>
</asp:Content>

