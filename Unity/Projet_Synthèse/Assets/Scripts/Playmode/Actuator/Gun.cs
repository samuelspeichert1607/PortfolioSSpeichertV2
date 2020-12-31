using System.Collections;
using System.Collections.Generic;
using Harmony;
using ProjetSynthese;
using UnityEngine;

namespace ProjetSynthese
{

    [AddComponentMenu("Game/Actuator/Gun")]
    public class Gun : ReloadableWeapon
    {
        private float currentClip;

        private ProjectileShooter guntip;
        private CharacterStatistics stats;

        private void InjectGun([EntityScope] ProjectileShooter guntip,
            [EntityScope] CharacterStatistics stats)
        {
            this.guntip = guntip;
            this.stats = stats;
        }

        private void Awake()
        {
            InjectDependencies("InjectGun");
        }

        private void Start()
        {
            currentClip = clip;
        }

        public override void Fire()
        {
            if (!hasFired && !isReloading)
            {
                StartCoroutine(FireWeapon());
            }
        }

        protected override void ReloadCompleted()
        {
            currentClip = clip;
        }

        private IEnumerator FireWeapon()
        {
            
            if (currentClip > 0 || IsClipInfinite)
            {
                hasFired = true;
                for (int i = 0; i < multiFire; i++)
                {
                    GameObject bullet = guntip.Fire();
                    bullet.transform.Rotate(0, 0, Random.Range(-Spread / 2f, Spread / 2f));
                    bullet.GetComponentInChildren<CharacterStatistics>().ReceiveDamageClone(stats.CloneDamageBonus());

                }
                currentClip -= (IsClipInfinite) ? 0 : 1;

            }
            else
            {
                Reload();
            }
            yield return new WaitForSeconds(fireRate);
            hasFired = false;
        }

        
    }
}
