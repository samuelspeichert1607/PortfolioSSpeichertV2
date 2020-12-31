#include "ROB.h"

/// <summary>
/// La classe ROB va faire le parcours du labyrinthe en stockant les points, soit avec une pile ou avec une file
/// tout en accumulant les points et en retournant en arri�re s'il croise un cul-de-sac.
/// </summary>

//Constructeur du ROB
ROB::ROB()
{
	cube = new Cube("cube5.txt");
	
}
//Destructeur du ROB
ROB::~ROB()
{

}

/// <summary>
/// Cette fonction fait en sorte, gr�ce � une pile, que ROB va parcourrir le labyrinthe afin de trouver la sortie.
/// </summary>
/// <param name="nope">Aucun param�tre.</param>
/// <returns>Aucune valeur de retour.</returns>
void ROB::SolvePathToExit()
{
	static Block * currentBlock = cube->GetStartBlock(); //Le bloc actuellement parcouru.

	while (currentBlock->value != 'E') //Tant que le ROB n'a pas trouv� la sortie.
	{
		if (currentBlock->upBlock != NULL && currentBlock->upBlock->value != '*' && currentBlock->upBlock->visited == false) //Si le bloc au-dessus est libre et non-visit�.
		{
			currentBlock = currentBlock->upBlock; //On se d�place sur le bloc du dessus...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock); //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else if (currentBlock->downBlock != NULL && currentBlock->downBlock->value != '*' && currentBlock->downBlock->visited == false) //Si le bloc en-dessous est libre et non-visit�.
		{
			currentBlock = currentBlock->downBlock; //On se d�place sur le bloc du dessous...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock);  //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else if (currentBlock->leftBlock != NULL && currentBlock->leftBlock->value != '*' && currentBlock->leftBlock->visited == false) //Si le bloc de gauche est libre et non-visit�.
		{
			currentBlock = currentBlock->leftBlock; //On se d�place sur le bloc de gauche...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock);  //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else if (currentBlock->rightBlock != NULL && currentBlock->rightBlock->value != '*' && currentBlock->rightBlock->visited == false) //Si le bloc de droit est libre et non-visit�.
		{
			currentBlock = currentBlock->rightBlock; //On se d�place sur le bloc de droit...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock);  //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else if (currentBlock->frontBlock != NULL && currentBlock->frontBlock->value != '*' &&  currentBlock->frontBlock->visited == false) //Si le bloc d'en avant est libre et non-visit�.
		{
			currentBlock = currentBlock->frontBlock; //On se d�place sur le bloc d'en avant...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock);  //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else if (currentBlock->behindBlock != NULL && currentBlock->behindBlock->value != '*' && currentBlock->behindBlock->visited == false) //Si le bloc d'en arri�re est libre et non-visit�.
		{
			currentBlock = currentBlock->behindBlock; //On se d�place sur le bloc de derri�re...
			currentBlock->visited = true; //... qui est maintenant visit�.
			cheminDeSortie.Push(currentBlock);  //On met ce bloc dans la pile.
			SolvePathToExit(); //Et on r�apelle la fonction.
		}
		else
		{
			currentBlock = cheminDeSortie.Pop(); //On enl�ve les blocs pour retourner en arri�re.
		}

	}
}

/// <summary>
/// Cette fonction fait afficher le contenu de la pile cheminDeSortie.
/// </summary>
/// <param name="nope">Aucun param�tre.</param>
/// <returns>Aucune valeur de retour.</returns>
void ROB::GetSolutionPathToExit()
{
	cheminDeSortie.Affiche();
}

/// <summary>
/// Cette fonction r�cursive fait en sorte, gr�ce � une file, que ROB va parcourrir le labyrinthe 
/// afin de trouver la sortie, tout en ramassant les points pr�sents dans le labyrinthe.
/// </summary>
/// <param name="currentBlock">Le block qui vient d'�tre parcourru par ROB.</param>
/// <returns>Aucune valeur de retour.</returns>
void ROB::SolveAllPoints(Block * currentBlock)
{
	//Si on vient d'appeler cette fonction pour la premiere fois, alors on l'assigne au bloc de d�part.
	if (currentBlock == NULL)
	{
		currentBlock = cube->GetStartBlock();
	}

	currentBlock->visited = true;
	if (currentBlock->value != 'E')
	{
		//V�rifie si le bloc contient un chiffre.
		if (isdigit(currentBlock->value))
		{
			allPoints.Ajouter(currentBlock);
			totalPoints += currentBlock->points; //Additionne les chiffres en plus.
		}
		//V�rifie si la case du dessus est libre.
		if (currentBlock->upBlock != NULL && currentBlock->upBlock->value != '*' && currentBlock->upBlock->visited == false) //Si le bloc au-dessus est libre et non-visit�.
		{
			SolveAllPoints(currentBlock->upBlock); //Et on r�apelle la fonction.
		}
		//V�rifie si la case du dessous est libre.
		if (currentBlock->downBlock != NULL && currentBlock->downBlock->value != '*' && currentBlock->downBlock->visited == false) //Si le bloc en-dessous est libre et non-visit�.
		{
			SolveAllPoints(currentBlock->downBlock); //Et on r�apelle la fonction.
		}
		//V�rifie si la case de gauche est libre.
		if (currentBlock->leftBlock != NULL && currentBlock->leftBlock->value != '*' && currentBlock->leftBlock->visited == false)
		{
			SolveAllPoints(currentBlock->leftBlock); //Et on r�apelle la fonction.
		}
		//V�rifie si la case de droite est libre.
		if (currentBlock->rightBlock != NULL && currentBlock->rightBlock->value != '*' && currentBlock->rightBlock->visited == false)
		{	
			SolveAllPoints(currentBlock->rightBlock); //Et on r�apelle la fonction.
		}
		//V�rifie si la case d'en avant est libre.
		if (currentBlock->frontBlock != NULL && currentBlock->frontBlock->value != '*' &&  currentBlock->frontBlock->visited == false)
		{
			SolveAllPoints(currentBlock->frontBlock); //Et on r�apelle la fonction.
		}
		//V�rifie si la case d'en arri�re est libre.
		if (currentBlock->behindBlock != NULL && currentBlock->behindBlock->value != '*' && currentBlock->behindBlock->visited == false)
		{
			SolveAllPoints(currentBlock->behindBlock); //Et on r�apelle la fonction.
		}
	}
	
	
}	
	
/// <summary>
/// Cette fonction fait afficher le contenu de la file allPoints.
/// </summary>
/// <param name="nope">Aucun param�tre.</param>
/// <returns>Aucune valeur de retour.</returns>
void ROB::GetSolutionAllPoints()
{
	allPoints.Affiche();
	cout << "Le total des points est de :" << totalPoints << endl;
}