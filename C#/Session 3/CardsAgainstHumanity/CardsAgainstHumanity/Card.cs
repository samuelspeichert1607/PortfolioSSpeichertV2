using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAgainstHumanity
{
    

    class Card
    {
        public Card(int cardID, string text)
        {
            this.cardID = cardID;
            this.text = text;
        }


       
        public int cardID = -1;
        public string text = "";
        Player ownwer = null;

        //string family = "Cards Against Humanity";


        //string[,] textLanguage = new string[3, 2] { { "one", "two" }, { "three", "four" }, { "five", "six" } };
        //bool writableCard = false;
       





    }
}
