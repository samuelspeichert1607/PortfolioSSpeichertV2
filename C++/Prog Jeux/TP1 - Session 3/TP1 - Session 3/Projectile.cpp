#include "Projectile.h";

/// <summary>
/// Constructeur de l'ennemi, pour les test unitaires
/// </summary>
/// <returns>Aucune valeur de retour</returns>
Projectile::Projectile(int startX, int startY, float startAngle)
{
	positionX = startX;
	positionY = startY;
	projectileAngle = startAngle;
	


	projectileTexture.loadFromFile("Assets\\Projectiles\\bullet.png");

	projectileSprite.setTexture(projectileTexture);
	projectileSprite.setPosition(positionX, positionY);
	projectileSprite.setOrigin(projectileTexture.getSize().x / 2, projectileTexture.getSize().y / 2);
	projectileSprite.setRotation(projectileAngle);
}

/// Constructeur de l'ennemi
/// </summary>
/// <returns>Aucune valeur de retour</returns>
Projectile::Projectile()
{

	projectileTexture.loadFromFile("Assets\\Projectiles\\bullet.png");

	projectileSprite.setTexture(projectileTexture);
	projectileSprite.setPosition(positionX, positionY);
	projectileSprite.setOrigin(projectileTexture.getSize().x / 2, projectileTexture.getSize().y / 2);
	projectileSprite.setRotation(projectileAngle);
}

/// <summary>
/// Méthode permettant de retourner la valeur en X
/// </summary>
/// <returns>Retourne la position en X</returns>
int Projectile::GetPositionX()
{
	return positionX;

}
/// <summary>
/// Méthode permettant de retourner la valeur en Y
/// </summary>
/// <returns>Retourne la position en Y</returns>
int Projectile::GetPositionY()
{
	return positionY;
}
/// <summary>
/// Méthode permettant de changer la valeur en X
/// </summary>
/// <param name="newPositionX">position en X a assigner".</param>
/// <returns>Aucune valeur de retour</returns>
void Projectile::SetPositionX(int newPositionX)
{
	positionX = newPositionX;
	projectileSprite.setPosition(positionX, positionY);

}
/// <summary>
/// Méthode permettant de changer la valeur en Y
/// </summary>
/// <param name="newPositionY">position en Y a assigner".</param>
/// <returns>Aucune valeur de retour</returns>
void Projectile::SetPositionY(int newPositionY)
{
	positionY = newPositionY;
	projectileSprite.setPosition(positionX, positionY);
}
/// <summary>
/// Méthode permettant de changer l'angle
/// </summary>
/// <param name="newPositionX">l'angle a assigner".</param>
/// <returns>Aucune valeur de retour</returns>
void Projectile::SetAngle(float newAngle)
{
	projectileAngle = newAngle;
}
/// <summary>
/// Méthode permettant de retourner le sprite
/// </summary>
/// <returns>Retourne le sprite</returns>
Sprite Projectile::GetSprite()
{
	return projectileSprite;
}
/// <summary>
/// Méthode permettant de retourner la texture
/// </summary>
/// <returns>Retourne la texture</returns>
Texture Projectile::GetTexture()
{
	return projectileTexture;
}

/// <summary>
/// Méthode permettant de faire déplacer le projectile
/// </summary>
/// <returns>Aucune valeur de retour</returns>
void Projectile::Update()
{
	SetPositionX(positionX + cos(projectileAngle) * SPEED);
	SetPositionY(positionY + sin(projectileAngle) * SPEED);
}