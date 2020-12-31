using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class FollowPlayer : GameScript
    {
        private GameObject player;
        public GameObject Player
        {
            set { player = value; }
        }

        private bool isPlayerDead = false;

        private KillableObject health;

        [SerializeField]
        [Tooltip("Speed at which the camera can move")]
        private float cameraSpeed;

        private void Update()
        {
            if(player != null)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), cameraSpeed);
            }
            else
            {
                if (!isPlayerDead)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void StopFollowingPlayer()
        {
            isPlayerDead = true;
        }

        private void onEnable()
        {
            health.OnDeath += StopFollowingPlayer;
        }
        private void onDisable()
        {
            health.OnDeath -= StopFollowingPlayer;
        }
    }
}
