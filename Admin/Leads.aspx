<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Leads.aspx.cs" Inherits="Admin_Leads" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs4" runat="server">
        <div class="leads">
        <input id="leadsHidden" type="hidden"  runat="server" value="" />
            <table class="leadsTable" border="1">
                <tr>
                    <td>
                        <div id="leadsData" runat="server" class="unVisi" visible="false">
                            <div class="cenl">
                                <table>
                                    <tr>
                                        <td>
                                            <div class="list">
                                                <asp:Repeater ID="leadGrid" runat="server">
                                                    <HeaderTemplate>
                                                        <table border="1" cellspacing="1" cellpadding="4">
                                                            <tr>
                                                                <th>
                                                                    ID
                                                                </th>
                                                                <th>
                                                                    From
                                                                </th>
                                                                <th>
                                                                    Title
                                                                </th>
                                                                <th>
                                                                    Received On
                                                                </th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("LeadID") %>
                                                            </td>
                                                            <td>
                                                                <%#Eval("LeadName")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("LeadTitle")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("spLeadDate")%>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="leadReadBut" Text="Read" runat="server" OnClick="leadReadBut_Click"
                                                                    CommandName='<%# DataBinder.Eval(Container.DataItem,"LeadID") %>'></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="leadRemoveBut" Text="Delete" runat="server" OnClick="leadRemoveBut_Click"
                                                                    CommandName='<%# DataBinder.Eval(Container.DataItem,"LeadID") %>'></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="leadsRead" runat="server" class="unVisiHeb" visible="false">
                            <div class="cenl">
                                <table>
                                    <tr>
                                        <td>
                                            <input id="leadHidden" type="hidden" runat="server" value="" />
                                            התקבל ב:
                                            <asp:Label ID="leadsReadDate" CssClass="textHeb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            שם:
                                            <asp:Label ID="leadsReadName" CssClass="textHeb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            נושא:
                                            <asp:Label ID="leadsReadTitle" CssClass="textHeb" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textHeb">
                                            <br />
                                            ההודעה:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="leadsReadBody" CssClass="textHeb" Height="120px" Width="500px" runat="server"
                                                ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textHeb">
                                            מייל השולח:
                                            <asp:Label ID="leadsReadMail" CssClass="textEn" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="leadsDeleteButton" Text="Delete" runat="server" OnClick="leadsDeleteButton_Click" />
                                <asp:Button ID="leadsBackButton" Text="Back" runat="server" OnClick="leadsBackButton_Click" />
                            </div>
                        </div>
                        <div id="leadsNotify" runat="server" class="unVisi" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="leadsNotifyLabel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="okBut" Text="Ok" runat="server" OnClick="okBut_Click" />
                                        <asp:Button ID="cancelBut" Text="Cancel" Visible="false" runat="server"
                                            OnClick="cancelBut_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
