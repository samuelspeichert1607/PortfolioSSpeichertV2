#pragma once

#include <cmath>
#include <SFML/Graphics.hpp>
#include "Projectile.h"

using namespace std;
using namespace sf;


class LasPlagas
{
public:
	LasPlagas(int startX, int startY, float startAngle);
	LasPlagas();
	~LasPlagas();

	int GetPositionX();
	int GetPositionY();
	void SetPositionX(int newPositionX);
	void SetPositionY(int newPositionY);
	void SetAngle(int positionJoueurX, int positionJoueurY);

	bool activated = false;
	Sprite GetSprite();
	Texture GetTexture();

	void Update();

	const float SPEED = 2.0f;

private:
	int positionX;
	int positionY;
	float lasPlagasAngle;

	Sprite lasPlagasSprite;
	Texture lasPlagasTexture;

};