using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/View/AchievementsInfo")]
    public class AchievementsInfo : GameScript
    {

        [SerializeField]
        private float scrollSpeed;

        [SerializeField]
        private float maxScrollHeight;

        [SerializeField]
        private float achievementHeight;

        [SerializeField]
        private float achievementTitleFontSize;

        public float AchievementHeight
        {
            get
            {
                return achievementHeight;
            }
        }

        public float AchievementTitleFontSize
        {
            get
            {
                return achievementTitleFontSize;
            }
        }

        public float MaxScrollHeight
        {
            get
            {
                return maxScrollHeight;
            }
        }

        public float ScrollSpeed
        {
            get
            {
                return scrollSpeed;
            }
        }
    }
}
