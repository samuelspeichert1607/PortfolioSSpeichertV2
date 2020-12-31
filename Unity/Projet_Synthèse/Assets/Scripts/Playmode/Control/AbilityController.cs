using System.Collections;
using System.Collections.Generic;
using ProjetSynthese;
using UnityEngine;


namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/AbilityController")]
    public class AbilityController : GameScript
    {
        [SerializeField]
        [Tooltip("First ability the entity has")]
        private GameObject firstAbilityPrefab;
        [SerializeField]
        [Tooltip("second ability the entity has")]
        private GameObject secondAbilityPrefab;

        private Ability firstAbilityInstance;
        private Ability secondAbilityInstance;


        private void Start()
        {
            GameObject ability;

            ability = Instantiate(firstAbilityPrefab, transform.position, transform.rotation);
            ability.transform.parent = transform;
            firstAbilityInstance = ability.GetComponent<AbilityCastPoint>();

            ability = Instantiate(secondAbilityPrefab, transform.position, transform.rotation);
            ability.transform.parent = transform;
            secondAbilityInstance = ability.GetComponent<AbilityCastPoint>();
        }

        public void CastFirstAbility()
        {
            firstAbilityInstance.Cast();
        }
        public void CastSecondAbility()
        {
            secondAbilityInstance.Cast();
        }

        public void ResetCooldownFirstAbility()
        {
            ResetCooldown(firstAbilityInstance);
        }

        public void ResetCooldownSecondAbility()
        {
            ResetCooldown(secondAbilityInstance);
        }

        private void ResetCooldown(Ability ability)
        {
            bool cooldownReset = false;
            while (ability != null && cooldownReset == false)
            {
                if (ability is AbilityHasCooldown)
                {
                    ((AbilityHasCooldown)ability).ResetCooldown();
                    cooldownReset = true;
                }
                else
                {
                    ability = ability.GetNextOnStack();
                }
            }
        }



    }


}

