using UnityEngine;
using Harmony;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/SplitOnCollision")]
    public class SplitOnHit : GameScript
    {

        private EntityDestroyer death;
        private Gun gun;
        private HitStimulus hitStimulus;

        public void InjectSplitAfterDelay([EntityScope] EntityDestroyer entityDestroyer,
                                        [EntityScope] Gun gun,
                                        [EntityScope] HitStimulus hitStimulus)
        {
            this.hitStimulus = hitStimulus;
            death = entityDestroyer;
            this.gun = gun;
        }

        private void Awake()
        {
            InjectDependencies("InjectSplitAfterDelay");
        }

        private void OnEnable()
        {
            hitStimulus.OnHit += OnHit;
        }

        private void OnDisable()
        {
            hitStimulus.OnHit -= OnHit;
        }

        private void OnHit()
        {
            gun.Fire();
            death.Destroy();
        }
    }

}


