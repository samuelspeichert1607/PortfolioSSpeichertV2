namespace ProjetSynthese
{
    public class CameraPositionsConstants
    {
        public enum PostitionSets
        {
            Player1Camera = 1,
            Player2Camera = 2,
            Player3Camera = 3,
            Player4Camera = 4
        }
        public enum PlayerQuantity
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4
        }

        private static readonly float[][] player1CameraPositions;
        private static readonly float[][] player2CameraPositions;
        private static readonly float[][] player3CameraPositions;
        private static readonly float[][] player4CameraPositions;

        static CameraPositionsConstants()
        {
            player1CameraPositions = new float[][] { new[] { 0f, 0f, 1f, 1f }, new[] { 0f, 0.5f, 1f, 1f }, new[] { 0f, 0.5f, 0.5f, 1f }, new[] { 0f, 0.5f, 0.5f, 1f } };
            player2CameraPositions = new float[][] { new[] { 0f, 0f, 0f, 0f }, new[] { 0f, 0f, 1f, 0.5f }, new[] { 0.5f, 0.5f, 0.5f, 0.5f }, new[] { 0.5f, 0.5f, 0.5f, 0.5f } };
            player3CameraPositions = new float[][] { new[] { 0f, 0f, 0f, 0f }, new[] { 0f, 0f, 0f, 0f }, new[] { 0.25f, 0f, 0.5f, 0.5f }, new[] { 0f, 0f, 0.5f, 0.5f } };
            player4CameraPositions = new float[][] { new[] { 0f, 0f, 0f, 0f }, new[] { 0f, 0f, 0f, 0f }, new[] { 0f, 0f, 0f, 0f }, new[] { 0.5f, 0f, 0.5f, 0.5f } };
        }

        public static float[] GetCameraPosition(PostitionSets player, PlayerQuantity quantity)
        {
            switch (player)
            {
                case (PostitionSets.Player1Camera):
                    {
                        return player1CameraPositions[(int)quantity - 1];
                    }
                case (PostitionSets.Player2Camera):
                    {
                        return player2CameraPositions[(int)quantity - 1];
                    }
                case (PostitionSets.Player3Camera):
                    {
                        return player3CameraPositions[(int)quantity - 1];
                    }
                case (PostitionSets.Player4Camera):
                    {
                        return player4CameraPositions[(int)quantity - 1];
                    }
            }
            return new float[0];
        }
    }
}