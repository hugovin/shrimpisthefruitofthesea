<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="NewInfo.aspx.cs" Inherits="NewInfo" Title="Untitled Page" %>
<%@ Reference Control="~/uc_WhatsNew.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>
<asp:Content ID="HomeContent" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
   <div id="cont">
        <asp:PlaceHolder ID="PlaceHolder_Resources_NewInformation" runat="server"></asp:PlaceHolder>
      
       <div id="sidebar-content">		       
                  	
                    <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
                     <asp:PlaceHolder ID="PlaceHolder_uc_WhatsNew" runat="server"></asp:PlaceHolder>			       	


	    </div>
            </div>           
</asp:Content>

