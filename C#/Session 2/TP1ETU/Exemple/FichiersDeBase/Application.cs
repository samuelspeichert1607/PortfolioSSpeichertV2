using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Exemple
{
  class Application
  {
    private RenderWindow window = null;

    // Vous pouvez mettre une autre couleur provenant de l'énumération Color
    private Color backgroundColor = Color.Red;


    // Propriétés spécifiques à SFML pour la gestion de l'affichage du pointage.
    Text text = null;

    // Attention, ici on mentionne que la police à utiliser doit être Comic.ttf. Ce fichier DOIT être dans le 
    // même dossier que le fichier .exe i.-e. généralement bin/Debug/. Voici ce que vous devez faire pour vous
    // assurer que ce sera le cas.
    // 1) Choisissez la police que vous souhaitez utiliser (un fichier .ttf). Cette police peut être téléchargée
    //    si vous respectez les droits d'auteur.
    // 2) Placez le fichier .ttf dans le dossier où se trouve le fichir .csproj de votre projet.
    // 3) Ajoutez le fichir .ttf dans votre projet (bouton droit sur le projet + Ajouter + Élément existant). Il se
    //    peut que le filtre ne permette pas l'affichage du fichier .ttf.  Changez-le au besoin.
    // 4) Faites afficher les propriétés du fichier .ttf que vous avez ajouté et, choisissez "Copier si plus récent" 
    //    pour l'option "Copier dans le répertoire de sortie".
    Font gameFont = new Font( "Comic.ttf" );


    // Propriété SFML pour l'affichage du bloc    
    private Sprite sprite = null;
    private Texture texture = null;

    // Propriété SFML pour spécifier la position du bloc
    Vector2f positionDuBloc = new Vector2f(0,0);

    string texteAAfficher = "";
    private void OnClose( object sender, EventArgs e )
    {
      // Instructions requises pour fermer la fenêtre
      RenderWindow window = (RenderWindow)sender;
      window.Close( );
    }
    void OnMouseMoved( object sender, MouseMoveEventArgs e )
    {
      // Il est possible d'obtenir les coordonnées de la souris avec e.X et e.Y
      positionDuBloc = new Vector2f( e.X, e.Y );
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
      // Il est possible d'obtenir le code de la touche pressée avec e.Code
      texteAAfficher = string.Format("La touche entrée est {0}", e.Code);

      // Vous pouvez faire un traitement particulier ainsi:
      if(e.Code == Keyboard.Key.Down)
      {
        // Comme traitment, on change la couleur de l'arrière-plan en vert
        backgroundColor = Color.Green;
      }
      else
      {
        // Comme traitment, on change la couleur de l'arrière-plan en rouge
        backgroundColor = Color.Red;
      }
      
    }
    public Application( string windowTitle, uint width, uint height )
    {
      
      window = new RenderWindow( new SFML.Window.VideoMode( width, height ), windowTitle );
      #region Connexion des événements associés à la fenêtre avec les méthodes précédentes
      window.Closed += new EventHandler( OnClose );
      window.KeyPressed += new EventHandler<KeyEventArgs>( OnKeyPressed );
      window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>( OnMousePressed );
      window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>( OnMouseReleased );
      window.MouseMoved += new EventHandler<MouseMoveEventArgs>( OnMouseMoved );
      #endregion


      
      // Instantiation des propriétés pour l'affichage du bloc
      // Attention, ici on mentionne que la texture à utiliser doit être littleblock.bmp. Ce fichier DOIT être dans le 
      // même dossier que le fichier .exe i.-e. généralement bin/Debug/. Voici ce que vous devez faire pour vous
      // assurer que ce sera le cas.
      // 1) Choisissez la texture que vous souhaitez utiliser (un fichier .bmp fonctionne). Cette texture peut être 
      //    téléchargée si vous respectez les droits d'auteur.
      // 2) Placez le fichier .bmp dans le dossier où se trouve le fichir .csproj de votre projet.
      // 3) Ajoutez le fichir .bmp dans votre projet (bouton droit sur le projet + Ajouter + Élément existant). Il se
      //    peut que le filtre ne permette pas l'affichage du fichier .bmp.  Changez-le au besoin.
      // 4) Faites afficher les propriétés du fichier .bmp que vous avez ajouté et, choisissez "Copier si plus récent" 
      //    pour l'option "Copier dans le répertoire de sortie". 

      texture = new Texture( "littleblock.bmp" );
      sprite = new Sprite( texture );
      // Vous pouvez utiliser d'autres couleurs dans l'énumération Color.
      sprite.Color = Color.Blue; 
    }


    public void Run( )
    {
      window.SetActive( );
      while ( window.IsOpen )
      {
        window.Clear( backgroundColor );
        window.DispatchEvents( );
        Draw( );
        window.Display( );
      }
    }

    public void Draw( )
    {
      text = new Text( texteAAfficher, gameFont );
      window.Draw(text);

      sprite.Position = positionDuBloc;
      window.Draw(sprite);
    }
  }
}
