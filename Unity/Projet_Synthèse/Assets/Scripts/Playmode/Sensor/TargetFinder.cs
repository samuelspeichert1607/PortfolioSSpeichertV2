using System;
using System.Collections;
using System.Collections.Generic;
using Harmony;
using ProjetSynthese;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Radius of the detection circle")]
    private float radius;
    [SerializeField]
    [Tooltip("Mask of targets that can be detected.")]
    private R.E.Layer enemyMask;
    private LayerMask enemyMaskDetection;

    private GameObject target;

    

    private void Awake()
    {
        enemyMaskDetection = 1 << LayerMask.NameToLayer(enemyMask.ToString()); //Creates the layermask to allow detection with OverlapCircleAll
    }

    private void FixedUpdate()
    {
        if (HasNoTarget())
        {
            FindNewTarget();
        }
        else if (TargetIsOutOfRange())
        {
            LooseTarget();

        }

    }

    public GameObject GetTarget()
    {
        return target;
    }

    private void LooseTarget()
    {
        target = null;
    }

    private bool HasNoTarget()
    {
        return target == null;
    }

    private bool TargetIsOutOfRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) > radius ||
               Physics2DExtensions.DetectWallBetweenTwoPoints(gameObject.transform.position, target.transform.position);
    }

    private void FindNewTarget()
    {
        Collider2D[] targets =
            Physics2D.OverlapCircleAll(transform.position, radius, enemyMaskDetection);
        if (targets.Length != 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                target = targets[i].gameObject;
                if (!Physics2DExtensions.DetectWallBetweenTwoPoints(gameObject.transform.position, target.transform.position))
                {
                    break;
                }
                else
                {
                    target = null;
                }
            }


        }
    }


}
