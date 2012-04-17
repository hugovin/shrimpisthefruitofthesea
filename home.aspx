
<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="home.aspx.cs"
    Inherits="home" Title="Untitled Page" %>

<asp:Content ID="HomeContent" ContentPlaceHolderID="HomeContent_Master" runat="Server">


     <script src="js/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
		var flashvars = {xml:"/xml/data.aspx"
		};
		var params = {
			menu: "false",
			scale: "noScale",
			allowFullscreen: "true",
			allowScriptAccess: "always",
			bgcolor: "#F0F0F0",
			WMode:"transparent" 
		};
		var attributes = {
			id:"<% Response.Write(Global.globalTeatherFlash); %>"
		};
		swfobject.embedSWF("<% Response.Write(Global.globalTeatherFlash); %>", "altContent", "755", "260", "9.0.0", "expressInstall.swf", flashvars, params, attributes);
	</script>
	<script type="text/javascript">
		var flashvars2 = {carXML:"/xml/carouselData.aspx"};
		var params2 = {
			menu: "false",
			scale: "noScale",
			allowFullscreen: "true",
			allowScriptAccess: "always",
			bgcolor: "#F0F0F0",
			WMode:"transparent" 
		};
		var attributes2 = {id:"Carrusel"};
		swfobject.embedSWF("Carrusel.swf", "MooFlow", "765", "251", "9.0.0", "expressInstall.swf", flashvars2, params2, attributes2);
	</script>
    <script type="text/javascript">
		/* <![CDATA[ */
        window.addEvent('domready', function() {
	        /**
	         * Element with id 'demo-block' is the container and all h4-elements
	         * inside are fetched as tab headers. The following elements are their
	         * content.
	         */
	        var tabs = new SimpleTabs('demo-block', {
		        selector: 'h4'
	        });

	        /**
	         * Anchors with # are not unobtrusive, its only for showing the addTab method
	         */
        });
		    /* ]]> */
    </script>
    <div id="cont">
        <asp:PlaceHolder ID="PlaceHolder_slideshow" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="PlaceHolder_demo_block" runat="server"></asp:PlaceHolder>
        <div class="box">
            <asp:PlaceHolder ID="PlaceHolder_Highlights" runat="server"></asp:PlaceHolder>    
        </div>
        <div class="carrousell">
            <div id="MooFlow">
                <asp:PlaceHolder ID="PlaceHolder_Brands" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
