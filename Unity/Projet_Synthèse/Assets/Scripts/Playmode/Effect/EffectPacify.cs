using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Pacify", menuName = "Game/Effects/Pacify")]
    public class EffectPacify : Effect
    {
        [SerializeField]
        [Range(0, 3600)]
        [Tooltip("Pacify effect duration in seconds")]
        private float durationInSeconds;

        public override void Apply(GameObject caster, GameObject target)
        {
            CharacterStatistics stats = target.GetComponent<CharacterStatistics>();
            stats.StartCoroutine(TemporaryPacify(stats, durationInSeconds));
        }

        private IEnumerator TemporaryPacify(CharacterStatistics entityStats, float duration)
        {
            entityStats.AddPacification();
            yield return new WaitForSeconds(duration);
            entityStats.RemovePacification();
        }
    }

}


