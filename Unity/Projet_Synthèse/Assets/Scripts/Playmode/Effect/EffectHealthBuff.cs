using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "HealthBuff", menuName = "Game/Effects/HealthBonus")]
    public class EffectHealthBuff : Effect
    {
        [SerializeField]
        [Tooltip("Potencity of the health bonus effect")]
        private int healthBonusStrenght;

        [SerializeField]
        [Tooltip("Duration of the health bonus effect")]
        private float duration;

        [SerializeField]
        [Tooltip("Is the health bonus flat value or percentage?")]
        private bool isPercentage;

        private KillableObject lifeControl;

        public override void Apply(GameObject caster, GameObject target)
        {
            KillableObject lifeControl = target.GetComponent<KillableObject>();
            lifeControl.StartCoroutine(TemporaryHealthBonus(lifeControl, healthBonusStrenght, isPercentage, duration));
        }

        private IEnumerator TemporaryHealthBonus(KillableObject lifeController, int healthBonus, bool isPercentage, float duration)
        {
            lifeController.IncreaseHealth(healthBonus, isPercentage);
            lifeController.ResetLife();

            yield return new WaitForSeconds(duration);

            lifeController.DecreaseHealth(healthBonus, isPercentage);
        }


    }

}


