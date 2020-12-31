/*
 * Cette classe sert à la gestion d'un Tetromino (composé de ses quatres petits blocs), pour ce qui est de son gel lorsqu'il ne peux plus descendre,
 * de ses colisions avec les autres tetrominos et les murs, ainsi que pour son affichage à l'écran.
 *  
 * 
 * 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.System;

namespace TP1
{
  // ppoulin
  // Pas d'entête de classe
  // CC-1
    public class Tetromino
    {   
        //TetrisGame game = new TetrisGame();
        private int topLeftColumnOffset = 0;   //La position en colonne (ou en X) du Tétromino courant.
        private int topLeftRowOffset = 0;      //La position en rangée (ou en Y) du Tétromino courant.
        private Sprite sprite = null;          //Le sprite associé au Tetromino
        private Texture texture = null;        //La texture associée au sprite associé au Tetromino

            //TetrominoType blockType = TetrominoType.Square;
               //Cube jaune  // La création des instances des petits blocs selon leurs décalages.
                 public TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Yellow);
                 public TetrominoLittleBlock b2 = new TetrominoLittleBlock(0, 1, Color.Yellow);
                 public TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, 0, Color.Yellow);
                 public TetrominoLittleBlock b4 = new TetrominoLittleBlock(1, 1, Color.Yellow);
            
            // ppoulin
            // Les instructions suivantes ne devraient pas se retrouver dans la version finale
            // du travail.
            // CIC-2
            //Barre cyan // La création des instances des petits blocs selon leurs décalages.
              //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Cyan);
              //TetrominoLittleBlock b2 = new TetrominoLittleBlock(1, 0, Color.Cyan);
              //TetrominoLittleBlock b3 = new TetrominoLittleBlock(2, 0, Color.Cyan);
              //TetrominoLittleBlock b4 = new TetrominoLittleBlock(3, 0, Color.Cyan);

            //T magenta // La création des instances des petits blocs selon leurs décalages.
              //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Magenta);
              //TetrominoLittleBlock b2 = new TetrominoLittleBlock(1, 0, Color.Magenta);
              //TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, -1, Color.Magenta);
              //TetrominoLittleBlock b4 = new TetrominoLittleBlock(2, 0, Color.Magenta);

            ////Right snake green // La création des instances des petits blocs selon leurs décalages.
              //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Green);
              //TetrominoLittleBlock b2 = new TetrominoLittleBlock(1, 0, Color.Green);
              //TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, -1, Color.Green);
              //TetrominoLittleBlock b4 = new TetrominoLittleBlock(2, -1, Color.Green);

            ////Left snake red // La création des instances des petits blocs selon leurs décalages.
              //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0,-1, Color.Red);
              //TetrominoLittleBlock b2 = new TetrominoLittleBlock(1,-1, Color.Red);
              //TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, 0, Color.Red);
              //TetrominoLittleBlock b4 = new TetrominoLittleBlock(2, 0, Color.Red);


            //Left gun blue // La création des instances des petits blocs selon leurs décalages.
                //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Blue);
                //TetrominoLittleBlock b2 = new TetrominoLittleBlock(0, 1, Color.Blue);
                //TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, 1, Color.Blue);
                //TetrominoLittleBlock b4 = new TetrominoLittleBlock(2, 1, Color.Blue);

            //Light gun orange // La création des instances des petits blocs selon leurs décalages.
                //TetrominoLittleBlock b1 = new TetrominoLittleBlock(0, 0, Color.Yellow);
                //TetrominoLittleBlock b2 = new TetrominoLittleBlock(0, 1, Color.Yellow);
                //TetrominoLittleBlock b3 = new TetrominoLittleBlock(1, 1, Color.Yellow);
                //TetrominoLittleBlock b4 = new TetrominoLittleBlock(2, 1, Color.Yellow);

                // ppoulin
                // Paramètres non documentés
                // MCP-3
            /// <summary>
            /// Constructeur de la classe Tetromino
            /// </summary>
        public Tetromino(int topLeftColumnOffset, int topLeftRowOffset, TetrominoType type)
        {
            this.topLeftColumnOffset = topLeftColumnOffset;
            this.topLeftRowOffset = topLeftRowOffset;
            texture = new Texture("littleblock.bmp");
            sprite = new Sprite(texture);   
        }
        //Fonction GetTopLeftColumnOffset : Cette méthode va retourner la position en colonne
        // du tetromino pour qu'il soit utilisable dans d'autres classes.
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Cette fonction va retourner l'entier topLeftColumnOffset.
        public int GetTopLeftColumnOffset()
        {
            return topLeftColumnOffset;
        }
        //Fonction GetTopLeftRowOffset : Cette méthode va retourner la position en rangée
        // du tetromino pour qu'il soit utilisable dans d'autres classes.
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Cette fonction va retourner l'entier topLeftRowOffset.
        public int GetTopLeftRowOffset()
        {
            return topLeftRowOffset;
        }

        // ppoulin
        // Méthode non documentée
        // MCP-3
        public int GetHeight(int height)
        {
            return height;
        }
        //Fonction Freeze : Cette méthode va s'occuper de geler les 4 petits blocs du tetromino
        //lorsqu'il ne pourra plus descendre.
        
        // ppoulin
        // Paramètre incorrectement documenté
        // MCP-1
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public void Freeze(TetrisGame game)
        {
            game.FreezeContent(b1.GetParentRowOffset() + topLeftRowOffset, b1.GetParentColumnOffset() + topLeftColumnOffset);
            game.FreezeContent(b2.GetParentRowOffset() + topLeftRowOffset, b2.GetParentColumnOffset() + topLeftColumnOffset);
            game.FreezeContent(b3.GetParentRowOffset() + topLeftRowOffset, b3.GetParentColumnOffset() + topLeftColumnOffset);
            game.FreezeContent(b4.GetParentRowOffset() + topLeftRowOffset, b4.GetParentColumnOffset() + topLeftColumnOffset);
        }
        //Fonction MoveDown : Cette méthode va simplement incrémenter la position en rangée
        //du tetromino, afin de le faire decendre.
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public void MoveDown()
        {
            topLeftRowOffset++;
        }
        // ppoulin
        // Faute de français
        //Fonction CanMoveDown : Cette méthode vérifie si le tetromino peux encore descendre en bas, 
        //                       en prenant compte de ses petits blocs.
        //Paramètres rentrés : - TetrisGame game : La partie de tetris qui est en cours.
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public bool CanMoveDown(TetrisGame game)        //Conçu en colaborationa avec Thomas Gauthier-Cossette
        {
            int potentialNextRowB1 = topLeftRowOffset + b1.GetParentRowOffset()+2;   //L'éventuelle rangée que le bloc pourrait atteindre.
            int potentialNextColumnB1 = topLeftColumnOffset + b1.GetParentColumnOffset(); //L'éventuelle colonne que le bloc pourrait atteindre.

            int potentialNextRowB3 = topLeftRowOffset + b3.GetParentRowOffset()+2;
            int potentialNextColumnB3 = topLeftColumnOffset + b3.GetParentColumnOffset();

            bool[,] logicalGameBoard = game.GetLogicalGameBoard();    //Le tableau logique de la partie.
            
            if (potentialNextRowB1 < TetrisGame.NB_ROWS && topLeftRowOffset < TetrisGame.NB_ROWS)   //On s'assure de ne pas dépasser la limite inférieur de l'écran.
            {
                if ((logicalGameBoard[potentialNextRowB1, potentialNextColumnB1] == true) && (logicalGameBoard[potentialNextRowB3, potentialNextColumnB3] == true))   //On s'assure qu'il n'y a pas de bloc gelé en dessous.
                {
                    return true;   
                }
                else
                {
                    return false;  
                }
            }

            return false;
        }

        //Fonction MoveLeft : Cette méthode va simplement décrémenter la position en colonne
        //du tetromino, afin de le faire bouger à gauche.
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public void MoveLeft()
        {
            topLeftColumnOffset--;
        }

        // ppoulin
        // Faute de français
        //Fonction CanMoveLeft : Cette méthode vérifie si le tetromino peux encore descendre en bouger vers la gauche, 
        //                        en prenant compte de ses petits blocs.
        //Paramètres rentrés : - TetrisGame game : La partie de tetris qui est en cours.
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public bool CanMoveLeft(TetrisGame game)
        {
            int potentialNextRow = topLeftRowOffset + b1.GetParentRowOffset();
            int potentialNextColumn = topLeftColumnOffset + b1.GetParentColumnOffset() + 2;
            bool[,] logicalGameBoard = game.GetLogicalGameBoard();

            if ((potentialNextColumn > 0) && (topLeftColumnOffset > 0))
            {
                if (logicalGameBoard[potentialNextRow, potentialNextColumn-1] == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        //Fonction MoveRight : Cette méthode va simplement incrémenter la position en colonne
        //du tetromino, afin de le faire bouger à droite.
        //Paramètres rentrés : - Aucun
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public void MoveRight()
        {
            topLeftColumnOffset++;
        }
        // ppoulin
        // Faute de français
        //Fonction CanMoveRight : Cette méthode vérifie si le tetromino peux encore descendre en bouger vers la droite, 
        //                        en prenant compte de ses petits blocs.
        //Paramètres rentrés : - TetrisGame game : La partie de tetris qui est en cours.
        //Visibilité : publique
        //
        //Aucune valeur de retour.
        public bool CanMoveRight(TetrisGame game)
        {
            int potentialNextRow = topLeftRowOffset + b1.GetParentRowOffset() ;
            int potentialNextColumn = topLeftColumnOffset + b1.GetParentColumnOffset()+ 2;
            bool[,] logicalGameBoard = game.GetLogicalGameBoard();

            if ((potentialNextColumn < TetrisGame.NB_COLUMNS) && (topLeftColumnOffset < TetrisGame.NB_COLUMNS))
            {
                if (logicalGameBoard[potentialNextRow, potentialNextColumn] == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        //Fonction Draw : Cette méthode va dessiner le sprite à la position en colonne et rangée (en prenant compte des décalages
        //                topLeftColumnOffset et topLeftRowOffset) des petits blocs du tetrominos.
        //Paramètres rentrés : - RenderWindow window : Il s'agit du rendu visuel de la fenêtre de l'application.
        //Visibilité : publique
        //
        //Aucune valeur de retour
        public void Draw(RenderWindow window)
        {
            b1.Draw(window,  topLeftRowOffset, topLeftColumnOffset);
            b2.Draw(window,  topLeftRowOffset, topLeftColumnOffset);
            b3.Draw(window,  topLeftRowOffset, topLeftColumnOffset);
            b4.Draw(window,  topLeftRowOffset, topLeftColumnOffset);
        }
    }
}
