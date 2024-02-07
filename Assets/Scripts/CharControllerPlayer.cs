using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;

    private Camera camMain;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;    
    private float verticalVelocity = 0.0f;
    private float gravity = 9.81f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isSlope = false;
    private Vector3 slopeVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        camMain = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        CheckMouseLock();
        Rotation();
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();
        CheckSlop();
    }

    private void CheckMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            switch(Cursor.lockState)
            {
                case CursorLockMode.Locked:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case CursorLockMode.None:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                default:
                    break;
            }
        }
    }

    private void Rotation()
    {
        transform.rotation = Quaternion.Euler(0.0f, camMain.transform.eulerAngles.y, 0.0f);
    }

    private void CheckGround()
    {
        if(verticalVelocity < 0.0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        else if (verticalVelocity > 0.0f)
        {
            isGround = false;
        }
    }

    private void Moving()
    {
        moveDir = new Vector3(InputHorizontal(), 0.0f, InputVertical());

        if (isSlope)
        {
            characterController.Move(-slopeVelocity * Time.deltaTime);
        }
        else 
        {
            characterController.Move(transform.rotation * moveDir * Time.deltaTime);
        }        
    }

    private float InputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private float InputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void Jumping()
    {
        if (!isGround || isSlope)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void CheckGravity()
    {
        if (isGround)
        {
            verticalVelocity = 0.0f;
        }

        if (isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move(new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void CheckSlop()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, characterController.height, LayerMask.GetMask("Ground", "Roof")))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            
            if (angle >= characterController.slopeLimit)
            {
                isSlope = true;
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0.0f, gravity, 0.0f), hit.normal);
            }
            else
            {
                isSlope = false;
            }
        }
    }
}
