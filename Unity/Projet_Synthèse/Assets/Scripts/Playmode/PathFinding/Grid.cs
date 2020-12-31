using System.Collections.Generic;
using Harmony;
using UnityEngine;
using System.IO;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/State/Grid")]
    public class Grid : GameScript
    {
        [Tooltip("Show the correct path in cyan")]
        [SerializeField]
        private bool showCorrectPath;

        [Tooltip("Show the nodes that were check to find path in grey")]
        [SerializeField]
        private bool showTestedPaths;

        [Tooltip("Show walls in blue")]
        [SerializeField]
        private bool showWalls;

        [Tooltip("Show neibors of all shown nodes")]
        [SerializeField]
        private bool showNeighbours;

        [Tooltip("time the displayed nodes will show")]
        [SerializeField]
        private float nodeShowTime;

        [Tooltip("Position en haut à gauche de la grille.")]
        [SerializeField]
        private GameObject lowerLeftStartingPoint;

        [Tooltip("Position en bas à droite de la grille.")]
        [SerializeField]
        private GameObject upperRightEndingPoint;

        [Tooltip("Taille d'un carré de la grille.")]
        [Range(0.35f, 100f)]
        [SerializeField]
        private float nodeSize = 1;

        //[HideInInspector]
        [SerializeField]
        [Tooltip("Nombre de ligne de la grille. Invisible dans l'inspecteur, car calculée.")]
        private int nbRows = 0;

        //[HideInInspector]
        [SerializeField]
        [Tooltip("Nombre de colonnes de la grille. Invisible dans l'inspecteur, car calculée.")]
        private int nbColumns = 0;

        [HideInInspector]
        [SerializeField]
        [Tooltip("Véritable grille. Invisible dans l'inspecteur, car calculée. Une seule dimension pour des questions de performances.")]
        //nodes dans l'ordre de bas en haut et de gauche à droite
        private Node[] grid = new Node[] { };

        //seulement pour les tests
        public float NodeSize
        {
            get
            {
                return nodeSize;
            }

            private set
            {
                nodeSize = value;
            }
        }

        public void Create()
        {
            DeleteCurrentGrid();

            CreateNewGrid();
        }

        public void Clear()
        {
            DeleteCurrentGrid();
        }

        public Grid CopyGrid()
        {
            Grid temp = new Grid();
            temp.grid = grid;
            temp.nbRows = nbRows;
            temp.nbColumns = nbColumns;
            temp.nodeShowTime = nodeShowTime;
            temp.NodeSize = NodeSize;
            temp.lowerLeftStartingPoint = lowerLeftStartingPoint;
            temp.upperRightEndingPoint = upperRightEndingPoint;
            temp.showCorrectPath = showCorrectPath;
            temp.showNeighbours = showNeighbours;
            temp.showTestedPaths = showTestedPaths;
            temp.showWalls = showWalls;
            return temp;
        }

        public void DrawDebug()
        {
            for (int i = 0; i < nbColumns; i++)
            {

                for (int j = 0; j < nbRows; j++)
                {
                    Node node = GetNodeInGrid(i, j);
                    Debug.DrawLine(node.Position - Vector2.one / 10,
                                   node.Position + Vector2.one / 10,
                                   node.IsOnWall ? Color.red : Color.green);
                }

            }

        }

        public void DrawNeighbours()
        {
            foreach (Node node in grid)
            {
                for (int i = 0; i < node.Neighbours.Length; i++)
                {
                    Debug.DrawLine(node.Position,
                                   node.Neighbours[i].Position,
                                   Color.cyan);
                }
            }
        }

        public void DrawNode(Node node, Color color)
        {
            Debug.DrawLine(node.Position - Vector2.one / 10,
                               node.Position + Vector2.one / 10,
                               color, nodeShowTime);
            //draws lines between neighbors
            if (showNeighbours)
            {
                for (int i = 0; i < node.Neighbours.Length; i++)
                {
                    Debug.DrawLine(node.Position,
                                   node.Neighbours[i].Position,
                                   color, nodeShowTime);
                }
            }
        }

        public void DrawNodeForever(Node node, Color color)
        {
            Debug.DrawLine(node.Position - Vector2.one / 10,
                               node.Position + Vector2.one / 10,
                               color, float.MaxValue);
            //draws lines between neighbors
            if (showNeighbours)
            {
                for (int i = 0; i < node.Neighbours.Length; i++)
                {
                    Debug.DrawLine(node.Position,
                                   node.Neighbours[i].Position,
                                   color, float.MaxValue);
                }
            }
        }

        public void UnvisitAllNodes()
        {
            foreach (Node node in grid)
            {
                node.IsVisited = false;
            }
        }

        private void DeleteCurrentGrid()
        {
#if UNITY_EDITOR

            //Make grid size 0
            nbRows = 0;
            nbColumns = 0;

            //Delete all nodes
            for(int i = grid.Length - 1; i >= 0; i--)
            {
                DestroyImmediate(grid[i]);
            }
            grid = new Node[] { };

            //Make a cleanup of the remaining nodes in the scene.
            Node[] allNodes = FindObjectsOfType<Node>();
            for (int i = allNodes.Length - 1; i >= 0; i--)
            {
                DestroyImmediate(allNodes[i]);
            }

#endif
        }

        private void CreateNewGrid()
        {
#if UNITY_EDITOR

            //We dont know how many nodes will be created. At the end, we'll convert this list to an array.
            List<Node> newGrid = new List<Node>();

            //Check from left to right.
            Vector2 currentNodePosition = lowerLeftStartingPoint.transform.position;
            while (currentNodePosition.x <= upperRightEndingPoint.transform.position.x)
            {
                int currentNbRows = 0;
                //Check from bottom to top.
                while (currentNodePosition.y <= upperRightEndingPoint.transform.position.y)
                {
                    Node node = ScriptableObject.CreateInstance<Node>();
                    node.Position = currentNodePosition;
                    node.IsOnWall = CheckIsOnWall(currentNodePosition);
                    if (node.IsOnWall && showWalls)
                    {
                        DrawNodeForever(node, Color.blue);
                    }
                    newGrid.Add(node);

                    node.name = "Node" + newGrid.Count;
                    //Go to next position in Y
                    currentNodePosition.y += NodeSize;

                    //Increment number of columns
                    currentNbRows++;
                }

                //We can't know for sure how many rows we have for each column.
                //It should be the same every time.
                if (nbRows == 0)
                {
                    nbRows = currentNbRows;
                }
                else
                {
                    Debug.Assert(nbRows == currentNbRows, "Something is wrong with the grid. The amount of columns " +
                                                                "is not the same on each row. Please fix this.");
                }

                //Go to next position in X and reset Y
                currentNodePosition.x += NodeSize;
                currentNodePosition.y = lowerLeftStartingPoint.transform.position.y;

                //Increment number of collunm
                nbColumns++;
            }

            grid = newGrid.ToArray();

            FindAdjacentNodes();
#endif
        }

        private bool CheckIsOnWall(Vector2 position)
        {
            foreach (RaycastHit2D hit in Physics2D.BoxCastAll(position,
                                                              Vector2.one * NodeSize,
                                                              0,
                                                              Vector2.zero))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(R.S.Layer.WallDetection))
                {
                    return true;
                }
            }
            return false;
        }

        public Node GetNodeByPoint(Vector2 target)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (!grid[i].IsOnWall)
                {                    
                    float distance = Mathf.Sqrt(Mathf.Pow(target.x - grid[i].Position.x, 2) + Mathf.Pow(target.y - grid[i].Position.y, 2));
                    if (distance <= NodeSize)
                    {
                        return grid[i];
                    }
                }
            }
            throw new System.Exception("there is no node around position " + target);
        }

        private Node GetNodeInGrid(int x, int y)
        {
            return grid[x * nbRows + y];
        }

        /// <summary>
        /// instancie l'ensemble des nodes voisines de chaque nodes
        /// Si une node voisine est null, elle n'est pas insérée
        /// </summary>
        private void FindAdjacentNodes()
        {
            for (int i = 0; i < nbColumns; i++)
            {
                for (int j = 0; j < nbRows; j++)
                {
                    if (GetNodeInGrid(i, j).IsOnWall == false)
                    {
                        List<Node> voisins = new List<Node>();

                        if (j - 1 >= 0)
                        {
                            voisins.Add(GetNodeInGrid(i, j - 1));
                        }

                        if (j + 1 < nbRows)
                        {
                            voisins.Add(GetNodeInGrid(i, j + 1));
                        }

                        if (i - 1 >= 0)
                        {
                            voisins.Add(GetNodeInGrid(i - 1, j));
                        }

                        if (i + 1 < nbColumns)
                        {
                            voisins.Add(GetNodeInGrid(i + 1, j));
                        }

                        if (j - 1 >= 0 && i - 1 >= 0)
                        {
                            voisins.Add(GetNodeInGrid(i - 1, j - 1));
                        }

                        if (j + 1 < nbRows && i + 1 < nbColumns)
                        {
                            voisins.Add(GetNodeInGrid(i + 1, j + 1));
                        }



                        if (j - 1 >= 0 && i + 1 < nbColumns)
                        {
                            voisins.Add(GetNodeInGrid(i + 1, j - 1));
                        }


                        if (j + 1 < nbRows && i - 1 >= 0)
                        {
                            voisins.Add(GetNodeInGrid(i - 1, j + 1));
                        }



                        GetNodeInGrid(i, j).Neighbours = voisins.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// sort le chemin vers un node spécifique
        /// dois être appelé après avoir appelé UnvisitAllNodes()
        /// </summary>
        /// <param name="target">copie du fogParticle sur le target</param>
        /// <param name="current">copie du fogParticle courrant</param>
        public List<Node> getPath(Node target, Node current)
        {
            if (showTestedPaths)
            {
                DrawNode(current, Color.grey);
            }
            current.IsVisited = true;

            if (current == target)
            {
                List<Node> temp = new List<Node>();
                temp.Add(current);
                return temp;
            }
            List<Node> OrderedNeighbours = OrderNeighbours(current.Neighbours, target);
            if (current.Neighbours.Length != OrderedNeighbours.Count)
            {
                Debug.Log("error");
            }
            for (int i = 0; i < OrderedNeighbours.Count; i++)
            {
                if (OrderedNeighbours[i].IsOnWall == false && !OrderedNeighbours[i].IsVisited)
                {
                    List<Node> temp = new List<Node>();
                    temp.Add(current);
                    List<Node> tempNext = getPath(target, OrderedNeighbours[i]);
                    if (tempNext != null)
                    {
                        temp.AddRange(tempNext);
                        if (showCorrectPath)
                        {
                            DrawNode(current, Color.cyan);
                        }
                        return temp;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// retourne la liste des nodes voisins dans l'ordre du plus proche au plus loin comparé au node de destination finale 
        /// </summary>
        /// <param name="neighbours">liste des voisins du node courant</param>
        /// <param name="target">destination finale du chemin</param>
        /// <returns>la liste des nodes voisins dans l'ordre du plus proche au plus loin comparé au node de destination finale</returns>
        private List<Node> OrderNeighbours(Node[] neighbours, Node target)
        {
            double[] neighboursDistances = new double[neighbours.Length];
            for (int i = 0; i < neighboursDistances.Length; i++)
            {
                neighboursDistances[i] = Mathf.Sqrt(Mathf.Pow((neighbours[i].Position.x - target.Position.x), 2) + Mathf.Pow((neighbours[i].Position.y - target.Position.y), 2));
            }
            List<Node> newOrder = new List<Node>();
            //on passe du plus petit au plus grand
            for (int i = 0; i < neighboursDistances.Length; i++)
            {
                double smallestDistance = -1;
                int smallestDistancePosition = -1;
                //on compares les distances pour avoir le prochain plus petit
                for (int j = 0; j < neighboursDistances.Length; j++)
                {
                    bool isOk = true;
                    //on regarde si ce chiffre est déjà dans la liste
                    for (int k = 0; k < newOrder.Count; k++)
                    {
                        if (neighbours[j] == newOrder[k])
                        {
                            isOk = false;
                        }
                    }
                    if (isOk)
                    {
                        if (smallestDistance == -1 || smallestDistance > neighboursDistances[j])
                        {
                            smallestDistance = neighboursDistances[j];
                            smallestDistancePosition = j;
                        }
                    }
                }
                newOrder.Add(neighbours[smallestDistancePosition]);
            }
            return newOrder;
        }

    }
}