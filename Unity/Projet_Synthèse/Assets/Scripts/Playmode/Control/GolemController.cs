using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class GolemController : MonsterController
    {
        private MeleeWeaponController meleeWeaponController;

        private void InjectGolemController([EntityScope] MeleeWeaponController meleeWeaponController)
        {
            this.meleeWeaponController = meleeWeaponController;
        }

        private void Awake()
        {
            InjectDependencies("InjectGolemController");
            base.Awake();
        }

        protected override void ExecuteAttack()
        {
            meleeWeaponController.Attack();
        }
    }
}
