using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/GardianController")]
    public class GardianController : EnnemyController
    {

        [SerializeField]
        [Tooltip("Metal prefab that will be dropped.")]
        protected GameObject metalPrefab;

        [SerializeField]
        [Tooltip("Minimum metal quantity dropped on death.")]
        [Range(0,49)]
        protected int nbMinMetalToDrop;

        [SerializeField]
        [Tooltip("Maximum metal quantity dropped on death.")]
        [Range(1,50)]
        protected int nbMaxMetalToDrop;

        // Update is called once per frame
        protected override void Update()
        {
            State.Update();
        }

        protected override void Awake()
        {
            Inject();
            int nbMetal = Random.Range(nbMinMetalToDrop, nbMaxMetalToDrop);
            metalPrefab.GetComponent<MetalStimulus>().NbMetal = nbMetal;
        }

        protected override void OnDeath()
        {
            Instantiate(metalPrefab, gameObject.transform.position,gameObject.transform.rotation);
        }

        protected override void OnHealthChanged(int currentHealthPoints, int maxHealthPoints)
        {
        }

        protected override void OnPlayerEnterDetected(GameObject player)
        {
            State.OnPlayerEnterDetected(player);
        }

        protected override void OnPlayerExitDetected(GameObject player)
        {
            State.OnPlayerExitDetected(player);
        }

        public override void Move(Vector2 destination)
        {
            mover.Move(destination);
        }
    }
}
