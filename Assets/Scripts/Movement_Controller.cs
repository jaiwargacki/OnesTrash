using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement_Controller : MonoBehaviour
{

    public Camera playerCamera;
    
    public float speed = 6.0f;
    public float sprintSpeed = 15.0f;
    public float crouchSpeed = 2.0f;

    public float jumpSpeed = 8.0f;
    public float gravity = 9.8f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Movement
        Vector3 forward = playerCamera.transform.TransformDirection(Vector3.forward);
        Vector3 right = playerCamera.transform.TransformDirection(Vector3.right);

        float curSpeed = canMove ? (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : (Input.GetKey(KeyCode.LeftControl) ? crouchSpeed : speed)) : 0;
        float curSpeedX = curSpeed * Input.GetAxis("Vertical");
        float curSpeedY = curSpeed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Jump
        if (characterController.isGrounded && Input.GetButton("Jump") && canMove)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        characterController.Move(moveDirection * Time.deltaTime);

        #region Rotation
        if (canMove) 
        {
            bool isMoving = moveDirection.x != 0 || moveDirection.z != 0;
            if (isMoving)
            {
                Vector3 moveDirectionNoY = new Vector3(moveDirection.x, 0, moveDirection.z);
                Quaternion goalRotation = Quaternion.LookRotation(moveDirectionNoY);
                transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 0.01f);
            }
        }
        #endregion
    }
}
