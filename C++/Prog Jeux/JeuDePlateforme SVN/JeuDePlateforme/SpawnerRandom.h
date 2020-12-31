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
		static enum typeEnnemy { goomba, spike, paraGoomba }; //Les diff�rents types d'ennemis pouvant �tre cr�es.

		static Ennemy * CreateEnnemy(typeEnnemy ennemy, const float posX, const float posY, RenderWindow* const renderWindow)
		{
			if (goomba == ennemy) //Si le ticket entr� en param�tre est un goomba
			{
				return new Goomba(posX, posY, renderWindow); //On en cr�e un sur la heap et on le retourne
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
