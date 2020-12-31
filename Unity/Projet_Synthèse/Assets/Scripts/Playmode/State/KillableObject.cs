using System.Collections;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void EntityHealthChangedEventHandler(int unitCurrentHealth, int unitMaxHealth);
    public delegate void EntityDeathEventHandler();


    [AddComponentMenu("Game/State/Health")]
    public class KillableObject : GameScript
    {
        [SerializeField]
        [Tooltip("the health points of the current unit")]
        private uint unitHealthPoints;
        [SerializeField]
        [Tooltip("the armor points of the current unit")]
        private uint unitArmorPoints;
        [SerializeField]
        [Range(0, 50)]
        [Tooltip("the damage points reduced from each hit when unit has armor.")]
        private int armorDamageReduction = 5;
        [SerializeField]
        [Range(0, 1000)]
        [Tooltip("the treshold representing 100% damage absorbtion by armor. Any armor below this value will absorb only a part of damage dealt.")]
        private int armorAbsorbtionTreshold = 100;

        [SerializeField]
        [Tooltip("Prevents the entity to be healed.")]
        private bool isImmuneToHealing = false;

        private HealthCalculator healthCalculator;
        private ArmorCalculator armorCalculator;

        private CharacterStatistics stats;

        private int damageReceivedMultiplier = 100;
        private const int damageReceivedDivider = 100;

        public event EntityHealthChangedEventHandler OnHealthChanged;
        public event EntityHealthChangedEventHandler OnArmorChanged;
        public event EntityDeathEventHandler OnDeath;

        private void InjectCharacterStatistics([EntityScope] CharacterStatistics stats)
        {
            this.stats = stats;
        }

        private void Awake()
        {
            InjectDependencies("InjectCharacterStatistics");
            healthCalculator = new HealthCalculator((int)unitHealthPoints);
            armorCalculator = new ArmorCalculator((int)unitArmorPoints, armorDamageReduction, armorAbsorbtionTreshold);
        }

        public void ReceiveDamage(int damages)
        {
            int remainingDamages = stats.CalculateDamageReceived(damages);
            if (armorCalculator.UnitCurrentArmor > 0)
            {
                remainingDamages = armorCalculator.AbsorbDamages(damages);
                NotifyArmorChanged();
            }
            if (remainingDamages > 0)
            {
                healthCalculator.ReceiveDamages(remainingDamages);
                NotifyHealthChanged();
                if (healthCalculator.isDead())
                {
                    NotifyDeath();
                    GetTopParent().SetActive(false);
                }

            }

        }

        public void Heal(int healingPoints)
        {
            if (!isImmuneToHealing)
            {
                ForceHeal(healingPoints);
            }
        }

        public void ForceHeal(int healingPoints)
        {
            if (healthCalculator.UnitCurrentHealth < healthCalculator.UnitMaxHealth)
            {
                healthCalculator.ReceiveHealing(healingPoints);
                NotifyHealthChanged();
            }
        }

        public void RepairArmor(int repairPoints)
        {
            if (armorCalculator.UnitCurrentArmor < armorCalculator.UnitMaxArmor)
            {
                armorCalculator.Repair(repairPoints);
            }

        }

        public void ResetLife(int newMaxHealth = -1)
        {
            if (newMaxHealth > 0)
            {
                healthCalculator.ResetHealth(newMaxHealth);
                NotifyHealthChanged();
            }
            else
            {
                healthCalculator.ResetHealth();
                NotifyHealthChanged();
            }

        }

        public void ResetArmor(int newMaxArmor = -1)
        {
            if (newMaxArmor >= 0)
            {
                armorCalculator.ResetArmor(newMaxArmor);
                NotifyArmorChanged();
            }
            else
            {
                armorCalculator.ResetArmor();
                NotifyArmorChanged();
            }

        }

        public void IncreaseHealth(int healthBonus, bool isPercentage)
        {
            if (isPercentage)
            {
                healthCalculator.AddMaxHealthPercentage(healthBonus);
            }
            else
            {
                healthCalculator.AddBonusMaxHealth(healthBonus);
            }
            NotifyHealthChanged();

        }
        public void DecreaseHealth(int healthBonus, bool isPercentage)
        {
            if (isPercentage)
            {
                healthCalculator.RemoveMaxHealthPercentage(healthBonus);
            }
            else
            {
                healthCalculator.RemoveBonusMaxHealth(healthBonus);
            }
            NotifyHealthChanged();

        }

        public void IncreaseArmor(int armorBonus, bool isPercentage)
        {
            if (isPercentage)
            {
                armorCalculator.AddMaxArmorPercentage(armorBonus);
            }
            else
            {
                armorCalculator.AddBonusMaxArmor(armorBonus);
            }
            NotifyArmorChanged();

        }
        public void ReduceArmor(int armorBonus, bool isPercentage)
        {
            if (isPercentage)
            {
                armorCalculator.RemoveMaxArmorPercentage(armorBonus);
            }
            else
            {
                armorCalculator.RemoveBonusMaxArmor(armorBonus);
            }
            NotifyArmorChanged();

        }
        

        private void ResetArmorStrenght(uint armorNewDamageReduction, uint armorNewAbsorbtionTreshold)
        {
            armorCalculator.ResetResistance(armorNewDamageReduction, armorNewAbsorbtionTreshold);
            ResetArmor();
        }

        private void NotifyDeath()
        {
            if (OnDeath != null) OnDeath();
        }
        private void NotifyHealthChanged()
        {
            if (OnHealthChanged != null)
                OnHealthChanged(healthCalculator.UnitCurrentHealth, healthCalculator.UnitMaxHealth);
        }
        private void NotifyArmorChanged()
        {
            if (OnArmorChanged != null)
                OnArmorChanged(armorCalculator.UnitCurrentArmor, armorCalculator.UnitMaxArmor);
        }




    }
}