using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Push", menuName = "Game/Effects/Push")]
    public class EffectPush : Effect
    {
        [SerializeField]
        [Range(10,10000)]
        [Tooltip("The force at which the object is pushed")]
        protected float forceApplied;

        [SerializeField]
        [Tooltip("Should be the push done backward?")]
        protected bool pushIsBackward;

        public override void Apply(GameObject caster, GameObject target)
        {
            target.GetComponentInChildren<Rigidbody2D>().AddForce((target.transform.up) * forceApplied * (pushIsBackward ? -1 : 1));
            //For now: Only push target backward as a "knockback". 
            //Depending of time remaining, I shall make something little more fancy.
            //But this will do for now.
        }
    }

}

