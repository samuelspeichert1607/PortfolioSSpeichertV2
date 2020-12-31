#include "Noeud.h"

Noeud::Noeud()
{
}


Noeud::~Noeud()
{
}

Noeud * Noeud::GetSuivant()
{
	return suivant;
}

void Noeud::SetSuivant(Noeud * _suivant)
{
	suivant = _suivant;
}

Block * Noeud::GetBlock()
{
	return block;
}

void Noeud::SetBlock(Block * _block)
{
	block = _block;
}


