using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/MonsterController")]
    public class MonsterController : EnnemyController
    {
        protected void InjectMonsterState([TagScope(R.S.Tag.Submarine)] GameObject submarineController)
        {
            this.mainTarget = submarineController;
        }

        protected override void Awake()
        {
            InjectDependencies("InjectMonsterState");
            startState = StartingStates.Chase;
            base.Awake();
        }
    }
}