using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "SpeedBuff", menuName = "Game/Effects/BuffSpeed")]
    public class BuffSpeed : Effect
    {
        [SerializeField]
        [Tooltip("Potencity of the speed bonus effect")]
        private int intensity;

        [SerializeField]
        [Tooltip("Duration of the damage bonus effect")]
        private float duration;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics characterStats = target.GetComponentInChildren<CharacterStatistics>();
            characterStats.StartCoroutine(TemporarySpeedIncrease(characterStats, intensity, duration));
        }

        private IEnumerator TemporarySpeedIncrease(CharacterStatistics entityStats ,int intensity, float duration)
        {
            entityStats.IncreaseMovementSpeed(intensity);
            yield return new WaitForSeconds(duration);
            entityStats.DecreaseMovementSpeed(intensity);
        }
        
    }

}


