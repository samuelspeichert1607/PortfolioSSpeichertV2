using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP1;
namespace UnitTestsTP1
{
  [TestClass]
  public class TestsTetromino
  {
       /// <summary>
       /// Test du constructeur de Tetromino
       /// </summary>
       [TestMethod]
       public void TestTetrominoCtor( )
       {
         Tetromino block = new Tetromino( 5, 0, TetrominoType.Square );
         Assert.AreEqual( 0, block.GetTopLeftRowOffset() );
         Assert.AreEqual( 5, block.GetTopLeftColumnOffset() );
         Assert.AreEqual( 2, block.GetHeight(2) );   //Je ne sais pas si c'est une bonne valeur à rentrer...  
       }

        //<summary>
        //Test de la méthode CanMoveDown OK #1.
        //On s'assure qu'un bloc qui vient d'être créé dans une 
        //nouvelle partie puisse descendre.
        //</summary>
       [TestMethod]
       public void TestCanMoveDownOK01( )
       {
         Tetromino block = new Tetromino( 7, 0, TetrominoType.Square );
         TetrisGame game = new TetrisGame();
         Assert.IsTrue( block.CanMoveDown( game ) );      
       }
       /// <summary>
       /// Test de la méthode CanMoveDown OK #2
       /// On s'assure qu'un bloc seul situé au centre (vertical) dans une 
       /// nouvelle partie puisse descendre. 
       /// </summary>
       [TestMethod]
       public void TestCanMoveDownOK02( )
       {
         Tetromino block = new Tetromino(7, TetrisGame.NB_ROWS / 2, TetrominoType.Square);

         TetrisGame game = new TetrisGame( );

         Assert.IsTrue( block.CanMoveDown( game ) );
       }
       /// <summary>
       /// Test de la méthode CanMoveDown pas OK #1
       /// On s'assure qu'un bloc situé dans le bas de la rangée ne peux pas bouger.
       /// </summary>
       [TestMethod]
       public void TestCanMoveDownNotOK01( )
       {
         Tetromino block = new Tetromino( 1, TetrisGame.NB_ROWS-2, TetrominoType.Square );
         TetrisGame game = new TetrisGame( );
         Assert.IsFalse( block.CanMoveDown( game ) );
       }

       /// <summary>
       /// Test de la méthode CanMoveDown pas OK #2
       /// On s'assure qu'un bloc ayant un bloc figé sous lui ne peut pas descendre.
       /// </summary>
       [TestMethod]
       public void TestCanMoveDownNotOK02( )
       {
         Tetromino block = new Tetromino( 1, TetrisGame.NB_ROWS-3, TetrominoType.Square );
         TetrisGame game = new TetrisGame( );
         game.FreezeContent(TetrisGame.NB_ROWS-1,1);
         Assert.IsFalse( block.CanMoveDown( game ) );
       }
       /// <summary>
       /// Test de la méthode CanMoveDown pas OK #3
       /// On s'assure qu'un bloc ayant un bloc figé sous lui ne peut pas descendre.
       /// </summary>
       [TestMethod]
       public void TestCanMoveDownNotOK03()
       {
           Tetromino block = new Tetromino(1, TetrisGame.NB_ROWS - 2, TetrominoType.Square);
           TetrisGame game = new TetrisGame();
           Assert.IsFalse(block.CanMoveDown(game));
       }
       /// <summary>
       /// Test de la méthode CanMoveDown pas OK #4
       /// On s'assure qu'un bloc hors de la zone de jeu ne puisse pas descendre.
       /// </summary>
       [TestMethod]
       public void TestCanMoveDownNotOK04()
       {
           Tetromino block = new Tetromino(4, 237483878, TetrominoType.Square);
           TetrisGame game = new TetrisGame();
           Assert.IsFalse(block.CanMoveDown(game));
       }


       /// <summary>
       /// Test de la méthode MoveDown #1
       /// (S'assure si un bloc tombe bien)
       /// </summary>
       [TestMethod]
       public void TestMoveDown01( )
       {
         Tetromino block = new Tetromino( 7, 0, TetrominoType.Square );
         block.MoveDown();
         Assert.AreEqual( 1, block.GetTopLeftRowOffset());
       }

       /// <summary>
       /// Test de la méthode MoveDown #2
       /// (S'assure si un bloc ne traverse pas les limites de la zone de jeu.)
       /// </summary>

      //<summary>
      //Test de la méthode CanMoveLeft OK #1.
      //On s'assure qu'un bloc qui vient d'être créé dans une 
      //nouvelle partie puisse bouger vers la gauche.
      //</summary>
      [TestMethod]
      public void TestCanMoveLeftOK01()
      {
          Tetromino block = new Tetromino(7, 0, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsTrue(block.CanMoveLeft(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveLeft OK #2
      /// On s'assure qu'un bloc seul situé au centre (vertical) dans une 
      /// nouvelle partie puisse bouger vers la gauche. 
      /// </summary>
      [TestMethod]
      public void TestCanMoveLeftOK02()
      {
          Tetromino block = new Tetromino(7, TetrisGame.NB_ROWS / 2, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsTrue(block.CanMoveLeft(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveLeft pas OK #1
      /// On s'assure qu'un bloc situé dans la limite-gauche ne peux pas bouger.
      /// </summary>
      [TestMethod]
      public void TestCanMoveLeftNotOK01()
      {
          Tetromino block = new Tetromino(1, TetrisGame.NB_ROWS - 2, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsTrue(block.CanMoveLeft(game));
      }

      /// <summary>
      /// Test de la méthode CanMoveLeft pas OK #2
      /// On s'assure qu'un bloc ayant un bloc figé
      /// à sa gauche ne peut pas bouger vers la gauche.
      /// </summary>
      [TestMethod]
      public void TestCanMoveLeftNotOK02()
      {
          Tetromino block = new Tetromino(10, 5, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          game.FreezeContent(5, 8);
          Assert.IsFalse(block.CanMoveLeft(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveLeft pas OK #3
      /// On s'assure qu'un bloc hors des limites
      /// ne peut pas bouger vers la gauche.
      /// </summary>
      [TestMethod]
      public void TestCanMoveLeftNotOK03()
      {
          Tetromino block = new Tetromino(-99, TetrisGame.NB_ROWS - 2, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsFalse(block.CanMoveLeft(game));
      }
      /// <summary>
      /// Test de la méthode MoveLeft #1
      /// (S'assure si un bloc tombe bien)
      /// </summary>
      [TestMethod]
      public void TestMoveLeft01()
      {
          Tetromino block = new Tetromino(7, 5, TetrominoType.Square);
          block.MoveLeft();
          Assert.AreEqual(6, block.GetTopLeftColumnOffset());
      }

      //<summary>
      //Test de la méthode CanMoveRight OK #1.
      //On s'assure qu'un bloc qui vient d'être créé dans une 
      //nouvelle partie puisse bouger vers la droite.
      //</summary>
      [TestMethod]
      public void TestCanMoveRightOK01()
      {
          Tetromino block = new Tetromino(7, 0, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsTrue(block.CanMoveRight(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveRight OK #2
      /// On s'assure qu'un bloc seul situé au centre (vertical) dans une 
      /// nouvelle partie puisse bouger vers la droite. 
      /// </summary>
      [TestMethod]
      public void TestCanMoveRightOK02()
      {
          Tetromino block = new Tetromino(7, TetrisGame.NB_ROWS / 2, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsTrue(block.CanMoveRight(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveRight pas OK #1
      /// On s'assure qu'un bloc situé dans la limite-droite ne peux pas bouger.
      /// </summary>
      [TestMethod]
      public void TestCanMoveRightNotOK01()
      {
          Tetromino block = new Tetromino(TetrisGame.NB_COLUMNS - 1, 6, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsFalse(block.CanMoveRight(game));
      }

      /// <summary>
      /// Test de la méthode CanMoveRight pas OK #2
      /// On s'assure qu'un bloc ayant un bloc figé
      /// à sa droite ne peut pas bouger vers la droite.
      /// </summary>
      [TestMethod]
      public void TestCanMoveRightNotOK02()
      {
          Tetromino block = new Tetromino(9, 7, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          game.FreezeContent(7, 10);
          Assert.IsFalse(block.CanMoveRight(game));
      }
      /// <summary>
      /// Test de la méthode CanMoveRight pas OK #3
      /// On s'assure qu'un bloc hors des limites
      /// ne peut pas bouger vers la gauche.
      /// </summary>
      [TestMethod]
      public void TestCanMoveRightNotOK03()
      {
          Tetromino block = new Tetromino(99, TetrisGame.NB_ROWS - 2, TetrominoType.Square);
          TetrisGame game = new TetrisGame();
          Assert.IsFalse(block.CanMoveRight(game));
      }
      /// <summary>
      /// Test de la méthode MoveRight #1
      /// (S'assure si un bloc tombe bien)
      /// </summary>
      [TestMethod]
      public void TestMoveRight01()
      {
          Tetromino block = new Tetromino(7, 5, TetrominoType.Square);
          block.MoveRight();
          Assert.AreEqual(8, block.GetTopLeftColumnOffset());
      }


  }
}
