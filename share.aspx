<%@ Page Language="C#" AutoEventWireup="true" CodeFile="share.aspx.cs" Inherits="share" %>
<html>
<head runat="server">

	<link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/global.css" />

    <title>Share This</title>
    <script language="javascript" type="text/javascript">
        function nuevoAjax(){
            var xmlhttp=false;
            try {
                xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
            } catch (e) {
                try {
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }   catch (E) {
                    xmlhttp = false;
                }
            }
            if (!xmlhttp && typeof XMLHttpRequest!='undefined') {
                xmlhttp = new XMLHttpRequest();
            }
            return xmlhttp;
        }
        function ltrim(s) {
            return s.replace(/^\s+/, "");
        }
        function rtrim(s) {
            return s.replace(/\s+$/, "");
        }
        function trim(s) {
            return rtrim(ltrim(s));
        }
        function strim(s){
            return s.replace(/ /g, '');
        }
        function checkEmail(email){
            var pattern=/^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
            inputvalue = email;
            if(pattern.test(inputvalue)){
		        return true;
            }else{   
		        return false;
            }
        }
        function sendMails(){
            var mails = trim(document.getElementById('toTextArea').value);
            document.getElementById('successMail').innerHTML = "";
            if(mails != ""){
                mails = mails.replace(" ","");
                mails = mails.replace(";",",");
                mails = mails.replace("#",",");
                invalidMails = "";
                listMails = mails.split(",");
                allValid = true;
                for(i = 0; i<listMails.length;i++){
                      if(checkEmail(listMails[i]) == false){
                        allValid = false;
                        invalidMails += listMails[i]+", ";
                    }
                }
                if(invalidMails.length >0){
                    invalidMails = invalidMails.substr(0,invalidMails.length-2);
                }
                if(allValid){
                    emailStrings = '';
                    for(i = 0; i<listMails.length;i++){
                        emailStrings += listMails[i]+",";
                    }
                    
                    
                    
                    emailAjax = nuevoAjax();
                    emailAjax.open("GET","shareMailer.aspx?from="+name+"&mailList="+emailStrings+"&urls="+document.getElementById('hiddenContent').value,true);
                    emailAjax.onreadystatechange=function() {
                           if (emailAjax.readyState==4) {
                                  var response = emailAjax.responseText;
                                  response = response.substr(0,1);
                                  //alert(response);
                                  if(response == "0"){
                                        //correct.
                                        document.getElementById('errorMessage').innerHTML = "";
                                        document.getElementById('successMail').innerHTML = "Thank you. The email has been sent.";
                                  }else{
                                        //incorrect.
                                        document.getElementById('errorMessage').innerHTML = "An error has occured. Please check the information and try again.";
                                  }
                           }
                   }
                   emailAjax.send(null);
                   
                   
                   
                    return true;
                }else{
                    document.getElementById('errorMessage').innerHTML = "The box has invalid emails: "+invalidMails.substr(0,15)+"...";
                    return false;
                }
           }else{
                document.getElementById('errorMessage').innerHTML = "Mail field cannot be empty.";
                //alert("Mail field cannot be empty.");
           }
        }
        function showTwitterLogin(){
            document.getElementById('shareThisPrincipal').style.display="none";
            document.getElementById('twitterLogin').style.display = "block";
        }
        function showTwitterSuccess(){
            document.getElementById('twitterLogin').style.display = "none";
            document.getElementById('twitterFinish').style.display = "block";
            document.getElementById('twitterError').style.display = "none";
            document.getElementById('shareThisPrincipal').style.display="none";
        }
        function showTwitterError(){
            document.getElementById('twitterLogin').style.display = "none";
            document.getElementById('twitterFinish').style.display = "none";
            document.getElementById('twitterError').style.display = "block";
            document.getElementById('shareThisPrincipal').style.display="none";
        }
        function showShareThis(){
            document.getElementById('twitterLogin').style.display = "none";
            document.getElementById('twitterFinish').style.display = "none";
            document.getElementById('twitterError').style.display = "none";
            document.getElementById('shareThisPrincipal').style.display="block";
        }
        function shareOnTwitter(){
            var name = document.getElementById('twitterUsername').value;
            var pass = document.getElementById('twitterPassword').value;
            var content = document.getElementById('hiddenContent').value;
            document.getElementById('nTwitterCharger').style.display = "none";
            document.getElementById('twitterCharger').style.display = "block";
            if(name != "" && pass != ""){
                    twitterAjax = nuevoAjax();
                    twitterAjax.open("GET","publishTwitter.aspx?user="+name+"&password="+pass+"&urls="+content,true);
                    twitterAjax.onreadystatechange=function() {
                           if (twitterAjax.readyState==4) {
                                    document.getElementById('nTwitterCharger').style.display = "block";
                                    document.getElementById('twitterCharger').style.display = "none";
                                  //alert(twitterAjax.responseText);
                                  var response = twitterAjax.responseText;
                                  response = response.substr(0,1);
                                  //alert(response);
                                  if(response == "0"){
                                        //correct.
                                        showTwitterSuccess();
                                  }else{
                                        //incorrect.
                                        showTwitterError();
                                  }
                           }
                   }
                   twitterAjax.send(null);
           }
        }
    </script>
</head>

<body style="background-image:none; background-color:#eee;">

<div class="contChangePass">

<div class="leftShare">
            	<h2>FROM:</h2>
                <h2>TO:</h2>
            </div>
<div class="rigthShare">
            	<p><% Response.Write(username); %></p>
                <textarea cols="45" rows="4" id="toTextArea" name="toTextArea"></textarea>
</div>
<div class="clear"></div>            
<div class="shareText">
            	<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque sapien ipsum, lobortis et, egestas nec, sodales eget, lorem. Sed facilisis molestie augue. Donec justo. Donec dapibus venenatis justo. Vivamus rutrum, nulla in aliquet vulputate.</p>
            	<div id="errorMessage" style="float:right; color:Red;"></div><div id="successMail" style="float:right; color:blue;"></div>
            <br /><div class="btnSend"><a href="#" onclick="sendMails(); return false;"><img src="<% = Global.globalSiteImagesPath %>/buttonSend.jpg" width="52" height="19"/></a></div></div>
            
            <div id="shareThisPrincipal" class="">
                <div class="shareIcons">
                            <h2>Share this page on....</h2>               
								<ul>
                                  <li><a href="#" onclick="showTwitterLogin(); return false;"><img src="<% = Global.globalSiteImagesPath %>/twShare.jpg" /></a></li>
                                  <li><% Response.Write("<a href=\"http://www.facebook.com/share.php?u="+urls+"&t="+title+"\" target=\"_blank\"><img src=\"" + Global.globalSiteImagesPath + "/fbShare.jpg\" /></a>");%></li>
                                  <li><%Response.Write("<a href=\"http://www.myspace.com/Modules/PostTo/Pages/?l=3&u="+urls+"&t="+title+"&c="+content+"\" target=\"_blank\"><img src=\"" + Global.globalSiteImagesPath + "/MSshare.jpg\" width=\"65\" height=\"57\" /></a>"); %></li>
                                </ul>
                </div> 
            </div>
            <div id="twitterLogin" class="mbHidden">
            	<div class="titleShare"><h1>Share on Twitter</h1></div>
                	<div class="mainformShare">
                    	<div><input type="text" size="35" value="Username" name="twitterUsername" id="twitterUsername"/></div>
                        <div><input type="password" size="35" value="Password" name="twitterPassword" id="twitterPassword"/></div>
                        <% Response.Write("<input name=\"hiddenContent\" id=\"hiddenContent\" type=\"hidden\" value=\"" + sContent + " " + urls + "\"/>"); %>
                    </div>
                    <div class="controlShare" id="nTwitterCharger" name="nTwitterCharger">
                    	<a href="#" onclick="shareOnTwitter(); return false;"><img src="<% = Global.globalSiteImagesPath %>/share-this_on_Twitter.jpg"/> </a>
                    </div>
                    <div class="controlShare" id="twitterCharger" name="twitterCharger" style="display:none;">
                    	<img src="<% = Global.globalSiteImagesPath %>/loadinfo.gif" height="21"/>
                    </div>
                    <div class="clear"></div> 
                    <div class="returnMain"><a href="#" onclick="showShareThis();"> < Back</a></div> 
                   <div class="clear"></div>        
            </div>          
            <div id="twitterError" class="mbHidden">
            	<div class="titleShare"><h1>Share on Twitter</h1></div>
                    	<div class="labelMessage">
                        	<p class="messageError1">The page cannot be share on twitter. Please check your username and password.</p>
                        </div>
                        <div class="clear"></div> 
                    <div class="returnMain"><a href="#" onclick="showShareThis();"> < Back</a></div> 
                   <div class="clear"></div>        
            </div>   
            <div id="twitterFinish" class="mbHidden"> 
            	<div class="titleShare"><h1>Share on Twitter</h1></div>
					<div class="contMessageVerif">                		
                    	<div class="imgVerif"><img src="<% = Global.globalSiteImagesPath %>/share-this_flecha.gif" width="24" height="20"/> </div>
                            <div class="labelVerif">
                                <p>This page has been shared on Twitter</p>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="clear"></div> 
                    <div class="returnMain"><a href="#" onclick="showShareThis();"> < Back</a></div> 
                   <div class="clear"></div>        
            </div>   
</div>                
</body>

</html>
