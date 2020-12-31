using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void PlayerInRangeSensorEventHandler(GameObject player);

    [AddComponentMenu("Game/Sensor/PlayerInRangeSensor")]
    public class PlayerInRangeSensor : GameScript
    {
        private new Collider2D collider2D;

        public event PlayerInRangeSensorEventHandler OnPlayerInRangeEnter;
        public event PlayerInRangeSensorEventHandler OnPlayerInRangeExit;


        private void InjectPlayerInRangeSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerInRangeSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.PlayerDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetect, you must have a " + R.S.Layer.PlayerDetectSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        public void PlayerInRangeEnter(GameObject player)
        {
            if (OnPlayerInRangeEnter != null) OnPlayerInRangeEnter(player);
        }

        public void PlayerInRangeExit(GameObject player)
        {
            if (OnPlayerInRangeExit != null) OnPlayerInRangeExit(player);
        }
    }
}
