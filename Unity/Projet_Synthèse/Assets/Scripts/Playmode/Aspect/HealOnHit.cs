using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/HealOnHit")]
    public class HealOnHit : GameScript
    {
        private KillableObject health;
        private HitSensor hitSensor;

        private void InjectDamageOnHit([EntityScope] KillableObject health, [EntityScope] HitSensor hitSensor)
        {
            this.health = health;
            this.hitSensor = hitSensor;
        }

        private void Awake()
        {
            InjectDependencies("InjectDamageOnHit");
        }

        private void OnEnable()
        {
            hitSensor.OnHeal += OnHeal;
        }

        private void OnDisable()
        {
            hitSensor.OnHeal -= OnHeal;
        }

        private void OnHeal(int hitPoints)
        {
            health.Heal(hitPoints);
        }
    }
}