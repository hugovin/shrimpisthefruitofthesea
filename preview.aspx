<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="preview.aspx.cs" Inherits="prewiew" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <div id="cont">
        <div class="mainAbout">
		   
		   	<div id="titleTrial"><h1><% Response.Write(pagePrintTitle); %></h1>
		   	</div>
			<div id="subTrial""><h3><strong><% Response.Write(pagePrintSubTitle); %></strong></h3>
			<br /></div>
            
		   	<div id="infoTrial">
<p><% Response.Write(pagePrintContent); %></p>
			</div>
        <div id="contPagTrial1">
        <asp:Literal ID="paginationUp" runat="server"></asp:Literal>
        </div>
		
		<div style="clear: both;"></div>
        
        <div id="listTrial">
		    <asp:Literal ID="pagesContent" runat="server"></asp:Literal>
        </div>
        
  <div id="contPagTrial2">
  <asp:Literal ID="paginationDown" runat="server"></asp:Literal>
        </div>
</div>
    </div>
    <div id="hiddenVideo" class="mbHidden" style="visibility: visible;">
<!--
                <div class="quote">
                    <div class="quoteTop">
                    	<div class="popTitle">Demo</div>
                    </div>
                    <div class="quoteBody">
                        <div >
        	                <iframe src="getResourceVideo.aspx?pid=70425" width="100%" height="400" style="overflow:hidden; background:none;" allowtransparency="true" frameborder="0" id="videoIframe"></iframe>
        	            </div>
        	        </div>
                    <div class="quoteTButt"></div>
                </div>
                    -->
    </div>
</asp:Content>
