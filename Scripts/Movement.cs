///Made by @MrDeadLord
///It's base Character & Camera movement script
///No matter if it 1st or 3rd person, just assign right object to _camera
///Apply any suggestions of making it shorter or better ^_^
///Hope it'll help anyone
///
///P.S. Look at Input.GetAxis() in CharacterMove() and make shure you have equal names in input and here
///P.S.S. Same in CameraMoving()

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    #region Variables
    [Header("Camera settings")]
    [SerializeField] [Tooltip("Mouse sensitivity. Multiply param.")] private float sensitivity = 150;
    [SerializeField] [Tooltip("Max/min(up/down) angles of camera")] private float maxAngle = 80, minAngle = -80;
    [SerializeField] [Tooltip("Camera(if 1st)/transform obj(if 3rd)")] private Transform camera;
    float mouseX, mouseY;
    float camRot = 0;

    [Header("Movement settings")]
    [SerializeField] private float walkSpeed = 8;
    [SerializeField] private float runSpeed = 12;
    /// <summary>
    /// Use to take control from player and give it back
    /// </summary>
    [HideInInspector] public bool canMove = true;
    Vector3 inputForce = Vector3.zero;
    CharacterController charContr;
    
    [Header("Jump settings")]
    [SerializeField] [Tooltip("Jump hight")]private float jumpForce = 5;
    float gravityForce;
    #endregion

    #region UnityTime
    void Awake()
    {
        //Disabling cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        charContr = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!canMove)
            return;

        CameraMoving();

        if (Input.GetButton("Run"))
            CharacterMove(true);
        else
            CharacterMove(false);

        LocalGravity();
    }
    #endregion

    private void CameraMoving()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        camRot -= mouseY;
        camRot = Mathf.Clamp(camRot, minAngle, maxAngle);

        camera.localRotation = Quaternion.Euler(camRot, 0, 0);
        transform.Rotate(Vector3.up * mouseX);  //Moving body right with camera. Adjust it if 3rd person
    }

    /// <summary>
    /// Moving char by WASD
    /// </summary>
    /// <param name="isRunning">Running param.</param>
    private void CharacterMove(bool isRunning)
    {
        if (charContr.isGrounded)
        {
            float x = Input.GetAxis("Left/Right") * (isRunning ? runSpeed : walkSpeed);
            float z = Input.GetAxis("Forward/Back") * (isRunning ? runSpeed : walkSpeed);

            inputForce = transform.right * x + transform.forward * z;
        }

        inputForce.y = gravityForce;
        charContr.Move(inputForce * Time.deltaTime);

    }

    /// <summary>
    /// Making char fall to the ground without floating
    /// </summary>
    private void LocalGravity()
    {
        if (!charContr.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1;

        if (Input.GetButtonDown("Jump") && charContr.isGrounded)
            gravityForce = jumpForce;
    }
}
