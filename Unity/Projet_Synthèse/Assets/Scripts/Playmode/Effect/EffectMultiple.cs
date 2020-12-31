using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "MultipleEffects", menuName = "Game/Effects/Multiple")]
    public class EffectMultiple : Effect
    {
        [SerializeField]
        [Tooltip("List of all applied effects.")]
        private Effect[] AppliedEffects;

        public override void Apply(GameObject caster, GameObject target)
        {
            foreach (Effect effect in AppliedEffects)
            {
                effect.Apply(caster,target);
            }
        }
    }

}


