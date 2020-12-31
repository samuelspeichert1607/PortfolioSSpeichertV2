using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Immobilize", menuName = "Game/Effects/Immobilize")]
    public class EffectImmobilize : Effect
    {
        [SerializeField]
        [Range(0, 3600)]
        [Tooltip("Immobilize effect duration in seconds")]
        private float durationInSeconds;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
            stats.StartCoroutine(TemporaryImmobilize(stats, durationInSeconds));
        }

        private IEnumerator TemporaryImmobilize(CharacterStatistics entityStats, float duration)
        {
            entityStats.AddImmobility();
            yield return new WaitForSeconds(duration);
            entityStats.RemoveImmobility();
        }
    }

}


