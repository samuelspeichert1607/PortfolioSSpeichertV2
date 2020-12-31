using System.Collections;
using System.Collections.Generic;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Data/AdventurerClassRepository")]
    public class AdventurerClassRepository : GameScript
    {
        private Repository repository;

        public void InjectAdventurerClassRepository([ApplicationScope] IDbConnectionFactory connectionFactory,
            [ApplicationScope] IDbParameterFactory parameterFactory)
        {
            repository = new Repository(this, connectionFactory, parameterFactory, new AdventurerClassMapper());
        }

        public void Awake()
        {
            InjectDependencies("InjectAdventurerClassRepository");
        }

        public virtual void AddAdventurerClass(AdventurerClass adventurerClass)
        {
            repository.AddAdventurerClass(adventurerClass);
        }

        public virtual AdventurerClass GetAnAdventurerClass(string adventurerClassName)
        {
            return repository.GetAnAdventurerClass(adventurerClassName);
        }

        public virtual IList<AdventurerClass> GetAllAdventurerClasses()
        {
            return repository.GetAllAdventurerClasses();
        }

        #region Repository
        private class Repository : DbRepository<AdventurerClass>
        {
            private readonly AdventurerClassRepository adventurerClassRepository;

            public Repository(AdventurerClassRepository adventurerClassRepository,
                [NotNull] IDbConnectionFactory connectionFactory,
                [NotNull] IDbParameterFactory parameterFactory,
                [NotNull] IDbDataMapper<AdventurerClass> dataMapper)
                : base(connectionFactory, parameterFactory, dataMapper)
            {
                this.adventurerClassRepository = adventurerClassRepository;
            }

            public void AddAdventurerClass(AdventurerClass adventurerClass)
            {
                adventurerClass.Id = ExecuteInsert("INSERT INTO AdventurerClass (nameClass) VALUES (?);", new object[]
                {
                    adventurerClass.NameClass
                });
            }

            public AdventurerClass GetAnAdventurerClass(string adventurerClassName)
            {
                return ExecuteSelectOne("SELECT * FROM AdventurerClass WHERE nameClass=?;", new object[] { adventurerClassName });
            }

            public IList<AdventurerClass> GetAllAdventurerClasses()
            {
                return ExecuteSelectAll("SELECT * FROM AdventurerClass;", new object[] { });
            }
        }
        #endregion
    }
}


