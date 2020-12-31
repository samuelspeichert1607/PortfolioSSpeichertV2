using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    private Movement keyboardInput;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject spawnPoint;

    // Use this for initialization
    void Awake () {
        keyboardInput = GetComponent<Movement>();
    }
	
	// Update is called to activate an event
	void OnEnable () {
		
	}

    // Update is called to deactivate an event
    void OnDisable()
    {
        //RaycastHit2D[] = Physics2D.CircleCastAll();
        //foreach(RaycastHit2D hit3D in all)
        //{

        //}
    }

    private void OnFireBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
