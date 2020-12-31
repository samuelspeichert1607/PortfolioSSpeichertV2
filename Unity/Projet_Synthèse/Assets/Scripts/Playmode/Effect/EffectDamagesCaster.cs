using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "DamageCaster", menuName = "Game/Effects/DamageCaster")]
    public class EffectDamagesCaster : Effect
    {
        [SerializeField]
        [Tooltip("Damages to apply to yourself")]
        private int damagePoints;

        public override void Apply(GameObject caster, GameObject target)
        {
            caster.GetComponentInChildren<KillableObject>().ReceiveDamage(damagePoints);
        }
    }
}
