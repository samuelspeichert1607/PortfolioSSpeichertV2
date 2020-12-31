#include "Collectible.h"

using namespace platformer;

Collectible::Collectible(const float posX, const float posY, RenderWindow* const renderWindow)
{
	setPosition(posX, posY);
	startPositionX = posX;
	startPositionY = posY;
	direction = DIRECTION::UP;
}

Collectible::~Collectible()
{
}

bool Collectible::chargerTextures(const char texturePath[])
{
	if (!texture.loadFromFile(texturePath))
	{
		return false;
	}

	texture.setSmooth(false);
	setTexture(texture);
	return true;
}

void Collectible::update()
{
	if (direction == DIRECTION::UP)
	{
		setPosition(getPosition().x, getPosition().y - SPEED);
	}
	else 
	{
		setPosition(getPosition().x, getPosition().y + SPEED);
	}
	if (getPosition().y < startPositionY - texture.getSize().y)
	{
		direction = DIRECTION::DOWN;
	}
	if (getPosition().y > startPositionY)
	{
		direction = DIRECTION::UP;
	}
}

int Collectible::getValue()
{
	return value;
}