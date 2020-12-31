#include "Cavalier.h"
//Alex
Cavalier::Cavalier(bool isAlly, int id)
{
	speed = 5;
	health = 750;
	maxHealth = 750;
	range = 10;
	attackPoints = 150;
	cout = 10;
	minionType = MinionType::_Cavalier;
	this->isAlly = isAlly;
	this->ID = id;
	if (isAlly)
	{
		isFacingLeft = false;
	}
	else
	{
		isFacingLeft = true;
	}
	isDead = false;
	PositionInitiale();
}

Cavalier::~Cavalier()
{
}
//Sam et Alex
void Cavalier::PositionInitiale()
{
	if (isAlly)
	{
		isFacingLeft = false;
		int noTourSuivante = rand() % 3;
		int tours[3];
		tours[0] = noTourSuivante;
		while (noTourSuivante == tours[0])
		{
			noTourSuivante = rand() % 3;
		}
		tours[1] = noTourSuivante;
		while (noTourSuivante == tours[0] || noTourSuivante == tours[1])
		{
			noTourSuivante = rand() % 3;
		}
		tours[2] = noTourSuivante;
		for (int i = 0; i < 3; i++)
		{
			if (tours[i] == 0)
			{
				AjouterDestination(  Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 4));
			}
			else if (tours[i] == 1)
			{
				AjouterDestination(  Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 2));
			}
			else if (tours[i] == 2)
			{
				AjouterDestination(  Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN * 3 / 4));
			}
		}
	}
	else
	{
		isFacingLeft = true;
		AjouterDestination(  Vector2f(64, HAUTEUR_ECRAN / 2));
	}
	AjouterDestination(  Vector2f(LARGEUR_ECRAN / 2, (float)(rand() % (int)HAUTEUR_ECRAN)));
	ordreAAttaquer.Push(target);
	target = nullptr;
}
//Alex
void Cavalier::charge()
{
	speed += 3;
	attackPoints *= 2;
	specialAttackTime.restart();
}
//Alex
void Cavalier::annulerCharge()
{
	speed -= 3;
	attackPoints /= 2;
}