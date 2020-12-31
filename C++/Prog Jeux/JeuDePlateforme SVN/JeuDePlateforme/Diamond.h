#pragma once
#include "Collectible.h"

namespace platformer
{
	class Diamond : public Collectible
	{
	public:
		Diamond(const float posX, const float posY, RenderWindow* const renderWindow);
		~Diamond();

	};
}