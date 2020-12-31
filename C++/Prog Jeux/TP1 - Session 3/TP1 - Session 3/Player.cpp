#include "Player.h";


Player::Player(int startX, int startY)
{
	positionX = startX;
	positionY = startY;

	playerTexture.loadFromFile("Assets\\Tireur.png");

	playerSprite.setTexture(playerTexture);
	playerSprite.setPosition(positionX, positionY);
	playerSprite.setOrigin(playerTexture.getSize().x / 2, playerTexture.getSize().y / 2);
}

Player::Player()
{
	
	positionX = 1280/2;
	positionY = 960/2;
	playerAngle = 0.0f;

	playerTexture.loadFromFile("Assets\\Tireur.png");

	playerSprite.setTexture(playerTexture);

	playerSprite.setPosition(positionX, positionY);
	
	playerSprite.setOrigin(playerTexture.getSize().x / 2, playerTexture.getSize().y / 2);

}


int Player::GetPositionX()
{
	return positionX;

}

int Player::GetPositionY()
{
	return positionY;
}

float Player::GetAngle()
{
	return playerAngle;
}


float Player::GetHp()
{
	return hpRemaining;
}

void Player::SetPositionX(int newPositionX)
{
	positionX = newPositionX;
	playerSprite.setPosition(newPositionX, positionY);

}

void Player::SetPositionY(int newPositionY)
{
	positionY = newPositionY;
	playerSprite.setPosition(positionX, newPositionY);
}

void Player::SetAngle(float newAngle)
{
	playerAngle = newAngle;
	playerSprite.setRotation(playerAngle);
}

void Player::SetHp(float newHp)
{
	hpRemaining -= newHp;
}







Sprite Player::GetSprite()
{
	return playerSprite;
}

Texture Player::GetTexture()
{
	return playerTexture;
}

void Player::Update()
{

}