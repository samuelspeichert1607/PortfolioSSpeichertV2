using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/TargetSelf(Ability)")]
    public class AbilityTargetsSelf : Ability
    {
        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;


        public override void Cast()
        {
            ability.SetTarget(gameObject.GetTopParent());
            ability.Cast();
        }

        public override Ability GetNextOnStack()
        {
            return ability;
        }

    }

}


