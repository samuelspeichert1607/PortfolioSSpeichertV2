#include "Ennemy.h"

Texture Ennemy::texture;

Ennemy::Ennemy(const float posX, const float posY, RenderWindow* const renderWindow)
: Acteur(posX, posY, renderWindow)
{
	setPosition(posX, posY);
}

Ennemy::~Ennemy()
{
	/*for (size_t i = 0; i < NBR_NIVEAUX; i++)
	{
	delete[] intRectsImmobile[i];
	delete[] intRectsMouvement[i];
	}*/
//	delete[] intRectsImmobile;
//	delete[] intRectsMouvement;
}

//bool Ennemy::chargerTextures(const char texturePath[])
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


void Ennemy::deplacement(bool direction)
{

	if (direction == true)
	{
		setPosition(getPosition().x + ENNEMY_SPEED, getPosition().y);
	}
	else if (direction == false)
	{
		setPosition(getPosition().x - ENNEMY_SPEED, getPosition().y);
	}
}

void Ennemy::update()
{
	velocityY += 3; //2 = gravité
	move(0, velocityY);
}
void Ennemy::stop()
{
	velocityY = 0;
}