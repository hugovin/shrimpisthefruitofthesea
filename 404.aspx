<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" Title="Untitled Page" %>
<%@ Reference Control="~/uc_NewsNinfo.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
<div id="cont">
    <div id="main-content">
                    <div>
                        <div class="boxURLRequested">
                            <div class="topURLRequested"></div>
                            <div class="mainURLRequested">
                            	<div class="contURLRequested">
                                <h1>The Web page you requested is not available while offline.</h1><br />
                                <p>Please try again in a few minutes. <a href="home.aspx">Click here to go to the homepage</a></p></div>
                            </div>
                            <div class="bottonURLRequested"></div>
                        </div>
                    </div>
                    <!--<div class="goToHomepage"><p>Click here to go to the <a href="#">Homepage</a> or <a href="#">continue</a> shopping</p></div>-->
               </div>
        <div id="sidebar-content">		       
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
			<!-- End div Special academic pricing-->
			<asp:PlaceHolder ID="PlaceHolder_uc_NewsNinfo" runat="server"></asp:PlaceHolder>
		    <!-- End div boxNews And info-->

	    </div>  
    </div>
</asp:Content>

