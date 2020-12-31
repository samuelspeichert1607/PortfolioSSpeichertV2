#include "Block.h"

Texture Block::blockT;

Block::Block()
{


}

Block::~Block()
{

}

void Block::Init(const float posX, const float posY, RenderWindow* const renderWindow)
{
	setPosition(posX, posY);
}


bool Block::chargerTextures(const char texturePath[]) // Content\\Tiles\\BlockA0.png
{
	if (!blockT.loadFromFile(texturePath))
	{
		return false;
	}

	blockT.setSmooth(false);
	return true;
}

void Block::ajustementsVisuels()
{
	setTexture(blockT);
	//setOrigin(blockT.getSize().x / 2, blockT.getSize().y / 2);
	//for (size_t i = 0; i < NBR_NIVEAUX; i++)
	//{
	//	intRectsImmobile[i] = new IntRect[NBR_ANIMS_IMMOBILE];
	//	intRectsImmobile[i] = new IntRect[NBR_ANIMS_MOUVEMENT];

	//	int largeur = texture.getSize().x / NBR_ANIMS;
	//	int hauteur = texture.getSize().y / NBR_NIVEAUX;

	//	for (size_t j = 0; i < NBR_ANIMS_IMMOBILE; i++)
	//	{
	//		intRectsImmobile[i][j].left = largeur * j;
	//		intRectsImmobile[i][j].top = hauteur * j;
	//		intRectsImmobile[i][j].width = largeur;
	//		intRectsImmobile[i][j].height = hauteur;
	//	}

	//	for (size_t j = 0; i < NBR_ANIMS_MOUVEMENT; i++)
	//	{
	//		intRectsImmobile[i][j].left = largeur * j;
	//		intRectsImmobile[i][j].top = hauteur * j;
	//		intRectsImmobile[i][j].width = largeur;
	//		intRectsImmobile[i][j].height = hauteur;
	//	}
	//}

	//setTextureRect(intRectsImmobile[cadran][0]);
	//setOrigin(intRectsImmobile[0][0].height / 2, intRectsImmobile[0][0].width / 2);
}