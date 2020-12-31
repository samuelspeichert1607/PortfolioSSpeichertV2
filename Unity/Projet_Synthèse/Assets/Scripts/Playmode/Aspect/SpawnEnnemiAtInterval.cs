using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/SpawnEnnemiAtInterval")]
    public class SpawnEnnemiAtInterval : GameScript
    {
        [SerializeField]
        [Tooltip("Monsters to spawn")]
        private GameObject[] monsters;

        [SerializeField]
        [Tooltip("Time between each spawn")]
        private float interval;

        [SerializeField]
        [Tooltip("Maximum number of spawned monsters that can be alive")]
        private int nbMonstersMax;

        private List<GameObject> spawnedMonsters;

        private float currentTime;

        private void Start()
        {
            spawnedMonsters = new List<GameObject>();
            currentTime = 0;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            List<GameObject> monstersToRemove=new List<GameObject>();

            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                if (spawnedMonsters[i] == null)
                {
                    monstersToRemove.Add(spawnedMonsters[i]);
                }
            }
            for (int i = 0; i < monstersToRemove.Count; i++)
            {
                spawnedMonsters.Remove(monstersToRemove[i]);
            }

            if (spawnedMonsters.Count < nbMonstersMax && currentTime >= interval)
            {
                int noMonsterToSpawn = (int)Random.Range(0, monsters.Length - 1);
                spawnedMonsters.Add(Instantiate(monsters[noMonsterToSpawn], gameObject.transform.position, gameObject.transform.rotation));
                currentTime = 0;
            }
        }
    }
}
