using System.Collections;
using System.Collections.Generic;
using ProjetSynthese;
using UnityEngine;

public class DestroyComponentAfterDelay : GameScript
{
    [SerializeField]
    [Tooltip("Delay before destroying the component")]
    private float delay;
    
    void Start()
    {
        StartCoroutine(DestructionCountDown(delay));
    }

    private IEnumerator DestructionCountDown(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
