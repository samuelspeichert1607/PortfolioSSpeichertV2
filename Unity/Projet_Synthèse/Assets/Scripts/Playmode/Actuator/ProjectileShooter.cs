using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Actuator/ProjectileShooter")]
    public class ProjectileShooter : GameScript
    {
        [SerializeField]
        private GameObject projectilePrefab;

        public GameObject Fire()
        {
            return Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}