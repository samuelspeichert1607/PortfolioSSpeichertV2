<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
