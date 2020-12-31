#include "GuerrierLeger.h"

GuerrierLeger::GuerrierLeger(bool isAlly, int id)
{
	speed = 3;
	health = 360;
	maxHealth = 360;
	range = 5;
	attackPoints = 50;
	cout = 3;
	minionType = MinionType::_GuerrierLeger;
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

GuerrierLeger::~GuerrierLeger()
{

}

void GuerrierLeger::PositionInitiale()
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

void GuerrierLeger::millesLames()
{
	attackPoints *= 2;
	specialAttackTime.restart();
}

void GuerrierLeger::annulerMillesLames()
{
	attackPoints /= 2;
}
