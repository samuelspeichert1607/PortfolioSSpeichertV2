using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetSynthese
{
    public class InstantiateOnStart : GameScript
    {

        [SerializeField]
        [Tooltip("Object that will be spawned instantly")]
        private GameObject objectToSpawn;

        private void Start()
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

}


