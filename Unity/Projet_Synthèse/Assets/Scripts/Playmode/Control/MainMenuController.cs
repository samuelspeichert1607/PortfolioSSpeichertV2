using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/MainMenuController")]
    public class MainMenuController : GameScript, IMenuController
    {
        [SerializeField]
        private Activity gameActivity;

        [SerializeField]
        private Selectable buttonPlayer1;

        [SerializeField]
        private Selectable buttonPlayer2;

        [SerializeField]
        private Selectable buttonPlayer3;

        [SerializeField]
        private Selectable buttonPlayer4;

        private ActivityStack activityStack;

        private GameActivityParameters gameActivityParameters;

        private void InjectMainMenuController([ApplicationScope] ActivityStack activityStack, [ApplicationScope] GameActivityParameters gameActivityParameters)
        {
            this.activityStack = activityStack;
            this.gameActivityParameters = gameActivityParameters;
        }

        private void Awake()
        {
            InjectDependencies("InjectMainMenuController");
        }

        public void OnCreate(params object[] parameters)
        {
            //Nothing to do
        }

        public void OnResume()
        {
            buttonPlayer1.Select();
        }

        public void OnPause()
        {
            //Nothing to do
        }

        public void OnStop()
        {
            //Nothing to do
        }

        [CalledOutsideOfCode]
        public void StartGame1()
        {
            gameActivityParameters.NumberOfPlayers = 1;
            activityStack.StartActivity(gameActivity);
        }

        [CalledOutsideOfCode]
        public void StartGame2()
        {
            gameActivityParameters.NumberOfPlayers = 2;
            activityStack.StartActivity(gameActivity);
        }

        [CalledOutsideOfCode]
        public void StartGame3()
        {
            gameActivityParameters.NumberOfPlayers = 3;
            activityStack.StartActivity(gameActivity);
        }

        [CalledOutsideOfCode]
        public void StartGame4()
        {
            gameActivityParameters.NumberOfPlayers = 4;
            activityStack.StartActivity(gameActivity);
        }

        [CalledOutsideOfCode]
        public void QuitGame()
        {
            activityStack.StopCurrentActivity();
        }
    }
}