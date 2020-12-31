using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class HitStimulusOnStay : HitStimulus
    {

        protected override void OnEnable()
        {
            collider2D.Events().OnStayTrigger += OnStayTrigger;
        }

        protected override void OnDisable()
        {
            collider2D.Events().OnStayTrigger -= OnStayTrigger;
        }

        private void OnStayTrigger(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(target.ToString()))
            {
                HitSensor hitSensor = other.GetComponent<HitSensor>();
                foreach (Effect effect in appliedEffectsOnhit)
                {
                    hitSensor.ApplyEffect(effect, gameObject.GetTopParent());
                }
                NotifyHit();
            }
        }
    }

}


