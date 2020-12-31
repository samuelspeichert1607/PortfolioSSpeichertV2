using UnityEngine;

namespace ProjetSynthese
{
    public class GameActivityParameters : GameScript
    {
        [SerializeField]
        [Tooltip("Number of players we want to begin the game.")]
        private int numberOfPlayers = 4;
        public int NumberOfPlayers
        {
            get { return numberOfPlayers; }
            set { numberOfPlayers = value; }
        }
    }
}