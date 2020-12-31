using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
        

    class Player
    {
        public string username = "";


        public bool isCardCzar = false;
        public int points = 0;
        public Card[] handCards = new Card[7];

        public Player(string username)
        {
            this.username = username;
        }
        
        









    }
}
