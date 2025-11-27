using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private float pitchAngle = 0f;
    public bool groundedPlayer;
    public bool isCrouch;
    public float speed;
    public float jumpHeight;
    public float gravity;
    public float pitchSpeed;
    public float yawSpeed;
    public float speedSprint;
    public float normalSpeed;
    public float crouchSpeed;
    public float cameraOffset;
    public GameObject playerCamera;
    private Vector3 currentCamera;
    private Vector3 crouchCamera;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        UnityEngine.Cursor.visible = false;

        currentCamera = playerCamera.transform.localPosition;
        crouchCamera = currentCamera + new Vector3(0, cameraOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer) //Check if player is on the ground
        {
            velocity.y = 0;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraLeftRight = playerCamera.transform.right;
        cameraLeftRight.y = 0f;
        cameraLeftRight.Normalize();

        Vector3 move = cameraForward * vertical + cameraLeftRight * horizontal;

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Gets player input
        
        if (move != Vector3.zero) //If input detected, move player
        {
            //gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer) //Gets jump input and checks if player is already jumping
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity); //Sqrt square roots jumpHeight * -2f * gravity
        }

        

        //Sprinting & Crouching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
            isCrouch = true;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, crouchCamera, 1);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !isCrouch) 
        {
            speed = speedSprint;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, currentCamera, 1);
        }
        else
        {
            speed = normalSpeed; //revert speed back to normal
            isCrouch = false;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, currentCamera, 1);
        }

        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime); //Frame independant
        controller.Move(velocity * Time.deltaTime);

        //Camera Controller
        float mouseY = Input.GetAxis("Mouse Y") * pitchSpeed;
        float mouseX = Input.GetAxis("Mouse X") * yawSpeed;

        pitchAngle -= mouseY;
        pitchAngle = Mathf.Clamp(pitchAngle, -90f, 90f); //Locks pitch rotation

        playerCamera.transform.localRotation = Quaternion.Euler(pitchAngle, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        
    }
}