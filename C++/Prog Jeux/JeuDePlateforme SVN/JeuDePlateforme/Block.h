#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;

class Block : public Sprite
{
public:
	Block();
	~Block();
	void Init(const float posX, const float posY, RenderWindow* const renderWindow);
	static bool chargerTextures(const char texturePath[]);
	void ajustementsVisuels();
	const int LARGEUR_BLOCK = 32;

private:

	static Texture blockT;
	Sprite* spriteBlock;
	
	RenderWindow* renderWindow;

};