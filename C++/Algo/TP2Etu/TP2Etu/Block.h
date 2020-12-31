#pragma once

using namespace std;

/// <summary>
/// Un bloc repr�sente un espace carr� du labyrinthe. Il contient un pointeur sur presque tous les autres blocs qui l'entourent,
/// la valeur assign�e, le nombre de points pr�sents autour ainsi qu'un bool�en qui dit s'il a d�ja �t� visit� ou non.
/// </summary>

struct Block
{
	int x;
	int y;
	int z;
	int points;
	char value;
	bool visited;
	Block * upBlock;
	Block * downBlock;
	Block * leftBlock;
	Block * rightBlock;
	Block * frontBlock;
	Block * behindBlock;
};

