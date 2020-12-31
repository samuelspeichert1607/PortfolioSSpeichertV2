#pragma once

class PlayerControls
{
public:
	static bool moveUp;
	static bool moveDown;
	static bool moveLeft;
	static bool moveRight;

	static bool rotateLeft;
	static bool rotateRight;

	static bool scaleUp;
	static bool scaleDown;

	static bool quit;

private:
	PlayerControls();
};

//Une variable statique est toutjours seulement définie dans une classe, elle doit ensuite être déclarée
//De suite après la déclaration de la classe ou dans le cpp, s'il existe

bool PlayerControls::moveUp;
bool PlayerControls::moveDown;
bool PlayerControls::moveLeft;
bool PlayerControls::moveRight;

bool PlayerControls::rotateLeft;
bool PlayerControls::rotateRight;

bool PlayerControls::scaleUp;
bool PlayerControls::scaleDown;

bool PlayerControls::quit;