using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Resistance", menuName = "Game/Effects/Resistance")]
    public class EffectResistance : Effect
    {
        [SerializeField]
        [Range(1,100)]
        [Tooltip("The resistance percentage")]
        protected int resistance;

        [SerializeField]
        [Range(0,3600)]
        [Tooltip("The duration of the buff in seconds")]
        protected float durationInSeconds;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
            stats.StartCoroutine(DamageResistance(stats, resistance, durationInSeconds));
        }

        private IEnumerator DamageResistance(CharacterStatistics stats,int percentageChanged, float duration)
        {
            stats.IncreaseResistanceMultiplier(percentageChanged);
            yield return new WaitForSeconds(duration);
            stats.DecreaseResistanceMultiplier(percentageChanged);
        }
    }

}

