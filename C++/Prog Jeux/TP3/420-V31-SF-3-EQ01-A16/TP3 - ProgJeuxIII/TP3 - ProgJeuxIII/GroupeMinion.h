#pragma once

#include "Minion.h"

	class GroupeMinion : public IComponant
	{
	public:
		GroupeMinion();  //Tout ce qu'on a ici est aussi dans souls
		virtual ~GroupeMinion();
		virtual void bouger(Liste<Minion>* minionList);
		virtual void arreter();
		

		//Permettent de gerer la collection de componants
		void ajouter(Minion * minionToAdd);
		
		void vider();
		//void AjouterDestination(Vector2f * positionCible);
		virtual void AjouterDestination(const Vector2f& positionCible);

	private:
		Liste<Minion> groupeDeMinions;
		
	};
