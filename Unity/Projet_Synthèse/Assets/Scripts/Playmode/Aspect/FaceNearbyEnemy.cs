using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class FaceNearbyEnemy : GameScript
    {
        
        [SerializeField]
        [Tooltip("Degree variation: Sometimes a 90 degree fix is required.")]
        private float zVariationCorrection;

        private TargetFinder targetFinder;

        private GameObject target;

        private void InjectFaceNearbyEnemy([GameObjectScope] TargetFinder targetFinder)
        {
            this.targetFinder = targetFinder;
        }

        private void Awake()
        {
            InjectDependencies("InjectFaceNearbyEnemy");
        }

        private void Update()
        {
            target = targetFinder.GetTarget();
            if (target != null)
            {
                UpdateAngle();
            }
        }

        public GameObject GetTarget()
        {
            return target;
        }

        private void UpdateAngle()
        {
            float angle = Mathf.Atan((target.transform.position.y - transform.position.y) /
                                     (target.transform.position.x - transform.position.x)) * 180 / Mathf.PI + zVariationCorrection;
            if (transform.position.x > target.transform.position.x)
            {
                angle += 180;
            }
            transform.parent.eulerAngles = new Vector3(0, 0, angle);
        }


    }


}

