#pragma once
#include "Minion.h"

class GuerrierLourd : public Minion
{
public:
	GuerrierLourd(bool isAlly, int id);
	~GuerrierLourd();
	void murDAcier();
	void PositionInitiale();
};