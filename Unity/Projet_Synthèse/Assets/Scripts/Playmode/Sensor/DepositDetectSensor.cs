using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void DepositDetectSensorEventHandler(GameObject player);

    [AddComponentMenu("Game/Sensor/DepositDetectSensor")]
    public class DepositDetectSensor : GameScript
    {
        private new Collider2D collider2D;

        public event DepositDetectSensorEventHandler OnDetectDepositEnter;
        public event DepositDetectSensorEventHandler OnDetectDepositExit;


        private void InjectDepositDetectSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectDepositDetectSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.DepositDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetect, you must have a " + R.S.Layer.DepositDetectSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        public void DepositDetectEnter(GameObject deposit)
        {
            if (OnDetectDepositEnter != null) OnDetectDepositEnter(deposit);
        }

        public void DepositDetectExit(GameObject deposit)
        {
            if (OnDetectDepositExit != null) OnDetectDepositExit(deposit);
        }
    }
}

