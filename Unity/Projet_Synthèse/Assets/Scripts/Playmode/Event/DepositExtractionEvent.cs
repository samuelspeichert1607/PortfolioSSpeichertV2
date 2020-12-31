using System.Diagnostics;
using Harmony;

namespace ProjetSynthese
{
    public class DepositExtractionEvent : IEvent
    {
        public int MetalAmountRecolted { get; private set; }

        public DepositExtractionEvent(int metalAmountRecolted)
        {
            MetalAmountRecolted = metalAmountRecolted;
        }
    }
}


