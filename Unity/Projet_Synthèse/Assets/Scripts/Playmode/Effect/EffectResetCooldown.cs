using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "ResetCooldown", menuName = "Game/Effects/ResetCooldown")]
    public class EffectResetCooldown : Effect
    {
        [SerializeField]
        [Tooltip("Leave unchecked if we reset the first ability. checkif we reset the second ability cooldown.")]
        protected bool isSecondAbility = false;

        public override void Apply(GameObject caster, GameObject target)
        {
            AbilityController controller = caster.GetComponentInChildren<AbilityController>();
            if (isSecondAbility)
            {
                controller.ResetCooldownSecondAbility();
            }
            else
            {
                controller.ResetCooldownFirstAbility();
            }
        }
    }

}

