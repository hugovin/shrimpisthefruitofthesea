<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" AutoEventWireup="true" CodeFile="mnt_FreeTools.aspx.cs" Inherits="CMS_mnt_FreeTools" %>

<asp:Content ID="freetools" ContentPlaceHolderID="GenericTemplate" runat="server">
    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <% Response.Write(str_TinyMCE); %>    
    <!-- /TinyMCE -->

   <div class="wrapper">

            <div class="MainContainer2">
 
                	<!-- -->
                    
  <div id="contCajas4">
					
                                            
                      
  <div id="contCajas7">
    <div class="cdro_titulo1">Top Area&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
                                            Title:<br />
                                            <input class="class_texto2" id="Title1" name="Title1" value="<%Response.Write(Title1); %>" type="text" /><br /><br />
                                            Sub-Title:<br />
                                            <input class="class_texto2" id="SubTitle1" name="SubTitle1" value="<%Response.Write(SubTitle1); %>" type="text" /><br /><br />
                                            Content:<br /><br />
                                            <textarea id="Content1" name="Content1" rows="15" cols="80" style="width: 100%"><%Response.Write(Content1);%></textarea> <br /><br />
                                            

                      </div>
                                          <div class="clear"></div><!-- fin caja Template A -->
                                          <div id="contCajas8"><br />
                                            <div class="clear">
                                            <br /><hr /><br />
                                            </div></div>
                                          
              </div>
                    <div class="clear"></div>

<div id="contCajas4">
					
                                            
                      
  <div id="contCajas7">
    <div class="cdro_titulo1">Middle Area&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
                                            Title:<br />
                                            <input class="class_texto2" id="Title2" name="Title2" value="<%Response.Write(Title2); %>" type="text" /><br /><br />
                                            Sub-Title:<br />
                                            <input class="class_texto2" id="SubTitle2" name="SubTitle2" value="<%Response.Write(SubTitle2); %>" type="text" /><br /><br />
                                            Content:<br /><br />
                                            <textarea id="Content2" name="Content2" rows="15" cols="80" style="width: 100%"><%Response.Write(Content2);%></textarea> <br /><br />
                                            

                      </div>
                                          <div class="clear"></div><!-- fin caja Template A -->
                                          <div id="Div3"><br />
                                            <div class="clear">
                                            <br /><hr /><br />
                                            </div></div>
                                          
              </div>
                    <div class="clear"></div>
 <div id="contCajas4">
                                            
                                            
                      
  <div id="contCajas7">
    <div class="cdro_titulo1">Below Text&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
                                            Title:<br />
                                            <input class="class_texto2" id="Title3" name="Title3" value="<%Response.Write(Title3); %>" type="text" /><br /><br />
                                            Sub-Title:<br />
                                            <input class="class_texto2" id="SubTitle3" name="SubTitle3" value="<%Response.Write(SubTitle3); %>" type="text" /><br /><br />
                                            Content:<br /><br />
                                            <textarea id="Content3" name="Content3" rows="15" cols="80" style="width: 100%"><%Response.Write(Content3);%></textarea> <br /><br />

                      </div>
                                          <div class="clear"></div><!-- fin caja Template A -->
                                          <div id="contCajas8"><br />
                      <a href="#" style="float:left">
                          <asp:Button ID="btn_cancel" runat="server" Text="" CssClass="class_Cancel" 
                                                  onclick="btn_cancel_Click" /></a><a href="#" style="float:right">
                                              <asp:Button ID="btn_Save" runat="server" Text="" CssClass="class_btnSave"
                                                  onclick="btn_Save_Click" /></a><div class="clear"></div></div>
                                          
              </div>
                      <div class="clear"></div>
                      
                <!-- fin cuerpo -->              
                </div>    
   			<div class="clear"></div>
        	
    </div>

</asp:Content>
