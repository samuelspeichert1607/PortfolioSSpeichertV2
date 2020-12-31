using System;
using UnityEngine;
using Harmony;

namespace ProjetSynthese
{
    public delegate void ArtefactDetectStimulusEventHandler(GameObject player);

    [AddComponentMenu("Game/Stimulus/ArtefactDetectStimulus")]
    public class ArtefactDetectStimulus : GameScript
    {
        private new Collider2D collider2D;

        public event ArtefactDetectStimulusEventHandler OnArtefactDetectEnter;
        public event ArtefactDetectStimulusEventHandler OnArtefactDetectExit;

        private void InjectArtefactDetectStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectArtefactDetectStimulus");

            int layer = LayerMask.NameToLayer(R.S.Layer.ArtefactDetectSensor);
            if (layer == -1)
            {
                throw new Exception("In order to use a PlayerDetectStimulus, you must have a " + R.S.Layer.ArtefactDetectSensor + " layer.");
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
            ArtefactDetectSensor artefactDetectSensor = other.GetComponent<ArtefactDetectSensor>();
            if (artefactDetectSensor != null)
            {
                artefactDetectSensor.ArtefactDetectEnter(this.gameObject);
                
                Destroy(gameObject.GetTopParent());
                GameObject canvas = GameObject.Find("CanvasArtefact");
                if(canvas != null)
                {
                    canvas.GetComponent<CanvasArtefactController>().ArtefactsCollected += 1;
                }

                if (OnArtefactDetectEnter != null) OnArtefactDetectEnter(this.gameObject);
            }
        }

        private void OnExitTrigger(Collider2D other)
        {
            ArtefactDetectSensor artefactDetectSensor = other.GetComponent<ArtefactDetectSensor>();
            if (artefactDetectSensor != null)
            {
                artefactDetectSensor.ArtefactDetectExit(this.gameObject);

                if (OnArtefactDetectExit != null) OnArtefactDetectExit(this.gameObject);
            }
        }
    }
}
