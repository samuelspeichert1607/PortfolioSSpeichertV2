#pragma once
//#include "stdafx.h"
#include "Noeud.h"

using namespace std;

template<class T>
class File
{
public:
	File();
	~File();
	void Ajouter(T* _contenu);
	T* Retirer();

private:
	Noeud<T>* premierNoeud;
	Noeud<T>* dernierNoeud;
};
#include "File.hpp"