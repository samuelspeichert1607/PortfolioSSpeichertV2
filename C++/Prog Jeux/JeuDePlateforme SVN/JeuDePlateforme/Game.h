#pragma once

#ifndef _DEBUG
	#define NDEBUG 
#endif
#include <cassert>

#include <SFML/Graphics.hpp>
#include "Player.h"
#include "LevelManager.h"
#include "Collectible.h"
#include "Diamond.h"
#include "Coin.h"
#include "Block.h"
#include "Goomba.h"
#include "Spike.h"
#include "ParaGoomba.h"
using namespace sf;
using namespace std;


namespace platformer
{
	class Game
	{
	public:
		Game();
		~Game();
		int run();

		const int LARGEUR = 800;
		const int HAUTEUR = 480;

	private:
		bool init();
		void getInputs();
		void update();
		void draw();

		//Nouveau à présent qu'on utilise les pointeurs, on va tout libérer avant de terminer
		void unload();
		bool testCollisionBoxes(int x, int y, int oWidth, int oHeight, int xTwo, int yTwo, int oTwoWidth, int oTwoHeight);
		bool testCollisionBoxes2(Sprite spr1, Sprite spr2);
		void BlockBuilder(const char levelPath[], const char blockTPath[]);
		bool VerifierCollisionBetweenPlayerAndBlock();
		bool VerifierCollisionBetweenPlayerAndCollectible();
		bool VerifierCollisionBetweenPlayerAndEnnemy();
		void VerifierCollisionBetweenEnnemyAndBlock();
		const int numBackground = 3;

		Sprite* background;
		Texture* backgroundT;
		Sprite* backgroundSky;
		Texture* backgroundSkyT;

		Player* player = NULL;

		bool can_jump = false; //Booléen indiquant si le joueur peut sauter ou non.

		

		Font font; // Le style d'écriture utilisé pour le score et le hp.
		Text showScore; // Le texte qui va afficher le score du joueur.
		int score = 0; // Le score du joueur
		
		vector<Block> blockList; //liste des blocks du niveau.
		vector<Ennemy*> ennemyList; //liste des ennemis du niveau.
		vector<Collectible*> collectibleList; //liste des pièces du niveau.

		RenderWindow mainWin;
		View view;
		Event event;
	};
}