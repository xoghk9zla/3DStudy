using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    private float gravity = 9.81f;
    private float verticalVelocity = 0.0f;
    [SerializeField] private bool isGround = false;
    private bool isJump = false;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float mouseSensitvity = 5.0f;

    private Vector2 rotateValue;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
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
        if(rigid.velocity.y < 0.0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
    }

    private void Moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.z = Input.GetAxisRaw("Vertical");
        
        rigid.velocity = transform.rotation * moveDir * moveSpeed;
    }

    private void Jumping()
    {
        if (!isGround)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
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

        if(isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
    }
}
