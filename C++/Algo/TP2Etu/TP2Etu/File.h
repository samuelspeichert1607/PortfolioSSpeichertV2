#pragma once
#include "Noeud.h"
#include "Block.h"
#include <iostream>

class File
{
public:
	File();
	~File();
	void Ajouter(Block * _block);
	Block * Retirer();
	void Affiche();

private:
	Noeud* premierNoeud; //Premier noeud dans la file.
	Noeud* dernierNoeud; //Dernier noeud dans la pile.
	int nbDeBlocks = 0; //Blocs stockés dans la pile.
};

