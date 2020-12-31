using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Event/PlayerHealthEventPublisher")]
    public class PlayerHealthEventPublisher : GameScript
    {
        private KillableObject health;
        private PlayerHealthEventChannel eventChannel;

        private void InjectPlayerHealthEventPublisher([EntityScope] KillableObject health,
                                                     [EventChannelScope] PlayerHealthEventChannel eventChannel)
        {
            this.health = health;
            this.eventChannel = eventChannel;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerHealthEventPublisher");
        }

        private void OnEnable()
        {
            health.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            health.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int unitCurrentHealth, int unitMaxHealth)
        {
            eventChannel.Publish(new PlayerHealthEvent(health, unitCurrentHealth, unitMaxHealth));
        }
    }
}