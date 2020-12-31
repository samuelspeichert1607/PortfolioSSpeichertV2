#pragma once
#include "Minion.h"
#include "Sujet.h"

class Tour : public Minion, public Sujet
{
public:
	Tour(bool isAlly, int ID);
	~Tour();
	Minion* spawn(MinionType type);
private:
	int positionCourrante = 0;
};