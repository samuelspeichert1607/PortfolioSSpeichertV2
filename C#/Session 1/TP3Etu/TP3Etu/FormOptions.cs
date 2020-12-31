using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP3ProfGame
{
    public partial class FormOptions : Form
    {
        public TP3ProfGame.FormJeuPrincipal Jeu = new TP3ProfGame.FormJeuPrincipal();



        public FormOptions()
        {
            InitializeComponent();
        }
        //Fonction SetNombreLignesOptions : Cette fonction configure l'entier nouveauNbLignesDansTableauJeu, via le compteur numérique numericUpDownLignes 
        //Paramètres rentrés : - int nouveauNbLignesDansTableauJeu : C'est le nombre de lignes modifié, via le compteur numérique numericUpDownLignes 
        //
        //
        //Aucune valeur de retour.
        public void SetNombreLignesOptions(int nouveauNbLignesDansTableauJeu)
        {
            numericUpDownLignes.Value = nouveauNbLignesDansTableauJeu;
        }
        //Fonction GetNombreLignesOptions : Cette fonction, en plus de convertir en entier la valeur du compteur numérique numericUpDownLignes, va retourner
        // cette valeur convertie pour qu'elle soit transférable dans le formulaire de jeu.
        //Paramètres rentrés : - int nouveauNbLignesDansTableauJeu : C'est le nombre de ligne modifié, via le compteur numérique numericUpDownLignes
        //
        //
        //Cette fonction va retourner l'entier lignesEntier.
        public int GetNombreLignesOptions()
        {
            int lignesEntier = Decimal.ToInt32(numericUpDownLignes.Value);   //Cet entier est le résultat de la conversion en entier de la valeur du compteur numérique numericUpDownLignes.
            return lignesEntier;
        }
        //Fonction SetNombreColonnesOptions : Cette fonction configure l'entier nouveauNbColonnesDansTableauJeu, via le compteur numérique numericUpDownColonnes 
        //Paramètres rentrés : - int nouveauNbColonnesDansTableauJeu : C'est le nombre de colonnes modifié, via le compteur numérique numericUpDownColonnes
        //
        //
        //Aucune valeur de retour.
        public void SetNombreColonnesOptions(int nouveauNbColonnesDansTableauJeu)
        {
            numericUpDownColonnes.Value = nouveauNbColonnesDansTableauJeu;
        }
        //Fonction GetNombreColonnesOptions : Cette fonction, en plus de convertir en entier la valeur compteur numérique numericUpDownColonnes, va retourner
        // cette valeur convertie pour qu'elle soit transférable dans le formulaire de jeu.
        //Paramètres rentrés : - int nouveauNbColonnesDansTableauJeu : C'est le nombre de colonnes modifié, via le compteur numérique numericUpDownColonnes
        //
        //
        //Cette fonction va retourner l'entier colonnesEntier.
        public int GetNombreColonnesOptions()
        {
            int colonnesEntier = Decimal.ToInt32(numericUpDownLignes.Value);    //Cet entier est le résultat de la conversion en entier de la valeur du compteur numérique numericUpDownColonnes.
            return colonnesEntier;
        }

        //Fonction trackBar1_Scroll : Cette fonction va afficher dans la boite de texte textBoxRayon la valeur de la glissière trackBar1.
        //Aucun paramètre rentré manuellement.
        //Aucune valeur de retour.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBoxRayon.Text = trackBar1.Value.ToString();
        }

        //Fonction SetRayonGrenadeOptions : Cette fonction configure l'entier NOUVEAU_RAYON_GRENADE, via la glissière trackBar1.
        //Paramètres rentrés : - int NOUVEAU_RAYON_GRENADE : C'est le rayon d'action, via le compteur numérique numericUpDownLignes 
        //
        //
        //Aucune valeur de retour. 
        public void SetRayonGrenadeOptions(int NOUVEAU_RAYON_ACTION)
        {
            trackBar1.Value = NOUVEAU_RAYON_ACTION; 
        }
        //Fonction GetRayonGrenadeOptions : Cette fonction va retourner la valeur de la glissère trackBar1 pour qu'elle soit transférable dans le formulaire de jeu.
        //Paramètres rentrés : Aucun paramètre rentré.
        //Cette fonction va retourner la valeur de la glissère trackBar1.
        public int GetRayonGrenadeOptions()
        {
            return trackBar1.Value;
        }
        //Fonction SetSonActiveOptions : Cette fonction configure le booléen nouveauSonActive, via la case checkBoxSon.
        //Paramètres rentrés : - bool nouveauSonActive : C'est le booléen, dont sa valeur (true ou false)
        // est déterminée via la case checkBoxSon. 
        //
        //
        //Aucune valeur de retour. 
        public void SetSonActiveOptions(bool nouveauSonActive)
        {
            checkBoxSon.Checked = nouveauSonActive;
        }
        //Fonction GetRayonGrenadeOptions : Cette fonction va retourner la valeur booléenne de la case checkBoxSon
        // pour qu'elle soit transférable dans le formulaire de jeu.
        //Paramètres rentrés : Aucun paramètre rentré.
        //Cette fonction va retourner la valeur booléenne de checkBoxSon.
        public bool GetSonActiveOptions()
        {
            return checkBoxSon.Checked;
        }

       

    }
}
