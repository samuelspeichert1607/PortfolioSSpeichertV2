#include "Pile.h"
#include <iostream>

/// <summary>
/// La pile est une structure de données qui permet de stocker des données.
/// Mais on peut accéder seulement à la dernière donnée stockée.
/// </summary>

Pile::Pile()
{
	premierNoeud = NULL;
}


Pile::~Pile()
{
	delete premierNoeud->GetSuivant();
	delete premierNoeud;

	for (int i = 0; i < nbElements; i++)
	{
		delete premierNoeud->GetSuivant();
		delete premierNoeud;
	}
}

void Pile::Push(Block * _block)
{
		Noeud * nouveauNoeud = new Noeud();
		nouveauNoeud->SetBlock(_block);
		nouveauNoeud->SetSuivant(premierNoeud);
		premierNoeud = nouveauNoeud;
		nbElements++;
}

Block * Pile::Pop()
{
	Block * block = NULL;
	if (premierNoeud != NULL)
	{
		block = premierNoeud->GetBlock();
		Noeud * noeudAEnlever = premierNoeud;
		premierNoeud = premierNoeud->GetSuivant();
		delete noeudAEnlever;
		nbElements--;
	}

	return block;
}



/// <summary>
/// La fonction afficher de la pile fait afficher à la console le contenu de la pile, tout en mettant en forme les coordonnées.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void Pile::Affiche()
{
	//TODO : Afficher toutes les données de la Pile

	Noeud * noeudCourant = premierNoeud;
	for (int i = 0; i < nbElements; i++)
	{
		//Afficher ici...
		cout << "(" << noeudCourant->GetBlock()->x << "," << noeudCourant->GetBlock()->y << "," << noeudCourant->GetBlock()->z << ")" << endl;
		noeudCourant = noeudCourant->GetSuivant();
	}
}

