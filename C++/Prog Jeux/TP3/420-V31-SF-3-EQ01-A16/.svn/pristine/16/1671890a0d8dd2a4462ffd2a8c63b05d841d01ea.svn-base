#pragma once
#include <iostream>
#include <fstream>
#include <string>
using namespace std;
template <class T>
class Noeud
{
public:
	Noeud();
	~Noeud();
	Noeud<T>* getSuivant();
	Noeud<T>* getPrevious();
	void setSuivant(Noeud<T>* _suivant);
	void setPrevious(Noeud<T>* _previous);
	T* getContenu();
	void setContenu(T* _contenu);
private:
	T* contenu;
	Noeud<T>* prochainNoeud;
	Noeud<T>* noeudPrecedant;
};
#include "Noeud.hpp"