#pragma once
#include "Magicien.h"
#include "Lancier.h"
#include "Cavalier.h"
#include "GuerrierLeger.h"
#include "GuerrierLourd.h"
#include "Archer.h"
#include "Barbare.h"
#include "Tour.h"


class SpawnerMinion
{
public:
	static int nextIdMinion;
	//Alex
	static Minion * CreateEnnemy(MinionType minion, const float posX, const float posY, bool isAlly)
	{
		nextIdMinion++;
		if (MinionType::_Magichien == minion) //Si le ticket entré en paramètre est un goomba
		{
			Magicien* nouveau = new Magicien(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_Lancier == minion)
		{
			Lancier* nouveau = new Lancier(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_Cavalier == minion)
		{
			Cavalier* nouveau = new Cavalier(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_GuerrierLeger == minion)
		{
			GuerrierLeger* nouveau = new GuerrierLeger(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_GuerrierLourd == minion)
		{
			GuerrierLourd* nouveau = new GuerrierLourd(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_Archer == minion)
		{
			Archer* nouveau = new Archer(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_Barbare == minion)
		{
			Barbare* nouveau = new Barbare(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			return nouveau;//on le retourne
		}
		else if (MinionType::_Tour == minion)
		{
			Tour* nouveau = new Tour(isAlly, nextIdMinion); //On en crée un sur la heap
			nouveau->setPosition(posX, posY); // on met la position
			nouveau->setOrigin(105 / 2, 138 / 2);//on met l'origine du minon
			return nouveau;//on le retourne
		}
		return NULL;
	}
private:
	SpawnerMinion();
};

