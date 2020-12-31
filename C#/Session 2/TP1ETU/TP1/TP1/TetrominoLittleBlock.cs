/*
 * Cette classe sert à la gestion d'un des quatres petits blocs d'un Tetromino, 
 *pour ce qui est de son visuel ainsi que de son décalage.
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
    public class TetrominoLittleBlock
    {
    private Sprite sprite = null;    //Sprite du petit bloc.
    private Texture texture = null;  //Sprite du petit bloc.
    // ppoulin
    // Faute de français
    private int topLeftColumnOffset = 0; //Décallage en colonne du petit bloc.
    private int topLeftRowOffset = 0;    //Décallage en rangée du petit bloc.

    /// <summary>
    /// Constructeur du petit bloc
    /// </summary>
      public TetrominoLittleBlock(int topLeftColumnOffset, int topLeftRowOffset, Color blockColor)
      {
          this.topLeftColumnOffset = topLeftColumnOffset;
          this.topLeftRowOffset = topLeftRowOffset;
          texture = new Texture("littleblock.bmp");
          sprite = new Sprite(texture);
          sprite.Color = blockColor;
      }


      //Fonction GetParentRowOffset : Cette méthode va retourner la position en rangée
      // du petit bloc pour qu'il soit utilisable dans d'autres classes.
      //Paramètres rentrés : - Aucun
      //Visibilité : publique
      //
      //Cette fonction va retourner l'entier topLeftRowOffset.
      public int GetParentRowOffset()
      {
        return topLeftRowOffset;
      }
      //Fonction GetParentColumnOffset : Cette méthode va retourner la position en colonne
      // du petit bloc pour qu'il soit utilisable dans d'autres classes.
      //Paramètres rentrés : - Aucun
      //Visibilité : publique
      //
      //Cette fonction va retourner l'entier topLeftColumnOffset.
      public int GetParentColumnOffset()
      {
        return topLeftColumnOffset;
      }
      //Fonction Draw : Cette méthode va dessiner le sprite à la position en colonne et rangée du petit bloc.
      //Paramètres rentrés : - RenderWindow window : Il s'agit du rendu visuel de la fenêtre de l'application.
      //                     - int parentRow : Il s'agit de la rangée utilisée par
      //                     - int parentColumn :
      //Visibilité : publique
      //
      //Cette fonction va retourner l'entier topLeftColumnOffset. 
      public void Draw(RenderWindow window, int parentRow, int parentColumn)
      {
          // Vous pouvez utiliser d'autres couleurs dans l'énumération Color.
          sprite.Position = new Vector2f((GetParentColumnOffset() + parentColumn) * 32, (GetParentRowOffset() + parentRow) * 32);
          window.Draw(sprite);
      }
    }
}
