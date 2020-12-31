#pragma once
#include "Cube.h"
#include "Block.h"
#include "Pile.h"
#include "File.h"

using namespace std;

class ROB
{
public:
	ROB();
	~ROB();
	void SolvePathToExit();
	void GetSolutionPathToExit();

	void SolveAllPoints(Block * currentBlock);
	void GetSolutionAllPoints();

private:
	Pile cheminDeSortie; //La pile pour stocker les blocs parcourus par ROB.
	File allPoints; //La file pour stocker les blocs parcourus par ROB.
	Cube * cube; //Le labyrinthe parcouru par ROB.

	int totalPoints = 0;
};

