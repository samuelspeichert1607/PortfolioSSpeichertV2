using System.Collections;
using System.Collections.Generic;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Data/AchievementRepository")]
    public class AchievementRepository : GameScript
    {
        private Repository repository;

        public void InjectAchievementRepository([ApplicationScope] IDbConnectionFactory connectionFactory,
            [ApplicationScope] IDbParameterFactory parameterFactory)
        {
            repository = new Repository(this, connectionFactory, parameterFactory, new AchievementMapper());
        }

        public void Awake()
        {
            InjectDependencies("InjectAchievementRepository");
        }

        public virtual Achievement GetAnAchievement(string achievementName)
        {
            return repository.GetAnAchievement(achievementName);
        }

        public virtual void UnlockAchievement(string achievementName)
        {
            repository.UnlockAchievement(achievementName);
        }

        public virtual IList<Achievement> GetAllAchievements()
        {
            return repository.GetAllAchievements();
        }

        #region Repository
        private class Repository : DbRepository<Achievement>
        {
            private readonly AchievementRepository achievementRepository;

            public Repository(AchievementRepository achievementRepository,
                [NotNull] IDbConnectionFactory connectionFactory,
                [NotNull] IDbParameterFactory parameterFactory,
                [NotNull] IDbDataMapper<Achievement> dataMapper)
                : base(connectionFactory, parameterFactory, dataMapper)
            {
                this.achievementRepository = achievementRepository;
            }

            public void UnlockAchievement(string achievementName)
            {
                ExecuteUpdate("UPDATE Achievement SET is_unlocked = 1 WHERE name = ?;", new object[]
                {
                    achievementName
                });
            }

            public Achievement GetAnAchievement(string achievementName)
            {
                return ExecuteSelectOne("SELECT * FROM Achievement WHERE name=?;", new object[] { achievementName });
            }

            public IList<Achievement> GetAllAchievements()
            {
                return ExecuteSelectAll("SELECT * FROM Achievement;", new object[] { });
            }
        }
        #endregion
    }
}
