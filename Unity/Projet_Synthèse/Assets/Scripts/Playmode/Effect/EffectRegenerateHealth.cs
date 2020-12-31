using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Regeneration", menuName = "Game/Effects/Regeneration")]
    public class EffectRegenerateHealth : Effect
    {
        [SerializeField]
        [Tooltip("Duration of the regeneration in number of ticks.")]
        private int regenerationTickDuration;

        [SerializeField]
        [Tooltip("Time in seconds spent between each regeneration tick")]
        private float regenerationIntervalInSeconds;

        [SerializeField]
        [Range(0, 100)]
        [Tooltip("amount of health regenerated per tick")]
        private int regenerationHealthPotencityPerTick;

        public override void Apply(GameObject caster, GameObject target)
        {
            KillableObject lifeControl = target.GetComponent<KillableObject>();
            lifeControl.StartCoroutine(
                RegenerateLife(
                    lifeControl, regenerationTickDuration, regenerationIntervalInSeconds, regenerationHealthPotencityPerTick));
        }

        public IEnumerator RegenerateLife(KillableObject lifeControl, int tickDuration, float tickInverval, int potencity)
        {
            for (int i = 0; i < tickDuration; i++)
            {
                yield return new WaitForSeconds(tickInverval);
                lifeControl.Heal(potencity);

            }


        }


    }

}


