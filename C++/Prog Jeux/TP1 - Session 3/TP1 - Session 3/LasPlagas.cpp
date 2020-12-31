#define _USE_MATH_DEFINES

#include "LasPlagas.h";


LasPlagas::LasPlagas(int startX, int startY, float startAngle)
{
	lasPlagasAngle = startAngle;

	lasPlagasSprite.setTexture(lasPlagasTexture);
	lasPlagasSprite.setPosition(positionX, positionY);
	lasPlagasSprite.setOrigin(lasPlagasTexture.getSize().x / 2, lasPlagasTexture.getSize().y / 2);
	lasPlagasSprite.setRotation(lasPlagasAngle);
}



LasPlagas::LasPlagas()
{
	int startSpot = rand() % 4;
	switch (startSpot)
	{
	case 0:
		positionX = 0;
		positionY = 0;
		break;
	case 1:
		positionX = 1280;
		positionY = 0;
		break;
	case 2:
		positionX = 0;
		positionY = 960;
		break;
	case 3:
		positionX = 1280;
		positionY = 960;
		break;

	}
			
	positionX = rand() % 1280;
	positionY = rand() % 960; 

	lasPlagasTexture.loadFromFile("Assets\\Zombies\\zombie.png");

	lasPlagasSprite.setTexture(lasPlagasTexture);
	lasPlagasSprite.setPosition(positionX, positionY);
	lasPlagasSprite.setOrigin(lasPlagasTexture.getSize().x / 2, lasPlagasTexture.getSize().y / 2);
	lasPlagasSprite.setRotation(lasPlagasAngle);
}

LasPlagas::~LasPlagas()
{

}



int LasPlagas::GetPositionX()
{
	return positionX;
}

int LasPlagas::GetPositionY()
{
	return positionY;
}

void LasPlagas::SetPositionX(int newPositionX)
{
	positionX += newPositionX;
	lasPlagasSprite.setPosition(positionX, positionY);
}

void LasPlagas::SetPositionY(int newPositionY)
{
	positionY += newPositionY;
	lasPlagasSprite.setPosition(positionX, positionY);
}

void LasPlagas::SetAngle(int positionJoueurX, int positionJoueurY)
{
	lasPlagasAngle = (atan2f((positionJoueurY - positionY), (positionJoueurX - positionX))) ;
	lasPlagasSprite.setRotation(lasPlagasAngle * 180 / M_PI);
}


Sprite LasPlagas::GetSprite()
{
	return lasPlagasSprite;
}

Texture LasPlagas::GetTexture()
{
	return lasPlagasTexture;
}

void LasPlagas::Update()
{
	SetPositionX(cos(lasPlagasAngle) * SPEED);
	SetPositionY(sin(lasPlagasAngle) * SPEED);



}