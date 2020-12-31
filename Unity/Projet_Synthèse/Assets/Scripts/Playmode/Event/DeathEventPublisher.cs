using UnityEngine;
using Harmony;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Event/DeathEventPublisher")]
    public class DeathEventPublisher : GameScript
    {
        [SerializeField]
        private R.E.Prefab prefab;

        private KillableObject health;
        private DeathEventChannel eventChannel;

        private void InjectDeathEventPublisher([EntityScope] KillableObject health,[EventChannelScope] DeathEventChannel eventChannel)//
        {
            this.health = health;
            this.eventChannel = eventChannel;
        }

        private void Awake()
        {
            InjectDependencies("InjectDeathEventPublisher");
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
            eventChannel.Publish(new DeathEvent(prefab));
        }
    }
}