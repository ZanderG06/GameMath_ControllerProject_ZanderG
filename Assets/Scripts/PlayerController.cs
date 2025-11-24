using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    public bool groundedPlayer;
    public float speed;
    public float jumpHeight;
    public float gravity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if(groundedPlayer) //Check if player is on the ground
        {
            velocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Gets player input
        if (move != Vector3.zero) //If input detected, move player
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer) //Gets jump input and checks if player is already jumping
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity); //Sqrt square roots jumpHeight * -2f * gravity
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * Time.deltaTime * speed); //Frame independant
        controller.Move(velocity * Time.deltaTime);
    }
}