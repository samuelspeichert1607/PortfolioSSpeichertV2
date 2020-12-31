using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory instance = null;

    private static BulletFactory Instance
    {
        get
        {
            #if UNITY_EDITOR
                Debug.Assert(instance != null, "Make sure that BulletFactory is the first" + "script to be created");
            #endif
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }
        
    public void OnDestroy()
    {
        instance = null;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
