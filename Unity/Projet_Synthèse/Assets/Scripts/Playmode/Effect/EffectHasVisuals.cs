using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [CreateAssetMenu(fileName = "Particles", menuName = "Game/Effects/VisualParticles")]
    public class EffectHasVisuals : Effect
    {
        [SerializeField]
        [Tooltip("Gameobject with a particle effect to play as ability visuals")]
        private GameObject particleEffect;

        public override void Apply(GameObject caster, GameObject target)
        {
            GameObject effect = Instantiate(particleEffect, target.transform.position, target.transform.rotation);
            effect.transform.parent = target.transform;
            ParticleSystem particles = effect.GetComponent<ParticleSystem>();
            particles.Stop();
            particles.Play();
        }
    }

}

