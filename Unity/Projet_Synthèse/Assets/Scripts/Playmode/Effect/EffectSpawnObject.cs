using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Game/Effects/SpawnObject")]
    public class EffectSpawnObject : Effect
    {
        [SerializeField]
        [Tooltip("The gameobject we wish to spawn")]
        private GameObject objectToSpawn;

        public override void Apply(GameObject caster, GameObject target)
        {
            Instantiate(objectToSpawn, caster.transform.position, caster.transform.rotation);
        }
    }

}

