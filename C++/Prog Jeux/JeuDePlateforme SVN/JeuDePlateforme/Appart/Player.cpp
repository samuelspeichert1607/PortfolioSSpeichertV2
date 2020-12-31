#include "Player.h"
using namespace platformer;

Texture Player::playerT;

Player::Player(const float posX, const float posY, RenderWindow* const renderWindow) 
: Acteur(posX,posY,renderWindow)
{
	nbLife = 3;
}

Player::~Player()
{
	//for (size_t i = 0; i < NBR_NIVEAUX; i++)
	//{
	//	delete[] intRects[i];
	//	delete[] intRects[i];
	//}
	//delete[] intRects;
	//delete[] intRects;
}

void Player::keyboardMovement()
{
	if (Keyboard::isKeyPressed(Keyboard::Right))
	{
		interfaceDeplacement.x = CLAVIER_DROITE;
		interfaceDeplacement.y = 0.0f;
		for (size_t i = 0; i < NBR_NIVEAUX; i++)
		{
			delete[] intRects[i];
			delete[] intRects[i];
		}

	}
	delete[] intRects;
	delete[] intRects;
}



void Player::deplacement(bool direction) 
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


void Player::update()
{
	velocityY += 3; //2 = gravité
	move(0, velocityY);
}

void Player::jump()
{
	setPosition(getPosition().x, getPosition().y - getTexture()->getSize().y / 2); //Doit rester pour que le personnage puisse sauter.
	velocityY -= 24;
}



void Player::stop()
{
<<<<<<< .mine
||||||| .r11

=======
	velocityY = 0;
}

>>>>>>> .r12
<<<<<<< .mine
	velocityY = 0;
}||||||| .r11
	velocityY = 0;
	//setPosition(getPosition().x,getPosition().y - 1);
	//
}=======
void Player::retriveLife()
{
	nbLife--;
}

int Player::getNbLife()
{
	return nbLife;
}

>>>>>>> .r12
