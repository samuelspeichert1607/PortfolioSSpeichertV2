using System.Collections;
using System.Collections.Generic;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Data/GameScoreRepository")]
    public class GameScoreRepository : GameScript
    {

        [SerializeField]
        [Tooltip("Quantity of game scores to keep.")]
        private uint nbGameScoresToKeep = 10;

        private Repository repository;

        public void InjectGameScoreRepository([ApplicationScope] IDbConnectionFactory connectionFactory,
            [ApplicationScope] IDbParameterFactory parameterFactory)
        {
            repository = new Repository(this, connectionFactory, parameterFactory, new GameScoreMapper());
        }

        public void Awake()
        {
            InjectDependencies("InjectGameScoreRepository");
        }

        public virtual void AddGameScore(GameScore gameScore)
        {
            repository.AddGameScore(gameScore);
        }

        //Pour suppression si necessaire
        public virtual bool IsLeaderboardFull()
        {
            return repository.Count() >= nbGameScoresToKeep;
        }

        public virtual IList<GameScore> GetAllGameScores()
        {
            return repository.GetAllGameScores();
        }

        #region Repository
        private class Repository : DbRepository<GameScore>
        {
            private readonly GameScoreRepository gameScoreRepository;

            public Repository(GameScoreRepository gameScoreRepository,
                [NotNull] IDbConnectionFactory connectionFactory,
                [NotNull] IDbParameterFactory parameterFactory,
                [NotNull] IDbDataMapper<GameScore> dataMapper)
                : base(connectionFactory, parameterFactory, dataMapper)
            {
                this.gameScoreRepository = gameScoreRepository;
            }

            public void AddGameScore(GameScore gameScore)
            {
                gameScore.Id = ExecuteInsert("INSERT INTO GameScore (timeGame,metalQuantityGathered,metalQuantitySpent," +
                                             "nbConstructedTurret,nbDestructedTurret,nbConstructedExtractor,nbDestructedExtractor," +
                                             "nbEnemiesKilled,fkIdLevel) " +
                                             "VALUES (?,?,?,?,?,?,?,?,?);", new object[]
                {
                    gameScore.TimeGame,
                    gameScore.MetalQuantityGathered,
                    gameScore.MetalQuantitySpent,
                    gameScore.NbConstructedTurret,
                    gameScore.NbDestructedTurret,
                    gameScore.NbConstructedExtractor,
                    gameScore.NbDestructedExtractor,
                    gameScore.NbEnemiesKilled,
                    //TODO gameScore.LevelName
                });

                //TODO DeleteTooLowScores();
            }

            public IList<GameScore> GetAllGameScores()
            {
                return ExecuteSelectAll("SELECT * FROM GameScore;", new object[] { });
            }

            /* voir les conditions de suppression...
            private void DeleteTooOldScores()
            {
                ExecuteDelete("DELETE FROM GameScore WHERE id NOT IN (SELECT id FROM TopHighScores LIMIT ?)",
                    new object[] { highScoreRepository.nbScoresToKeep });
            }*/

            public long Count()
            {
                return ExecuteScallar("SElECT COUNT(*) FROM GameScore;", new object[] { });
            }
        }
        #endregion
    }
}


