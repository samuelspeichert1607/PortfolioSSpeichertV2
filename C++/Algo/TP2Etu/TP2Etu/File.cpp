#include "File.h"
/// <summary>
/// La file est une structure de données qui permet de stocker des données... Mais on peut accéder à la donnée à la sortie et à l'entrée.
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
		block = premierNoeud->GetBlock(); //Le bs actuel va prendre son chèque du premier du mois.
		Noeud * noeudAEnlever = premierNoeud; //Le bs sort de la file
		premierNoeud = premierNoeud->GetSuivant(); //C'est au tour du prochain bs de prendre son chèque du premier du mois.
		delete noeudAEnlever; //Le bs sort de la file.
		if (premierNoeud == NULL) //La file est vide. Ya donc personne en premier...
		{
			dernierNoeud = NULL; //... ni en dernier
		}
	}
	//return  temporaire pour la compilation
	return block; //Le bs retourne chez lui avec sa bière et ses grateux du 6/49
}

/// <summary>
/// La fonction afficher de la file fait afficher à la console le contenu de la pile, tout en mettant en forme les coordonnées.
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

