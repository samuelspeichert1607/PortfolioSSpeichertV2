using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/TargetNearby(Ability)")]
    public class AbilityRequiresNearbyEntity : Ability
    {

        [SerializeField]
        [Tooltip("Ability radius in unity meters")]
        private float radius;

        [SerializeField]
        [Tooltip("Tag of the targeted entity")]
        private R.E.Tag targetedTag;

        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;

        public override void Cast()
        {
            findTarget();
            if (target != null)
            {
                ability.SetTarget(target);
                ability.Cast();
            }
            //GameObject newEffect = Instantiate(visual, nearbyEntity.transform.position,nearbyEntity.transform.rotation);
            //newEffect.transform.parent = nearbyEntity.transform;

        }

        public override Ability GetNextOnStack()
        {
            return ability;
        }

        private void findTarget()
        {
            Collider2D[] targets =
                Physics2D.OverlapCircleAll(transform.position, radius);
            if (targets.Length != 0)
            {
                GameObject targetFound = null;
                for (int i = 0; i < targets.Length; i++)
                {
                    targetFound = targets[i].gameObject;
                    if (targetFound.tag == targetedTag.ToString() && targetFound.GetTopParent() != gameObject.GetTopParent() && 
                        !Physics2DExtensions.DetectWallBetweenTwoPoints(gameObject.transform.position, targetFound.transform.position))
                    {
                        target = targetFound.GetTopParent();;
                        return;

                    }
                    else
                    {
                        targetFound = null;
                    }
                }

            }

        }
    }
}
