#pragma once
#include "Acteur.h"
#include "Ennemy.h"


namespace platformer
{
	class CleverGoomba : public Ennemy
	{
	public:
		CleverGoomba(const float posX, const float posY, RenderWindow* const renderWindow);
		~CleverGoomba();

		void deplacement(bool direction);
		void update();
		void stop();

	private:

		static Texture texture;

		float velocityY = 0;

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