#pragma once
#include <SFML/Graphics.hpp>
#include "Liste.h"

using sf::Vector2f;
using sf::RenderWindow;


class Minion;

class IComponant
{
	public:  //Tout ce qu'on a ici est aussi dans souls
		//virtual ~IComponant() = 0;
		virtual void bouger(Liste<Minion>* minionList) = 0;
		virtual void arreter() = 0;
		//virtual void AjouterDestination(Vector2f* destination) = 0;
		//void setPositionCible(const Vector2f& positionCible);
		
};