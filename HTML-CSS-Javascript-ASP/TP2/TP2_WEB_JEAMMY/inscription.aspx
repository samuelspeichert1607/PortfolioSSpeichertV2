<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inscription.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="validations.js"></script>
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
            <p class="styleparagraphe1">
                    <asp:Label ID="Label1" runat="server" Text="Veuillez choisir l'évènement" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Larger" Font-Underline="True"></asp:Label>
                    </p>
            <div>
                <asp:Panel ID="Panel1" runat="server" Height="252px">
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
            
            <asp:Panel ID="PanelFormInscription" runat="server" Height="404px">
                <asp:Label ID="LabelEvenement" runat="server" Text="Nom de l'évènement" Font-Bold="True" Font-Size="Larger" Font-Underline="True"></asp:Label>
                <br />
                <asp:Label ID="LabelJeuxDispo" runat="server" Text="Jeux disponibles" Font-Bold="True" Font-Underline="True"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownListJeu" runat="server" OnSelectedIndexChanged="DropDownListJeu_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownListLocal" runat="server" CausesValidation="True" >
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownListHeure" runat="server" CausesValidation="True" >
                </asp:DropDownList>
                <asp:Button ID="ButtonReserver" runat="server" Text="Réserver" OnClick="ButtonReserver_Click" />
                <br />
                
                <asp:Label ID="LabelConfDispo" runat="server" Text="Conférences disponibles" Font-Bold="True" Font-Underline="True"></asp:Label>
                

                <br />

                <asp:CheckBox ID="CheckBoxConference1" runat="server" Text="Melee is love - 19h30 - G-170 - Par Maxime Morin" AutoPostBack="True" OnCheckedChanged="CheckBoxConference1_CheckedChanged" />

                <br />
                
                <asp:CheckBox ID="CheckBoxConference2" runat="server" Text="Trop de FPS? - 20h30 - G-171 - Par Daniel Moreau" AutoPostBack="True" OnCheckedChanged="CheckBoxConference2_CheckedChanged" />
                
                <br />
                
                <asp:CheckBox ID="CheckBoxConference3" runat="server" Text="Beer Mario Kart - 21h30 - G-172 - Par Adolf Bieber" AutoPostBack="True" OnCheckedChanged="CheckBoxConference3_CheckedChanged" />
                
                <br />
                
                <asp:Label ID="LabelInscriptions" runat="server" Text="Mes inscriptions" Font-Bold="True" Font-Underline="True"></asp:Label>
                
                <br />
                <asp:ListBox ID="ListBoxInscriptions" runat="server" Width="568px" Height="56px"></asp:ListBox>
                <asp:Button ID="ButtonRetirer" runat="server" Text="Retirer" OnClick="ButtonRetirer_Click" Height="19px" Width="143px" />
                <br />
                <asp:CustomValidator ID="CustomValidatorMinMaxInscriptions" runat="server" ControlToValidate="ListBoxInscriptions" ErrorMessage="Vous devez avoir au moins trois inscriptions et maximum quatres inscriptions." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorNbInscriptions_ServerValidate" ValidateEmptyText="false"></asp:CustomValidator>
                <br />
                <asp:CustomValidator ID="CustomValidatorConference" runat="server" ErrorMessage="Vous devez assister à au moins une conférence." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorConference_ServerValidate"></asp:CustomValidator>
                <br />
                <asp:CustomValidator ID="CustomValidatorConflits" runat="server" ErrorMessage="Il y a un conflit d'horaire dans vos réservations." Font-Bold="True" ForeColor="#CC0000" OnServerValidate="CustomValidatorConflits_ServerValidate"></asp:CustomValidator>
                <br />
                <asp:Label ID="LabelPrenom" runat="server" Text="Prénom :"></asp:Label>
                <asp:TextBox ID="TextBoxPrenom" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxPrenom" ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Veuillez écrire votre prénom..." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="LabelNom" runat="server" Text="Nom :"></asp:Label>
                <asp:TextBox ID="TextBoxNom" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxNom"  ID="RequiredFieldValidator2" runat="server" ErrorMessage="Veuillez écrire votre nom..." Font-Bold="True" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="LabelMatricule" runat="server" Text="Matricule :"></asp:Label>
                <asp:TextBox ID="TextBoxMatricule" runat="server" MaxLength="7" ></asp:TextBox>
                <asp:RequiredFieldValidator ValidateEmptyText="false" EnableClientScript="false" ControlToValidate="TextBoxMatricule" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Veuillez écrire votre matricule..." Font-Bold="True" ForeColor="#CC0000" ></asp:RequiredFieldValidator>
                
                &nbsp;&nbsp;
                <asp:CustomValidator ID="CustomValidatorMatricule" runat="server" ErrorMessage="7 caractères" Font-Bold="True" ForeColor="#CC0000" ValidateEmptyText="false" ></asp:CustomValidator>
                
                <br />
                <asp:Button ID="ButtonSoumettre" runat="server" Text="Soumettre" OnClick="ButtonSoumettre_Click" Height="21px" Width="70px" />
                
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="ButtonReinitialiser" runat="server" OnClick="ButtonReinitialiser_Click" Text="Reinitialiser" />
                
            </asp:Panel>
            
        </div>
        <div class="clear"></div>
        <div id="piedDePage"></div>
    </div>
    </form>
</body>
</html>
