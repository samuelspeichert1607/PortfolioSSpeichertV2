using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Actuator/EnnemyMover")]
    public class EnnemyMover : GameScript
    {
        [Tooltip("Valeur plus haute ou égale à 42 peut causer des problèmes.\nDéfinis la vitesse de l'ennemi par rapport au nombre d'images par seconde")]
        [SerializeField]
        [Range(1, 60)]
        private float speed;

        private CharacterStatistics stats;

        private Coroutine immobilizeCoroutine;

        private Rigidbody2D topParentRigidBody;

        private void InjectEnnemyMover([TopParentScope] Rigidbody2D topParentRigidBody,
            [EntityScope] CharacterStatistics stats)
        {
            this.topParentRigidBody = topParentRigidBody;
            this.stats = stats;
        }

        private void Awake()
        {
            InjectDependencies("InjectEnnemyMover");
        }

        public void Move(Vector2 destination)
        {
            Vector3 topParentPosition = topParentRigidBody.transform.position;
            float newX = topParentPosition.x;
            float newY = topParentPosition.y;
            if (topParentPosition.x < destination.x)
            {
                newX += 1f * stats.CalculateMovementSpeed(speed) * Time.deltaTime;
                if (newX > destination.x)
                {
                    newX = destination.x;
                }
            }
            if (topParentPosition.x > destination.x)
            {
                newX -= 1f * stats.CalculateMovementSpeed(speed) * Time.deltaTime;
                if (newX < destination.x)
                {
                    newX = destination.x;
                }
            }
            if (topParentPosition.y < destination.y)
            {
                newY += 1f * stats.CalculateMovementSpeed(speed) * Time.deltaTime;
                if (newY > destination.y)
                {
                    newY = destination.y;
                }
            }
            if (topParentPosition.y > destination.y)
            {
                newY -= 1f * stats.CalculateMovementSpeed(speed) * Time.deltaTime;
                if (newY < destination.y)
                {
                    newY = destination.y;
                }
            }
            Vector2 newPosition = new Vector2(newX, newY);

            Turn(newPosition);
            topParentRigidBody.MovePosition(newPosition);


        }

        public void Turn(Vector2 destination)
        {
            Vector2 mouvement = new Vector2(destination.x - topParentRigidBody.transform.position.x,
                                            destination.y - topParentRigidBody.transform.position.y);
            float angle = Mathf.Atan2(mouvement.y, mouvement.x) * Mathf.Rad2Deg;
            topParentRigidBody.rotation = angle;
        }
        
    }


}

