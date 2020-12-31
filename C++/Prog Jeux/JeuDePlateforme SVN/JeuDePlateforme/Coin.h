#pragma once
#include "Collectible.h"

namespace platformer
{
	class Coin : public Collectible
	{
	public:
		Coin(const float posX, const float posY, RenderWindow* const renderWindow);
		~Coin();

	};
}