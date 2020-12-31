using System.Data.Common;

namespace ProjetSynthese
{
    public class GameScoreMapper : SqLiteDataMapper<GameScore>
    {
        public override GameScore GetObjectFromReader(DbDataReader reader)
        {
            return new GameScore
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                TimeGame = reader.GetString(reader.GetOrdinal("timeGame")),
                MetalQuantityGathered= reader.GetInt32(reader.GetOrdinal("metalQuantityGathered")),
                MetalQuantitySpent= reader.GetInt32(reader.GetOrdinal("metalQuantitySpent")),
                NbConstructedTurret = reader.GetInt32(reader.GetOrdinal("nbConstructedTurret")),
                NbDestructedTurret = reader.GetInt32(reader.GetOrdinal("nbDestructedTurret")),
                NbConstructedExtractor = reader.GetInt32(reader.GetOrdinal("nbConstructedExtractor")),
                NbDestructedExtractor = reader.GetInt32(reader.GetOrdinal("nbDestructedExtractor")),
                NbEnemiesKilled = reader.GetInt32(reader.GetOrdinal("nbEnemiesKilled")),
                //TODO LevelName = (needs the other class...)
            };
        }
    }
}


