<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="LeadPage.aspx.cs" Inherits="LeadPage" %>

<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="mainLead" class="insideTextLead">
        <table>
            <tr>
                <td>
                    <div id="leaveLead" runat="server">
                        <table>
                            <tr>
                                <td>
                                    שם:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="getLeadName" CssClass="insideText" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    אי-מייל:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="getLeadMail" CssClass="english" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    נושא:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="getLeadTitle" CssClass="insideText" runat="server" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ההודעה:
                                </td>
                                <td>
                                    <asp:TextBox ID="getLeadBody" Height="300px" Width="500px" CssClass="insideText"
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="insideLead">
                            <asp:Label ID="leadLabel" runat="server" CssClass="red"></asp:Label>
                            <br />
                            <asp:Button ID="sendLead" runat="server" Text="שלח" OnClick="sendLead_Click" OnClientClick="return checkLead()" />
                            <asp:Button ID="cancelLead" runat="server" Text="בטל" OnClick="cancelLead_Click" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
