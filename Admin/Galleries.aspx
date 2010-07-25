<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Galleries.aspx.cs" Inherits="Admin_Galleries" %>
<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tab-content2" id="tabs4">
        <input id="allArtistsHidden" runat="server" type="hidden" value="" />
        <div id="mainAllArtists" runat="server" class="mailNo" visible="false">
            <div class="cen">
                Select Action:
                <asp:DropDownList ID="allArtistsSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="allArtistsSelector_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div id="addAllArtistsGallery" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="galleryUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                        <asp:Label ID="galleryLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        Gallery Name Hebrew:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="allArtistsGalleryNameHe" CssClass="textHeb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Gallery Name English:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="allArtistsGalleryNameEn" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Gallery Place:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="allArtistsGalleryPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addAllArtistsGalleryButton" Text="Add" runat="server" OnClick="addAllArtistsGalleryButton_Click" />
                <asp:Button ID="cancelAddAllArtistsGalleryButton" Text="Cancel" runat="server" OnClick="cancelAllArtistsGalleryButton_Click" />
            </div>
        </div>
        <div id="removeUpdateAllArtistsGallery" runat="server" class="mailNo" visible="false">
            <input id="allArtistsGalleryHiddenRe" type="hidden" runat="server" value="" />
            <input id="allArtistsGalleryHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            Select Gallery:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="removeUpdateAllArtistsGallerySelector" runat="server" Width="200px"
                                Height="200px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeAllArtistsGalleryButton" Text="Remove" runat="server" OnClick="removeAllArtistsGalleryButton_Click" />
                <asp:Button ID="updateAllArtistsGalleryButton" Text="Update" runat="server" OnClick="updateAllArtistsGalleryButton_Click" />
                <asp:Button ID="cancelAllArtistsGalleryButton" Text="Cancel" runat="server" OnClick="cancelAllArtistsGalleryButton_Click" />
            </div>
        </div>
        <div id="addPicToGallery" runat="server" class="mailNo" visible="false">
            <div class="cen">
                <table>
                    <tr id="allArtistsPicUpdateInfo" runat="server">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Last Update:
                                    </td>
                                    <td>
                                        <asp:Label ID="allArtistsPicLastUpdateLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="allArtistsPicStatusLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Add To Gallery:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="addPicGallerySelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="right">
                                        Picture Place:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="allArtistsPicPlace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <img id="allArtistsPicUploadPic" alt="" src="" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Picture:
                                    </td>
                                    <td align="left">
                                        <input id="allArtistsPicUpload" type="file" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="addAllArtistsPicGalleryButton" Text="Add" runat="server" OnClick="addAllArtistsPicGalleryButton_Click" />
                <asp:Button ID="disableAllArtistsPicGallery" Text="Disable" runat="server" OnClick="disableAllArtistsPicGallery_Click" />
                <asp:Button ID="enableAllArtistsPicGallery" Text="Enable" runat="server" OnClick="enableAllArtistsPicGallery_Click" />
                <asp:Button ID="cancelAddPicGalleryButton" Text="Cancel" runat="server" OnClick="cancelAddPicGalleryButton_Click" />
            </div>
        </div>
        <div id="removeUpdatePicGallery" runat="server" class="mailNo" visible="false">
            <input id="allArtistsPicHiddenRe" type="hidden" runat="server" value="" />
            <input id="allArtistsPicHiddenUp" type="hidden" runat="server" value="" />
            <div class="cen">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Select Picture:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="removeUpdatePicGallerySelector" runat="server" Width="200px" Height="200px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="removeAllArtistsPicGallery" Text="Remove" runat="server" OnClick="removeAllArtistsPicGallery_Click" />
                <asp:Button ID="updateAllArtistsPicGallery" Text="Update" runat="server" OnClick="updateAllArtistsPicGallery_Click" />
                <asp:Button ID="cancelAllArtistsPicGallery" Text="Cancel" runat="server" OnClick="cancelAllArtistsPicGallery_Click" />
            </div>
        </div>
        <div id="allArtistsNotify" runat="server" class="mailNo" visible="false">
            <asp:Label ID="notifyLabel" CssClass="notify" Text="" runat="server"></asp:Label><br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="okBut" runat="server" Text="Ok" OnClick="okBut_Click" />
                    </td>
                    <td>
                        <asp:Button ID="cancelBut" runat="server" Text="Cancel" OnClick="cancelBut_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

