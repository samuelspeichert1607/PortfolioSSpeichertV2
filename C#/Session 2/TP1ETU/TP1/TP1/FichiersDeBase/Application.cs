using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TP1
{
  public class Application
  {
    private RenderWindow window = null;
    private Color backgroundColor = Color.Black;
    TetrisGame game = null;
    private void OnClose( object sender, EventArgs e )
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close( );
    }
    void OnMouseMoved( object sender, MouseMoveEventArgs e )
    {
      // A COMPLETER SELON LES BESOINS    
    }
    void OnMousePressed( object sender, MouseButtonEventArgs e )
    {
      // A COMPLETER SELON LES BESOINS     
    }
    void OnMouseReleased( object sender, MouseButtonEventArgs e )
    {
      // A COMPLETER SELON LES BESOINS
    }
    void OnKeyPressed( object sender, KeyEventArgs e )
    {
      // A DECOMMENTER LORSQUE VOUS AUREZ CODÉ LES MÉTHODES CONCERNÉES
      
      if ( e.Code == Keyboard.Key.Left && game.GetActiveBlock( ).CanMoveLeft( game ) )
        game.GetActiveBlock( ).MoveLeft( );
      else if ( e.Code == Keyboard.Key.Right && game.GetActiveBlock( ).CanMoveRight( game) )
        game.GetActiveBlock( ).MoveRight( );
      else if ( e.Code == Keyboard.Key.Space && game.GetActiveBlock( ).CanMoveDown( game ) )
        game.GetActiveBlock( ).MoveDown( );                                                
      /*  
      else if ( e.Code == Keyboard.Key.Up && game.GetActiveBlock( ).CanRotateCW( game ) )
        game.GetActiveBlock( ).RotateCW( );
      else if ( e.Code == Keyboard.Key.Down && game.GetActiveBlock( ).CanRotateCCW( game ) )
        game.GetActiveBlock( ).RotateCCW( );
      */
    }
    public Application( string windowTitle, int width, int height )
    {
#region Code nécessaire pour connecter les événements avec les fonctions      
      window = new RenderWindow( new SFML.Window.VideoMode( (uint)width, (uint)height ), windowTitle );
      window.Closed += new EventHandler( OnClose );
      window.KeyPressed += new EventHandler<KeyEventArgs>( OnKeyPressed );
      window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>( OnMousePressed );
      window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>( OnMouseReleased );
      window.MouseMoved += new EventHandler<MouseMoveEventArgs>( OnMouseMoved );
      window.SetFramerateLimit(30);
#endregion
      game = new TetrisGame( );
    }


    public void Run( )
    {
      window.SetActive( );
      while ( window.IsOpen )
      {
        game.UpdateGame();
        window.Clear( backgroundColor );
        window.DispatchEvents( );
        game.Draw( window );
        window.Display( );
      }
    }

    
  }
}
