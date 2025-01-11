using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject player;

    public float thirdPersonDistance = 10.0f;
    public float lookSpeed = 2.0f;

    private Vector3 playLastPosition;

    void Start()
    {
        playerCamera.transform.position = player.transform.position - player.transform.forward * thirdPersonDistance;
        playerCamera.transform.LookAt(player.transform);
    }

    void Update()
    {
        #region Camera Movement
        Vector3 moveDirection = player.transform.position - playLastPosition;
        playerCamera.transform.position += moveDirection;
        playLastPosition = player.transform.position;
        #endregion

        #region Camera Around Player
        float xMove = Input.GetAxis("Mouse Y") * lookSpeed;
        float yMove = Input.GetAxis("Mouse X") * lookSpeed;
        
        playerCamera.transform.RotateAround(player.transform.position, Vector3.up, yMove);
        playerCamera.transform.RotateAround(player.transform.position, playerCamera.transform.right, -xMove);
        
        if (playerCamera.transform.position.y < player.transform.position.y)
        {
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, player.transform.position.y, playerCamera.transform.position.z);
        }

        playerCamera.transform.LookAt(player.transform);
        #endregion
        
    }
}
