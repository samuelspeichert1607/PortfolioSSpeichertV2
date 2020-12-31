namespace ProjetSynthese
{
    public class TeamMember
    {
        public long Id { get; set; }
        public long IdAdventurerClass { get; set; }
        public long IdGameScore { get; set; }
        public bool WasKilled { get; set; }
        public string LivingDuration { get; set; }
    }
}