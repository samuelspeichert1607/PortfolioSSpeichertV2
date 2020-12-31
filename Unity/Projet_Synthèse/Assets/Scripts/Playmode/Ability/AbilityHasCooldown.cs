using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/Cooldown(Ability)")]
    public class AbilityHasCooldown : Ability
    {
        [SerializeField]
        [Tooltip("Defines how long the cooldown of the ability is")]
        private int cooldownDuration;

        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;

        private bool isOnCooldown = false;
        private Coroutine cooldownCoroutine;

        private void Start()
        {
            
        }

        public override void Cast()
        {
            if (!isOnCooldown)
            {
                cooldownCoroutine = StartCoroutine(StartCooldown());
                ability.SetTarget(target);
                ability.Cast();
            }
            
        }

        public override Ability GetNextOnStack()
        {
            return ability;
        }

        public void ResetCooldown()
        {
            if (cooldownCoroutine != null)
            {
                StopCoroutine(cooldownCoroutine);
            }
            isOnCooldown = false;
        }

        private IEnumerator StartCooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            isOnCooldown = false;
        }
    }

}



