#pragma once
#include "Acteur.h"
#include "Ennemy.h"

namespace platformer
{
	class ParaGoomba : public Ennemy
	{
	public:
		ParaGoomba(const float posX, const float posY, RenderWindow* const renderWindow);
		~ParaGoomba();

		void deplacement(bool direction);

	private:

		static Texture texture;

		int leftRangeX = 0;
		int rightRangeX = 0;
		//Booléen qui détermine si le joueur peut bouger à gauche.
		bool canMoveLeft = true;


	};
}