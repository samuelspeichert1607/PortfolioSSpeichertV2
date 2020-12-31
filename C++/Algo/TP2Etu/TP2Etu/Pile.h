#pragma once
#include "Noeud.h"
#include "Block.h"

using namespace std;

class Pile
{
public:
	Pile();
	~Pile();
	void Push(Block * _block);
	Block * Pop();
	void Affiche();
	
private:
	Noeud* premierNoeud; //Le premier noeud parcouru dans la pile.
	int nbElements = 0; //Ajouté à cause des test unitaires.
};

