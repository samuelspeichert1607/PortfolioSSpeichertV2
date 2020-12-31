using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "ArmorRegeneration", menuName = "Game/Effects/Regeneration")]
    public class EffectRegenerateArmor : Effect
    {
        [SerializeField]
        [Tooltip("Duration of the regeneration in number of ticks.")]
        private int regenerationTickDuration;

        [SerializeField]
        [Tooltip("Time in seconds spent between each regeneration tick")]
        private float regenerationIntervalInSeconds;

        [SerializeField]
        [Range(0, 100)]
        [Tooltip("amount of armor regenerated per tick")]
        private int regenerationArmorPotencityPerTick;

        public override void Apply(GameObject caster, GameObject target)
        {
            KillableObject lifeControl = target.GetComponent<KillableObject>();
            lifeControl.StartCoroutine(
                RegenerateArmor(
                    lifeControl,regenerationTickDuration,regenerationIntervalInSeconds,regenerationArmorPotencityPerTick));
        }

        private IEnumerator RegenerateArmor(KillableObject lifeControl, int tickDuration, float tickInverval, int potencity)
        {
            for (int i = 0; i < tickDuration; i++)
            {
                yield return new WaitForSeconds(tickInverval);
                lifeControl.RepairArmor(potencity);

            }
        }

        
    }

}


