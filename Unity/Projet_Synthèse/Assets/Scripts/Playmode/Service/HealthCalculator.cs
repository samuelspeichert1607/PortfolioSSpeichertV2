

namespace ProjetSynthese
{

    public class HealthCalculator
    {
        private int unitMaxHealth = 100;
        private int unitBonusHealth = 0;
        private int unitHealthPercentage = 100;
        private int unitCurrentHealth = 100;

        private const int unitHealthPercentageDivider = 100;

        public int UnitMaxHealth { get { return (unitMaxHealth*unitHealthPercentage/unitHealthPercentageDivider)+unitBonusHealth; }}
        public int UnitCurrentHealth { get { return unitCurrentHealth; } }
        
        public HealthCalculator(int unitInitialHealth)
        {
            unitMaxHealth = unitInitialHealth;
            unitCurrentHealth = UnitMaxHealth;
        }

        public void ReceiveDamages(int damagePoints)
        {
            if (damagePoints > 0)
            {
                unitCurrentHealth -= damagePoints;
            }
        }

        public void ReceiveHealing(int healingPoints)
        {
            if (healingPoints > 0)
            {
                unitCurrentHealth += healingPoints;
                if (unitCurrentHealth > UnitMaxHealth)
                {
                    unitCurrentHealth = UnitMaxHealth;
                }
            }
        }

        public bool isDead()
        {
            return (unitCurrentHealth <= 0);
        }

        public void ResetHealth()
        {
            unitCurrentHealth = UnitMaxHealth;
        }

        public void ResetHealth(int newMaxHealth)
        {
            unitMaxHealth = newMaxHealth;
            unitBonusHealth = 0;
            unitHealthPercentage = 100;
            unitCurrentHealth = UnitMaxHealth;
        }

        public void AddBonusMaxHealth(int bonusHealth)
        {
            unitBonusHealth += bonusHealth;
        }
        public void RemoveBonusMaxHealth(int bonusHealth)
        {
            unitBonusHealth -= bonusHealth;
            if (unitCurrentHealth > UnitMaxHealth)
            {
                unitCurrentHealth = UnitMaxHealth;
            }
        }

        public void AddMaxHealthPercentage(int healthPercentage)
        {
            unitHealthPercentage += healthPercentage;
        }

        public void RemoveMaxHealthPercentage(int healthPercentage)
        {
            unitHealthPercentage -= healthPercentage;
            if (unitCurrentHealth > UnitMaxHealth)
            {
                unitCurrentHealth = UnitMaxHealth;
            }
        }

    }

}



