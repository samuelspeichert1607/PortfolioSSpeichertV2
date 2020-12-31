//Notez que tous les observateurs peuvent réutiliser ce fichier intégralement.  Ce sera toujours la forme de l'observateur
#pragma once

class Sujet;


class Observateur
{
public:
	virtual bool notifier(Sujet* sujet) = 0;
};