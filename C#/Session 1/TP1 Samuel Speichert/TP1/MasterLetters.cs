/*Voici le jeu de MasterMind (Ou MasterLettres dans ce cas-ci) fait par moi-même. Le but du jeu est de trouver la combinaison de trois lettres, générée par le programme.
 *L'usager dispose d'un maximum de trois tentatives pour trouver la bonne combinaison, avec comme indices le nombre de lettres correctes, ainsi que le nombre de lettres
 *bien placées. Les combinaisons invalides ne comptent pas comme étant un tour valide. 
 * 
 * 
 * 
 * Auteur: Samuel Speichert 
 * */

using System;
using System.IO;

namespace ProgrammationJeuxVideo1
{
  class TP1
  {
    /// <Description de Run>
    /// Paramètres entrants : Aucun
    /// La méthode Run est le point de départ (point d'entrée)
    /// dans notre application.
    /// Valeurs retournées : Aucun
    /// </summary>
    public void Run( )
    {
      
      Console.CursorVisible = false;  
      Console.WindowWidth = 60;       
      Console.BackgroundColor = ConsoleColor.White; 
      
      bool joueurVeutQuitterJeu = false;

      while (joueurVeutQuitterJeu == false)     //Boucle qui va répéter les actions ci-dessous tant que le booléen joueurVeutQuitterJeu est faux.
       {
           Console.Clear(); 
           Console.ForegroundColor = ConsoleColor.Black; 
           
           AfficherEcranAccueil();   

           joueurVeutQuitterJeu = DemanderJoueurSiQuitterJeu();

           if (joueurVeutQuitterJeu == false)  //Alternative qui donne à l'usager le choix de quitter l'application, ou de jouer une partie, dépendant de la valeur du booléen joueurVeutQuitterJeu 
           {
               JouerPartie();      
           }
           else
           {
               MessageAuRevoir();  
           } 
       }

       
    }
    /// <Description de MessageAuRevoir>
    /// Paramètres rentrants : Aucun
    /// La méthode MessageAuRevoir va afficher un message d'au revoir
    /// si le joueur a appuyé sur 'q' au menu principal, tout en effaçant le contenu de la console prédédemment affiché.
    /// Valeur de retour : Aucun
    /// </Description de MessageAuRevoir>
    void MessageAuRevoir()
      {
        Console.WriteLine("Beu-bye... Revenez quand vous voulez!");
      }
    /// <Description de AfficherEcranAccueil>
    /// Paramètres rentrants : Aucun
    /// La méthode AfficherEcranAccueil va afficher un menu principal
    /// qui indique la possibilité à l'usager de commencer une partie
    /// ou de quitter l'application. (Voir ce site pour la police de 
    /// caractère de l'écran d'accueil : http://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20)
    /// Valeurs de retour : Aucun
    /// </Description de AfficherEcranAccueil>
    void AfficherEcranAccueil()
    {

        string lineTitle;

        using (StreamReader reader = new StreamReader("title.txt"))
        {
            while ((lineTitle = reader.ReadLine()) != null)
            {
                Console.WriteLine(lineTitle);
            }
            lineTitle = reader.ReadLine();
        }
    }
         
    /// <Description de DemanderJoueurSiQuitterJeu>
    /// Paramètres rentrants : Aucun
    /// La méthode DemanderJoueurSiQuitterJeu permet au joueur d'executer la fonction MessageAuRevoir si le joueur entre le caractere 'q' ou 'Q' dans la console.
    /// Le booléen joueurVeutQuitterJeu sera retournée
    /// Valeur retournée : joueurVeutQuitterJeu  true = Si l'usager a rentré dans la console le caractère 'q' ou 'Q'    false = S'il a appuyé sur une autre touche.
    /// </Description de DemanderJoueurSiQuitterJeu>
    bool DemanderJoueurSiQuitterJeu()
     {
        Console.WriteLine(@"************************************************************
*                                                          *
*                Appuyez sur Q si vous voulez              *
*                     quitter le jeu.                      *
*            Appuyez sur une autre touche pour jouer.      *
*                                                          *
************************************************************");
        bool joueurVeutQuitterJeu = false;
        char choixDuJoueur;
        choixDuJoueur = Console.ReadKey().KeyChar;

        if (choixDuJoueur == 'q' || choixDuJoueur == 'Q') //C'est l'alternative qui va mettre au booléen joueurQuitterJeu la valeur "vrai" si l'usager entre la commande 'q' ou 'Q' dans la console.
        {
          joueurVeutQuitterJeu = true;
        }
            
        return joueurVeutQuitterJeu;  
     }
    /// <Description de JouerPartie>
    /// Paramètres rentrants : Aucun
    /// La méthode JouerPartie permet au joueur de gérer une partie, pour ce qui est de la génération d'une bonne réponse, de la gestion du nombre de tours,
    /// de la validation du nombre de tours, ainsi que pour afficher l'écran de défaite ou l'écran de victoire.
    /// Valeurs retournées : Aucune
    /// </Description de JouerPartie>
    void JouerPartie()
    
    {
        Console.ForegroundColor = ConsoleColor.Black;
        string combinaisonATrouver = ChoisirCombinaisonATrouver();
        int numTourCourant = 1;
        string combinaisonEntree = "";
        Console.ReadKey();
        Console.Clear();
        bool combinaisonEstValide = false;

        while ((combinaisonEntree != combinaisonATrouver) && (numTourCourant < 4)) //Cette boucle va répéter les actions ci-dessous tant que la combinaisonEntree n'est pas égale à combinaisonATrouver,  
        {                                                                          //et si l'entier numTourCourant est plus petit que quatre.
            combinaisonEntree = JouerTour(combinaisonATrouver, combinaisonEntree, numTourCourant, combinaisonEstValide);
            combinaisonEstValide = ValiderCombinaison(combinaisonEntree);

            if ((combinaisonEntree != combinaisonATrouver) && (combinaisonEstValide == true)) //Cette alternative va augmenter de un l'entier numTourCourant si la chaine combinaisonEntree n'est toujours pas
            {                                                                                 //égale à la chaine combinaisonATrouver, et si le booléen combinaisonEstValide, est égal à
                numTourCourant++;
            }
            else if (combinaisonEstValide == false)                                           //Si le booléen combinaisonEstValide est égal à false, cette alternative va indiquer à l'usager de réecrire une autre
            {                                                                                 //combinaison, vu qu'elle est invalide.
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Combinaison invalide, veuillez réessayez s'il vous plait.");
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        if (numTourCourant >= 4) //Cette alternative va appeler la fonction AfficherEcranFinPartiePerdue si l'entier numTourCourant est plus grand ou égal à 4. 
        {                        //Sinon, la fonction AfficherEcranFinPartieGagnee sera affichée.
            AfficherEcranFinPartiePerdue(combinaisonATrouver);
        }
        else
        {
            AfficherEcranFinPartieGagnee();
        }
    }


    //Tout ce qui a un lien avec la gestion de la partie, c'est-à-dire
    //les méthodes JouerTour, ChoisirCombinaisonATrouver, CompterNombreLettresCorrectes
    //et CompterNombreLettresBienPlacees, est présent dans la région ci-dessous.
    #region Gestion de la partie
    /// <Description de JouerTour>
    /// Paramètres rentrants : string combinaisonATrouver, string combinaisonEntree, int numTourCourant, bool combinaisonEstValide
    /// La méthode JouerTour va gérer les tours de jeu de la partie. Elle comprend le texte affiché, ainsi que le bout de code pour que le joueur puisse rentrer une combinaison.
    /// Par ailleurs, elle va retourner la chaine combinaisonEntree, qui sera convertie en lettres majuscules, peu importe si l'usager a écrie sa combinaison en lettres majuscules ou en lettres minuscules.
    /// Valeurs retournées :  string combinaisonEntree, qui sera convertie en lettres majuscules grâce à ToUpper()
    /// </Description de JouerTour>
    string JouerTour(string combinaisonATrouver, string combinaisonEntree, int numTourCourant, bool combinaisonEstValide)
     {
         Console.WriteLine("Vous êtes au tour numéro : " + numTourCourant);    //Pardonnez le manque d'indentation ici-présent, c'est pour la mise en forme du texte.
         Console.WriteLine(@"
Une combinaison valide est composée des lettres 
'a', 'b' et 'c'. Aucun doublon n'est accepté.");
         Console.WriteLine();
         Console.WriteLine("Veuillez entrez votre combinaison : ");
         
         int nombreLettresCorrectes = CompterNombreLettresCorrectes(combinaisonEntree, combinaisonATrouver, combinaisonEstValide);
         int nombreLettresBienPlacees = CompterNombreLettresBienPlacees(combinaisonEntree, combinaisonATrouver, combinaisonEstValide);
         
         Console.WriteLine("Il y a {0} lettres de correctes.", nombreLettresCorrectes); 
         Console.WriteLine("Il y a {0} lettres de bien placées.", nombreLettresBienPlacees);
         combinaisonEntree = Console.ReadLine();

         return combinaisonEntree.ToUpper();    
      }
    /// <Description de ChoisirCombinaisonATrouver>
    /// Paramètres rentrants : Aucun
    /// Choisit de manière aléatoire une combinaison à trouver, parmi six bonnes réponses possibles. 
    /// </summary>
    /// <returns> Cette fonction va retourner la combinaison à trouver dans le jeu, ainsi que sa longueur, déterminée dans un tableau.</returns>
    string ChoisirCombinaisonATrouver()
    {
      string[] combinaisonsPossibles = new string[]{"ABC","ACB","BAC","BCA","CAB","CBA"};
      Random rnd = new Random();
      return combinaisonsPossibles[rnd.Next(0, combinaisonsPossibles.Length)];
    }
    /// <Description de CompterNombreLettresCorrectes>
    /// Paramètres rentrants : string combinaisonEntree, string combinaisonATrouver, bool combinaisonEstValide
    /// Va compter le nombre de lettres correctes dans la chaine combinaisonEntree, selon ce que la chaine combinaisonATrouver aura comme lettre. 
    /// </summary>
    /// <returns>Valeur retournée : nombreLettresCorrectes, qui est augmentée de 1 si les condtions des alternatives sont remplies.</returns>
    int CompterNombreLettresCorrectes(string combinaisonEntree, string combinaisonATrouver, bool combinaisonEstValide)
    {
        int i = 0;
        int j = 0;
        int nombreLettresCorrectes = 0;
        
        for (i = 0; i < combinaisonEntree.Length; i++)     //Cette boucle imbriquée va calculer le nombre de lettres correctes dans la chaine combinaisonEntree, comparativement
        {                                                  //a la chaine combinaisonATrouver
            for (j = 0; j < combinaisonATrouver.Length; j++)
            {
              if (i == j)
              {
                  nombreLettresCorrectes++;
              }
            }  
        }
        
        return nombreLettresCorrectes;
    }
    /// <Description de CompterNombreLettresBienPlacees>
    /// Paramètres rentrants : string combinaisonEntree, string combinaisonATrouver, bool combinaisonEstValide
    /// Choisit de manière aléatoire une combinaison à trouver, parmi six bonnes réponses possibles. 
    /// </summary>
    /// <returns> Valeur retournée : nombreLettresBienPlacees, qui est augmentée de 1 si les condtions des alternatives sont remplies.</returns>
    int CompterNombreLettresBienPlacees(string combinaisonEntree, string combinaisonATrouver, bool combinaisonEstValide)
    {
        int nombreLettresBienPlacees = 0;
        int positionDuA = combinaisonEntree.IndexOf('A');
        int positionDuB = combinaisonEntree.IndexOf('B');
        int positionDuC = combinaisonEntree.IndexOf('C');
        int positionDuABonneReponse = combinaisonATrouver.IndexOf('A');
        int positionDuBBonneReponse = combinaisonATrouver.IndexOf('B');
        int positionDuCBonneReponse = combinaisonATrouver.IndexOf('C');

        //Les alternatives suivantes vont tester si la position des lettres A, B et C sont les mêmes chez la chaine combinaisonEntree ainsi que chez la
        //chaine combinaisonATrouver. Elles vont aussi tester si la chaine combinaisonEntree a une longueur de 3 caractères, et si le booléen combinaisonEstValide
        //est vrai. Si c'est conditions sont remplies, l'entier nombreLettresBienPlacees sera augmenté de un.

        if (positionDuA == positionDuABonneReponse && (combinaisonEntree.Length == 3) && (combinaisonEstValide == true))
        {
            nombreLettresBienPlacees++;
        }
        if (positionDuB == positionDuBBonneReponse && (combinaisonEntree.Length == 3) && (combinaisonEstValide == true))
        {
            nombreLettresBienPlacees++;
        }
        if (positionDuC == positionDuCBonneReponse && (combinaisonEntree.Length == 3) && (combinaisonEstValide == true))
        {
            nombreLettresBienPlacees++;
        }
        return nombreLettresBienPlacees;
    }
    #endregion                           



    /// <Description de AfficherEcranFinPartieGagnee>
    /// Paramètres rentrants : Aucun
    /// Cette fonction va afficher un message de victoire si le joueur parvient à trouver la bonne combinaison en moins de trois tours.
    /// (Voir ce site pour la police de caractère de l'écran de victoire : http://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20)
    /// </summary>
    /// <returns> Aucun retour de valeur</returns>
    void AfficherEcranFinPartieGagnee()
    {
        Console.Beep(4000,500);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@" 
 __     __  ______   ______  ________   ______   ______  _______   ________ 
|  \   |  \|      \ /      \|        \ /      \ |      \|       \ |        \
| $$   | $$ \$$$$$$|  $$$$$$\\$$$$$$$$|  $$$$$$\ \$$$$$$| $$$$$$$\| $$$$$$$$
| $$   | $$  | $$  | $$   \$$  | $$   | $$  | $$  | $$  | $$__| $$| $$__    
 \$$\ /  $$  | $$  | $$        | $$   | $$  | $$  | $$  | $$    $$| $$  \   
  \$$\  $$   | $$  | $$   __   | $$   | $$  | $$  | $$  | $$$$$$$\| $$$$$   
   \$$ $$   _| $$_ | $$__/  \  | $$   | $$__/ $$ _| $$_ | $$  | $$| $$_____ 
    \$$$   |   $$ \ \$$    $$  | $$    \$$    $$|   $$ \| $$  | $$| $$     \
     \$     \$$$$$$  \$$$$$$    \$$     \$$$$$$  \$$$$$$ \$$   \$$ \$$$$$$$$");
        Console.ReadKey();
    }
    /// <Description de AfficherEcranFinPartiePerdue>
    /// Paramètres rentrants : string combinaisonATrouver
    /// Cette fonction va afficher un message de défaite si le joueur ne parvient pas à trouver la bonne combinaison en moins de trois tours.
    /// La chaine combinaisonATrouver entre en paramètre afin d'afficher dans la console la bonne combinaison que l'usager aurait dû rentrer.
    /// (Voir ce site pour la police de caractère de l'écran de défaite : http://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20)
    /// </summary>
    /// <returns> Aucun retour de valeur.</returns>
    void AfficherEcranFinPartiePerdue(string combinaisonATrouver)
    {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(@"
▓█████▄ ▓█████   █████▒▄▄▄       ██▓▄▄▄█████▓▓█████ 
▒██▀ ██▌▓█   ▀ ▓██   ▒▒████▄    ▓██▒▓  ██▒ ▓▒▓█   ▀ 
░██   █▌▒███   ▒████ ░▒██  ▀█▄  ▒██▒▒ ▓██░ ▒░▒███   
░▓█▄   ▌▒▓█  ▄ ░▓█▒  ░░██▄▄▄▄██ ░██░░ ▓██▓ ░ ▒▓█  ▄ 
░▒████▓ ░▒████▒░▒█░    ▓█   ▓██▒░██░  ▒██▒ ░ ░▒████▒
 ▒▒▓  ▒ ░░ ▒░ ░ ▒ ░    ▒▒   ▓▒█░░▓    ▒ ░░   ░░ ▒░ ░
 ░ ▒  ▒  ░ ░  ░ ░       ▒   ▒▒ ░ ▒ ░    ░     ░ ░  ░
 ░ ░  ░    ░    ░ ░     ░   ▒    ▒ ░  ░         ░   
   ░       ░  ░             ░  ░ ░              ░  ░
 ░                                                  ");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("La bonne combinaison était en réalité :" + combinaisonATrouver);
    Console.ReadKey();
    
    }
    /// <Description de ValiderCombinaison>
    /// Paramètres rentrants : string combinaisonEntree
    /// Cette fonction va vérifier si la combinaison rentrée est conforme aux normes, c'est-à-dire si elle contient des caractères valides (A, B et C dans ce cas)
    /// et si elle est égale à 3 caractères.
    /// </summary>
    /// <returns> On retourne le booléen combinaisonEstValide true = si les conditions des alternatives sont remplies, sinon, elle sera retournée false.</returns>
    bool ValiderCombinaison(string combinaisonEntree)
    {
        bool combinaisonEstValide = false;
        bool combinaisonValideA;
        bool combinaisonValideB;
        bool combinaisonValideC;
        
        combinaisonValideA = combinaisonEntree.Contains("A");
        combinaisonValideB = combinaisonEntree.Contains("B");
        combinaisonValideC = combinaisonEntree.Contains("C");

        if ((combinaisonValideA == true) && (combinaisonValideB == true) && (combinaisonValideC == true) && (combinaisonEntree.Length == 3)) //Cette alternative va vérifier si la chaine combinaisonEntree contient
        {                                                                                                                                    //les caractères A, B et C, et si elle a une longueur de trois caractères. 
            combinaisonEstValide = true;
        }
        else 
        {
            combinaisonEstValide = false;
        }

        return combinaisonEstValide;
    }             
  }
}