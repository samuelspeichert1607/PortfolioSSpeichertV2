<%@ Page Language="C#" AutoEventWireup="true" CodeFile="confirmation.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="container">
        <div id="entete">
            <div id="enteteDroite"><img src="gabarit/headlogo.jpg" style="height: 334px; width: 596px" /></div>
            <div id="enteteGauche"><h1>Friendly Fry Days</h1></div>
        </div>
        
        <div class="clear"></div>
        <div id="menuVertical">
            <div class="linkItem"><a id="activemenulink" href="Default.aspx">Accueil</a></div>
            <div class="linkItem"><a href="inscription.aspx">Inscription</a></div>
        </div>
        <div id="contenu">
            <p class="styleparagraphe1">Bienvenue</p>
            <div>
                <asp:Panel ID="Panel1" runat="server" Height="662px">
                    <asp:Label ID="Label1" runat="server" Text="Confirmation de l'inscription" Font-Bold="True" Font-Size="Larger" Font-Underline="True"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Table ID="TableConfirm" runat="server" BorderStyle="Solid" GridLines="Both">
                    
                </asp:Table>
                    <br />
                    <asp:ListBox ID="ListBoxConfirmation" runat="server" Height="244px" Width="399px"></asp:ListBox>
                </asp:Panel>
            </div>
            
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    </div>
    </form>
</body>
</html>
