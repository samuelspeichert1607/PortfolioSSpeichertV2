#pragma once
#include "Acteur.h"

using namespace platformer;

class Ennemy : public Acteur
{
public:
	Ennemy(const float posX, const float posY, RenderWindow* const renderWindow);
	virtual ~Ennemy();

	virtual void deplacement(bool direction) = 0;
	virtual void update();
	virtual void stop();

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

protected :
	const float ENNEMY_SPEED = 4.0f;

};
