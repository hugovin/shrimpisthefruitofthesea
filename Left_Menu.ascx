<%@ Control ClassName="Left_Menu" Language="C#" AutoEventWireup="true" CodeBehind="Left_Menu.ascx.cs" CodeFile="Left_Menu.ascx.cs" Inherits="uc_Left_Menu.Left_Menu" %>
<link href="<% = Global.globalSiteStylePath %>/content.css" rel="stylesheet" type="text/css" />

<script src="js/expandir.js" type="text/javascript"></script>

<div id="sideBar">
    <div id="FinderAdvanced" runat="server">
        <div id="advanceMed">
        <div class="searchTop" id="link2">
        <!-- <a href="#" id="link" name="link">Product Finder</a> -->
           <a href="#" id="link" name="link" >Product Finder</a>
        </div>
        <div id="searchPop" class="searchPop">
            <div class="cajaArriba">
                <div class="cajaFlecha">
                    <div id="advanced" class="estiloFlecha" style="background-image: url(<% = Global.globalSiteImagesPath %>/cer.gif);
                        background-repeat: no-repeat;">
                    </div>
                </div>
            </div>
            <div class="advancemedio advanceMed" id="advanceMed" style="padding: 0px; text-align: left; padding:15px 0 0 12px;">
                <form action="result.aspx" method="POST" name="avanzado" id="avanzado">
                
                    <input name="txtadv" type="text" style="margin:2px;" id="Text1" value="Keyword / Item #" size="20" onclick="javascript:document.avanzado.txtadv.value=''">
                <div id="refineSearch" style="position: absolute; overflow: hidden; visibility: hidden;">
                    <asp:PlaceHolder ID="PlaceHolder_Finder" runat="server"></asp:PlaceHolder>
                    <input type="image" src="<% = Global.globalSiteImagesPath %>/searchLeft.jpg" id="refine-search" name="parents-btn" value="Search" width="70"
                        style="cursor: pointer; padding:10px 0 5px 0;" />
                </div>
                <div id="search" style="position: static; overflow: hidden;">
                    <input type="image" src="<% = Global.globalSiteImagesPath %>/buttonRefine.jpg" name="link2" value="Refine Search"  onclick="changeimage('advanced'); return false;" />
                </div>
                </form>
            </div>
            <div class="cajaAbajo advanceBot">
            </div>
        </div>
    </div>
    </div>
    <div id="accordion2" runat="server">
    </div>
    <div class="register">
        <p>
        Choose to receive Email <br />
        Newsletters, Catalogs or <br />
        Special Offers. <br />
        </p>
        <center>
        <%
            Site siteSignUp = new Site();
            if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
            {
                Response.Write("<a href=\"emailsignup.aspx\"><img src=\"" + Global.globalSiteImagesPath + "/signup.jpg\" border=\"0\" /></a>");
            }
            else {
                Response.Write("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" rel=\"type:element\" onClick=\"followLink='emailsignup.aspx';clear_follow();\"><img src=\"" + Global.globalSiteImagesPath + "/signup.jpg\" border=\"0\" /></a>");
            }
        %>
        </center>
   </div>
</div>
