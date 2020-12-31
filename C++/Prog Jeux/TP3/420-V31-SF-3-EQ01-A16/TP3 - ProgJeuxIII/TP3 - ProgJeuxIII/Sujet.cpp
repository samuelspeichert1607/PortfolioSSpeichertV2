//Notez que tous les sujets peuvent r�utiliser cette classe int�gralement.  Ce sera toujours la forme du sujet
//Notez aussi que les pointeurs vont toujours pointer vers des objets qui existent d�j� ailleurs, donc pas de new et de delete ici.

#include "Sujet.h"
#include "Observateur.h"

//Sam
void Sujet::ajouterObservateur(Observateur* observateur)
{
	//Si l'observateur n'est pas d�j� dans la liste...
	if (!(std::find(observateurs.begin(), observateurs.end(), observateur) != observateurs.end()))
	{
		observateurs.push_back(observateur);
	}
}
//Sam
void Sujet::retirerObservateur(Observateur* observateur)
{
	//Pour retirer un observateur de la liste.  Deux pointeurs qui pointent � la m�me adresse sont �gaux.
	for (int i = 0; i < observateurs.size(); i++)
	{
		if (observateurs[i] == observateur)
		{
			observateurs.erase(observateurs.begin() + i);
			break;
		}
	}
}
//Sam et Alex
//Suite � une action on fait toujours �a.
vector<Observateur*> Sujet::notifierTousLesObservateurs()
{
	vector<Observateur*> observateursMort = vector<Observateur*>();
	for (int i = 0; i < observateurs.size(); i++)
	{
		if (observateurs.at(i) != nullptr)
		{
			if (observateurs.at(i)->notifier(this))
			{
				observateursMort.push_back(observateurs.at(i));
				observateurs.erase(observateurs.begin() + i);
				i--;
			}
		}
	}
	return observateursMort;
}