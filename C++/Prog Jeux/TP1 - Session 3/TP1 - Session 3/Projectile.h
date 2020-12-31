#pragma once
#include <SFML/Graphics.hpp>

using namespace std;
using namespace sf;


class Projectile
{
public:
	Projectile(int startX, int startY, float startAngle);
	Projectile();

	int GetPositionX();
	int GetPositionY();
	void SetPositionX(int newPositionX);
	void SetPositionY(int newPositionY);
	void SetAngle(float newAngle);

	bool activated = false;
	Sprite GetSprite();
	Texture GetTexture();

	void Update();

	const float SPEED = 24.0f; //Vitesse du projectile

private:
	


	int positionX; //Position en X
	int positionY; //Position en Y
	float projectileAngle; //Angle du projectile

	Sprite projectileSprite; //Sprite du projectile
	Texture projectileTexture; //Texture du projectile

};