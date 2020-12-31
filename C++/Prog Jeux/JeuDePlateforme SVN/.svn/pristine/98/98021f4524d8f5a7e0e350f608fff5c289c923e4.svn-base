#define _USE_MATH_DEFINES

#include "LevelManager.h"
#include <fstream>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <algorithm>

using namespace platformer;

LevelManager::LevelManager(string fileName)
{
	sFilename = fileName;
	loadLevel();
}


LevelManager::~LevelManager()
{
	
}



void LevelManager::loadLevel()
{
	ifstream streamInput;

	//tentative d'ouverture du fichier
	streamInput.open(sFilename);

	//vérification du résultat
	if (!streamInput)
	{
		string exceptionCaption = "Le fichier " + sFilename + " n'existe pas.";
		throw invalid_argument(exceptionCaption);
	}

	level = new string[HAUTEUR_NIVEAU];

	string raw;
	getline(streamInput, raw);

	if (streamInput.is_open())
	{
		level[0] = raw;
		for (int i = 1; i < HAUTEUR_NIVEAU; i++)
		{
			streamInput >> level[i];
			level[i].erase(remove(level[i].begin(), level[i].end(), ','), level[i].end());
		}
	}

	streamInput.clear();
	streamInput.close();

}


string * LevelManager::getLevel()
{
	return level;
}

