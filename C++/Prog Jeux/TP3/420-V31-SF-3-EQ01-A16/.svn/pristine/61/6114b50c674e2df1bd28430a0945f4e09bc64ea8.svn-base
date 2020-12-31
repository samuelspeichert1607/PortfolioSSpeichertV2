#include "Tour.h"
#include "SpawnerMinion.h"

//alex
Tour::Tour(bool isAlly, int ID)
{
	speed = 0;
	health = 24000;
	maxHealth = 24000;
	this->ID = ID;
	range = 0;
	attackPoints = 0;
	cout = 0;
	minionType = MinionType::_Tour;
	isDead = false;
	this->isAlly = isAlly;
	if (isAlly)
	{
		isFacingLeft = false;
		setColor(Color(0, 128, 255));
	}
	else
	{
		isFacingLeft = true;
		setColor(Color::Red);
	}
}
//Sam
Tour::~Tour()
{

}
//sam
Minion* Tour::spawn(MinionType type)
{
	static int positionCourrante = 0;
	if (!isDead)
	{
		if (isAlly)
		{
			if (positionCourrante == 0)
			{
				positionCourrante++;
				return SpawnerMinion::CreateEnnemy(type, getPosition().x + getOrigin().x + LARGEUR_MINON / 2, getPosition().y, isAlly);
			}
			else if (positionCourrante == 1)
			{
				positionCourrante++;
				return SpawnerMinion::CreateEnnemy(type, getPosition().x, getPosition().y + getOrigin().y + HAUTEUR_MINON / 2, isAlly);
			}
			else if (positionCourrante == 2)
			{
				positionCourrante = 0;
				return SpawnerMinion::CreateEnnemy(type, getPosition().x, getPosition().y - getOrigin().y - HAUTEUR_MINON / 2, isAlly);
			}
			
		}
		else
		{
			return SpawnerMinion::CreateEnnemy(type, getPosition().x - getOrigin().x - LARGEUR_MINON / 2, getPosition().y, isAlly);
		}
	}
		return NULL;
}