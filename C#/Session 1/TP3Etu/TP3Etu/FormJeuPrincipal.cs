// ppoulin
/*Voici mon jeu de Battleship. Le but du jeu est de démolir les trois bateaux de taille différentes. Des armes différentes seront à votre disposition afin de mettre
 * en oeuvre vos nombreux déluges! Parmi ces armes, il y a :
 * - Le canon, qui endommage seulement une partie du bateau. 
 * - La grenade, qui fait un tir en forme de X (le rayon de la grenade peux être déterminé dans les paramètres).
 * - Le laser, celui-là va faire un tir en diagonale, en horizontale ou à la verticale selon la surface de la carte.
 * - La bombe, qui fait un tir en forme de carré (le rayon de la bombe peux être déterminé dans les paramètres).
 * - Le cocktail molotov, qui peut brûler le bateau en entier une fois qu'il en touche un.
 * 
 * De plus, le joueur a la possibilité de recommencer la partie, de quitter, ainsi que de changer les paramètres
 * du jeu, c'est-à-dire qu'il peux activer/désactiver le son, changer le nombre de lignes et le nombre de colonnes de la carte,
 * ainsi que de changer le rayon d'action de la bombe et de la grenade. Des statistiques sur la progression de naufrages des bateaux
 * seront affichés, ainsi que le nombre de tirs faits.
 * 
 * Les sont ont étés pris sur www.soundbible.com/ 
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
using System.Media;

namespace TP3ProfGame
{
    public partial class FormJeuPrincipal : Form
    {

        #region Variables partagées
        SoundPlayer player = new SoundPlayer();   //Cette variable va être utilisée lors de la gestion des sons.
        int nbLignesDansTableauDeJeu = 16;        //Cet entier représente le nombre de lignes du tableau.
        int nbColonnesDansTableauDeJeu = 16;      //Cet entier représente le nombre de colonnes du tableau.
        bool sonActive = true;                    //Ce booléen est true si le son du jeu est activé (voir formulaire options pour le désactiver).
        int RAYON_ACTION = 3;

        int coupsReussis = 0; //Entier représentant les dommages infligés, pour la valeur de la barre de précision.

        const int TAILLE_PETIT_BATEAU = 3;  //Le petit bateau sera toujours de taille 3.
        const int TAILLE_MOYEN_BATEAU = 5;  //Le moyen bateau sera toujours de taille 5.
        const int TAILLE_GRAND_BATEAU = 7;  //Le grand bateau sera toujours de taille 7.

        int nbBouletsLancees = 0;     //Cette variable, utilisée pour les statistiques, représente le nombre de boulets lancés.
        int nbGrenadesLancees = 0;    //Cette variable, utilisée pour les statistiques, représente le nombre de grenades lancées.
        int nbLasersLances = 0;       //Cette variable, utilisée pour les statistiques, représente le nombre de lasers lancés.
        int nbBombesLancees = 0;      //Cette variable, utilisée pour les statistiques, représente le nombre de bombes lancés.
        int nbCocktailsLances = 0;    //Cette variable, utilisée pour les statistiques, représente le nombre de cocktails lancés.

        enum TypeBateau { AUCUN_BATEAU, PETIT_BATEAU, MOYEN_BATEAU, GRAND_BATEAU, BATEAU_DEMOLI };
        TypeBateau[,] tableauLogique = null;
        #endregion

        // Représentation visuelle du jeu. Ce tableau est généré par la méthode 
        // ConstruireTableauJeu. Vous n'avez pas à comprendre tout le code fourni
        // par cette méthode.  Par contre, je vous invite à y jeter un coup d'oeil
        // pour voir comment on peut générer des contrôles par programmation.
        PictureBox[,] toutesImagesVisuelles = null;

        public FormJeuPrincipal()
        {
            InitializeComponent();
            InitialiserBateaux();
        }

        // Fonction InitialiserBateaux : Cette fonction va accomplir plusieurs tâches : Non seulement elle va réinitialiser les variables partagées lorsqu'une
        // partie est recommencé, mais elle va aussi sélectionner aléatoirement l'emplacement des trois bateaux, en plus de les positionner de manière
        //horizontale ou verticale.
        // Aucun paramètre                       
        // Aucune valeur de retour.
        public void InitialiserBateaux()
        {

            RAYON_ACTION = GetRayonGrenade(RAYON_ACTION);
            nbLignesDansTableauDeJeu = GetNbLignes(nbLignesDansTableauDeJeu);
            nbColonnesDansTableauDeJeu = GetNbColonnes(nbColonnesDansTableauDeJeu);
            sonActive = GetSonActive(sonActive);
            tableauLogique = new TypeBateau[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
            Debug.Assert(nbLignesDansTableauDeJeu == nbColonnesDansTableauDeJeu, "Le tableau doit avoir le même nombre de lignes et de colonnes");
            Random rnd = new Random();     //Déclaration du générateur d'aléatoire.
            int positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - 1);       //C'est la position générée aléatoirement d'un bateau, au niveau des lignes.
            int positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - 1);  //C'est la position générée aléatoirement d'un bateau, au niveau des colonnes.
            bool caseEstValide = true; //C'est le booléen qui déterminera si un bateau peu être placé ou non sur la case.
            int caseSwitch = rnd.Next(1, 3);   //Cet entier va déterminer aléatoirement la valeur du switch ci-dessous.
            switch (caseSwitch)    //Cet alternative "switch" va choisir aléatoirement entre différents cas. Si égale à 1, alors le bateau sera placé horizontalement. Si égale à 2, verticalement.
            {
                case 1:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - 1);
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - TAILLE_PETIT_BATEAU);
                    for (int i = 0; i < TAILLE_PETIT_BATEAU; i++)       //Ce compteur "pour" va placer des morceaux de petits bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] == TypeBateau.AUCUN_BATEAU)   //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire, positionColonnesAleatoire + i);
                        }
                        
                        if (caseEstValide == true)    //Si la fonction VerifierBateaux retourne true, alors les instructions à l'intérieur seront placés.
                        {
                           tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] = TypeBateau.PETIT_BATEAU;
                        }
                    }
                    break;
                case 2:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - TAILLE_PETIT_BATEAU);  
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - 1);
                    for (int i = 0; i < TAILLE_PETIT_BATEAU; i++)          //Ce compteur "pour" va placer des morceaux de petits bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] == TypeBateau.AUCUN_BATEAU)    //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire + i, positionColonnesAleatoire);
                        }

                        if (caseEstValide == true)
                        {
                            tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] = TypeBateau.PETIT_BATEAU;
                        }
                    }
                    break;
            }
            int caseSwitch2 = rnd.Next(1, 3);  //Cet entier sera assigné à une valeur aléatoire. Si égale à 1, alors le bateau sera placé horizontalement. Si égale à 2, verticalement.
            switch (caseSwitch2)
            {
                case 1:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - 1);
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - TAILLE_MOYEN_BATEAU);
                    for (int i = 0; i < TAILLE_MOYEN_BATEAU; i++)  //Ce compteur "pour" va placer des morceaux de moyens bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] == TypeBateau.AUCUN_BATEAU)   //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire, positionColonnesAleatoire + i);

                        }

                        if (caseEstValide == true)
                        {
                            tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] = TypeBateau.MOYEN_BATEAU;
                        }
                    }
                    break;
                case 2:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - TAILLE_MOYEN_BATEAU);
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - 1);
                    for (int i = 0; i < TAILLE_MOYEN_BATEAU; i++)  //Ce compteur "pour" va placer des morceaux de moyens bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] == TypeBateau.AUCUN_BATEAU)    //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire + i, positionColonnesAleatoire);

                        }

                        if (caseEstValide == true)
                        {
                            tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] = TypeBateau.MOYEN_BATEAU;
                        }
                    }
                    break;
            }
            int caseSwitch3 = rnd.Next(1, 3);   //Cet entier sera assigné à une valeur aléatoire. Si égale à 1, alors le bateau sera placé horizontalement. Si égale à 2, verticalement.
            switch (caseSwitch3)
            {
                case 1:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - 1);
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - TAILLE_GRAND_BATEAU);
                    for (int i = 0; i < TAILLE_GRAND_BATEAU; i++)     //Ce compteur "pour" va placer des morceaux de grands bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] == TypeBateau.AUCUN_BATEAU)  //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire, positionColonnesAleatoire + i);

                        }

                        if (caseEstValide == true)
                        {
                            tableauLogique[positionLigneAleatoire, positionColonnesAleatoire + i] = TypeBateau.GRAND_BATEAU;
                        }
                    }
                    break;
                case 2:
                    positionLigneAleatoire = rnd.Next(0, nbLignesDansTableauDeJeu - TAILLE_GRAND_BATEAU);
                    positionColonnesAleatoire = rnd.Next(0, nbColonnesDansTableauDeJeu - 1);
                    for (int i = 0; i < TAILLE_GRAND_BATEAU; i++)             //Ce compteur "pour" va placer des morceaux de grands bateaux jusqu'a ce qu'il en aille égal à la taille spécifiée.
                    {
                        if (tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] == TypeBateau.AUCUN_BATEAU)  //Cette alternative va faire appel à une fonction si un aucun bateau n'est positionné à cet endroit.
                        {
                            caseEstValide = VerifierBateaux(positionLigneAleatoire + i, positionColonnesAleatoire);

                        }

                        if (caseEstValide == true)
                        {
                            tableauLogique[positionLigneAleatoire + i, positionColonnesAleatoire] = TypeBateau.GRAND_BATEAU;
                        }
                    }
                    break;
            }
        }


        // Fonction VerifierBateaux : Cette fonction va s'assurer que le bateau ne va pas se juxtaposer sur un autre bateau, grâce au booléen
        //  estValide.
        // Paramètres entrés : - int nouvellePositionLigneAleatoire : il s'agit de la nouvelle coordonnée en ligne, modifiée via InitialiserBateau grâce
        //                       à un Pour.    
        //                     - int nouvellePositionColonneAleatoire : il s'agit de la nouvelle coordonnée en colonne, modifiée via InitialiserBateau grâce
        //                       à un Pour.      
        // Le booléen retourné est : estValide
        private bool VerifierBateaux(int nouvellePositionLigneAleatoire, int nouvellePositionColonneAleatoire)
        {
            bool estValide = true; //C'est le booléen qui sera retourné. Il deviendra équivalent à la coordonnée égalisée.
            estValide = tableauLogique[nouvellePositionLigneAleatoire, nouvellePositionColonneAleatoire] == TypeBateau.AUCUN_BATEAU;
            return estValide;
        }

        /// <summary>
        /// Méthode appelée lorsqu'un PictureBox de la surface du jeu est cliqué.
        /// </summary>
        /// <param name="sender">Aucun paramètre.</param>
        /// <param name="e">Aucune valeur de retour.</param>

        public void ConstruireTableauJeu()
        {
            // Remettre le table layout dans son état initial
            tableauJeuOrdi.SuspendLayout();
            tableauJeuOrdi.Controls.Clear();
            tableauJeuOrdi.ColumnStyles.Clear();
            tableauJeuOrdi.RowStyles.Clear();



            // Spécifier les paramètres du table layout
            tableauJeuOrdi.ColumnCount = nbColonnesDansTableauDeJeu;
            tableauJeuOrdi.RowCount = nbLignesDansTableauDeJeu;

            for (int i = 0; i < tableauJeuOrdi.RowCount; i++)
            {
                RowStyle rs = new RowStyle(SizeType.Percent, 100 / tableauJeuOrdi.RowCount);
                tableauJeuOrdi.RowStyles.Add(rs);
            }

            for (int i = 0; i < tableauJeuOrdi.ColumnCount; i++)
            {
                ColumnStyle rs = new ColumnStyle(SizeType.Percent, 100 / tableauJeuOrdi.ColumnCount);
                tableauJeuOrdi.ColumnStyles.Add(rs);
            }



            // Ajouter les PictureBox dans le table layout
            for (int ligne = 0; ligne < nbLignesDansTableauDeJeu; ligne++)
            {
                for (int colonne = 0; colonne < nbColonnesDansTableauDeJeu; colonne++)
                {
                    // Création dynamique des PictureBox qui contiendront les pièces de jeu
                    PictureBox newPictureBox = new PictureBox();
                    newPictureBox.Width = 64;
                    newPictureBox.Height = 64;
                    newPictureBox.Dock = DockStyle.Fill;
                    newPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    newPictureBox.Margin = new Padding(0, 0, 0, 0);
                    // Attention au format du texte associé au PictureBox.  Il est toujours
                    // sous le format Ligne;Colonne

                    newPictureBox.Text = ligne + ";" + colonne;
                    newPictureBox.Click += new EventHandler(OnPictureBoxClicked);
                    newPictureBox.MouseEnter += new EventHandler(tableauJeuOrdi_MouseEnter);
                    newPictureBox.MouseLeave += new EventHandler(tableauJeuOrdi_MouseLeave);
                    toutesImagesVisuelles[ligne, colonne] = newPictureBox;


                    // Ajout dynamique du PictureBox créé dans la grille de mise en forme.
                    // A noter que l' "origine" du repère dans le tableau est en haut à gauche.
                    // C'est pour cela qu'il est nécessaire de faire le calcul ci-après
                    tableauJeuOrdi.Controls.Add(newPictureBox, colonne, ligne);
                }
            }
            tableauJeuOrdi.ResumeLayout();
        }
        // Fonction OnPictureBoxClicked : Cette fonction va appeler la fonction TraiterClicSurPictureBox lorsqu'une
        //picturebox est cliquée
        // Aucun paramètre entré manuellement.                      
        // Aucune valeur de retour.
        private void OnPictureBoxClicked(object sender, EventArgs e)
        {
            TraiterClicSurPictureBox(sender as PictureBox);
        }
        // Fonction tableauJeuOrdi_MouseEnter : Cette fonction va appeler la fonction TraiterCurseurSurPictureBox lorsque
        // le curseur rentre d'un pictureBox.
        // Aucun paramètre entré manuellement.                      
        // Aucune valeur de retour.
        private void tableauJeuOrdi_MouseEnter(object sender, EventArgs e)
        {
            TraiterCurseurSurPictureBox(sender as PictureBox);
        }
        // Fonction tableauJeuOrdi_MouseLeave : Cette fonction va appeler la fonction TraiterCurseurHorsDuPictureBox 
        //lorsque le curseur sort d'un pictureBox.
        // Aucun paramètre entré manuellement.                       
        // Aucune valeur de retour.
        private void tableauJeuOrdi_MouseLeave(object sender, EventArgs e)
        {
            TraiterCurseurHorsDuPictureBox(sender as PictureBox);
        }
        //Fonction TraiterClicSurPictureBox: Cette fonction, en plus de gérer le clic sur un pictureBox, va convertir 
        // les coordonnées en texte du pictureBox cliquée en deux sous-chaînes, qui seront à leur tour convertis en
        // entier, afin d'être plus facilement manipulables. De plus, cette fonction va faire appel à une fonction de
        // lancement d'arme correspondante au radio button coché (ex : si radioButtonBoulet.Checked == true, alors la
        // fonction LancerBouletCanon sera lancée)
        //Paramètre rentré :  PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        // ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicSurPictureBox.             
        //Aucune valeur de retour.
        private void TraiterClicSurPictureBox(PictureBox pictureBoxCliquee)
        {
            // Utilisez la propriété .Text pour obtenir sous la forme Ligne;Colonne les coordonnées du PictureBox cliqué.

            System.Diagnostics.Debug.WriteLine("Le PictureBox " + pictureBoxCliquee.Text + " a été cliqué.");

            string coordonneesChaine = pictureBoxCliquee.Text;   //C'est les coordonnées du pictureboxCliquée, qui a été mise en chaine de caractères.
            int pointVirgule = coordonneesChaine.IndexOf(";");    //Cet entier va extraire le point-virgule de la chaine de caractère ci-dessus.
            string chaineLigneCliquee = coordonneesChaine.Substring(0, pointVirgule); //La sous-chaine égale à la coordonnée en ligne est extraite.
            string chaineColonneCliquee = coordonneesChaine.Substring(pointVirgule + 1, coordonneesChaine.Length - 1 - pointVirgule); //La sous-chaine égale à la coordonnée en colonne est extraite.
            int colonneCliquee = int.Parse(chaineColonneCliquee);  //La sous-chaine égale à la coordonnée en ligne extraite est convertie en entier.
            int ligneCliquee = int.Parse(chaineLigneCliquee);      //La sous-chaine égale à la coordonnée en colonne extraite est convertie en entier.

            if (radioButtonBoulet.Checked == true)    //Si le radioButton associé au boulet de cannon est cochée, alors la fonction LancerBoulet sera appelée.
            {
                LancerBouletCanon(pictureBoxCliquee, colonneCliquee, ligneCliquee);
            }
            if (radioButtonGrenade.Checked == true)   //Si le radioButton associé à la grenade est cochée, alors la fonction LancerGrenade sera appelée.
            {
                LancerGrenade(pictureBoxCliquee, colonneCliquee, ligneCliquee);
            }
            if (radioButtonLaser.Checked == true)   //Si le radioButton associé au laser est cochée, alors la fonction LancerLaser sera appelée.
            {
                LancerLaser(pictureBoxCliquee, colonneCliquee, ligneCliquee);
            }
            if (radioButtonBombe.Checked == true)   //Si le radioButton associé à la bombe est cochée, alors la fonction LancerBombe sera appelée.
            {
                LancerBombe(pictureBoxCliquee, colonneCliquee, ligneCliquee);
            }
            if (radioButtonCocktail.Checked == true)   //Si le radioButton associé au cocktail Molotov est cochée, alors la fonction LancerCocktail sera appelée.
            {
                LancerCocktail(pictureBoxCliquee, colonneCliquee, ligneCliquee);
            }


        }
        //Fonction TraiterCurseurSurPictureBox: Cette fonction, en plus de gérer le positionnement du curseur sur un pictureBox, va convertir 
        // les coordonnées en texte du pictureBox cliquée en deux sous-chaînes, qui seront à leur tour convertis en
        // entier, afin d'être plus facilement manipulables. De plus, cette fonction va colorier une ou des cases
        // qui seront atteintes par une arme, (ex : si radioButtonBoulet.Checked == true, alors une seule case sera
        // coloriée vue qu'il y en aura qu'une seule d'atteinte par cette arme.
        //Paramètre rentré :  PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        // ConstruireTableauJeu, qui sera traitée dans la fonction TraiterCurseurSurPictureBox.             
        //Aucune valeur de retour.
        private void TraiterCurseurSurPictureBox(PictureBox pictureBoxCliquee)
        {
            PictureBox pictureBoxTemporaire = new PictureBox();   //Ce pictureBox temporaire sert, lors du soulignement des endroits ou l'on peut cliquer pour lancer le laser, à créer un deuxième curseur. 

            pictureBoxTemporaire = pictureBoxCliquee;

            string coordonneesChaine = pictureBoxCliquee.Text;  //C'est les coordonnées du pictureboxCliquée, qui a été mise en chaine de caractères.
            int pointVirgule = coordonneesChaine.IndexOf(";");  //Cet entier va extraire le point-virgule de la chaine de caractère ci-dessus.
            string chaineColonneCliquee = coordonneesChaine.Substring(0, pointVirgule);  //La sous-chaine égale à la coordonnée en colonne est extraite.
            string chaineLigneCliquee = coordonneesChaine.Substring(pointVirgule + 1, coordonneesChaine.Length - 1 - pointVirgule);  //La sous-chaine égale à la coordonnée en ligne est extraite.
            int colonneCliquee = int.Parse(chaineColonneCliquee);    //La sous-chaine égale à la coordonnée en colonne extraite est convertie en entier.
            int ligneCliquee = int.Parse(chaineLigneCliquee);       //La sous-chaine égale à la coordonnée en ligne extraite est convertie en entier.


            if (radioButtonBoulet.Checked == true)    //Vérifie si le "radio button" relié au boulet est coché.
            {
                if (colonneCliquee < tableauLogique.Length && ligneCliquee < tableauLogique.Length && colonneCliquee >= 0 && ligneCliquee >= 0)    //Cet alternative vérifie si l'emplacement du curseur est valide, c'est-à-dire dans le tableau généré.
                {
                    pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee, ligneCliquee];
                    pictureBoxCliquee.BackColor = Color.Black;
                }
            }
            if (radioButtonGrenade.Checked == true)  //Vérifie si le "radio button" relié à la grenade est coché.
            {
                for (int i = 0; i < RAYON_ACTION; i++) //Ce compteur "Pour" va colorier les cases visées par la grenade, jusqu'a ce que le rayon d'action soit atteint.
                {
                    if ((colonneCliquee - i >= 0) && (ligneCliquee - i >= 0)) //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement.                          
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee - i];
                        pictureBoxCliquee.BackColor = Color.DarkGreen;
                    }
                    if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - i >= 0))   //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee - i];
                        pictureBoxCliquee.BackColor = Color.DarkGreen;
                    }
                    if ((colonneCliquee - i >= 0) && (ligneCliquee + i < nbLignesDansTableauDeJeu))   //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee + i];
                        pictureBoxCliquee.BackColor = Color.DarkGreen;
                    }
                    if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee + i < nbLignesDansTableauDeJeu))    //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de bas, afin d'éviter un débordement.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee + i];
                        pictureBoxCliquee.BackColor = Color.DarkGreen;
                    }
                }
            }
            if (radioButtonLaser.Checked == true)   //Vérifie si le "radio button" relié au laser est coché.
            {
                if (colonneCliquee < nbColonnesDansTableauDeJeu && ligneCliquee < nbLignesDansTableauDeJeu && colonneCliquee >= 0 && ligneCliquee >= 0)  //Cette alternative va vérifier la validitié des cases visées sur le tableau via toutes les bordures, afin d'éviter un débordement.
                {
                    pictureBoxTemporaire.BackColor = Color.Red;
                    for (int i = 0; i < nbColonnesDansTableauDeJeu; i++) //Ces compteurs Pour vont souligner les cases sur le rebord gauche du tableau.           
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[i, 0];
                        pictureBoxCliquee.BackColor = Color.DarkRed;
                    }
                    for (int j = 0; j < nbLignesDansTableauDeJeu; j++)    //Ces compteurs Pour vont souligner les cases sur le rebord haut du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[0, j];
                        pictureBoxCliquee.BackColor = Color.DarkRed;
                    }
                    for (int k = nbColonnesDansTableauDeJeu - 1; 0 < k; k--)  //Ces compteurs Pour vont souligner les cases sur le rebord droite du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[k, nbColonnesDansTableauDeJeu - 1];
                        pictureBoxCliquee.BackColor = Color.DarkRed;
                    }
                    for (int l = nbLignesDansTableauDeJeu - 1; 0 < l; l--)  //Ces compteurs Pour vont souligner les cases sur le rebord bas du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[nbLignesDansTableauDeJeu - 1, l];
                        pictureBoxCliquee.BackColor = Color.DarkRed;
                    }
                }
            }
            if (radioButtonBombe.Checked == true)    //Vérifie si le "radio button" relié à la bombe est coché.
            {
                for (int i = 0; i < RAYON_ACTION; i++)    //Ce compteur Pour imbriqué va coloriées les cases visées par la bombe, jusqu'a ce que le rayon d'action soit atteint.
                {
                    for (int j = 0; j < RAYON_ACTION; j++)
                    {
                        if ((colonneCliquee - i >= 0) && (colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - j >= 0) && (ligneCliquee + j < nbLignesDansTableauDeJeu))  //Cette alternative va vérifier la validitié des cases soulignées sur le tableau via toutes les bordures, afin d'éviter un débordement.
                        {
                            if ((colonneCliquee - i >= 0) && (ligneCliquee - j >= 0))                        //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee - j];   
                                pictureBoxCliquee.BackColor = Color.Purple;
                            }
                            if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - j >= 0))     //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee - j];
                                pictureBoxCliquee.BackColor = Color.Purple;
                            }
                            if ((colonneCliquee - i >= 0) && (ligneCliquee + j < nbLignesDansTableauDeJeu))      //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee + j];
                                pictureBoxCliquee.BackColor = Color.Purple;
                            }
                            if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee + j < nbLignesDansTableauDeJeu))    //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de bas, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee + j];
                                pictureBoxCliquee.BackColor = Color.Purple;
                            }
                        }
                    }
                }
            }
            if (radioButtonCocktail.Checked == true)      //Vérifie si le "radio button" relié au cocktail molotov est coché.
            {
                pictureBoxCliquee.BackColor = Color.DarkOrange;
            }
        }
        //Fonction TraiterClicHorsDuPictureBox: Cette fonction, en plus de gérer le positionnement du curseur hors d'un pictureBox, va convertir 
        // les coordonnées en texte du pictureBox cliquée en deux sous-chaînes, qui seront à leur tour convertis en
        // entier, afin d'être plus facilement manipulables. De plus, cette fonction va rendre transparent une ou des cases
        // qui seront atteintes par une arme, (ex : si radioButtonBoulet.Checked == true, alors une seule case sera
        // mise transparente vue qu'il y en aura qu'une seule d'atteinte par cette arme.
        //Paramètre rentré :  PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        // ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.             
        //Aucune valeur de retour.
        private void TraiterCurseurHorsDuPictureBox(PictureBox pictureBoxCliquee)
        {
            //Note : Cette fonction est pratiquement un copier-collé de la fonction TraiterCurseurSurPictureBox, la seule différence est que la couleur
            //       assignée aux pictureBoxCliquee sera mise transparente, au lieu d'une autre couleur différente.
            PictureBox pictureBoxTemporaire = new PictureBox();
            pictureBoxTemporaire = pictureBoxCliquee;

            string coordonneesChaine = pictureBoxCliquee.Text;  //C'est les coordonnées du pictureboxCliquée, qui a été mise en chaine de caractères.
            int pointVirgule = coordonneesChaine.IndexOf(";");  //Cet entier va extraire le point-virgule de la chaine de caractère ci-dessus.
            string chaineColonneCliquee = coordonneesChaine.Substring(0, pointVirgule);  //La sous-chaine égale à la coordonnée en colonne est extraite.
            string chaineLigneCliquee = coordonneesChaine.Substring(pointVirgule + 1, coordonneesChaine.Length - 1 - pointVirgule);  //La sous-chaine égale à la coordonnée en ligne est extraite.
            int colonneCliquee = int.Parse(chaineColonneCliquee);    //La sous-chaine égale à la coordonnée en colonne extraite est convertie en entier.
            int ligneCliquee = int.Parse(chaineLigneCliquee);       //La sous-chaine égale à la coordonnée en ligne extraite est convertie en entier.


            if (radioButtonBoulet.Checked == true)   //Vérifie si le "radio button" relié au boulet est coché.
            {
                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee, ligneCliquee];
                pictureBoxCliquee.BackColor = Color.Transparent;
            }
            if (radioButtonGrenade.Checked == true)     //Vérifie si le "radio button" relié à la grenade est coché.
            {
                for (int i = 0; i < RAYON_ACTION; i++)     //Ce compteur "Pour" va colorier les cases visées par la grenade, jusqu'a ce que le rayon d'action soit atteint.
                {
                    if ((colonneCliquee - i >= 0) && (ligneCliquee - i >= 0))    //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement. 
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee - i];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - i >= 0))       //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement. 
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee - i];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    if ((colonneCliquee - i >= 0) && (ligneCliquee + i < nbLignesDansTableauDeJeu))         //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement. 
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee + i];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee + i < nbLignesDansTableauDeJeu))  //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de bas afin d'éviter un débordement. 
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee + i];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                }
                pictureBoxTemporaire.BackColor = Color.Transparent;
            }
            if (radioButtonLaser.Checked == true)       //Vérifie si le "radio button" relié au laser est coché.
            {
                if (colonneCliquee < nbColonnesDansTableauDeJeu && ligneCliquee < nbLignesDansTableauDeJeu && colonneCliquee >= 0 && ligneCliquee >= 0)
                {
                    for (int i = 0; i < nbColonnesDansTableauDeJeu; i++) //Ces compteurs "For" vont rendre transparent les cases sur le rebord gauche du tableau.           
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[i, 0];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    for (int j = 0; j < nbLignesDansTableauDeJeu; j++)    //Ces compteurs "For" vont rendre transparent les cases sur le rebord haut du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[0, j];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    for (int k = nbColonnesDansTableauDeJeu - 1; 0 < k; k--)  //Ces compteurs "For" vont rendre transparent les cases sur le rebord droite du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[k, nbColonnesDansTableauDeJeu - 1];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                    for (int l = nbLignesDansTableauDeJeu - 1; 0 < l; l--)  //Ces compteurs "For" vont rendre transparent les cases sur le rebord bas du tableau.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[nbLignesDansTableauDeJeu - 1, l];
                        pictureBoxCliquee.BackColor = Color.Transparent;
                    }
                }
            }
            if (radioButtonBombe.Checked == true)    //Vérifie si le "radio button" relié à la bombe est coché.
            {
                for (int i = 0; i < RAYON_ACTION; i++)    //Ce compteur Pour imbriqué va coloriées les cases visées par la bombe, jusqu'a ce que le rayon d'action soit atteint.
                {
                    for (int j = 0; j < RAYON_ACTION; j++)
                    {
                        if ((colonneCliquee - i >= 0) && (colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - j >= 0) && (ligneCliquee + j < nbLignesDansTableauDeJeu))  //Cette alternative va vérifier la validitié des cases soulignées sur le tableau via toutes les bordures, afin d'éviter un débordement.
                        {
                            if ((colonneCliquee - i >= 0) && (ligneCliquee - j >= 0))                        //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee - j];   
                                pictureBoxCliquee.BackColor = Color.Transparent;
                            }
                            if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - j >= 0))     //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee - j];
                                pictureBoxCliquee.BackColor = Color.Transparent;
                            }
                            if ((colonneCliquee - i >= 0) && (ligneCliquee + j < nbLignesDansTableauDeJeu))      //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee - i, ligneCliquee + j];
                                pictureBoxCliquee.BackColor = Color.Transparent;
                            }
                            if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee + j < nbLignesDansTableauDeJeu))    //Cette alternative va vérifier la validitié des cases visées sur le tableau via les bordures de droite et de bas, afin d'éviter un débordement. 
                            {
                                pictureBoxCliquee = toutesImagesVisuelles[colonneCliquee + i, ligneCliquee + j];
                                pictureBoxCliquee.BackColor = Color.Transparent;
                            }
                        }
                    }
                }
            }


            pictureBoxTemporaire.BackColor = Color.Transparent;
        }

        //Fonction LancerBouletCanon : Cette fonction va faire plusieurs tâches. En plus d'incrémenter l'entier nbBouletsLancees, et d'afficher la nouvelle valeur,
        // cette fonction va mettre une image spécifique de la picturebox sélectionnée, selon si elle a été touchée ou non. Par ailleurs, l'élément du tableauLogique sélectionné
        // selon la position du pictureBox sera égal à BATEAU_DÉMOLI si un bateau a été touché, en plus d'appeler la fonction MettreAJourStatistiques. Enfin, la fonction
        // GestionDesSons sera appelée à la toute fin.
        //Paramètres rentrés :  - PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        //                         ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.
        private void LancerBouletCanon(PictureBox pictureBoxCliquee, int colonneCliquee, int ligneCliquee)
        {
            int ligneCourante = ligneCliquee;     //Il s'agit de la ligne cliquée, ou la valeur en ligne de la case cliquée changera.
            int colonneCourante = colonneCliquee; //Il s'agit de la colonne cliquée, ou la valeur en colonne de la case cliquée changera.
            bool tirReussi = false; //Si ce booléen devient true, alors un son sera joué via la fonction GestionDesSons

            nbBouletsLancees += 1;
            lblQuantiteBoulet.Text = "Boulets lancés :" + nbBouletsLancees.ToString();

            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante, colonneCourante];

            if (tableauLogique[ligneCourante, colonneCourante] != TypeBateau.AUCUN_BATEAU)      //Si cette alternative est vraie, alors la case sélectionnée
            {                                                                                   //du tableauLogique prendra la valeur BATEAU_DÉMOLI, en plus de lui changer son image.
                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                MettreAJourStatistiques(colonneCourante, ligneCourante);
                tableauLogique[ligneCourante, colonneCourante] = TypeBateau.BATEAU_DEMOLI;
                tirReussi = true;
            }
            else
            {
                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
            }
            GestionDesSons(tirReussi);
        }
        //Fonction LancerGrenade : Cette fonction va faire plusieurs tâches. En plus d'incrémenter l'entier nbGrenadesLancees, et d'afficher la nouvelle valeur,
        // cette fonction va mettre une image spécifique de la picturebox sélectionnée, selon si elle a été touchée ou non, même chose pour les cases situées à ses diagonales
        // dont le nombre est déterminé par l'entier RAYON_ACTION. Par ailleurs, les éléments du tableauLogique sélectionné
        // selon la position du pictureBox seront égaux à BATEAU_DÉMOLI si un ou des bateaux ont étés touchés, 
        // en plus d'appeler la fonction MettreAJourStatistiques. Enfin, la fonction GestionDesSons sera appelée à la toute fin.
        //Paramètres rentrés :  - PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        //                         ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.

        private void LancerGrenade(PictureBox pictureBoxCliquee, int colonneCliquee, int ligneCliquee)
        {
            int ligneCourante = ligneCliquee;     //Il s'agit de la ligne cliquée, ou la valeur en ligne de la case cliquée changera.
            int colonneCourante = colonneCliquee; //Il s'agit de la colonne cliquée, ou la valeur en colonne de la case cliquée changera.
            bool tirReussi = false; //Si ce booléen devient true, alors un son sera joué via la fonction GestionDesSons

            nbGrenadesLancees += 1;
            lblQuantiteGrenades.Text = "Grenades lancées :" + nbGrenadesLancees.ToString();

            for (int i = 0; i < RAYON_ACTION; i++)     //Ce compteur Pour va parcourir les cases touchées par la grenade, jusqu'a ce que le rayon d'action soit atteint.
            {
                #region explosions de grenades en haut à gauche
                if ((colonneCliquee - i >= 0) && (ligneCliquee - i >= 0))    //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement. 
                {
                    pictureBoxCliquee = toutesImagesVisuelles[ligneCourante - i, colonneCourante - i];

                    if (tableauLogique[ligneCourante - i, colonneCourante - i] != TypeBateau.AUCUN_BATEAU)  //Vérifie si les cases altérées par la grenade n'ont aucun bateau.
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCourante - i, ligneCourante - i);
                        tableauLogique[ligneCourante - i, colonneCourante - i] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                    }
                }

                #endregion
                #region explosions de grenades en bas à gauche
                if ((colonneCliquee - i >= 0) && (ligneCliquee + i < nbLignesDansTableauDeJeu)) //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement. 
                {
                    pictureBoxCliquee = toutesImagesVisuelles[ligneCourante + i, colonneCourante - i];

                    if (tableauLogique[ligneCourante + i, colonneCourante - i] != TypeBateau.AUCUN_BATEAU)  //Vérifie si les cases altérées par la grenade n'ont aucun bateau.
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCourante - i, ligneCourante + i);
                        tableauLogique[ligneCourante + i, colonneCourante - i] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                    }
                }

                #endregion
                #region explosions de grenades en haut à droite
                if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee - i >= 0))  //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement. 
                {
                    pictureBoxCliquee = toutesImagesVisuelles[ligneCourante - i, colonneCourante + i];

                    if (tableauLogique[ligneCourante - i, colonneCourante + i] != TypeBateau.AUCUN_BATEAU) //Vérifie si les cases altérées par la grenade n'ont aucun bateau.
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCourante + i, ligneCourante - i);
                        tableauLogique[ligneCourante - i, colonneCourante + i] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                    }

                }

                #endregion
                #region explosions de grenades en bas à droite
                if ((colonneCliquee + i < nbColonnesDansTableauDeJeu) && (ligneCliquee + i < nbLignesDansTableauDeJeu))  //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de bas et de droite, afin d'éviter un débordement. 
                {
                    pictureBoxCliquee = toutesImagesVisuelles[ligneCourante + i, colonneCourante + i];

                    if (tableauLogique[ligneCourante + i, colonneCourante + i] != TypeBateau.AUCUN_BATEAU) //Vérifie si les cases altérées par la grenade n'ont aucun bateau.
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCourante + i, ligneCourante + i);
                        tableauLogique[ligneCourante + i, colonneCourante + i] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else
                    {
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                    }
                }



                #endregion
            }
            #region explosion du centre
            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante, colonneCourante];

            if (tableauLogique[ligneCourante, colonneCourante] != TypeBateau.AUCUN_BATEAU)  //Vérifie si les cases altérées par la grenade n'ont aucun bateau.
            {
                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                MettreAJourStatistiques(colonneCourante, ligneCourante);
                tableauLogique[ligneCourante, colonneCourante] = TypeBateau.BATEAU_DEMOLI; 
                tirReussi = true;
            }
            else
            {
                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
            }
            GestionDesSons(tirReussi);
            #endregion

        }
        //Fonction LancerLaser : Cette fonction va faire plusieurs tâches. En plus d'incrémenter l'entier nbLaserLances, et d'afficher la nouvelle valeur,
        // cette fonction va mettre une image spécifique de la picturebox sélectionnée, selon si elle a été touchée ou non, même chose pour toutes les cases situées à sa diagonale,
        // horizontalement ou verticale à elle, selon l'endroit de la bordure ou l'usager a cliqué. 
        // Par ailleurs, les éléments du tableauLogique sélectionné selon la position du pictureBox seront égaux à BATEAU_DÉMOLI si un ou des bateaux ont étés touchés,  
        // en plus d'appeler la fonction MettreAJourStatistiques. Enfin, la fonction GestionDesSons sera appelée à la toute fin.
        //Paramètres rentrés :  - PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        //                         ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.
        private void LancerLaser(PictureBox pictureBoxCliquee, int colonneCliquee, int ligneCliquee)
        {
            int ligneCourante = ligneCliquee;     //Il s'agit de la ligne cliquée, ou la valeur en ligne de la case cliquée changera.
            int colonneCourante = colonneCliquee; //Il s'agit de la colonne cliquée, ou la valeur en colonne de la case cliquée changera.
            bool tirReussi = false; //Si ce booléen devient true, alors un son sera joué via la fonction GestionDesSons
            int colonneAAtteindre = 0; // C'est la colonne que le compteur de la boucle While (voir plus bas) que la colonne doit atteindre.
            int ligneAAtteindre = 0;   // C'est la ligne que le compteur de la boucle While (voir plus bas) que la ligne doit atteindre.
            int incrementColonne = 0;  // C'est la valeur (1, 0 ou -1) que l'incrément de la colonne doit atteindre.
            int incrementLigne = 0;    // C'est la valeur (1, 0 ou -1) que l'incrément de la ligne doit atteindre.
  

            nbLasersLances += 1;
            lblQuantiteLasers.Text = "Lasers lancées :" + nbLasersLances.ToString();

            //Bordures de haut et de bas
            if (colonneCliquee != 0 && colonneCliquee < nbColonnesDansTableauDeJeu - 1)   //Cette alternative vérifie si le joueur n'a pas cliqué sur un des quatres coins.
            {
                if (ligneCliquee == 0)     //Cette alternative vérifie si le joueur a cliqué sur la bordure de haut.
                {
                    incrementColonne = 0;
                    incrementLigne = 1;
                    colonneAAtteindre = colonneCliquee;
                    ligneAAtteindre = nbLignesDansTableauDeJeu;
                }
                else if (ligneCliquee == (nbLignesDansTableauDeJeu - 1))      //Cette alternative vérifie si le joueur a cliqué sur la bordure de bas.
                {
                    incrementColonne = 0;
                    incrementLigne = -1;
                    colonneAAtteindre = colonneCliquee;
                    ligneAAtteindre = -1;
                }

                else
                    return;
            }

            //Diagonale qui commence en haut à gauche
            else if ((colonneCliquee == 0 && ligneCliquee == 0))
            {
                if (colonneCliquee == 0 && ligneCliquee == 0)   //Vérifie si la case cliquée est celle en haut à gauche.
                {
                    incrementColonne = 1;
                    incrementLigne = 1;
                    colonneAAtteindre = nbColonnesDansTableauDeJeu;
                    ligneAAtteindre = nbLignesDansTableauDeJeu;
                }

                else
                    return;
            }
            //Diagonale qui commence en haut à droite
            if (colonneCliquee == nbColonnesDansTableauDeJeu - 1 && ligneCliquee == 0)    //Vérifie si la case cliquée est celle en haut à droite.
            {
                incrementColonne = -1;
                incrementLigne = 1;
                colonneAAtteindre = -1;
                ligneAAtteindre = nbLignesDansTableauDeJeu;
            }
            //Diagonale qui débute en bas à droite
            if (colonneCliquee == nbColonnesDansTableauDeJeu - 1 && ligneCliquee == nbLignesDansTableauDeJeu - 1) //Vérifie si la case cliquée est celle en bas à droite.
            {
                incrementColonne = -1;
                incrementLigne = -1;
                colonneAAtteindre = -1;
                ligneAAtteindre = -1;
            }
            //Diagonale qui débute en bas à gauche
            if (colonneCliquee == 0 && ligneCliquee == nbLignesDansTableauDeJeu - 1)       //Vérifie si la case cliquée est celle en bas à gauche.
            {
                incrementColonne = 1;
                incrementLigne = -1;
                colonneAAtteindre = nbColonnesDansTableauDeJeu;
                ligneAAtteindre = -1;
            }
            // Bordures de haut et de bas
            else if ((ligneCliquee != 0) && (ligneCliquee < nbLignesDansTableauDeJeu - 1))  
            {
                if (colonneCliquee == 0)   //Si le joueur a cliqué sur la bordure sur la gauche.
                {
                    incrementColonne = 1;
                    incrementLigne = 0;
                    colonneAAtteindre = nbColonnesDansTableauDeJeu;
                    ligneAAtteindre = ligneCliquee;
                }
                else if (colonneCliquee == (nbColonnesDansTableauDeJeu - 1)) //Si le joueur a cliqué sur la bordure sur la droite.
                {
                    incrementColonne = -1;
                    incrementLigne = 0;
                    colonneAAtteindre = -1;
                    ligneAAtteindre = ligneCliquee;
                }

                else
                    return;
            }
            //Boucle while qui va changer le sprite des positions altérés par le laser.
            while ((ligneAAtteindre != ligneCourante) || (colonneAAtteindre != colonneCourante))
            {
                pictureBoxCliquee = toutesImagesVisuelles[ligneCourante, colonneCourante];

                if (tableauLogique[ligneCourante, colonneCourante] != TypeBateau.AUCUN_BATEAU) //Vérifie si les cases altérées par le laser sont occupées par un bateau.
                {
                    MettreAJourStatistiques(colonneCourante, ligneCourante);
                    pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                    tableauLogique[ligneCourante, colonneCourante] = TypeBateau.BATEAU_DEMOLI;
                    tirReussi = true;
                }
                else
                {
                    pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                }
                ligneCourante += incrementLigne;
                colonneCourante += incrementColonne;
            }
            GestionDesSons(tirReussi);
        }
        //Fonction LancerBombe : Cette fonction va faire plusieurs tâches. En plus d'incrémenter l'entier nbBombesLancees, et d'afficher la nouvelle valeur,
        // cette fonction va mettre une image spécifique de la picturebox sélectionnée, selon si elle a été touchée ou non, même chose pour les cases situées
        // autour d'elles, dont le nombre est déterminé par l'entier RAYON_ACTION. Par ailleurs, les éléments du tableauLogique sélectionné
        // selon la position du pictureBox seront égaux à BATEAU_DÉMOLI si un ou des bateaux ont étés touchés, 
        // en plus d'appeler la fonction MettreAJourStatistiques. Enfin, la fonction GestionDesSons sera appelée à la toute fin.
        //Paramètres rentrés :  - PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        //                         ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.
        private void LancerBombe(PictureBox pictureBoxCliquee, int colonneCliquee, int ligneCliquee)
        {
            bool tirReussi = false; //Si ce booléen devient true, alors un son sera joué via la fonction GestionDesSons
            nbBombesLancees += 1;
            lblQuantiteBombes.Text = "Bombes lancées :" + nbBombesLancees.ToString();
            for (int i = 0; i < RAYON_ACTION; i++)       //Ce compteur Pour imbriqué va coloriées les cases visées par la bombe, jusqu'a ce que le rayon d'action soit atteint.
            {
                for (int j = 0; j < RAYON_ACTION; j++)
                {
                    int ligneCourante = ligneCliquee;     //Il s'agit de la ligne cliquée, ou la valeur en ligne de la case cliquée changera.
                    int colonneCourante = colonneCliquee; //Il s'agit de la colonne cliquée, ou la valeur en colonne de la case cliquée changera.
                    if ((colonneCliquee - j >= 0) && (colonneCliquee + j < nbColonnesDansTableauDeJeu) && (ligneCliquee - i >= 0) && (ligneCliquee + i < nbLignesDansTableauDeJeu))   //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via toutes les bordures, afin d'éviter un débordement. 
                    {
                        if ((ligneCliquee - i >= 0) && (colonneCliquee - j >= 0))      //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de gauche et de haut, afin d'éviter un débordement. 
                        {
                            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante - i, colonneCourante - j];

                            if (tableauLogique[ligneCourante - i, colonneCourante - j] != TypeBateau.AUCUN_BATEAU) //Vérifie si les cases altérées par la bombe sont occupées par un bateau.
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                                
                                tableauLogique[ligneCourante - i, colonneCourante - j] = TypeBateau.BATEAU_DEMOLI; 
                                
                                tirReussi = true;
                                
                            }
                            else
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                            }  
                            MettreAJourStatistiques(colonneCourante - j, ligneCourante - i);
                        }
                        if ((ligneCliquee + i < nbLignesDansTableauDeJeu) && (colonneCliquee - j >= 0))      //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de gauche et de bas, afin d'éviter un débordement. 
                        {
                            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante + i, colonneCourante - j];

                            if (tableauLogique[ligneCourante + i, colonneCourante - j] != TypeBateau.AUCUN_BATEAU)     //Vérifie si les cases altérées par la bombe sont occupées par un bateau.
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                                tableauLogique[ligneCourante + i, colonneCourante - j] = TypeBateau.BATEAU_DEMOLI; 
                               
                                tirReussi = true;
                            }
                            else
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                            }
                             MettreAJourStatistiques(colonneCourante - j, ligneCourante + i);
                        }
                        if ((ligneCliquee - i >= 0) && (colonneCliquee + j < nbColonnesDansTableauDeJeu))        //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de droite et de haut, afin d'éviter un débordement. 
                        {
                            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante - i, colonneCourante + j];

                            if (tableauLogique[ligneCourante - i, colonneCourante + j] != TypeBateau.AUCUN_BATEAU)    //Vérifie si les cases altérées par la bombe sont occupées par un bateau.
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                                tableauLogique[ligneCourante - i, colonneCourante + j] = TypeBateau.BATEAU_DEMOLI; 
                               
                                tirReussi = true;
                            }
                            else
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                            }  
                            MettreAJourStatistiques(colonneCourante + j, ligneCourante - i);
                        }

                        if ((ligneCliquee + i < nbLignesDansTableauDeJeu) && (colonneCliquee + j < nbColonnesDansTableauDeJeu))   //Cette alternative va vérifier la validitié des cases altérées par la grenade sur le tableau via les bordures de droite et de bas, afin d'éviter un débordement. 
                        {
                            pictureBoxCliquee = toutesImagesVisuelles[ligneCourante + i, colonneCourante + j];

                            if (tableauLogique[ligneCourante + i, colonneCourante + j] != TypeBateau.AUCUN_BATEAU)  //Vérifie si les cases altérées par la bombe sont occupées par un bateau.
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;

                                tableauLogique[ligneCourante + i, colonneCourante + j] = TypeBateau.BATEAU_DEMOLI;
                                
                                tirReussi = true;
                            }
                            else
                            {
                                pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                            }  
                            MettreAJourStatistiques(colonneCourante + j, ligneCourante + i);
                        }

                    }
                }
            }
            GestionDesSons(tirReussi);
        }
        //Fonction LancerCocktail : Cette fonction va faire plusieurs tâches. En plus d'incrémenter l'entier nbCocktailsLancees, et d'afficher la nouvelle valeur,
        // cette fonction va mettre une image spécifique de la picturebox sélectionnée, selon si elle a été touchée ou non, même chose pour toutes les cases du tableauLogique
        // de la même valeur, si un bateau est touché. Par ailleurs, les éléments du tableauLogique sélectionné de même valeur,
        // selon la position du pictureBox seront tous égaux à BATEAU_DÉMOLI si un bateau a été touché, 
        // en plus d'appeler la fonction MettreAJourStatistiques. Enfin, la fonction GestionDesSons sera appelée à la toute fin.
        //Paramètres rentrés :  - PictureBox pictureBoxCliquee : Il s'agit de la PictureBox cliquée, générée dans la fonction 
        //                         ConstruireTableauJeu, qui sera traitée dans la fonction TraiterClicHorsDuPictureBox.  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.
        private void LancerCocktail(PictureBox pictureBoxCliquee, int colonneCliquee, int ligneCliquee)
        {
            int ligneCourante = ligneCliquee;     //Il s'agit de la ligne cliquée, ou la valeur en ligne de la case cliquée changera.
            int colonneCourante = colonneCliquee; //Il s'agit de la colonne cliquée, ou la valeur en colonne de la case cliquée changera.
            bool tirReussi = false; //Si ce booléen devient true, alors un son sera joué via la fonction GestionDesSons
            nbCocktailsLances += 1;
            lblQuantiteCocktails.Text = "Cocktails lancées :" + nbCocktailsLances.ToString();

            for (int i = 0; i < nbLignesDansTableauDeJeu; i++)          //C'est une boucle imbriquée qui va parcourir le contenu du tableauLogique afin d'accéder à son contenu.
            {
                for (int j = 0; j < nbLignesDansTableauDeJeu; j++)
                {
                    if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.PETIT_BATEAU && tableauLogique[i, j] == TypeBateau.PETIT_BATEAU)    //Cette alternative va rendre égale à BATEAU_DÉMOLI touts les cases occupées par la valeur
                    {                                                                                                                                  //PETIT_BATEAU.
                        pictureBoxCliquee = toutesImagesVisuelles[i, j];
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCliquee, ligneCliquee);
                        tableauLogique[i, j] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.MOYEN_BATEAU && tableauLogique[i, j] == TypeBateau.MOYEN_BATEAU)  //Cette alternative va rendre égale à BATEAU_DÉMOLI touts les cases occupées par la valeur
                    {                                                                                                                                     //MOYEN_BATEAU.
                        pictureBoxCliquee = toutesImagesVisuelles[i, j];
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCliquee, ligneCliquee);
                        tableauLogique[i, j] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.GRAND_BATEAU && tableauLogique[i, j] == TypeBateau.GRAND_BATEAU)  //Cette alternative va rendre égale à BATEAU_DÉMOLI touts les cases occupées par la valeur
                    {                                                                                                                                     //GRAND_BATEAU.
                        pictureBoxCliquee = toutesImagesVisuelles[i, j];
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_hit;
                        MettreAJourStatistiques(colonneCliquee, ligneCliquee);
                        tableauLogique[i, j] = TypeBateau.BATEAU_DEMOLI;
                        tirReussi = true;
                    }
                    else if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.AUCUN_BATEAU && tableauLogique[i, j] == TypeBateau.AUCUN_BATEAU)   //S'il n'y avait aucun bateau dans la case, alors l'image sera simplement mise à vide.
                    {
                        pictureBoxCliquee = toutesImagesVisuelles[ligneCliquee, colonneCliquee];
                        pictureBoxCliquee.BackgroundImage = Properties.Resources.img_empty;
                    }
                }
            }
            GestionDesSons(tirReussi);
        }
        //Fonction MettreAJourStatisiques : Cette fonction va calculer la vie restantes aux bateaux. Elle va aussi
        // vérifier si la partie est terminée.
        //Paramètres rentrés :  
        //                       - int colonneCliquee : Il s'agit de la position en colonne du pictureBox Cliquée. 
        //                       - int ligneCliquee : Il s'agit de la position en ligne du pictureBox Cliquée. 
        //Aucune valeur de retour.
        private void MettreAJourStatistiques(int colonneCliquee, int ligneCliquee)
        {
            int nombreDeLancersTotals = (nbBouletsLancees + nbGrenadesLancees + nbLasersLances + nbBombesLancees + nbCocktailsLances); // Représente le nombre de lancers totals avec toutes les armes.
            
            if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.PETIT_BATEAU && progressBarPetit.Value > 0)   //Cette alternative teste si la valeur du progressbar est plus grande que zéro.
            {                                                                                                            //elle teste aussi si le bateau cliqué est égal à PETIT_BATEAU.
                progressBarPetit.Value -= 33;
                coupsReussis++; 
                if (progressBarPetit.Value == 1)  //Cette alternative va égaliser la valeur du progressbar à zéro aussitôt qu'elle devient égale à un, dû au fait que
                {                                 //que le tiers d'un bateau de taille 3 (dans ce cas-ci le petit bateau) est égal à 33.33%.
                    progressBarPetit.Value = 0;
                }
            }
            if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.MOYEN_BATEAU && progressBarMoyen.Value > 0)  //Cette alternative teste si la valeur du progressbar est plus grande que zéro.                                                                                               
            {                                                                                                           //elle teste aussi si le bateau cliqué est égal à MOYEN_BATEAU.
                progressBarMoyen.Value -= 20;
                coupsReussis++; 
            }
            if (tableauLogique[ligneCliquee, colonneCliquee] == TypeBateau.GRAND_BATEAU && progressBarGrand.Value > 0)  //Cette alternative teste si la valeur du progressbar est plus grande que zéro.                                                                                               
            {                                                                                                           //elle teste aussi si le bateau cliqué est égal à GRAND_BATEAU.
                progressBarGrand.Value -= 14;
                coupsReussis++; 
                if (progressBarGrand.Value < 14)  //Cette alternative va égaliser la valeur du progressbar à zéro aussitôt qu'elle devient inférieur à 14, dû au fait que
                {                                 //que le septièment d'un bateau de taille 7 (dans ce cas-ci le grand bateau) est égal à 14.28%.
                    progressBarGrand.Value = 0;
                }
            }
            progressBarCumulatif.Value = ((progressBarPetit.Value + progressBarMoyen.Value + progressBarGrand.Value)/3);


                    
            if (100 * coupsReussis / nombreDeLancersTotals <= 100)     //Cette alternative va faire le produit croisé des domages infligés et du nombre de lancers total si le calcul est plus petit ou égal à 100.
            {
                progressBarPrecision.Value = (100 * coupsReussis / nombreDeLancersTotals); 
            }
            else
            {
                progressBarPrecision.Value = 100;
            }
           

            if (progressBarPetit.Value <= 0 && progressBarMoyen.Value <= 0 && progressBarGrand.Value <= 0)
            {
                DialogResult victoire = MessageBox.Show("Vous avez coulé toute la flotte ennemie! Désirez-vous rejouer?", "Victoire!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (victoire == DialogResult.Yes) //Si dans le formulaire d'options, le résultat retourné est OK, alors la partie sera réinitialisée, avec en plus
                {                                 //les nouveaux paramètres appliqués.
                    tableauLogique = null;
                    toutesImagesVisuelles = new PictureBox[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
                    ConstruireTableauJeu();
                    InitialiserBateaux();
                    progressBarPetit.Value = 100;
                    progressBarMoyen.Value = 100;
                    progressBarGrand.Value = 100;
                    nbBouletsLancees = 0;
                    nbGrenadesLancees = 0;
                    nbLasersLances = 0;
                    nbBombesLancees = 0;
                    nbCocktailsLances = 0;
                }
                if (victoire == DialogResult.No)  //En revanche, si dans le formulaire d'options, le résultat retourné est NO, alors l'application
                {                                 //sera simplement quittée.
                    Application.Exit();
                }
            }


        }
        //Fonction GestionDesSons : Cette fonction va gérér les sons, c'est-à-dire faire jouer correspondant à l'arme.
        //Paramètres rentrés :  
        //                       - bool tirReussi : Il s'agit du booléen qui est égal à "true" si un tir est réussi
        //                                          d'après les fonctions commencant par "Lancer".
        //Aucune valeur de retour.
        private void GestionDesSons(bool tirReussi)
        {
            //Son du boulet de canon
            if (tirReussi == true && sonActive == true && radioButtonBoulet.Checked == true)      //Cette alternative vérifie si le tir est réussi, si la case du son est activé et si le radioButton relié à LancerBoulet est coché.
            {
                player = new SoundPlayer(Properties.Resources.cannon_x);
                player.Load();
                player.Play();
            }
            else if (tirReussi == true && sonActive == true && radioButtonGrenade.Checked == true)   //Cette alternative vérifie si le tir est réussi, si la case du son est activé et si le radioButton relié à LancerGrenade est coché.
            {
                player = new SoundPlayer(Properties.Resources.grenade_x);
                player.Load();
                player.Play();
            }
            else if (tirReussi == true && sonActive == true && radioButtonLaser.Checked == true)   //Cette alternative vérifie si le tir est réussi, si la case du son est activé et si le radioButton relié à LancerLaser est coché.
            {
                player = new SoundPlayer(Properties.Resources.laser_x);
                player.Load();
                player.Play();
            }
            else if (tirReussi == true && sonActive == true && radioButtonBombe.Checked == true)     //Cette alternative vérifie si le tir est réussi, si la case du son est activé et si le radioButton relié à LancerBombe est coché.
            {
                player = new SoundPlayer(Properties.Resources.bomb_x);
                player.Load();
                player.Play();
            }
            else if (tirReussi == true && sonActive == true && radioButtonCocktail.Checked == true)  //Cette alternative vérifie si le tir est réussi, si la case du son est activé et si le radioButton relié à LancerCocktail est coché.
            {
                player = new SoundPlayer(Properties.Resources.cocktail_x);
                player.Load();
                player.Play();
            }
            else if (sonActive == true)        //Si aucune des conditions ci-dessus est remplies, alors le son joué sera commun (si le joueur rate son coup).
            {
                player = new SoundPlayer(Properties.Resources.failed_x);
                player.Load();
                player.Play();
            }
        }
        //Fonction Form1_Load : Charge un nouveau tableau de picturesBox ainsi qu'il fait appel à ConstruireTableauxJeu 
        //Aucun paramètre rentré manuellement.
        //Aucune valeur de retour.
        private void Form1_Load(object sender, EventArgs e)
        {

            toutesImagesVisuelles = new PictureBox[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
            ConstruireTableauJeu();
        }
        #region Relations entre les deux formulaires
        //Fonction changerLesOptionsToolStripMenuItem_Click : Lorsque cliqué, l'onglet rattaché à cette fonction va ouvrir le formulaire d'options.
        //Aucun paramètre rentré manuellement.
        //Aucune valeur de retour.
        public void changerLesOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions options = new FormOptions();
            options.SetRayonGrenadeOptions(RAYON_ACTION);
            options.SetNombreLignesOptions(nbLignesDansTableauDeJeu);
            options.SetNombreColonnesOptions(nbColonnesDansTableauDeJeu);
            options.SetSonActiveOptions(sonActive);
            if (options.ShowDialog() == DialogResult.OK)           //Si, dans le formulaire d'options, le résultat retourné est OK, alors la partie sera réinitialisée, avec en plus
            {                                                      //les nouveaux paramètres appliqués.
                RAYON_ACTION = options.GetRayonGrenadeOptions();
                nbLignesDansTableauDeJeu = options.GetNombreLignesOptions();
                nbColonnesDansTableauDeJeu = options.GetNombreColonnesOptions();
                sonActive = options.GetSonActiveOptions();

                tableauLogique = new TypeBateau[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
                toutesImagesVisuelles = new PictureBox[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
                ConstruireTableauJeu();
                InitialiserBateaux();
                progressBarPetit.Value = 100;
                progressBarMoyen.Value = 100;
                progressBarGrand.Value = 100;
                coupsReussis = 0;
                nbBouletsLancees = 0;
                nbGrenadesLancees = 0;
                nbLasersLances = 0;
                nbBombesLancees = 0;
                nbCocktailsLances = 0;
            }
        }
        //Fonction GetNbLignes : Cette fonction recoit l'entier nouveauNbLignesDansTableauDeJeu via le formulaire d'options Form Options
        //Paramètres rentrés : - int nouveauNbLignesDansTableauDeJeu : C'est le nombre de lignes modifié, recu du Formulaire d'options Form Options
        //
        //
        //L'entier retourné est nouveauNbLignesDansTableauDeJeu.
        public int GetNbLignes(int nouveauNbLignesDansTableauDeJeu)
        {
            return nouveauNbLignesDansTableauDeJeu;
        }
        //Fonction GetNbColonnes : Cette fonction recoit l'entier nouveauNbColonnesDansTableauDeJeu via le formulaire d'options Form Options
        //Paramètres rentrés : - int nouveauNbColonnesDansTableauDeJeu : C'est le nombre de colonnes modifié, recu du Formulaire d'options Form Options
        //
        //
        //L'entier retourné est nouveauNbColonnesDansTableauDeJeu.
        public int GetNbColonnes(int nbColonnesDansTableauDeJeu)
        {
            return nbColonnesDansTableauDeJeu;
        }
        //Fonction NOUVEAU_RAYON_ACTION : Cette fonction recoit l'entier NOUVEAU_RAYON_ACTION via le formulaire d'options Form Options
        //Paramètres rentrés : - int NOUVEAU_RAYON_ACTION : C'est le rayon d'action modifié, recu du Formulaire d'options Form Options
        //
        //
        //L'entier retourné est NOUVEAU_RAYON_ACTION.
        public int GetRayonGrenade(int NOUVEAU_RAYON_ACTION)
        {
            return NOUVEAU_RAYON_ACTION;
        }
        //Fonction GetSonActive : Cette fonction recoit le booléen nouveauSonActive via le formulaire d'options Form Options
        //Paramètres rentrés : - int nouveauSonActive : C'est la valeur true ou false, recu du Formulaire d'options Form Options
        //                                              selon si la case checkBoxSon est cochée ou non (voir formulaire d'options)
        //
        //Le booléen retourné est nouveauSonActive.
        public bool GetSonActive(bool nouveauSonActive)
        {
            return nouveauSonActive;
        }
        #endregion
        //Fonction quitterToolStripMenuItem_Click : Lorsque cliquée, l'onglet rattaché à cette fonction va fermer l'application.
        //Aucun paramètre rentré manuellement.
        //Aucune valeur de retour.
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Fonction recommencerToolStripMenuItem_Click : Lorsque cliquée, l'onglet rattaché à cette fonction va réinitialiser l'application, en plus de
        // remettre à zéro les paramètres, ainsi de rapeller les fonctions qui ont étés appelées au début.
        //Aucun paramètre rentré manuellement.
        //Aucune valeur de retour.
        private void recommencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableauLogique = null;
            toutesImagesVisuelles = new PictureBox[nbLignesDansTableauDeJeu, nbColonnesDansTableauDeJeu];
            ConstruireTableauJeu();
            InitialiserBateaux();
            coupsReussis = 0;
            progressBarPetit.Value = 100;
            progressBarMoyen.Value = 100;
            progressBarGrand.Value = 100;
            nbBouletsLancees = 0;
            nbGrenadesLancees = 0;
            nbLasersLances = 0;
            nbBombesLancees = 0;
            nbCocktailsLances = 0;
        }
    }
}
