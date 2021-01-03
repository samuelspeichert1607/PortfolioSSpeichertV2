<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="inscription" %>

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
            <div id="enteteDroite"><img src="img/logo.jpg" style="height: 200px; width: 362px" /></div>
            <div id="enteteGauche"><h1>Friendly FryDays</h1></div>
        </div>
        
        <div class="clear"></div>
        <div id="menuVertical">
            <div class="linkItem"><a id="activemenulink" href="Index.aspx">Accueil</a></div>
            <div class="linkItem"><a href="Inscription.aspx">Inscription</a></div>
            <div class="clear"></div>
        </div>
        <div id="contenu">
            <p class="styleparagraphe1">&nbsp;</p>
            <div style="height: 659px">
                
                <asp:Label ID="LabelBienvenue" runat="server" Font-Bold="True" Font-Size="Larger" Font-Underline="True" Text="Bienvenue mesdames et gamers,"></asp:Label>
                <br />
                <br />
                Voici le FriendlyFryDays. Mais qu&#39;est-ce que c&#39;est? Il s&#39;agit d&#39;un évènement ayant lieu<br />
                à tous les vendredis soir au Cégep de Ste-Foy, de 16h à 23h. À cet évènement, vous pouvez y jouer à toute sortes de jeux vidéo, qu&#39;ils soient rétros ou récents, pour le plaisir ou pour la compétition! Des conférences reliés à certains jeux sont aussi données.<br />
                <br />
                Il suffit de s&#39;inscrire pour y participer, et on vous attends donc en grand nombre!</div>
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    </div>
    </form>
</body>
</html>
