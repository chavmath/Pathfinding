using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 3f;
    private CharacterController characterController;

    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        characterController.SimpleMove(movement * speed);

        // Check if the player is grounded
        if (characterController.isGrounded)
        {
            // Reset vertical velocity when grounded
            verticalVelocity = 0f;

            // Check for jump input
            if (Input.GetButtonDown("Jump"))
            {
                // Add a vertical force to make the player jump
                verticalVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
            }
        }

        // Apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Apply vertical velocity to move the player vertically
        movement.y = verticalVelocity;
        characterController.Move(movement * Time.deltaTime);
    }
}
