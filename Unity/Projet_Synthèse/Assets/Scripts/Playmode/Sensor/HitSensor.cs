using System;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void HitSensorEventHandler(int hitPoints);


    [AddComponentMenu("Game/Sensor/HitSensor")]
    public class HitSensor : GameScript
    {
        [SerializeField]
        [Tooltip("Which Layer should the Hit Sensor use?")]
        private R.E.Layer layer = R.E.Layer.EnemyHitSensor;

        private new Collider2D collider2D;

        public event HitSensorEventHandler OnHit;
        public event HitSensorEventHandler OnHeal;

        private void InjectHitSensor([GameObjectScope] Collider2D collider2D)
        {
            this.collider2D = collider2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectHitSensor");

            int layer = LayerMask.NameToLayer(this.layer.ToString());
            if (layer == -1)
            {
                throw new Exception("In order to use a HitSensor, you must have a " + this.layer + " layer.");
            }
            collider2D.isTrigger = true;
            gameObject.layer = layer;
        }

        public void ApplyEffect(Effect effect, GameObject caster)
        {
            effect.Apply(caster,gameObject.GetTopParent());
        }
    }

}