<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/Style.css" rel="stylesheet" />
    <style type="text/css">
        #table {
            height: 34px;
            width: 318px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="container">
        <div id="entete">
            <div id="enteteDroite"><img src="img/logo.jpg" style="height: 200px; width: 362px" /></div>
            <div id="enteteGauche"><h1 style="margin-left: 0px">Friendly FryDays</h1></div>
        </div>
        
        <div class="clear"></div>
        <div id="menuVertical">
            <div class="linkItem"><a href="Index.aspx">Accueil</a></div>
            <div class="linkItem"><a href="Inscription.aspx">Inscription</a></div>
            <div class="clear"></div>
        </div>
        <div id="contenu">
            <p class="styleparagraphe1">Confirmation des inscriptions</p>
            <p class="styleparagraphe1">
                <asp:Label ID="Label1" runat="server" Text="Félicitations pour votre inscription, "></asp:Label>
                <asp:Label ID="LabelInfoPerso" runat="server"></asp:Label>
            &nbsp;!</p>
            <p class="styleparagraphe1">
                &nbsp;</p>
            <div>
                <asp:Table ID="TableConfirm" runat="server" BorderStyle="Groove" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell>Évènement</asp:TableCell>
                        <asp:TableCell>Date</asp:TableCell>
                        <asp:TableCell>Heure</asp:TableCell>
                        <asp:TableCell>Jeu/Conférence</asp:TableCell>
                        <asp:TableCell>Local</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    
    </div>
    </form>
</body>
</html>
