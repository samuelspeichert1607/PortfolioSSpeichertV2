using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace ProjetSynthese
{
    [TestFixture]
    public class PathfindingTests
    {
        public const string CAVE_OF_THE_LOST_PATH = "Assets/Scenes/Levels/CaveOfTheLost.unity";

        public const string CAVE_OF_TRIALS_PATH = "Assets/Scenes/Levels/CaveOfTrials.unity";

        public const string LAIR_OF_THE_LEVIANTHAN_PATH = "Assets/Scenes/Levels/CaveOfTheLost.unity";

        public const string CAVE_OF_INFERNO_PATH = "Assets/Scenes/Levels/CaveOfTheLost.unity";

        private AiPath pathfinder;

        private GameObject sub;

        private float nodeSize;

        private void PreparePathfinding(string sceneToLoad)
        {
            Scene usedScene = EditorSceneManager.OpenScene(sceneToLoad);
            GameObject[] gameObjects = usedScene.GetRootGameObjects();
            bool hasGrid = false;
            bool hasAiPath = false;
            bool hasSub = false;
            Grid tempGrid = null;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (!hasGrid && gameObjects[i].GetComponent<Grid>() != null)
                {
                    gameObjects[i].GetComponent<Grid>().Create();
                    tempGrid = gameObjects[i].GetComponent<Grid>();
                    nodeSize = gameObjects[i].GetComponent<Grid>().NodeSize;
                    hasGrid = true;
                    if (hasAiPath && hasSub)
                    {
                        break;
                    }
                }
                else if (!hasAiPath && gameObjects[i].GetComponentInChildrensParentsOrSiblings<AiPath>() != null)
                {
                    //on vérifie si l'acteur n'est pas dans un mur
                    if (!(gameObjects[i].GetComponent<Collider2D>().IsTouchingLayers(LayerMask.NameToLayer(R.S.Layer.WallDetection))))
                    {
                        pathfinder = gameObjects[i].GetComponentInChildrensParentsOrSiblings<AiPath>();
                        hasAiPath = true;
                        if (hasGrid && hasSub)
                        {
                            break;
                        }
                    }
                }
                else if (!hasSub && gameObjects[i].GetComponentInChildrensParentsOrSiblings<SubmarineController>() != null)
                {
                    sub = gameObjects[i];
                    hasSub = true;
                    if (hasGrid && hasAiPath)
                    {
                        break;
                    }
                }
            }
            if (!hasAiPath || !hasGrid || !hasSub)
            {
                if (!hasAiPath)
                {
                    throw new System.Exception("The scene loaded for tests (" + sceneToLoad + ") has no gameObject with the component AiPath" +
                        " or it's in a wall");
                }
                else if (!hasGrid)
                {
                    throw new System.Exception("The scene loaded for tests (" + sceneToLoad + ") has no gameObject with the component Grid");
                }
                else if (!hasSub)
                {
                    throw new System.Exception("The scene loaded for tests (" + sceneToLoad + ") has no gameObject with the component SubmarineController");
                }
            }
            pathfinder.setGrid(tempGrid, pathfinder.gameObject.transform.position);
        }

        private void TestPathfinding()
        {
            Vector2 target = sub.transform.position;
            pathfinder.SetNewPath(target);
            //on regarde si le chemin n'est pas vide
            int pathLengh = pathfinder.currentPath.Count;
            Assert.IsTrue(pathLengh > 0);
            //on regarde si le chmin se rends à destination
            Vector2 finalPosition = pathfinder.currentPath[pathLengh - 1].Position;
            float distance = Mathf.Sqrt(Mathf.Pow(target.x - finalPosition.x, 2) + Mathf.Pow(target.y - finalPosition.y, 2));
            Assert.IsTrue(distance <= nodeSize);
            //on regarde si un des points est dans un mur
            for (int i = 0; i < pathLengh; i++)
            {
                foreach (RaycastHit2D hit in Physics2D.BoxCastAll(pathfinder.currentPath[i].Position,
                                                              Vector2.one * nodeSize,
                                                              0,
                                                              Vector2.zero))
                {
                    Assert.IsTrue(hit.transform.gameObject.layer != LayerMask.NameToLayer(R.S.Layer.WallDetection), "the node " + i + " (" + pathfinder.currentPath[i].Position + ") is in a wall");
                }
            }
        }

        [Test]
        public void TestPathfindingCaveOfTheLost()
        {
            PreparePathfinding(CAVE_OF_THE_LOST_PATH);
            TestPathfinding();
        }

        [Test]
        public void TestPathfindingCaveOfTrials()
        {
            PreparePathfinding(CAVE_OF_TRIALS_PATH);
            TestPathfinding();
        }

        [Test]
        public void TestPathfindingLairOfTheLevianthan()
        {
            PreparePathfinding(CAVE_OF_THE_LOST_PATH);
            TestPathfinding();
        }

        [Test]
        public void TestPathfindingCaveOfInferno()
        {
            PreparePathfinding(CAVE_OF_THE_LOST_PATH);
            TestPathfinding();
        }

    }
}
