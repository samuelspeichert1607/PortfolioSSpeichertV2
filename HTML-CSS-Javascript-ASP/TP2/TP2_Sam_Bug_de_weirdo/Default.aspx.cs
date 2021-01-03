using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    /*Inclure ici les autres propritétés comme les tableaux de strings pour le contenu
     la gestion des id selon la date de l'événement, etc...*/

    /*Les listes qui serviront à gérer les validation et faciliter les validations, voir documentation pour méthodes et propriétés utiles*/
    /*https://msdn.microsoft.com/fr-ca/library/6sh2ey19(v=vs.110).aspx*/
    private List<Inscription> inscriptionsEvent1 = new List<Inscription>();
    private List<Inscription> inscriptionsEvent2 = new List<Inscription>();
    private List<Inscription> inscriptionsEvent3 = new List<Inscription>();


    /*Les 3 dates des événements, à votre discrétion*/
    DateTime d1 = new DateTime(2016, 3, 11);
    DateTime d2 = new DateTime(2016, 3, 18);
    DateTime d3 = new DateTime(2016, 3, 25);

    protected void Page_Init(object sender, EventArgs e)
    {
        //Il faut faire ce code seulement à l'initalisation sans quoi on écrasera notre variable de session
        if (!IsPostBack)
        {
            //On stocke la liste en partant dans la session ce qui est primordial lors de la première récupération
            //On aurait donc pu omettre la propriété et créer une liste ici, car dorénavant on ne travaillera que sur la session
            Session["liste"] = inscriptionsEvent1;
        }
    }
    /// <summary>
    /// Méthode qui est appelée au chargement de la page
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        /************
        *DANIELH: J'ai utilisé un style CSS directement au lieu de passer directement par la propriété ASP
        ************/
        if (!IsPostBack)
        {
            PanelFormInscription.Style.Add("display", "none");
        }
    }

    /// <summary>
    /// Méthode qui est appelée lorsqu'une date de calendrier a été cliquée
    /// </summary>
    /// <param name="sender">L'objet qui a initié l'événement</param>
    /// <param name="e">Données disponibles lors de la réception de l'événement</param>
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        //Button btnClic = (Button)sender;

        
        Inscription evenement  = new Inscription(0, "Soirée Nintendo", 16, 18, "G-159", false);
        Inscription evenement2 = new Inscription(0, "Soirée Sega", 18, 22, "G-152", false);
        Inscription evenement3 = new Inscription(0, "Soirée PlayStation", 17, 20, "G-151", false);
        
        /************
        *DANIELH: J'ai utilisé un style CSS directement au lieu de passer directement par la propriété ASP
        ************/
        //PanelFormInscription.Visible = true;
        PanelFormInscription.Style.Add("display", "normal");      
        
        
        foreach (DateTime day in Calendar1.SelectedDates)
        {
            //Label1.Text += d1;
            if (d1 == day)
            {
                Label1.Text = evenement.GetGame();
            }
            if (d2 == day)
            {
                Label1.Text = evenement2.GetGame();
            }
            if (d3 == day)
            {
                Label1.Text = evenement3.GetGame();
            }
        }

       // 

        
    }

    /// <summary>
    /// Méthode appelée avant l'affichage de chaque date du calendrier
    /// </summary>
    /// <param name="sender">Objet qui a demandé l'affichage du calendrier</param>
    /// <param name="e">Les données disponibles pour chaque date</param>
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.Date.CompareTo(DateTime.Today) < 0)
        {
            e.Day.IsSelectable = false;
        }

        e.Day.IsSelectable = false;

        if (e.Day.Date.ToShortDateString() == d1.ToShortDateString() ||
            e.Day.Date.ToShortDateString() == d2.ToShortDateString() ||
            e.Day.Date.ToShortDateString() == d3.ToShortDateString())
        {
            e.Day.IsSelectable = true;
            PanelFormInscription.Visible = true;
        }
    }
    protected void ButtonReserver_Click(object sender, EventArgs e)
    {

    }
    protected void ButtonSoumettre_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            //Le OnClick sera lancé quand les validations seront finis
            Server.Transfer("Default.aspx");
            //Response.Redirect("Default.aspx"); //Ne respawn pas le formulaire précédent.
        }
    }
}