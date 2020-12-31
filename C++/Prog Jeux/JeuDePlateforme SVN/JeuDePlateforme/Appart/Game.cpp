#define _USE_MATH_DEFINES

#include "Game.h"
#include <iostream>
#include <math.h>
#include <vector>
#include <algorithm>

#include <cmath> 

#include "SpawnerFixe.h"
#include "SpawnerRandom.h"
#include "SpawnerCollectible.h"
#include "Collectible.h"

using namespace std;
using namespace platformer;

Game::Game()
{
	//On place dans le contructeur ce qui permet à la game elle-même de fonctionner

	mainWin.create(VideoMode(LARGEUR, HAUTEUR, 32), "Jeu de Plateforme");  // , Style::Titlebar); / , Style::FullScreen);
	//view = mainWin.getDefaultView();


	view = View(sf::FloatRect(0, 0, LARGEUR, HAUTEUR)); //Largeur et hauteur de votre résolution d’écran 
	view.setCenter(LARGEUR / 2, HAUTEUR / 2);
	mainWin.setView(view); //Comme la vue est reçu en référence, la manipuler va influencer ce qu’on a à l’écran

	//Synchonisation coordonnée à l'écran!  Normalement 60 frames par secondes
	//À faire absolument
	mainWin.setFramerateLimit(60);
	//mainWin.setVerticalSyncEnabled(true); //Équivalent... normalement
	score = 0;
}


Game::~Game()
{
	//SSPEICHERT
	delete player;
	delete backgroundSky;
	delete backgroundSkyT;
	delete[]background;
	delete[]backgroundT;
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

<<<<<<< .mine
||||||| .r11
	//string * strLevel = level.getLevel();

	//string * strLevel = level.getLevel();

	//création du joueur
	/*player = new Player(LARGEUR / 2, HAUTEUR-32, &mainWin);
	player->chargerTextures("Content\\Sprites\\Player\\Idle.png");
	player->ajustementsVisuels();*/

	//blockArray = new Block[numberOfBlocks];

	//blockArray = new Block[numberOfBlocks];

	//pour que le joueur affiche a 32px au dessus du bas de l'écran.
	//player->setPosition(LARGEUR / 2, HAUTEUR - (player->getTexture()->getSize().y) - 32);

	//for (int i = 0; i < (HAUTEUR / 32) - 2; i++)
	//{
	//	for (int j = 0; j < (LARGEUR / 32); j++)
	//	{
	//		if (strLevel[i][j] == '1')
	//		{	
	//			numberOfBlocks++;		
	//			blockArray[numberOfBlocks].Init( j * (LARGEUR / 32), i * (HAUTEUR / 32), &mainWin);
	//			blockArray[numberOfBlocks].chargerTextures("Content\\Tiles\\BlockA0.png");
	//		    blockArray[numberOfBlocks].ajustementsVisuels();
	//		}
	//		if (strLevel[i][j] == '2')
	//		{
	//			player = new Player(i * (HAUTEUR / 32), j * (LARGEUR / 32), &mainWin);
	//			player->chargerTextures("Content\\Sprites\\Player\\Idle.png");
	//			player->ajustementsVisuels();
	//		}
	//	}
	//}

	//for (int i = 0; i < (HAUTEUR / 32) - 2; i++)
	//{
	//	for (int j = 0; j < (LARGEUR / 32); j++)
	//	{
	//		if (strLevel[i][j] == '1')
	//		{
	//			numberOfBlocks++;
	//			blockArray[numberOfBlocks].Init(i * (HAUTEUR / 32), j * (LARGEUR / 32), &mainWin);
	//			blockArray[numberOfBlocks].chargerTextures("Content\\Tiles\\BlockA0.png");
	//		    blockArray[numberOfBlocks].ajustementsVisuels();
	//		}
	//		if (strLevel[i][j] == '2')
	//		{
	//			player = new Player(i * (HAUTEUR / 32), j * (LARGEUR / 32), &mainWin);
	//			player->chargerTextures("Content\\Sprites\\Player\\Idle.png");
	//			player->ajustementsVisuels();
	//		}
	//	}
	//}

	//création du joueur
=======
	

	//création du joueur
>>>>>>> .r12
	
<<<<<<< .mine
	showScore.setFont(font);
	showScore.setString("Score :" + std::to_string(score));
	showScore.setCharacterSize(50);
	showScore.setColor(Color::Yellow);
	//SSpeichert
||||||| .r11
/*	player = new Player(32, 32, &mainWin);
	player->chargerTextures("Content\\Sprites\\Player\\Idle.png");
	player->ajustementsVisuels()*/;
	
	//block = new Block();
	//block->Init( 32, 256, &mainWin);
	//block->chargerTextures("Content\\Tiles\\BlockA0.png");
	//block->ajustementsVisuels();
=======
	/*Player *player2 = new Player(32, 32, &mainWin);
	player->chargerTextures("Content\\Sprites\\Player\\Idle.png");
	player->ajustementsVisuels();
	
	Block* block2 = new Block();
	block2->Init( 32, 32, &mainWin);
	block2->chargerTextures("Content\\Tiles\\BlockA0.png");
	block2->ajustementsVisuels();
>>>>>>> .r12

<<<<<<< .mine
||||||| .r11
	//player->setOrigin(player->texture.getSize().x / 2, player->texture.getSize().y / 2);
	//view.setCenter(player->getPosition().x, player->getPosition().y);


//	
//||||||| .r7
	//création du joueur
	
	
	//view.setCenter(player->getPosition().x, player->getPosition().y);


	
//=======
//>>>>>>> .r9
	//.. init ennemis
	//.. AjustementsVisuels
	
=======
	testCollisionBoxes3(*player2, *block2);*/

	//player->setOrigin(player->texture.getSize().x / 2, player->texture.getSize().y / 2);
	//view.setCenter(player->getPosition().x, player->getPosition().y);


//	
//||||||| .r7
	//création du joueur
	
	//player->setOrigin(32, 32);
	
	//view.setCenter(player->getPosition().x, player->getPosition().y);


	
//=======
//>>>>>>> .r9
	//.. init ennemis
	//.. AjustementsVisuels
	
>>>>>>> .r12
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
			if (player->getPosition().x <= view.getCenter().x + (LARGEUR / 2)-(player->LARGEUR_PLAYER))
			{
				player->deplacement(true);
			}
	}
	if (Keyboard::isKeyPressed(Keyboard::Left))
	{
			if (player->getPosition().x >= view.getCenter().x - (LARGEUR / 2))
			{
				player->deplacement(false);
			}
	}
	//JCOTE

	//SSPEICHERT
	if (Keyboard::isKeyPressed(Keyboard::Up) &&  can_jump == true) //
	{
		player->jump();
		can_jump = false;
		//TODO: Calcul du Jump
	}
	//SSPEICHERT
}

void Game::update()
{
//<<<<<<< .mine
//	for (int i = 0; i < 20; i++)
//	{
//		if (!(player->getGlobalBounds().intersects(block->getGlobalBounds())))
//		{
//			player->setPosition(player->getPosition().x, __min(player->getPosition().y + VITESSE, block->getPosition().y - player->getOrigin().y)); //
//		}
//
//	}
//||||||| .r7
//	//for (int i = 0; i < 20; i++)
//	//{
//	//	if (!(player->getGlobalBounds().intersects(block[i]->getGlobalBounds())))
//	//	{
//	//		player->setPosition(player->getPosition().x, __min(player->getPosition().y + VITESSE, block[i]->getPosition().y - player->getOrigin().y)); //
//	//	}
//	//	else
//	//	{
//	//		player->setPosition(player->getPosition().x, player->getPosition().y);
//	//	}
//	//	
//	//}
//=======
	

	//view.setCenter(player->getPosition().x + (LARGEUR/4), HAUTEUR/2);
	if (view.getCenter().x - (LARGEUR / 2) != -LARGEUR && view.getCenter().x + (LARGEUR / 2) != (LARGEUR*2))
	{
		view.setCenter(player->getPosition().x + (LARGEUR/4), HAUTEUR/2);
	}
	else if (player->getPosition().x > view.getCenter().x - (LARGEUR / 4))
	{
		view.setCenter(player->getPosition().x + (LARGEUR / 4), HAUTEUR / 2);
	}
	
	//player->setOrigin(player->texture.getSize().x / 2, player->texture.getSize().y / 2);
	//TOFIX: ramène  toujours le personnage en bas completement de l'écran.
	/*for (int i = 0; i < 20; i++)
	{
		if (!(player->getGlobalBounds().intersects(block[i]->getGlobalBounds())))
		{
			player->setPosition(player->getPosition().x, __min(player->getPosition().y + VITESSE, block[i]->getPosition().y - player->getOrigin().y));
		}
		
	}*/
	view.setCenter(player->getPosition().x + (LARGEUR/4), HAUTEUR/2);
	showScore.setPosition(player->getPosition().x - (LARGEUR / 4), 16);


<<<<<<< .mine
	if (!VerifierCollisionBetweenPlayerAndBlock() )
||||||| .r11

	if (!VerifierCollisionBox() )
=======
	//Pour la gravité

	//if (!VerifierCollisionBlock())
	if (!VerifierCollisionBlock() )
>>>>>>> .r12
	{
		player->update();
		can_jump = false;
		
	}
	else
	{
		player->stop();
		can_jump = true;
	}
<<<<<<< .mine

	VerifierCollisionBetweenEnnemyAndBlock();
	
||||||| .r11
	
=======
>>>>>>> .r12

	VerifierCollisionCollectable();

<<<<<<< .mine
	if (VerifierCollisionBetweenPlayerAndCoin())
	{
		score += 10;
	}
	else if (VerifierCollisionBetweenPlayerAndEnnemy())
	{
		score += 50;
	}
||||||| .r11
=======
	vector<Ennemy>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy < ennemyList.end(); iterEnnemy++)
	{
		iterEnnemy->deplacement(true);
		if (typeid(*iterEnnemy) == typeid(Goomba))
		{
			iterEnnemy->update();
		}
	}
>>>>>>> .r12

<<<<<<< .mine
	vector<Ennemy*>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
	{
		(*iterEnnemy)->deplacement(true);
	}
||||||| .r11
=======
	vector<Collectible>::iterator iterCollectible;
	for (iterCollectible = collectibleList.begin(); iterCollectible < collectibleList.end(); iterCollectible++)
	{
		iterCollectible->update();
	}
>>>>>>> .r12


	//vector<Ennemy*>::iterator iterEnnemy;
	//for (iterEnnemy = ennemyList.begin(); iterEnnemy < ennemyList.end(); iterEnnemy++)
	//{
	//		(*iterEnnemy)->deplacement(false);
	//		//if ((testCollisionBoxes2(*player, *iterEnnemy) && player->getPosition().y - player->getTexture()->getSize().y / 2 < iterEnnemy->getPosition().y))
	//		//{
	//		//	(*iterEnnemy)->update();
	//		//}
	//}


	//for (iter = blockList.begin(); iter < blockList.end(); iter++)
	//{
	//	if (!(player->getGlobalBounds().intersects(iter->getGlobalBounds())))
	//	{
	//		player->setPosition(player->getPosition().x, player->getPosition().y + VITESSE);
	//	}
	//}
	//vector<Block>::const_iterator iter;
	//
	//Collider collide(*player, *iter);

	//merged

	//for (int i = 0; i < 20; i++)
	//{
	//	if (!(player->getGlobalBounds().intersects(block[i]->getGlobalBounds())))
	//	{
	//		player->setPosition(player->getPosition().x, __min(player->getPosition().y + VITESSE, block[i]->getPosition().y - player->getOrigin().y)); //
	//	}
	//	else
	//	{
	//		player->setPosition(player->getPosition().x, player->getPosition().y);
	//	}
	//	
	//}

	//merged end

	verifierMortPlayer();
}

void Game::draw()
{
	mainWin.clear();
	//JCOTE
	mainWin.setView(view);
	backgroundSky->setPosition((view.getCenter().x)-(LARGEUR/2), (view.getCenter().y)-(HAUTEUR/2));
	mainWin.draw(*backgroundSky);

	for (size_t i = 0; i < numBackground; i++)
	{
		//vérification si le background se trouve dans la zone d'affichage et vaut la peine d'être affiché.
		if (background[i].getPosition().x < (view.getCenter().x) + (LARGEUR / 2) && background[i].getPosition().x + LARGEUR > (view.getCenter().x)-(LARGEUR/2))

		{
			mainWin.draw(background[i]);
		}	
	}
	//JCOTE
	
	//SSPEICHERT
<<<<<<< .mine
	showScore.setString("Score :" + std::to_string(score));
	mainWin.draw(showScore);
||||||| .r11
//<<<<<<< .mine
=======
>>>>>>> .r12

<<<<<<< .mine
	vector<Block>::iterator iterBlock;
	for (iterBlock = blockList.begin(); iterBlock != blockList.end(); iterBlock++)
	{
		mainWin.draw(*iterBlock);
	}

	vector<Ennemy*>::iterator iterEnnemy;
	for (iterEnnemy = ennemyList.begin(); iterEnnemy != ennemyList.end(); iterEnnemy++)
||||||| .r11

//	for (int i = 0; i < numberOfBlocks; i++)
//||||||| .r7
	

	//for (int i = 0; i < numberOfBlocks; i++)
//=======
	vector<Block>::const_iterator iter;
	for ( iter = blockList.begin(); iter < blockList.end(); iter++)
=======
	vector<Block>::const_iterator iter;
	for ( iter = blockList.begin(); iter < blockList.end(); iter++)
>>>>>>> .r12
	{
		mainWin.draw(*(*iterEnnemy));
	}

	vector<Coin>::iterator iterCoin;
	for (iterCoin = coinList.begin(); iterCoin != coinList.end(); iterCoin++)
	{
		mainWin.draw(*iterCoin);
	}

	vector<Collectible>::const_iterator iterCollectible;
	for (iterCollectible = collectibleList.begin(); iterCollectible < collectibleList.end(); iterCollectible++)
	{
		mainWin.draw(*iterCollectible);
	}

	mainWin.draw(*player);
	//SSPEICHERT
	mainWin.display();
}

void Game::unload()
{
	//JCOTE
	
	blockList.clear();
	ennemyList.clear();
	collectibleList.clear();
	score = 0;
	
	BlockBuilder("Content\\Levels\\testlevel.txt", "Content\\Tiles\\BlockA0.png");
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
				//block.setOrigin(16, 16);
				blockList.push_back(block);

			}
			if (strLevel[i][j] == '2')
			{
				if (player == NULL)
				{
					player = new Player(j * 32, i * 32, &mainWin);
					//en attendant l'animation
					player->chargerTextures("Content\\Sprites\\Player\\Idle.png"); 
					player->ajustementsVisuels();
					player->setOrigin(player->getTexture()->getSize().x / 2, player->getTexture()->getSize().y / 2);
				}
			}
			if (strLevel[i][j] == '3')
			{
				SpawnerFixe * spawner = new SpawnerDeGoomba();
				Ennemy * ennemi1 = spawner->FabriquerEnnemy(j * 32, i * 32, &mainWin);
				
<<<<<<< .mine
				ennemi1->chargerTextures("Content\\Sprites\\goomba.png");
||||||| .r11
		//		if (ennemyList.empty())
		//		{
					ennemi1->chargerTextures("Content\\Tiles\\BlockA0.png");
				//}


				ennemi1->setColor(Color::Blue);
=======
		//		if (ennemyList.empty())
		//		{
					ennemi1->chargerTextures("Content\\Tiles\\BlockA0.png");
		//		}


				ennemi1->setColor(Color::Blue);
>>>>>>> .r12
				ennemi1->ajustementsVisuels();
				ennemi1->setOrigin(ennemi1->getTexture()->getSize().x / 2, ennemi1->getTexture()->getSize().y / 2);
				ennemyList.push_back(ennemi1);
			}
			if (strLevel[i][j] == '4')
			{
				Ennemy * ennemi2 = SpawnerRandom::CreateEnnemy(SpawnerRandom::cleverGoomba, j * 32, i * 32, &mainWin);
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
<<<<<<< .mine
			if (strLevel[i][j] == '$')
			{
				Coin * coin = new Coin(j * 32, i * 32, &mainWin);
				coin->chargerTextures("Content\\Sprites\\coin.png");
				coin->ajustementsVisuels();
				coin->setOrigin(coin->getTexture()->getSize().x / 2, coin->getTexture()->getSize().y / 2);
				coinList.push_back(*coin);
			}
||||||| .r11
=======
			if (strLevel[i][j] == '6')
			{
				SpawnerCollectible * spawnerD = new SpawnerDiamond();
				Collectible * collectible = spawnerD->FabriqueCollectible(j * 32, i * 32, &mainWin);
				collectible->chargerTextures("Content\\Sprites\\Gem.png");
				collectibleList.push_back(*collectible);
			}
>>>>>>> .r12
		}
	}
}

<<<<<<< .mine

bool Game::VerifierCollisionBetweenPlayerAndBlock()
||||||| .r11

bool Game::VerifierCollisionBox()
=======
bool Game::VerifierCollisionBlock()
>>>>>>> .r12
{
	vector<Block>::iterator iter;
	float distanceX = 0;
	float distanceY = 0;


	for (iter = blockList.begin(); iter != blockList.end(); iter++)
	{
		distanceX = (iter->getPosition().x - player->getPosition().x) * (iter->getPosition().x - player->getPosition().x);
		distanceY = (iter->getPosition().y - player->getPosition().y) * (iter->getPosition().y - player->getPosition().y);

		double calcdistance = sqrt(distanceX - distanceY);


		if (iter->getPosition().x > 0 && iter->getPosition().x < LARGEUR && iter->getPosition().y > 0 && iter->getPosition().y < HAUTEUR)
		{
			if (testCollisionBoxes2(*player, *iter) && player->getPosition().y - player->getTexture()->getSize().y/2 < iter->getPosition().y)
			{
				player->setPosition(player->getPosition().x, iter->getPosition().y  - (iter->getTexture()->getSize().y)); //Ceci stabilise la position du joueur au dessus de la plateforme!
				return true;
<<<<<<< .mine

||||||| .r11
=======
			}		
		}
	}
}

bool Game::VerifierCollisionCollectable()
{
	vector<Collectible>::iterator iter;
	for (iter = collectibleList.begin(); iter < collectibleList.end(); iter++)
	{
		if (iter->getPosition().x > 0 && iter->getPosition().x < LARGEUR && iter->getPosition().y > 0 && iter->getPosition().y < HAUTEUR)
		{
			if (testCollisionBoxes2(*player, *iter) && player->getPosition().y - player->getTexture()->getSize().y / 2 < iter->getPosition().y)
			{
				score += iter->getValue();
				iter->setPosition(INT_MAX, INT_MAX);
				return true;
>>>>>>> .r12
			}
<<<<<<< .mine

||||||| .r11
				
=======
>>>>>>> .r12
		}
	}
<<<<<<< .mine

||||||| .r11
=======
}

bool Game::verifierMortPlayer()
{
	if (player->getPosition().y > HAUTEUR && player->getNbLife()>0)
	{
		player->retriveLife();
		player->setPosition(player->startPositionX,player->startPositionY);
		return true;
	}
	else if (player->getNbLife()<=0)
	{
		mainWin.close();
	}
>>>>>>> .r12
	return false;
}

<<<<<<< .mine
}


bool Game::VerifierCollisionBetweenPlayerAndCoin()
{
	vector<Coin>::iterator iterCoin;
	for (iterCoin = coinList.begin(); iterCoin < coinList.end(); iterCoin++)
	{
		if (iterCoin->getPosition().x > 0 && iterCoin->getPosition().x < LARGEUR && iterCoin->getPosition().y > 0 && iterCoin->getPosition().y < HAUTEUR)
		{
			if (testCollisionBoxes2(*player, *iterCoin))
			{
				coinList.erase(iterCoin);
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

		double calcdistance = sqrt(distanceX - distanceY);


		if (testCollisionBoxes2(*player, *(*iterEnnemy)) && player->getPosition().y < (*iterEnnemy)->getPosition().y && calcdistance < player->getTexture()->getSize().y / 2 + (*iterEnnemy)->getTexture()->getSize().y / 2)
		{
			player->jump(); //Un rebond est appliqué si le joueur saute sur l'ennemi.
			delete (*iterEnnemy); //On supprime de la mémoire le pointeur pointé.
			ennemyList.erase(iterEnnemy); // Et on supprime ce pointeur.
			return true;
		}
		//else if (player->getPosition().y - player->getTexture()->getSize().y / 2 > (*iterEnnemy)->getPosition().y)
		//{
		//	
		//}
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

			double calcdistance = sqrt(distanceX - distanceY);

			if (iterBlock->getPosition().x > 0 && iterBlock->getPosition().x < LARGEUR && iterBlock->getPosition().y > 0 && iterBlock->getPosition().y < HAUTEUR)
			{
				if (testCollisionBoxes2(*(*iterEnnemy), *iterBlock) && player->getPosition().y < (*iterEnnemy)->getPosition().y && calcdistance < player->getTexture()->getSize().y / 2 + (*iterEnnemy)->getTexture()->getSize().y / 2)
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
||||||| .r11
=======
//à supprimer
bool Game::testCollisionBoxes3(Player *spr1, vector<Block>::const_iterator spr2)
{
	if (spr1->getPosition().x + spr1->getTexture()->getSize().x >= spr2->getPosition().x &&
		spr1->getPosition().x <= spr2->getPosition().x + spr2->getTexture()->getSize().x &&
		spr1->getPosition().y + spr1->getTexture()->getSize().y  >= spr2->getPosition().y &&
		spr1->getPosition().y <= spr2->getPosition().y + spr2->getTexture()->getSize().y)
	{
		return true;
	}
	return false;
>>>>>>> .r12
}