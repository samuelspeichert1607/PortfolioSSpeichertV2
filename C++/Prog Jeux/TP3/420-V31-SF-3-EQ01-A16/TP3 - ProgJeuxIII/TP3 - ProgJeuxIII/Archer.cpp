#include "Archer.h"

Archer::Archer(bool isAlly, int id)
{
	speed = 3;
	health = 125;
	maxHealth = 125;
	range = 50;
	attackPoints = 35;
	cout = 3;
	minionType = MinionType::_Archer;
	this->isAlly = isAlly;
	this->ID = id;
	isDead = false;
	PositionInitiale();
}

Archer::~Archer()
{

}

void Archer::PositionInitiale()
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

void Archer::retraiteStrategique()
{
	//positionRetraite = Vector2f(getPosition().x - LARGEUR_ECRAN / 3, rand() % (int)HAUTEUR_ECRAN);
	AjouterDestination(Vector2f(getPosition().x - LARGEUR_ECRAN / 3, rand() % (int)HAUTEUR_ECRAN));
	speed = 5;
	specialAttackTime.restart();
}

void Archer::annulerRetraiteStrategique()
{
	speed = 3;
	Vector2f* temporaire = ordreAAttaquer.Pop();
	if (temporaire != positionRetraite)
	{
		ordreAAttaquer.Push(temporaire);
	}
}
