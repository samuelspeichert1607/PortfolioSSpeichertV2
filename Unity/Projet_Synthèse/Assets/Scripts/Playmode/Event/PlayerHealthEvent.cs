using Harmony;

namespace ProjetSynthese
{
    public class PlayerHealthEvent : IEvent
    {
        public KillableObject PlayerHealth { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        public int MaxHealthPoints { get; private set; }

        public PlayerHealthEvent(KillableObject playerHealth, int currentHealth, int maxHealth)
        {
            PlayerHealth = playerHealth;
            CurrentHealthPoints = currentHealth;
            MaxHealthPoints = maxHealth;
        }
    }
}