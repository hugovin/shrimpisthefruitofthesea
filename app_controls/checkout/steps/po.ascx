<%@ Control Language="C#" AutoEventWireup="true" CodeFile="po.ascx.cs" Inherits="CheckoutStep_PO" %>
 
 <style type="text/css">
    .hidden { display:none; visibility:hidden; }
 </style>
 
 <script type="text/javascript">
    function po_hide_all_options()
    {
        $("uploadpo_container").addClass('hidden');
        $("emailpo_container").addClass('hidden');
        $("printpo_container").addClass('hidden');
    }
    addOnLoad(po_hide_all_options);
    
       
    function show_option(option)
    {
        po_hide_all_options();
        $(option).removeClass('hidden');
    }
    
    function PopupPrintWindow()
    {
       var ponum = PONumberTextbox.value;
       var id = document.getElementById('ERordid').value;
       /* if(Page_ClientValidate())
        {
            popup("print_order.aspx?id="+id+"&po="+ponum+"", "PrintOrder");
        }*/
    }
    
    function PopupEmail()
    {
        if(Page_ClientValidate())
        {
            var ponum = PONumberTextbox.value;
            var id = document.getElementById('ERordid').value;
            var body = "";
            openPopup("mailto:"+po_email+"?subject=ER Order "+ id + " PO&body=" + body, 10, 10);
        }
    }
 </script>
 
<div class="boxItemForPurch">
     
    <div class="boxItemMain1">
        <h1>Purchase Order</h1><br/>        
        <p>Your order has been submitted.  When referring to your order please use Reference Number:  <strong><%Response.Write(OrId); %></strong>.  Since you have selected Purchase Order as your method of payment please upload your PO, send us your PO by email or Fax us your PO.  Please make sure your Purchase Order is on your School or District letterhead.</p>
    </div>
    <input id="ERordid" type="hidden" value="<%Response.Write(OrId);%>" />
    <asp:RequiredFieldValidator ID="PONumberValidator" ControlToValidate="PONumberTextbox" ErrorMessage="You must input a PO Number" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>

    <div>
       <div class="topOptionPO"></div>	
       <div class="mainOptionPO">
            <div class="controlOptionPO">
                <p>1. Enter PO Number:</p><asp:TextBox ID="PONumberTextbox" runat="server"></asp:TextBox> 
            </div>
            
            <div class="controlOptionPO" style="width:500px">
                <p>2. Choose the way you would like to send us your purchase order</p>
            </div>
            <asp:Literal ID="MessageLiteral" runat="server"></asp:Literal>
            <div class="contOptions">
                <div style="background:url(<% = Global.globalSiteImagesPath %>/optionUploadPO.jpg) no-repeat;" class="boxOption">
            	    <a href="#" onclick="show_option('uploadpo_container'); return false"><img src="<% = Global.globalSiteImagesPath %>/optionUploadPOButton.png" alt="Upload PO" width="87" height="24" class="boxControlOption" /></a>
                </div>
                <div style="background:url(<% = Global.globalSiteImagesPath %>/optionUploadEmail.jpg) no-repeat;" class="boxOption">
            	    <a href="#" onclick="show_option('emailpo_container'); return false"><img src="<% = Global.globalSiteImagesPath %>/optionUploadEmailButton.png" alt="Email PO" width="87" height="24" class="boxControlOption"/></a>
                </div>
                <div style="background: url(<% = Global.globalSiteImagesPath %>/optionUploadPrint.jpg) no-repeat;" class="boxOption">
            	    <a href="#" onclick="show_option('printpo_container'); return false"><img src="<% = Global.globalSiteImagesPath %>/optionUploadPrintButton.png" alt="Print PO" width="87" height="24" class="boxControlOption"/></a>
                </div>
                <div class="clear"></div>
            </div>
            
            <div id="uploadpo_container" class="boxItemMain1 container">
                <h1>Upload a PO</h1>
                <p>Valid file types: jpg, tiff, pdf, doc, docx<br />
                <asp:FileUpload ID="POFileUpload" runat="server" /><br /><br />
                <asp:LinkButton ID="UploadPOButton" Text="Upload PO" OnClick="UploadPOButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/optionUploadPOButton.png" alt="Upload PO" width="87" height="24" class="" /></asp:LinkButton></p>
            </div>

            <div id="emailpo_container" class="boxItemMain1 container">
                <h1>Email Your PO</h1>
                <p>When emailing your purchase order please use your Order Reference Number<br /><br />
                <asp:LinkButton ID="EmailPOButton" Text="Email PO" OnClientClick="PopupEmail();" OnClick="EmailPOButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/optionUploadEmailButton.png" alt="Email PO" width="87" height="24" class=""/></asp:LinkButton></p>
            </div>

            <div id="printpo_container" class="boxItemMain1 container">
                <h1>Fax Your Purchase Order</h1>
                <p>Fax your purchase order on your school or district letterhead.  Fax to:  (800) 610-5005<br /><br />
                <asp:LinkButton ID="PrintPOButton" Text="Print PO" OnClientClick="PopupPrintWindow();" OnClick="PrintPOButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/optionUploadPrintButton.png" alt="Print PO" width="87" height="24" class=""/></asp:LinkButton></p>
            </div>
       </div>
       <div class="bottonOptionPO"></div>
    </div>

</div>