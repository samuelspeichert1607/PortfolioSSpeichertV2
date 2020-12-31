namespace ProjetSynthese
{
    public class GameScore
    {
        public long Id { get; set; }
        public string TimeGame { get; set; }
        public long MetalQuantityGathered { get; set; }
        public long MetalQuantitySpent { get; set; }
        public long NbConstructedTurret { get; set; }
        public long NbDestructedTurret { get; set; }
        public long NbConstructedExtractor { get; set; }
        public long NbDestructedExtractor { get; set; }
        public long NbEnemiesKilled { get; set; }
        public string LevelName { get; set; }
    }
}