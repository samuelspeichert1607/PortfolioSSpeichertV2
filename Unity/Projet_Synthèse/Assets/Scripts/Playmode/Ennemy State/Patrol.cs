using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/EnnemyState/Patrol")]
    public class Patrol : EnnemyState
    {
        private GameObject[] pathOfPatrol;

        private int currentPathPosition;

        /// <summary>
        /// instancie le state Patrol
        /// </summary>
        /// <param name="pathOfPatrol">liste de gameObjects qui détermine le chemin à parcourir</param>
        /// <param name="pathFinder">Script qui donne les chemins vers les nodes. Doit exister dans la scène</param>
        public Patrol(GameObject[] pathOfPatrol, AiPath pathFinder, EnnemyController ennemy, List<GameObject> objectsDetected)
        {
            this.ennemy = ennemy;
            this.objectsDetected = objectsDetected;
            this.pathFinder = pathFinder;
            this.pathOfPatrol = pathOfPatrol;
            currentPathPosition = 0;
            pathFinder.SetNewPath(pathOfPatrol[currentPathPosition].transform.position);
        }

        public override void OnPlayerEnterDetected(GameObject player)
        {
            ennemy.State = new Chase(player, pathFinder, ennemy, objectsDetected);
        }

        public override void OnPlayerExitDetected(GameObject player)
        {

        }

        public override void OnPlayerInRangeDetected(GameObject player)
        {
            if (!isObjectAlreadyThere(player))
            {
                objectsDetected.Add(player);
            }
            ennemy.State = new Attack(player, pathFinder, ennemy, objectsDetected);
        }

        public override void OnPlayerOutOfRangeDetected(GameObject player)
        {
            ennemy.State = new Chase(player, pathFinder, ennemy, objectsDetected);
        }

        // Update is called once per frame
        public override void Update()
        {
            Node destinationNode = pathFinder.GetDestinationNode(ennemy.gameObject.transform.position);
            if ((object)destinationNode == null)
            {
                currentPathPosition++;
                if (currentPathPosition >= pathOfPatrol.Length)
                {
                    currentPathPosition = 0;
                }
                pathFinder.SetNewPath(pathOfPatrol[currentPathPosition].transform.position);
                destinationNode = pathFinder.GetDestinationNode(ennemy.gameObject.transform.position);
            }
            ennemy.Move(destinationNode.Position);
        }
    }
}
