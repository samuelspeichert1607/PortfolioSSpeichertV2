using System.Collections.Generic;
using System;
using Harmony;
using UnityEngine;
using System.Collections;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/State/AiPath")]
    public class AiPath : GameScript
    {
        [Tooltip("Show ai target in red")]
        [SerializeField]
        private bool showTargets;

        [Tooltip("Show ai path in green")]
        [SerializeField]
        private bool showPath;

        //chemin à suivre
        public List<Node> currentPath { get; private set; }

        //Ensemble des nodes sur la carte
        public Grid nodes { get; private set; }

        //Node sur lequel l'utilisteur se trouve
        private Node currentNode;

        private int pathPosition;

        private Vector2 topParentPosition;

        //seulement pour les tests
        public void setGrid(Grid grid, Vector2 topParentPosition)
        {
            this.nodes = grid;
            pathPosition = 0;
            this.topParentPosition = topParentPosition;
            currentNode = GetNodeByPoint(topParentPosition);
        }

        private void InjectGrid([SceneScope] Grid grid, [TopParentScope] Transform topParentPosition)
        {
            this.nodes = grid.CopyGrid();
            this.topParentPosition = topParentPosition.position;
        }        

        public void Awake()
        {
            //fait dans le start, car nous avons besoin de la grid des particules de fog qui soit créé et qu'il aient de la physique
            InjectDependencies("InjectGrid");
            pathPosition = 0;
            currentNode = GetNodeByPoint(topParentPosition);
        }

        /// <summary>
        /// retourne le point à atteindre pour continuer le chemin
        /// stoque le node courrant
        /// si le point à atteindre est atteinds, il retournera le prochain
        /// si la fin du chemin est atteinte, il retournera null
        /// </summary>
        /// <param name="position">la position courante de l'utilisateur du chemin</param>
        /// <returns>retourne le node à atteindre. Si la fin du chemin est attinds, retourne null</returns>
        public Node GetDestinationNode(Vector2 position)
        {
            //si nous avons atteinds la fin du chemin
            if (pathPosition >= currentPath.Count - 1)
            {
                return null;
            }
            //si l'utilisateur est rendu sur le node
            if ((int)position.x == (int)currentPath[pathPosition].Position.x && (int)position.y == (int)currentPath[pathPosition].Position.y)
            {
                currentNode = currentPath[pathPosition];//car on sais que l'utilisateur est exactement sur ce node
                pathPosition++;
                if (showPath)
                {
                    nodes.DrawNode(currentNode, Color.green);
                }
            }

            return currentPath[pathPosition];
        }

        /// <summary>
        /// crée un nouveau chemin à suivre
        /// </summary>
        /// <param name="target">destination finale du chemin</param>
        public void SetNewPath(Vector2 target)
        {
            nodes.UnvisitAllNodes();
            currentPath = nodes.getPath(GetNodeByPoint(target), currentNode);
            pathPosition = 0;
            if (showTargets)
            {
                nodes.DrawNodeForever(currentPath[currentPath.Count - 1], Color.red);
            }
            if (currentPath == null)
            {
                throw new Exception("There is no Path between " + currentNode.Position + " and " + target);
            }
        }
        /// <summary>
        /// donne le premier node qui touche le collider donné
        /// </summary>
        /// <param name="target">collider dans lequel nous devons trouver</param>
        /// <returns></returns>
        public Node GetNodeByPoint(Vector2 target)
        {
            return nodes.GetNodeByPoint(target);
        }
    }
}
