using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Invisiblity", menuName = "Game/Effects/Invisibility")]
    public class EffectInvisibility : Effect
    {
        [SerializeField]
        [Tooltip("Duration of the invisibility effect")]
        private float invisibilityDuration;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
            stats.StartCoroutine(playerIsInvisible(stats, invisibilityDuration));
        }

        public IEnumerator playerIsInvisible(CharacterStatistics entityStats, float duration)
        {
            entityStats.AddInvisibility();
            yield return new WaitForSeconds(duration);
            entityStats.RemoveInvisibility();

        }
    }

}


