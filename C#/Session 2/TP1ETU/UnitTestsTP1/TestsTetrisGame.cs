using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TP1
{
  [TestClass]
  public class TestsTetrisGame
  {
    ///////////////////////////////////////////////////////////////////////////////
    // REMARQUES IMPORTANTES                                                     //
    // POUR RÉALISER VOS TESTS, VOUS POUVEZ ASSUMER QUE LA MÉTHODE FREEZECONTENT //
    // EST FONCTIONNELLE ET A ÉTÉ CORRECTEMENT TESTÉE. IDEM POUR LE CONSTRUCTEUR //
    // DE LA CLASSE TETRISGAME                                                   //
    ///////////////////////////////////////////////////////////////////////////////


      //La méthode IsRowCompleted est correctement testée
      //La méthode RemoveCompletedRows est correctement testée

      /// <summary>
      /// Test du constructeur de TetrisGame
      /// </summary>
      [TestMethod]
      public void TestTetrisGameCtor()
      {
          TetrisGame game = new TetrisGame();
          bool[,] logicalGameBoard = game.GetLogicalGameBoard();

          for (int i = 0; i < logicalGameBoard.GetLength(0); i++)
          {
              for (int j = 0; j < logicalGameBoard.GetLength(1); j++)
              {
                  Assert.IsTrue(logicalGameBoard[i, j] == true);
              }
          }
      }
      //<summary>
      //Test de la méthode TestFreezeContent01. 
      //On s'assure qu'un bloc qui vient d'être déposé
      //tout en bas est gelé.
      //</summary>
      [TestMethod]
      public void TestFreezeContent01()
      {
         Tetromino block = new Tetromino(7, TetrisGame.NB_ROWS-1, TetrominoType.Square);
         TetrisGame game = new TetrisGame();
         bool[,] logicalGameBoard = game.GetLogicalGameBoard();

         game.FreezeContent(TetrisGame.NB_ROWS - 1, 7);
         Assert.IsFalse(logicalGameBoard[TetrisGame.NB_ROWS-1, 7]);        
      }
      //<summary>
      //Test de la méthode FreezeContent02.
      //On s'assure qu'un bloc qui vient d'être déposé
      //sur un bloc gelé est gelé.
      //</summary>
      [TestMethod]
      public void TestFreezeContent02()
      {
      // ppoulin
      // Test redondant avec le précédent.
      // Je ne comprends pas la situation que tu as voulu tester

          Tetromino block = new Tetromino(9, 18, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          bool[,] logicalGameBoard = game.GetLogicalGameBoard();
          game.FreezeContent(19, 9);
          Assert.IsFalse(logicalGameBoard[19, 9]);
      }
      //<summary>
      //Test de la méthode FreezeContent (Côté completion ligne).
      //On s'assure que la ligne est complète.
      //</summary>
      [TestMethod]
      public void TestIsRowCompleted()
      {
          TetrisGame game = new TetrisGame();
          bool[,] logicalGameBoard = game.GetLogicalGameBoard();
          //Blocs de test générés. À décommenter s'il y a lieu.
          int j = 2;
          for (j = 2; j < logicalGameBoard.GetLength(1); j++)
          {
              logicalGameBoard[TetrisGame.NB_ROWS - 1, j] = false;
              logicalGameBoard[TetrisGame.NB_ROWS - 2, j] = false;    
          }
          Tetromino block = new Tetromino(0, TetrisGame.NB_ROWS - 2, TetrominoType.Square);

          for (int k = 0; k < logicalGameBoard.GetLength(1)-1; k++)
          {
              Assert.IsFalse(logicalGameBoard[TetrisGame.NB_ROWS - 1, k]);
              Assert.IsFalse(logicalGameBoard[TetrisGame.NB_ROWS - 2, k]);
          }
         // ppoulin
         // Ce test ne teste pas la méthode IsRowCompleted
      }
  }
}
