using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    private bool step = true;

    AudioSource audioSource;

    [SerializeField]
    private AudioClip[] ground;

    [SerializeField]
    private float audioStepLengthWalk = 0.50f;

    [SerializeField]
    private float audioStepLengthRun = 0.25f;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CharacterController controller = GetComponent<CharacterController>();

        if(controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Ground" && step == true ||
           controller.isGrounded && controller.velocity.magnitude < 7 && controller.velocity.magnitude > 5 && hit.gameObject.tag == "Untagged" && step == true)
        {
            WalkOnGround();
        }
    }

    // Update is called once per frame
    private IEnumerator WalkOnGround()
    {
        step = false;
        audioSource.clip = ground[Random.Range(0, ground.Length)];
        audioSource.volume = 0.1f;
        audioSource.Play();
        yield return new WaitForSeconds(audioStepLengthWalk);
        step = true;
    }
}
