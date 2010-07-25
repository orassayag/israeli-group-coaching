<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemoveFromMailList.aspx.cs"
    Inherits="RemoveFromMailList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המרכז לאימון בקבוצה</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--[if IE]>
<link rel="stylesheet" type="text/css" href="styleIE.css" />
<![endif]-->
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="insideRemove">
        <div class="insideLead">
            <table>
                <tr>
                    <td class="insideRemoveTd">
                        <div id="question" runat="server" visible="true">
                            האם את/ה בטוח/ה שברצונך לצאת מרשימת התפוצה?
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="buttons" runat="server" visible="true">
                            <asp:Button ID="removeYes" runat="server" Text="כן" OnClick="removeYes_Click" />
                            <asp:Button ID="removeNo" runat="server" Text="לא" OnClick="removeNo_Click" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="removeLabel" CssClass="blue" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
