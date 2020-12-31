using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    public class TurretSpawner : GameScript
    {

        [SerializeField]
        private GameObject turretPrefab;

        private PurchasableObject turretPrice;

        private void Awake()
        {
            turretPrice = turretPrefab.GetComponentInChildren<PurchasableObject>();
        }

        public void Spawn()
        {
            SpawnAt(transform.position,Quaternion.Euler(Vector3.zero));
        }

        public void SpawnAt(Vector3 position, Quaternion rotation)
        {
            GameObject objectSpawned = Instantiate(turretPrefab, position, rotation);

        }

        public int GetTurretPrice()
        {
            return turretPrice.GetPrice();
        }
    }
}


