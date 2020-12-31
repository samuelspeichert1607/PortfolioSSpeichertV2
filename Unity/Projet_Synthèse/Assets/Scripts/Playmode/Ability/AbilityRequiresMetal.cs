using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/RequiresMetal(Ability)")]
    public class AbilityRequiresMetal : Ability
    {
        [SerializeField]
        [Tooltip("The metal cost of the ability")]
        private int metalPrice;

        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;

        private Metal submarineMetal;

        private void InjectAbilityCostsMetal([TagScope(R.S.Tag.Submarine)] Metal submarineMetal)
        {
            this.submarineMetal = submarineMetal;
        }

        public override Ability GetNextOnStack()
        {
            return ability;
        }

        private void Awake()
        {

            InjectDependencies("InjectAbilityCostsMetal");
        }

        public override void Cast()
        {
            if (submarineMetal.MetalQuantity > metalPrice)
            {
                ability.SetTarget(target);
                ability.Cast();
            }
        }
    }

}

