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
     private int currentId = 0;

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
            Session["id"] = currentId;
        }
    }
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
        //Button btnClic = (Button)sender;

        
        //Inscription evenement  = new Inscription(0, "Soirée Jeux de combat", "16", "18", "G-159", false);
        //Inscription evenement2 = new Inscription(0, "Soirée FPS", 18, 22, "G-152", false);
        //Inscription evenement3 = new Inscription(0, "Soirée Party games", 17, 20, "G-151", false);
        PanelFormInscription.Visible = true;
        foreach (DateTime day in Calendar1.SelectedDates)
        {
            //Label1.Text += d1;
            if (d1 == day)
            {
                Label1.Text = "Soirée Jeux de combat";
                DropDownListJeu.Items.Clear();
                DropDownListJeu.Items.Add(new ListItem("Super Smash Bros. Melee", "Super Smash Bros. Melee"));
                DropDownListJeu.Items.Add(new ListItem("Killer Instinct", "Killer Instinct"));
                DropDownListJeu.Items.Add(new ListItem("Street Fighter V", "Street Fighter V"));
                DropDownListJeu.Items.Add(new ListItem("Mortal Kombat X", "Mortal Kombat X"));
                DropDownListJeu.Items.Add(new ListItem("Dead Or Alive 5", "Dead Or Alive 5"));
                DropDownListJeu.Items.Add(new ListItem("DBZ : Budokai Tenkaitchi 3", "DBZ : Budokai Tenkaitchi 3"));

                DropDownListLocal.Items.Clear();
                DropDownListLocal.Items.Add(new ListItem("G-151", "G-151"));
                DropDownListLocal.Items.Add(new ListItem("G-152", "G-152"));
                DropDownListLocal.Items.Add(new ListItem("G-153", "G-153"));

                DropDownListHeure.Items.Clear();
                DropDownListHeure.Items.Add(new ListItem("16h", "16h"));
                DropDownListHeure.Items.Add(new ListItem("17h", "17h"));
                DropDownListHeure.Items.Add(new ListItem("18h", "18h"));
                DropDownListHeure.Items.Add(new ListItem("19h", "19h"));
                DropDownListHeure.Items.Add(new ListItem("20h", "20h"));
                DropDownListHeure.Items.Add(new ListItem("21h", "21h"));
                DropDownListHeure.Items.Add(new ListItem("22h", "22h"));

                    
            }
            if (d2 == day)
            {
                Label1.Text = "Soirée FPS";
                DropDownListJeu.Items.Clear();
            }
            if (d3 == day)
            {
                Label1.Text = "Soirée Party games";
                DropDownListJeu.Items.Clear();
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
        if (!String.IsNullOrEmpty(DropDownListJeu.Text))
        {
            //Création du ListItem
            ListItem item = new ListItem();
            item.Value = currentId.ToString();
            item.Text = d1.ToShortDateString() + " " + DropDownListHeure.Text + " " + DropDownListLocal.Text + " " + DropDownListJeu.Text;

            //On ajoute l'item dans la première liste de l'interface
            ListBoxInscriptions.Items.Add(item);
            //On retire de la liste l'heure de la réservation, ce qui évite de nombreux conflits d'horaires
              //DropDownListHeure.Items.RemoveAt(DropDownListHeure.SelectedIndex);

            if (DropDownListHeure.Items.Count == 0)
            {
                ButtonReserver.Enabled = false;
            }
            else
            {
                ButtonReserver.Enabled = true;
            }

            

            //On doit absolument récuperer la liste de la session car elle est à jour !!
            List<Inscription> liste = (List<Inscription>)Session["liste"];
            currentId = (int)Session["id"]; 
            
            
            //On crée notre item en mémoire
            Inscription itemMemoire = new Inscription(currentId, DropDownListJeu.Text, DropDownListHeure.Text, DropDownListHeure.Text, DropDownListLocal.Text, false);

            //on travaille sur la liste récupérée !
            liste.Add(itemMemoire);

            //important sans quoi tout le monde aura le même id, mais voir mon commentaire dans les propriétés !
            currentId++;

            //on restocke la liste manipulée dans la mémoire
            Session["liste"] = liste;
            Session["id"] = currentId;
            //Session["liste"] = currentId;
            //Session["liste"] = itemMemoire;
            

        }
    }
    protected void ButtonSoumettre_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && RequiredFieldValidator1.IsValid && RequiredFieldValidator2.IsValid && RequiredFieldValidator3.IsValid && CustomValidatorMinMaxInscriptions.IsValid && CustomValidatorConference.IsValid && CustomValidatorConflits.IsValid)
        {
            List<Inscription> liste = (List<Inscription>)Session["liste"];
            Response.Redirect("confirmation.aspx");
        }
    }
    protected void CheckBoxConference1_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference1.Text;
        
        if (CheckBoxConference1.Checked == true)
        {
            ListBoxInscriptions.Items.Add(item);

            //On doit absolument récuperer la liste de la session car elle est à jour !!
            List<Inscription> liste = (List<Inscription>)Session["liste"];

            //important sans quoi tout le monde aura le même id, mais voir mon commentaire dans les propriétés ! 
            currentId++;

            //On crée notre item en mémoire
            Inscription itemMemoire = new Inscription(currentId, CheckBoxConference1.Text, DropDownListHeure.Text, DropDownListHeure.Text, DropDownListLocal.Text, true);

            //on travaille sur la liste récupérée !
            liste.Add(itemMemoire);

            //on restocke la liste manipulée dans la mémoire
            Session["liste"] = liste;
        }
        else
        {
            ListBoxInscriptions.Items.Remove(item);
        }
    }   
    protected void CheckBoxConference2_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference2.Text;

        if (CheckBoxConference2.Checked == true)
        {
            ListBoxInscriptions.Items.Add(item);

            //On doit absolument récuperer la liste de la session car elle est à jour !!
            List<Inscription> liste = (List<Inscription>)Session["liste"];

            //important sans quoi tout le monde aura le même id, mais voir mon commentaire dans les propriétés !
            currentId++;

            //On crée notre item en mémoire
            Inscription itemMemoire = new Inscription(currentId, CheckBoxConference2.Text, DropDownListHeure.Text, DropDownListHeure.Text, DropDownListLocal.Text, true);

            //on travaille sur la liste récupérée !
            liste.Add(itemMemoire);

            

            //on restocke la liste manipulée dans la mémoire
            Session["liste"] = liste;
        }
        else
        {
            ListBoxInscriptions.Items.Remove(item);
        }
    }    
    protected void CheckBoxConference3_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference3.Text;


        if (CheckBoxConference3.Checked == true)
        {
            ListBoxInscriptions.Items.Add(item);


            //On doit absolument récuperer la liste de la session car elle est à jour !!
            List<Inscription> liste = (List<Inscription>)Session["liste"];
                                
            //important sans quoi tout le monde aura le même id, mais voir mon commentaire dans les propriétés !
            currentId++;

            //On crée notre item en mémoire
            Inscription itemMemoire = new Inscription(currentId, CheckBoxConference3.Text, DropDownListHeure.Text, DropDownListHeure.Text, DropDownListLocal.Text, true);

            //on travaille sur la liste récupérée !
            liste.Add(itemMemoire);

            //on restocke la liste manipulée dans la mémoire
            Session["liste"] = liste;
        }
        else
        {
            ListBoxInscriptions.Items.Remove(item);
        }
    }
    protected void ButtonRetirer_Click(object sender, EventArgs e)
    {
        if (ListBoxInscriptions.SelectedIndex != -1)
        {
            ListBoxInscriptions.Items.RemoveAt(ListBoxInscriptions.SelectedIndex);
            //ListBoxInscriptions.Items.RemoveAt(int.Parse(ListBoxInscriptions.SelectedItem.Value));
        }
        //DropDownListHeure.Items.Add(new ListItem(DropDownListHeure.Text, DropDownListHeure.Text));
    }
    protected void DropDownListJeu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListJeu.SelectedIndex == 0)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-151", "G-151"));
            DropDownListLocal.Items.Add(new ListItem("G-152", "G-152"));
            DropDownListLocal.Items.Add(new ListItem("G-153", "G-153"));
        }
        else if (DropDownListJeu.SelectedIndex == 1)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-154", "G-154"));
            DropDownListLocal.Items.Add(new ListItem("G-155", "G-155"));
            DropDownListLocal.Items.Add(new ListItem("G-156", "G-156"));
        }
        else if (DropDownListJeu.SelectedIndex == 2)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-157", "G-157"));
            DropDownListLocal.Items.Add(new ListItem("G-158", "G-158"));
            DropDownListLocal.Items.Add(new ListItem("G-159", "G-159"));
        }
        else if (DropDownListJeu.SelectedIndex == 3)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-160", "G-160"));
            DropDownListLocal.Items.Add(new ListItem("G-161", "G-161"));
            DropDownListLocal.Items.Add(new ListItem("G-162", "G-162"));
        }
        else if (DropDownListJeu.SelectedIndex == 4)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-163", "G-163"));
            DropDownListLocal.Items.Add(new ListItem("G-164", "G-164"));
            DropDownListLocal.Items.Add(new ListItem("G-165", "G-165"));
        }
        else if (DropDownListJeu.SelectedIndex == 5)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add(new ListItem("G-166", "G-166"));
            DropDownListLocal.Items.Add(new ListItem("G-167", "G-167"));
            DropDownListLocal.Items.Add(new ListItem("G-168", "G-168"));
        }
    }

    protected void CustomValidatorNbInscriptions_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ListBoxInscriptions.Items.Count < 3 || ListBoxInscriptions.Items.Count > 4)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }



    protected void CustomValidatorConference_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (CheckBoxConference1.Checked == true || CheckBoxConference2.Checked == true || CheckBoxConference3.Checked == true)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
    protected void CustomValidatorConflits_ServerValidate(object source, ServerValidateEventArgs args)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference1.Text;


    //    int selectionCount = 0;
    //    foreach (string items in ListBoxInscriptions.Items)
    //    {
    //        if (items)
    //        {
    //            selectionCount++;
    //        }
    //    }
    //args.IsValid = (selectionCount == 2);


        //int occurence = 0;
        //int[] tableauDheures = new int[7] { 16, 17, 18, 19, 20, 21, 22 };
        //for (int i = 0; i < tableauDheures.Length; i++)
        //{
        //    if (tableauDheures[i] == tableauDheures[i-1])
        //    {
        //        occurence++;
        //    }
        //}


       

 
    }
    protected void ButtonReinitialiser_Click(object sender, EventArgs e)
    {
        CheckBoxConference1.Checked = false;
        CheckBoxConference2.Checked = false;
        CheckBoxConference3.Checked = false;
        ListBoxInscriptions.Items.Clear();
        currentId = 0;
    }
}