using System.Data.Common;

namespace ProjetSynthese
{
    public class TeamMemberMapper : SqLiteDataMapper<TeamMember>
    {
        public override TeamMember GetObjectFromReader(DbDataReader reader)
        {
            return new TeamMember
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                IdAdventurerClass = reader.GetInt32(reader.GetOrdinal("fkIdAdventurerClass")),
                IdGameScore = reader.GetInt32(reader.GetOrdinal("fkIdGameScore")),
                WasKilled = reader.GetInt32(reader.GetOrdinal("wasKilled")) != 0,
                LivingDuration = reader.GetString(reader.GetOrdinal("livingDuration"))
            };
        }
    }
}


