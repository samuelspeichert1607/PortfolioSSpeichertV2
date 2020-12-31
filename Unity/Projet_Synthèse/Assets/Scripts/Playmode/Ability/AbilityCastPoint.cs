using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public class AbilityCastPoint : Ability
    {
        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;

        public override Ability GetNextOnStack()
        {
            return ability;
        }

        public override void Cast()
        {
            ability.Cast();

        }
    }

}


