using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void PlayerDetectStimulusEventHandler(GameObject player);

    [AddComponentMenu("Game/Stimulus/PlayerDetectStimulus")]
    public class PlayerDetectStimulus : GameScript
    {

        private new Collider2D collider2D;

        public event PlayerDetectStimulusEventHandler OnPlayerDetectEnter;
        public event PlayerDetectStimulusEventHandler OnPlayerDetectExit;
        public event PlayerDetectStimulusEventHandler OnPlayerInRangeEnter;
        public event PlayerDetectStimulusEventHandler OnPlayerInRangeExit;

        private void InjectPlayerDetectStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerDetectStimulus");

            int layer = LayerMask.NameToLayer(R.S.Layer.PlayerDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetectStimulus, you must have a " + R.S.Layer.PlayerDetectSensor + " layer.");
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
            PlayerInRangeSensor playerInRangeSensor = other.GetComponent<PlayerInRangeSensor>();
            if (playerInRangeSensor != null)
            {
                playerInRangeSensor.PlayerInRangeEnter(this.gameObject);

                if (OnPlayerInRangeEnter != null) OnPlayerInRangeEnter(this.gameObject);
            }
            PlayerDetectSensor playerDetectSensor = other.GetComponent<PlayerDetectSensor>();
            if (playerDetectSensor != null)
            {
                playerDetectSensor.PlayerDetectEnter(this.gameObject);

                if (OnPlayerDetectEnter != null) OnPlayerDetectEnter(this.gameObject);
            }            
        }

        private void OnExitTrigger(Collider2D other)
        {
            PlayerInRangeSensor playerInRangeSensor = other.GetComponent<PlayerInRangeSensor>();
            if (playerInRangeSensor != null)
            {
                playerInRangeSensor.PlayerInRangeExit(this.gameObject);

                if (OnPlayerInRangeExit != null) OnPlayerInRangeExit(this.gameObject);
            }
            PlayerDetectSensor playerDetectSensor = other.GetComponent<PlayerDetectSensor>();
            if (playerDetectSensor != null)
            {
                playerDetectSensor.PlayerDetectExit(this.gameObject);

                if (OnPlayerDetectExit != null) OnPlayerDetectExit(this.gameObject);
            }            
        }
    }
}
