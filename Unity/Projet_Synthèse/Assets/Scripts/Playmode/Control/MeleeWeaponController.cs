using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/MeleeWeaponController")]
    public class MeleeWeaponController : GameScript
    {
        private float timeActionEnd;

        private void Awake()
        {
            GetComponent<HitStimulusOnStay>().enabled = false;
        }

        private void Update()
        {
            if (GetComponent<HitStimulusOnStay>().enabled && 
                Time.time > timeActionEnd)
            {
                GetComponent<HitStimulusOnStay>().OnHit -= OnHitOccured;
                GetComponent<HitStimulusOnStay>().enabled = false;
            }
        }

        public void Attack()
        {
            if (GetComponent<HitStimulusOnStay>().enabled == false)
            {
                GetComponent<HitStimulusOnStay>().enabled = true;
                GetComponent<HitStimulusOnStay>().OnHit += OnHitOccured;
                timeActionEnd = Time.time + 1;
            }
        }

        private void OnHitOccured()
        {
            GetComponent<HitStimulusOnStay>().enabled = false;
        }

    }
}


