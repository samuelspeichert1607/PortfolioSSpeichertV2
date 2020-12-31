#include "File.h"
/// <summary>
/// La file est une structure de donn�es qui permet de stocker des donn�es... Mais on peut acc�der � la donn�e � la sortie et � l'entr�e.
/// </summary>


File::File()
{
	Noeud* premierNoeud = NULL;
	Noeud* dernierNoeud = NULL;
}


File::~File()
{
	for (int i = 0; i < nbDeBlocks; i++)
	{
		premierNoeud = premierNoeud->GetSuivant();
		delete premierNoeud;
	}
}

void File::Ajouter(Block * _block)
{
	nbDeBlocks++;
	Noeud * nouveauNoeud = new Noeud();
	nouveauNoeud->SetBlock(_block);
	nouveauNoeud->SetSuivant(NULL);

	if (dernierNoeud == NULL)
	{
		premierNoeud = nouveauNoeud;
	}
	else
	{
		dernierNoeud->SetSuivant(nouveauNoeud);
	}
	dernierNoeud = nouveauNoeud;
	
}

Block * File::Retirer()
{
	Block * block = NULL;

	if (dernierNoeud = NULL)
	{
		block = premierNoeud->GetBlock(); //Le bs actuel va prendre son ch�que du premier du mois.
		Noeud * noeudAEnlever = premierNoeud; //Le bs sort de la file
		premierNoeud = premierNoeud->GetSuivant(); //C'est au tour du prochain bs de prendre son ch�que du premier du mois.
		delete noeudAEnlever; //Le bs sort de la file.
		if (premierNoeud == NULL) //La file est vide. Ya donc personne en premier...
		{
			dernierNoeud = NULL; //... ni en dernier
		}
	}
	//return  temporaire pour la compilation
	return block; //Le bs retourne chez lui avec sa bi�re et ses grateux du 6/49
}

/// <summary>
/// La fonction afficher de la file fait afficher � la console le contenu de la pile, tout en mettant en forme les coordonn�es.
/// </summary>
/// <returns>Aucune valeur de retour.</returns>
void File::Affiche()
{
	Noeud * noeudCourant = premierNoeud;
	for (int i = 0; i < nbDeBlocks; i++)
	{
		//Afficher ici...
		cout << "(" << noeudCourant->GetBlock()->x << "," << noeudCourant->GetBlock()->y << "," << noeudCourant->GetBlock()->z << ")" << endl;
		noeudCourant = noeudCourant->GetSuivant();
	}
}

