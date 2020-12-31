using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;

    private Transform barrell;
    private AudioSource audioSource;

    public AudioClip audioClip;

    private void Start()
    {
        barrell = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrell.position, barrell.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrell.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }
}
