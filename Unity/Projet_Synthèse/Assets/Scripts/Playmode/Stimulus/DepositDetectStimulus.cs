using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void DepositDetectStimulusEventHandler(GameObject player);

    [AddComponentMenu("Game/Stimulus/DepositDetectStimulus")]
    public class DepositDetectStimulus : GameScript
    {
        private new Collider2D collider2D;

        public event DepositDetectStimulusEventHandler OnDepositDetectEnter;
        public event DepositDetectStimulusEventHandler OnDepositDetectExit;

        private void InjectDepositDetectStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectDepositDetectStimulus");

            int layer = LayerMask.NameToLayer(R.S.Layer.DepositDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetectStimulus, you must have a " + R.S.Layer.DepositDetectSensor + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        private void OnEnable()
        {
            collider2D.Events().OnEnterTrigger += OnEnterTrigger;
            collider2D.Events().OnExitTrigger += OnExitTrigger;
        }

        private void OnDisable()
        {
            collider2D.Events().OnEnterTrigger -= OnEnterTrigger;
            collider2D.Events().OnExitTrigger -= OnExitTrigger;
        }

        private void OnEnterTrigger(Collider2D other)
        {
            DepositDetectSensor depositDetectSensor = other.GetComponent<DepositDetectSensor>();
            if (depositDetectSensor != null)
            {
                depositDetectSensor.DepositDetectEnter(this.gameObject);

                if (OnDepositDetectEnter != null) OnDepositDetectEnter(this.gameObject);
            }
        }

        private void OnExitTrigger(Collider2D other)
        {
            DepositDetectSensor depositDetectSensor = other.GetComponent<DepositDetectSensor>();
            if (depositDetectSensor != null)
            {
                depositDetectSensor.DepositDetectExit(this.gameObject);

                if (OnDepositDetectExit != null) OnDepositDetectExit(this.gameObject);
            }
        }
    }
}
