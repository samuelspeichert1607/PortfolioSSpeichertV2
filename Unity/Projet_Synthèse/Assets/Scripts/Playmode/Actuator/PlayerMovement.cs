using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetSynthese
{
    [AddComponentMenu("Game/Actuator/PlayerMovement")]
    public class PlayerMovement : GameScript, MovementInterface
    {
        [SerializeField]
        private float speed = 10.0f;

        private Transform topParentTransform;

        private Rigidbody2D topParentRigidbody2D;

        private float angle;

        private float stickXIntensity;

        private float stickYIntensity;

        private float speedMultiplier = 1;
        
        private float Speed { get { return speed * speedMultiplier; } }

        private void InjectPlayerMovement([TopParentScope] Transform topParentTransform,
                                       [TopParentScope] Rigidbody2D topParentRigidbody2D)
        {
            this.topParentTransform = topParentTransform;
            this.topParentRigidbody2D = topParentRigidbody2D;
        }

        private void Awake()
        {
            InjectDependencies("InjectPlayerMovement");
        }



        // Update is called once per frame
        private void Update()
        {
            Vector3 vecAngle = transform.eulerAngles;

            if ((stickXIntensity != 0) && (stickYIntensity != 0))
            {
                angle = Mathf.Atan(stickYIntensity / stickXIntensity) * Mathf.Rad2Deg; // - Mathf.PI
                if (stickXIntensity < 0)
                {
                    angle -= 180;
                }
                vecAngle = new Vector3(0, 0, angle - 90);
            }

            topParentRigidbody2D.transform.eulerAngles = vecAngle;
        }

        public void MoveX(float impulse)
        {
            topParentRigidbody2D.AddForce(new Vector3(impulse * Speed, 0.0f));
        }

        public void MoveY(float impulse)
        {
            topParentRigidbody2D.AddForce(new Vector3(0.0f, impulse * Speed));
        }

        public void AimX(float stickXIntensity)
        {
            this.stickXIntensity = stickXIntensity;
        }

        public void AimY(float stickYIntensity)
        {
            this.stickYIntensity = stickYIntensity;
        }

        public void Skill1()
        {
            
        }

        public void Build()
        {
            
        }

        public void Skill2()
        {
            
        }

        public void Repair()
        {
            
        }

        public void SwitchWeapon()
        {
            
        }

        public void MeleeAttack()
        {
            
        }

        public void Fire()
        {
            
        }

        public void TogglePause()
        {
            
        }

        public void AddTemporarySpeedModifier(float multiplier, float duration)
        {
            StartCoroutine(BuffSpeed(multiplier, duration));
        }

        private IEnumerator BuffSpeed(float multiplier, float duration)
        {
            speedMultiplier += multiplier;
            yield return new WaitForSeconds(duration);
            speedMultiplier -= multiplier;
        }
    }
}