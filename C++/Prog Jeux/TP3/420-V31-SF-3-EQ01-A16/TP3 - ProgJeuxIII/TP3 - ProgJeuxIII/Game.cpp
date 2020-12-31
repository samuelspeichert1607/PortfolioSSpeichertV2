#include "Game.h"
//Sam
Game::Game()
{
	//G�n�ration de la fen�tre.
	mainWin.create(VideoMode(LARGEUR_ECRAN, HAUTEUR_ECRAN, 32), "Projet SFML C++");

	//Synchonisation coordonn�e � l'�cran!  Normalement 60 frames par secondes
	//� faire absolument
	mainWin.setFramerateLimit(60);

	//Synchonisation coordonn�e � l'�cran!
	mainWin.setVerticalSyncEnabled(true);
}
//Alex et Sam
Game::~Game()
{
	int nbElements = minionList.getNbElements();
	for (int i = 0; i < nbElements; i++)
	{
		Minion* temporaire = minionList.at(0);
		minionList.retirer(minionList.at(0));
		delete temporaire;
	}
	int nbElementsSpells = spellList.getNbElements();
	for (int i = 0; i < nbElementsSpells; i++)
	{
		Spell* temporaire = spellList.at(0);
		spellList.retirer(spellList.at(0));
		delete temporaire;
	}
	Spell* temporaire = ordreDesSpells.Pop();
	while (temporaire->getID() > -1)
	{
		delete temporaire;
		temporaire = ordreDesSpells.Pop();
	}
	listeProjectile.clear();
}
//Sam
int Game::testTest()
{
	return 0;
}
//Sam
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
		else if (stateGame == STATE_GAME::play)
		{
		//////////////////////////////////////////////////////////////////////////
		// 1 - Gestion des entr�es
		//////////////////////////////////////////////////////////////////////////

		getInputs();

		//////////////////////////////////////////////////////////////////////////
		// 2 - Gestion de la logique 
		//////////////////////////////////////////////////////////////////////////

		update();

		//////////////////////////////////////////////////////////////////////////
		// 3 - Gestion de l'affichage 
		//////////////////////////////////////////////////////////////////////////
		//Toujours important d'effacer l'�cran pr�c�dent

		mainWin.clear();
		draw();
		mainWin.display();

		}
	}
	return EXIT_SUCCESS;
}
//Sam
bool Game::init()
{
	nbRessources = 18;
	nbRessourcesEnnemie = 18;

	minionList.ajouter(SpawnerMinion::CreateEnnemy(_Tour, 64, HAUTEUR_ECRAN / 2, true));
	minionList.ajouter(SpawnerMinion::CreateEnnemy(_Tour, LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 4, false));
	minionList.ajouter(SpawnerMinion::CreateEnnemy(_Tour, LARGEUR_ECRAN - 64, HAUTEUR_ECRAN / 2, false));
	minionList.ajouter(SpawnerMinion::CreateEnnemy(_Tour, LARGEUR_ECRAN - 64, HAUTEUR_ECRAN * 3 / 4, false));
	minionList.at(0)->ajustementsVisuels();
	minionList.at(1)->ajustementsVisuels();
	minionList.at(2)->ajustementsVisuels();
	minionList.at(3)->ajustementsVisuels();

	allyTower = (Tour*)minionList.at(0);

	//l'affichage de notre s�lecteur
	selecteur.setFillColor(Color::Transparent);
	selecteur.setOutlineColor(Color::White);
	selecteur.setOutlineThickness(1);


	if (!backgroundT.loadFromFile("..\\..\\Sprites\\Terrain.png"))
	{
		return false;
	}
	//Sam
	if (!titleScreenTexture.loadFromFile("..\\..\\Sprites\\titlescreen.png"))
	{
		return false;
	}
	titleScreenSprite.setTexture(titleScreenTexture);
	titleScreenSprite.setPosition(0, 0);
	
	if (!font.loadFromFile("..\\..\\Sprites\\IHateComicSans.ttf"))
	{
		return false;
	}

	background.setTexture(backgroundT);
	background.setPosition(0, 0);


	commandes.setString("Q : Archer\tW : Barbare\tE : Cavalier\tR : Guerrier leger\tT : Guerrier Lourd\tY : Lancier\tU : Magi-chien\n0 : Eclair\t1 : Boule de feu\t2 : Meteore\t3 : Gel\t4 : Fatigue\t5 : Vigueur\t6 : Poison\t7 : Guide\t8 : Soin\t9 : Affutage\nAppuyez sur SPACE pour jeter un sort et faite un clic droit dessus pour l'activer.");

	commandes.setFont(font);
	commandes.setCharacterSize(18);
	commandes.setColor(Color::White);
	commandes.setPosition(32, 10);
	textRessource.setString("Nombre de ressources : " + to_string(nbRessources));
	textRessource.setFont(font);
	textRessource.setCharacterSize(18);
	textRessource.setColor(Color::Yellow);
	textRessource.setPosition(32, 73);
	textRessourceEnnemie.setString("Nombre de ressources de l'ennmie : " + to_string(nbRessourcesEnnemie));
	textRessourceEnnemie.setFont(font);
	textRessourceEnnemie.setCharacterSize(18);
	textRessourceEnnemie.setColor(Color::White);
	textRessourceEnnemie.setPosition(LARGEUR_ECRAN - textRessourceEnnemie.getString().getSize()*textRessourceEnnemie.getCharacterSize() - 32, 73);

	return true;
}
//Sam et Alex
void Game::getInputs()
{
	//Pas mal d'�v�nements � g�rer
	while (mainWin.pollEvent(event))
	{
		

			//Tous les �v�nement de press de souris
			if (Keyboard::isKeyPressed(sf::Keyboard::Escape))
			{
				mainWin.close();
			}
			else if (event.type == Event::MouseButtonPressed)
			{
				//Si c'est le bouton gauche
				if (event.mouseButton.button == Mouse::Left)
				{
					//On arr�te et on vide le dernier mist, la s�lection va en cr�er un nouveau
					//groupeDeMinions.arreter();
					groupeDeMinions.vider();

					//Rectangle de s�lection: d�but du traitement
					positionSelecteur = Vector2f(Mouse::getPosition(mainWin).x, Mouse::getPosition(mainWin).y);


				}

				//Avec le click droit, s�lection de la soul � d�placer.
				else if (event.mouseButton.button == Mouse::Right)
				{

					Vector2f v(Mouse::getPosition(mainWin).x, Mouse::getPosition(mainWin).y);
					groupeDeMinions.AjouterDestination(v);

					//activerObjetJeu(sf::Mouse::getPosition(mainWin));
				}
			}

			//Avec le release de la souris on cr�e le mist.
			else if (event.type == Event::MouseButtonReleased && event.mouseButton.button == Mouse::Left)
			{
				//On taille un rectangle de collision
				IntRect containter;
				containter.left = std::min(positionSelecteur.x, tailleSelecteur.x);
				containter.top = std::min(positionSelecteur.y, tailleSelecteur.y);
				containter.width = abs(positionSelecteur.x - tailleSelecteur.x);
				containter.height = abs(positionSelecteur.y - tailleSelecteur.y);

				//Si la position de la soul est dans le rectangle, on l'ajoute au mist.
				for (int i = 0; i < minionList.getNbElements(); i++)
				{
					if (containter.contains(minionList.at(i)->getPosition().x, minionList.at(i)->getPosition().y) && minionList.at(i)->getIsAlly())
					{
						groupeDeMinions.ajouter(minionList.at(i));
					}
				}

				//On "kill" le s�lecteur
				positionSelecteur = Vector2f(-1, -1);
				tailleSelecteur = Vector2f(-1, -1);
			}


			//En dernier; c'est de loin le plus rare
			else if (event.type == Event::Closed)
			{
				mainWin.close();
			}

		}
		//Si le bouton gauche reste enfonc� 
		if (Mouse::isButtonPressed(sf::Mouse::Left) && positionSelecteur.x > -1)
		{
			tailleSelecteur = Vector2f(Mouse::getPosition(mainWin).x, Mouse::getPosition(mainWin).y);
		}





		//Interface du joueur pour cr�er des minions
		if (Keyboard::isKeyPressed(sf::Keyboard::Q) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_Archer));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::W) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_Barbare));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::E) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_Cavalier));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::R) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_GuerrierLeger));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::T) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_GuerrierLourd));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::Y) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_Lancier));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(sf::Keyboard::U) && allyClock.getElapsedTime().asMilliseconds() >= 200)
		{
			minionQueueAlly.Ajouter(allyTower->spawn(MinionType::_Magichien));
			Minion * nouveauMinion = minionQueueAlly.Retirer();
			if (nbRessources >= nouveauMinion->getCout())
			{
				if (nouveauMinion != NULL)
				{
					nouveauMinion->ajustementsVisuels();
					minionList.ajouter(nouveauMinion);
				}
				nbRessources -= nouveauMinion->getCout();
				allyClock.restart();
			}
			else
			{
				delete nouveauMinion;
			}
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Space))
		{
			if (!testerPrescenceSpell(sf::Mouse::getPosition(mainWin)) && spellClock.getElapsedTime().asMilliseconds() >= 200)
			{
				spellClock.restart();
				ajouterSpell(sf::Mouse::getPosition(mainWin));
			}
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num0) || Keyboard::isKeyPressed(Keyboard::Key::Numpad0))
		{
			choixSpell = Eclair;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num1) || Keyboard::isKeyPressed(Keyboard::Key::Numpad1))
		{
			choixSpell = BouleDeFeu;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num2) || Keyboard::isKeyPressed(Keyboard::Key::Numpad2))
		{
			choixSpell = Meteore;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num3) || Keyboard::isKeyPressed(Keyboard::Key::Numpad3))
		{
			choixSpell = Gel;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num4) || Keyboard::isKeyPressed(Keyboard::Key::Numpad4))
		{
			choixSpell = Fatigue;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num5) || Keyboard::isKeyPressed(Keyboard::Key::Numpad5))
		{
			choixSpell = Vigueur;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num6) || Keyboard::isKeyPressed(Keyboard::Key::Numpad6))
		{
			choixSpell = Poison;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num7) || Keyboard::isKeyPressed(Keyboard::Key::Numpad7))
		{
			choixSpell = Guide;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num8) || Keyboard::isKeyPressed(Keyboard::Key::Numpad8))
		{
			choixSpell = Soin;
		}
		else if (Keyboard::isKeyPressed(Keyboard::Key::Num9) || Keyboard::isKeyPressed(Keyboard::Key::Numpad9))
		{
			choixSpell = Affutage;
		}
	

	
}

//Sam et Alex
void Game::update()
{
	if (ressourceClock.getElapsedTime().asSeconds() >= 1)
	{
		ressourceClock.restart();
		nbRessources += 2;
		nbRessourcesEnnemie += 2;
	}

	if (ennemyClock.getElapsedTime().asSeconds() >= rand() % 7 + 1)
	{
		int noEnnemy = rand() % 7;
		minionQueueEnnemy.Ajouter(((Tour*)(minionList.at((rand() % 3) + 1)))->spawn((MinionType)noEnnemy));
		Minion * nouveauMinion = minionQueueEnnemy.Retirer();
		if (nbRessourcesEnnemie >= nouveauMinion->getCout())
		{
			if (nouveauMinion != NULL)
			{
				nouveauMinion->ajustementsVisuels();
				minionList.ajouter(nouveauMinion);
			}
			nbRessourcesEnnemie -= nouveauMinion->getCout();
			ennemyClock.restart();
		}
		else
		{
			delete nouveauMinion;
		}
	}

	for (int i = 4; i < minionList.getNbElements(); i++)
	{
		if (minionList.at(i)->lookForEnnemies(&minionList))
		{
			Projectile *temporaire = minionList.at(i)->attack();
			if (temporaire != NULL)
			{
				Projectile proj = *temporaire;
				listeProjectile.push_back(proj);
			}
			delete temporaire;
		}
		else
		{
			minionList.at(i)->bouger(&minionList);
		}
	}
	for (int i = 0; i < listeProjectile.size(); i++)
	{
		for (int j = 0; j < minionList.getNbElements(); j++)
		{
			if (minionList.at(j)->getIsAlly() != listeProjectile.at(i).getIsAlly() && !(minionList.at(j)->getIsDead())
				&& minionList.at(j)->getGlobalBounds().intersects(listeProjectile.at(i).getGlobalBounds()))
			{
				if (minionList.at(j)->gotHit(listeProjectile.at(i).getAttackPoints()))
				{
					if (minionList.at(j)->getMinionType() == _Tour)
					{
						minionList.at(j)->die();
						if ((minionList.at(0)->getIsDead() == true) || (minionList.at(1)->getIsDead() == true && minionList.at(2)->getIsDead() == true && minionList.at(3)->getIsDead() == true))
						{
							timeBeforeEnd.restart();
						}
					}
					else
					{
						Minion* temporaire = minionList.at(j);
						minionList.retirer(minionList.at(j));
						for (int k = 0; k < spellList.getNbElements(); k++)
						{
							spellList.at(k)->retirerObservateur(temporaire);
						}
						delete temporaire;
						j--;
					}
				}
				listeProjectile.erase(listeProjectile.begin() + i);
				i--;
				if (listeProjectile.size() == 0 || i < 0)
				{
					break;
				}
			}
		}
		if (i >= 0 && listeProjectile.at(i).update())
		{
			listeProjectile.erase(listeProjectile.begin() + i);
			i--;
		}
	}
	//Calculs pour l'affichage du joli rectangle
	selecteur.setPosition(std::min(positionSelecteur.x, tailleSelecteur.x), std::min(positionSelecteur.y, tailleSelecteur.y));
	selecteur.setSize(Vector2f(abs(positionSelecteur.x - tailleSelecteur.x), abs(positionSelecteur.y - tailleSelecteur.y)));


	Spell* currentSpell = ordreDesSpells.Pop();
	if (currentSpell->getID() > -1)
	{
		currentSpell->addTime();
		vector<Observateur*> observateursMort = currentSpell->notifierTousLesObservateurs();
		for (int i = 0; i < observateursMort.size(); i++)
		{
			Minion* temporaire = (Minion*)observateursMort.at(i);
			if (temporaire->getMinionType() == _Tour)
			{
				temporaire->die();
				if ((minionList.at(0)->getIsDead() == true) || (minionList.at(1)->getIsDead() == true && minionList.at(2)->getIsDead() == true && minionList.at(3)->getIsDead() == true))
				{
					timeBeforeEnd.restart();
				}
			}
			else
			{
				minionList.retirer(temporaire);
				for (int k = 0; k < spellList.getNbElements(); k++)
				{
					spellList.at(k)->retirerObservateur(temporaire);
				}
				delete temporaire;
			}
			observateursMort.erase(observateursMort.begin() + i);
			i--;
		}
		if (currentSpell->getElaspedTime() >= currentSpell->getEffectTime())
		{
			spellList.retirer(currentSpell);
			delete currentSpell;
		}
		else
		{
			ordreDesSpells.Push(currentSpell);
		}
	}
}

//Sam
bool Game::testerPrescenceSpell(Vector2i& position)
{
	for (int i = 0; i < spellList.getNbElements(); i++)
	{
		if (spellList.at(i) != nullptr && spellList.at(i)->collisionClick(position.x, position.y))
		{
			return true;
		}
	}

	return false;
}
//Alex
void Game::ajouterSpell(Vector2i position)
{
	static int nextIdSpell = 0;
	Spell* nouveau = new Spell(choixSpell, nextIdSpell);
	if (nbRessources >= nouveau->getCout())
	{
		nouveau->ajustementsVisuels();
		nouveau->setPosition((Vector2f)position);
		//ajout observateur
		for (int i = 0; i < minionList.getNbElements(); i++)
		{
			nouveau->ajouterObservateur((Observateur*)minionList.at(i));
		}
		ordreDesSpells.Push(nouveau);
		spellList.ajouter(nouveau);
		nbRessources -= nouveau->getCout();
		nextIdSpell++;
	}
	else
	{
		delete nouveau;
	}
}
//Sam
/*
void Game::activerObjetJeu(Vector2i position)
{
for (int i = 0; i < spellList.getNbElements(); i++)
{
if (spellList.at(i) != nullptr)
{
if (spellList.at(i)->collisionClick(position))
{
spellList.at(i)->activer();
}
}
}
}*/
//Sam et Alex
//Fonction qui s'occupe de l'affichage du jeu.
void Game::draw()
{
	mainWin.draw(background);

	for (int i = 0; i < minionList.getNbElements(); i++)
	{
		if (minionList.at(i)->getMinionType() == _Tour && minionList.at(i)->getIsDead())
		{
		}
		else
		{
			mainWin.draw(*(minionList.at(i)));

			RectangleShape maxLifeBar;
			maxLifeBar.setSize(sf::Vector2f(50, 5));
			maxLifeBar.setOrigin(maxLifeBar.getSize().x / 2, maxLifeBar.getSize().y / 2);
			maxLifeBar.setPosition(minionList.at(i)->getPosition().x, minionList.at(i)->getPosition().y - minionList.at(i)->getOrigin().y - 5);
			maxLifeBar.setFillColor(Color::Black);
			mainWin.draw(maxLifeBar);

			RectangleShape lifeBar;
			lifeBar.setSize(sf::Vector2f((50 * minionList.at(i)->getHealth()) / minionList.at(i)->getMaxHealth(), 5));
			lifeBar.setOrigin(lifeBar.getSize().x / 2, lifeBar.getSize().y / 2);
			lifeBar.setPosition(minionList.at(i)->getPosition().x, minionList.at(i)->getPosition().y - minionList.at(i)->getOrigin().y - 5);
			lifeBar.setFillColor(Color::Green);
			lifeBar.setOutlineColor(Color::Black);
			mainWin.draw(lifeBar);
		}
	}

	for (int i = 0; i < listeProjectile.size(); i++)
	{
		mainWin.draw(listeProjectile.at(i));
	}
	mainWin.draw(commandes);
	textRessource.setString("Nombre de ressources : " + to_string(nbRessources));
	mainWin.draw(textRessource);
	textRessourceEnnemie.setString("Nombre de ressources : " + to_string(nbRessourcesEnnemie));
	textRessourceEnnemie.setPosition(LARGEUR_ECRAN - textRessourceEnnemie.getString().getSize()*textRessourceEnnemie.getCharacterSize() - 32, 73);
	mainWin.draw(textRessourceEnnemie);
	mainWin.draw(selecteur);


	if (minionList.at(0)->getIsDead() == true)
	{
		issueDeLaPartie.setString("u is very dead");
		issueDeLaPartie.setFont(font);
		issueDeLaPartie.setCharacterSize(72);
		issueDeLaPartie.setColor(Color::Red);
		FloatRect textRect = issueDeLaPartie.getLocalBounds();
		issueDeLaPartie.setOrigin(textRect.left + textRect.width / 2.0f, textRect.top + textRect.height / 2.0f);
		issueDeLaPartie.setPosition(LARGEUR_ECRAN / 2, HAUTEUR_ECRAN / 2);
		mainWin.draw(issueDeLaPartie);

		if (timeBeforeEnd.getElapsedTime().asSeconds() > 2)
		{
			mainWin.close();
		}
	}
	else if (minionList.at(1)->getIsDead() == true && minionList.at(2)->getIsDead() == true && minionList.at(3)->getIsDead() == true)
	{
		issueDeLaPartie.setString("YOU WON!");
		issueDeLaPartie.setFont(font);
		issueDeLaPartie.setCharacterSize(72);
		issueDeLaPartie.setColor(Color::Blue);
		FloatRect textRect = issueDeLaPartie.getLocalBounds();
		issueDeLaPartie.setOrigin(textRect.left + textRect.width / 2.0f, textRect.top + textRect.height / 2.0f);
		issueDeLaPartie.setPosition(LARGEUR_ECRAN / 2, HAUTEUR_ECRAN / 2);
		mainWin.draw(issueDeLaPartie);

		if (timeBeforeEnd.getElapsedTime().asSeconds() > 2)
		{
			mainWin.close();
		}
	}

	for (int i = 0; i < spellList.getNbElements(); i++)
	{
		if (spellList.at(i) != nullptr)
		{
			mainWin.draw(*(spellList.at(i)));
		}
	}


}

void Game::runTitle()
{
	while (mainWin.pollEvent(event))
	{
		if (event.type == Event::Closed)
		{
			mainWin.close();
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

