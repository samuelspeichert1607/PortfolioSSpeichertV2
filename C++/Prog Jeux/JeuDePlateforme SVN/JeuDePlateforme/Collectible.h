#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;
namespace platformer
{
	class Collectible :public Sprite
	{
	public:
		Collectible(const float posX, const float posY, RenderWindow* const renderWindow);
		~Collectible();
		bool chargerTextures(const char texturePath[]);
		int getValue();
		void update();
	private:
		Texture texture;
		const int value = 100;
		const float SPEED = 1.2;
		int startPositionX;
		int startPositionY;
		enum DIRECTION{ UP, DOWN };
		DIRECTION direction;
	};
}
