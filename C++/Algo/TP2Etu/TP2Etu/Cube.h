#pragma once

#include <iostream>
#include <fstream>
#include <string>
#include "Block.h"
#include "constants.h"

using namespace std;

class Cube
{
public:
	Cube(string cubePath);
    ~Cube();	
	Block * GetStartBlock();
	void ResetAllVisitedBlocksToFalse();


private:
	Block * startBlock; //Bloc de départ.
	Block * tabBlocks[CUBE_SIZE][CUBE_SIZE][CUBE_SIZE]; //Tableau 3D qui contient tous les blocs du labyrinthe.
};

