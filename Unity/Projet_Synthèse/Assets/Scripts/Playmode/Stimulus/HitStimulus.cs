using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void HitStimulusEventHandler();

    [AddComponentMenu("Game/Stimulus/HitStimulus")]
    public class HitStimulus : GameScript
    {

        [SerializeField]
        [Tooltip("The affected target by the stimulus")]
        protected R.E.Layer target;

        [SerializeField]
        [Tooltip("Effects applied on hit")]
        protected Effect[] appliedEffectsOnhit;
        

        protected new Collider2D collider2D;

        public event HitStimulusEventHandler OnHit;

        protected void InjectHitStimulus([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        protected void Awake()
        {
            Inject();

            int layer = LayerMask.NameToLayer(R.S.Layer.HitStimulus);
            if (layer == -1)
            {
                throw new Exception("In order to use a HitStimulus, you must have a " + R.S.Layer.HitStimulus + " layer.");
            }
            gameObject.layer = layer;
        }

        private void Inject()
        {
            InjectDependencies("InjectHitStimulus");
        }

        protected virtual void OnEnable()
        {
            collider2D.Events().OnEnterTrigger += OnEnterTrigger;
        }

        protected virtual void OnDisable()
        {
            collider2D.Events().OnEnterTrigger -= OnEnterTrigger;
        }

        private void OnEnterTrigger(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(target.ToString()))
            {
                HitSensor hitSensor = other.GetComponent<HitSensor>();
                for (int i = 0; i < appliedEffectsOnhit.Length; i++)
                {
                    hitSensor.ApplyEffect(appliedEffectsOnhit[i], gameObject.GetTopParent());
                }
               NotifyHit();
            }
            

        }

        protected void NotifyHit()
        {
            if (OnHit != null) OnHit();
        }
    }



}
