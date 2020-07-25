<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AvailityAssessment.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Input:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="479px"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </div>
        <div></div>
        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
