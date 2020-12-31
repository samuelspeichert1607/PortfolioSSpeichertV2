#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;

class Projectile : public RectangleShape
{
	public:
		Projectile(int speed, int attackPoints, int range, float angle, bool isAlly);
		~Projectile();
		bool update();
		int getSpeed() const;
		int getAttackPoints() const;
		int getRange() const;
		float getAngle() const;
		bool getIsAlly() const;
		float getNbUpdates() const;

	private:
		int speed = 0;
		int attackPoints = 0;
		int range = 0;
		float angle = 0;
		bool isAlly = false;
		int nbUpdates = 0;//distance parcourue par le projetile
		int nbUpdatesAFaire = 0;
};