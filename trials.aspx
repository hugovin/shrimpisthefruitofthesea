<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="trials.aspx.cs" Inherits="trials" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">

<div id="cont">
<div class="mainAbout">

    <script src="js/downloadtrials.js" type="text/javascript"></script>
		   	<div id="titleTrial"><h1><% Response.Write(pagePrintTitle); %></h1></div>
			<div id="subTrial""><h3><strong><% Response.Write(pagePrintSubTitle); %></strong></h3><br /></div>
		   	<div id="infoTrial">

<p><% Response.Write(pagePrintContent); %></p>
			
			</div>
			
        <div id="contPagTrial1">
		<asp:Literal ID="paginationUp" runat="server"></asp:Literal>
        
        </div>
		
		<div style="clear: both;"></div>
        
        <div id="listTrial">
		
		<asp:Literal ID="pagesContent" runat="server"></asp:Literal>
		
		</asp:Literal>	
        </div>
        
  <div id="contPagTrial2">
		<asp:Literal ID="paginationDown" runat="server"></asp:Literal>
        
        </div>

</div> <!-- End div mainAccount -->
</div>
</asp:Content>

