﻿using System.Collections.Generic;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Data/HighScoreRepository")]
    public class HighScoreRepository : GameScript
    {
        [SerializeField]
        private uint nbScoresToKeep = 5;

        private Repository repository;

        public void InjectHighScoreRepository([ApplicationScope] IDbConnectionFactory connectionFactory,
                                              [ApplicationScope] IDbParameterFactory parameterFactory)
        {
            repository = new Repository(this, connectionFactory, parameterFactory, new HighScoreMapper());
        }

        public void Awake()
        {
            InjectDependencies("InjectHighScoreRepository");
        }

        public virtual void AddScore(HighScore highScore)
        {
            repository.AddScore(highScore);
        }

        public virtual bool IsLeaderboardFull()
        {
            return repository.Count() >= nbScoresToKeep;
        }

        public virtual HighScore GetLowestHighScore()
        {
            return repository.GetLowestHighScore();
        }

        public virtual IList<HighScore> GetAllHighScores()
        {
            return repository.GetAllHighScores();
        }

        private class Repository : DbRepository<HighScore>
        {
            private readonly HighScoreRepository highScoreRepository;

            public Repository(HighScoreRepository highScoreRepository,
                              [NotNull] IDbConnectionFactory connectionFactory,
                              [NotNull] IDbParameterFactory parameterFactory,
                              [NotNull] IDbDataMapper<HighScore> dataMapper)
                : base(connectionFactory, parameterFactory, dataMapper)
            {
                this.highScoreRepository = highScoreRepository;
            }

            public void AddScore(HighScore highScore)
            {
                highScore.Id = ExecuteInsert("INSERT INTO HighScore (name, score) VALUES (?,?);", new object[]
                {
                    highScore.Name,
                    highScore.ScorePoints
                });

                DeleteTooLowScores();
            }

            public HighScore GetLowestHighScore()
            {
                return ExecuteSelectOne("SELECT * FROM LowestHighScore;", new object[] {});
            }

            public IList<HighScore> GetAllHighScores()
            {
                return ExecuteSelectAll("SELECT * FROM TopHighScores;", new object[] {});
            }

            private void DeleteTooLowScores()
            {
                ExecuteDelete("DELETE FROM HighScore WHERE id NOT IN (SELECT id FROM TopHighScores LIMIT ?)",
                              new object[] {highScoreRepository.nbScoresToKeep});
            }

            public long Count()
            {
                return ExecuteScallar("SElECT COUNT(*) FROM HighScore;", new object[] {});
            }
        }
    }
}