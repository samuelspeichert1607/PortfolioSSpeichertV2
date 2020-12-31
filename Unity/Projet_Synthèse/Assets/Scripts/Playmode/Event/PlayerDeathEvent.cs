using Harmony;

namespace ProjetSynthese
{
    public class PlayerDeathEvent : IEvent
    {
        public bool PlayerDead { get; private set; }

        public PlayerDeathEvent()
        {
            PlayerDead = true;
        }
    }
}
