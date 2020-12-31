template <class T>
File<T>::File()
{
	//TODO : Compléter le code de construction
	premierNoeud = NULL;
	dernierNoeud = NULL;
}

template <class T>
File<T>::~File()
{
	//TODO : Compléter le code de destruction
	while (premierNoeud != nullptr)
	{
		Retirer();
	}
	delete premierNoeud;
	delete dernierNoeud;
}

template <class T>
void File<T>::Ajouter(T* _contenu)
{
	//TODO : Compléter le code d'ajout
	Noeud<T> * nouveauNoeud = new Noeud<T>();
	nouveauNoeud->setContenu(_contenu);
	nouveauNoeud->setSuivant(NULL);

	if (dernierNoeud == NULL)
	{
		premierNoeud = nouveauNoeud;
	}
	else
	{
		dernierNoeud->setSuivant(nouveauNoeud);
	}
	dernierNoeud = nouveauNoeud;
}

template <class T>
T* File<T>::Retirer()
{
	T * contenu = NULL;
	//TODO : Compléter le code de retrait
	if (premierNoeud != NULL)
	{
		contenu = premierNoeud->getContenu(); //Le bs actuel va prendre son chèque du premier du mois.
		Noeud<T> * noeudAEnlever = premierNoeud; //Le bs sort de la file
		premierNoeud = premierNoeud->getSuivant(); //C'est au tour du prochain bs de prendre son chèque du premier du mois.
		delete noeudAEnlever; //Le bs sort de la file.
		if (premierNoeud == NULL) //La file est vide. Ya donc personne en premier...
		{
			dernierNoeud = NULL; //... ni en dernier
		}
	}
	return contenu; //Le bs retourne chez lui avec sa bière et ses grateux du 6/49
}