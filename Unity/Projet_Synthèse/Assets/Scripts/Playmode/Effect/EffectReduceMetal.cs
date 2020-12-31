using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "MetalCost", menuName = "Game/Effects/ReduceMetal")]
    public class EffectReduceMetal : Effect
    {
        [SerializeField]
        [Tooltip("The metal cost of the ability")]
        private int metalPrice;

        private void ReduceMetal()
        {
            Metal submarineMetal =
                GameObject.FindGameObjectWithTag(R.S.Tag.Submarine).GetComponentInChildren<Metal>();
            submarineMetal.ReduceMetalQuantity(metalPrice);
        }

        public override void Apply(GameObject caster, GameObject target)
        {
            ReduceMetal();
        }
    }

}

