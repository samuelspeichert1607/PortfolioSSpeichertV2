#include "Spell.h"

//Alex
Spell::Spell(SpellType spellType, int ID) :ID(ID), spellType(spellType)
{
	createdTime.restart();
	if (spellType == Eclair)
	{
		cout = 2;
		effectTime = 0;
		impactRadius = 25;
	}
	else if (spellType == BouleDeFeu)
	{
		cout = 4;
		effectTime = 0;
		impactRadius = 25;
	}
	else if (spellType == Meteore)
	{
		cout = 6;
		effectTime = 0;
		impactRadius = 35;
	}
	else if (spellType == Gel)
	{
		cout = 4;
		effectTime = 3;
		impactRadius = 30;
	}
	else if (spellType == Fatigue)
	{
		cout = 2;
		effectTime = 4;
		impactRadius = 40;
	}
	else if (spellType == Vigueur)
	{
		cout = 2;
		effectTime = 4;
		impactRadius = 40;
	}
	else if (spellType == Clonage)
	{
		cout = 4;
		effectTime = 5;
		impactRadius = 1;
	}
	else if (spellType == Poison)
	{
		cout = 3;
		effectTime = 10;
		impactRadius = 40;
	}
	else if (spellType == Guide)
	{
		cout = 2;
		effectTime = 7;
		impactRadius = 30;
	}
	else if (spellType == Soin)
	{
		cout = 3;
		effectTime = 0;
		impactRadius = 30;
	}
	else if (spellType == Affutage)
	{
		cout = 4;
		effectTime = 7;
		impactRadius = LARGEUR_ECRAN;
	}
	else if (spellType == Divination)
	{
		cout = 1;
		effectTime = 0;
		impactRadius = LARGEUR_ECRAN;
	}
	elaspedTime = 0;
}
//Sam
Spell::~Spell()
{
	observateurs.clear();
}
//alex
bool Spell::ajustementsVisuels()
{
	IntRect intRect = IntRect();
	if (spellType == Eclair)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Yellow);
	}
	else if (spellType == BouleDeFeu)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color(255, 128, 0));
	}
	else if (spellType == Meteore)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Black);
	}
	else if (spellType == Gel)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Cyan);
	}
	else if (spellType == Fatigue)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color(0, 51, 0));
	}
	else if (spellType == Vigueur)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Green);
	}
	else if (spellType == Clonage)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color(255, 0, 127));
	}
	else if (spellType == Poison)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color(102, 0, 204));
	}
	else if (spellType == Guide)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Magenta);
	}
	else if (spellType == Soin)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Red);
	}
	else if (spellType == Affutage)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::Blue);
	}
	else if (spellType == Divination)
	{
		if (!texture.loadFromFile("..\\..\\Sprites\\Radiation.png"))
		{
			return false;
		}
		setColor(Color::White);
	}
	setTexture(texture);
	setOrigin(getGlobalBounds().width / 2, getGlobalBounds().height / 2);
	return true;
}
//Sam
int Spell::getImpactRadius() const
{
	return impactRadius;
}
//Sam
int Spell::getCout() const
{
	return cout;
}
//Sam
SpellType Spell::getType() const
{
	return spellType;
}
//Alex
int Spell::getElaspedTime() const
{
	return elaspedTime;
}
//Alex
int Spell::getEffectTime() const
{
	return effectTime;
}
//Sam
int Spell::getID() const
{
	if (this == NULL)
	{
		return -1;
	}
	else
	{
		return ID;
	}
}


//Alex
bool Spell::collisionClick(float posX, float posY)
{
	return getGlobalBounds().intersects(FloatRect(posX, posY, 1, 1));
}

//Alex
void Spell::addTime()
{
	elaspedTime = createdTime.getElapsedTime().asSeconds();
}

///Surcharges d'opérateurs
//Alex
bool Spell::operator < (const Spell& Spell) const
{
	return this->ID < Spell.ID;
}
//Alex
bool Spell::operator >(const Spell& Spell) const
{
	return this->ID > Spell.ID;
}
//Alex
bool Spell::operator >= (const Spell& Spell) const
{
	return this->ID >= Spell.ID;
}
//Alex
bool Spell::operator <= (const Spell& Spell) const
{
	return this->ID <= Spell.ID;
}
//Alex
bool Spell::operator == (const Spell& Spell) const
{
	return this->ID == Spell.ID;
}
//Alex
bool Spell::operator != (const Spell& Spell) const
{
	return this->ID != Spell.ID;
}