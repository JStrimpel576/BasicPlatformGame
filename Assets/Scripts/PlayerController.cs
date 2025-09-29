using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb;
    private float inputHorizontal;
    private int maxNumJumps;
    private int numJumps;
    //because this is public, it can be accessed through the Unity Editor
    public float horizontalMoveSpeed;
    public float jumpForce;

    public float dashSpeed;
    private float maxDashTime = 1.0f;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime;
    private bool ableToDash = false;

    public GameObject doubleJumpHatLocation;
    public GameObject dashCapeLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //I can only get this component because the Rigidbody2D is attached to the player
        //This script is also attached to the player
        rb = GetComponent<Rigidbody2D>();

        maxNumJumps = 1;
        numJumps = 1;

        currentDashTime = maxDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
        dash();
    }

    private void movePlayerLateral()
    {
        //if A/D/<-/-> are pressed move player accordingly
        //Horizontal is defined in the input section of the project settings
        //the line below will return:
        //0 - no button pressed
        //1 - right arrow or D pressed
        //2 - left arrow or A pressed
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        flipPlayerSprite(inputHorizontal);
        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }

    private void flipPlayerSprite(float inputHorizontal)
    {
        //this will allow the player to look in the direction they are moving
        if (inputHorizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            numJumps++;
        }
    }

    private void dash()
    {
        if (ableToDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentDashTime = 0.0f;
            }
            if (currentDashTime < maxDashTime)
            {
                rb.linearVelocity = new Vector2(dashSpeed * inputHorizontal, rb.linearVelocity.y);
                currentDashTime += dashStoppingSpeed;
            }
        }
    }

    //Collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision will contain information about the object the player collided with
        //Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Ground"))
        {
            numJumps = 1;
        }
        else if(collision.gameObject.CompareTag("obBottom"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    //Triggers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //double jump
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            GameObject hat = collision.gameObject;
            equipDoubleJumpHat(hat);
            maxNumJumps = 2;
        }

        if(collision.gameObject.CompareTag("Dash"))
        {
            GameObject cape = collision.gameObject;
            equipDashCape(cape);
            ableToDash = true;
        }
    }

    private void equipDoubleJumpHat(GameObject hat)
    {
        hat.transform.position = doubleJumpHatLocation.transform.position;
        hat.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void equipDashCape(GameObject cape)
    {
        cape.transform.position = dashCapeLocation.transform.position;
        cape.gameObject.transform.SetParent(this.gameObject.transform);
    }
}
