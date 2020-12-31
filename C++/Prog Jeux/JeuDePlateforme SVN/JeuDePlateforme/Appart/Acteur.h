#pragma once
#include <SFML/Graphics.hpp>
#include <string>

using namespace sf;

using std::string;

namespace platformer
{
	class Acteur : public Sprite
	{
		public:
			Acteur(const float posX, const float posY, RenderWindow* const renderWindow);
			~Acteur();

			bool chargerTextures(const char texturePath[]);
			void ajustementsVisuels();
			
			virtual void deplacement(bool direction) =0;
			inline bool getState();
			void setState(bool dead);
			Texture texture; // static Texture texture;
			const int startPositionX;
			const int startPositionY;

		protected:

			const enum DIRECTIONS { IMMOBILE = -1, GAUCHE, DROITE, SAUT };
<<<<<<< .mine
			const float VITESSE = 9.0f;
||||||| .r11
			const float VITESSE = 6.0f;
=======
			DIRECTIONS etat;
			const float VITESSE = 6.0f;
>>>>>>> .r12
			const int SPEED_ANIMATION = 10;

			const int NBR_NIVEAUX = 5;
			const int NBR_ANIMS_IMMOBILE = 11;
			const int NBR_ANIMS_MOUVEMENT = 11;
			const int NBR_ANIMS_MORT = 12;
			const int NBR_ANIMS_JUMP = 11;
			const int NBR_ANIMS_VICTOIRE = 11;
			const int NBR_ANIMS = 12;


			RenderWindow* renderWindow;

			IntRect** intRects;

			int animateur;
			int cadran;

			Vector2f interfaceDeplacement;

			int animateurImmobile;
			int directionImmobile;
			bool estMobile;
			bool isDead;
	};
}

