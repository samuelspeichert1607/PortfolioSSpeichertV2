using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/ScoutController")]
    public class ScoutController : GardianController
    {
        [SerializeField]
        [Tooltip("Distance ennemies have to be to this scout to be alerted")]
        private float alertRadius;

        protected override void OnHealthChanged(int currentHealthPoints, int maxHealthPoints)
        {
            //lors de la réception de coup, tous les ennemis autour du scout se dirigeront vers lui
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(gameObject.transform.position,
                                                                  alertRadius,
                                                                  Vector2.zero))
            {
                if (hit.collider.gameObject.GetComponentInChildren<AiPath>() != null && hit.collider.gameObject != topParentGameObject)
                {
                    hit.collider.gameObject.GetComponentInChildren<AiPath>().SetNewPath(topParentGameObject.transform.position);
                }
            }
        }

    }
}