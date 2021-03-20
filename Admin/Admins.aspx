<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Admins.aspx.cs" Inherits="Admin_Admins" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs8" runat="server">
        <div class="admins">
            <input id="adminsHidden" type="hidden" runat="server" value="" />
            <div class="cen">
                <table class="adminslTable" border="1">
                    <tr>
                        <td>
                            <asp:DropDownList ID="adminSelector" AutoPostBack="true" OnSelectedIndexChanged="adminSelector_SelectedIndexChanged"
                                runat="server" CssClass="textEn">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="addAdmin" runat="server" class="unVisi" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            User ID
                                            <asp:TextBox ID="generalGetUserID" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            Password
                                            <asp:TextBox ID="generalGetPassword" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            Re-Write Password
                                            <asp:TextBox ID="generalGetPassword2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="adminLabel" Text="" runat="server" CssClass="red"></asp:Label>
                                <br />
                                <asp:Button ID="addAddAdmin" runat="server" Text="Add" OnClick="addAddAdmin_Click" OnClientClick="return validateField()" />
                                <asp:Button ID="cancelAddAdmin" runat="server" Text="Cancel" OnClick="cancelAddAdmin_Click" />
                                <input id="loginIDUp" runat="server" type="hidden" value="" />
                                <input id="loginIDRe" runat="server" type="hidden" value="" />
                            </div>
                            <div id="removeUpdateAdmin" runat="server" class="unVisi" visible="false">
                                <asp:DropDownList ID="removeUpdateAdminSelector" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="cancelRemoveUpdateAdmin" runat="server" Text="Cancel" OnClick="cancelRemoveUpdateAdmin_Click" />
                                <asp:Button ID="updateRemoveUpdateAdmin" runat="server" Text="Update" OnClick="updateRemoveUpdateAdmin_Click" />
                                <asp:Button ID="removeRemoveUpdateAdmin" runat="server" Text="Remove" OnClick="removeRemoveUpdateAdmin_Click" />
                            </div>
                            <div id="adminsNotify" runat="server" class="unVisi" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="adminsNotifyLabel" runat="server" Text=""></asp:Label>
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
