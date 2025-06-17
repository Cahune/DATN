using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerControllerScene2 : MonoBehaviour
{
    public Transform Camera;
    public Transform groundCheck;
    public float speed = 4f, runSpeed = 8f, sensitivity = 100f, gravity = -9.81f, jumpHeight = 3f;
    //public float maxYAngle = 60f;

    public LayerMask groundMask;
    public CharacterController characterController;

    private float initialYRot;
    private Vector3 velocity;
    float xRot, yRot;
    private bool isGrounded;
    private float walkSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Khởi tạo hướng ban đầu
        //initialYRot = transform.localRotation.eulerAngles.y; // Lưu hướng ban đầu
        //yRot = initialYRot; // Khởi tạo yRot bằng hướng ban đầu
        //xRot = Camera.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //yRot += mouseX;
        xRot -= mouseY;

        //float relativeYRot = yRot - initialYRot; 
        //relativeYRot = Mathf.Clamp(relativeYRot, -maxYAngle, maxYAngle); 
        //yRot = initialYRot + relativeYRot; 

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        transform.Rotate(Vector3.up * mouseX);
        Camera.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        
    }

    private void FixedUpdate()
    {
        if (!characterController.enabled) return;
        //movement


        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = runSpeed;
        }
        else walkSpeed = speed;

        float x = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;

        Vector3 move = this.transform.forward * y + this.transform.right * x;
        characterController.Move(move);

        //add gravity
        velocity.y += gravity * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (velocity.y < 0 && isGrounded) //on ground
        {
            velocity.y = 0;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        //Debug.Log(isGrounded);
        characterController.Move(velocity * Time.deltaTime);
    }
}
