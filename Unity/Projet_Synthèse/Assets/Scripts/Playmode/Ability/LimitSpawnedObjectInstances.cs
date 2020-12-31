using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;


namespace ProjetSynthese
{
    [AddComponentMenu("Game/Ability/SummonLimit(Ability)")]
    public class LimitSpawnedObjectInstances : Ability
    {
        [SerializeField]
        [Tooltip("Ability to cast")]
        private Ability ability;

        [SerializeField]
        [Tooltip("The tag exclusive to the spawned item")]
        private R.E.Tag tag;

        [SerializeField]
        [Tooltip("The quantity of summons")]
        private int instanceLimit;

        public override void Cast()
        {
            if (GameObject.FindGameObjectsWithTag(tag.ToString()).Length < instanceLimit)
            {
                ability.SetTarget(target);
                ability.Cast();
            }
        }

        public override Ability GetNextOnStack()
        {
            return ability;
        }
    }

}