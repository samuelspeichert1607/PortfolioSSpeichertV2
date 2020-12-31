using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void HitDealt();

    public abstract class RangedWeapon : GameScript
    {
        [SerializeField]
        [Tooltip("The delay (in seconds) between two gun shots.")]
        protected float fireRate;

        [SerializeField]
        [Range(0, 360)]
        [Tooltip("The weapon innaccuracy. 0 means perfect accuracy. 360 means that it could fire in any random direction, really.")]
        protected float Spread;

        [SerializeField]
        [Range(1, 24)]
        [Tooltip("number of projectiles shot per tick.")]
        protected float multiFire = 1;

        protected bool hasFired = false;

        protected int multiplicativePowerBonus = 0;
        protected int additivePowerBonus = 0;

        public event HitDealt OnHitDealt;

        public abstract void Fire();
        
        
         
    }

}


