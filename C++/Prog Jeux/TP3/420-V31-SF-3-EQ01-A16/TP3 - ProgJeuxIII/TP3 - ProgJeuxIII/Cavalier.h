#pragma once
#include "Minion.h"

class Cavalier : public Minion
{
public:
	Cavalier(bool isAlly, int id);
	~Cavalier();
	void charge();
	void annulerCharge();
	void PositionInitiale();
};