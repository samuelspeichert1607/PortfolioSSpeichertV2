/*Voici BlackJack
 * 
 * Même si ce jeu est fait pour m'évaluer, je vous souhaite de bien vous amuser!
 * 
 * Auteur: Samuel Speichert 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TP2_Master_Lettres___Prise_2
{
    public partial class Form1 : Form
    {
        const int NB_CARTES_PAR_JOUEUR = 5;
        const int NB_COULEURS = 4;
        const int NB_CARTES_PAR_COULEUR = 13;

        int numeroCarte = 0;
        int numeroCarteAi = 0;

        int pointageDuJoueur = 0;
        int pointageDeLordi = 0;
        
        int finPartie = 0;

        float nbCartesRestantes = 52.0f;
        Random rndCartes = new Random();


        Button[] cartesDuJoueur = new Button[NB_CARTES_PAR_JOUEUR];
        int[] numeroCartesDuJoueur = new int[5];
        Button[] cartesDeLordinateur = new Button[NB_CARTES_PAR_JOUEUR];

        
        string[] nomDesCartes = new string[NB_CARTES_PAR_COULEUR * NB_COULEURS] { "As de trèfle", "2 de trèfle", "3 de trèfle", "4 de trèfle", "5 de trèfle", "6 de trèfle", "7 de trèfle", "8 de trèfle", "9 de trèfle", "10 de trèfle", "Valet de trèfle", "Dame de trèfle", "Roi de trèfle", 
                                               "As de carreau", "2 de carreau", "3 de carreau", "4 de carreau", "5 de carreau", "6 de carreau", "7 de carreau", "8 de carreau", "9 de carreau", "10 de carreau", "Valet de carreau", "Dame de carreau", "Roi de carreau", 
                                               "As de coeur", "2 de coeur", "3 de coeur", "4 de coeur", "5 de coeur", "6 de coeur", "7 de coeur", "8 de coeur", "9 de coeur", "10 de coeur", "Valet de coeur", "Dame de coeur", "Roi de coeur", 
                                               "As de pique", "2 de pique", "3 de pique", "4 de pique", "5 de pique", "6 de pique", "7 de pique", "8 de pique", "9 de pique", "10 de pique", "Valet de pique", "Dame de pique", "Roi de pique" };

        int[] valeurDesCartes = new int[NB_CARTES_PAR_COULEUR * NB_COULEURS] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 
                                                                               1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10,
                                                                               1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 
                                                                               1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };     

        public Form1()
        {
            InitializeComponent();
            ReinitialiserJeu();
        }          
        
        //Fonction ReinitialiserJeu : C'est la fonction qui va assigner des valeurs de base à des tableaux et des paramètres.
        //Aucune valeur de retour
        public void ReinitialiserJeu()
        {
            nomDesCartes = new string[NB_CARTES_PAR_COULEUR * NB_COULEURS] { "As de trèfle", "2 de trèfle", "3 de trèfle", "4 de trèfle", "5 de trèfle", "6 de trèfle", "7 de trèfle", "8 de trèfle", "9 de trèfle", "10 de trèfle", "Valet de trèfle", "Dame de trèfle", "Roi de trèfle", 
                                               "As de carreau", "2 de carreau", "3 de carreau", "4 de carreau", "5 de carreau", "6 de carreau", "7 de carreau", "8 de carreau", "9 de carreau", "10 de carreau", "Valet de carreau", "Dame de carreau", "Roi de carreau", 
                                               "As de coeur", "2 de coeur", "3 de coeur", "4 de coeur", "5 de coeur", "6 de coeur", "7 de coeur", "8 de coeur", "9 de coeur", "10 de coeur", "Valet de coeur", "Dame de coeur", "Roi de coeur", 
                                               "As de pique", "2 de pique", "3 de pique", "4 de pique", "5 de pique", "6 de pique", "7 de pique", "8 de pique", "9 de pique", "10 de pique", "Valet de pique", "Dame de pique", "Roi de pique" }; 

            pointageDuJoueur = 0;
           
            numeroCarte = 0;
            finPartie = 0;
            
            cartesDuJoueur[0] = btnLancerCarte1;
            cartesDuJoueur[1] = btnLancerCarte2;
            cartesDuJoueur[2] = btnLancerCarte3;
            cartesDuJoueur[3] = btnLancerCarte4;
            cartesDuJoueur[4] = btnLancerCarte5;
            
            cartesDuJoueur[0].Enabled = false;
            cartesDuJoueur[1].Enabled = false;
            cartesDuJoueur[2].Enabled = true;
            cartesDuJoueur[3].Enabled = false;
            cartesDuJoueur[4].Enabled = false;
            passerMain.Enabled = true;

            cartesDuJoueur[0].Text = "Lancer Carte";
            cartesDuJoueur[1].Text = "Lancer Carte";
            cartesDuJoueur[2].Text = "Lancer Carte";
            cartesDuJoueur[3].Text = "Lancer Carte";
            cartesDuJoueur[4].Text = "Lancer Carte";
            
            ChoisirCartesInitiales();

            cartesDeLordinateur[0] = btnCarteOrdi1;
            cartesDeLordinateur[1] = btnCarteOrdi2;
            cartesDeLordinateur[2] = btnCarteOrdi3;
            cartesDeLordinateur[3] = btnCarteOrdi4;
            cartesDeLordinateur[4] = btnCarteOrdi5;

            cartesDeLordinateur[0].Text = "Lancer Carte";
            cartesDeLordinateur[1].Text = "Lancer Carte";
            cartesDeLordinateur[2].Text = "Lancer Carte";
            cartesDeLordinateur[3].Text = "Lancer Carte";
            cartesDeLordinateur[4].Text = "Lancer Carte";
        }
         //Fonction ChoisirCartesInitiales : C'est la fonction qui va appeler deux fois de suite la fonction LancerCarte,
         //afin que le joueur aille deux cartes de retournées au début de la partie. 
         //Aucune valeur n'est entrée en paramètre
         //Aucune valeur de retour
        public void ChoisirCartesInitiales()
        {
            numeroCarte = 0;
            LancerCarte(numeroCarte++, 0);
            LancerCarte(numeroCarte++, 0); 
        }
        // Cartes de l'usager : Les fonctions qui gèrent le clic des boutons des cartes du joueur sont comprises dans cette région. De plus,
        // elles vont toutes activer la minuterie delaiDeRetournement en l'assignant à la valeur booléenne <<true>>
        #region Cartes de l'usager 
        private void btnLancerCarte1_Click(object sender, EventArgs e)
        {

        }

        private void btnLancerCarte2_Click(object sender, EventArgs e)
        {
          
        }

        private void btnLancerCarte3_Click(object sender, EventArgs e)
        {
            delaiDuRetournement.Enabled = true;
        }
        private void btnLancerCarte4_Click(object sender, EventArgs e)
        {
            delaiDuRetournement.Enabled = true;
        }
        private void btnLancerCarte5_Click(object sender, EventArgs e)
        {
            delaiDuRetournement.Enabled = true;  
        }
        #endregion Cartes de l'usager
        // Fonction passerMain_Click : Lorsqu'on aura appuyé dessus, cette fonction va activer le compteur de temps delaiDuRetournementPasserMain
        // en l'assignant à la valeur booléenne <<true>>.
        // Aucune valeur n'est entrée en paramètre.
        // Aucune valeur de retour
        private void passerMain_Click(object sender, EventArgs e)
        {
            delaiDuRetournementPasserMain.Enabled = true;
        }
        // Fonction MettreAJourStatistiques : Cette fonction va calculer la probabilité qu'une carte spécifique (identifiée par l'entier carteChoisie)
        //                                    soit tirée, en l'affichant à l'écran grâce à un étiquette(label) rattachée à la carte.
        //                                    
        // Il n'y a qu'une valeur rentrée en paramètre : - int carteChoisie : Il s'agit de la variable qui représente la position de la carte choisie
        //                                                 dans le tableau de chaînes nomDesCartes et le tableau d'entiers valeurDesCartes.             
        // Aucune valeur de retour.
        void MettreAJourStatistiques(int carteChoisie)
        {
            float statsAs = 4.0f;
            float stats2 = 4.0f;
            float stats3 = 4.0f;
            float stats4 = 4.0f;
            float stats5 = 4.0f;
            float stats6 = 4.0f;
            float stats7 = 4.0f;
            float stats8 = 4.0f;
            float stats9 = 4.0f;
            float stats10 = 4.0f;
            float statsValet = 4.0f;
            float statsDame = 4.0f;
            float statsRoi = 4.0f;

            //Toutes les alternatives qui vont suivre vérifient si carteChoisie, entré en paramètre,
            //est égal à l'index généré au hasard (voir la fonction LancerCarte et LancerCarteAi).
            //Elles vont aussi vérifier si le tableau de chaînes nomDesCartes, à la position de l'index entré
            //en paramètre(carteChoisie), est égale à "true"

            #region Chances de piger un as
            if (carteChoisie == 0 && nomDesCartes[0] == "Vide")
            {
                statsAs -= 1;
            }
            if (carteChoisie == 13 && nomDesCartes[13] == "Vide")
            {
                statsAs -= 1;
            }
            if (carteChoisie == 26 && nomDesCartes[26] == "Vide")
            {
                statsAs -= 1;
            }
            if (carteChoisie == 39 && nomDesCartes[39] == "Vide")
            {
                statsAs -= 1;
            }

            labelTirerAs.Text = "Probabilité de retourner un as :" + ((statsAs * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 2



            if (carteChoisie == 1 && nomDesCartes[1] == "Vide")
            {
                stats2 -= 1;
            }
            if (carteChoisie == 14 && nomDesCartes[14] == "Vide")
            {
                stats2 -= 1;
            }
            if (carteChoisie == 27 && nomDesCartes[27] == "Vide")
            {
                stats2 -= 1;
            }
            if (carteChoisie == 40 && nomDesCartes[40] == "Vide")
            {
                stats2 -= 1;
            }

            labelTirer2.Text = "Probabilité de retourner un 2 :" + ((stats2 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 3



            if (carteChoisie == 2 && nomDesCartes[2] == "Vide")
            {
                stats3 -= 1;

            }
            if (carteChoisie == 15 && nomDesCartes[15] == "Vide")
            {
                stats3 -= 1;

            }
            if (carteChoisie == 28 && nomDesCartes[28] == "Vide")
            {
                stats3 -= 1;

            }
            if (carteChoisie == 41 && nomDesCartes[41] == "Vide")
            {
                stats3 -= 1;

            }

            labelTirer3.Text = "Probabilité de retourner un 3 :" + ((stats3 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 4

            if (carteChoisie == 3 && nomDesCartes[3] == "Vide")
            {
                stats4 -= 1;

            }
            if (carteChoisie == 16 && nomDesCartes[16] == "Vide")
            {
                stats4 -= 1;

            }
            if (carteChoisie == 29 && nomDesCartes[29] == "Vide")
            {
                stats4 -= 1;

            }
            if (carteChoisie == 42 && nomDesCartes[42] == "Vide")
            {
                stats4 -= 1;

            }

            labelTirer4.Text = "Probabilité de retourner un 4 :" + ((stats4 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 5

            if (carteChoisie == 4 && nomDesCartes[4] == "Vide")
            {
                stats5 -= 1;

            }
            if (carteChoisie == 17 && nomDesCartes[17] == "Vide")
            {
                stats5 -= 1;

            }
            if (carteChoisie == 30 && nomDesCartes[30] == "Vide")
            {
                stats5 -= 1;

            }
            if (carteChoisie == 43 && nomDesCartes[43] == "Vide")
            {
                stats5 -= 1;

            }

            labelTirer5.Text = "Probabilité de retourner un 5 :" + ((stats5 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 6

            if (carteChoisie == 5 && nomDesCartes[5] == "Vide")
            {
                stats6 -= 1;

            }
            if (carteChoisie == 18 && nomDesCartes[18] == "Vide")
            {
                stats6 -= 1;

            }
            if (carteChoisie == 31 && nomDesCartes[31] == "Vide")
            {
                stats6 -= 1;

            }
            if (carteChoisie == 44 && nomDesCartes[44] == "Vide")
            {
                stats6 -= 1;

            }

            labelTirer6.Text = "Probabilité de retourner un 6 :" + ((stats6 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 7

            if (carteChoisie == 6 && nomDesCartes[6] == "Vide")
            {
                stats7 -= 1;

            }
            if (carteChoisie == 19 && nomDesCartes[19] == "Vide")
            {
                stats7 -= 1;

            }
            if (carteChoisie == 32 && nomDesCartes[32] == "Vide")
            {
                stats7 -= 1;

            }
            if (carteChoisie == 45 && nomDesCartes[45] == "Vide")
            {
                stats7 -= 1;

            }

            labelTirer7.Text = "Probabilité de retourner un 7 :" + ((stats7 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 8

            if (carteChoisie == 7 && nomDesCartes[7] == "Vide")
            {
                stats8 -= 1;

            }
            if (carteChoisie == 20 && nomDesCartes[20] == "Vide")
            {
                stats8 -= 1;

            }
            if (carteChoisie == 33 && nomDesCartes[33] == "Vide")
            {
                stats8 -= 1;

            }
            if (carteChoisie == 46 && nomDesCartes[46] == "Vide")
            {
                stats8 -= 1;

            }

            labelTirer8.Text = "Probabilité de retourner un 8 :" + ((stats8 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 9

            if (carteChoisie == 8 && nomDesCartes[8] == "Vide")
            {
                stats9 -= 1;

            }
            if (carteChoisie == 21 && nomDesCartes[21] == "Vide")
            {
                stats9 -= 1;

            }
            if (carteChoisie == 34 && nomDesCartes[34] == "Vide")
            {
                stats9 -= 1;

            }
            if (carteChoisie == 47 && nomDesCartes[47] == "Vide")
            {
                stats9 -= 1;

            }

            labelTirer9.Text = "Probabilité de retourner un 9 :" + ((stats9 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un 10

            if (carteChoisie == 9 && nomDesCartes[9] == "Vide")
            {
                stats10 -= 1;

            }
            if (carteChoisie == 22 && nomDesCartes[22] == "Vide")
            {
                stats10 -= 1;

            }
            if (carteChoisie == 35 && nomDesCartes[35] == "Vide")
            {
                stats10 -= 1;

            }
            if (carteChoisie == 48 && nomDesCartes[48] == "Vide")
            {
                stats10 -= 1;

            }

            labelTirer10.Text = "Probabilité de retourner un 10 :" + ((stats10 * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger un Valet

            if (carteChoisie == 10 && nomDesCartes[10] == "Vide")
            {
                statsValet -= 1;

            }
            if (carteChoisie == 23 && nomDesCartes[23] == "Vide")
            {
                statsValet -= 1;

            }
            if (carteChoisie == 36 && nomDesCartes[36] == "Vide")
            {
                statsValet -= 1;

            }
            if (carteChoisie == 49 && nomDesCartes[49] == "Vide")
            {
                statsValet -= 1;

            }

            labelTirerValet.Text = "Probabilité de retourner un valet :" + ((statsValet * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger une Dame

            if (carteChoisie == 11 && nomDesCartes[11] == "Vide")
            {
                statsDame -= 1;
            }
            if (carteChoisie == 24 && nomDesCartes[24] == "Vide")
            {
                statsDame -= 1;
            }
            if (carteChoisie == 37 && nomDesCartes[37] == "Vide")
            {
                statsDame -= 1;
            }
            if (carteChoisie == 50 && nomDesCartes[50] == "Vide")
            {
                statsDame -= 1;
            }

            labelTirerDame.Text = "Probabilité de retourner une dame :" + ((statsDame * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion

            #region Chances de piger une Roi

            if (carteChoisie == 12 && nomDesCartes[12] == "Vide")
            {
                statsRoi -= 1;
            }
            if (carteChoisie == 25 && nomDesCartes[25] == "Vide")
            {
                statsRoi -= 1;
            }
            if (carteChoisie == 38 && nomDesCartes[38] == "Vide")
            {
                statsRoi -= 1;
            }
            if (carteChoisie == 51 && nomDesCartes[51] == "Vide")
            {
                statsRoi -= 1;
            }

            labelTirerRoi.Text = "Probabilité de retourner un roi :" + ((statsRoi * 100.0f) / nbCartesRestantes).ToString() + " " + "%";
            #endregion
            Debug.Assert(nbCartesRestantes <= 52 || nbCartesRestantes >= 0, "nbCartesRestantes doit avoir une valeur entre 0 et 52 inclusivement.");
        }
        // Fonction LancerCarte : Cette fonction va accomplir deux tâches : elle va générer une carte aléatoire dont l'indice sera rentré
        // dans les tableaux nomDesCartes et valeurDesCartes, afin de retourner une carte et de vérifier si elle n'a pas été utilisée, et
        // pour incrémenter le pointage via pointageDuJoueur
        // Il y a deux valeurs rentrées en paramètres : - int numeroCarte : Il s'agit de l'index qui va représenter la carte non-dégrisée dans
        //                                                                  le tableau cartesDuJoueur.
        //                                              - int joueurId : Il s'agit de la variable qui va vérifier le joueur qui est en train 
        //                                                               de jouer son tour. Si cet entier est égal à 0, c'est le joueur qui
        //                                                               joue son tour. Si cet entier est égal à 1, c'est l'ordinateur qui
        //                                                               joue son tour.                             
        // Aucune valeur de retour.
        void LancerCarte(int numeroCarte, int joueurId)
        {
            Debug.Assert(numeroCarte > 0 || numeroCarte < 4, "l'indice numeroCarte n'est pas valide. Il doit être situé entre 0 et 4 inclusivement.");
            //Piger la carte et vérifier si la carte n'a pas été utilisée.
            delaiDuRetournement.Enabled = false;
            int valMin = 0;
            int valMax = 52;
            int i = 0;
            int finPartie = 0;
            int carteChoisie = 0;
            //Les intructions qui suivent vont être exécutés seulement si joueurId == 0;
            if (joueurId == 0)
            {   
                Debug.Assert(i < cartesDuJoueur.Length, "La taille du tableau cartesDuJoueur se doit d'être plus grande que l'entier i (le compteur)");
                for (i = 0; i < cartesDuJoueur.Length; i++)       //Cette boucle va répéter les actions suivantes jusqu'a ce que le compteur i soit égale à la taille du tableau cartesDuJoueur.
                {
                    Debug.Assert(valMax > valMin, "L'entier valMax se doit d'être plus grand ou égal à valMin");
                    carteChoisie = rndCartes.Next(valMin, valMax);
                    if (nomDesCartes[carteChoisie] != "Vide")  // Si le tableau de chaînes nomDesCartes n'est pas égale à "vide" (selon sa position, générée au hasard), alors elle sera assignée au texte "vide",
                            {                                  // en plus de décrémenter le réel nbCartesRestantes, de écrire du texte à l'étiquette assignée et d'appeler la fonction MettreAJourStatistiques.
                                                               // sinon, elle va simplement décrémenter le compteur i.
                                cartesDuJoueur[numeroCarte].Text = nomDesCartes[carteChoisie];
                                nomDesCartes[carteChoisie] = "Vide";
                                nbCartesRestantes -= 1;
                                MettreAJourStatistiques(carteChoisie);
                            }
                            else
                            {
                              i--;
                            }
                }

                //Gestion du pointage 
                if(carteChoisie == 0 || carteChoisie == 13 || carteChoisie == 26 || carteChoisie == 39) // Cette alternative va donne le choix à l'usager s'il désire que son pointage soit incrémenté de 1 ou 11, à la suite d'un retournement d'as. 
                {                                                                                       // Chaque indice testé (soit 0, 13, 26 et 39) est équivalent à un as dans les tableaux valeurDesCartes et nomDesCartes.
                    DialogResult pigerUnAs = MessageBox.Show("Hey! Vous avez pigé un as! Appuyez sur 'Yes' pour obtenir 11 points, ou 'No' pour obtenir 1 point.", "Un as!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (pigerUnAs == DialogResult.Yes)      
                    {
                        pointageDuJoueur += 11;  
                    }
                    if (pigerUnAs == DialogResult.No)
                    {
                        pointageDuJoueur += 1;
                    }
                }
                else    //Si ce n'est pas un as qui est pigé, alors le pointage sera incrémenté selon la valeur assignée à la position représentée par carteChoisie.
                {
                    pointageDuJoueur += valeurDesCartes[carteChoisie];
                }

                labelPointageJoueur.Text = "Pointage du joueur" + pointageDuJoueur.ToString(); 
   
                if (numeroCarte < 4 && finPartie != 1)     //Cette instruction a permis à la gestion d'un bug qui dégrisait la 3ème carte (numeroCarte) pour des raisons inconnues. Elle permet de griser la carte suivante et de dégriser celle qui est présentement activée.
                {
                    cartesDuJoueur[numeroCarte].Enabled = false;
                    cartesDuJoueur[numeroCarte + 1].Enabled = true;
                }

                if (pointageDuJoueur > 21)  
                {
                    GererFinPartie(1, 0);
                }
                
                if (numeroCarte == 2 && pointageDuJoueur > 21)       
                {
                    cartesDuJoueur[numeroCarte].Enabled = true;      
                }
                else
                {
                    cartesDuJoueur[numeroCarte].Enabled = false;
                }
            }
        }
        // Fonction LancerCarteAi : Cette fonction va accomplir deux tâches : elle va générer une carte aléatoire dont l'indice sera rentré
        // dans les tableaux nomDesCartes et valeurDesCartes, afin de retourner une carte et de vérifier si elle n'a pas été utilisée, et
        // pour incrémenter le pointage via pointageDeLordi.
        // Il y a deux valeurs rentrées en paramètres : - int numeroCarteAi : Il s'agit de l'index qui va représenter la carte non-dégrisée dans
        //                                                                    le tableau cartesDeLordinateur.
        //                                              - int joueurId : Il s'agit de la variable qui va vérifier le joueur qui est en train 
        //                                                               de jouer son tour. Si cet entier est égal à 0, c'est le joueur qui
        //                                                               joue son tour. Si cet entier est égal à 1, c'est l'ordinateur qui
        //                                                               joue son tour.                             
        // Aucune valeur de retour.
        void LancerCarteAi(int numeroCarteAi, int joueurId)
        {
            //La fonction LancerCarteAi possède des alternatives similaires, voire semblable à la fonction LancerCarte. Seules les instructions différentes seront commentées.
            Debug.Assert(numeroCarteAi > 0 || numeroCarteAi < 4, "L'indice numeroCarte n'est pas valide. Il doit être situé entre 0 et 4 inclusivement.");
            delaiDuRetournementAi.Enabled = false;
            int carteChoisieAi = 0;
            if (joueurId == 1)
            {    
                cartesDeLordinateur[numeroCarteAi].Enabled = true;
                
                for (int i = 0; i < cartesDeLordinateur.Length; i++)
                {
                    carteChoisieAi = rndCartes.Next(0, 52);
                    
                    if (nomDesCartes[carteChoisieAi] != "Vide")
                    {  
                        cartesDeLordinateur[numeroCarteAi].Text = nomDesCartes[carteChoisieAi]; //nomDesCartes[carteChoisieAi];
                        nomDesCartes[carteChoisieAi] = "Vide";
                        
                        nbCartesRestantes -= 1;
                        MettreAJourStatistiques(carteChoisieAi);
                      
                        delaiDuRetournementAi.Enabled = true;
                    }
                    else
                    {
                        i--;
                    }
                }
                if (carteChoisieAi == 0 || carteChoisieAi == 13 || carteChoisieAi == 26 || carteChoisieAi == 39) //Contrairement à l'usager qui a le choix, l'ordinateur va incrémenter son pointage de 1  
                {                                                                                                //s'il est supérieur ou égal à 11. Sinon, il sera incrémenté de 11.
                    if(pointageDeLordi >= 11)
                    {
                        pointageDeLordi += 1;
                    }
                    if (pointageDeLordi < 11)
                    {
                        pointageDeLordi += 11;
                    }
                }
                else
                {
                    pointageDeLordi += valeurDesCartes[carteChoisieAi];
                }
                
                labelPointageAi.Text = "Pointage du croupier" + pointageDeLordi.ToString();

                if (pointageDeLordi > pointageDuJoueur)    
                {
                    GererFinPartie(1, 1);
                }
            }
        }
        // Fonction GererFinPartie : Cette fonction, appelée via LancerCarte et LancerCarteAi, est la fonction qui va gérer
        // Il y a deux valeurs entrées en paramètres : - int finPartie : Il s'agit de la variable qui va vérifier si la partie est terminée
        //                                                              (= 1) ou non ( = 0) 
        //                                             - int joueurId : Il s'agit de la variable qui va vérifier le joueur qui est en train 
        //                                                               de jouer son tour. Si cet entier est égal à 0, c'est le joueur qui
        //                                                               joue son tour. Si cet entier est égal à 1, c'est l'ordinateur qui
        //                                                               joue son tour.
        // Aucune valeur de retour
        void GererFinPartie(int finPartie, int joueurId)
        {
            if (finPartie == 1 && joueurId == 0)
            {
                DialogResult finDePartie = MessageBox.Show("Awn... Vous avez perdu! Désirez-vous rejouer?", "Perdu!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (finDePartie == DialogResult.Yes)
                {
                    Application.Restart();
                }
                if (finDePartie == DialogResult.No)
                {
                    Application.Exit(); 
                }
            }
            if (finPartie == 1 && joueurId == 1)
            {
                if (pointageDeLordi > 21 || numeroCarte == 4)
                {
                    DialogResult finDePartie = MessageBox.Show(" Félicitations! Vous avez gagné! Désirez-vous rejouer?", "Youpi!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (finDePartie == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    if (finDePartie == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    DialogResult finDePartie = MessageBox.Show("Awn... Vous avez perdu! Désirez-vous rejouer?", "Perdu!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (finDePartie == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    if (finDePartie == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                if (pointageDeLordi == pointageDuJoueur)//Cette instruction teste l'égalité de points entre les deux joueurs.  
                {
                    if (numeroCarteAi < numeroCarte)  //Cette instruction si le nombre de cartes piochées par l'ordinateur est inférieur au nombre de cartes piochées par l'usager. 
                    {
                        DialogResult finDePartie = MessageBox.Show("Awn... Vous avez perdu! Désirez-vous rejouer?", "Perdu!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (finDePartie == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        if (finDePartie == DialogResult.No)
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        if (numeroCarteAi < numeroCarte)  
                        {
                            DialogResult finDePartie = MessageBox.Show(" Félicitations! Vous avez gagné! Désirez-vous rejouer?", "Youpi!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            if (finDePartie == DialogResult.Yes)
                            {
                                Application.Restart();
                            }
                            if (finDePartie == DialogResult.No)
                            {
                                Application.Exit();
                            }
                        }
                    }
                if (numeroCarteAi == numeroCarte)  //Cette instruction teste si le nombre de cartes piochées par les deux joueurs est égale.
                {
                    DialogResult finDePartie = MessageBox.Show("Égalité... Voulez-vous rejouer?", "Hum...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (finDePartie == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    if (finDePartie == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                }
            }
        }
        //Les onglets permettent à l'usager de quitter l'application ou de la réinitialiser.
        #region //Onglets principaux
        private void recommencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region //Onglets des jeux simultanés

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        #endregion
        //Les fonctions qui suivent testent les délais de retournement d'une carte, qui est de une seconde. Elles contiennent aussi les fonctions appelées et les instructions exécutées lors que les délais sont activés. 
        #region // Delais de retournements
        private void delaiDuRetournement_Tick(object sender, EventArgs e)
        {
            LancerCarte(numeroCarte++, 0);
        }
        private void delaiDuRetournementPasserMain_Tick(object sender, EventArgs e)
        {

            passerMain.Enabled = false;
            btnLancerCarte3.Enabled = false;
            btnLancerCarte4.Enabled = false;
            btnLancerCarte5.Enabled = false;

            delaiDuRetournementAi.Enabled = true;
            
        }
        private void delaiDuRetournementAi_Tick(object sender, EventArgs e)
        {
            int compteurAi = 0;
            while (compteurAi < cartesDeLordinateur.Length && pointageDeLordi < pointageDuJoueur && pointageDeLordi < 21)
            {
                LancerCarteAi(compteurAi++, 1);
            }
        }

        #endregion
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
