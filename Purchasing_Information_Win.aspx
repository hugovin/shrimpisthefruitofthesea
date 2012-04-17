<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Purchasing_Information_Win.aspx.cs" Inherits="Purchasing_Information_Win" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/global.css"/>
    <link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/mootabs1.2.css"/>        
    <script type="text/javascript" src="js/script.js"></script>
	<script type="text/javascript" charset="utf-8" src="js/mootools-1.2.1-core.js"></script>    
    <script type="text/javascript" charset="utf-8" src="js/more-slider.js"></script>  
    <script type="text/javascript" src="js/SimpleTabs.js"></script>
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
	<script type="text/javascript" charset="utf-8" src="js/backgroundslider.js"></script>
    <script type="text/javascript" charset="utf-8" src="js/slideshow.js"></script>
    <script type="text/javascript" charset="utf-8" src="js/multibox.js"></script> 
    <script type="text/javascript" charset="utf-8" src="js/overlay.js"></script>

    <script type="text/javascript" src="js/MooFlow.js"> </script>
    <script type="text/javascript">
    window.addEvent('domready', function(){
        //call multiBox
        var initMultiBox = new multiBox({
            mbClass: '.mb',//class you need to add links that you want to trigger multiBox with (remember and update CSS files)
            container: $(document.body),//where to inject multiBox
            descClassName: 'multiBoxDesc',//the class name of the description divs
            path: './Files/',//path to mp3 and flv players
            useOverlay: true,//use a semi-transparent background. default: false;
            maxSize: {w:520, h:719},//max dimensions (width,height) - set to null to disable resizing
            addDownload: true,//do you want the files to be downloadable?
            pathToDownloadScript: 'js/ForceDownload.asp',//if above is true, specify path to download script (classicASP and ASP.NET versions included)
            addRollover: true,//add rollover fade to each multibox link
            addOverlayIcon: true,//adds overlay icons to images within multibox links
            addChain: true,//cycle through all images fading them out then in
            recalcTop: true,//subtract the height of controls panel from top position
            addTips: true//adds MooTools built in 'Tips' class to each element (see: http://mootools.net/docs/Plugins/Tips)
        });
    });
    </script>	
    <script type="text/javascript" src="js/centerImages.js"></script>
    <title>ER test Plain HTML</title>
</head>
<body style="background-image: none; background-color: #eee">   
    <asp:PlaceHolder ID="PlaceHolder_Resources_Purchasing" runat="server"></asp:PlaceHolder>                   
</body>
</html>
