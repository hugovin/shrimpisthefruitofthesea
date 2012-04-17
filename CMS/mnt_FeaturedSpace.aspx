<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="mnt_FeaturedSpace.aspx.cs" Inherits="mnt_FeaturedSpace" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/calendar.css" rel="stylesheet" type="text/css" />

    <script src="js/dhtmlapi.js" type="text/javascript"></script>

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <script src="js/calendar_us.js" type="text/javascript"></script>

    <script src="js/calendar_us2.js" type="text/javascript"></script>

    <script src="js/Links2.js" type="text/javascript"></script>

    <script language="Javascript" type="text/javascript" src="js/dnd2.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->
    <%
    if(ddlHourFrom.Attributes["onchange"] == null)
    {
      ddlHourFrom.Attributes.Add("onchange", "javascript:save_hour_hidden_from(this)");
    }

    if(ddlMinFrom.Attributes["onchange"] == null)
    {
      ddlMinFrom.Attributes.Add("onchange", "javascript:save_minute_hidden_from(this)");
    }

    if(ddlAMPMFrom.Attributes["onchange"] == null)
    {
      ddlAMPMFrom.Attributes.Add("onchange", "javascript:save_ampm_hidden_from(this)");
    }

    if(ddlHourTo.Attributes["onchange"] == null)
    {
      ddlHourTo.Attributes.Add("onchange", "javascript:save_hour_hidden_to(this)");
    }

    if(ddlMinTo.Attributes["onchange"] == null)
    {
      ddlMinTo.Attributes.Add("onchange", "javascript:save_minute_hidden_to(this)");
    }

    if(ddlAMPMTo.Attributes["onchange"] == null)
    {
      ddlAMPMTo.Attributes.Add("onchange", "javascript:save_amppm_hidden_to(this)");
    }
    %>
    <div class="newOpciones">
        <a href="mnt_AddSc.aspx?pages=true">
            <div class="newItem">
                <img src="images/point.png" border="0" />&nbsp;&nbsp;Adds</div>
        </a></a><a href="mnt_FeaturedSpace.aspx">
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
            Theater</h3>
        <div class="cdro_caja2">
            <div id="div_featureSpace" runat="server">
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="div_new" runat="server" visible="false">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btn_New" CssClass="class_addNew" runat="server" OnClick="btn_New_Click">
                        </asp:Button>
                    </td>
                    <td>
                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="div_dates" runat="server" visible="false">
            <div class="cdro_caja">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="88">
                        </td>
                        <td width="58" align="right" valign="top">
                            Image
                        </td>
                        <td width="450">
                            <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
                            image
                            <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
                            Url
                            <div id="container_file_uploader_image"><br />
                                <asp:FileUpload CssClass="class_file" ID="FUimage" runat="server"></asp:FileUpload>
                                <asp:Button ID="btn_Upload" runat="server" CssClass="class_btnUpload" OnClick="btn_Upload_Click">
                                </asp:Button>
                            </div>
                            <div id="container_url_image" style="display: none"><br /> 
                                <input type="text" id="txt_image_url" name="txt_image_url" />
                            </div>
                        </td>
                        <td>
                            <input id="hidePath" runat="server" type="hidden" />
                            <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
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
                            Title&nbsp;&nbsp;
                        </td>
                        <td width="450">
                            <asp:TextBox ID="txt_Title" CssClass="class_file" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td>
                            <div id="show_image" runat="server">
                                <%if (ActualImage != "")
                                  {%>
                                <a href="../images/<%Response.Write(ActualImage);%>" target="_blank" id="mb2" class="mb"
                                    title="Left Add"><span class="class_btnActualImage">
                                        <%} %>
                                    </span></a>
                            </div>
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
                        <td align="right" valign="top">
                            Link&nbsp;&nbsp;
                        </td>
                        <td width="450">
                            <input id="txt_link" class="class_file" name="txt_link" type="text" value="<%Response.Write(linktopage);%>" />
                                                                                                                    <div class="radioOpt2">
                                    <div id="div_internalLink" class="spaceInternal">
                                        <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                        </td>
                        <td>
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
                            Alt&nbsp;&nbsp;
                        </td>
                        <td width="450">
                            <asp:TextBox ID="txt_Alt" CssClass="class_file" Text="" runat="server"></asp:TextBox>
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
                            From
                        </td>
                        <td width="450">
                            <input type="text" name="txtFrom" value="<%Response.Write(txtFrom); %>" id="txtFrom" />

                            <script type="text/javascript" language="JavaScript">new tcal ({'controlname' : 'txtFrom'});</script>

                            <asp:DropDownList ID="ddlHourFrom" runat="server">
                                <asp:ListItem Value="01"></asp:ListItem>
                                <asp:ListItem Value="02"></asp:ListItem>
                                <asp:ListItem Value="03"></asp:ListItem>
                                <asp:ListItem Value="04"></asp:ListItem>
                                <asp:ListItem Value="05"></asp:ListItem>
                                <asp:ListItem Value="06"></asp:ListItem>
                                <asp:ListItem Value="07"></asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="11"></asp:ListItem>
                                <asp:ListItem Value="12"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_from_hour" name="hidden_from_hour" value="<%Response.Write(hidden_from_hour); %>" />
                            &nbsp;:
                            <asp:DropDownList ID="ddlMinFrom" runat="server">
                                <asp:ListItem Value="00"></asp:ListItem>
                                <asp:ListItem Value="01"></asp:ListItem>
                                <asp:ListItem Value="02"></asp:ListItem>
                                <asp:ListItem Value="03"></asp:ListItem>
                                <asp:ListItem Value="04"></asp:ListItem>
                                <asp:ListItem Value="05"></asp:ListItem>
                                <asp:ListItem Value="06"></asp:ListItem>
                                <asp:ListItem Value="07"></asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="11"></asp:ListItem>
                                <asp:ListItem Value="12"></asp:ListItem>
                                <asp:ListItem Value="13"></asp:ListItem>
                                <asp:ListItem Value="14"></asp:ListItem>
                                <asp:ListItem Value="15"></asp:ListItem>
                                <asp:ListItem Value="16"></asp:ListItem>
                                <asp:ListItem Value="17"></asp:ListItem>
                                <asp:ListItem Value="18"></asp:ListItem>
                                <asp:ListItem Value="19"></asp:ListItem>
                                <asp:ListItem Value="20"></asp:ListItem>
                                <asp:ListItem Value="21"></asp:ListItem>
                                <asp:ListItem Value="22"></asp:ListItem>
                                <asp:ListItem Value="23"></asp:ListItem>
                                <asp:ListItem Value="24"></asp:ListItem>
                                <asp:ListItem Value="25"></asp:ListItem>
                                <asp:ListItem Value="26"></asp:ListItem>
                                <asp:ListItem Value="27"></asp:ListItem>
                                <asp:ListItem Value="28"></asp:ListItem>
                                <asp:ListItem Value="29"></asp:ListItem>
                                <asp:ListItem Value="30"></asp:ListItem>
                                <asp:ListItem Value="31"></asp:ListItem>
                                <asp:ListItem Value="32"></asp:ListItem>
                                <asp:ListItem Value="33"></asp:ListItem>
                                <asp:ListItem Value="34"></asp:ListItem>
                                <asp:ListItem Value="35"></asp:ListItem>
                                <asp:ListItem Value="36"></asp:ListItem>
                                <asp:ListItem Value="37"></asp:ListItem>
                                <asp:ListItem Value="38"></asp:ListItem>
                                <asp:ListItem Value="39"></asp:ListItem>
                                <asp:ListItem Value="40"></asp:ListItem>
                                <asp:ListItem Value="41"></asp:ListItem>
                                <asp:ListItem Value="42"></asp:ListItem>
                                <asp:ListItem Value="43"></asp:ListItem>
                                <asp:ListItem Value="44"></asp:ListItem>
                                <asp:ListItem Value="45"></asp:ListItem>
                                <asp:ListItem Value="46"></asp:ListItem>
                                <asp:ListItem Value="47"></asp:ListItem>
                                <asp:ListItem Value="48"></asp:ListItem>
                                <asp:ListItem Value="49"></asp:ListItem>
                                <asp:ListItem Value="50"></asp:ListItem>
                                <asp:ListItem Value="51"></asp:ListItem>
                                <asp:ListItem Value="52"></asp:ListItem>
                                <asp:ListItem Value="53"></asp:ListItem>
                                <asp:ListItem Value="54"></asp:ListItem>
                                <asp:ListItem Value="55"></asp:ListItem>
                                <asp:ListItem Value="56"></asp:ListItem>
                                <asp:ListItem Value="57"></asp:ListItem>
                                <asp:ListItem Value="58"></asp:ListItem>
                                <asp:ListItem Value="59"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_from_minute" name="hidden_from_minute" value="<%Response.Write(hidden_from_minute); %>" />
                            &nbsp;
                            <asp:DropDownList ID="ddlAMPMFrom" runat="server">
                                <asp:ListItem Value="AM"></asp:ListItem>
                                <asp:ListItem Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_from_ampm" name="hidden_from_ampm" value="<%Response.Write(hidden_from_ampm); %>" />
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
                            To
                        </td>
                        <td width="450">
                            <input type="text" name="txtTo" value="<%Response.Write(txtTo); %>" id="txtTo" />

                            <script type="text/javascript" language="JavaScript">new tcal2 ({'controlname' : 'txtTo'});</script>

                            <asp:DropDownList ID="ddlHourTo" runat="server" Height="18px" Width="43px">
                                <asp:ListItem Value="01">01</asp:ListItem>
                                <asp:ListItem Value="02">02</asp:ListItem>
                                <asp:ListItem Value="03">03</asp:ListItem>
                                <asp:ListItem Value="04">04</asp:ListItem>
                                <asp:ListItem Value="05">05</asp:ListItem>
                                <asp:ListItem Value="06">06</asp:ListItem>
                                <asp:ListItem Value="07">07</asp:ListItem>
                                <asp:ListItem Value="08">08</asp:ListItem>
                                <asp:ListItem Value="09">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_to_hour" name="hidden_to_hour" value="<%Response.Write(hidden_to_hour); %>" />
                            :
                            <asp:DropDownList ID="ddlMinTo" runat="server">
                                <asp:ListItem Value="00"></asp:ListItem>
                                <asp:ListItem Value="01"></asp:ListItem>
                                <asp:ListItem Value="02"></asp:ListItem>
                                <asp:ListItem Value="03"></asp:ListItem>
                                <asp:ListItem Value="04"></asp:ListItem>
                                <asp:ListItem Value="05"></asp:ListItem>
                                <asp:ListItem Value="06"></asp:ListItem>
                                <asp:ListItem Value="07"></asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="11"></asp:ListItem>
                                <asp:ListItem Value="12"></asp:ListItem>
                                <asp:ListItem Value="13"></asp:ListItem>
                                <asp:ListItem Value="14"></asp:ListItem>
                                <asp:ListItem Value="15"></asp:ListItem>
                                <asp:ListItem Value="16"></asp:ListItem>
                                <asp:ListItem Value="17"></asp:ListItem>
                                <asp:ListItem Value="18"></asp:ListItem>
                                <asp:ListItem Value="19"></asp:ListItem>
                                <asp:ListItem Value="20"></asp:ListItem>
                                <asp:ListItem Value="21"></asp:ListItem>
                                <asp:ListItem Value="22"></asp:ListItem>
                                <asp:ListItem Value="23"></asp:ListItem>
                                <asp:ListItem Value="24"></asp:ListItem>
                                <asp:ListItem Value="25"></asp:ListItem>
                                <asp:ListItem Value="26"></asp:ListItem>
                                <asp:ListItem Value="27"></asp:ListItem>
                                <asp:ListItem Value="28"></asp:ListItem>
                                <asp:ListItem Value="29"></asp:ListItem>
                                <asp:ListItem Value="30"></asp:ListItem>
                                <asp:ListItem Value="31"></asp:ListItem>
                                <asp:ListItem Value="32"></asp:ListItem>
                                <asp:ListItem Value="33"></asp:ListItem>
                                <asp:ListItem Value="34"></asp:ListItem>
                                <asp:ListItem Value="35"></asp:ListItem>
                                <asp:ListItem Value="36"></asp:ListItem>
                                <asp:ListItem Value="37"></asp:ListItem>
                                <asp:ListItem Value="38"></asp:ListItem>
                                <asp:ListItem Value="39"></asp:ListItem>
                                <asp:ListItem Value="40"></asp:ListItem>
                                <asp:ListItem Value="41"></asp:ListItem>
                                <asp:ListItem Value="42"></asp:ListItem>
                                <asp:ListItem Value="43"></asp:ListItem>
                                <asp:ListItem Value="44"></asp:ListItem>
                                <asp:ListItem Value="45"></asp:ListItem>
                                <asp:ListItem Value="46"></asp:ListItem>
                                <asp:ListItem Value="47"></asp:ListItem>
                                <asp:ListItem Value="48"></asp:ListItem>
                                <asp:ListItem Value="49"></asp:ListItem>
                                <asp:ListItem Value="50"></asp:ListItem>
                                <asp:ListItem Value="51"></asp:ListItem>
                                <asp:ListItem Value="52"></asp:ListItem>
                                <asp:ListItem Value="53"></asp:ListItem>
                                <asp:ListItem Value="54"></asp:ListItem>
                                <asp:ListItem Value="55"></asp:ListItem>
                                <asp:ListItem Value="56"></asp:ListItem>
                                <asp:ListItem Value="57"></asp:ListItem>
                                <asp:ListItem Value="58"></asp:ListItem>
                                <asp:ListItem Value="59"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_to_minute" name="hidden_to_minute" value="<%Response.Write(hidden_to_minute); %>" />
                            &nbsp;
                            <asp:DropDownList ID="ddlAMPMTo" runat="server">
                                <asp:ListItem Value="AM"></asp:ListItem>
                                <asp:ListItem Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="hidden" id="hidden_to_ampm" name="hidden_to_ampm" value="<%Response.Write(hidden_to_ampm); %>" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Label ID="lbError" runat="server" Text="**Complete all the information" Visible="False"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td align="center">
                            <img src="images/note2.png" width="215" height="22" />
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
                            <div id="linksContent">
                            <br />
                                <input id="selectOpt1" name="radioLinkType" type="radio" value="external" />External
                                <input id="selectOpt2" name="radioLinkType" type="radio" value="internal" />Internal
                                <input id="selectOpt3" name="radioLinkType" type="radio" value="landing" /><% Response.Write(add_edit_landing_page1); %>Landing Page<br />
                                <div class="radioOpt1">
                                </div>
                                <div class="radioOpt3">
                                    <div id="div_landingPage">
                                        <br />
                                        Title:<br />
                                        <input type="text" name="txtlandTitle" value="<% Response.Write(landingPageTitle); %>" /><br />
                                        Content:<br />
                                        <textarea id="elm1" name="elm1"><% Response.Write(landingPageContent); %></textarea><br />
                                        <% if(landingPageImage != ""){ %>
                                        Current Image:
                                        <% Response.Write(landingPageImage); %>
                                        <% } %>
                                        <div id="selectionImageLanding">
                                            <input type="radio" id="rb_select_image_uploadLanding" name="rb_image_typeLanding"
                                                value="upload" checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />Upload
                                            image
                                            <input type="radio" id="rb_select_image_urlLanding" name="rb_image_typeLanding" value="url"
                                                onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />External
                                            Url
                                            <div id="container_file_uploader_image_Landing">
                                                <asp:FileUpload ID="UploadLanding" runat="server" Height="22px" Width="217px" />
                                                <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div id="container_url_image_Landing" style="display: none">
                                                <input type="text" id="txt_image_url_landing" name="txt_image_url_landing" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <script type="text/javascript">
	                                <!--
		                                $('#selectOpt1').click(function() {
			                                $('.radioOpt1').css("display","block");
			                                $('.radioOpt2').css("display","none");
			                                $('.radioOpt3').css("display","none");
		                                });
		                                $('#selectOpt2').click(function() {
			                                $('.radioOpt1').css("display","none");
			                                $('.radioOpt2').css("display","block");
			                                $('.radioOpt3').css("display","none");
		                                });
		                                $('#selectOpt3').click(function() {
			                                $('.radioOpt1').css("display","none");
			                                $('.radioOpt2').css("display","none");
			                                $('.radioOpt3').css("display","block");
		                                });
	                                -->
                                </script>

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
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="class_Cancel" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Do you want to save this a as Theather')"
                                            OnClick="btnSave_Click" />
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
    </div>
    <div id="canBeSorted" style="display:none;"></div>
    <img alt="" runat="server" id="imageUploaded" visible="false" src="" style="height: 178px;
        position: absolute; width: 214px; top: 395px; left: 1118px;" />

    <script type="text/javascript">
        var maxRows=5;// max rows to sortorder
        function sortorder(){
      
        var j=1;//order number start in 1
        for(i=0; i<document.aspnetForm.elements.length; i++)
	        {
    	    
		        if (document.aspnetForm.elements[i].name.substring(0,7)== "txtCaIt"){
			        document.aspnetForm.elements[i].value=j++;
			        }
	        }
	        document.aspnetForm.action = "mnt_FeaturedSpace.aspx?UpdPosition=true";
	        document.aspnetForm.submit();
	     }
    </script>
    <input type="hidden" id="LinkID" value="1" />
    <input type="hidden" id="txt_link2" />
    <input type="hidden" id="LinkIDmore" />
    <input type="hidden" id="txtLinkmore2" />
</asp:Content>
