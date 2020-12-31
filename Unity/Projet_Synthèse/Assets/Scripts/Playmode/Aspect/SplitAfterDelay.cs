using UnityEngine;
using System.Collections;
using Harmony;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/SplitAfterDelay")]
    public class SplitAfterDelay : GameScript
    {
        [SerializeField]
        private float delayBeforeSplitInSeconds;

        private EntityDestroyer death;
        private RangedWeapon rangedWeapon;

        public void InjectSplitAfterDelay([EntityScope] EntityDestroyer entityDestroyer, [EntityScope] RangedWeapon rangedWeapon)
        {
            death = entityDestroyer;
            this.rangedWeapon = rangedWeapon;
        }

        private void Awake()
        {
            InjectDependencies("InjectSplitAfterDelay");
        }

        private void Start()
        {
            StartCoroutine(DestroyAfterDelayRoutine());
        }

        private IEnumerator DestroyAfterDelayRoutine()
        {
            yield return new WaitForSeconds(delayBeforeSplitInSeconds);
            rangedWeapon.Fire();
            death.Destroy();
        }
    }
}
