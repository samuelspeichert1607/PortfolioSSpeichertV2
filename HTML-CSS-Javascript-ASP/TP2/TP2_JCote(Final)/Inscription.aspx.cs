using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Site web conçu par Jeammy Côté et Samuel Speichert.
public partial class _Default : System.Web.UI.Page
{
    //tableau des information personnel de l'utilisateur. (nom, prenom, matricule)
    string[] infoPerso = new string[3] { " ", " ", " " };
    /*Les listes qui serviront à gérer les validation et faciliter les validations, voir documentation pour méthodes et propriétés utiles*/
    /*https://msdn.microsoft.com/fr-ca/library/6sh2ey19(v=vs.110).aspx */
    private List<Inscription> inscriptionsEvent1 = new List<Inscription>();
    private List<Inscription> inscriptionsEvent2 = new List<Inscription>();
    private List<Inscription> inscriptionsEvent3 = new List<Inscription>();
    /*Les 3 dates des événements, à votre discrétion*/
    DateTime d1 = new DateTime(2016, 3, 11);
    DateTime d2 = new DateTime(2016, 3, 18);
    DateTime d3 = new DateTime(2016, 3, 25);
    private int currentId = 0;
    //l'id du checkBox à supprimer
    int[] checkBoxId = new int[3]{0,0,0};
    /// <summary>
    /// Méthode qui est appelée au chargement de la page
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        //Il faut faire ce code seulement à l'initalisation sans quoi on écrasera notre variable de session
        if (!IsPostBack)
        {
            Session["listeEvent1"] = inscriptionsEvent1;
            Session["listeEvent2"] = inscriptionsEvent2;
            Session["listeEvent3"] = inscriptionsEvent3;
            Session["infoPerso"] = infoPerso;
            Session["id"] = currentId;
        }
    }
    /// <summary>
    /// Méthode qui est appelée lorsqu'une date de calendrier a été cliquée
    /// </summary>
    /// <param name="sender">L'objet qui a initié l'événement</param>
    /// <param name="e">Données disponibles lors de la réception de l'événement</param>
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Inscription evenement1 = new Inscription(0, "Soirée Nintendo - 11 mars 2016", 16, 18,"G152", false);
        Inscription evenement2 = new Inscription(0, "Soirée Doom - 18 mars 2016", 16, 18, "G152", false);
        Inscription evenement3 = new Inscription(0, "Soirée Tetrusse - 25 mars 2016", 16, 18, "G152", false);
        PanelInscription.Visible = true;
        foreach (DateTime day in Calendar1.SelectedDates)
        {
            if(d1 == day)
            {
                EventName.Text = evenement1.GetGame();
                inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                //Session["listeEvent1"] = inscriptionsEvent1;
                Session["listeActive"] = Session["listeEvent1"];
            }
            if(d2==day)
            {
                EventName.Text = evenement2.GetGame();
                inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                //Session["listeEvent2"] = inscriptionsEvent2;
                Session["listeActive"] = Session["listeEvent2"];
            }
            if (d3 == day)
            {
                EventName.Text = evenement3.GetGame();
                inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                //Session["listeEvent3"] = inscriptionsEvent3;
                Session["listeActive"] = Session["listeEvent3"];
            }
        }
        ListBoxInscriptions.Items.Clear();
        CheckBoxConference1.Checked = false;
        CheckBoxConference2.Checked = false;
        CheckBoxConference3.Checked = false;

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
    /// <summary>
    /// Méthode qui est appelée lors du clic du bouton réservation.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void ButtonReservation_Click(object sender, EventArgs e)
    {
        if(!String.IsNullOrEmpty(DropDownListGame.Text))
        {
            ListItem item = new ListItem(); //Instance de l'item ajouté dans la listBox.
            Inscription newInscription = new Inscription(currentId, DropDownListGame.Text, int.Parse(DropDownListHour.Text), int.Parse(DropDownListHour.Text)+1, 
              DropDownListLocal.Text, false);
            int Id = (int)Session["id"]; //Création d'une session stockant les ids
            item.Value = Id.ToString();
            item.Text = DropDownListHour.Text + ":00:00" + " - " + Calendar1.SelectedDate + " - " + DropDownListGame.Text + " - " + DropDownListLocal.Text;
            
            Id++;
            Session["id"] = Id;
            ListBoxInscriptions.Items.Add(item);

            foreach (DateTime day in Calendar1.SelectedDates) //Cette boucle foreach va déterminer si, dépendant de la journée selectionnée, 
            {                                                 //dans quelle liste (évènement)on va ajouter une inscription.
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.Add(newInscription); 
                    Session["listeEvent1"] = inscriptionsEvent1; 
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.Add(newInscription);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.Add(newInscription);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }
        }
    
    }
    /// <summary>
    /// Méthode qui est appelée lors du cochage du CheckBoxConference1.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void CheckBoxConference_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference1.Text;
       
        
        
        Inscription newInscription = new Inscription(currentId, "L'art du Telefrag", 18, 19, "J-216", true);

        if(CheckBoxConference1.Checked == false)
        {
            ListBoxInscriptions.Items.Remove(item);

            foreach (DateTime day in Calendar1.SelectedDates) //Retrait d'une conférence dans la liste selon la journée sélectionnée.
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.RemoveAt(checkBoxId[0]);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.RemoveAt(checkBoxId[0]);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.RemoveAt(checkBoxId[0]);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }
        }
        else
        {
            ListBoxInscriptions.Items.Add(item);
            checkBoxId[0] = currentId;
            currentId++;

            foreach (DateTime day in Calendar1.SelectedDates) //Ajout d'une conférence dans la liste selon la journée sélectionnée.
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.Add(newInscription);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.Add(newInscription);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.Add(newInscription);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }
        }
    }
    /// <summary>
    /// Méthode qui est appelée lors du cochage du CheckBoxConference2.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void CheckBoxConference2_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference2.Text;
        
        Inscription newInscription = new Inscription(currentId, "Le T-Bag pour les nuls", 19, 20, "J-117", true);

        if (CheckBoxConference2.Checked == false)
        {
            ListBoxInscriptions.Items.Remove(item);
            
            foreach (DateTime day in Calendar1.SelectedDates)
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.RemoveAt(checkBoxId[1]);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.RemoveAt(checkBoxId[1]);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.RemoveAt(checkBoxId[1]);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }

        }
        else
        {
            ListBoxInscriptions.Items.Add(item);


            currentId++;


            foreach (DateTime day in Calendar1.SelectedDates)
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.Add(newInscription);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.Add(newInscription);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.Add(newInscription);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }
        }
    }
    /// <summary>
    /// Méthode qui est appelée lors du cochage du CheckBoxConference3.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void CheckBoxConference3_CheckedChanged(object sender, EventArgs e)
    {
        ListItem item = new ListItem();
        item.Value = currentId.ToString();
        item.Text = CheckBoxConference3.Text;
        
        Inscription newInscription = new Inscription(currentId, " Le Monster Kill au bon moment ", 20, 21, "J-218", true);

        if (CheckBoxConference3.Checked == false)
        {
            ListBoxInscriptions.Items.Remove(item);

            foreach (DateTime day in Calendar1.SelectedDates)
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.RemoveAt(checkBoxId[2]);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.RemoveAt(checkBoxId[2]);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.RemoveAt(checkBoxId[2]);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }

        }
        else
        {
            ListBoxInscriptions.Items.Add(item);


            currentId++;


            foreach (DateTime day in Calendar1.SelectedDates)
            {
                if (d1 == day)
                {
                    inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
                    inscriptionsEvent1.Add(newInscription);
                    Session["listeEvent1"] = inscriptionsEvent1;
                }
                if (d2 == day)
                {
                    inscriptionsEvent2 = (List<Inscription>)Session["listeEvent2"];
                    inscriptionsEvent2.Add(newInscription);
                    Session["listeEvent2"] = inscriptionsEvent2;
                }
                if (d3 == day)
                {
                    inscriptionsEvent3 = (List<Inscription>)Session["listeEvent3"];
                    inscriptionsEvent3.Add(newInscription);
                    Session["listeEvent3"] = inscriptionsEvent3;
                }
            }
        }
    }
    /// <summary>
    /// Méthode qui est appelée lors d'un clic du bouton retirer.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        if(ListBoxInscriptions.SelectedIndex != -1)
        {
            inscriptionsEvent1 = (List<Inscription>)Session["listeEvent1"];
            inscriptionsEvent1.RemoveAt(ListBoxInscriptions.SelectedIndex);
            
            ListBoxInscriptions.Items.RemoveAt(ListBoxInscriptions.SelectedIndex);
            Session["listeEvent1"] = inscriptionsEvent1;
        }
        
    }
    /// <summary>
    /// Méthode qui est appelée lors d'un changement d'index dans la liste déroulante ListGame.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void DropDownListGame_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownListGame.SelectedIndex == 0)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-151");
            DropDownListLocal.Items.Add("G-152");
            DropDownListLocal.Items.Add("G-153");
        }
        else if (DropDownListGame.SelectedIndex == 1)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-154");
            DropDownListLocal.Items.Add("G-155");
            DropDownListLocal.Items.Add("G-156");
        }
        else if (DropDownListGame.SelectedIndex == 2)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-157");
            DropDownListLocal.Items.Add("G-158");
            DropDownListLocal.Items.Add("G-159");
        }
        else if (DropDownListGame.SelectedIndex == 3)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-160");
            DropDownListLocal.Items.Add("G-161");
            DropDownListLocal.Items.Add("G-162");
        }
        else if (DropDownListGame.SelectedIndex == 4)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-163");
            DropDownListLocal.Items.Add("G-164");
            DropDownListLocal.Items.Add("G-165");
        }
        else if (DropDownListGame.SelectedIndex == 5)
        {
            DropDownListLocal.Items.Clear();
            DropDownListLocal.Items.Add("G-164");
            DropDownListLocal.Items.Add("G-165");
            DropDownListLocal.Items.Add("G-166");
        }

    }

    
    /// <summary>
    /// Méthode qui est appelée lors d'un clic du bouton Soumettre.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        
        infoPerso = (string[])Session["infoPerso"];
        infoPerso[0] = TextBoxPrenom.Text;
        infoPerso[1] = TextBoxNom.Text;
        infoPerso[2] = TextBoxMatricule.Text;

        if (Page.IsValid && RequiredFieldValidator1.IsValid && RequiredFieldValidator2.IsValid && RequiredFieldValidator3.IsValid 
            && CustomValidatorMinMaxInscriptions.IsValid && CustomValidatorConference.IsValid && CustomValidatorConflits.IsValid)
        {
            Session["infoPerso"] = infoPerso;
            Response.Redirect("Confirmation.aspx");
        }
    }
    /// <summary>
    /// Méthode qui est appelée lors de la validation-serveur ratachée au ListBoxInscriptions.
    /// </summary>
    /// <param name="source">instance de la source</param>
    /// <param name="args">L'objet ou notre décision(isValid) est affectée.</param>
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
    /// <summary>
    /// Méthode qui est appelée lors de la validation-serveur qui s'assure s'il y a au moins une conférence de cochée.
    /// </summary>
    /// <param name="source">instance de la source</param>
    /// <param name="args">L'objet ou notre décision(isValid) est affectée.</param>
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
    /// <summary>
    /// Méthode qui est appelée lors de la validation-serveur ratachée au ListBoxInscriptions, afin de s'assurer qu'aucun conflit n'est présent.
    /// </summary>
    /// <param name="source">instance de la source</param>
    /// <param name="args">L'objet ou notre décision(isValid) est affectée.</param>
    protected void CustomValidatorConflits_ServerValidate(object source, ServerValidateEventArgs args)
    {
        List<Inscription> validateHoursList1 = new List<Inscription>();
        validateHoursList1 = (List<Inscription>)Session["listeEvent1"];

        List<Inscription> validateHoursList2 = new List<Inscription>();
        validateHoursList2 = (List<Inscription>)Session["listeEvent2"];

        List<Inscription> validateHoursList3 = new List<Inscription>();
        validateHoursList3 = (List<Inscription>)Session["listeEvent3"];

        for (int i = 0; i < validateHoursList1.Count -1; i++) //Les boucles imbriquées qui suivent comparent chacun des heures des inscriptions de la liste.
        {
            for (int j = 0; j < validateHoursList1.Count -1; j++)
            {
                if (validateHoursList1[i].GetStartTime() == validateHoursList1[j].GetStartTime())
                {
                    args.IsValid = true;
                    break;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }
        for (int i = 0; i < validateHoursList2.Count - 1; i++)
        {
            for (int j = 0; j < validateHoursList2.Count - 1; j++)
            {
                if (validateHoursList2[i].GetStartTime() == validateHoursList2[j].GetStartTime())
                {
                    args.IsValid = true;
                    break;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }
        for (int i = 0; i < validateHoursList3.Count - 1; i++)
        {
            for (int j = 0; j < validateHoursList3.Count - 1; j++)
            {
                if (validateHoursList3[i].GetStartTime() == validateHoursList3[j].GetStartTime())
                {
                    args.IsValid = true;
                    break;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }
    }
    /// <summary>
    /// Méthode qui est appelée lors d'un clic du bouton Réinitialiser.
    /// </summary>
    /// <param name="sender">L'objet qui a demandé le chargement de la page</param>
    /// <param name="e">données disponibles au chargement de la page</param>
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        CheckBoxConference1.Checked = false;
        CheckBoxConference2.Checked = false;
        CheckBoxConference3.Checked = false;
        ListBoxInscriptions.Items.Clear();
        currentId = 0;

        List<Inscription> resetInscriptionsEvent1 = new List<Inscription>();
        List<Inscription> resetInscriptionsEvent2 = new List<Inscription>();
        List<Inscription> resetInscriptionsEvent3 = new List<Inscription>();

        string[] resetInfoPerso = new string[3] { " ", " ", " " };

        Session["listeEvent1"] = inscriptionsEvent1;
        Session["listeEvent2"] = inscriptionsEvent2;
        Session["listeEvent3"] = inscriptionsEvent3;
        Session["infoPerso"] = resetInfoPerso;
        Session["id"] = 0;
    }
    
}