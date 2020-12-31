using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/SplitOnDetect")]
    public class SplitOnDetect : GameScript
    {
        [SerializeField]
        private float delayBeforeSplitInSeconds;

        private EntityDestroyer death;
        private RangedWeapon rangedWeapon;
        private TargetFinder targetFinder;

        public void InjectSplitAfterDelay([EntityScope] EntityDestroyer entityDestroyer, 
            [EntityScope] RangedWeapon rangedWeapon,
            [EntityScope] TargetFinder targetFinder)
        {
            death = entityDestroyer;
            this.rangedWeapon = rangedWeapon;
            this.targetFinder = targetFinder;
        }

        private void Awake()
        {
            InjectDependencies("InjectSplitAfterDelay");
        }

        private void Update()
        {
            if (targetFinder.GetTarget() != null)
            {
                rangedWeapon.Fire();
                death.Destroy();
            }
        }
    }
}
