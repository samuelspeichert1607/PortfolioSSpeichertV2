#pragma once
#include "Ennemy.h"
#include "Goomba.h"
#include "Collectible.h"

//En théorie, on peut mettre un nombre illimité de classes dans des fichiers .h et leur .cpp associés
//Mais ne faites ça que si les classes sont vraiment très simples et dans une hiérarchie d'héritage
namespace platformer
{
	class SpawnerFixe
	{
	public:
		virtual Ennemy * FabriquerEnnemy(const float posX, const float posY, RenderWindow* const renderWindow) = 0;
	};

	class SpawnerDeGoomba : public SpawnerFixe
	{
	public:
		Ennemy * FabriquerEnnemy(const float posX, const float posY, RenderWindow* const renderWindow)
		{
			return new Goomba(posX, posY, renderWindow);
		}
	};
}
