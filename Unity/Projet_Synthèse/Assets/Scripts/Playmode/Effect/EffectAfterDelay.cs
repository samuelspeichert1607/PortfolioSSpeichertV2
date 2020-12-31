using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "DelayedEffect", menuName = "Game/Effects/Delay")]
    public class EffectAfterDelay : Effect
    {
        [SerializeField]
        [Tooltip("Ability to cast")]
        private Effect effect;

        [SerializeField]
        [Tooltip("Time waited before casting the buff, in seconds.")]
        private float waitBeforeCastInSeconds;

        public override void Apply(GameObject caster, GameObject target)
        {
            caster.GetComponent<AbilityController>()
                .StartCoroutine(ApplyAfterDelay(caster, target, waitBeforeCastInSeconds, effect));
        }
        

        private IEnumerator ApplyAfterDelay(GameObject caster,GameObject target ,float waitInSeconds, Effect appliedEffect)
        {
            yield return new WaitForSeconds(waitBeforeCastInSeconds);
            effect.Apply(caster,target);
        }
    }
}
