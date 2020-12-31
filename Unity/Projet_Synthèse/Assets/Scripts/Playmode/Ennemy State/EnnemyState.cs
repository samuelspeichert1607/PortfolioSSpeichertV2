using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/EnnemyState/EnnemyState")]
    public abstract class EnnemyState
    {
        protected EnnemyController ennemy;

        protected AiPath pathFinder;

        protected GameObject target;

        protected List<GameObject> objectsDetected;

        // Update is called once per frame
        public abstract void Update();

        /// <summary>
        /// appelé lorsque le joueur entre dans la vue
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        public abstract void OnPlayerEnterDetected(GameObject player);

        /// <summary>
        /// appelé lorsque le joueur sort de la vue
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        public abstract void OnPlayerExitDetected(GameObject player);

        /// <summary>
        /// appelé lorsque le joueur est assez proche pour être attaqué
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        public abstract void OnPlayerInRangeDetected(GameObject player);

        /// <summary>
        /// appelé lorsque le joueur n'est plus assez proche pour être attaqué
        /// </summary>
        /// <param name="player">le gameobject du joueur détecté</param>
        public abstract void OnPlayerOutOfRangeDetected(GameObject player);

        protected enum targetTypes
        {
            Player = 0,
            Turret = 1,
            Extractor = 2,
            Sub = 3,
            None = 4
        }

        private void RemoveNullTargets()
        {
            for (int i = 0; i < objectsDetected.Count; i++)
            {
                if (objectsDetected[i] == null)
                {
                    objectsDetected.RemoveAt(i);
                    i--;
                }
            }
        } 

        protected bool isObjectAlreadyThere(GameObject target)
        {
            for (int i = 0; i < objectsDetected.Count; i++)
            {
                if (objectsDetected[i].GetTopParent() == target.GetTopParent())
                {
                    return true;
                }
            }
            return false;
        }

        protected void FindTargetWithMostPriority()
        {
            RemoveNullTargets();
            GameObject currentObjectInPriority = null;
            targetTypes currentObjectType = targetTypes.None;
            for (int i = 0; i < objectsDetected.Count; i++)
            {
                //si c'est le sous-marin
                if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Submarine.ToString())
                {
                    if (currentObjectType > targetTypes.Sub)
                    {
                        currentObjectType = targetTypes.Sub;
                        currentObjectInPriority = objectsDetected[i];
                    }
                }
                else if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Construct.ToString())
                {
                    //si c'est un extracteur
                    if (objectsDetected[i].GetTopParent().layer == LayerMask.NameToLayer(R.S.Layer.MetalExtractor))
                    {
                        if (currentObjectType > targetTypes.Extractor)
                        {
                            currentObjectType = targetTypes.Extractor;
                            currentObjectInPriority = objectsDetected[i];
                        }
                    }
                    //si c'est une tourelle
                    else
                    {
                        if (currentObjectType > targetTypes.Turret)
                        {
                            currentObjectType = targetTypes.Turret;
                            currentObjectInPriority = objectsDetected[i];
                        }
                    }
                }
                //si c'est un player
                else if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Player.ToString())
                {
                    if (currentObjectType > targetTypes.Player)
                    {
                        currentObjectType = targetTypes.Player;
                        currentObjectInPriority = objectsDetected[i];
                    }
                }
            }
            target = currentObjectInPriority;
        }

        protected bool IsInPriority(GameObject target)
        {
            targetTypes currentObjectType = targetTypes.None;
            if (target.GetTopParent().tag == R.E.Tag.Submarine.ToString())
            {
                currentObjectType = targetTypes.Sub;
            }
            else if (target.GetTopParent().tag == R.E.Tag.Construct.ToString())
            {
                //si c'est un extracteur
                if (target.GetTopParent().layer == LayerMask.NameToLayer(R.S.Layer.MetalExtractor))
                {
                    currentObjectType = targetTypes.Extractor;
                }
                //si c'est une tourelle
                else
                {
                    currentObjectType = targetTypes.Turret;
                }
            }
            //si c'est un player
            else if (target.GetTopParent().tag == R.E.Tag.Player.ToString())
            {
                if (currentObjectType > targetTypes.Player)
                {
                    currentObjectType = targetTypes.Player;
                    return true;
                }
            }
            for (int i = 0; i < objectsDetected.Count; i++)
            {
                //si c'est le sous-marin
                if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Submarine.ToString())
                {
                    if (currentObjectType > targetTypes.Sub)
                    {
                        return false;
                    }
                }
                else if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Construct.ToString())
                {
                    //si c'est un extracteur
                    if (objectsDetected[i].GetTopParent().layer == LayerMask.NameToLayer(R.S.Layer.MetalExtractor))
                    {
                        if (currentObjectType > targetTypes.Extractor)
                        {
                            return false;
                        }
                    }
                    //si c'est une tourelle
                    else
                    {
                        if (currentObjectType > targetTypes.Turret)
                        {
                            return false;
                        }
                    }
                }
                //si c'est un player
                else if (objectsDetected[i].GetTopParent().tag == R.E.Tag.Player.ToString())
                {
                    if (currentObjectType > targetTypes.Player)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
