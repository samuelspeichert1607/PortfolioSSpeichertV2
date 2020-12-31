#define _USE_MATH_DEFINES

#include "Game.h"
#include <iostream>
#include <math.h>
#include <vector>
#include <algorithm>

#include <cmath> 

#include "SpawnerFixe.h"
#include "SpawnerRandom.h"

using namespace std;
using namespace platformer;

Game::Game()
{
	//On place dans le constructeur ce qui permet à la game elle-même de fonctionner

	mainWin.create(VideoMode(LARGEUR, HAUTEUR, 32), "Jeu de Plateforme");  // , Style::Titlebar); / , Style::FullScreen);

	view = View(sf::FloatRect(0, 0, LARGEUR, HAUTEUR)); //Largeur et hauteur de votre résolution d’écran 
	view.setCenter(LARGEUR / 2, HAUTEUR / 2); //Le centre de la vue est fixé.
	mainWin.setView(view); //Comme la vue est reçu en référence, la manipuler va influencer ce qu’on a à l’écran

	//Synchonisation coordonnée à l'écran!  Normalement 60 frames par secondes
	//À faire absolument
	mainWin.setFramerateLimit(60);
	//mainWin.setVerticalSyncEnabled(true); //Équivalent... normalement
}


Game::~Game()
{
	//SSPEICHERT
	delete player;
	delete backgroundT;
	delete background;
	delete backgroundSkyT;
	delete backgroundSky;

	/*vector<Block>::iterator iterBlock;
	for (iterBlock = blockList.begin(); iterBlock != blockList.end(); iterBlock++)
	{
		delete iterBlock;
	}*/

	vector<Ennemy*>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
	{
		delete (*iterEnnemy);
	}

	vector<Collectible*>::iterator iterCollectible;
	for (iterCollectible = collectibleList.begin(); iterCollectible != collectibleList.end(); iterCollectible++)
	{
		delete (*iterCollectible);
	}
	//SSPEICHERT
}


int Game::run()
{
	if (!init())
	{
		return EXIT_FAILURE;
	}

	while (mainWin.isOpen())
	{
		getInputs();
		update();
		draw();
	}

	unload();

	return EXIT_SUCCESS;
}

bool Game::init()
{
	//JCOTE
	//TODO: créer une classe parent pour le background et les blocs.
	backgroundT = new Texture[numBackground];
	background = new Sprite[numBackground];
	backgroundSkyT = new Texture;
	backgroundSky = new Sprite;

	if (!backgroundSkyT->loadFromFile("Content\\Backgrounds\\Layer0_2.png"))
	{
		return false;
	}
	if (!backgroundT[0].loadFromFile("Content\\Backgrounds\\Layer1_0.png"))
	{
		return false;
	}
	if (!backgroundT[1].loadFromFile("Content\\Backgrounds\\Layer1_1.png"))
	{
		return false;
	}
	if (!backgroundT[2].loadFromFile("Content\\Backgrounds\\Layer1_2.png"))
	{
		return false;
	}

	//setTexture
	backgroundSky->setTexture(*backgroundSkyT);
	for (size_t i = 0; i < numBackground; i++)
	{
		background[i].setTexture(backgroundT[i]);
	}

	//setPosition
	backgroundSky->setPosition(0, 0);
	background[0].setPosition(-LARGEUR, 0);
	background[1].setPosition(0,0);
	background[2].setPosition(LARGEUR,0);

	//SSpeichert et JCote
	//création des blocks
	BlockBuilder("Content\\Levels\\testlevel.txt", "Content\\Tiles\\BlockA0.png");
	//SSpeichert et JCote

	//SSpeichert
	font.loadFromFile("Content\\Fonts\\Aztecways.ttf");

	
	showScore.setFont(font);
	showScore.setString("Score :" + std::to_string(score));
	showScore.setCharacterSize(50);
	showScore.setColor(Color::Yellow);
	//SSpeichert

	//JCOTE
	return true;
}

void Game::getInputs()
{
	//On passe l'événement en référence et celui-ci est chargé du dernier événement reçu!
	while (mainWin.pollEvent(event))
	{
		//x sur la fenêtre
		if (event.type == Event::Closed)
		{
			mainWin.close();
		}
	}
	//JCOTE
	//pour les déplacements du joueur.
	if (Keyboard::isKeyPressed(Keyboard::Right))
	{
		player->deplacement(true);
	}
	if (Keyboard::isKeyPressed(Keyboard::Left))
	{
		player->deplacement(false);
	}
	//JCOTE

	//SSPEICHERT
	if (Keyboard::isKeyPressed(Keyboard::Up) &&  can_jump == true)
	{
		player->jump();
		can_jump = false;
	}
	//SSPEICHERT
}

void Game::update()
{

	view.setCenter(player->getPosition().x + (LARGEUR/4), HAUTEUR/2);
	showScore.setPosition(player->getPosition().x - (LARGEUR / 4), 16);


	if (!VerifierCollisionBetweenPlayerAndBlock() )
	{
		player->update();
		can_jump = false;
		
	}
	else
	{
		player->stop();
		can_jump = true;
	}

	VerifierCollisionBetweenEnnemyAndBlock();
	
	if (VerifierCollisionBetweenPlayerAndCollectible())
	{
		score += 10;
	}
	else if (VerifierCollisionBetweenPlayerAndEnnemy())
	{
		score += 50;
	}

	vector<Ennemy*>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
	{
		(*iterEnnemy)->deplacement(false);
	}

}

void Game::draw()
{
	mainWin.clear();
	//JCOTE
	mainWin.setView(view);
	backgroundSky->setPosition((view.getCenter().x) - (LARGEUR / 2), (view.getCenter().y) - (HAUTEUR / 2));
	mainWin.draw(*backgroundSky);
	for (size_t i = 0; i < numBackground; i++)
	{
		//TODO: changer la vérification pour qu'elle fonctionne avec setview.
		//vérification si le background se trouve dans la zone d'affichage et vaut la peine d'être affiché.
		if (background[i].getPosition().x < (view.getCenter().x) + (LARGEUR / 2) && background[i].getPosition().x + LARGEUR >(view.getCenter().x) - (LARGEUR / 2))
		{
			mainWin.draw(background[i]);
		}	
	}
	//JCOTE
	
	//SSPEICHERT
	showScore.setString("Score :" + std::to_string(score));
	mainWin.draw(showScore);

	vector<Block>::iterator iterBlock;
	for (iterBlock = blockList.begin(); iterBlock != blockList.end(); iterBlock++)
	{
		mainWin.draw(*iterBlock);
	}

	vector<Ennemy*>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
	{
		mainWin.draw(*(*iterEnnemy));
	}

	vector<Collectible*>::iterator iterCollectible;
	for (iterCollectible = collectibleList.begin(); iterCollectible != collectibleList.end(); iterCollectible++)
	{
		mainWin.draw(*(*iterCollectible));
	}

	mainWin.draw(*player);
	//SSPEICHERT
	mainWin.display();
}

void Game::unload()
{
	//JCOTE
	delete backgroundSky;
	delete backgroundSkyT;
	delete[]background;
	delete[]backgroundT;
	delete player;
	//JCOTE
}

// code pris ici : http://stackoverflow.com/questions/6083626/box-collision-code
bool Game::testCollisionBoxes(int x, int y, int oWidth, int oHeight, int xTwo, int yTwo, int oTwoWidth, int oTwoHeight)
{
	if (x + oWidth < xTwo || x > xTwo + oTwoWidth) return false;
	if (y + oHeight < yTwo || y > yTwo + oTwoHeight) return false;

	return true;
}

//SSPEICHERT
bool Game::testCollisionBoxes2(Sprite spr1, Sprite spr2)
{
	IntRect r1(spr1.getPosition().x - spr1.getTexture()->getSize().x / 2, spr1.getPosition().y - spr1.getTexture()->getSize().y / 2, spr1.getTexture()->getSize().x, spr1.getTexture()->getSize().y);
	IntRect r2(spr2.getPosition().x - spr2.getTexture()->getSize().x / 2, spr2.getPosition().y - spr2.getTexture()->getSize().y / 2, spr2.getTexture()->getSize().x, spr2.getTexture()->getSize().y);
	
	return r1.intersects(r2);
}
//SSPEICHERT

void Game::BlockBuilder(const char levelPath[], const char BlockTPath[])
{
	LevelManager level(levelPath);
	string * strLevel = level.getLevel();


	for (int i = 0; i < (HAUTEUR / 32); i++)
	{
		for (int j = 0; j < (LARGEUR / 32); j++)
		{
			if (strLevel[i][j] == '1')
			{
				Block block;
				block.Init(j * block.LARGEUR_BLOCK, i * block.LARGEUR_BLOCK, &mainWin);
				if (blockList.empty())
				{
					block.chargerTextures(BlockTPath);
				}
				block.ajustementsVisuels();
				blockList.push_back(block);

			}
			if (strLevel[i][j] == '2')
			{
				player = new Player(j * 32, i * 32, &mainWin);
				player->chargerTextures("Content\\Sprites\\Player\\Idle.png"); 
				player->ajustementsVisuels();
				player->setOrigin(player->getTexture()->getSize().x / 2, player->getTexture()->getSize().y / 2);
			}
			if (strLevel[i][j] == '3')
			{
				SpawnerFixe * spawner = new SpawnerDeGoomba();
				Ennemy * ennemi1 = spawner->FabriquerEnnemy(j * 32, i * 32, &mainWin);
				
				ennemi1->chargerTextures("Content\\Sprites\\goomba.png");
				ennemi1->ajustementsVisuels();
				ennemi1->setOrigin(ennemi1->getTexture()->getSize().x / 2, ennemi1->getTexture()->getSize().y / 2);
				ennemyList.push_back(ennemi1);
			}
			if (strLevel[i][j] == '4')
			{
				Ennemy * ennemi2 = SpawnerRandom::CreateEnnemy(SpawnerRandom::spike, j * 32, i * 32, &mainWin);
				ennemi2->chargerTextures("Content\\Sprites\\goomba.png");
			 	ennemi2->setColor(Color::Red);
				ennemi2->ajustementsVisuels();
				ennemi2->setOrigin(ennemi2->getTexture()->getSize().x / 2, ennemi2->getTexture()->getSize().y / 2);
				ennemyList.push_back(ennemi2);
			}
			if (strLevel[i][j] == '5')
			{
				Ennemy * ennemi3 = SpawnerRandom::CreateEnnemy(SpawnerRandom::paraGoomba, j * 32, i * 32, &mainWin);
				ennemi3->chargerTextures("Content\\Sprites\\goomba.png");
				ennemi3->setColor(Color::Cyan);
				ennemi3->ajustementsVisuels();
				ennemi3->setOrigin(ennemi3->getTexture()->getSize().x / 2, ennemi3->getTexture()->getSize().y / 2);
				ennemyList.push_back(ennemi3);
			}
			if (strLevel[i][j] == '$')
			{
				Collectible * collectible = new Coin(j * 32, i * 32, &mainWin);
				collectible->chargerTextures("Content\\Sprites\\coin.png");
				collectible->setOrigin(collectible->getTexture()->getSize().x / 2, collectible->getTexture()->getSize().y / 2);
				collectibleList.push_back(collectible);
			}
			if (strLevel[i][j] == 'D')
			{
				Collectible * collectible = new Coin(j * 32, i * 32, &mainWin);
				collectible->chargerTextures("Content\\Sprites\\Gem.png");
				collectible->setOrigin(collectible->getTexture()->getSize().x / 2, collectible->getTexture()->getSize().y / 2);
				collectibleList.push_back(collectible);
			}
		}
	}
}


bool Game::VerifierCollisionBetweenPlayerAndBlock()
{
	vector<Block>::iterator iter;
	float distanceX = 0;
	float distanceY = 0;


	for (iter = blockList.begin(); iter != blockList.end(); iter++)
	{
		distanceX = (iter->getPosition().x - player->getPosition().x) * (iter->getPosition().x - player->getPosition().x);
		distanceY = (iter->getPosition().y - player->getPosition().y) * (iter->getPosition().y - player->getPosition().y);

		double distance = sqrt(distanceX - distanceY);


		if (iter->getPosition().x > 0 && iter->getPosition().x < LARGEUR && iter->getPosition().y > 0 && iter->getPosition().y < HAUTEUR)
		{
			if (testCollisionBoxes2(*player, *iter) && player->getPosition().y - player->getTexture()->getSize().y/2 < iter->getPosition().y)
			{
				player->setPosition(player->getPosition().x, iter->getPosition().y  - (iter->getTexture()->getSize().y)); //Ceci stabilise la position du joueur au dessus de la plateforme!
				return true;
			}

		}
	}

	return false;

}


bool Game::VerifierCollisionBetweenPlayerAndCollectible()
{
	vector<Collectible*>::iterator iterCollectible;
	for (iterCollectible = collectibleList.begin(); iterCollectible < collectibleList.end(); iterCollectible++)
	{
		if ((*iterCollectible)->getPosition().x > 0 && (*iterCollectible)->getPosition().x < LARGEUR && (*iterCollectible)->getPosition().y > 0 && (*iterCollectible)->getPosition().y < HAUTEUR)
		{
			if (testCollisionBoxes2(*player, *(*iterCollectible)))
			{
				collectibleList.erase(iterCollectible);
				return true;
			}
		}
	}

	return false;

}

bool Game::VerifierCollisionBetweenPlayerAndEnnemy()
{
	vector<Ennemy*>::iterator iterEnnemy;

	float distanceX = 0;
	float distanceY = 0;

	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
	{
		distanceX = ((*iterEnnemy)->getPosition().x - player->getPosition().x) * ((*iterEnnemy)->getPosition().x - player->getPosition().x);
		distanceY = ((*iterEnnemy)->getPosition().y - player->getPosition().y) * ((*iterEnnemy)->getPosition().y - player->getPosition().y);

		double distance = sqrt(distanceX - distanceY);

		if (testCollisionBoxes2(*player, *(*iterEnnemy)))
		{
			if (typeid(*(*iterEnnemy)) == typeid(Goomba) || typeid(*(*iterEnnemy)) == typeid(ParaGoomba))
			{
				if (player->getPosition().y < (*iterEnnemy)->getPosition().y && distance < player->getTexture()->getSize().y / 2 + (*iterEnnemy)->getTexture()->getSize().y / 2)
				{
					player->jump(); //Un rebond est appliqué si le joueur saute sur l'ennemi.
					delete (*iterEnnemy); //On supprime de la mémoire le pointeur pointé.
					ennemyList.erase(iterEnnemy); // Et on supprime ce pointeur.
				}
			}
			return true;
		}
	}

	return false;

}


void Game::VerifierCollisionBetweenEnnemyAndBlock()
{
	vector<Block>::iterator iterBlock;
	vector<Ennemy*>::iterator iterEnnemy;

	float distanceX = 0;
	float distanceY = 0;

	for (iterBlock = blockList.begin(); iterBlock != blockList.end(); iterBlock++)
	{
		for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
		{

			distanceX = (iterBlock->getPosition().x - (*iterEnnemy)->getPosition().x) * (iterBlock->getPosition().x - (*iterEnnemy)->getPosition().x);
			distanceY = (iterBlock->getPosition().y - (*iterEnnemy)->getPosition().y) * (iterBlock->getPosition().y - (*iterEnnemy)->getPosition().y);

			double distance = sqrt(distanceX - distanceY);

			if (iterBlock->getPosition().x > 0 && iterBlock->getPosition().x < LARGEUR && iterBlock->getPosition().y > 0 && iterBlock->getPosition().y < HAUTEUR)
			{
				if (testCollisionBoxes2(*(*iterEnnemy), *iterBlock) && player->getPosition().y < (*iterEnnemy)->getPosition().y && distance < player->getTexture()->getSize().y / 2 + (*iterEnnemy)->getTexture()->getSize().y / 2)
				{
					(*iterEnnemy)->stop();
				}
				else if ((typeid(*(*iterEnnemy)) != typeid(ParaGoomba)))
				{
					(*iterEnnemy)->update();
				}
			}
		}
	}
}