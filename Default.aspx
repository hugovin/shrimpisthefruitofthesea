<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/global.css"/>
    <link rel="stylesheet" type="text/css" href="<% = Global.globalSiteStylePath %>/mootabs1.2.css"/>    
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
        	<div class="logo"></div>
            <div class="login">
            	<div class="loginNav">                    
                	<ul>
                    	<li>Welcome <strong>Jack Frost</strong>  (<a href="#">not you?</a>)&nbsp;&nbsp;  </li>
                        <li><a href="#">  My Account</a></li>
                        <li><a href="#">Wish List</a></li>
                        <li class="last" style="background-image:none;"><a href="#" >View Cart</a></li>
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
                  <input name="topSearch" type="text" id="topSearch" value="" size="30" > 
              </div>
                <div class="advSearch">
                	<a href="#"><strong>+</strong> Advanced Search</a>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <div id="topNav">
        	<div id="topLeft">
            	<div id="topRight">
               	  <ul>
                    <li ><a href="#"class="current">Educators</a></li>                                 
                    <li><a href="#">Schools</a></li>
                    <li><a href="#">Parents</a></li>
                    <li><a href="#">Students</a></li>
                    <li ><a href="#"class="request">Request a Quote</a></li>
            	  </ul>
            	</div>
            </div>          
		</div>
        <div id="bread">
        	<div class="breadAdd"><img src="<% = Global.globalSiteImagesPath %>/pig.jpg" width="462" height="51" />          </div>
          <div class="breadDivider"></div>
            <div class="breadAdd">
            	<img src="<% = Global.globalSiteImagesPath %>/moneyBuble.jpg" width="390" height="50" />          </div>
      </div>
        <div id="mainCont">
        	<div id="cont">
              <div id="slideshow">    
      			<span id="loading">Loading</span>   
                  <ul id="pictures">
                    <li><img src="<% = Global.globalSiteImagesPath %>/img01.jpg" alt="Melbourne" title="Melbourne" /></li>
                    <li><img src="<% = Global.globalSiteImagesPath %>/img02.jpg" alt="Buenos Aires" title="Buenos Aires" /></li>
                    <li><img src="<% = Global.globalSiteImagesPath %>/img03.jpg" alt="Urubamba" title="Urubamba" /></li>
                    <li><img src="<% = Global.globalSiteImagesPath %>/img04.jpg" alt="London" title="London" /></li>
                    <li><img src="<% = Global.globalSiteImagesPath %>/img05.jpg" alt="Venice" title="Venice" /></li>        
                  </ul>      
                  <ul id="menu">
                    <li><a href="<% = Global.globalSiteImagesPath %>/cities/melbourne.jpg">Melbourne</a></li>
                    <li><a href="<% = Global.globalSiteImagesPath %>/cities/buenos_aires.jpg">Buenos Aires</a></li>
                    <li><a href="<% = Global.globalSiteImagesPath %>/cities/urubamba.jpg">Urubamba</a></li>
                    <li><a href="<% = Global.globalSiteImagesPath %>/cities/london.jpg">London</a></li>
                    <li><a href="<% = Global.globalSiteImagesPath %>/cities/venice.jpg">Venice</a></li>
                  </ul>
                  <input style="display:none" type="checkbox" name="auto" checked="checked" id="option-auto" />
       		  </div>             	     
              <div id="demo_block">
                    <h4>What’s New</h4>
                    <div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct1.jpg" width="106" height="145" />
                            <h2> <a href="·">some title product here on two lines</a></h2>
                          <p>Amatemn ollo oa áva, occo</p>
                      </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct2.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                          <p>Aman omanollo oa áva, occo</p>
                      </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct3.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Aman olemelo oa áva, occo</p>
                      </div>
                        <div class="seeMore"><a href="#">+ see more</a></div>
                    </div>             
                    <h4>Featured Products</h4>
                    <div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct2.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Aman ollo oa áva, oloemcco</p>
                        </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct3.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Aman olmarenlo oa áva, occo</p>
                        </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct2.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Ammama an ollo oa áva, occo</p>
                        </div>
                        <div class="seeMore"><a href="#">+ see more</a></div>
                    </div>             
                    <h4 title="Here is another tab!">Best Sellers</h4>
                    <div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct1.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Aman ollo oa áva, ocxxco</p>
                        </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct2.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Amxan ollo oxa áva, occo</p>
                        </div>
                        <div class="tabDivider"></div>
                        <div class="product">
                            <img src="<% = Global.globalSiteImagesPath %>/imgProduct3.jpg" />
                            <h2> <a href="#">some title product here on two lines</a></h2>
                            <p>Axman oxllo oa áva, occo</p>
                        </div>
                        <div class="seeMore"><a href="#">+ see more</a></div>
                    </div> 
			   </div>                
              <div class="box">
                	<div class="blueBox">                    	
                    </div>
                    <div class="helpBox">                    	
                    </div>
                    <div class="greenBox">                    	
                    </div>
              </div>
              <div class="carrousell"><div id="MooFlow">
    <div><img src="<% = Global.globalSiteImagesPath %>/adobe.jpg" title="Adobe" alt="Adobe" /></div>
    <div><img src="<% = Global.globalSiteImagesPath %>/carel.jpg" title="COREL" alt="COREL" /></div>
    <div><img src="<% = Global.globalSiteImagesPath %>/sunbusrst.jpg" title="SUNBURST" alt="SUNBURST" /></div>
    <div><img src="<% = Global.globalSiteImagesPath %>/riverdeep.jpg" title="Riverdeep" alt="Riverdeep" /></div>
    <div><img src="<% = Global.globalSiteImagesPath %>/symantec.jpg" title="symantec" alt="symantec" /></div>
  </div></div>
            </div>
            <div id="sideBar">
            	<div  id="advanceMed">
                	<div class="searchTop" id="link">Product Finder</div>
                    <div class="searchPop"  style=" padding-top:5px; text-align:center;">
                   	<div class="cajaArriba">
                    	<div class="cajaFlecha">
                        	 <div id="advanced" class= "estiloFlecha" style="background-image:url(<% = Global.globalSiteImagesPath %>/cer.gif); background-repeat:no-repeat;" >
                            
                            </div> 
                        </div> 
                    	 
                    </div>
                    <div class="advancemedio advanceMed" id="advanceMed" style=" padding:0px; text-align:center;">
                    	<form onsubmit="return validaVacioAdvanced(this)" method="POST" name="avanzado" id="avanzado"> 
                        	<input name="advanced" type="text" id="advanced" value="Keyword / Item #" size="20" onclick="javascript:document.avanzado.advanced.value=''"> 
                    	<div style="padding:0px 10px; margin:10px 0px 10px 0px;"> Consectetuer adoòscomg elit sed diam nonummy nibh .</div>
                        <div id="refineSearch"  style="position:absolute; overflow:hidden; visibility:hidden; ">
                          <select style="width:150px; margin:2px;"><option selected>Subject / Skills</option><option>Subject 1</option><option>Subject 2</option></select><br />
                          <select style="width:150px; margin:2px;"><option selected>Teaching</option><option>Teaching 1</option><option>Teaching 2</option></select><br />
                          <select style="width:150px; margin:2px;"><option selected>Grades</option><option>Grades 1</option><option>Grades 2</option></select><br />
                          <select style="width:150px; margin:2px;"><option selected>Platform</option><option>Platform 1</option><option>Platform 2</option></select><br />
                          <select style="width:150px; margin:2px;"><option selected>Publisher</option><option>Publisher 1</option><option>Publisher 2</option></select><br />
                             
                            <input   type="button" id="refine-search"  name="parents-btn" value="Search" width="70"  style="cursor:pointer;" onClick="location.href = 'subject-results.html'" />
                        </div>
                        <div id="search" style="position:static; overflow:hidden;">
                            
                            <input   type="button"   name="link2" value="Refine Search" width="70"  height="17" onclick="changeimage('advanced');" />
                        </div>
                        </form>
                    </div>
                    <div class="cajaAbajo advanceBot">
                    	
                    </div>	
                    </div>
                </div>
            	<div id="accordion2">
	<dl class="accordion2" id="slider2">		
	  <dt>Subjects</dt>
		<dd>
			<ul> 
                <li><a href="#">Gallery</a> 
                    <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li>  
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
</ul>
		</dd>
		<dt>Browse</dt>
		<dd>
			<ul> 
                <li><a href="#">Gallery</a> 
                    <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li>  
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
</ul>
	  </dd>
        <dt>Resource Center</dt>
		<dd>
			<ul> 
                <li><a href="#">Gallery</a> 
                    <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li>  
                <li><a href="#">Gallery</a> 
                  <div id="navBox">
            	<div class="top"></div>
                <div class="middle">
          <ul> 
                            <li><a href="#">Gallery 1</a></li> 
                            <li><a href="#">Gallery 2</a></li> 
                            <li><a href="#">Gallery 3</a></li> 
                            <li><a href="#">Gallery 4</a></li> 
                  </ul>				
      </div>
                <div class="bottom">                    
                  </div>
            </div> 
                </li> 
</ul>
		</dd>
  </dl>
  <script type="text/javascript">



var slider2=new accordion.slider("slider2");
slider2.init("slider2",3,"open");

</script>
</div>
			<div class="register">
           	  <p>Lorem ipsum dolor sit adipiscing elit.</p>
                <input name="register" type="text" id="register" value="" size="20" onclick="#">
                <a href="#"><img src="<% = Global.globalSiteImagesPath %>/signup.jpg" border="0" /></a></div>
            </div>
            
            <div class="clear"></div>
        </div><div class="clear"></div>
        <div id="footer">
        	<div id="footNav">
        	<div id="footLeft">
           	  <div id="footRight">
               	  <div class="italicFooter"><em>  providing software, hardware and supplemental learning solutions.</em></div>
                <div class="share"><a href="#"><img src="<% = Global.globalSiteImagesPath %>/footBluePlus.jpg" width="11" height="11" /> Share</a></div>
           	  </div>
            </div>          
		</div>
       		<div class="footCont">
        	<div class="footList"><img src="<% = Global.globalSiteImagesPath %>/usaFlag.jpg" width="78" height="36" /><img src="<% = Global.globalSiteImagesPath %>/candFlag.jpg" width="88" height="36" />
              <h3>Educational Resources</h3>
                    <p>1550 Executive Drive<br />
                    P.O. Box 1900 <br />
                    Elgin, IL  60121-1900 </p>
                    <h3>Email</h3>
                    <p><span>Customer Service:</span> <a href="mailto:custerv@edresources.com">custerv@edresources.com</a><br />
                      <span>Sales:</span> <a href="mailto:sales@edresources.com">sales@edresources.com</a></p>
          </div>
            <div class="footList">
              <h3>Resources</h3>
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
              <h3>About ER</h3>
                    <ul>
                    <li><a href="#">About Us</a></li>
                    <li><a href="#">Contact Us</a></li>
                    <li><a href="#">Employment</a></li>
                    <li><a href="#">Mission</a></li>
                    <li><a href="#">Partners</a></li>
                    <li><a href="#">Resellers</a></li>
              </ul>
          </div>
            <div class="footList" style="background-image:none;">
              <h2>Register Now!</h2>
              <h4>Sign up today to stay informed.</h4>
              <p>Lorem ipsum dolor sit amet, consectetur <br />adipisicing elit, </p>
                    <input name="register" type="text" id="register" value="" size="32" onclick="#"><br />
                    <a href="#"><img src="<% = Global.globalSiteImagesPath %>/footersignUp.jpg" alt="sign up" border="0" /></a> </div>
          <div class="clear"></div>
          <div class="footBL">All Content © copyright 2009 ER.com, All Rights Reserved</div>
          <div class="footBR">
          	<h1>1 (800) 860-7004  Toll Free</h1>
                <ul>
                <li><a href="#">Site Map</a>  | </li> 
                <li><a href="#">Privacy Policy</a>   |</li>   
                <li><a href="#">Terms of Use</a></li>
                </ul>
          </div>
        </div>
        	
        </div>
    </div>
</body>
</html>
