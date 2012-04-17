<%@ Control ClassName="mnt_Sitemap" Language="C#" AutoEventWireup="true" CodeBehind="~/CMS/mnt_SiteMap.ascx.cs" CodeFile="~/CMS/mnt_SiteMap.ascx.cs" Inherits="miniSitemap.CMS_mnt_SiteMap" %>


<link href="css/style-box.css" rel="stylesheet" type="text/css" />

<div style="position: relative; float: right; margin: 10px;"><a href="#" onclick="document.getElementById('div_internalLink').style.visibility='hidden'; return false;"><img src="images/btn_accept.jpg" border="0"> </a></div>
<h2>Site Map</h2>
<div>
    <div class="boxMap">
        <div class="boxTitle">About ER</div>
        <div class="siteMapList">
            <ul><asp:PlaceHolder ID="PlaceHolder_AboutUs" runat="server"></asp:PlaceHolder></ul>
        </div>
    </div>
    <div class="boxMap">
    <div class="sitemapColII">
        <div class="boxTitle">Resource Center</div>
        <div class="siteMapList">
            <ul>
               <asp:PlaceHolder ID="PlaceHolder_ResourceCenter" runat="server"></asp:PlaceHolder>
            </ul>
        </div>
    </div>
    </div>
    <div class="clear"></div>
</div>
<div>
    <h3>Home information</h3> 
    <div class="boxMap">
    <div class="boxTitle">
        Products</div>
    <div class="siteMapList">
        <ul>
            <li onclick="getInternalPagelink('WhatNew.aspx?cp=5');">WhatsNew</li>
            <li onclick="getInternalPagelink('Clasification.aspx?cp=1');">Feature Products</li>
            <li onclick="getInternalPagelink('Clasification.aspx?cp=3');">Best Sellers</a></li>
        </ul>
    </div>
    </div>
    <div class="boxMap">
    <div class="boxTitle">
        HighLights</div>
    <div class="siteMapList">
        <ul>
            <asp:PlaceHolder ID="PlaceHolder_highlights" runat="server"></asp:PlaceHolder>
        </ul>
    </div>
    </div>
    <div class="boxMap">
    <div class="boxTitle">
        Feature brands</div>
    <div class="siteMapList">
        <ul>
            <asp:PlaceHolder ID="PlaceHolder_Brands" runat="server"></asp:PlaceHolder>
        </ul>
    </div>
    </div>
    <div class="boxMap">
    <div class="boxTitle">
        Theater</div>
    <div class="siteMapList">
        <ul>
            <asp:PlaceHolder ID="PlaceHolder_theater" runat="server"></asp:PlaceHolder>
        </ul>
    </div>
    </div>
    <div class="clear"></div>
</div>
