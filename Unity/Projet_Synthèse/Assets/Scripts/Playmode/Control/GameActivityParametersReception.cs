using UnityEngine;
using Harmony;

namespace ProjetSynthese
{
    public class GameActivityParametersReception : GameScript
    {
        private GameActivityParameters gameActivityParameters;
        public GameActivityParameters GameActivityParameters
        {
            get { return gameActivityParameters;  } 
            set { gameActivityParameters = value; } 
        }


        private void InjectParametersFromMenu([ApplicationScope] GameActivityParameters gameActivityParameters)
        {
            this.gameActivityParameters = gameActivityParameters;
        }

        private void Awake()
        {
            InjectDependencies("InjectParametersFromMenu");
            KillUselessPlayers();
        }

        private void KillUselessPlayers()
        {
            if (gameActivityParameters.NumberOfPlayers == 1)
            {
                Destroy(GameObject.Find("Player_2"));
                Destroy(GameObject.Find("Player_3"));
                Destroy(GameObject.Find("Player_4"));
            }
            if (gameActivityParameters.NumberOfPlayers == 2)
            {
                Destroy(GameObject.Find("Player_3"));
                Destroy(GameObject.Find("Player_4"));
            }
            else if(gameActivityParameters.NumberOfPlayers == 3)
            {
                Destroy(GameObject.Find("Player_4"));
            }
        }
    }
}