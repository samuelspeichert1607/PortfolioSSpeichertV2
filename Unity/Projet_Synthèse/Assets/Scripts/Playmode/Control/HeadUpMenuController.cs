using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/HeadUpMenuController")]
    public class HeadUpMenuController : GameScript
    {
        //private HealthBarView healthBarView;
        private ScoreView scoreView;
        //private PlayerHealthEventChannel playerHealthEventChannel;
        private ScoreEventChannel scoreEventChannel;

        private void InjectHeadUpMenuController([EntityScope] ScoreView scoreView,
                                               [EventChannelScope] ScoreEventChannel scoreEventChannel) //[EntityScope] HealthBarView healthBarView,[EventChannelScope] PlayerHealthEventChannel playerHealthEventChannel,
        {
            //this.healthBarView = healthBarView;
            this.scoreView = scoreView;
            //this.playerHealthEventChannel = playerHealthEventChannel;
            this.scoreEventChannel = scoreEventChannel;
        }

        private void Awake()
        {
            InjectDependencies("InjectHeadUpMenuController");

            //playerHealthEventChannel.OnEventPublished += OnPlayerHealthChanged;
            scoreEventChannel.OnEventPublished += OnScoreChanged;
        }

        private void OnDestroy()
        {
            //playerHealthEventChannel.OnEventPublished -= OnPlayerHealthChanged;
            scoreEventChannel.OnEventPublished -= OnScoreChanged;
        }

        //private void UpdateHealthBarView(Health playerHealth)
        //{
        //    healthBarView.SetHealthPercentage((float) playerHealth.HealthPoints / playerHealth.MaxHealthPoints);
        //}

        private void UpdateScoreView(Score score)
        {
            scoreView.SetScore(score.ScorePoints);
        }

        //private void OnPlayerHealthChanged(PlayerHealthEvent healthEvent)
        //{
        //    UpdateHealthBarView(healthEvent.PlayerHealth);
        //}

        private void OnScoreChanged(ScoreEvent scoreEvent)
        {
            UpdateScoreView(scoreEvent.Score);
        }
    }
}