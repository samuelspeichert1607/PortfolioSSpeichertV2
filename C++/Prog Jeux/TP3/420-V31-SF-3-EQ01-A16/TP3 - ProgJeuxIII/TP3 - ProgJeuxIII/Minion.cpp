#define _USE_MATH_DEFINES

#include "Minion.h"
//Sam
Minion::Minion()
{
	target = nullptr;
}

//Sam
Minion::~Minion()
{
	if (target != nullptr)
	{
		delete target;
	}

	Vector2f * temp = ordreAAttaquer.Pop();
	do
	{
		if (temp != nullptr)
		{
			delete temp;
		}
		temp = ordreAAttaquer.Pop();
	} while (temp != NULL);


}

//Alex et Sam
bool Minion::ajustementsVisuels()
{
	IntRect intRect = IntRect();
	if (minionType == _Tour)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Tour.png"))
		{
			return false;
		}
		setTexture(texture);
		setOrigin(getGlobalBounds().width / 2, getGlobalBounds().height / 2);
		return true;
	}
	else if (minionType == _Archer)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);
		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 0;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 2;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 0;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 0;
		}
	}
	else if (minionType == _Barbare)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 1;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 3;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 1;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 0;
		}
	}
	else if (minionType == _Cavalier)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 0;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 1;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 3;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 3;
		}
	}
	else if (minionType == _GuerrierLeger)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 2;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 1;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 2;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 2;
		}
	}
	else if (minionType == _GuerrierLourd)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 1;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 1;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 1;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 2;
		}
	}
	else if (minionType == _Lancier)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 2;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 0;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 3;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 2;
		}
	}
	else if (minionType == _Magichien)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\characters_size_ok.png"))
		{
			return false;
		}
		//Chargement du fichier de texture et placement dans le sprite.
		setTexture(texture);

		//Tableau de frames d�pendant du nombre d'animations
		if (isAlly)
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 0;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 3;
		}
		else
		{
			intRect.left = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL) * 4;
			intRect.top = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL) * 2;
		}
	}
	intRect.width = (texture.getSize().x / NB_SPRITES_DANS_TEXTURE_HORIZONTAL);
	intRect.height = (texture.getSize().y / NB_SPRITES_DANS_TEXTURE_VERTICAL);
	//Le rectangle de notre texture est le premier rectangle.
	setTextureRect(intRect);
	setOrigin(getGlobalBounds().width / 2, getGlobalBounds().height / 2);
	return true;
}
//alex
bool Minion::gotHit(int damage)
{
	health -= damage;
	if (health <= 0)
	{
		return true;
	}
	else
	{
		return false;
	}
}
//alex
MinionType Minion::getMinionType() const
{
	return minionType;
}

//Sam
int Minion::getHealth() const
{
	return health;
}

//Sam
int Minion::getMaxHealth() const
{
	return maxHealth;
}


/// <summary>
/// Permet seulement de v�rifier si une position x/y est � l'int�rieur du cercle
/// </summary>
/// <param name="posX">La position en x.</param>
/// <param name="PosY">La position en y.</param>
/// <returns></returns>
//Alex
bool Minion::cliquer(float posX, float posY)
{
	return getGlobalBounds().intersects(FloatRect(posX, posY, 1, 1));
}
/// <summary>
/// Permet de faire d�placer le minion, ainsi que de le faire arr�ter
/// </summary>
/// <param name="ListeMinion">Pointeur sur une liste de minion</param>
/// <returns></returns>
//Alex
void Minion::bouger(Liste<Minion>* minionList)
{
	if (!isParalized)
	{
		//Une destination nulle pas de mouvement
		if (target != nullptr)
		{
			//On sauvegarde la position pr�c�dente au cas
			positionBackup = new Vector2f(getPosition());
			float actualSpeed = speed;
			if (isTired)
			{
				actualSpeed--;
			}
			else if (isViorous)
			{
				actualSpeed++;
			}
			//On effectue le d�placement
			Transformable::move(cos(angle) * actualSpeed, sin(angle) * actualSpeed);

			//Vue la faible vitesse et pour garder les choses simples, s'il y a collision, on annule 
			//tout simplement le mouvement pour ce refresh de frame et on termine la m�thode imm�diatement
			for (int i = 0; i < minionList->getNbElements(); i++)
			{
				if (testCollisionMinion((minionList->at(i))))
				{
					backup();
					return;
				}
			}
			//� PARTIR D'ICI C'EST QU'IL N'Y A PAS EU DE COLLISION

			//Avec les float, il faut accepter un seuil de tol�rance, quand on arrive � distance de la "vitesse" vis�, on consid�re avoir atteint notre destination
			if ((abs(target->x - getPosition().x) <= actualSpeed) && (abs(target->y - getPosition().y) <= actualSpeed))
			{
				//On la fixe, pour �tre certain
				setPosition(*target);
				delete target;
				//On arr�te de bouger
				if (isModeManual == true)
				{
					arreter();
				}
				else if (isModeManual == false)
				{
					calculerAngle();
				}
			}
			//Dans tous les cas, pour �viter les leaks on lib�re le backup
			cleanBackup();
		}
		else if (isModeManual == true)
		{
			arreter();
		}
		else if (isModeManual == false)
		{
			calculerAngle();
		}
	}
	if (isPoisoned)
	{
		health--;
	}
}

/// <summary>
/// Permet de faire arr�ter le minion
/// </summary>
/// <returns></returns>
//Sam
void Minion::arreter()
{
	if (target != nullptr)
	{
		delete target;
		target = nullptr;
		isModeManual = true;
	}
}
/// <summary>
/// Permet de faire un backup � la position pr�c�dente du minion
/// </summary>
/// <returns></returns>
//Alex
void Minion::backup()
{
	if (positionBackup != nullptr)
	{
		setPosition(*positionBackup);
		delete positionBackup;
		positionBackup = nullptr;
	}
}
//Alex
void Minion::cleanBackup()
{
	if (positionBackup != nullptr)
	{
		delete positionBackup;
		positionBackup = nullptr;
	}
}

/// <summary>
/// Tester la collision des Minions.  Algo de collision des cercles que vous connaissez d�j�
/// </summary>
/// <param name="autreMinion">The autre Minion.</param>
/// <returns>vrai si nous sommes en colision avec un autre Minion, sionon faux</returns>
//Alex
bool Minion::testCollisionMinion(Minion* autreMinion)
{
	//S'assurer qu'on est pas avec null ou avec sois-m�me.  parce qu'on collisionne toujours avec soit-m�me...
	if (autreMinion == nullptr || autreMinion == this || autreMinion->getMinionType() == _Tour && autreMinion->getIsDead())
	{
		return false;
	}
	return false;
	//return getGlobalBounds().intersects(autreMinion->getGlobalBounds());
}

/// <summary>
/// donne un projectile qui va dans la direction du target du minion
/// </summary>
//Alex
Projectile * Minion::attack()
{
	if (isPoisoned)
	{
		health--;
	}
	if (attackTime.getElapsedTime().asMilliseconds() >= (50 - speed) * 2)
	{
		attackTime.restart();
		float angleProjectile = atanf(((target->y - getPosition().y)) / (target->x - getPosition().x));
		if (target->x < getPosition().x)
		{
			angleProjectile += M_PI;
		}
		float actualRange = range;
		if (isGuided)
		{
			actualRange += 30;
		}
		float actualAttackPoints = attackPoints;
		if (isAffuted)
		{
			actualAttackPoints *= 2;
		}
		Projectile* ARetourner = new Projectile(speed, actualAttackPoints, actualRange, angleProjectile, isAlly);
		ARetourner->setPosition(Vector2f(getPosition().x + cos(angleProjectile) * (getOrigin().x + ARetourner->getOrigin().x), getPosition().y + sin(angleProjectile) * (getOrigin().y + ARetourner->getOrigin().y)));
		return ARetourner;
	}
	else
	{
		return NULL;
	}
}
//alex
Vector2f* Minion::getTarget() const
{
	return target;
}
//Alex
int Minion::getCout() const
{
	if (this == NULL)
	{
		return INT_MAX;
	}
	else
	{
		return cout;
	}
}
//Sam
int Minion::getRange() const
{
	return range;
}
//Sam
int Minion::getAttackPoints() const
{
	return attackPoints;
}
//Sam
int Minion::getSpeed() const
{
	return speed;
}
/// <summary>
/// ajoute une destination donn�e
/// ne pas oublier d'appler calculerAngle lorque nous voulons utiliser le contenu de la liste
/// </summary>
/// <param name="autreMinion">la destination � ajouter</param>
//Alex
void Minion::AjouterDestination(const Vector2f& positionCible)
{
	if (target != nullptr)
	{
		ordreAAttaquer.Push(target);
	}
	ordreAAttaquer.Push(new Vector2f(positionCible));

	calculerAngle();
}

/// <summary>
/// met la prochaine destination et calcule l'angle d'attaque
/// </summary>
//Alex
void Minion::calculerAngle()
{
	target = ordreAAttaquer.Pop();
	if (target != NULL)
	{
		//On calcule l'angle pour la direction de la soul.
		angle = atanf(((target->y - getPosition().y)) / ((target->x - getPosition().x)));

		if (target->x < getPosition().x)
		{
			angle += M_PI;
		}
	}
}
/// <summary>
/// trouve si un ennemi est � port� d'attaque et le met comme sa cible
/// </summary>
/// <returns>vrai si nous avons trouv� un ennemie � port�, sionon faux</returns>
//Alex et Sam
bool Minion::lookForEnnemies(Liste<Minion>* minionList)
{
	float actualRange = range + speed;
	if (isGuided)
	{
		actualRange += 30;
	}
	/*float distanceAcceptable = actualRange * 12 / 50 /*+ attackPoints * 30 / 200*/;
	/*if (distanceAcceptable < 5)
	{
	distanceAcceptable = actualRange;
	}*/
	for (int i = 0; i < minionList->getNbElements(); i++)
	{
		if (minionList->at(i) != nullptr && minionList->at(i) != this &&
			(minionList->at(i)->getMinionType() != _Tour || !(minionList->at(i)->getIsDead())))
		{
			if (getPosition().x == minionList->at(i)->getPosition().x && getPosition().y == minionList->at(i)->getPosition().y)
			{
				if (minionList->at(i)->getIsAlly() != isAlly)
				{
					if (target == nullptr || (*target).x != minionList->at(i)->getPosition().x ||
						(*target).y != minionList->at(i)->getPosition().y)
					{
						AjouterDestination(Vector2f(minionList->at(i)->getPosition().x, minionList->at(i)->getPosition().y));
					}
					return true;
				}
			}
			else
			{
				float angleAttaque = atanf(((minionList->at(i)->getPosition().y - getPosition().y)) /
					(minionList->at(i)->getPosition().x - getPosition().x));
				if (minionList->at(i)->getPosition().x < getPosition().x)
				{
					angleAttaque += M_PI;
				}
				Vector2f positionAttackMinion = Vector2f(getPosition().x + cos(angleAttaque) * getOrigin().x,
					getPosition().y + sin(angleAttaque) * getOrigin().y);
				while (getGlobalBounds().intersects(FloatRect(positionAttackMinion.x, positionAttackMinion.y, 1, 1)))
				{
					positionAttackMinion.x += cos(angleAttaque);
					positionAttackMinion.y += sin(angleAttaque);
				}
				Vector2f positionAttackEnnemy = Vector2f(
					minionList->at(i)->getPosition().x + cos(angleAttaque + M_PI) * minionList->at(i)->getOrigin().x,
					minionList->at(i)->getPosition().y + sin(angleAttaque + M_PI) * minionList->at(i)->getOrigin().y);
				while (minionList->at(i)->getGlobalBounds().intersects(FloatRect(positionAttackEnnemy.x, positionAttackEnnemy.y, 1, 1)))
				{
					positionAttackEnnemy.x += cos(angleAttaque + M_PI);
					positionAttackEnnemy.y += sin(angleAttaque + M_PI);
				}
				if (minionList->at(i)->getIsAlly() != isAlly && abs(sqrt(pow(positionAttackEnnemy.x - positionAttackMinion.x, 2) +
					pow(positionAttackEnnemy.y - positionAttackMinion.y, 2))) <= abs(actualRange) || 
					minionList->at(i)->getIsAlly() != isAlly && minionList->at(i)->getGlobalBounds().intersects(FloatRect(
					positionAttackMinion.x, positionAttackMinion.y, 1, 1)))
				{
					//Probleme de vecteur gauche ici!!!! Le target sera � un moment donn� � NULL
					if (target == nullptr || (*target).x != minionList->at(i)->getPosition().x ||
						(*target).y != minionList->at(i)->getPosition().y)
					{
						AjouterDestination(Vector2f(minionList->at(i)->getPosition().x, minionList->at(i)->getPosition().y));
					}
					return true;
				}
			}
		}
	}
	return false;
}
//Sam
bool Minion::getIsFacingLeft() const
{
	return isFacingLeft;
}

//Alex
/// <summary>
/// Permet de faire r�agir les minions � une radiation selon leur distance
/// </summary>
/// <param name="ListeMinion">Pointeur sur une liste de minion</param>
/// <returns>vrai si mort, sinon faux</returns>
bool Minion::notifier(Sujet * sujet)
{
	if (typeid(*sujet) == typeid(Spell))
	{
		Spell *b = (Spell*)sujet;
		float distance = sqrt(pow(b->getPosition().x - getPosition().x, 2) + pow(b->getPosition().y - getPosition().y, 2)) - sqrt(pow(getOrigin().x, 2) + pow(getOrigin().y, 2));
		if (b->getType() == Eclair || b->getType() == BouleDeFeu || b->getType() == Meteore)
		{
			if (distance <= b->getImpactRadius())
			{
				int dommage = 0;
				if (b->getType() == Eclair)
				{
					dommage = 80;
				}
				else if (b->getType() == BouleDeFeu)
				{
					dommage = 250;
				}
				else if (b->getType() == Meteore)
				{
					dommage = 500;
				}
				if (minionType == _Tour)
				{
					dommage /= 2;
				}
				return gotHit(dommage);
			}
		}
		else if (b->getType() == Gel)
		{
			if (b->getElaspedTime() >= b->getEffectTime())
			{
				isParalized = false;
			}
			else if (distance <= b->getImpactRadius())
			{
				isParalized = true;
			}
		}
		else if (b->getType() == Fatigue)
		{
			if (b->getElaspedTime() >= b->getEffectTime())
			{
				isTired = false;
			}
			if (!isAlly && distance <= b->getImpactRadius())
			{
				isTired = true;
			}
		}
		else if (b->getType() == Vigueur)
		{
			if (b->getElaspedTime() >= b->getEffectTime())
			{
				isViorous = false;
			}
			if (isAlly && distance <= b->getImpactRadius())
			{
				isViorous = true;
			}
		}
		else if (b->getType() == Poison)
		{
			if (b->getElaspedTime() >= b->getEffectTime())
			{
				isPoisoned = false;
			}
			if (!isAlly && distance <= b->getImpactRadius())
			{
				isPoisoned = true;
			}
			return gotHit(0);
		}
		else if (b->getType() == Guide)
		{
			if (minionType == _Archer || minionType == _Lancier)
			{
				if (b->getElaspedTime() >= b->getEffectTime())
				{
					isGuided = false;
				}
				if (isAlly && distance <= b->getImpactRadius())
				{
					isGuided = true;
				}
			}
		}
		else if (b->getType() == Soin)
		{
			if (isAlly && distance <= b->getImpactRadius())
			{
				health = maxHealth;
			}
		}
		else if (b->getType() == Affutage)
		{
			if (minionType == _GuerrierLeger || minionType == _Barbare || minionType == _GuerrierLourd)
			{
				if (b->getElaspedTime() >= b->getEffectTime())
				{
					isAffuted = false;
				}
				if (isAlly)
				{
					isAffuted = true;
				}
			}
		}
		if (b->getType() != Gel)
		{
			isParalized = false;
		}
		if (b->getType() != Fatigue)
		{
			isTired = false;
		}
		if (b->getType() != Vigueur)
		{
			isViorous = false;
		}
		if (b->getType() != Guide)
		{
			isGuided = false;
		}
		if (b->getType() != Affutage)
		{
			isAffuted = false;
		}
	}
	return false;
}
//alex
int Minion::getID() const
{
	return ID;
}
//Alex
int Minion::getIsAlly() const
{
	return isAlly;
}

//Sam
Pile<Vector2f> Minion::getOrdreAAttaquer() const
{
	return ordreAAttaquer;
}

//Alex
bool Minion::getIsDead() const
{
	return isDead;
}
//Alex
void Minion::die()
{
	isDead = true;
}


void Minion::attaqueSpeciale()
{

}


////LES OPERATORS!!!

//sam
bool Minion::operator < (const Minion& minion) const
{
	return this->ID < minion.ID;
}
//sam
bool Minion::operator >(const Minion& minion) const
{
	return this->ID > minion.ID;
}
//sam
bool Minion::operator >= (const Minion& minion) const
{
	return this->ID >= minion.ID;
}
//sam
bool Minion::operator <= (const Minion& minion) const
{
	return this->ID <= minion.ID;
}
//sam
bool Minion::operator == (const Minion& minion) const
{
	return this->ID == minion.ID;
}
//sam
bool Minion::operator != (const Minion& minion) const
{
	return this->ID != minion.ID;
}