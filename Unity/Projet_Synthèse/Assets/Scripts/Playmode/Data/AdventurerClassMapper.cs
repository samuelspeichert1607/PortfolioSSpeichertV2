using System.Data.Common;

namespace ProjetSynthese
{
    public class AdventurerClassMapper : SqLiteDataMapper<AdventurerClass>
    {
        public override AdventurerClass GetObjectFromReader(DbDataReader reader)
        {
            return new AdventurerClass
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                NameClass = reader.GetString(reader.GetOrdinal("nameClass")),
                NameAdventurer = reader.GetString(reader.GetOrdinal("nameAdventurer")),
                MeleeWeapon = reader.GetString(reader.GetOrdinal("meleeWeapon")),
                RangedWeaponOne = reader.GetString(reader.GetOrdinal("rangedWeaponOne")),
                RangedWeaponTwo = reader.GetString(reader.GetOrdinal("rangedWeaponTwo")),
                SkillOne = reader.GetString(reader.GetOrdinal("skillOne")),
                SkillTwo = reader.GetString(reader.GetOrdinal("skillTwo")),
                StoryAdventurer = reader.GetString(reader.GetOrdinal("storyAdventurer"))
            };
        }
    }
}


