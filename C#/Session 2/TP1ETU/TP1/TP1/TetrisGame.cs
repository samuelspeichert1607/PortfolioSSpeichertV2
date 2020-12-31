

using System;
using System.Windows.Forms;

using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TP1
{
  // ppoulin
  // Pas d'entête de classe
  // CC-1
  public class TetrisGame
  {
      
    // Propriétés spécifiques à SFML pour la gestion de l'affichage du pointage.
    private Text text = null;
    private Font gameFont = new Font( "Comic.ttf" );

    private const int SCREEN_WIDTH_IN_PIXELS =  512;  // Résolution en largeur de l'écran  
    private const int SCREEN_HEIGHT_IN_PIXELS = 768;  // Résolution en hauteur de l'écran
    
    public const int NB_ROWS = SCREEN_HEIGHT_IN_PIXELS / 32; //Constante représensant le nombre de rangées du jeu
    public const int NB_COLUMNS = SCREEN_WIDTH_IN_PIXELS / 32; //Constante représensant le nombre de colonnes du jeu
    
    public const int TETRIS_LITTLE_BLOCK_SIZE = 32; //La taille en pixels d'un petit bloc (en largeur et en hauteur)

    public int framesRemaingingBeforeMovingBlockDown = 10; //Frames restantes avant qu'un bloc tombe (décrémentée dans la méthode Update)
                                                           
    private bool[,] logicalGameBoard = new bool[NB_ROWS, NB_COLUMNS];  //Tableau logique. Le déplacement des pièces se base sur cela.

    Tetromino activeBlock = new Tetromino(7, 0, TetrominoType.Square);  //Tetromino qui est en ce moment contrôllé par l'utilisateur.

    private int currentScore = 0;  //Cet entier représente le score accumulé par le joueur.

    /// <summary>
    /// Constructeur du jeu
    /// </summary>
    public TetrisGame()
    {
      text = new Text( "", gameFont );

      for (int i = 0; i < NB_ROWS; i++)
      {
          for (int j = 0; j < NB_COLUMNS; j++)
          {
              logicalGameBoard[i, j] = true;
          }
      }
      // ppoulin
      // Enlève les lignes qui ne servent pas de la version finale de ton travail.


      //Blocs de test générés. À décommenter s'il y a lieu.
      //for (int j = 2; j < NB_COLUMNS; j++)
      //{
      //    logicalGameBoard[TetrisGame.NB_ROWS - 1, j] = false;
      //    logicalGameBoard[TetrisGame.NB_ROWS - 2, j] = false;
      //}
    }
    // ppoulin
    // Faute de français
    //Fonction FreezeContent : Lorsqu'un tetromino ne peux plus descendre vers le bas, cette méthode va le geler,
    //et s'il y a lieu, supprimer les rangées complétées par ce tetromino.
    //Paramètres rentrés : int rowNum : le numéro de la rangée du bloc gelé 
    //                     int colNum : le numéro de la colonne du bloc gelé  
    //Visibilité : Publique
    //
    //Aucune valeur de retour
    
    public void FreezeContent(int rowNum, int colNum)
    {
	// ppoulin
        // Tu aurais pu utiliser des assertions ici pour t'assurer que les paramètres ne dépassent
        // pas les limites du tableau.
        // SCA-1
        logicalGameBoard[rowNum, colNum] = false;    //Cette assignation va geler les cases occupées par un tetromino déposé.
 
        int i = 0;    //Compteur des rangées
        int j = 0;    //Compteur des colonnes.
        int nbCasesOccupeesParLigne = 0;     //Cet entier va être incrémentée à chaque fois que la boucle ci-dessous détecte une case occupée.

        for (i = 0; i < logicalGameBoard.GetLength(0); i++)        //Aidé par Emile Turcotte : Cette boucle imbriquée va calculer le nombre de cases occupées dans 
        {                                                          //une ligne où l'on vient juste d'y déposer un tetromino.
            nbCasesOccupeesParLigne = 0;
            for (j = 0; j < logicalGameBoard.GetLength(1); j++)
            {
              if (logicalGameBoard[i, j] == false)
              {
                 nbCasesOccupeesParLigne++;
              }
            }    
            if(nbCasesOccupeesParLigne == logicalGameBoard.GetLength(1))   //Si la ligne est pleine...   
            {
                 for(int k = 0; k < logicalGameBoard.GetLength(1); k++)    //...on la vide en entier.
                 {
                    logicalGameBoard[i, k] = true;
                    currentScore++;                 //Incrémentation du score de 16 points par ligne.
                 }   
                MoveLinesDown(rowNum);
            }
        }
    }

    //Fonction MoveLinesDown : Cette fonction va décaler vers le bas les lignes supérieures à une ligne complète qui vient
    // de se faire supprimer.
    //Paramètres rentrés : int rowNum : le numéro de la rangée supprimée 
    //Visibilité : Publique
    //
    //Aucune valeur de retour
    // ppoulin
    // Pourquoi cette méthode est-elle publique?
    // VM-1
    public void MoveLinesDown(int rowNum)
    {
        for (int i = rowNum-1; i >= 0; i--)       //Cette boucle imbriquée décale les rangées en haut de la ligne complète supprimée vers le bas.
        {
            for (int j = 0; j < TetrisGame.NB_COLUMNS; j++)
            {
                logicalGameBoard[i + 1, j] = logicalGameBoard[i, j];
            }
        }
    }
    //Fonction GetActiveBlock : Cette méthode va retourner le tetromino actuel
    // pour qu'il soit utilisable dans d'autres classes.
    //Paramètres rentrés : - Aucun
    //Visibilité : publique
    //
    //Cette fonction va retourner le tetromino activeBlock.
    public Tetromino GetActiveBlock()
    {
        return activeBlock;
    }
    // Fonction CreateNewTetromino : Cette fonction ne fait que retourner un nouveau tetromino contrôlable par l'utilisateur,
    //qui est aussi assigné à la variable activeBlock.                               
    // Aucun paramètre. 
    // Visibilité : publique               
    // Retourne activeBlock, le nouveaun Tetromino contrôlable.
    public Tetromino CreateNewTetromino()
    {
        return activeBlock = new Tetromino(7, 0, TetrominoType.Square);    
    }
    //Fonction GetLogicalGameBoard : Cette méthode va retourner le tableau logique
    // pour qu'il soit utilisable dans d'autres classes.
    //Paramètres rentrés : - Aucun
    //Visibilité : publique
    //
    //Cette fonction va retourner le tableau de booléens logicalGameBoard.
    public bool[,] GetLogicalGameBoard()
    {
        return logicalGameBoard;
    }
    // Fonction Draw : Cette fonction ne fait qu'afficher le score, ainsi que les blocs.                               
    // Paramètre : RenderWindow window : Il s'agit du rendu visuel de la fenêtre de l'application. 
    // Visibilité : publique               
    // Aucune valeur de retour.

    public void Draw(RenderWindow window)
    {
      
      // Affichage du pointage
      text.DisplayedString = string.Format("Score: {0}",currentScore);
      window.Draw(text);
        
      activeBlock.Draw(window); 

      //Boucle imbriquée qui colorie les blocs en gelés en blanc.  
      for (int i = 0; i < NB_ROWS; i++)
      {
          for (int j = 0; j < NB_COLUMNS; j++)
          {
              if (logicalGameBoard[i, j] == false)
              {
                  TetrominoLittleBlock temp = new TetrominoLittleBlock(j, i, Color.White);
                  temp.Draw(window, 0, 0);
              }
          }
      }
      // ppoulin
      // Ce n'est pas l'endroit pour appeler cette méthode.
      // Il faut respecter la sémantique du nom.  La méthode est responsable d'afficher (Draw)
      // pas de mettre à jour.
      // CIC-3
      UpdateGame();
    }
    // Fonction UpdateGame : Cette méthode a plusieurs fonctions. Non seulement elle décrémente .e no                    
    // Aucun paramètre.         
    // Visibilité : publique.        
    // Aucune valeur de retour
    public void UpdateGame()
    { 
       framesRemaingingBeforeMovingBlockDown--;    
        
       if (framesRemaingingBeforeMovingBlockDown == 0)
       {     
           
          if (activeBlock.CanMoveDown(this) == true)       //Si cette alternative retourne faux, le bloc peux donc bouger
          {                                                //et le nombre de frames reviendra à 10.
              activeBlock.MoveDown();
              framesRemaingingBeforeMovingBlockDown = 10;
          }
          else                                             //Sinon, le bloc sera gelé, en plus qu'un nouveau Tetromino sera
          {                                                //généré en haut et que le nombre de frames reviendra à 10.
              activeBlock.Freeze(this);
              activeBlock = CreateNewTetromino();
              framesRemaingingBeforeMovingBlockDown = 10;
          }
       }

        for(int j = 0; j < NB_COLUMNS; j++)      //Cette boucle vérifie si les tetrominos débordent l'écran du jeu.
        {
            if (logicalGameBoard[0,j] == false)
            {
                HandleEndOfGame();
            }
        }
    }
    // Fonction HandleEndOfGame : Cette méthode est appelée pour donner à l'utilisateur le choix de recommencer une partie ou de quitter l'application.                  
    // Aucun paramètre.         
    // Visibilité : privée.        
    // Aucune valeur de retour
    private void HandleEndOfGame()
    {
      string message = "Voulez-vous rejouer?";
      string title = "Perdu!";
      DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      
      if(System.Windows.Forms.DialogResult.No == result)
      {
        // Quitter le jeu
        System.Environment.Exit( 0 );
      }
      else
      {
        // Redémarrer
        InitializeNewGame( );
      }
      
    }
    // Fonction InitializeNewGame : Cette méthode est appelée pour réinitialiser l'application, c'est-à-dire
    // retirer tous les blocs gelés, ainsi que de remettre à zéro le pointage.           
    // Aucun paramètre.         
    // Visibilité : privée.        
    // Aucune valeur de retour
    private void InitializeNewGame( )
    {
        for (int i = 0; i < NB_ROWS; i++)   //Boucle imbriquée qui enlève les blocs gelés.
        {
            for (int j = 0; j < NB_COLUMNS; j++)
            {
                logicalGameBoard[i, j] = true;
            }
        }
        currentScore = 0;
    }
  }
}
