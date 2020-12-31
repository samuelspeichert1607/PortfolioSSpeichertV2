using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class MyrmidonController : MonsterController
    {
        private MeleeWeaponController meleeWeaponController;
        private int attackRate = 5;
        private float timeActionEnd;

        private void InjectMyrmidonController([EntityScope] MeleeWeaponController meleeWeaponController)
        {
            this.meleeWeaponController = meleeWeaponController;
        }

        private void Awake()
        {
            InjectDependencies("InjectMyrmidonController");
            timeActionEnd = Time.time;
            base.Awake();
        }

        protected override void ExecuteAttack()
        {
            if (timeActionEnd < Time.time)
            {
                meleeWeaponController.Attack();
                timeActionEnd = Time.time + attackRate;
            }
        }
    }
}


