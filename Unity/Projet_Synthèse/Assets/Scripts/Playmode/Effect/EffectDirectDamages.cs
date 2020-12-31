using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "HitDamage", menuName = "Game/Effects/HitDamage")]
    public class EffectDirectDamages : Effect
    {
        [SerializeField]
        [Tooltip("Direct damage dealt to target.")]
        private int initialDamage;

        public override void Apply(GameObject caster, GameObject target)
        {
            int finalDamages = caster.GetComponentInChildren<CharacterStatistics>().CalculateDamageDealt(initialDamage);
            target.GetComponentInChildren<KillableObject>().ReceiveDamage(finalDamages);
        }

    }

}

