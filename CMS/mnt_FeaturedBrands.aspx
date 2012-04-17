<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true"
    CodeFile="mnt_FeaturedBrands.aspx.cs" Inherits="mnt_FeaturedBrands" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script src="js/dhtmlapi.js" type="text/javascript"></script>

    <div id="Selection_Area" runat="server">
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
    </div>
    <div class="master_container">
        <div class="titulos2">
            Sections inside Featured Brands<img src="images/arrow1.png" /></div>
        <div>
            <div id="contCajas2">
                <div class="cdro_caja4">
                    <div id="div_FeatureProducts" runat="server" class="test">
                        No Results
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <table>
                <tr>
                    <td>
                        <div id="AddButtons">
                            <asp:Button ID="btnAddFP" CssClass="class_addNew" runat="server" OnClick="btnAddFP_Click" />
                            <asp:Label ID="lblCapacity" runat="server" Text=""></asp:Label>
                        </div>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;
                    </td>
                    <td>
                        <div id="div_AddFP" runat="server">
                        </div>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" src="images/goBtnTable.jpg" runat="server" Text="Search"
                            OnClick="btnSearch_Click" Visible="false" />
                        <asp:Label ID="lbPositionError" runat="server" Text="" Visible="false"></asp:Label><br />
                    </td>
                </tr>
            </table>
            &nbsp;<br />
            <div id="search_result" visible="false" runat="server">
                <div class="cdro_caja4">
                    <div id="div_SearchResult" runat="server">
                    </div>
                    <div id="div_saveAndcancel" runat="server" visible="false">
                        <asp:FileUpload ID="FUimage" CssClass="class_file" runat="server"></asp:FileUpload>
                        <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
                        <input id="hidePath" runat="server" type="hidden" />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCancelFeat" runat="server" OnClick="btnCancelFeat_Click1" OnClientClick="return confirm('Do you want to leave without save?')"
                                        CssClass="class_Cancel" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveFeat" runat="server" OnClientClick="return confirm('Add New Product as a Feature Brand?')"
                                        OnClick="btnSaveFeat_Click" CssClass="class_btnSave" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="div_info" runat="server">
            </div>
        </div>
    </div>

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
	    document.aspnetForm.action = "mnt_FeaturedBrands.aspx?UpdPosition=true";
	    document.aspnetForm.submit();
	 }
    </script>

    <script language="Javascript" type="text/javascript" src="js/dnd2.js"></script>

</asp:Content>
