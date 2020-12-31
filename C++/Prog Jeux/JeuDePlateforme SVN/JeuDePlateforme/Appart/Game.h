#pragma once

#include <SFML/Graphics.hpp>
#include "Player.h"
#include "LevelManager.h"

#include "Coin.h"
#include "Block.h"
#include "Goomba.h"
#include "CleverGoomba.h"
#include "ParaGoomba.h"
#include "Collectible.h"
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
		const float VITESSE = 5;
		const float GRAVITE = 2;

		//Nombre d'animations et frames par animation qu'on a dans notre spriteSheet.
		//Oui c'est hardcodé; on ne peut pas faire autrement
		//Si vous avez une solution venez me l'exposer
		const int NOMBRE_ANIMATIONS = 3;
		const int NOMBRE_FRAMES = 2;

	private:
		bool init();
		void getInputs();
		void update();
		void draw();

		//Nouveau à présent qu'on utilise les pointeurs, on va tout libérer avant de terminer
		void unload();
		bool testCollisionBoxes(int x, int y, int oWidth, int oHeight, int xTwo, int yTwo, int oTwoWidth, int oTwoHeight);
		bool testCollisionBoxes2(Sprite spr1, Sprite spr2);
		bool testCollisionBoxes3(Player *spr1, vector<Block>::const_iterator spr2);
		void BlockBuilder(const char levelPath[], const char blockTPath[]);
<<<<<<< .mine
		bool VerifierCollisionBetweenPlayerAndBlock();
		bool VerifierCollisionBetweenPlayerAndCoin();
		bool VerifierCollisionBetweenPlayerAndEnnemy();
		void VerifierCollisionBetweenEnnemyAndBlock();
||||||| .r11
		bool VerifierCollisionBox();
=======
		bool VerifierCollisionBlock();
		bool VerifierCollisionCollectable();
		bool VerifierCollisionBox();
>>>>>>> .r12
		bool verifierMortPlayer();
		const int numBackground = 3;

		Sprite* background;
		Texture* backgroundT;
		Sprite* backgroundSky;
		Texture* backgroundSkyT;

		Player* player = NULL;

		bool can_jump = false;

		

		Font font; // Le style d'écriture utilisé pour le score et le hp.
		Text showScore; // Le texte qui va afficher le score du joueur.
		int score = 0; // Le score du joueur
		
		vector<Block> blockList; //liste des blocks du niveau.
		vector<Ennemy*> ennemyList; //liste des ennemis du niveau.
		vector<Coin> coinList; //liste des pièces du niveau.

<<<<<<< .mine
||||||| .r11
		vector<Block> blockList;
		vector<Ennemy> ennemyList;

=======
		vector<Block> blockList;
		vector<Ennemy> ennemyList;
		vector<Collectible> collectibleList;

>>>>>>> .r12
		RenderWindow mainWin;
		View view;
		Event event;

		int score;
	};
}