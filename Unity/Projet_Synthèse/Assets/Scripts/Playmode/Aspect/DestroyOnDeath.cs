using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Aspect/DestroyOnDeath")]
    public class DestroyOnDeath : GameScript
    {
        private KillableObject health;
        private EntityDestroyer entityDestroyer;

        private void InjectDestroyOnDeath([EntityScope] KillableObject health,[EntityScope] EntityDestroyer entityDestroyer) 
        {
            this.health = health;
            this.entityDestroyer = entityDestroyer;
        }

        private void Awake()
        {
            InjectDependencies("InjectDestroyOnDeath");
        }

        private void OnEnable()
        {
           health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
           health.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            entityDestroyer.Destroy();
        }
    }
}