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

		const int NBR_NIVEAUX = 5;
		const int NBR_ANIMS_IMMOBILE = 11;
		const int NBR_ANIMS_MOUVEMENT = 11;
		const int NBR_ANIMS_MORT = 12;
		const int NBR_ANIMS_JUMP = 11;
		const int NBR_ANIMS_VICTOIRE = 11;
		const int NBR_ANIMS = 12;

		const float CLAVIER_DROITE = 1.0f;

		enum CADRAN{ IMMOBILE, MOUVEMENT, JUMP, VICTOIRE, MORT };


	};
}