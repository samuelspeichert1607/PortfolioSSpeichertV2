<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inscription.aspx.cs" Inherits="_Default" %>

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
            <div class="linkItem"><a id="activemenulink" href="Index.html">Accueil</a></div>
            <div class="linkItem"><a href="inscription.html">Inscription</a></div>
            <div class="linkItem"><a href="contact.html">???</a></div>
            <div class="linkItem"><a href="jeux.html">???</a></div>
            <div class="clear"></div>
        </div>
        <div id="contenu">
            <p class="styleparagraphe1">Bienvenue</p>
            <div>
                <asp:Panel ID="Panel1" runat="server" Height="274px">
                    <asp:Label ID="Label1" runat="server" Text="Veuillez choisir l'évènement"></asp:Label>
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" OnSelectionChanged="Calendar1_SelectionChanged" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth" OnDayRender="Calendar1_DayRender">
                        <DayHeaderStyle ForeColor="#333333" Height="8pt" Font-Bold="True" Font-Size="8pt" />
                        <DayStyle BackColor="#CCCCCC" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" Font-Bold="True" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="#333399" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" BorderStyle="Solid" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    </asp:Calendar>            
                </asp:Panel>
            </div>
            
            <asp:Panel ID="PanelFormInscription" runat="server" Height="386px" Visible="False">
                <asp:Label ID="LabelEvenement" runat="server" Text="Nom de l'évènement"></asp:Label>
                <br />
                <asp:Label ID="LabelJeuxDispo" runat="server" Text="Jeux disponibles"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>Splatoon</asp:ListItem>
                    <asp:ListItem>Super Smash Bros Melee</asp:ListItem>
                    <asp:ListItem>Mario Party 3</asp:ListItem>
                    <asp:ListItem>Wii Sports Resort</asp:ListItem>
                    <asp:ListItem>Super Mario Kart</asp:ListItem>
                    <asp:ListItem>Contra</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>G-151</asp:ListItem>
                    <asp:ListItem>G-152</asp:ListItem>
                    <asp:ListItem>G-159</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList3" runat="server">
                    <asp:ListItem>16h00</asp:ListItem>
                    <asp:ListItem>17h00</asp:ListItem>
                    <asp:ListItem>18h00</asp:ListItem>
                    <asp:ListItem>19h00</asp:ListItem>
                    <asp:ListItem>20h00</asp:ListItem>
                    <asp:ListItem>21h00</asp:ListItem>
                    <asp:ListItem>22h00</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="ButtonReserver" runat="server" Text="Réserver" OnClick="ButtonReserver_Click" />
                <br />
                
                <asp:Label ID="LabelConfDispo" runat="server" Text="Conférences disponibles"></asp:Label>
                

                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Melee is love" />
                <br />
                
                <asp:CheckBox ID="CheckBox2" runat="server" Text="Trop de FPS?" />
                
                <br />
                
                <asp:CheckBox ID="CheckBox3" runat="server" Text="Beer Mario Kart" />
                
                <br />
                
                <asp:Label ID="LabelInscriptions" runat="server" Text="Mes inscriptions"></asp:Label>
                
                <br />
                <asp:ListBox ID="ListBoxInscriptions" runat="server">
                    <asp:ListItem>Ta mère</asp:ListItem>
                </asp:ListBox>
                <br />
                <asp:Button ID="ButtonRetirer" runat="server" Text="Retirer" />
                <br />
                Prénom :
                <asp:TextBox ID="TextBoxPrenom" runat="server"></asp:TextBox>
                <br />
                Nom :
                <asp:TextBox ID="TextBoxNom" runat="server"></asp:TextBox>
                
                <br />
                Matricule :
                <asp:TextBox ID="TextBoxMatricule" runat="server"></asp:TextBox>
                
            </asp:Panel>
            
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    </div>
    </form>
</body>
</html>
