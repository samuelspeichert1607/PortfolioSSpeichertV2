using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class BoatController : GameScript
    {
        private GameObject[] lavaBlocks;

        private Rigidbody2D boatRigidBody2D;

        private int i = 0;

        float step = 1f;

        // Use this for initialization
        private void Awake()
        {
            lavaBlocks = GameObject.FindGameObjectsWithTag("LavaTile");
            boatRigidBody2D = GetComponent<Rigidbody2D>();
            StartCoroutine("MakeBoatMove");
        }


        private void Update()
        {
            if (i < 1)
            {
                //lavaBlocks[lavaBlocks.Length - 1].GetComponent<SpriteRenderer>().color = Color.red;
                //boat.transform.position = lavaBlocks[lavaBlocks.Length - 1].transform.position;
                boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[lavaBlocks.Length - 1].transform.position, step);
                boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, 0.5f);
            }
            else
            {
                //lavaBlocks[i - 1].GetComponent<SpriteRenderer>().color = Color.red;
                //boat.transform.position = lavaBlocks[i - 1].transform.position;
                boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[i - 1].transform.position, step);
                boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, 0.5f);
            }

            //lavaBlocks[i].GetComponent<SpriteRenderer>().color = Color.gray;
            //boat.transform.position = lavaBlocks[i].transform.position;
            boatRigidBody2D.transform.position = Vector3.MoveTowards(boatRigidBody2D.transform.position, lavaBlocks[i].transform.position, step);
            boatRigidBody2D.transform.position = new Vector3(boatRigidBody2D.transform.position.x, boatRigidBody2D.transform.position.y, 0.5f);

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
                yield return new WaitForSeconds(2f);
            }
        }

        private void OnTriggerCollider2D(Collider2D other)
        {

        }

    }
}

