<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="mnt_HighLights.aspx.cs" Inherits="mnt_HighLights" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script src="js/Links.js" type="text/javascript"></script>

    <script type="text/javascript"> 
		function showEmail(id){
            document.getElementById(id).style.margin='-100px 100px 100px 100px';
            document.getElementById(id).style.visibility="visible" ;
        }
        function hideEmail(id){
            document.getElementById(id).style.visibility="hidden" ;
        }
    </script>

    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <script src="js/dhtmlapi.js" type="text/javascript"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->
    <div class="newOpciones">
        <a href="mnt_AddSc.aspx?pages=true">
            <div class="newItem">
                <img src="images/point.png" border="0" />&nbsp;&nbsp;Adds</div>
        </a><a href="mnt_FeaturedSpace.aspx">
            <div class="newItem">
                <img src="images/point.png" border="0" />&nbsp;&nbsp;Theater</div>
        </a><a href="mnt_HighLights.aspx">
            <div class="newItem">
                <img src="images/point.png" border="0" />&nbsp;&nbsp;Highlights</div>
        </a><a href="mnt_FeaturedBrands.aspx">
            <div class="newItem">
                <img src="images/point.png" border="0" />&nbsp;&nbsp;Featured Brands</div>
        </a>
    </div>
    <div class="master_container">
        <h3>
            Highlights</h3>
        <div>
            <div id="div_HighLights" runat="server">
                <div class="cdro_caja4">
                    <div id="div_HiglightsList" runat="server">
                    </div>
                </div>
                <asp:Button ID="btn_New" runat="server" CssClass="class_addNew" OnClick="btn_New_Click">
                </asp:Button>
                &nbsp;<asp:Label ID="lbmaxhigh" runat="server" Text=""></asp:Label>
            </div>
            <div id="div_neweditHighLight" runat="server" visible="false">
                <div class="cdro_caja">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="88">
                            </td>
                            <td width="58" align="right">
                                Image&nbsp;&nbsp;
                            </td>
                            <td width="310"><br />
                                <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                    checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
                                image
                                <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
                                Url
                                <div id="container_file_uploader_image"><br />
                                    <asp:FileUpload ID="FUimage" CssClass="class_file" runat="server"></asp:FileUpload>
                                </div>
                                <div id="container_url_image" style="display: none">
                                    <input type="text" id="txt_image_url" name="txt_image_url" />
                                </div>
                            </td>
                            <td width="175">
                                <asp:Label ID="lbErrorHighlight" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="88">
                            </td>
                            <td align="right">
                                Title
                            </td>
                            <td>
                                <input id="hidePath" runat="server" type="hidden" />
                                <asp:TextBox ID="txt_Title" CssClass="class_file" runat="server" Text=""></asp:TextBox>
                                <br />
                            </td>
                            <td>
                                <%if (lbErrorHighlight.Text != "")
                                  {%>
                                <a href="../images/<%Response.Write(lbErrorHighlight.Text);%>" target="_blank" id="mb2"
                                    class="mb" title="Left Add"><span class="class_btnActualImage"></span></a>
                                <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr height="40">
                            <td width="88" height="20">
                            </td>
                            <td align="right">
                                Alt&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Alt" CssClass="class_file" runat="server" Width="318px" Text=""></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="88">
                            </td>
                            <td align="right">
                                Link
                            </td>
                            <td>
                                <input id="txt_link" class="class_file" name="txt_link" type="text" value="<%Response.Write(linktopage);%>" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="88">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                <input id="radioExternal" name="radio" value="external" type="radio" checked />External
                                <input id="radioInternal" name="radio" value="internal" type="radio" />Internal
                                <input id="radioNewLanding" name="radio" value="landing" type="radio" /><% Response.Write(add_edit_landing_page1); %>Landing Page
                                <input id="radiotipo" value="" name="radiotipo" type="hidden" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <div id="htmlElement" style="display: none;">
                                    <div id="div_landingPage" name="div_landingPage">
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        Title:
                                        <input type="text" class="class_texto2" name="txtlandTitle" value="<% Response.Write(landingPageTitle); %>" /><br />
                                        Content:<br />
                                        <textarea id="elmlandpage" name="elmlandpage" cols="20" rows="15"><% Response.Write(landingPageContent); %></textarea><br />
                                        <% if(landingPageImage!=""){ %>
                                        <p>Current Image: <% Response.Write(landingPageImage); %></p>
                                        <% } %>
                                        <input type="radio" id="rb_select_image_uploadLanding" name="rb_image_typeLanding"
                                            value="upload" checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />Upload
                                        image
                                        <input type="radio" id="rb_select_image_urlLanding" name="rb_image_typeLanding" value="url"
                                            onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />External
                                        Url
                                        <div id="container_file_uploader_image_Landing">
                                            <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" />
                                        </div>
                                        <div id="container_url_image_Landing" style="display: none">
                                            <input type="text" id="txt_image_url_landing" name="txt_image_url_landing" />
                                        </div>
                                        <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                                    </div>
                                    <input id="hideidhigh" type="hidden" runat="server" />
                                    <input id="highidHide" runat="server" type="hidden" />
                                </div>
                                <div id="div_internalLink" style="display: none;" class="spaceInternal">
                                    <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="88">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_Cancel" CssClass="class_Cancel" runat="server" OnClientClick="return confirm('do you want to leave without save')"
                                                OnClick="btn_Cancel_Click"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" CssClass="class_btnSave"
                                                PostBackUrl="~/CMS/mnt_HighLights.aspx" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <input id="hidepos" name="hidepos" type="hidden" value="<%Response.Write(EditPosition);%>" />
        </div>
    </div>

    <script type="text/javascript">
    var maxRows=3;// max rows to sortorder
    function sortorder(){
  
    var j=1;//order number start in 1
    for(i=0; i<document.aspnetForm.elements.length; i++)
	    {
	    
		    if (document.aspnetForm.elements[i].name.substring(0,7)== "txtCaIt"){
			    document.aspnetForm.elements[i].value=j++;
			    }
	    }
	    document.aspnetForm.action = "mnt_HighLights.aspx?UpdPosition=true";
	    document.aspnetForm.submit();
	 }
    </script>

    <script language="Javascript" type="text/javascript" src="js/dnd2.js"></script>

    <script type="text/javascript">
        <!--
        $('#radioExternal').click(function() {
            $('#htmlElement').css("display","none");
            $('#div_internalLink').css("display","none");
        });
        $('#radioInternal').click(function() {
            $('#htmlElement').css("display","none");
            $('#div_internalLink').css("display","block");
        });
        $('#radioNewLanding').click(function() {
            $('#htmlElement').css("display","block");
            $('#div_internalLink').css("display","none");
        });
        -->
    </script>

    <input type="hidden" id="LinkIDmore" />
    <input type="hidden" id="txtLinkmore2" />
    <input type="hidden" id="LinkTypeT" />
    <input type="hidden" id="LinkID" value="1" />
</asp:Content>
