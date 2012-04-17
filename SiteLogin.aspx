<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteLogin.aspx.cs" Inherits="SiteLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<% Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Global.globalSiteStylePath + "/global.css\" />"); %>
    <title>User Login</title>
    
    <!-- Here is calling the ajaxObject again-->
    <script src="js/ajaxObject.js" type="text/javascript"></script>
    <!-- /Here is calling the ajaxObject again-->
    
    <!-- Script -->
    <!--  This script is calling the Ajax Object and Making the validation -->
    <script language="javascript" type="text/javascript">
        //Danilo Ramírez Mattey June 10, 2009
        function validLogin(){
            var storedUser = document.getElementById('lu').value;
            var storedPass = document.getElementById('lp').value;
            document.getElementById('chargingLoginBox').style.display="block";
            document.getElementById('errorBoxLogin').style.display="none";
            //Get the username and password to send it to the validation Page.
            searchAjax = new nuevoAjax();
            searchAjax.open("GET","loginValidation.aspx?username="+storedUser+"&password="+storedPass+"&"+ (new Date()).getTime(),true);
            searchAjax.onreadystatechange=function() {
                if (searchAjax.readyState==4) {
                        
                        result = searchAjax.responseText;
                        result = result.substr(0,4);
                        if(result=="True"){
                            document.getElementById('frm_login').action = parent.location;
                            if(parent.followLink != "" && parent.followLink != null){
                                document.getElementById('frm_login').action = parent.followLink;
                            }
                            parent.followLink = "";
                            document.getElementById('frm_login').submit();
                        }else{
                            document.getElementById('errorBoxLogin').style.display="block";
                            document.getElementById('chargingLoginBox').style.display="none";
                        }
                }
            }
            searchAjax.send(null);
        }
        function getSug(e)
        {
           tecla = document.all ? e.keyCode : e.which; 
           if(tecla == 13){
            validLogin();
            return false;
           }

        }
        function setParent(){
            document.getElementById('frm_login').action = parent.location;
        }
        function falseKey(event){
            tecla = document.all ? e.keyCode : e.which; 
           if(tecla == 13){
            return false;
           }else{
            return true;
           }
           
        }
    </script>
    <!-- /Script -->
</head>
<body style="background-image: none; background-color: #eee" >

    <!-- This is the original form from Home.master-->
    <form id="frm_login" name="frm_login" action="parent.location" method="post" target="_parent" onsubmit="setParent();">
     <div class="contChangePass">
        <div class="boxPassError" style="display:none;" id="errorBoxLogin" name="errorBoxLogin">
            <div class="imgError">
            </div>
            <div class="messageError">
                <p>
                    The e-mail address or password is incorrect. Please try again.
                </p>
            </div>
        </div>
        <div class="boxPassError" style="display:none;" id="chargingLoginBox" name="chargingLoginBox">
            <div class="messageError">
                <img src="<% = Global.globalSiteImagesPath %>/loading.gif" border="0" width="25" alt="Loading" />
            </div>
        </div>
        <div class="formChangesPass">
            <div class="cellChangePass">
                <div class="labelChangePassOK">
                    <p>
                        Email:</p>
                </div>
                <div class="controlChangePass">
                    <input type="text" size="40"  maxlength="100"  name="lu" onkeydown="if(event.keyCode == 13)return false;" onkeyup="getSug(event);" id="lu" style="width:275px !important;"/>
                </div>
            </div>
            <div class="cellChangePass">
                <div class="labelChangePassOK">
                    <p>
                        Password:</p>
                </div>
                <div class="controlChangePass">
                    <input type="password" size="40" maxlength="100" name="lp"  onkeydown="if(event.keyCode == 13)return false;" onkeyup="getSug(event);" id="lp" style="width:275px !important;"/>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="forgotPass"><br />
            <a href="newAccount.aspx" target="_top">Are You a New User?</a><br /><br />
            <a href="ForgotPassword.aspx" target="_top">Forgot your password?</a>
        </div>

    <div class="boxKeepLoged">
        <div class="controlKeep">
            <input type="checkbox" checked="checked" name="kl" /></div>
        <div class="labelKeep">
            <p>
                Keep me logged in</p>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="contControlPassOK">
        <div class="btnControlPass">
            
                <img  src="<% = Global.globalSiteImagesPath %>/buttonLogin.jpg" alt="Login" width="51" height="18" onclick="validLogin();" style="cursor:hand;"/>
        </div>
    </div>
    </form>
    <!-- /This is the original form from Home.master-->
    <script type="text/javascript">
      function setFocusOnLogin() {
      var loginTxt = document.getElementById("lu");
          try{
            loginTxt.focus();
          }catch(err){
          }
      }
      setFocusOnLogin();
    </script>
</body>
</html>
