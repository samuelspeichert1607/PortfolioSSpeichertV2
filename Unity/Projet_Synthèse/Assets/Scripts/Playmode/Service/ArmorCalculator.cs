
namespace ProjetSynthese
{

    public class ArmorCalculator
    {
        private int unitMaxArmor = 0;
        private int unitBonusArmor = 0;
        private int unitArmorPercentage = 100;
        private int unitCurrentArmor = 0;

        private const int unitArmorPercentageDivider = 100;

        private int armorDamageReduction = 5;

        private int armorDamageAbsorbtionTreshold = 100;

        public int UnitMaxArmor { get { return (unitMaxArmor*unitArmorPercentage/unitArmorPercentageDivider)+unitBonusArmor; } }
        public int UnitCurrentArmor { get { return unitCurrentArmor; } }

        public ArmorCalculator(int unitInitialArmor, int armorReduction = 5, int armorAbsorbtionTreshold = 100)
        {
            unitMaxArmor = unitInitialArmor;
            unitCurrentArmor = unitMaxArmor;
            armorDamageReduction = armorReduction;
            armorDamageReduction = armorReduction;
            armorDamageAbsorbtionTreshold = armorAbsorbtionTreshold;
        }

        public int AbsorbDamages(int damagePoints)
        {
            if (unitCurrentArmor > 0)
            {
                if (damagePoints > armorDamageReduction)
                {
                    damagePoints -= armorDamageReduction;

                    int damagesToArmor = CalculateAbsorbedDamages(damagePoints);
                    unitCurrentArmor -= damagesToArmor;
                    damagePoints = CalculateRemainingDamages(damagesToArmor, damagePoints);

                }
                else
                {
                    unitCurrentArmor--;
                    return 0;
                }
            }

            return damagePoints;
        }

        

        public void Repair(int healingPoints)
        {
            if (healingPoints > 0)
            {
                unitCurrentArmor += healingPoints;
                if (unitCurrentArmor > UnitMaxArmor)
                {
                    unitCurrentArmor = UnitMaxArmor;
                }
            }
        }

        public bool isDead()
        {
            return (unitCurrentArmor <= 0);
        }

        public void ResetArmor()
        {
            unitCurrentArmor = unitMaxArmor;
        }

        public void ResetArmor(int newMaxarmor)
        {
            unitMaxArmor = newMaxarmor;
            unitCurrentArmor = UnitMaxArmor;
        }

        public void ResetResistance(uint armorNewReduction, uint armorNewAbsorbtion)
        {
            armorDamageReduction = (int) armorNewReduction;
            armorDamageAbsorbtionTreshold = (int) armorNewAbsorbtion;
        }

        private int CalculateAbsorbedDamages(int damagpoints)
        {
            if (unitCurrentArmor >= armorDamageAbsorbtionTreshold)
            {
                return damagpoints;
            }
            else
            {
                int damages = (int)((float) damagpoints * 
                    ((float) unitCurrentArmor / (float) armorDamageAbsorbtionTreshold));
                damages += (damages == 0) ? 0 : 1;
                return damages;
            }
        }

        private int CalculateRemainingDamages(int damageToArmor, int damagePoints)
        {
            unitCurrentArmor -= damageToArmor;
            damagePoints -= damageToArmor;
            if (unitCurrentArmor < 0)
            {
                damagePoints += unitCurrentArmor * -1;
                unitCurrentArmor = 0;
            }
            return damagePoints;
        }

        public void AddBonusMaxArmor(int bonusArmor)
        {
            unitBonusArmor += bonusArmor;
        }
        public void RemoveBonusMaxArmor(int bonusArmor)
        {
            unitBonusArmor -= bonusArmor;
            if (unitCurrentArmor > UnitMaxArmor)
            {
                unitCurrentArmor = UnitMaxArmor;
            }
        }

        public void AddMaxArmorPercentage(int armorPercentage)
        {
            unitArmorPercentage += armorPercentage;
        }

        public void RemoveMaxArmorPercentage(int armorPercentage)
        {
            unitArmorPercentage -= armorPercentage;
            if (unitCurrentArmor > UnitMaxArmor)
            {
                unitCurrentArmor = UnitMaxArmor;
            }
        }
    }

}



