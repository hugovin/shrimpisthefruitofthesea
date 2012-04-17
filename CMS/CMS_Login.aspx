<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS_Login.aspx.cs" Inherits="CMS_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
<title>CMS - Login</title> 
    <link href="css/style-box.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
</head> 

<body>
    <form id="form1" runat="server">
<div class="content"> 
 
  <div class="login-box"> 
  
   	  <div class="h1"> 
        	<div align="center"> 
            	Login
            </div> 
   	  </div> 
      
      <div class="login-content"> 
            <div  id="login" runat="server">
                </div> 
                
        <div class="forgot-pass" > 
        	<div> 
            <a href="#">Forgot Password?</a> 
            </div> 
        </div> 
        <div> 
          <br /> 
        <br /> 
        <div align="center">
                <asp:ImageButton ID="ImageButton1" ImageUrl="imagesCss/login-btn.jpg" runat="server" Text="Login" onclick="btnLogin_Click"></asp:ImageButton>
        </div> 
        </div>         
    </div> 
     
  </div> 
    
</div>                                                       
   <div  id="Div2" runat="server" visible="false">
       <div id="div3" runat="server"></div>
       <div id="div4" runat="server" visible="false">*Invalid username</div>
       <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSaveNewUser_Click" />
       <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="btnCancel_Click" />  
   </div>
    </form>
</body>
</html>
