using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/EnnemyState/Attack")]
    public class Attack : EnnemyState
    {
        public Attack(GameObject target, AiPath pathFinder, EnnemyController ennemy, List<GameObject> objectsDetected)
        {
            this.ennemy = ennemy;
            this.objectsDetected = objectsDetected;
            FindTargetWithMostPriority();
            this.pathFinder = pathFinder;
            this.target = target;
        }

        public override void OnPlayerEnterDetected(GameObject player)
        {
            if (!isObjectAlreadyThere(player))
            {
                objectsDetected.Add(player);
            }
            FindTargetWithMostPriority();
            ennemy.State = new Chase(target, pathFinder, ennemy, objectsDetected);
        }

        public override void OnPlayerExitDetected(GameObject player)
        {
            objectsDetected.Remove(player);
            FindTargetWithMostPriority();
            if (target == null)
            {
                if (ennemy.StartState == EnnemyController.StartingStates.Patrol)
                {
                    ennemy.State = new Patrol(ennemy.PatrolPoints, pathFinder, ennemy, objectsDetected);
                }
                else
                {
                    ennemy.State = new Chase(ennemy.MainTarget, pathFinder, ennemy, objectsDetected);
                }
            }
            
        }

        public override void OnPlayerInRangeDetected(GameObject player)
        {
            if (!isObjectAlreadyThere(player))
            {
                objectsDetected.Add(player);
            }
            FindTargetWithMostPriority();
        }

        public override void OnPlayerOutOfRangeDetected(GameObject player)
        {
            FindTargetWithMostPriority();
            if (player.Equals(target))
            {
                ennemy.State = new Chase(target, pathFinder, ennemy, objectsDetected);
            }
        }

        // Update is called once per frame
        public override void Update()
        {
            if (target == null)
            {
                FindTargetWithMostPriority();
                if (target == null)
                {
                    if (ennemy.StartState == EnnemyController.StartingStates.Patrol)
                    {
                        ennemy.State = new Patrol(ennemy.PatrolPoints, pathFinder, ennemy, objectsDetected);
                    }
                    else
                    {
                        ennemy.State = new Chase(ennemy.MainTarget, pathFinder, ennemy, objectsDetected);
                    }
                }
                else
                {
                    ennemy.State = new Chase(target, pathFinder, ennemy, objectsDetected);
                }
            }
            ennemy.Rotate(target.transform.position);
            ennemy.Attack();
        }
    }
}