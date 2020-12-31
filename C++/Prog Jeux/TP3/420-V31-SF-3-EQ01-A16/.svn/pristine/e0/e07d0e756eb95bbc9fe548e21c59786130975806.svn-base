#include "Noeud.h"
template <class T>
class Liste
{
public:
	Liste();
	~Liste();
	bool ajouter(T* _contenu);
	bool retirer(T* _contenu);
	T* at(int id);
	int getNbElements();
private:
	Noeud<T>* premierNoeud = NULL;
	int nbElements = 0;
};
#include "Liste.hpp"