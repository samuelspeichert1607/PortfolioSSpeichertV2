using System.Data.Common;

namespace ProjetSynthese
{
    public class AchievementMapper : SqLiteDataMapper<Achievement>
    {
        public override Achievement GetObjectFromReader(DbDataReader reader)
        {
            return new Achievement
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Description = reader.GetString(reader.GetOrdinal("description")),
                IsUnlocked = reader.GetBoolean(reader.GetOrdinal("is_unlocked"))
            };

        }
    }
}
