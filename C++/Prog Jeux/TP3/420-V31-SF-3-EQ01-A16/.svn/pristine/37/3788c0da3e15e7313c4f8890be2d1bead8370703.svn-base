#pragma once
#include <SFML/Graphics.hpp>
#include "Sujet.h"
#include "Constantes.h"
using namespace sf;

class Spell : public Sprite, public Sujet
{
public:
	Spell(SpellType spellType, int ID);
	~Spell();
	bool ajustementsVisuels();
	int getImpactRadius() const;
	int getCout() const;
	int getEffectTime() const;
	int getElaspedTime() const;
	int getID() const;
	SpellType getType() const;
	bool collisionClick(float posX, float posY);
	void addTime();

	bool operator < (const Spell& spell) const;
	bool operator > (const Spell& spell) const;
	bool operator >= (const Spell& spell) const;
	bool operator <= (const Spell& spell) const;
	bool operator == (const Spell& spell) const;
	bool operator != (const Spell& spell) const;
private:
	int impactRadius = 0;
	int cout = 0;
	int effectTime = 0;
	int elaspedTime = 0;
	SpellType spellType = SpellType::Eclair;
	int ID = 0;
	Texture texture;
	Clock createdTime;
};