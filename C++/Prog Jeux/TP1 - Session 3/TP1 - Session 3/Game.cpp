#define _USE_MATH_DEFINES

#include "Game.h"
#include "PlayerControls.h"

/// <summary>
/// Constructeur de la classe Game
/// </summary>
/// <returns>Aucune valeur de retour</returns>
Game::Game()
{
	mainWin.create(VideoMode(LARGEUR, HAUTEUR, 32), "Another unoriginal zombie game");
	view = mainWin.getDefaultView();

	

	//Synchonisation coordonnée à l'écran!  Normalement 60 frames par secondes
	//À faire absolument
	mainWin.setVerticalSyncEnabled(true);
	mainWin.setFramerateLimit(60);  //Équivalent... normalement

}


int Game::testTest()
{
	return 0;
}

/// <summary>
/// Méthode permettant de faire rouler le jeu.
/// </summary>
/// <param name="&manager">La référence du gestionnaire de points.</param>
/// <param name="&lakitu">La référence du "lakitu".</param>
/// <returns>Retourne 1 si le programme se déroule bien. Sinon, il va retourner un autre entier.</returns>
int Game::run()
{
	
	

	if (!init())
	{
		return EXIT_FAILURE;
	}

	while (mainWin.isOpen())
	{
		if (stateGame == STATE_GAME::titleScreen)
		{
			runTitle();
		}
		if (stateGame == STATE_GAME::play)
		{
			//////////////////////////////////////////////////////////////////////////
			// 1 - Gestion des entrées
			//////////////////////////////////////////////////////////////////////////
			Event event;

			//On passe l'événement en référence et celui-ci est chargé du dernier événement reçu!
			while (mainWin.pollEvent(event))
			{
				if (event.type == Event::Closed)
				{
					mainWin.close();
				}
			}

			getInputs();

			//////////////////////////////////////////////////////////////////////////
			// 2 - Gestion de la logique 
			//////////////////////////////////////////////////////////////////////////

			Update();



			//////////////////////////////////////////////////////////////////////////
			// 3 - Gestion de l'affichage 
			//////////////////////////////////////////////////////////////////////////


			//Toujours important d'effacer l'écran précédent
			mainWin.clear();
			Draw();
			mainWin.display();

		}
		if (stateGame == STATE_GAME::gameOver)
		{
			endScreen();
		}
		
	}
	return EXIT_SUCCESS;
}

bool Game::init()
{
	

	backgroundTexture.loadFromFile("Assets\\Terrains\\GrassTexture1920x1080.jpg");

	backgroundSprite.setTexture(backgroundTexture);
	backgroundSprite.setPosition(0, 0);


	if (!titleScreenTexture.loadFromFile("Assets\\Title.png"))
	{
		return false;
	}
	titleScreenSprite.setTexture(titleScreenTexture);

	if (!gameOverScreenTexture.loadFromFile("Assets\\GameOver.png"))
	{
		return false;
	}
	gameOverScreenSprite.setTexture(gameOverScreenTexture);


	font.loadFromFile("Assets\\youmurdererbb_reg.ttf");
	showHp.setString("HP remaining:" + std::to_string(player.GetHp()));
	showHp.setFont(font);
	showHp.setCharacterSize(50);
	showHp.setColor(Color::Red);
	showHp.setPosition(10, 10);

	showScore.setString("Score :" + std::to_string(score));
	showScore.setFont(font);
	showScore.setCharacterSize(50);
	showScore.setColor(Color::Red);
	showScore.setPosition(650, 10);

	
	
	return true;
}

void Game::runTitle()
{
	while (mainWin.pollEvent(event))
	{
		if (event.type == Event::Closed)
		{
			mainWin.close();
		}

		if (event.type == Event::MouseButtonPressed)
		{
			if (event.mouseButton.button == Mouse::Left)
			{
				stateGame = STATE_GAME::play;
			}
		}

		if (event.type == Event::KeyPressed)
		{
			if (event.key.code == Keyboard::Return)
			{
				stateGame = STATE_GAME::play;
			}
		}
	}

	mainWin.clear();
	mainWin.draw(titleScreenSprite);
	mainWin.display();
}
/// <summary>
/// Méthode permettant d'afficher l'écran de fin.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::endScreen()
{
	while (mainWin.pollEvent(event))
	{
		if (event.type == Event::Closed)
		{
			mainWin.close();
		}
	}

	mainWin.clear();
	mainWin.draw(gameOverScreenSprite);
	mainWin.display();
}

/// <summary>
/// Méthode permettant de gérer les entrants du jeu.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::getInputs()
{
	//On passe l'événement en référence et celui-ci est chargé du dernier événement reçu!
	while (mainWin.pollEvent(event))
	{
		if (event.type == Event::Closed)
		{
			mainWin.close();
		}
	}

	//------------Clavier---------------------------------------------------------------------
	PlayerControls::moveLeft = Keyboard::isKeyPressed(Keyboard::Left);
	PlayerControls::moveRight = Keyboard::isKeyPressed(Keyboard::Right);
	PlayerControls::moveUp = Keyboard::isKeyPressed(Keyboard::Up);
	PlayerControls::moveDown = Keyboard::isKeyPressed(Keyboard::Down);
	PlayerControls::scaleUp = Keyboard::isKeyPressed(Keyboard::PageUp);
	PlayerControls::scaleDown = Keyboard::isKeyPressed(Keyboard::PageDown);

}

/// <summary>
/// Méthode permettant de gérer la logique du jeu.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::Update()
{
	cadence++;
	



	if (Mouse::getPosition(mainWin).x - player.GetPositionX() != 0 )
	{
		angleAiming = (atan2f((Mouse::getPosition(mainWin).y - player.GetPositionY()), (Mouse::getPosition(mainWin).x - player.GetPositionX()))) * 180 / M_PI;		
		// Aide de Jonathan Saindon
    }	
	
	player.SetAngle(angleAiming);

	if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && cadence > 100) 
	{	
		Fire();
		cadence = 0;
	}
	

	EnemyManager();


	for (int i = 0; i < MAX_PROJECTILE; i++)
	{
		if (tabProjectiles[i].activated == true)
		{
			tabProjectiles[i].Update();
		}
	}

	for (int i = 0; i < MAX_LASPLAGAS; i++)
	{
		if (tabLasPlagas[i].activated == true)
		{
			tabLasPlagas[i].Update();
			tabLasPlagas[i].SetAngle(player.GetPositionX(), player.GetPositionY());
		
			if (TestCollisionSpheresBetweenPlayerAndLasPlagas(tabLasPlagas[i]))
			{
			
				player.SetHp(-5);
				tabLasPlagas[i].activated = false;
				score += 100;
			}
		}
	}



	if (PlayerControls::moveUp)
	{
		player.SetPositionY(player.GetPositionY() - PLAYER_SPEED);
	}
	if (PlayerControls::moveDown)
	{
		player.SetPositionY(player.GetPositionY() + PLAYER_SPEED);
	}
	if (PlayerControls::moveLeft)
	{
		player.SetPositionX(player.GetPositionX() - PLAYER_SPEED);
	}
	if (PlayerControls::moveRight)
	{
		player.SetPositionX(player.GetPositionX() + PLAYER_SPEED);
	}

}

/// <summary>
/// Méthode permettant de gérer l'activation des ennemis.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::EnemyManager()
{
	static int indexLasPlagasActuel = 0;


	if (tabLasPlagas[indexLasPlagasActuel].activated != true)
	{
		tabLasPlagas[indexLasPlagasActuel].activated = true;

		tabLasPlagas[indexLasPlagasActuel].SetAngle(player.GetPositionX(), player.GetPositionY());
	}

	indexLasPlagasActuel++;

	if (indexLasPlagasActuel == MAX_LASPLAGAS)
	{
		indexLasPlagasActuel = 0;
	}

}


/// <summary>
/// Méthode permettant de gérer les tirs du joueur.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::Fire()
{
	static int indexProjectileActuel = 0;


	if (tabProjectiles[indexProjectileActuel].activated != true)
	{
		tabProjectiles[indexProjectileActuel].activated = true;
		tabProjectiles[indexProjectileActuel].SetPositionX(player.GetPositionX());
		tabProjectiles[indexProjectileActuel].SetPositionY(player.GetPositionY());

		tabProjectiles[indexProjectileActuel].SetAngle(angleAiming * M_PI / 180);
	}

	indexProjectileActuel++;

	if (indexProjectileActuel == MAX_PROJECTILE)
	{
		indexProjectileActuel = 0;
	}
		
}

/// <summary>
/// Méthode permettant de gérer les collisions entre le projectile et le Las Plagas. (Cette méthode ne fonctionne pas)
/// </summary>
/// <param name="projectile">Le projectile entré en collision avec le Las Plagas.</param>
/// <returns>Aucune valeur de retour</returns>
bool Game::TestCollisionSpheresBetweenProjectileAndLasPlagas(Projectile projectile)
{
	CircleShape hitBoxProjectile(projectile.GetTexture().getSize().x / 2);
	float projectileRadius = hitBoxProjectile.getRadius();

	for (int i = 0; i < MAX_LASPLAGAS; i++)
	{
		CircleShape hitBoxLasPlagas(tabLasPlagas[i].GetTexture().getSize().x/2);
		float lasPlagasRadius = hitBoxLasPlagas.getRadius();

		float distance = sqrtf(pow(tabLasPlagas[i].GetPositionX() - projectile.GetPositionX(), 2.0f) + pow(tabLasPlagas[i].GetPositionY() - projectile.GetPositionY(), 2.0f));

		if (distance < lasPlagasRadius + projectileRadius)
		{
			return true;
			tabLasPlagas[i].activated = false;
		}
	}
	return false;
}

/// <summary>
/// Méthode permettant de gérer les collisions entre le projectile et le Las Plagas.
/// </summary>
/// <param name="projectile">Le projectile entré en collision avec le joueur.</param>
/// <returns>Aucune valeur de retour</returns>
bool Game::TestCollisionSpheresBetweenPlayerAndLasPlagas(LasPlagas lasPlagas)
{

	CircleShape hitBoxLasPlagas(lasPlagas.GetTexture().getSize().x / 2);
	float lasPlagasRadius = hitBoxLasPlagas.getRadius();

	CircleShape hitBoxPlayer(player.GetTexture().getSize().x / 2);
	float playerRadius = hitBoxPlayer.getRadius();

	float distance = sqrtf(pow(lasPlagas.GetPositionX() - player.GetPositionX(), 2.0f) + pow(lasPlagas.GetPositionY() - player.GetPositionY(), 2.0f));

	if (distance < lasPlagasRadius + playerRadius)
	{
		return true;
	}

	return false;
}


/// <summary>
/// Méthode permettant de gérer l'affichage du jeu.
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Game::Draw()
{
	mainWin.draw(backgroundSprite);

	mainWin.draw(player.GetSprite());

	for (int i = 0; i < MAX_PROJECTILE; i++)
	{
		if (tabProjectiles[i].activated == true)
		{
			mainWin.draw(tabProjectiles[i].GetSprite());
		}
	}

	for (int i = 0; i < 3; i++)
	{
		if (tabLasPlagas[i].activated == true)
		{
			mainWin.draw(tabLasPlagas[i].GetSprite());
		}
	}
	showHp.setString("HP remaining:" + std::to_string(player.GetHp()));
	mainWin.draw(showHp);

	showHp.setString("Score :" + std::to_string(score));
	mainWin.draw(showScore);
	
}