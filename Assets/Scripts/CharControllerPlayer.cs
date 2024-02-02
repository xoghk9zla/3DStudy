using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;    
    private float verticalVelocity = 0.0f;
    private float gravity = 9.81f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;
    private bool isSlope = false;
    private Vector3 slopeVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();
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
        characterController.Move(transform.rotation * moveDir * Time.deltaTime);
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
        if (!isGround)
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
        verticalVelocity -= gravity * Time.deltaTime;

        if (isGround)
        {
            verticalVelocity = 0.0f;
        }

        if (isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }

        characterController.Move(new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

    }
}
