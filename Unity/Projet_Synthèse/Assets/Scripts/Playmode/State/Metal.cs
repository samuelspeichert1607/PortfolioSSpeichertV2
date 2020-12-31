using UnityEngine;

namespace ProjetSynthese
{
    public delegate void MetalQuantityChangedEventHandler(int oldMetalQuantity, int newMetalQuantity);
    public delegate void MetalQuantityDepletedEventHandler();
    public delegate void MetalQuantityFilledEventHandler();

    [AddComponentMenu("Game/State/Metal")]
    public class Metal : GameScript
    {
        [SerializeField]
        [Tooltip("Maximal metal quantity in object.")]
        private int maximalMetalQuantity;

        [SerializeField]
        [Tooltip("True if the object is the metal quantity for the submarine.")]
        private bool isSubmarine;

        private int metalQuantity;
        private int initialMetalQuantity;

        public event MetalQuantityChangedEventHandler OnMetalQuantityChanged;
        public event MetalQuantityDepletedEventHandler OnMetalQuantityDepleted;
        public event MetalQuantityFilledEventHandler OnMetalQuantityFilled;

        public int MetalQuantity
        {
            get { return metalQuantity; }
            private set
            {
                int oldMetalQuantity = metalQuantity;
                metalQuantity = value < 0 ? 0 : (value > maximalMetalQuantity ? maximalMetalQuantity : value);
                if (OnMetalQuantityChanged != null) OnMetalQuantityChanged(oldMetalQuantity, metalQuantity);
                if (metalQuantity <= 0 && OnMetalQuantityDepleted != null)
                {
                    OnMetalQuantityDepleted();
                }
                else if (metalQuantity == maximalMetalQuantity && OnMetalQuantityFilled != null)
                {
                    OnMetalQuantityFilled();
                }
            }
        }

        public int MaximalMetalQuantity
        {
            get { return maximalMetalQuantity; }
            set { maximalMetalQuantity = value; }
        }

        private void Awake()
        {
            if (isSubmarine)
            {
                MetalQuantity = maximalMetalQuantity/4;
            }
            else
            {
                MetalQuantity = maximalMetalQuantity;
            }
            initialMetalQuantity = MetalQuantity;
        }

        public void ReduceMetalQuantity(int amount)
        {
            MetalQuantity -= amount;
        }

        public void IncreaseMetalQuantity(int amount)
        {
            MetalQuantity += amount;
        }

        public void Reset()
        {
            MetalQuantity = initialMetalQuantity;
        }
    }
}