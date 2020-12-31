using UnityEngine;
using System;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public delegate void InputHandler(Inputs e);

    public event InputHandler InputEvent;

    private Inputs currentInput;

    [SerializeField]
    private int speed;

    [SerializeField]
    private float jumpHeight = 8.0f;

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private int MilisecondsBeforeStop;

    [SerializeField]
    private int maxMilisecondsBeforeStop;

    private Thread ThreadTillStop;

    private double milisecondsToGoBeforeStop;
    
    private Rigidbody2D rigidbody2D;

    private SpriteRenderer spriteRenderer;

    private bool onGround;

    private bool isCrouched;

    private bool shouldStop;

    private bool canShoot;

    private bool canJump;

    private bool stickingToWall;

    private int numberOfJumps;

    private Vector3 directionalVector;

    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private Animator animator;
    
    private DateTime dateForShooting = default(DateTime);
    private DateTime dateForJumping = default(DateTime);
    private TimeSpan remainingTimeToShoot;
    private TimeSpan remainingTimeToJump;

    // Use this BEFORE the initialization
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        GetComponent<KeyboardController>().InputEvent += getInput;

        isCrouched = false;
        shouldStop = false;
        canShoot = true;
        canJump = true;
        stickingToWall = false;
        numberOfJumps = 0;
        directionalVector = new Vector3(1, 1, 1);
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        if (!canShoot)
        {
            remainingTimeToShoot = dateForShooting - DateTime.Now;
            if (remainingTimeToShoot.Seconds == 0)
            {
                canShoot = true;
            }
        }

        if (!canJump)
        {
            remainingTimeToJump = dateForJumping - DateTime.Now;

            if (remainingTimeToJump.Seconds == 0)
            {
                canJump = true;
            }
        }

        if (shouldStop)
        {
            Stop();
            shouldStop = false;
        }

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            isCrouched = false;
            canJump = true;
            //boxCollider2D.size = new Vector2(0.5466604f, 1.79438f);
            //boxCollider2D.offset = new Vector2(0f, 0.02f);
        }

        animator.SetFloat(Constants.AnimatorParameters.Speed, speed);
        animator.SetFloat(Constants.AnimatorParameters.VelocityX, rigidbody2D.velocity.x);
        animator.SetFloat(Constants.AnimatorParameters.VelocityY, rigidbody2D.velocity.y);
        animator.SetBool(Constants.AnimatorParameters.Shooting, !canShoot);
        animator.SetBool(Constants.AnimatorParameters.OnGround, onGround);
        animator.SetBool(Constants.AnimatorParameters.IsCrouched, isCrouched);
        animator.SetBool("stickingToWall", stickingToWall);
    }

    private void getInput(Inputs input)
    {
        currentInput = input;

        if (input == Inputs.MoveLeft)
        {
            Move(-8);
        }
        if (input == Inputs.MoveRight)
        {
            Move(8);
        }
        if (input == Inputs.Jump)
        {
            Jump();
        }
        if (input == Inputs.Crouch)
        {
            Crouch();
        }
        if (input == Inputs.AimUp)
        {
            //AimUp
        }
        if (input == Inputs.Pause)
        {
            //Pause();
        }
        if (input == Inputs.SwitchToBow1)
        {
            //SwitchToBow1();
        }
        if (input == Inputs.ThrowAnArrow)
        {
            ThrowAnArrow();
        }
    }
    
    public void Move(int speed)
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);

        if (rigidbody2D.velocity.x < 0)
        {
            directionalVector = new Vector3(-1, 1, 1);
        }
        else
        {
            directionalVector = new Vector3(1, 1, 1);
        }

        AddTimeToThread(MilisecondsBeforeStop);
        if (ThreadTillStop == null || ThreadTillStop.ThreadState == ThreadState.Stopped)
        {
            ThreadTillStop = new Thread(() =>
            {
                CountToZeroThenStop();
            });
            ThreadTillStop.Start();
        }

    }

    public void Stop()
    {
        rigidbody2D.velocity = new Vector2(x: 0, y: rigidbody2D.velocity.y);
    }

    private void CountToZeroThenStop()
    {
        while (milisecondsToGoBeforeStop > 0)
        {
            Thread.Sleep(1);
            milisecondsToGoBeforeStop--;
        }
        shouldStop = true;
        //Debug.Log("Stop");
    }

    private void AddTimeToThread(int time)
    {
        milisecondsToGoBeforeStop += time;
        if (milisecondsToGoBeforeStop > maxMilisecondsBeforeStop)
        {
            milisecondsToGoBeforeStop = maxMilisecondsBeforeStop;
        }
    }
    
    public void Jump()
    {
        if(numberOfJumps < 2)
        {
            if (canJump)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
                dateForJumping = DateTime.Now.Add(TimeSpan.FromSeconds(1.5));
                canJump = false;
                numberOfJumps++;
            }
        }
    }

    int landingNumber = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Solid")
        {
            if(currentInput == Inputs.MoveRight && Math.Abs(transform.position.x - collision.gameObject.transform.position.x) < 1)
            {
                stickingToWall = true;
            }
            if (collision.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                
                //Debug.Log("Landing #" + landingNumber);
                onGround = true;
                numberOfJumps = 0;
            }
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                landingNumber++;
                Debug.Log("Walljump #" + landingNumber);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Math.Abs(transform.position.x - collision.gameObject.transform.position.x) > 1)
        {
            stickingToWall = false;
        }

        if (collision.gameObject.tag == "Solid" && rigidbody2D.velocity.y != 0 )
        {
            onGround = false;
        }
    }

    public void Crouch()
    {
        isCrouched = true;
        canJump = false;
        //boxCollider2D.size = new Vector2(boxCollider2D.size.x, boxCollider2D.size.y / 2);
        //boxCollider2D.offset = new Vector2(0f, -0.42f);
    }

    public void ThrowAnArrow()
    {
        if (canShoot)
        {
            CreateArrow();
            dateForShooting = DateTime.Now.Add(TimeSpan.FromSeconds(2));
            canShoot = false;
        }
    }

    private void CreateArrow()
    {
        bool canBeCreated = false;

        //For the arrow trajectory
        Vector3 arrowPosition = new Vector3(
                transform.position.x + (1.5f * directionalVector.x),
                transform.position.y,
                transform.position.z);
        
        //The types of layers that can be supported by the imminent raycast.
        string[] layers = new string[2] { Constants.Layers.Wood, Constants.Layers.Block };

        float arrowLength = arrow.GetComponent<SpriteRenderer>().bounds.size.x;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionalVector, 1500f,LayerMask.GetMask(layers));
        
        //If it detects a wall.
        if (hit.collider != null)
        {
            //Then we don't want the arrow to go through the wall.
            if(hit.distance > arrowLength)
            {
                canBeCreated = true;
            }
        }
        else
        {
            //Even if he doesn't detect any wall, the character still can create an arrow.
            canBeCreated = true;
        }

        //Then we create the arrow.
        if (canBeCreated)
        {
            var movingArrow = Instantiate(arrow, arrowPosition, transform.rotation) as GameObject;

            movingArrow.transform.localScale = directionalVector;

            movingArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * 2 * movingArrow.transform.localScale.x, 0);
        }
    }
}