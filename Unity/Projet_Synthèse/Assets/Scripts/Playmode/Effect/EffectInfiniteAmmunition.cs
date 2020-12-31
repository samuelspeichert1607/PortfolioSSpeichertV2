using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "InfiniteAmmo", menuName = "Game/Effects/InfiniteAmmo")]
    public class EffectInfiniteAmmunition : Effect
    {
        [SerializeField]
        [Range(0, 3600)]
        [Tooltip("Duration of the unlimited ammo effect")]
        private float effectDuration;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
            stats.StartCoroutine(TemporaryUnlimitedAmmo(stats, effectDuration));
        }

        private IEnumerator TemporaryUnlimitedAmmo(CharacterStatistics entityStats, float duration)
        {
            entityStats.AddInfiniteAmmunitionBuff();
            yield return new WaitForSeconds(duration);
            entityStats.RemoveInfiniteAmmunitionBuff();
        }
    }

}


