using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Sensor/MetalSensor")]
    public class MetalSensor : GameScript
    {

        private new Collider2D collider2D;

        private DepositExtractionEventChannel eventChannel;

        private void InjectMetalSensor([GameObjectScope] Collider2D collider2D, [EventChannelScope] DepositExtractionEventChannel eventChannel)
        {
            this.collider2D = collider2D;
            this.eventChannel = eventChannel;
        }

        private void Awake()
        {
            InjectDependencies("InjectMetalSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.MetalSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a MetalSensor, you must have a " + R.S.Layer.MetalSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        public void MetalCollected(int nbMetal)
        {
            eventChannel.Publish(new DepositExtractionEvent(nbMetal));
        }
    }
}
