#pragma once
#include <SFML/Graphics.hpp>

using namespace std;
using namespace sf;


class Player
{
public:
	Player(int startX, int startY);
	Player();

	int GetPositionX();
	int GetPositionY();
	float GetAngle();
	float GetHp();
	void SetPositionX(int newPositionX);
	void SetPositionY(int newPositionY);
	void SetAngle(float newAngle);
	void SetHp(float newHp);

	Sprite GetSprite();
	Texture GetTexture();

	void Update();


private:
	int positionX; //Position en X du joueur
	int positionY; //Position en Y du joueur
	float playerAngle; //L'angle du joueur
	float hpRemaining = 100.0f; //Hp restant du joueur.

	
	Sprite playerSprite;
	Texture playerTexture;

};