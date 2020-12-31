
using Harmony;
using ProjetSynthese;
using UnityEngine;


namespace ProjetSynthese
{
    [AddComponentMenu("Game/Actuator/BulletMoveForwardOnStart")]
    public class MoveForwardOnStart : GameScript
    {
        [SerializeField]
        [Tooltip("Speed at which the object (a bullet, most likely) moves")]
        private float speed;

        [SerializeField]
        [Tooltip("Speed randomization factor.")]
        private float speedRandomFactor;

        private Rigidbody2D body;

        private void InjectMoveForwardOnStart([EntityScope] Rigidbody2D rigidbody)
        {
            body = rigidbody;
        }

        private void Awake()
        {
            InjectDependencies("InjectMoveForwardOnStart");
        }

        private void Start()
        {
            body.velocity = transform.up * (speed + Random.Range(-speedRandomFactor, speedRandomFactor));
        }
    }

}

