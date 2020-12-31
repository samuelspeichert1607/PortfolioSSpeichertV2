using System;
using Harmony;
using UnityEngine;


namespace ProjetSynthese
{
    public delegate void ArtefactSensorEventHandler(GameObject player);

    [AddComponentMenu("Game/Sensor/ArtefactDetectSensor")]
    public class ArtefactDetectSensor : GameScript
    {
        private new Collider2D collider2D;

        public event ArtefactSensorEventHandler OnDetectArtefactEnter;
        public event ArtefactSensorEventHandler OnDetectArtefactExit;


        private void InjectArtefactDetectSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectArtefactDetectSensor");

            int layer = LayerMask.NameToLayer(R.S.Layer.ArtefactDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetect, you must have a " + R.S.Layer.ArtefactDetectSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        public void ArtefactDetectEnter(GameObject artefact)
        {
            if (OnDetectArtefactEnter != null) OnDetectArtefactEnter(artefact);
        }

        public void ArtefactDetectExit(GameObject artefact)
        {
            if (OnDetectArtefactExit != null) OnDetectArtefactExit(artefact);
        }
    }
}
