#pragma once
#include "Minion.h"

class GuerrierLeger : public Minion
{
public:
	GuerrierLeger(bool isAlly, int id);
	~GuerrierLeger();
	void millesLames();
	void annulerMillesLames();
	void PositionInitiale();
};