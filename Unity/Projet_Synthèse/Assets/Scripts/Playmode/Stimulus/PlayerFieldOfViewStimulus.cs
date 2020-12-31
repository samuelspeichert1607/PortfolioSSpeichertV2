using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void PlayerFieldOfViewStimulusEventHandler(GameObject fogParticle);

    [AddComponentMenu("Game/Stimulus/PlayerFieldOfViewStimulus")]
    public class PlayerFieldOfViewStimulus : GameScript
    {
        private new Collider2D collider2D;

        public event PlayerFieldOfViewStimulusEventHandler OnFogParticleDetectStay;

        private void InjectPlayerFieldOfViewStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerFieldOfViewStimulus");

            int layer = LayerMask.NameToLayer(R.S.Layer.PlayerFieldView);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetectStimulus, you must have a " + R.S.Layer.PlayerFieldView + " layer.");
            }
            gameObject.layer = layer;
            collider2D.isTrigger = true;
        }

        private void OnEnable()
        {
            collider2D.Events().OnStayTrigger += OnStayTrigger;
        }

        private void OnDisable()
        {
            collider2D.Events().OnStayTrigger -= OnStayTrigger;
        }

        private void OnStayTrigger(Collider2D other)
        {
            PlayerFieldOfViewSensor playerFieldOfViewSensor = other.GetComponent<PlayerFieldOfViewSensor>();
            if (playerFieldOfViewSensor != null)
            {
                playerFieldOfViewSensor.FogParticleDetectStay(this.gameObject);

                if (OnFogParticleDetectStay != null) OnFogParticleDetectStay(this.gameObject);
            }
        }
    }

}


