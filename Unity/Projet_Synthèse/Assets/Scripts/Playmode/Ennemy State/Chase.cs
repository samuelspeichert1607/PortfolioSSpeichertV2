using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/EnnemyState/Chase")]
    public class Chase : EnnemyState
    {

        private int nbNodesMoved;

        public Chase(GameObject target, AiPath pathFinder, EnnemyController ennemy, List<GameObject> objectsDetected)
        {
            this.ennemy = ennemy;
            this.objectsDetected = objectsDetected;
            if (!isObjectAlreadyThere(target))
            {
                objectsDetected.Add(target);
            }
            FindTargetWithMostPriority();
            this.pathFinder = pathFinder;
            pathFinder.SetNewPath(target.transform.position);
            nbNodesMoved = 0;
        }

        public override void OnPlayerEnterDetected(GameObject player)
        {
            if (!isObjectAlreadyThere(player))
            {
                objectsDetected.Add(player);
            }
            FindTargetWithMostPriority();
            pathFinder.SetNewPath(target.transform.position);
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
                    target = ennemy.MainTarget;
                    pathFinder.SetNewPath(target.transform.position);
                }
            }
            else
            {
                pathFinder.SetNewPath(target.transform.position);
            }
        }

        public override void OnPlayerInRangeDetected(GameObject player)
        {
            if (!isObjectAlreadyThere(player))
            {
                objectsDetected.Add(player);
            }
            FindTargetWithMostPriority();
            if (IsInPriority(player))
            {
                ennemy.State = new Attack(player, pathFinder, ennemy, objectsDetected);
            }
        }

        public override void OnPlayerOutOfRangeDetected(GameObject player)
        {

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
                        target = ennemy.MainTarget;
                        pathFinder.SetNewPath(target.transform.position);
                    }
                }
                else
                {
                    pathFinder.SetNewPath(target.transform.position);
                }
            }
            Node destinationNode = pathFinder.GetDestinationNode(ennemy.gameObject.transform.position);
            if ((object)destinationNode != null)
            {
                ennemy.Move(destinationNode.Position);
            }
            else
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
                        target = ennemy.MainTarget;
                        pathFinder.SetNewPath(target.transform.position);
                    }
                }
                else
                {
                    pathFinder.SetNewPath(target.transform.position);
                }
            }
        }

    }
}
