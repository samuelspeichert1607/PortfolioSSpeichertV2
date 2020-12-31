#include "Spike.h"
using namespace platformer;

Texture Spike::texture;

Spike::Spike(const float posX, const float posY, RenderWindow* const renderWindow)
: Ennemy(posX, posY, renderWindow)
{
	setPosition(posX, posY);
}

Spike::~Spike()
{
	
}

void Spike::deplacement(bool direction)
{

	if (direction == true)
	{
		setPosition(getPosition().x + VITESSE / 3, getPosition().y);
	//	assert(getPosition().x + VITESSE / 3 > getPosition().x);
	}
	else if (direction == false)
	{
		setPosition(getPosition().x - VITESSE / 3, getPosition().y);
	//	assert(getPosition().x + VITESSE / 3 < getPosition().x);
	}

}

void Spike::update()
{
	velocityY += .00003;
	move(0, velocityY);
}
void Spike::stop()
{
	velocityY = 0;
}
