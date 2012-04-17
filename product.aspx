<%@ Page Language="C#" MasterPageFile="~/Product.master" AutoEventWireup="true" CodeFile="product.aspx.cs"
    Inherits="product" Title="Untitled Page" %>


<%@ Reference Control="~/boxContact.ascx" %>
<%@ Reference Control="~/uc_RelatedProducts.ascx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" runat="Server">
<!-- Cart Code -->
    <script type="text/javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/utils.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/cart.js"></script>
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/TorchConfig.js"></script>
    
<!-- Cart Code -->    
<script language="javascript" type="text/javascript">

            window.addEvents({
                'domready': function(){
                    /* thumbnails example , div containers */
                    new SlideItMoo({
                                overallContainer: 'SlideItMoo_outer',
                                elementScrolled: 'SlideItMoo_inner',
                                thumbsContainer: 'SlideItMoo_items',		
                                itemsVisible:<%=_numRelatedSlide %>,
                                itemsSelector: '.SlideItMoo_element',
                                itemWidth: 165,
                                showControls:1});
                },
                'load': function(){
                    /* banner rotator example */	
                    new SlideItMoo({overallContainer: 'SlideItMoo_banners_outer',
                                    elementScrolled: 'SlideItMoo_banners_inner',
                                    thumbsContainer: 'SlideItMoo_banners_items',		
                                    itemsVisible:<%=_numRelatedSlide %>,
                                    itemsSelector: '.banner',
                                    showControls:0,
                                    autoSlide: 3000,
                                    transition: Fx.Transitions.Bounce.easeOut,
                                    duration: 1800,
                                    direction:-1});
                                    
                    /* info rotator example */	
                    
                }
            });
            function changeMasterImage(MasterImagePath, BigImagePath){
 	            var MasterIMG=document.getElementById('images');
	            var BigImg=document.getElementById('Img1');
	            MasterIMG.src=MasterImagePath;
	            BigImg.src=BigImagePath;
            }
            try{
                function selectlock(id){
                try{
                        document.getElementById('lockbig').style.background=document.getElementById(id).style.background;
                }catch(err2){
                    //examine this one. Don't know what it should do.
                }
                
                }
            }catch(err){
                //already defined. 
            }
            
        </script>
           
    <script type="text/javascript" language="javascript">
    function gotomarc(marcador){
    
    if (marcador == '1')
    window.location.href='#LicenseOptions';
    if (marcador == '2')
    window.location.href='#marcResources';
    if (marcador == '3')
    window.location.href='#marcSystem';
    if (marcador == '4')
    window.location.href='#marcReview';
    if (marcador == '5')
    window.location.href='#marcSimpro';
    if (marcador == '6')
    window.location.href='#marcFunding';
    if (marcador == '7')
    window.location.href='#marcAdditional';
    }
    
    </script>
    <%
        Response.Write("<script language='javascript' type='text/javascript'>");
        Response.Write("    function quoteThisProduct(){");
        Response.Write("           var productTitle=\"" + _Title+ "\";");
        Response.Write("           var quantity = document.getElementById('qtyProduct').value;");
        Response.Write("           location.href=\"requestaquote.aspx?title=\"+productTitle+\"&qty=\"+quantity;");
        Response.Write("    }");
        Response.Write("</script>");
         %>
    <script language="javascript" type="text/javascript">
        function notAllowNumbers(id){
            var box = document.getElementById(id);
            if(isNaN(box.value)){
                box.value = "1";
            }
            
        }
    </script>
    <script src="js/centerImages.js" type="text/javascript"></script>
    <script src="js/downloadtrials.js" type="text/javascript"></script>
    <div id="print">
    <div class="prodDetLeft">
        <div class="prodDet">
            <div class="prodDetLeft">
                <div class="prodSlide">
                    <div id="gallerywrapper">
                    <div style="padding-bottom:40px;">
                        <div id="defaultimage">
                            <!-- <a id="linkDefaultimage" href="#" onclick="document.getElementById('bigimage').style.visibility='visible';">-->
                                <div id="boxContImage" style="<% ="width: 280px; height: 280px;"%>">
                                    <img id="images" style="width: 280px; height:280px;" src="<% = strFolder + _Imagetn  %>" title="<%= _Pubname%> : <%=_Title %>"/><!--onload="
                                    (document.getElementById('boxContImage'),this)" -->
                                </div>
                            <!-- </a>-->
                            <br />
                           <!-- <a id="imgZoom" href="#" onclick="document.getElementById('bigimage').style.visibility='visible';">
                                <img src="images/zoom.jpg" class="zoom" width="55" height="15" border="0" /></a>-->
                                
                           <!-- <script language="JavaScript">
                                var m = document.getElementById("imgZoom").complete;
                                if (m != true) {
                                    alert("The image is not completely loaded")
		                             document.getElementById("imgZoom").style.display='none';
		                             document.getElementById("linkDefaultimage").style.visibility='hidden';
		                             
                                }
                            </script>    -->
                                
                                
                                
                            <br /><br />
                            </div>
                            <div style="width: 280px; display: table;">
                                <asp:PlaceHolder ID="PlaceHolder_ProImages" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                        <div id="bigimage" style="position: absolute; z-index: 2; background: white; visibility: hidden; left: 283px; top: 60px;">
                 <div class="topProductPOP">
                    <div class="closeProductPOP" onclick="document.getElementById('bigimage').style.visibility='hidden'"></div>
                </div>
                <div class="contProductPOP">
                    <div id="mainProductPOP" style="width:280px; height:280px; padding:26px;">
                       <img id="Img1" style="width:280px; height:280px;" src="<% = strFolder + _Imagetn  %>" alt="<% =_Title %>" /> <!-- onload="getDim(document.getElementById('mainProductPOP'),this)" -->
                    </div>
                                </div>
                <div class="bottonProductPOP"></div>
        </div>

                    </div>
                </div>
                <div class="prodInfoDesc">
                    <h1>
                        <% =_Title %></h1>
                    <h2>
                            <%=_Version %> <%if( AdditionalLic > 1){ %><a href="#LicenseOptions">See Additional Licensing Options</a><%} %>
                            </h2>
                    <p>
                        Item #
                        <%=_Sku %></p>
                    <p>
                        <em>by</em>: <a href="result.aspx?findopt5=<%=_Pubid %>&am=1&asm=3">
                            <%=_Pubname %></a></p>
                    <h3>
                        Grade Levels: <span>
                            <%=_Grades %></span></h3>
                    <div class="clear"></div>
                    <div class="prodInfo2"></div>
                        <div class="prodInfoL">
                            <p>
                                <%if (_Plat_mac_flag == "1"){%><%=_Mac%><%} %>
                                <%if (_Plat_mac_flag == "1" && _Plat_win_flag == "1")
                                  {%>/<%} %>
                                <%if (_Plat_win_flag == "1")
                                  {%><%=_Win%><%} %></p>
                            <h4>
                            <%if (_TitleId != Resources.Resource.TorchProductId)
                              {%>
                                List Price: $<%=_Srp%>
                                <%} %>
                            </h4>
                            <% if (Convert.ToDouble(_Yousave) > 0)
                               {%>
                               <h2>
                                $<%=_Er_price%></h2>
                            <h3>
                                YOUR DISCOUNTED PRICE</h3>
                            <h4>
                                You Save: $<%=_Yousave%></h4>
                            <% }else
                               {%>
                                    
                              <%if (_TitleId != Resources.Resource.TorchProductId)
                                {%>
                                <h6>$<%=_Er_price%></h6>
                                <%}
                                else
                                { %>
                                    <h6><span id="conf_price">Setup Your Torch <img src="images/configButton.jpg" onclick="javascript:gotomarc(1);" class="confButton" /></span></h6>
                                <%} %>
                            <% }%>
                            <p>
                            </p>
                            <p>
                                <input id="qtyProduct" name="qtyProduct" type="text" value="1" width="35px" size="3" maxlength="4" onblur="notAllowNumbers('qtyProduct');"/>
                                Qty</p>
                            <div class="leftBtn">
                                 <%if (_TitleId != Resources.Resource.TorchProductId)
                                   {%>
                                <a href="#" id="A2" class="mb" title="" rel="type:element">
                                    
                                    <%=Cart.CreateAddToCartLinkProduct("<img src=\"images/addCarDet.jpg\" width=\"118\" height=\"31\" />", _TitleId, _Sku, 1, productNameToCart)%>
                                </a>
                                <%} %>
                                
                                
                                </div>
                            <div class="rigthBtn">
                                <%if ((bool)Session[SiteConstants.UserValidLogin])
                                  { %>
                                    <a href="addWish.aspx?p=<% =_TitleId%>&sk=<% =_Sku%>&skd=<% =_Skudesc%>" rel="width:580,height:131,ajax:true" id="mb10"
                                        class="mb" title="Add Product"><img src="images/addWishDet.jpg"/></a> 
                                <%}else{ %>                                
                                    <a href="#htmlElement" id="mb15" class="mb" title="" rel="type:element"><img src="images/addWishDet.jpg"></a>
                               <%} %>
                                <!--<a href="requestaquote.aspx?title=<% =_Title%>"><img src="images/quoteDet.jpg"/></a>-->
                                <a href="#" onclick="quoteThisProduct(); return false;"><img src="images/quoteDet.jpg"/></a>
                                
                            </div>
                            <% if (_Student_Pricing_Flag == "1") { %>
                            <p style="color:RED"><b>Academic Proof Required</b></p>
                            <% }%>
                            <% if (_Price_Rule == "AGENT")
                               { %>
                            <p style="color:RED"><b>Cannot discount due to contractual obligations.</b></p>
                            <% }%>
                            <div class="clear"></div>
                        </div>
                        
                        <div class="prodInfoR">
                            <%
                                if (_Trial_Flag== "1")
                              {
                                  if (!(((bool)Session[SiteConstants.UserValidLogin]) || (productTrialValidated == "")))
                                  {%>
                                    <a href="#htmlElement" id="A3" class="mb" rel="type:element"><img src="images/dowloadBtn.jpg" width="195" height="55" border="0" /></a>
                            <% 
                                }
                                  else { 
                                  %>
                                    <%Response.Write("<a href=\"" + "#" + "\" onClick=\"downloadTrial(" + productId + "); return false;\" title=\"Download the Trial\">"); %><img src="images/dowloadBtn.jpg" width="195" height="55" border="0" /></a>
                                  <%
                                  }
                              }%>
                            <%
                                if (_Demo_Flag== "1")
                              {
                                  if (!((bool)Session[SiteConstants.UserValidLogin]))
                                  {%>
                            <a href="#htmlElement" id="mb15" class="mb" rel="type:element"><img src="images/demoBtn.jpg" width="195" height="54" border="0" /></a>
                            <% }
                                  else { 
                                  %>
                                  <%Response.Write("<a href=\"getResourceVideo.aspx?pid="+productId+"\" rel=\"width:800,height:620,ajax:true\" id=\"mb10\" class=\"mb\" title=\"See Video\">"); %><img src="images/demoBtn.jpg" width="195" height="55" border="0" /></a>
                                  <%
                                  }
                              }%>
                        </div>
                    <div class="clear"></div>
                </div>
                <div class="prodDetBarX">
                    <div class="prodDetBarR">
                        <div class="prodDetBarL">
                            <p>
                                <strong>Jump to:</strong><img src="images/prodBarSeparator.jpg" />
                                <select name="JumpTo" onchange="gotomarc(this.value);" style="background:#ccc; color:#000; height:20px; border:0;" width:100px;>
                                    <option value="0" id="opt_des" runat="server"> Description</option>
                                    <option value="1" id="opt_lic" runat="server"> License options</option>
                                    <option value="2" id="opt_res" runat="server"> Resources</option>
                                    <option value="3" id="opt_sys" runat="server"> System Requirements</option>
                                    <option value="4" id="opt_pro" runat="server"> Product Review</option>
                                    <option value="5" id="opt_sim" runat="server"> Similar Products</option>
                                    <option value="6" id="opt_fun" runat="server"> Funding</option>
                                    <option value="7" id="opt_add" runat="server"> Additional Information</option>
                                </select>
                            </p>
                        </div>
                    </div>
                    <div class="prodBarButt">
                    </div>
                </div>
                <div id="accordion4"><br /><br/>
                    <dl class="accordion4" id="slider4"><div class="bullets">
                        <% if (_Short_description != ""){ %>
                        <dt>
                            <strong>Description</strong><br /><br />
                                <%=_Short_description %><br />
                            <span><br />+more</span>
                        </dt>
                        <dd>
                           <p><%=_Long_description %></p><br />
                        </dd>
                        <%} %>
                        <dt>
                            <div id="LicOpt" runat="server">
                            <%if (_TitleId != Resources.Resource.TorchProductId)
                              { %>
                             <a name="LicenseOptions" id="LicenseOptions"></a>
                             <hr /><br /> <strong> License options</strong><br /><br />
                                
                                <div id="prodDetBox">
                                    <div id="prodDetBoxTop">
                                        <div id="prodDetBoxBut">
                                            <div class="formAccordion">
                                                <div class="formSKU">
                                                    <p>
                                                        <b>SKU</b></p>
                                                </div>
                                                <div class="formDesc1">
                                                    <p>
                                                        <b>Description</b></p>
                                                </div>
                                                <div class="formQty1">
                                                    <p>
                                                        <b>Platform</b></p>
                                                </div>
                                                <div class="formVersion">
                                                    <p>
                                                        <b>Version</b></p>
                                                </div>
                                                <div class="formUnit">
                                                    <p>
                                                        <b>Unit Price</b></p>
                                                </div>
                                                <div class="formTotal">
                                                    <p>
                                                        &nbsp;</p>
                                                </div>
                                            </div>
                                            <asp:PlaceHolder ID="PlaceHolder_LicOptions" runat="server"></asp:PlaceHolder>
                                            <div style="clear: both">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%}
                              else
                              { %>
                             <a name="LicenseOptions" id="A1"></a>
                             <hr /><br /> <strong> Configure your torch</strong><br /><br />
                               <form id="configurator" runat="server">
    <div class="configureTorchDiv">
        Choose your State:
        <select id="States" class="selectState">
            <option value="0">Select State</option>
            <option value="AL">Alabama</option>
            <option value="AK">Alaska</option>
            <option value="AZ">Arizona</option>
            <option value="AR">Arkansas</option>
            <option value="CA">California</option>
            <option value="CO">Colorado</option>
            <option value="CT">Connecticut</option>
            <option value="DC">D.C.</option>
            <option value="DE">Delaware</option>
            <option value="FL">Florida</option>
            <option value="GA">Georgia</option>
            <option value="HI">Hawaii</option>
            <option value="ID">Idaho</option>
            <option value="IL">Illinois</option>
            <option value="IN">Indiana</option>
            <option value="IA">Iowa</option>
            <option value="KS">Kansas</option>
            <option value="KY">Kentucky</option>
            <option value="LA">Louisiana</option>
            <option value="ME">Maine</option>
            <option value="MD">Maryland</option>
            <option value="MA">Massachusetts</option>
            <option value="MI">Michigan</option>
            <option value="MN">Minnesota</option>
            <option value="MS">Mississippi</option>
            <option value="MO">Missouri</option>
            <option value="MT">Montana</option>
            <option value="NE">Nebraska</option>
            <option value="NV">Nevada</option>
            <option value="NH">New Hampshire</option>
            <option value="NJ">New Jersey</option>
            <option value="NM">New Mexico</option>
            <option value="NY">New York</option>
            <option value="NC">North Carolina</option>
            <option value="ND">North Dakota</option>
            <option value="OH">Ohio</option>
            <option value="OK">Oklahoma</option>
            <option value="OR">Oregon</option>
            <option value="PA">Pennsylvania</option>
            <option value="RI">Rhode Island</option>
            <option value="SC">South Carolina</option>
            <option value="SD">South Dakota</option>
            <option value="TN">Tennessee</option>
            <option value="TX">Texas</option>
            <option value="UT">Utah</option>
            <option value="VT">Vermont</option>
            <option value="VA">Virginia</option>
            <option value="WA">Washington</option>
            <option value="WV">West Virginia</option>
            <option value="WI">Wisconsin</option>
            <option value="WY">Wyoming</option>
        </select><br />
        <div id="grade">
            <div id="gradedp" class="gradedp">
            <label>
                Select Grades:
            </label>
            <select name="GRADE" id="GRADE" onchange="SelectGrade();"  class="selectGrade">
                <option selected value="">Select Grade</option>
                <option value="2">Grades 4-8</option>
                <option value="1">Grade 6</option>
                <option value="1">Grade 7</option>
                <option value="1">Grade 8</option>
            </select>
            <a id="explain-grades" href="#" class="Tips3">
                <img id="explain-grades" src="images/icoHint.png" alt="Help" onmouseover="displayTooltip();" onmouseout="hideTooltip();" /></a>
           </div>
            <div class="tooltiphide" id="tooltip">
                Only 4 - 8 can come in an "All Subjects" configuration. You must select
                a single subject for grades 6, 7, and 8.
            </div>
            <!-- the div with a class of 'tooltip' must immediately follow the triggering anchor tag. -->
        </div>
        <div style="clear:both"></div>
        <div id="subject">
            <label>
                Select Subject:
            </label>
            <div id="SUBJECT" class="radioSubject">
                <input id="SUBJECT_ALL" name="SUBJECT" type="radio" value="2" onclick="SelectSubject();" /><span id="ALL">All Subjects</span>
                <input id="SUBJECT_MTH" name="SUBJECT" type="radio" value="1" onclick="SelectSubject();"/><span id="MTH">Math</span>
                <input id="SUBJECT_SCI" name="SUBJECT" type="radio" value="1" onclick="SelectSubject();"/><span id="SCI">Science</span>
                <input id="SUBJECT_SOC" name="SUBJECT" type="radio" value="1" onclick="SelectSubject();"/><span id="SOC">Social
                    Studies</span>
            </div>
            <input type="hidden" id="newSku" />
        </div>
        <strong>Additional Features</strong>
        <div id="edition">
            <input id="ELL_EDITION" name="ELL_EDITION" value="YES" type="checkbox" onchange="ELLSelected()" />
            <strong style="color: orange;">New!</strong> Spanish Heritage ELL Edition <a href="http://doc.sunburst.com/sb_torch_ell.pdf"
                target="_blank" style="font-size: 12px; color: #333;" target="_blank">Learn More</a>
        </div>
        <br style="clear: both;" />
        <div id="interactivity">
            <div id="whiteboard">
                <label>
                    Do you use an interactive whiteboard?</label>
                <input id="WHITEBRD" name="WHITEBRD" type="radio" value="YES" onclick="whiteBoardYes();" />Yes
                <input id="WHITEBRD" name="WHITEBRD" type="radio" value="NO" onclick="whiteBoardNo();" />No
                <input id="WHITEBRD" name="WHITEBRD" type="radio" value="NS" onclick="whiteBoardNo();" />Not Sure
                <br />
                <div id="whiteboard-brand" class="sub-option1">
                    Select brand
                    <select name="WHITEBRD-BRND" id="WHITEBRD-BRND" onchange="WhiteBrand(this);">
                        <option selected value="">Select Brand</option>
                        <option value="PMTH">Promethean</option>
                        <option value="SMRT">SMART</option>
                        <option value="OTHR">Other</option>
                    </select>
                </div>
                <div id="whiteboardbrandother" class="sub-option1">
                    Other:
                    <input type="text" name="WHITEBRD-BRND-OTHR" id="WHITEBRD-BRND-OTHR" value="" />
                </div>
            </div>
            <div id="lrs">
                <label>
                    Do you use a Learner Response System?</label>
                <input id="LRS" name="LRS" type="radio" value="YES" onclick="ResponseYes();" />Yes
                <input id="LRS" name="LRS" type="radio" value="NO" onclick="ResponseNo();" />No
                <input id="LRS" name="LRS" type="radio" value="NS" onclick="ResponseNo();" />Not Sure
                <br />
                <div id="lrs-brand" class="sub-option1">
                    Select brand:
                    <select name="LRS-BRND" id="LRS-BRND" onchange="ResponseSysChoose(this)">
                        <option selected value="">Select Brand</option>
                        <option value="PMTH">Promethean</option>
                        <option value="SMRT">SMART</option>
                        <option value="RENN">Renaissance</option>
                        <option value="EINS">eInstruction</option>
                        <option value="OTHR">Other</option>
                    </select>
                </div>
                <div id="lrsbrandother" class="sub-option1">
                    Other:
                    <input type="text" name="LRS-BRND-OTHR" id="LRS-BRND-OTHR" value="" />
                </div>
            </div>
                                             <%if (_TitleId == Resources.Resource.TorchProductId)
                                   {%>
                                <a href="#" id="A4" class="mb" title="" rel="type:element">
                                    
                                    <%=Cart.CreateAddToCartLinkProduct("<img src=\"images/addCarDet.jpg\" class=\"buttonToRight\" width=\"118\" height=\"31\" />", _TitleId, _Sku, 1, productNameToCart)%>
                                    <div style="clear:both"></div>
                                </a>
                                <%} %>
        </div>
    </form>
                                <%} %>
                            </div>
                        </dt>
                        <dd>
                        </dd>
                        <dt>
                            <div id="ProRes1" runat="server">
                                <a name="marcResources" id="marcResources"></a>
                                <hr /><br /><strong>Resources</strong><br /><br />
                                <span>+more</span>
                            </div>
                        </dt>
                        <dd>
                            <div id="ProRes2" runat="server">
                                <asp:PlaceHolder ID="PlaceHolder_ProRes" runat="server"></asp:PlaceHolder>
                            </div>
                        </dd>
                        <dt>
                            <div id="SysReq1" runat="server">
                                    <a name="marcSystem" id="marcSystem"></a>
                                <hr /><br /> <strong> System Requirements</strong><br /><br />
                                <span>+more</span>
                            </div>
                        </dt>
                        <dd>
                            <div id="SysReq2" runat="server">
                                
                                <asp:PlaceHolder ID="PlaceHolder_SysReqCPU" runat="server"></asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder_SysReqMem" runat="server"></asp:PlaceHolder>
                                
                            </div>
                        </dd>
                        <dt>
                            <div id="ProRev1" runat="server">
                            <a name="marcReview" id="marcReview"></a>
                                <hr /><br /> <strong>Product Review</strong><br /><br />
                                <span>+more</span>
                            </div>
                        </dt>
                        <dd>
                            <div id="ProRev2" runat="server">
                                <asp:PlaceHolder ID="PlaceHolder_Review" runat="server"></asp:PlaceHolder>
                            </div>
                        </dd>
                        <dt>
                            <div id="Funding1" runat="server">
                            <a name="marcFunding" id="marcFunding"></a>
                                <hr /><br /> <strong>Funding & Usage</strong><br /><br />
                                <span>+more</span>
                            </div>
                        </dt>
                        <dd>
                            <div id="Funding2" runat="server">
                                <asp:PlaceHolder ID="PlaceHolder_funding" runat="server"></asp:PlaceHolder>
                            </div>
                        </dd>
                         <dt>
                            <div id="AdditionalInfo1" runat="server">
                            <a name="marcAdditional" id="marcAdditional"></a>
                                <hr /><br /> <strong>Additional Information</strong><br /><br />
                                <span>+more</span>
                            </div>
                        </dt>
                        <dd>
                            <div id="AdditionalInfo2" runat="server">
                                <asp:PlaceHolder ID="PlaceHolder_AdditionalInfo" runat="server"></asp:PlaceHolder>
                            </div>
                        </dd>
                        </div>
                    </dl>

                    <script type="text/javascript">
                var slider4=new accordion.slider("slider4");
                slider4.init("slider4",7,"open");
                    </script>

                </div>
            </div>
        </div>
        <div>
        </div>
        <div>
        </div>
        <div>
        </div>
        <div id="resultControls1">
            <br />
            <br />
            <a name="marcReview" id="marcSimpro"></a>
            <div id="RelPro" runat="server">
                <h2>
                    Similar Products</h2>
                <div id="SlideItMoo_outer">
                    <div id="SlideItMoo_inner">
                        <div id="SlideItMoo_items">
                            <asp:PlaceHolder ID="PlaceHolser_Slide" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div id="sidebar-content">
        <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="PlaceHolder_uc_RelatedProducts" runat="server"></asp:PlaceHolder>
      <asp:PlaceHolder ID="PlaceHolder_uc_Specials" runat="server"></asp:PlaceHolder>
    </div>

    <script type="text/javascript" language="javascript">
        var SkuContainers=new Array();
        <%
            int i = 0;
                foreach(TorchPricesSku sku in _ListOfSkus) {
                Response.Write("SkuContainers["+i.ToString()+"]= {sku:" +sku.Sku+",subjectid:" +sku.Subject+",gradeid:" +sku.Grade+",price:" +sku.Price+"};");
                i++;
                } 
        %>

    </script>



</asp:Content>
