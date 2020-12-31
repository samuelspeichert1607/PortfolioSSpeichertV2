#pragma once
#include <iostream>
#include <sstream>
#include "ROB.h"

using namespace std;
class ConsoleMenu
{
public:
	ConsoleMenu();
	~ConsoleMenu();
	void Run();

private:
	char ReadValidInput(char tabValidInputs[], int nbElements);
	void DisplayMenu();
	void DisplayCredits();
	bool ManageSelection(char entry);
	
};

