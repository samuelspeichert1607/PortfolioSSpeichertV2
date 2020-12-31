#pragma once

#include <cmath>
#include "Player.h"
#include "Projectile.h"
#include "LasPlagas.h"
using namespace sf;

class Game
{
public:
	Game();
	int run();
	int testTest();
	bool init();
	void getInputs();
	void Update();
	void EnemyManager();
	void Fire();
	bool TestCollisionSpheresBetweenProjectileAndLasPlagas(Projectile projectile);
	bool TestCollisionSpheresBetweenPlayerAndLasPlagas(LasPlagas lasPlagas);
	void runTitle();
	void endScreen();

	void Draw();
	
	Font font; // Le style d'�criture utilis� pour le score et le hp.
	Text showHp; // Le texte qui va afficher le hp.
	Text showScore; // Le texte qui va afficher le score.
	const float PLAYER_SPEED = 8.0f; //La vitesse des d�placements du joueur

	const int LARGEUR = 1280; //Largeur de la fen�tre
	const int HAUTEUR = 960; //Longueur de la fen�tre

	
	

	Player player; //Joueur d�clar�
	
	static const int MAX_PROJECTILE = 30; //Maximum de projectiles autoris�
	static const int MAX_LASPLAGAS = 3; //Maximum de Las Plagas autoris�




private:
	enum STATE_GAME { titleScreen, play, gameOver }; //Diff�rents �crans de jeu

	Sprite backgroundSprite; //Sprite du background
	Texture backgroundTexture; //Texture du background

	Sprite titleScreenSprite; //Sprite de l'�cran titre
	Texture titleScreenTexture; //Texture de l'�cran titre

	Sprite gameOverScreenSprite; //Sprite de l'�cran de fin de jeu.
	Texture gameOverScreenTexture; //Texture de l'�cran de fin de jeu

	LasPlagas tabLasPlagas[MAX_LASPLAGAS]; //Tableau de Las Plagas
	Projectile tabProjectiles[MAX_PROJECTILE]; //Tableau de Projectiles




	int cadence = 100; //Fr�quence de tir du joueur

	int score = 0; //Score

	float angleAiming = 0.0f; //Angle de visuel du joueur
	
	
	RenderWindow mainWin; //Fen�tre d'affichage
	View view; // Vue
	Event event;
	STATE_GAME stateGame = STATE_GAME::titleScreen; //�cran de d�part.
};