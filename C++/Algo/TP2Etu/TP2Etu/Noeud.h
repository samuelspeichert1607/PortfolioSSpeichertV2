#pragma once
#include "Block.h"

using namespace std;

class Noeud
{
public:
	Noeud();
	~Noeud();
	Noeud * GetSuivant();
	void SetSuivant(Noeud * _suivant);
	Block * GetBlock();
	void SetBlock(Block * _block);
	
private:
	Block * block; //Block stocké
	Noeud * suivant;

};

