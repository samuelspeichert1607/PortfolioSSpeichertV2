using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //private List<Inscription> inscriptionsEvent1 = new List<Inscription>();

    protected void Page_Init(object sender, EventArgs e)
    {
        ////Récupération de la liste dans la session, très important
          List<Inscription> liste = (List<Inscription>)Session["liste"];
        ////Inscription itemMemoire = (Inscription)Session["liste"];

       
        
        ////On vide la liste sinon elle va dupliquer les informations
        //ListBoxConfirmation.Items.Clear();

        ////Pour chaque item trouvé dans la liste mémoire on crée un ListItem et on l'ajoute dans le 2e ListBox
        foreach (Inscription item in liste)
        {
            ListItem listItem = new ListItem();
            listItem.Value = item.GetId().ToString();
            listItem.Text = item.GetGame() + " " + item.GetStartTime();
            ListBoxConfirmation.Items.Add(listItem);
        }

        //Pas besoin de restocker la liste elle n'a pas été changée, seulement lue !
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

      

    }

    

        
}