using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/KeyController")]
    public class KeyController : GameScript
    {
        [SerializeField]
        [Tooltip("Door that the key belongs to")]
        private GameObject door;

        private PlayerDetectSensor playerSensor;

        private GameObject topParent;

        private void InjectKeyController([EntityScope] PlayerDetectSensor playerSensor,
                                         [TopParentScope] GameObject topParent)
        {
            this.playerSensor = playerSensor;
            this.topParent = topParent;

        }

        private void Awake()
        {
            InjectDependencies("InjectKeyController");
        }

        private void OnEnable()
        {
            playerSensor.OnDetectPlayerEnter += KeyTaken;
        }

        private void OnDisable()
        {
            playerSensor.OnDetectPlayerEnter -= KeyTaken;
        }

        public void SetDoor(GameObject door)
        {
            this.door = door; 
        }

        private void KeyTaken(GameObject player)
        {
            Destroy(door);
            Destroy(topParent);
        }
    }
}
