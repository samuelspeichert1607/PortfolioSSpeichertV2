using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/IEnnemyController")]
    public interface IEnnemyController
    {
        /// <summary>
        /// appelé lorsque le joueur entre dans la vue
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        void OnPlayerEnterDetected(GameObject player);

        /// <summary>
        /// appelé lorsque le joueur sort de la vue
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        void OnPlayerExitDetected(GameObject player);

        /// <summary>
        /// appelé lorsque la vie de l'ennemi change
        /// </summary>
        /// <param name="currentHealthPoints">points de vie de l'unité</param>
        /// <param name="maxHealthPoints">points de vie maximums de l'unité</param>
        void OnHealthChanged(int currentHealthPoints, int maxHealthPoints);

        /// <summary>
        /// appelé lorsque l'ennemi meurt
        /// </summary>
        void OnDeath();

        /// <summary>
        /// déplace l'ennemi vers un point
        /// </summary>
        /// <param name="destination">point vers lequel il faut se diriger</param>
        void Move(Vector2 destination);
    }
}
