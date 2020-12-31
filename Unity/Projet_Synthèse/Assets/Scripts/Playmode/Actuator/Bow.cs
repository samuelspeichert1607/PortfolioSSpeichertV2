using System;
using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Actuator/Bow")]
    public class Bow : RangedWeapon
    {

        [SerializeField]
        [Range(0.001f, 1f)]
        [Tooltip("The bow minimum delay between arrow pulling ticks")]
        private float bowPullTickRate;

        [SerializeField]
        [Tooltip("The maximum pull ticks stored in the bow")]
        [Range(1, 2000)]
        private int maxPullTicks;

        [SerializeField]
        [Tooltip("The power multiplier for fully charging bow, in percentage")]
        [Range(10, 500)]
        private int bowPullBonus;

        private int currentCharge;

        private bool keepCharging = false;

        private ProjectileShooter bowtip;
        private CharacterStatistics stats;

        private void InjectGun([EntityScope] ProjectileShooter bowtip,
            [EntityScope] CharacterStatistics stats)
        {
            this.bowtip = bowtip;
            this.stats = stats;
        }

        private void Awake()
        {
            InjectDependencies("InjectGun");
        }

        public override void Fire()
        {
            if (!hasFired)
            {
                if (currentCharge < 1)
                {
                    currentCharge = 1;
                    keepCharging = true;
                    StartCoroutine(PullArrow());

                }
                else
                {
                    keepCharging = true;
                }
            }

        }

        public void ReleaseArrow()
        {
            for (int i = 0; i < multiFire; i++)
            {
                GameObject arrow = bowtip.Fire();
                arrow.transform.Rotate(0, 0, Random.Range(-Spread / 2f, Spread / 2f));
                stats.IncreaseDamageMultiplier(((bowPullBonus * currentCharge) / maxPullTicks) + multiplicativePowerBonus);
                arrow.GetComponentInChildren<CharacterStatistics>().ReceiveDamageClone(stats.CloneDamageBonus());
                stats.DecreaseDamageMultiplier(((bowPullBonus * currentCharge) / maxPullTicks) + multiplicativePowerBonus);
            }
            currentCharge = 0;
        }

        private IEnumerator PullArrow()
        {
            while (keepCharging)
            {
                keepCharging = false;
                currentCharge++;
                yield return new WaitForSeconds(bowPullTickRate);
            }
            hasFired = true;
            ReleaseArrow();
            yield return new WaitForSeconds(fireRate);
            hasFired = false;
        }

    }

}


