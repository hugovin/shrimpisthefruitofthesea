<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS_GroupSelection.aspx.cs"
    Inherits="CMSsite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>CMS</title>
    <link href="css/multibox.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 6]><link rel="stylesheet" href="ie6.css" type="text/css" media="all" /><![endif]-->

    <script type="text/javascript" src="js/mootools.js"></script>

    <script type="text/javascript" src="js/overlay.js"></script>

    <script type="text/javascript" src="js/multibox.js"></script>
</head>
<body>
    <div class="wrapper">
        <div class="header">
            <a href="#">
                <img src="images/logo.jpg" alt="CMS" border="0" /></a>
        </div>
        <div class="login-info">
            <div class="LoginOptions">
                <a href="CMS_Login.aspx">
                    <img src="images/close-icon.jpg" alt="Close" border="0" align="absmiddle" /></a>&nbsp;Logout
            </div>
        </div>
        <div class="menuHolder">
            <div class="menu">
                <div id="div_selectionGroup" runat="server">
                </div>
                </a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="MainContainer">
            <div class="Boxes">
                <div class="toolTip">
                    <img src="images/box-resourcecenter.jpg" alt="Resource Center" border="0" usemap="#Map" />
                    <span></span>
                </div>
                <!--<div class="toolTip">
                    <a href="#"><span></span>&nbsp;</a></div>
                <div class="toolTip">
                    <a href="#"><span></span>
                       
                    </a>
                </div>
                <div class="toolTip" style="display:none;">
                    <a href="#">
                        <img src="images/box-checkout.jpg" alt="Checkout" border="0" usemap="#Map4" /><span></span>
                       
                    </a>
                </div>-->
            </div>
            <div class="buttBoxes">
                <a href="mnt_Site.aspx"><img src="images/site_info.jpg" alt="Site Information" border="0" /></a>
                <a href="mnt_Generics.aspx?Generic=2"><img src="imagesCss/box-about.jpg" alt="About" border="0" /></a>
                <span><img src="imagesCss/box-home.jpg" alt="Home" border="0" /></span> 
                <span><img src="images/RC_mainpage.jpg" alt="Resource Center Main Page" border="0" /></span>
            </div>
            <div class="clear">
            </div>

            <script type="text/javascript"> 
			var box = {};
			window.addEvent('domready', function(){
				box = new MultiBox('mb', {descClassName: 'multiBoxDesc', useOverlay: true});
			});
            </script>

        </div>
        <div class="push">
        </div>
    </div>
</body>
</html>
