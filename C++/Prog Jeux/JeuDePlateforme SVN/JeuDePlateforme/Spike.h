#pragma once
#include "Acteur.h"
#include "Ennemy.h"

#ifndef _DEBUG
	#define NDEBUG 
#endif
#include <cassert>

namespace platformer
{
	class Spike : public Ennemy
	{
	public:
		Spike(const float posX, const float posY, RenderWindow* const renderWindow);
		~Spike();

		void deplacement(bool direction);
		void update();
		void stop();

	private:

		static Texture texture;

		float velocityY = 0;

	};
}