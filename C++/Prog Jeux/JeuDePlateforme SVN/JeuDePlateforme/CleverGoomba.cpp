#include "CleverGoomba.h"
using namespace platformer;

Texture CleverGoomba::texture;

CleverGoomba::CleverGoomba(const float posX, const float posY, RenderWindow* const renderWindow)
: Ennemy(posX, posY, renderWindow)
{
	setPosition(posX, posY);
}

CleverGoomba::~CleverGoomba()
{
	/*for (size_t i = 0; i < NBR_NIVEAUX; i++)
	{
	delete[] intRectsImmobile[i];
	delete[] intRectsMouvement[i];
	}*/
//	delete[] intRectsImmobile;
//	delete[] intRectsMouvement;
}

//bool CleverGoomba::chargerTextures(const char texturePath[])
//{
//	if (!texture.loadFromFile(texturePath))
//	{
//		return false;
//	}
//
//
//	/*<<<<<<< .mine
//
//	playerT.setSmooth(false);
//	||||||| .r2*/
//
//
//	//playerT.setSmooth(false);
//	//=======
//	texture.setSmooth(false);
//	//>>>>>>> .r4
//	return true;
//}

//void CleverGoomba::ajustementsVisuels()
//{
//	setTexture(texture);
//	setOrigin(texture.getSize().x / 2, texture.getSize().y / 2);
//	intRectsImmobile = new IntRect[NBR_ANIMS_IMMOBILE];
//	intRectsMouvement = new IntRect[NBR_ANIMS_MOUVEMENT];
//	intRectsJump = new IntRect[NBR_ANIMS_JUMP];
//	intRectsMort = new IntRect[NBR_ANIMS_MORT];
//	intRectsVictoire = new IntRect[NBR_ANIMS_VICTOIRE];
//
//	int hauteur = texture.getSize().y / NBR_NIVEAUX;
//
//	for (size_t i = 0; i < NBR_ANIMS_IMMOBILE; i++)
//	{
//		int largeur = texture.getSize().x / NBR_ANIMS_IMMOBILE;
//		intRectsImmobile[i].left = largeur * i;
//		intRectsImmobile[i].top = hauteur * i;
//		intRectsImmobile[i].width = largeur;
//		intRectsImmobile[i].height = hauteur;
//	}
//
//	for (size_t i = 0; i < NBR_ANIMS_MOUVEMENT; i++)
//	{
//		int largeur = texture.getSize().x / NBR_ANIMS_MOUVEMENT;
//		intRectsMouvement[i].left = largeur * i;
//		intRectsMouvement[i].top = hauteur * i;
//		intRectsMouvement[i].width = largeur;
//		intRectsMouvement[i].height = hauteur;
//	}
//
//	for (size_t i = 0; i < NBR_ANIMS_JUMP; i++)
//	{
//		int largeur = texture.getSize().x / NBR_ANIMS_JUMP;
//		intRectsJump[i].left = largeur * i;
//		intRectsJump[i].top = hauteur * i;
//		intRectsJump[i].width = largeur;
//		intRectsJump[i].height = hauteur;
//	}
//
//	for (size_t i = 0; i < NBR_ANIMS_MORT; i++)
//	{
//		int largeur = texture.getSize().x / NBR_ANIMS_MORT;
//		intRectsMort[i].left = largeur * i;
//		intRectsMort[i].top = hauteur * i;
//		intRectsMort[i].width = largeur;
//		intRectsMort[i].height = hauteur;
//	}
//
//	for (size_t i = 0; i < NBR_ANIMS_VICTOIRE; i++)
//	{
//		int largeur = texture.getSize().x / NBR_ANIMS_VICTOIRE;
//		intRectsVictoire[i].left = largeur * i;
//		intRectsVictoire[i].top = hauteur * i;
//		intRectsVictoire[i].width = largeur;
//		intRectsVictoire[i].height = hauteur;
//	}
//
//
//	//setTextureRect(intRectsImmobile[0]); Jeammy, j'ai moi-même décommenté ceci : a décommenter lorsque tu veux
//	//setOrigin(intRectsImmobile[0].height / 2, intRectsImmobile[0].width / 2);
//}

void CleverGoomba::deplacement(bool direction)
{

	if (direction == true)
	{
		setPosition(getPosition().x + VITESSE, getPosition().y);
	}
	else if (direction == false)
	{
		setPosition(getPosition().x - VITESSE, getPosition().y);
	}

}

void CleverGoomba::update()
{
	velocityY += .00003;
	move(0, velocityY);
}
void CleverGoomba::stop()
{
	velocityY = 0;
}
