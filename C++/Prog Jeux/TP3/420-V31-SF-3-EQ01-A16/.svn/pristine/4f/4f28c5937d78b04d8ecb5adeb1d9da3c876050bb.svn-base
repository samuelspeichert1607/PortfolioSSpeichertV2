#pragma once

#include <vector>
class Observateur;

using std::vector;

class Sujet
{

protected:
	vector<Observateur*> observateurs;
public:
	//~Sujet();
	void ajouterObservateur(Observateur* observateur);
	void retirerObservateur(Observateur* observateur);
	/// <summary>
	/// Virtuel seulement pour que Sujet fonctionne avec typeid
	/// </summary>
	virtual vector<Observateur*> notifierTousLesObservateurs();


};