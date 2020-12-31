#define _USE_MATH_DEFINES
#include "Projectile.h"

//alex
Projectile::Projectile(int _speed, int _attackPoints, int _range, float _angle, bool _isAlly) : speed(_speed), attackPoints(_attackPoints), range(_range),
angle(_angle), isAlly(_isAlly)
{
	setSize(Vector2f(5/*attackPoints*/, 5));
	setOrigin(getSize().x / 2, getSize().y / 2);
	setRotation(360 * angle / (2 * M_PI));
	if (isAlly)
	{
		setFillColor(Color::Blue);
	}
	else
	{
		setFillColor(Color::Red);
	}
	nbUpdatesAFaire = abs(range / speed);
	if ((float)((float)range / (float)speed) > abs(range / speed))
	{
		nbUpdatesAFaire++;
	}
	nbUpdates = 0;
}
//alex
Projectile::~Projectile()
{

}
/// <summary>
/// déplace le projectile et s'il est allé trops loin on retourne vrai pour dire qu'il ne devrais plus exister
/// </summary>
/// <returns>vrai si le projevtile est allé au bout de sa portée et ne devrais plus exister, sionon faux</returns>
//Alex
bool Projectile::update()
{
	bool isOk = true;
	if (nbUpdates > nbUpdatesAFaire)
	{
		isOk = true;
	}
	else
	{
		isOk = false;
	}
	move(cos(angle) * speed, sin(angle) * speed);
	nbUpdates++;
	return isOk;
}
//alex
int Projectile::getSpeed() const
{
	return speed;
}
//alex
int Projectile::getAttackPoints() const
{
	return attackPoints;
}
//alex
int Projectile::getRange() const
{
	return range;
}
//alex
float Projectile::getAngle() const
{
	return angle;
}
bool Projectile::getIsAlly() const
{
	return isAlly;
}
//alex
float Projectile::getNbUpdates() const
{
	return nbUpdates;
}
