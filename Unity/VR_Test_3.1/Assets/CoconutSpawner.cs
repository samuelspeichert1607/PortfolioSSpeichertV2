using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    private Timer timer1;
    public GameObject coconut;

    private Random random;
    private float randomXposition;
    private float randomZposition;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    private void Update()
    {
        randomXposition = Random.Range(-3f, 3);
        randomZposition = Random.Range(-3f, 3);
    }

    private IEnumerator ExampleCoroutine()
    {
        //After we have waited 5 seconds print the time again.
        for (;;)
        {
            // execute block of code here
            Instantiate(coconut, new Vector3(randomXposition, 5, randomZposition), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

    }
}
