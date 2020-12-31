using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/State/PurchasableObject")]
    public class PurchasableObject : GameScript
    {

        [SerializeField]
        [Tooltip("The quantity of metal that should be spent when you purchase this item.")]
        [Range(1, 5000)]
        private int objectPrice;

        public int GetPrice()
        {
            return objectPrice;
        }

    }

}

