using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class EndOfGameManager : GameScript
    {
        [SerializeField]
        private Menu winGameMenu;

        [SerializeField]
        private Menu lostGameMenu;

        private PlayerDeathEventChannel eventChannel;

        private GameActivityParameters gameActivityParameters;

        private ActivityStack activityStack;

        private SubmarineController submarineController;

        private int remainingPlayers;

        private bool gameHasBeenWon;

        public void InjectEndOfGameManager([ApplicationScope] GameActivityParameters gameActivityParameters,
                                           [EventChannelScope]PlayerDeathEventChannel eventChannel,
                                           [ApplicationScope] ActivityStack activityStack,
                                           [TagScope(R.S.Tag.Submarine)] SubmarineController submarineController)
        {
            this.eventChannel = eventChannel;
            this.gameActivityParameters = gameActivityParameters;
            this.activityStack = activityStack;
            this.submarineController = submarineController;
        }
        
        private void Awake()
        {
            InjectDependencies("InjectEndOfGameManager");

            remainingPlayers = gameActivityParameters.NumberOfPlayers;

            gameHasBeenWon = false;
        }

        private void OnEnable()
        {
            eventChannel.OnEventPublished += OnDeath;
            submarineController.OnSubDestroyed += OnSubMarineDeath;
            submarineController.OnSubRepaired += OnSubMarineRepaired;
        }

        private void OnDisable()
        {
            eventChannel.OnEventPublished -= OnDeath;
            submarineController.OnSubDestroyed -= OnSubMarineDeath;
            submarineController.OnSubRepaired -= OnSubMarineRepaired;
        }

        private void OnDeath(PlayerDeathEvent playerDeathEvent)
        {
            remainingPlayers--;
            Debug.Log("A player has been killed...");
            if(remainingPlayers == 0)
            {
                activityStack.StartMenu(lostGameMenu);
                Debug.Log("All players were killed :'(...");
            }
        }

        private void OnSubMarineDeath()
        {
            activityStack.StartMenu(lostGameMenu);
        }

        private void OnSubMarineRepaired()
        {
            if(!gameHasBeenWon)
            {
                activityStack.StartMenu(winGameMenu);
                gameHasBeenWon = true;
            }
        }
    }
}


