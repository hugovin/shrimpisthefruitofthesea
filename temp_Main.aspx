<%@ Page Language="C#" AutoEventWireup="true" CodeFile="temp_Main.aspx.cs" Inherits="temp_Main" %>

<%@ Reference Control="Left_Menu.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/global.css" />
    <link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/mootabs1.2.css" />

    <script type="text/javascript" src="js/script.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/mootools-1.2.1-core.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/more-slider.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/morphlist.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/barackslideshow.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/demo.js"></script>

    <script type="text/javascript" charset="utf-8" src="js/mootabs1.2.js"></script>

    <script type="text/javascript" src="js/SimpleTabs.js"></script>

    <script type="text/javascript" src="js/MooFlow.js"></script>

    <script type="text/javascript">
    /* <![CDATA[ */
        var myMooFlowPage = {      
            start: function(){        
                var mf = new MooFlow($('MooFlow'), {
                    startIndex: 2,
                    useSlider: true,
                    useCaption: true,
                });                
            }            
        };        
        window.addEvent('domready', myMooFlowPage.start);        
    /* ]]> */    
    </script>

    <script type="text/javascript">
		/* <![CDATA[ */

window.addEvent('domready', function() {

	/**
	 * Element with id 'demo_block' is the container and all h4-elements
	 * inside are fetched as tab headers. The following elements are their
	 * content.
	 */
	var tabs = new SimpleTabs('demo_block', {
		selector: 'h4'
	});

	/**
	 * Anchors with # are not unobtrusive, its only for showing the addTab method
	 */
	

});
		/* ]]> */
    </script>

    <title>ER test Plain HTML</title>
</head>
<body>
    <div id="wrapper">
        <div id="head">
            <div class="logo">
            </div>
            <div class="login">
                <div class="loginNav">
                    <ul>
                        <li>Welcome <strong>Jack Frost</strong> (<a href="#">not you?</a>)&nbsp;&nbsp; </li>
                        <li><a href="#">My Account</a></li>
                        <li><a href="#">Wish List</a></li>
                        <li class="last" style="background-image: none;"><a href="#">View Cart</a></li>
                    </ul>
                </div>
                <div class="phoneNumber">
                    <a href="#">
                        <img src="<% = Global.globalSiteImagesPath %>/phoneNumber.jpg" border="0" />
                    </a>
                </div>
                <div class="topSearch">
                    <a href="#">
                        <img src="<% = Global.globalSiteImagesPath %>/searchLeft.jpg" border="0" />
                    </a>
                    <input name="topSearch" type="text" id="topSearch" value="" size="30">
                </div>
                <div class="advSearch">
                    <a href="#"><strong>+</strong> Advanced Search</a>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div id="topNav">
            <div id="topLeft">
                <div id="topRight" runat="server">
                </div>
            </div>
        </div>
        <div id="bread" runat="server">
        </div>
        <div id="mainCont">
            <div id="cont">
                <div id="slideshow" runat="server">
                </div>
                <div id="demo_block" runat="server">
                    
                </div>
                <div class="box">
                    <div class="blueBox">
                    </div>
                    <div class="helpBox">
                    </div>
                    <div class="greenBox">
                    </div>
                </div>
                <div class="carrousell">
                    <div id="MooFlow">
                        <div>
                            <img src="<% = Global.globalSiteImagesPath %>/adobe.jpg" title="Adobe" alt="Adobe" /></div>
                        <div>
                            <img src="<% = Global.globalSiteImagesPath %>/carel.jpg" title="COREL" alt="COREL" /></div>
                        <div>
                            <img src="<% = Global.globalSiteImagesPath %>/sunbusrst.jpg" title="SUNBURST" alt="SUNBURST" /></div>
                        <div>
                            <img src="<% = Global.globalSiteImagesPath %>/riverdeep.jpg" title="Riverdeep" alt="Riverdeep" /></div>
                        <div>
                            <img src="<% = Global.globalSiteImagesPath %>/symantec.jpg" title="symantec" alt="symantec" /></div>
                    </div>
                </div>
            </div>
            <asp:PlaceHolder ID="LeftMenuPlaceHolder" runat="server"></asp:PlaceHolder>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="footer">
            <div id="footNav">
                <div id="footLeft">
                    <div id="footRight">
                        <div class="italicFooter">
                            <em>providing software, hardware and supplemental learning solutions.</em></div>
                        <div class="share">
                            <a href="#">
                                <img src="<% = Global.globalSiteImagesPath %>/footBluePlus.jpg" width="11" height="11" />
                                Share</a></div>
                    </div>
                </div>
            </div>
            <div class="footCont">
                <div class="footList">
                    <img src="<% = Global.globalSiteImagesPath %>/usaFlag.jpg" width="78" height="36" /><img src="<% = Global.globalSiteImagesPath %>/candFlag.jpg"
                        width="88" height="36" />
                    <h3>
                        Educational Resources</h3>
                    <p>
                        1550 Executive Drive<br />
                        P.O. Box 1900
                        <br />
                        Elgin, IL 60121-1900
                    </p>
                    <h3>
                        Email</h3>
                    <p>
                        <span>Customer Service:</span> <a href="mailto:custerv@edresources.com">custerv@edresources.com</a><br />
                        <span>Sales:</span> <a href="mailto:sales@edresources.com">sales@edresources.com</a></p>
                </div>
                <div class="footList">
                    <h3>
                        Resources</h3>
                    <ul>
                        <li><a href="#">News & Information</a></li>
                        <li><a href="#">Contact Expert</a></li>
                        <li><a href="#">Support</a></li>
                        <li><a href="#">Training Tools</a></li>
                        <li><a href="#">Free Tools</a></li>
                        <li><a href="#">Special Academic Pricing</a></li>
                        <li><a href="#">Software Licensing</a></li>
                    </ul>
                </div>
                <div class="footList">
                    <h3>
                        About ER</h3>
                    <ul>
                        <li><a href="#">About Us</a></li>
                        <li><a href="#">Contact Us</a></li>
                        <li><a href="#">Employment</a></li>
                        <li><a href="#">Mission</a></li>
                        <li><a href="#">Partners</a></li>
                        <li><a href="#">Resellers</a></li>
                    </ul>
                </div>
                <div class="footList" style="background-image: none;">
                    <h2>
                        Register Now!</h2>
                    <h4>
                        Sign up today to stay informed.</h4>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur
                        <br />
                        adipisicing elit,
                    </p>
                    <input name="register" type="text" id="register" value="" size="32" onclick="#"><br />
                    <a href="#">
                        <img src="<% = Global.globalSiteImagesPath %>/footersignUp.jpg" alt="sign up" border="0" /></a>
                </div>
                <div class="clear">
                </div>
                <div class="footBL">
                    All Content © copyright 2009 ER.com, All Rights Reserved</div>
                <div class="footBR">
                    <h1>
                        1 (800) 860-7004 Toll Free</h1>
                    <ul>
                        <li><a href="#">Site Map</a> | </li>
                        <li><a href="#">Privacy Policy</a> |</li>
                        <li><a href="#">Terms of Use</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
