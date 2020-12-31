using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "DamagesEffect", menuName = "Game/Effects/DamagesModifier")]
    public class EffectDamagesModifier : Effect
    {
        [SerializeField]
        [Tooltip("Potencity of the damage bonus effect")]
        private int damageModifierStrenght;

        [SerializeField]
        [Tooltip("Duration of the damage bonus effect")]
        private float duration;

        [SerializeField]
        [Tooltip("Is the damage bonus flat value or percentage?")]
        private bool isPercentage;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponentInChildren<CharacterStatistics>();
            stats.StartCoroutine(DamageModifier(stats, damageModifierStrenght, duration, isPercentage));
        }

        private IEnumerator DamageModifier(CharacterStatistics stats, int potencity, float duration, bool isPercentage)
        {
            if (isPercentage)
            {
                stats.IncreaseDamageMultiplier(potencity);
            }
            else
            {
                stats.IncreaseDamageBonus(potencity);
            }
            yield return new WaitForSeconds(duration);
            if (isPercentage)
            {
                stats.DecreaseDamageMultiplier(potencity);
            }
            else
            {
                stats.DecreaseDamageBonus(potencity);
            }
        }
    }

}


