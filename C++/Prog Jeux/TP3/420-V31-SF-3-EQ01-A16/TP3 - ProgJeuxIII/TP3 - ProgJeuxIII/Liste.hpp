template <class T>
Liste<T>::Liste()
{
	premierNoeud = NULL;
}
template <class T>
Liste<T>::~Liste()
{
	while (premierNoeud != NULL)
	{
		retirer(premierNoeud->getContenu());
	}
}
/// <summary>
/// La fonction Ajouter va faire l'ajout d'un contenu dans la liste, tout en s'assurant
/// que l'ordre de placement du contenu soit respect� et qu'il n'y ait aucun doublon.
/// </summary>
/// <param name="_contenu">Un pointeur vers le contenu que l'on d�sire ajouter.</param>
/// <returns>La fonction retourne un bool�en indiquant si l'ajout d'un �l�ment est courron� de succ�s ou non.</returns>
//Sam
template <class T>
bool Liste<T>::ajouter(T* _contenu)
{
	Noeud<T>* nouveau = new Noeud<T>();
	nouveau->setContenu(_contenu);
	nouveau->setSuivant(NULL);
	nouveau->setPrevious(NULL);
	if (premierNoeud == NULL)
	{
		premierNoeud = nouveau;
		premierNoeud->setSuivant(premierNoeud);
		premierNoeud->setPrevious(premierNoeud);
		nbElements++;
		return true;
	}
	//si va en premier
	else if (*_contenu < *(premierNoeud->getContenu()))
	{
		nouveau->setSuivant(premierNoeud);
		nouveau->setPrevious(premierNoeud->getPrevious());
		premierNoeud->getPrevious()->setSuivant(nouveau);
		premierNoeud->setPrevious(nouveau);
		premierNoeud = nouveau;
		nbElements++;
		return true;
	}
	else if (*(premierNoeud->getContenu()) == *_contenu)
	{
		return false;
	}
	//si il va en 2�me et il y a juste un seul noeud
	else if (*(premierNoeud->getSuivant()->getContenu()) == *(premierNoeud->getContenu()))
	{
		premierNoeud->setSuivant(nouveau);
		nouveau->setPrevious(premierNoeud);
		premierNoeud->setPrevious(nouveau);
		nouveau->setSuivant(premierNoeud);
		nbElements++;
		return true;
	}
	//si il va � la fin
	else if (*(premierNoeud->getPrevious()->getContenu()) < *_contenu)
	{
			nouveau->setSuivant(premierNoeud);
			premierNoeud->getPrevious()->setSuivant(nouveau);
			nouveau->setPrevious(premierNoeud->getPrevious());
			premierNoeud->setPrevious(nouveau);
			nbElements++;
			return true;
	}
	//si elle va plus loin que le premier noeud et qu'il y en a au moins 2
	else
	{
		Noeud<T>* noeudCourantArriere = premierNoeud;
		Noeud<T>* noeudCourantAvant = premierNoeud;
		do
		{
			//si on est au dernier noeud
			if (*(noeudCourantArriere->getContenu()) > *(_contenu) && *(noeudCourantArriere->getPrevious()->getContenu()) < *(_contenu))
			{
				nouveau->setSuivant(noeudCourantArriere);
				noeudCourantArriere->getPrevious()->setSuivant(nouveau);
				nouveau->setPrevious(noeudCourantArriere->getPrevious());
				noeudCourantArriere->setPrevious(nouveau);
				nbElements++;
				return true;
			}
			else if (*(noeudCourantAvant->getContenu()) < *(_contenu) && *(noeudCourantAvant->getSuivant()->getContenu()) > *(_contenu))
			{
				nouveau->setPrevious(noeudCourantAvant);
				noeudCourantAvant->getSuivant()->setPrevious(nouveau);
				nouveau->setSuivant(noeudCourantAvant->getSuivant());
				noeudCourantAvant->setSuivant(nouveau);
				nbElements++;
				return true;
			}
			noeudCourantArriere = noeudCourantArriere->getPrevious();
			noeudCourantAvant = noeudCourantAvant->getSuivant();
		} while (*(noeudCourantArriere->getContenu()) != *(noeudCourantAvant->getContenu())); 
		return false;
	}
}
/// <summary>
/// Retire le contenu sp�cifi�.
/// </summary>
/// <param name="_contenu">le contenu � retirer.</param>
/// <returns>vrai si le contenu a �t� retir�, faux si le contenu envoy� n'�tais pas l� ou que la liste �tais vide</returns>
//Alex
template <class T>
bool Liste<T>::retirer(T* _contenu)
{
	if (premierNoeud == NULL)
	{
		return false;
	}
	else if (*(premierNoeud->getContenu()) == *_contenu)
	{
		if (*(premierNoeud->getSuivant()->getContenu()) == *(premierNoeud->getContenu()))
		{
			delete premierNoeud;
			premierNoeud = NULL;
			nbElements--;
			return true;
		}
		Noeud<T>* temporaire = premierNoeud;
		premierNoeud = temporaire->getSuivant();
		premierNoeud->setPrevious(temporaire->getPrevious());
		temporaire->getPrevious()->setSuivant(premierNoeud);
		delete temporaire;
		nbElements--;
		return true;
	}
	Noeud<T>* courantAvant = premierNoeud;
	Noeud<T>* courantDerriere = premierNoeud;
	do
	{
		if (*(courantAvant->getSuivant()->getContenu()) == *_contenu)
		{
			Noeud<T>* temporaire = courantAvant->getSuivant();
			courantAvant->setSuivant(temporaire->getSuivant());
			temporaire->getSuivant()->setPrevious(courantAvant);
			delete temporaire;
			nbElements--;
			return true;
		}
		else if (*(courantDerriere->getPrevious()->getContenu()) == *_contenu)
		{
			Noeud<T>* temporaire = courantDerriere->getPrevious();
			courantDerriere->setPrevious(temporaire->getPrevious());
			temporaire->getPrevious()->setSuivant(courantDerriere);
			delete temporaire;
			nbElements--;
			return true;
		}
		courantAvant = courantAvant->getSuivant();
		courantDerriere = courantDerriere->getPrevious();
	} while (*(courantAvant->getContenu()) != *(courantDerriere->getContenu()));
	return false;
}
/// <summary>
/// Donne le contenu � la position specifi�.
/// </summary>
/// <param name="id">Position du contenu � retourner.</param>
/// <returns></returns>
//Alex
template <class T>
T* Liste<T>::at(int id)
{
	Noeud<T>* courant = premierNoeud;
	for (int i = 0; i < id; i++)
	{
		courant = courant->getSuivant();
		if (*(courant->getContenu()) == *(premierNoeud->getContenu()))
		{
			return NULL;
		}
	}
	return courant->getContenu();
}

//Sam
template <class T>
int Liste<T>::getNbElements()
{
	return nbElements;
}