#pragma once
#include <SFML/Graphics.hpp>
#include <math.h>
#include "Constantes.h"
#include "Observateur.h"
#include "IComponant.h"
#include "Projectile.h"
#include "Pile.h"
#include "Spell.h"

using namespace sf;


class Minion : public Observateur, public Sprite, public IComponant
{
	public:
		Minion();

		virtual ~Minion();

		bool ajustementsVisuels();

		bool gotHit(int damage);

		int getHealth() const;

		int getMaxHealth() const;

		bool cliquer(float posX, float PosY);
		
		virtual void bouger(Liste<Minion>* minionList);

		virtual void arreter();

		virtual Projectile * attack();

		Vector2f* getTarget() const;

		int getCout() const;

		int getRange() const;

		MinionType getMinionType() const;

		int getAttackPoints() const;

		int getSpeed() const;

		int getIsAlly() const;

		void AjouterDestination(const Vector2f& positionCible);

		bool getIsFacingLeft() const;

		bool notifier(Sujet * sujet);

		int getID() const;

		bool lookForEnnemies(Liste<Minion>* minionList);

		Pile<Vector2f> getOrdreAAttaquer() const;

		bool getIsDead() const;

		void die();

		virtual void attaqueSpeciale();

		bool operator < (const Minion& minion) const;
		bool operator > (const Minion& minion) const;
		bool operator >= (const Minion& minion) const;
		bool operator <= (const Minion& minion) const;
		bool operator == (const Minion& minion) const;
		bool operator != (const Minion& minion) const;

	protected :
		//Id pour le retrouver dans le minion
		int ID = -1;

		// Points de vie du minion.
		int health; 

		// Points de vie maximal du minion.
		int maxHealth; 

		//Type de minion.
		MinionType minionType;

		//Si le minion regarde vers la gauche.
		bool isFacingLeft = true;

		//Si le minion est alli� ou ennemi.
		bool isAlly = true;

		//Si le minion a d�ja fait son attaque sp�ciale.
		bool specialAttackAlreadyDone = false;

		//Vitesse du minion.
		int speed;

		//Port�e du minion.
		int range;

		//Points d'attaque du minion.
		int attackPoints;

		//Prix pour enr�ler le minion dans l'arm�e.
		int cout;

		//Pile utilis�e pour l'ordre des ennemis � attaquer.
		Pile<Vector2f> ordreAAttaquer;

		//Cible actuelle du minion
		Vector2f* target = nullptr;

		//Temps d'attaque
		Clock attackTime;

		//Temps pour l'attaque sp�ciale
		Clock specialAttackTime;

		//Texture du minion
		Texture texture;

		bool isModeManual = false;

		void backup();
		void cleanBackup();
		bool testCollisionMinion(Minion* autreMinion);
		void Minion::calculerAngle();

		Vector2f* positionBackup = nullptr;
		float angle;

		bool isDead = false;
		
		bool isParalized = false;
		
		bool isTired = false;

		bool isViorous = false;

		bool isPoisoned = false;

		bool isGuided = false;

		bool isAffuted = false;

};