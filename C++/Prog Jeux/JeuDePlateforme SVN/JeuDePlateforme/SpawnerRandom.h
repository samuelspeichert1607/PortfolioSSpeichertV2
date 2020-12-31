#pragma once
#include "Ennemy.h"
#include "Goomba.h"
#include "Spike.h"
#include "ParaGoomba.h"

namespace platformer
{
	class SpawnerRandom
	{
	public:
		static enum typeEnnemy { goomba, spike, paraGoomba }; //Les différents types d'ennemis pouvant être crées.

		static Ennemy * CreateEnnemy(typeEnnemy ennemy, const float posX, const float posY, RenderWindow* const renderWindow)
		{
			if (goomba == ennemy) //Si le ticket entré en paramètre est un goomba
			{
				return new Goomba(posX, posY, renderWindow); //On en crée un sur la heap et on le retourne
			}
			else if (spike == ennemy)
			{
				return new Spike(posX, posY, renderWindow);
			}
			else if (paraGoomba == ennemy)
			{
				return new ParaGoomba(posX, posY, renderWindow);
			}
			return nullptr;
		}
	private:
		SpawnerRandom();
	};
}
