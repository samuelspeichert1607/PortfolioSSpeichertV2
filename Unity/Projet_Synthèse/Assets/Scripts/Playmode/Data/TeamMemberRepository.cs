using System.Collections.Generic;
using Harmony;
using JetBrains.Annotations;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Data/TeamMemberRepository")]
    public class TeamMemberRepository : GameScript
    {
        private Repository repository;

        public void InjectTeamMemberRepository([ApplicationScope] IDbConnectionFactory connectionFactory,
            [ApplicationScope] IDbParameterFactory parameterFactory)
        {
            repository = new Repository(this, connectionFactory, parameterFactory, new TeamMemberMapper());
        }

        public void Awake()
        {
            InjectDependencies("InjectTeamMemberRepository");
        }

        public virtual void AddTeamMember(TeamMember teamMember)
        {
            repository.AddTeamMember(teamMember);
        }

        public virtual IList<TeamMember> GetAllTeamMembers(int idGameScore)
        {
            return repository.GetAllTeamMembers(idGameScore);
        }

        #region Repository
        private class Repository : DbRepository<TeamMember>
        {
            private readonly TeamMemberRepository teamMemberRepository;

            public Repository(TeamMemberRepository teamMemberRepository,
                [NotNull] IDbConnectionFactory connectionFactory,
                [NotNull] IDbParameterFactory parameterFactory,
                [NotNull] IDbDataMapper<TeamMember> dataMapper)
                : base(connectionFactory, parameterFactory, dataMapper)
            {
                this.teamMemberRepository = teamMemberRepository;
            }

            public void AddTeamMember(TeamMember teamMember)
            {
                teamMember.Id = ExecuteInsert("INSERT INTO TeamMember (fkIdAdventurerClass,fkIdGameScore,wasKilled,livingDuration) VALUES (?,?,?,?);", new object[]
                {
                    teamMember.IdAdventurerClass,
                    teamMember.IdGameScore,
                    teamMember.WasKilled ? 1L : 0L,
                    teamMember.LivingDuration
                });
            }

            public IList<TeamMember> GetAllTeamMembers(int idGameScore)
            {
                return ExecuteSelectAll("SELECT * FROM TeamMember WHERE fkIdGameScore=?;", new object[] { idGameScore });
            }
        }
        #endregion
    }
}


