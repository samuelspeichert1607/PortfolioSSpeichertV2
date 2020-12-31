using System.Collections;
using System.Collections.Generic;
using Harmony;
using NSubstitute.Exceptions;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "ArmorBuff", menuName = "Game/Effects/ArmorBonus")]
    public class EffectBuffArmor : Effect
    {
        [SerializeField]
        [Tooltip("Potencity of the armor bonus effect")]
        private int armorBonusStrenght;

        [SerializeField]
        [Tooltip("Duration of the armor bonus effect")]
        private float duration;

        [SerializeField]
        [Tooltip("Is the armor bonus flat value or percentage?")]
        private bool isPercentage;
        

        public override void Apply(GameObject caster, GameObject target)
        {
            KillableObject lifeControl = target.GetComponent<KillableObject>();
            lifeControl.StartCoroutine(TemporaryArmorBonus(lifeControl,armorBonusStrenght,isPercentage,duration));
        }

        private IEnumerator TemporaryArmorBonus(KillableObject lifeController, int armorBonus, bool isPercentage, float duration)
        {
            lifeController.IncreaseArmor(armorBonus,isPercentage);
            lifeController.ResetArmor();

            yield return new WaitForSeconds(duration);

            lifeController.ReduceArmor(armorBonus,isPercentage);
        }


    }

}


