using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void PlayerFieldOfViewSensorEventHandler(GameObject playerFieldOfView);

    [AddComponentMenu("Game/Sensor/PlayerFieldOfViewSensor")]
    public class PlayerFieldOfViewSensor : GameScript
    {
        private Collider2D collider2D;

        public event PlayerFieldOfViewSensorEventHandler OnDetectFogParticleStay;

        private void InjectPlayerFieldOfViewSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerFieldOfViewSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.FogOfWarGrid);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerFieldOfView, you must have a " + R.S.Layer.FogOfWarGrid + " layer.");
            }
            gameObject.layer = layer;
            //collider2D.isTrigger = true;
        }

        public void FogParticleDetectStay(GameObject playerFieldOfView)
        {
            if (OnDetectFogParticleStay != null) OnDetectFogParticleStay(playerFieldOfView);
        }
    }

}


