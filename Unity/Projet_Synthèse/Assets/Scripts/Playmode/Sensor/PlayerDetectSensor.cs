using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void PlayerDetectSensorEventHandler(GameObject player);

    [AddComponentMenu("Game/Sensor/PlayerDetectSensor")]
    public class PlayerDetectSensor : GameScript
    {
        private new Collider2D collider2D;

        public event PlayerDetectSensorEventHandler OnDetectPlayerEnter;
        public event PlayerDetectSensorEventHandler OnDetectPlayerExit;


        private void InjectPlayerDetectSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerDetectSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.PlayerDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetect, you must have a " + R.S.Layer.PlayerDetectSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        public void PlayerDetectEnter(GameObject player)
        {
            if (OnDetectPlayerEnter != null) OnDetectPlayerEnter(player);
        }

        public void PlayerDetectExit(GameObject player)
        {
            if (OnDetectPlayerExit != null) OnDetectPlayerExit(player);
        }
    }
}
