#include "Lancier.h"
//Alex
Lancier::Lancier(bool isAlly, int ID)
{
	speed = 4;
	health = 185;
	maxHealth = 185;
	range = 10;
	attackPoints = 55;
	cout = 2;
	minionType = MinionType::_Lancier;
	this->isAlly = isAlly;
	this->ID = ID;
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

Lancier::~Lancier()
{

}
//Sam et Alex
void Lancier::PositionInitiale()
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
				AjouterDestination( Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 4));
			}
			else if (tours[i] == 1)
			{
				AjouterDestination( Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 2));
			}
			else if (tours[i] == 2)
			{
				AjouterDestination( Vector2f(LARGEUR_ECRAN - 64, HAUTEUR_ECRAN * 3 / 4));
			}
		}
	}
	else
	{
		isFacingLeft = true;
		AjouterDestination( Vector2f(64, HAUTEUR_ECRAN / 2));
	}
	AjouterDestination( Vector2f(LARGEUR_ECRAN / 2, (float)(rand() % (int)HAUTEUR_ECRAN)));
	ordreAAttaquer.Push(target);
	target = nullptr;
}
//Alex
void Lancier::javelot()
{
	range += 40;
	specialAttackTime.restart();
}
//Alex
void Lancier::annulerJavelot()
{
	range -= 40;
}