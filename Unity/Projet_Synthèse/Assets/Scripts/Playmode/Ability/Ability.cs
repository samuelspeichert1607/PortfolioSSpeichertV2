using System.Collections;
using System.Collections.Generic;
using Harmony;
using ProjetSynthese;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/AbilityCore")]
    public class Ability : GameScript
    {
        [SerializeField]
        [Tooltip("The effects caused by the ability")]
        private Effect abilityEffect;

        protected GameObject target = null;

        public virtual void Cast()
        {
            abilityEffect.Apply(gameObject.GetTopParent(), target);
        }

        public virtual Ability GetNextOnStack()
        {
            return null;
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

    }

}


