using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public static class Physics2DExtensions
    {
        public static bool CheckIfWallsIsInTheWay(GameObject objectWhoWantsToKnow, string layerNameNeeded,
            GameObject relatedGameObject)
        {
            bool wallIsInTheWay = false;
            if (relatedGameObject.gameObject.layer == LayerMask.NameToLayer(layerNameNeeded))
            {
                wallIsInTheWay = DetectWallBetweenTwoPoints(objectWhoWantsToKnow.transform.position,
                    relatedGameObject.transform.parent.position);
            }
            return wallIsInTheWay;
        }


        public static bool DetectWallBetweenTwoPoints(Vector3 initialObjectPosition, Vector3 secondObjectPosition)
        {
            bool wallIsInTheWay = false;
            RaycastHit2D[] hits = Physics2D.LinecastAll(initialObjectPosition, secondObjectPosition);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallDetection"))
                {
                    wallIsInTheWay = true;
                }
            }
            return wallIsInTheWay;

        }
    }
    

}


