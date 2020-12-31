using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public abstract class ReloadableWeapon : RangedWeapon
    {
        [SerializeField]
        [Tooltip("number of bullets the gun have.")]
        protected float clip;

        [SerializeField]
        [Tooltip("The time it takes to reload once you run out of ammo.")]
        protected float reloadDelay;

        [SerializeField]
        [Tooltip("Check this boolean if you want the weapon to have ulimited ammo. Use only for NPCs or testing")]
        protected bool isClipInfinite = false;

        private bool isAmmoConsumed = true;
        private Coroutine infiniteAmmunitionBuff;

        protected bool IsClipInfinite { get { return (isClipInfinite || !isAmmoConsumed); } }

        protected bool isReloading = false;

        public void InfiniteAmmoBuff(float duration)
        {
            if (infiniteAmmunitionBuff != null)
            {
                StopCoroutine(infiniteAmmunitionBuff);
                infiniteAmmunitionBuff = null;
            }
            infiniteAmmunitionBuff = StartCoroutine(InfiniteAmmunition(duration));
        }

        protected abstract void ReloadCompleted();

        protected void Reload()
        {
            StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ReloadWeapon()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadDelay);
            isReloading = false;
            ReloadCompleted();
        }

        private IEnumerator InfiniteAmmunition(float duration)
        {
            isAmmoConsumed = false;
            yield return new WaitForSeconds(duration);
            isAmmoConsumed = true;
        }
        
    }

}


