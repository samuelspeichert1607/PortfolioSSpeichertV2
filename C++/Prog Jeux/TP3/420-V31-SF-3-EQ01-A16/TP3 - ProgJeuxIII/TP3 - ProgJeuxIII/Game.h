#pragma once
#ifndef _DEBUG
	#define NDEBUG 
#endif
#include <cassert>
#include <SFML/Graphics.hpp>
#include "Constantes.h"
#include <vector>
#include "SpawnerMinion.h"
#include "GroupeMinion.h"
#include "Spell.h"
#include "File.h"

using namespace sf;
using namespace std;

class Game
{
	public:
		Game();
		~Game();
		int run();
		int testTest();

		private:
		
		void runTitle(); // Écran titre

		bool init();
		void getInputs();
		void update();
		void draw();

		

		bool testerPrescenceSpell(Vector2i& position);
		void ajouterSpell(Vector2i position);
		//void activerSpell(Vector2i position);


		static const int NBR_OBJETS = 30;
	
	private:

		Sprite background;
		Texture backgroundT;
		int nbRessources = 18;
		int nbRessourcesEnnemie = 18;
		RectangleShape selecteur;
		Vector2f positionSelecteur = Vector2f(-1, -1);
		Vector2f tailleSelecteur = Vector2f(-1, -1);
		SpellType choixSpell = SpellType::Eclair;
		
		Liste<Minion> minionList = Liste<Minion>();
		Liste<Spell> spellList = Liste<Spell>();
		vector<Projectile> listeProjectile = vector<Projectile>();
		//Pile utilisée pour l'ordre des spells.
		Pile<Spell> ordreDesSpells = Pile<Spell>();

		/// <summary>
		/// Mist qui détermine ce qu'on déplace
		/// </summary>
		GroupeMinion groupeDeMinions;
		Tour* allyTower;

		File<Minion> minionQueueAlly; //File des minions alliés
		File<Minion> minionQueueEnnemy; //File des minions ennemis

		RenderWindow mainWin;
		Event event;
		Clock allyClock;
		Clock spellClock;
		Clock ennemyClock;
		Clock timeBeforeEnd;
		Clock ressourceClock;

		Font font;
		Text commandes;
		Text textRessource;
		Text textRessourceEnnemie;
		Text issueDeLaPartie;

		enum STATE_GAME { titleScreen, play, gameOver }; //Différents écrans de jeu
		STATE_GAME stateGame = STATE_GAME::titleScreen; //Écran de départ.

		Sprite titleScreenSprite; //Sprite de l'écran titre
		Texture titleScreenTexture; //Texture de l'écran titre
};