<%@ Control Language="C#" AutoEventWireup="true" CodeFile="thankyou.ascx.cs" Inherits="CheckoutStep_ThankYou" %>


    <script language="javascript"  type="text/javascript">
        function hideModal()
        {
             document.getElementById('mbThanksSumary').style.visibility = 'hidden';
        }
      </script>

<div class="boxItemForPurch">
    <div class="boxItemMain1">                  
            <h1>Thank You</h1><br />            
            <asp:Panel ID="ThankYouMessagePanel" runat="server">
                <p>Thank you!  We will process your order as soon as possible.  We will be in contact with you shortly if we need any additional information.  We appreciate your business!</p>
                <br />
                <h2>When referring to your order please use Reference Number: <%Response.Write(orderid); %></h2>
            </asp:Panel>

	    <br/>
            <asp:Panel ID="CreateAccountPanel" CssClass="boxCreateAccount" Visible="false" runat="server">
	           <a href="../../../newAccount.aspx" style="display:block; margin:130px 0 0 26px"><img src="<% = Global.globalSiteImagesPath %>/buttonYes.png" width="70" height="26"/></a>
           </asp:Panel>
            
            <div class="clear"></div>
             <asp:Panel ID="StudentVerificationPanel" CssClass="boxVerification" Visible="false" runat="server">                   
            <div class="topVerification"></div>
            <div class="mainVerification">
                <div class="contMainVerification">                        	
                    <h1>Academic Proof Required</h1><br />
<p>In order to provide these titles at such low prices, we must verify academic eligibility.  Please see <a target="_blank" href="http://doc.edresources.com/academic_proof.pdf">What documentation is needed to receive Academic Discounts for specific requirements</a>.  An Customer Service representative will be getting in touch with you shortly.  </p>
                </div>
            </div>
            <div class="bottonVerification"></div>
        </asp:Panel>
    </div>
</div>



<div class="dividerLineBox"></div>
<div class="boxItemMain2">
    <div class="btnContinueShop">
        <asp:LinkButton ID="ContinueButton" Text="ContinueShopping" Visible="False" Enabled="False" OnClick="ContinueButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonContinueShopping.jpg" width="130" height="26" /></asp:LinkButton>
	<a href="home.aspx"><img src="<% = Global.globalSiteImagesPath %>/buttonContinueShopping.jpg" width="130" height="26" /></a>
    </div>
</div>

		   <div id="mbThanksSumary" style="visibility:hidden; position:absolute; top:40%; left:35%"> <!-- class="mbHidden" -->
                <div class="quote">
                    <div class="quoteTop">
                        <div class="popTitle">Thank You!</div>
                        <div class="popClose" onclick="hideModal();" ></div>
                    </div>
                    <div class="quoteBody">
                        <div><h1>Thank You</h1><br /><p>The Order #<%Response.Write(orderid); %> has been submitted</p></div>
                        <div style="float:right; padding-top:50px;"><a href="#"><img src="<% = Global.globalSiteImagesPath %>/buttonContinue.jpg" onclick="hideModal()"></a></div>
                        <div class="clear"></div>
                    </div>
                    <div class="quoteTButt">
                    </div>
                </div>
            </div>
       