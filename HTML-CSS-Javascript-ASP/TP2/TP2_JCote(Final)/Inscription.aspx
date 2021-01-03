<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inscription.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/Style.css" rel="stylesheet" />
    <script src="js/Validation.js"></script>
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
            <div class="linkItem"><a href="Index.aspx">Accueil</a></div>
            <div class="linkItem"><a id="activemenulink" href="Inscription.aspx">Inscription</a></div>
            <div class="clear"></div>
        </div>
        <div id="contenu">
            <div>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" OnSelectionChanged="Calendar1_SelectionChanged" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth" OnDayRender="Calendar1_DayRender">
                        <DayHeaderStyle ForeColor="#333333" Height="8pt" Font-Bold="True" Font-Size="8pt" />
                        <DayStyle BackColor="#CCCCCC" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" Font-Bold="True" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="#333399" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" BorderStyle="Solid" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    </asp:Calendar>            
                    <br />
                    <asp:Panel ID="PanelInscription" runat="server" Visible="False">
                        <asp:Label ID="EventName" runat="server" Text="Nom de l'évenement" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        <br />
                        <asp:Label ID="LabelAvailGames" runat="server" Text="Jeux disponibles" Font-Bold="True"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DropDownListGame" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListGame_SelectedIndexChanged">
                            <asp:ListItem Value="Smash">Smash</asp:ListItem>
                            <asp:ListItem Value="Splatoon">Splatoon</asp:ListItem>
                            <asp:ListItem Value="Doom">Doom</asp:ListItem>
                            <asp:ListItem Value="Street Fighter V">Street Fighters V</asp:ListItem>
                            <asp:ListItem Value="Tetris">Tetris</asp:ListItem>
                            <asp:ListItem Value="TrackMania turbo">TrackMania turbo</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownListLocal" runat="server" AutoPostBack="True">
                            <asp:ListItem>G-151</asp:ListItem>
                            <asp:ListItem>G-152</asp:ListItem>
                            <asp:ListItem>G-153</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DropDownListHour" runat="server">
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:Button ID="ButtonReservation" runat="server" Text="Réserver" OnClick="ButtonReservation_Click" />
                        <br />
                        <br />
                        <asp:Label ID="LabelConference" runat="server" Text="Conférences disponibles" Font-Bold="True"></asp:Label>
                        <br />
                        <asp:CheckBox ID="CheckBoxConference1" runat="server" Text="18:30:00 L'art du Telefrag (par Daniel Huot, local J-216)" OnCheckedChanged="CheckBoxConference_CheckedChanged" AutoPostBack="True" />
                        <br />
                        <asp:CheckBox ID="CheckBoxConference2" runat="server" Text="19:30:00 Le T-Bag pour les nuls (par Sgt. Johnson, local J-117) **Présence incertaine" AutoPostBack="True" OnCheckedChanged="CheckBoxConference2_CheckedChanged" />
                        <br />
                        <asp:CheckBox ID="CheckBoxConference3" runat="server" Text="20:30:00 Le Monster Kill au bon moment (par Raynor, local J-218)" AutoPostBack="True" OnCheckedChanged="CheckBoxConference3_CheckedChanged" />
                        <br />
                        <br />
                        <asp:Label ID="LabelMesInscriptions" runat="server" Text="Mes inscriptions" Font-Bold="True" Font-Overline="False" Font-Underline="True"></asp:Label>
                        <br />
                        <asp:ListBox ID="ListBoxInscriptions" runat="server" Height="100px" Width="560px" ></asp:ListBox>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonRemove" runat="server" Text="Retirer" OnClick="ButtonRemove_Click" />
                        <br />
                        <asp:CustomValidator ID="CustomValidatorMinMaxInscriptions" runat="server" ControlToValidate="ListBoxInscriptions" ErrorMessage="Vous devez avoir au moins trois inscriptions et maximum quatres inscriptions." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorNbInscriptions_ServerValidate"></asp:CustomValidator>
                        <br />
                        <asp:CustomValidator ID="CustomValidatorConference" runat="server" ErrorMessage="Vous devez assister à au moins une conférence." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorConference_ServerValidate"></asp:CustomValidator>
                        <br />
                        <asp:CustomValidator ID="CustomValidatorConflits" runat="server" ErrorMessage="Il y a un conflit d'horaire dans vos réservations." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorConflits_ServerValidate"></asp:CustomValidator>
                        <br />
                        <asp:Label ID="LabelPrenom" runat="server" Text="Prénom :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBoxPrenom" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxPrenom" ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Veuillez écrire votre prénom." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="LabelNom" runat="server" Text="Nom :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBoxNom" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxNom"  ID="RequiredFieldValidator2" runat="server" ErrorMessage="Veuillez écrire votre nom." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="LabelMatricule" runat="server" Text="Matricule :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBoxMatricule" runat="server" MaxLength="7"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxMatricule" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Veuillez écrire votre matricule..." Font-Bold="True" ForeColor="#CC0000" ></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidatorMatricule" runat="server" ErrorMessage="Un minimum de sept caractères est requis." Font-Bold="True" ForeColor="#CC0000" ValidateEmptyText="false" OnClientValidationFunction="ClientValidate" ></asp:CustomValidator>
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonSubmit" runat="server" Text="Soumettre" OnClick="ButtonSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonReset" runat="server" Text="Réinitialiser" OnClick="ButtonReset_Click" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    </div>
    </form>
</body>
</html>
