<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="News.aspx.cs" Inherits="Admin_News" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs7" runat="server">
        <div class="news">
            <input id="newsHidden" type="hidden" runat="server" value="" />
            <input id="newsPauseOnMouseHover" type="hidden" runat="server" value="" />
            <div class="cen">
                <table class="newslTable" border="1">
                    <tr>
                        <td>
                            <asp:DropDownList ID="newsSelector" AutoPostBack="true" OnSelectedIndexChanged="newsSelector_SelectedIndexChanged"
                                runat="server" CssClass="textEn">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="addNews" runat="server" class="unVisi" visible="false">
                                <div class="can">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="newsLastUpdateLabel" CssClass="textHeb" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                :תוכן החדשות<FCKeditorV2:FCKeditor Height="400px" Width="700px" AutoDetectLanguage="true"
                                                    EnableSourceXHTML="true" BasePath="/fckeditor/" ID="newsBody" SkinPath="skins/office2003/"
                                                    runat="server">
                                                </FCKeditorV2:FCKeditor>
                                                <br />
                                            </td>
                                            <td>
                                                :מיקום
                                                <br />
                                                <asp:DropDownList ID="newsOrderSelector" runat="server" CssClass="textEn">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                :שם החדשות בתפריט
                                                <br />
                                                <asp:TextBox ID="newsNameInMenu" runat="server" CssClass="textHeb" Width="400px" >
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="newsLabel" Text="" runat="server" CssClass="red"></asp:Label>
                                    <br />
                                    <asp:Button ID="addAddNews" runat="server" Text="Add" OnClick="addAddNews_Click" />
                                    <asp:Button ID="cancelAddNews" runat="server" Text="Cancel" OnClick="cancelAddNews_Click" />
                                    <input id="newsUp" runat="server" type="hidden" value="" />
                                    <input id="newsRe" runat="server" type="hidden" value="" />
                                </div>
                            </div>
                            <input id="newsHiddenRe" type="hidden" runat="server" value="" />
                            <input id="newsHiddenUp" type="hidden" runat="server" value="" />
                            <div id="removeUpdateNews" runat="server" class="unVisi" visible="false">
                                <div class="can">
                                    <asp:DropDownList ID="removeUpdateNewsSelector" Width="400px" CssClass="textHeb"
                                        runat="server">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Button ID="cancelRemoveUpdateNews" runat="server" Text="Cancel" OnClick="cancelRemoveUpdateNews_Click" />
                                    <asp:Button ID="updateRemoveUpdateNews" runat="server" Text="Update" OnClick="updateRemoveUpdateNews_Click" />
                                    <asp:Button ID="removeRemoveUpdateNews" runat="server" Text="Remove" OnClick="removeRemoveUpdateNews_Click" />
                                </div>
                            </div>
                            <div id="newsGeneralOptions" runat="server" class="unVisi" visible="false">
                                <div class="can">
                                    <table>
                                        <tr>
                                            <td>
                                                Select Speed:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="newsSpeedSelector" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Select Pause Time (Seconds):
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="newsPauseTimeSelector" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Number Of News To Show:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="newsNumberItemsSelector" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Pause On Mouse Over:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="newsPauseOnMouseOver" Checked="true" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="newsOptionsOkButton" runat="server" Text="Ok" OnClick="newsOptionsOkButton_Click" />
                                    <asp:Button ID="newsOptionsDefaultButton" runat="server" Text="Restore Default Settings"
                                        OnClick="newsOptionsDefaultButton_Click" />
                                    <asp:Button ID="newsOptionsCancelButton" runat="server" Text="Cancel" OnClick="newsOptionsCancelButton_Click" />
                                </div>
                            </div>
                            <div id="newsNotify" runat="server" class="unVisi" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="newsNotifyLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="okBut" Text="Ok" runat="server" OnClick="okBut_Click" />
                                            <asp:Button ID="cancelBut" Text="Cancel" Visible="false" runat="server" OnClick="cancelBut_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
