<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Generic_e.aspx.cs" Inherits="Generic_e" Title="Untitled Page" %>
<%@ Reference Control="~/uc_SAPricing.ascx" %>
<%@ Reference Control="~/uc_NewsNinfo.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <div id="cont">
        <div id="print">                
            <asp:PlaceHolder ID="PlaceHolder_Generic_E" runat="server"></asp:PlaceHolder>  
        </div>
        <div id="sidebar-content">		       
                  	
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
            <!-- End div boxContact-->
             <asp:PlaceHolder ID="PlaceHolder_uc_SAPricing" runat="server"></asp:PlaceHolder>
	       	
			<!-- End div Special academic pricing-->
			<asp:PlaceHolder ID="PlaceHolder_uc_NewsNinfo" runat="server"></asp:PlaceHolder>
		    <!-- End div boxNews And info-->

	    </div>   
    </div>  
</asp:Content>

