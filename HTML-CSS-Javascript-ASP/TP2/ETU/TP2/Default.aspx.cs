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
    
    /// <summary>
    /// Méthode qui est appelée au chargement de la page
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Méthode qui est appelée lorsqu'une date de calendrier a été cliquée
    /// </summary>
    /// <param name="sender">L'objet qui a initié l'événement</param>
    /// <param name="e">Données disponibles lors de la réception de l'événement</param>
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        
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
        }
    }
}