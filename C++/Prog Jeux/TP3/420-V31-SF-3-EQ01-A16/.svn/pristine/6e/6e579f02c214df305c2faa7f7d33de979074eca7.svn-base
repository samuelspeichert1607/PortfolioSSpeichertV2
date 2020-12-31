#include "GroupeMinion.h"

/// <summary>
/// Constructeur de minion.
/// </summary>
//Sam
GroupeMinion::GroupeMinion()
{
}

/// <summary>
/// Destructeur de minion.
/// </summary>
//Sam
GroupeMinion::~GroupeMinion()
{
	vider();
}

//Ajouter cible
//Sam
void GroupeMinion::ajouter(Minion * minionToAdd)
{
	groupeDeMinions.ajouter(minionToAdd);
}


/// <summary>
/// Permet de faire réagir les minions à une radiation selon leur distance
/// </summary>
/// <returns></returns>
//Sam
void GroupeMinion::vider()
{
	for (int i = 0; i < groupeDeMinions.getNbElements(); i++)
	{
		if (typeid(groupeDeMinions.at(i)) == typeid(GroupeMinion))
		{
			GroupeMinion* m = (GroupeMinion*)groupeDeMinions.at(i);
			m->vider();
			delete m; // ?
		}
	}

	for (int i = 0; i < groupeDeMinions.getNbElements(); i++)
	{
		groupeDeMinions.retirer(groupeDeMinions.at(i));
	}

}

/// <summary>
/// Permet de faire bouger les minions à une radiation selon leur distance
/// </summary>
/// <param name="ListeMinion">Pointeur sur une liste de minion</param>
/// <returns></returns>
//Sam
void GroupeMinion::bouger(Liste<Minion>* minionList)
{
	for (int i = 0; i < groupeDeMinions.getNbElements(); i++)
	{
		groupeDeMinions.at(i)->bouger(minionList);
	}
}

/// <summary>
/// Permet de faire arreter les minions à une radiation selon leur distance
/// </summary>
/// <returns></returns>
//Sam
void GroupeMinion::arreter()
{
	for (int i = 0; i < groupeDeMinions.getNbElements(); i++)
	{
		groupeDeMinions.at(i)->arreter();
	}
}


/// <summary>
/// Permet de faire bouger les minions à une radiation selon leur distance
/// </summary>
/// <param name="Vector2i positionCible">Vector2f& positionCible</param>
/// <returns></returns>
//Sam
void GroupeMinion::AjouterDestination(const Vector2f& positionCible)
{
	for (int i = 0; i < groupeDeMinions.getNbElements(); i++)
	{
		groupeDeMinions.at(i)->AjouterDestination(positionCible);
	}
}