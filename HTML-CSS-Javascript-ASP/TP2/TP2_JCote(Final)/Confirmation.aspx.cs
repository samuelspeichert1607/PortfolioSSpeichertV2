using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Confirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    /// <summary>
    /// Méthode qui est appelée lors de l'initialisation du formulaire Confirmation.apsx.cs.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] infoPerso = (string[])Session["infoPerso"];
            for (int i = 0; i < infoPerso.Length -1; i++)
            {
                LabelInfoPerso.Text += infoPerso[i] + " ";
            } 
            List<Inscription> inscriptionEvent1 = (List<Inscription>)Session["listeEvent1"];
            List<Inscription> inscriptionEvent2 = (List<Inscription>)Session["listeEvent2"];
            List<Inscription> inscriptionEvent3 = (List<Inscription>)Session["listeEvent3"];
            foreach (Inscription newInscription in inscriptionEvent1) //Créer les lignes et les cellules du tableau en ajoutant les données enregistrées dans la liste inscriptionEvent1. 
            {
                TableRow row = new TableRow();
                TableCell cellEvent = new TableCell();
                cellEvent.Text = "Soirée Nintendo";
                row.Cells.Add(cellEvent);
                TableCell cellDate = new TableCell();
                cellDate.Text = "11/03/2016";
                row.Cells.Add(cellDate);
                TableCell cellHour = new TableCell();
                cellHour.Text = newInscription.GetStartTime().ToString()+"H";
                row.Cells.Add(cellHour);
                TableCell cellGame = new TableCell();
                cellGame.Text = newInscription.GetGame();
                row.Cells.Add(cellGame);
                TableCell cellLocal = new TableCell();
                cellLocal.Text = newInscription.GetLocal();
                row.Cells.Add(cellLocal);

                TableConfirm.Rows.Add(row);
            }
            foreach (Inscription newInscription in inscriptionEvent2) //Créer les lignes et les cellules du tableau en ajoutant les données enregistrées dans la liste inscriptionEvent2.
            {
                TableRow row = new TableRow();
                TableCell cellEvent = new TableCell();
                cellEvent.Text = "Soirée Doom";
                row.Cells.Add(cellEvent);
                TableCell cellDate = new TableCell();
                cellDate.Text = "18/03/2016";
                row.Cells.Add(cellDate);
                TableCell cellHour = new TableCell();
                cellHour.Text = newInscription.GetStartTime().ToString();
                row.Cells.Add(cellHour);
                TableCell cellGame = new TableCell();
                cellGame.Text = newInscription.GetGame();
                row.Cells.Add(cellGame);
                TableCell cellLocal = new TableCell();
                cellLocal.Text = newInscription.GetLocal();
                row.Cells.Add(cellLocal);

                TableConfirm.Rows.Add(row);
            }
            foreach (Inscription newInscription in inscriptionEvent3) //Créer les lignes et les cellules du tableau en ajoutant les données enregistrées dans la liste inscriptionEvent3.
            {
                TableRow row = new TableRow();
                TableCell cellEvent = new TableCell();
                cellEvent.Text = "Soirée Tetrusse";
                row.Cells.Add(cellEvent);
                TableCell cellDate = new TableCell();
                cellDate.Text = "25/03/2016";
                row.Cells.Add(cellDate);
                TableCell cellHour = new TableCell();
                cellHour.Text = newInscription.GetStartTime().ToString();
                row.Cells.Add(cellHour);
                TableCell cellGame = new TableCell();
                cellGame.Text = newInscription.GetGame();
                row.Cells.Add(cellGame);
                TableCell cellLocal = new TableCell();
                cellLocal.Text = newInscription.GetLocal();
                row.Cells.Add(cellLocal);

                TableConfirm.Rows.Add(row);
            }
        }
    }
}