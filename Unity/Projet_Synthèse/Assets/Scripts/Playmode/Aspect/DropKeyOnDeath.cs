using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/DropKeyOnDeath")]
    public class DropKeyOnDeath : GameScript
    {

        [SerializeField]
        [Tooltip("prefab of a key")]
        private GameObject key;

        [SerializeField]
        [Tooltip("Door that the key belongs to")]
        private GameObject door;

        private KillableObject health;

        private void InjectDropKey([EntityScope] KillableObject health)
        {
            this.health = health;
        }

        private void Awake()
        {
            InjectDependencies("InjectDropKey");
        }

        private void OnEnable()
        {
            health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            GameObject createdKey = Instantiate(key, gameObject.transform.position, gameObject.transform.rotation);
            createdKey.GetComponentInChildren<KeyController>().SetDoor(door);
        }
    }
}
