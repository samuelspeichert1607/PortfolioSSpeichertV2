#include <iostream>
#include <string>

//Constructeur de pile
template <class T>
Pile<T>::Pile()
{
	premierNoeud = NULL;
}

//Destructeur de pile
template <class T>
Pile<T>::~Pile()
{
	while (premierNoeud != nullptr)
	{
		Pop();
	}
}

/// <summary>
/// La fonction Push de la pile va rentrer un élément dans la pile. 
/// </summary>
/// <param name="_contenu">Un pointeur vers le contenu que l'on désire insérer.</param>
/// <returns>Aucun retour.</returns>
template <class T>
void Pile<T>::Push(T* _contenu)
{
	Noeud<T>* nouveau = new Noeud<T>();
	nouveau->setContenu(_contenu);
	nouveau->setSuivant(premierNoeud);
	premierNoeud = nouveau;
}

/// <summary>
/// La fonction Pop de la pile va retirer l'élément au-dessus de la pile. 
/// </summary>
/// <returns>Cette fonction va retourner un pointeur vers le contenu retiré.</returns>
template <class T>
T* Pile<T>::Pop()
{
	T* temporaire = nullptr;
	if (premierNoeud != nullptr)
	{
		temporaire = premierNoeud->getContenu();
		Noeud<T>* noeudTemporaire = premierNoeud;
		premierNoeud = premierNoeud->getSuivant();
		delete noeudTemporaire;
	}
	return temporaire;
}