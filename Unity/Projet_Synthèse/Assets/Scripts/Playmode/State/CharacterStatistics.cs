using System.Collections;
using System.Collections.Generic;
using ProjetSynthese;
using UnityEngine;

namespace ProjetSynthese
{
    public class CharacterStatistics : GameScript
    {
        #region Stats



        private const int divider = 100;

        private int damageMultiplier = 100;
        private int damageBonus = 0;

        private const int minimumResistanceMultiplier = 10;
        private int resistanceMultiplier = 100;

        private int movementSpeedMultiplier = 100;

        private int immobilizationStacks = 0;
        private int infiniteAmmunitionBonusStacks = 0;
        private int pacificationStacks = 0;
        private int invisibilityStacks = 0;

        #endregion

        #region Stats Calculations

        public int CalculateDamageDealt(int hitPower)
        {
            return (hitPower * damageMultiplier / divider) + damageBonus;
        }

        public int CalculateDamageReceived(int hitReceived)
        {
            return hitReceived *
                (
                (resistanceMultiplier < minimumResistanceMultiplier) ?
                minimumResistanceMultiplier : resistanceMultiplier
                ) / divider;
        }

        public float CalculateMovementSpeed(float movementSpeed)
        {
            if (immobilizationStacks > 0)
            {
                return 0;
            }
            return movementSpeed * (float)movementSpeedMultiplier / (float)divider;
        }

        public bool HasInfiniteAmmunition()
        {
            return infiniteAmmunitionBonusStacks > 0;
        }

        public bool IsPacified()
        {
            return pacificationStacks > 0;
        }

        public bool IsInvisible()
        {
            return invisibilityStacks > 0;
        }

        #endregion

        #region Effects Additons & Substractions

        public void IncreaseDamageMultiplier(int percentage)
        {
            damageMultiplier += percentage;
        }
        public void DecreaseDamageMultiplier(int percentage)
        {
            damageMultiplier -= percentage;
        }

        public void IncreaseDamageBonus(int value)
        {
            damageBonus += value;
        }
        public void DecreaseDamageBonus(int value)
        {
            damageBonus -= value;
        }

        public void IncreaseResistanceMultiplier(int percentage)
        {
            resistanceMultiplier += percentage;
        }
        public void DecreaseResistanceMultiplier(int percentage)
        {
            resistanceMultiplier -= percentage;
        }

        public void IncreaseMovementSpeed(int percentage)
        {
            resistanceMultiplier += percentage;
        }
        public void DecreaseMovementSpeed(int percentage)
        {
            resistanceMultiplier -= percentage;
        }

        public void AddImmobility()
        {
            immobilizationStacks++;
        }

        public void RemoveImmobility()
        {
            immobilizationStacks--;
        }

        public void AddInfiniteAmmunitionBuff()
        {
            infiniteAmmunitionBonusStacks++;
        }

        public void RemoveInfiniteAmmunitionBuff()
        {
            infiniteAmmunitionBonusStacks--;
        }

        public void AddPacification()
        {
            pacificationStacks++;
        }

        public void RemovePacification()
        {
            pacificationStacks--;
        }

        public void AddInvisibility()
        {
            invisibilityStacks++;
        }

        public void RemoveInvisibility()
        {
            invisibilityStacks--;
        }

        #endregion

        public int[] CloneDamageBonus()
        {
            return new int[] {damageMultiplier,damageBonus};
        }

        public void ReceiveDamageClone(int[] damage)
        {
            damageMultiplier = damage[0];
            damageBonus = damage[1];
        }

    }

}


