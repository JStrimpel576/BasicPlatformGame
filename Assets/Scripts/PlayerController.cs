using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb;
    private float inputHorizontal;
    //because this is public, it can be accessed through the Unity Editor
    public float horizontalMoveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //I can only get this component because the Rigidbody2D is attached to the player
        //This script is also attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        //jump();
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

        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }
}
