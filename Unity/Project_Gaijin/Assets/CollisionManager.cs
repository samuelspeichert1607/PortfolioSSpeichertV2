using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    private Rigidbody2D rigidbody2D;
    
	private void Awake ()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the arrow hits a wood surface, it will stick on it.
        if(collision.gameObject.name == Constants.Layers.Wood)
        {
            rigidbody2D.simulated = false;
        }
    }
}
