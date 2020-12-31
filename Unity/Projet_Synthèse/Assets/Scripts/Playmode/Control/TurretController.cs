using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/TurretController")]
    public class TurretController : GameScript
    {

        private KillableObject healthControl;
        private RangedWeapon rangedWeapon;
        private FaceNearbyEnemy enemyTargetFinder;
        

        private void InjectTurretController([GameObjectScope] KillableObject health,
            [EntityScope] FaceNearbyEnemy targetFinder,
            [EntityScope] RangedWeapon rangedWeapon)
        {
            healthControl = health;
            enemyTargetFinder = targetFinder;
            this.rangedWeapon = rangedWeapon;
        }

        private void Awake()
        {
            InjectDependencies("InjectTurretController");
        }

        private void Update()
        {
            if (enemyTargetFinder.GetTarget() != null)
            {
                rangedWeapon.Fire();
            }
            
        }

        public void OnHitReceive(int damagePoints)
        {
            healthControl.ReceiveDamage(damagePoints);
        }


        


    }

}

