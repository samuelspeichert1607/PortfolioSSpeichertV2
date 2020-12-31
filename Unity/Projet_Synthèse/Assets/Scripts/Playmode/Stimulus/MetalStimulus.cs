using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Stimulus/MetalStimulus")]
    public class MetalStimulus : GameScript
    {

        private new Collider2D collider2D;

        [SerializeField]
        private int nbMetal;

        public int NbMetal
        {
            get
            {
                return nbMetal;
            }

            set
            {
                nbMetal = value;
            }
        }

        private void InjectMetalStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectMetalStimulus");

            int layer = LayerMask.NameToLayer(R.S.Layer.MetalSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a MetalStimulus, you must have a " + R.S.Layer.MetalSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        private void OnEnable()
        {
            collider2D.Events().OnEnterTrigger += OnEnterTrigger;
        }

        private void OnDisable()
        {
            collider2D.Events().OnEnterTrigger -= OnEnterTrigger;
        }

        private void OnEnterTrigger(Collider2D other)
        {
            MetalSensor metalSensor = other.GetComponent<MetalSensor>();
            if (metalSensor != null)
            {
                metalSensor.MetalCollected(NbMetal);
                Destroy(this.gameObject);
            }
        }

    }
}
