#pragma once
#include <string>
#include <SFML/Graphics.hpp>
using namespace sf;


using namespace std;

namespace platformer
{
	class LevelManager
	{
	public:
		LevelManager(string fileName);
		~LevelManager();
		string * getLevel();

		const int HAUTEUR_NIVEAU = 15;
		

	private:
		void loadLevel();
		string sFilename = "";
		string * level;

	};
}