#include "Cube.h"
/// <summary>
/// Cette classe va contenir le labyrinthe re�u via un fichier texte dans un tableau 3D.
/// </summary>

/// <summary>
/// Non-seulement c'est le constructeur de cube, mais en plus c'est la fonction qui fait rentrer le labyrinthe d'un fichier texte dans 
/// un tableau 3D, pour pouvoir r�soudre des probl�mes gr�ce au ROB.
/// Note : J'ai �t� partiellement aid� par Alex Bouchard pour cette m�thode.
/// </summary>
Cube::Cube(string cubePath)
{
	ifstream streamInput;

	// On ouvre le r�pertoire.
	streamInput.open(cubePath); 
	
	//Si le fichier est absent.
	if (!streamInput)
	{
		string exceptionCaption = "Le fichier " + cubePath + " n'existe pas.";
		throw invalid_argument(exceptionCaption);
	}
	
	//La ligne qui est parcourue en ce moment.
	string currentLine;

	//Ce triple "for" va cr�er des blocs dans le tableau 3D.
	for (int i = 0; i < CUBE_SIZE; i++)
	{
		for (int j = 0; j < CUBE_SIZE; j++)
		{
			for (int k = 0; k < CUBE_SIZE; k++)
			{
				tabBlocks[i][j][k] = new Block();
			}
		}
	}	

	//It�rateurs en y et en z
	int j = -1;
	int k = -1;
	
	//Tant que nous sommes sur la ligne courrante
	while (getline(streamInput, currentLine))
	{
		j++; //On incr�mente la position en y

		    //On effectue la s�paration des "+" dans le fichier texte.
			if (currentLine[0] == '+')
			{
				k++; //On incr�mente la position en z
				j = -1;  //On assigne la position du y en dehors du fichier texte.
			}
			else
			{
				for (int i = 0; i < currentLine.size(); i++) //Tant que nous sommes encore dans la ligne courante.
				{
					tabBlocks[i][j][k]->x = i; //Position en x assign�e.
					tabBlocks[i][j][k]->y = j; //Position en y assign�e.
					tabBlocks[i][j][k]->z = k; //Position en z assign�e.

					//Si le caract�re actuel est un chiffre
					if (isdigit(currentLine[i])) 
					{
						tabBlocks[i][j][k]->points = currentLine[i] - '0'; //Ceci va transformer la valeur num�rique en nombre d�cimal, et l'assigner au bloc.
					}
					else
					{
						tabBlocks[i][j][k]->points = 0;
					}

					tabBlocks[i][j][k]->value = currentLine[i]; //Le caract�re actuel est assign� au bloc actuel.
					tabBlocks[i][j][k]->visited = false; //Si le bloc n'est pas visit�.

					tabBlocks[i][j][k]->upBlock = NULL; //Si le bloc du dessus est NULL.
					tabBlocks[i][j][k]->downBlock = NULL; //Si le bloc du dessous est NULL.
					if (tabBlocks[i][j][k]->value == 'U') //Si le bloc actuel m�ne � l'�tage du dessus.
					{
						tabBlocks[i][j][k]->upBlock = tabBlocks[i][j][k + 1];
					}
					else if (tabBlocks[i][j][k]->value == 'D') //Si le bloc actuel m�ne � l'�tage du dessous.
					{
						tabBlocks[i][j][k]->downBlock = tabBlocks[i][j][k - 1];
					}

					if (tabBlocks[i][j][k]->value == 'S') //Si le bloc actuel est le point de d�part.
					{
						startBlock = tabBlocks[i][j][k];
					}

					// Assignation des blocs de gauche.
					if (i != 0)
					{
						tabBlocks[i][j][k]->leftBlock = tabBlocks[i - 1][j][k];
					}
					else
					{
						tabBlocks[i][j][k]->leftBlock = NULL;
					}

					// Assignation des blocs de droite.
					if (i != CUBE_SIZE)
					{
						tabBlocks[i][j][k]->rightBlock = tabBlocks[i + 1][j][k];
					}
					else
					{
						tabBlocks[i][j][k]->rightBlock = NULL;
					}

					// Assignation des blocs de devant.
					if (j != 0)
					{
						tabBlocks[i][j][k]->frontBlock = tabBlocks[i][j - 1][k];
					}
					else
					{
						tabBlocks[i][j][k]->frontBlock = NULL;
					}

					// Assignation des blocs de derri�re.
					if (j != CUBE_SIZE)
					{
						tabBlocks[i][j][k]->behindBlock = tabBlocks[i][j + 1][k];
					}
					else
					{
						tabBlocks[i][j][k]->behindBlock = NULL;
					}
				}

			}
	}
	streamInput.clear(); //On ferme le fichier.
	streamInput.close();
}

/// <summary>
/// Destructeur de cube.
/// </summary>
Cube::~Cube()
{
	for (int i = 0; i < CUBE_SIZE; i++)
	{
		for (int j = 0; j < CUBE_SIZE; j++)
		{
			for (int k = 0; k < CUBE_SIZE; k++)
			{
				delete tabBlocks[i][j][k];
				tabBlocks[i][j][k] = nullptr;
			}
		}
	}

	delete[] tabBlocks;
}

/// <summary>
/// Cette fonction va retourner le bloc de d�part, pour �viter de violer l'encapsulation.
/// </summary>
/// <returns>Cette fonction va retourner le bloc de d�part.</returns>
Block * Cube::GetStartBlock()
{
	return startBlock;
}

/// <summary>
/// Cette fonction va r�initialiser les blocs, c'est-�-dire qu'ils ne seront plus consid�r�s comme
/// ayant �t� visit�s ult�rieurement.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void Cube::ResetAllVisitedBlocksToFalse()
{
	for (int i = 0; i < CUBE_SIZE; i++)
	{
		for (int j = 0; j < CUBE_SIZE; j++)
		{
			for (int k = 0; k < CUBE_SIZE; k++)
			{
				tabBlocks[i][j][k]->visited = false;
			}
		}
	}
}


