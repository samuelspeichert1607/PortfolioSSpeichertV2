#pragma once

using namespace std;

/// <summary>
/// Un bloc représente un espace carré du labyrinthe. Il contient un pointeur sur presque tous les autres blocs qui l'entourent,
/// la valeur assignée, le nombre de points présents autour ainsi qu'un booléen qui dit s'il a déja été visité ou non.
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

