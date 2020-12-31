#include "Goomba.h"
using namespace platformer;

Texture Goomba::texture;

Goomba::Goomba(const float posX, const float posY, RenderWindow* const renderWindow)
: Ennemy(posX, posY, renderWindow)
{
	setPosition(posX, posY);
}

Goomba::~Goomba()
{

}

void Goomba::deplacement(bool direction)
{

	if (direction == true)
	{
		setPosition(getPosition().x + VITESSE / 3, getPosition().y);
	}
	else if (direction == false)
	{
		setPosition(getPosition().x - VITESSE / 3, getPosition().y);
	}
}


void Goomba::update()
{
	velocityY += .00003; 
	move(0, velocityY);
}
void Goomba::stop()
{
	velocityY = 0;
}
