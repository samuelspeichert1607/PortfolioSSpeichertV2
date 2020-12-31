using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private GameObject[] lavaBlocks;

    private Rigidbody2D boatRigidBody2D;

    private List<GameObject> passagers;

    private int i = 0;

    // Use this for initialization
    private void Awake()
    {
        lavaBlocks = GameObject.FindGameObjectsWithTag("LavaTile");
        boatRigidBody2D = GetComponent<Rigidbody2D>();
        passagers = new List<GameObject>();
    
        StartCoroutine("MakeBoatMove");
    }


    private void Update()
    {
        if (i == 0)
        {
            //boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[lavaBlocks.Length - 1].transform.position, 1f);
            boatRigidBody2D.MovePosition(lavaBlocks[lavaBlocks.Length - 1].transform.position);
            //boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, -0.5f);
            if (passagers.Count > 0)
            {
                foreach (GameObject passager in passagers)
                {
                    passager.transform.position = Vector3.MoveTowards(passager.transform.position, lavaBlocks[lavaBlocks.Length - 1].transform.position, 1f);
                    //passager.transform.position = new Vector3(passager.transform.position.x, passager.transform.position.y, -0.5f);
                }
            }
        }
        else
        {
            //boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[i - 1].transform.position, 1f);
            boatRigidBody2D.MovePosition(lavaBlocks[i - 1].transform.position);
            //boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, -0.5f);

            if (passagers.Count > 0)
            {
                foreach (GameObject passager in passagers)
                {
                    passager.transform.position = Vector3.MoveTowards(passager.transform.position, lavaBlocks[i - 1].transform.position, 1f);
                    //passager.transform.position = new Vector3(passager.transform.position.x, passager.transform.position.y, -0.5f);
                }
            }
            
        }
        boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[i].transform.position, 1f);
        //boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, -0.5f);
        
    }

    private IEnumerator MakeBoatMove()
    {
        for (;;)
        {
            i++;

            if (i == lavaBlocks.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            passagers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            passagers.Remove(other.gameObject);
        }
    }

}
