using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    private float gravity = 9.81f;
    private float verticalVelocity = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float mouseSensitvity = 5.0f;

    private Vector2 rotateValue;

    private Transform trsCam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        trsCam = transform.GetChild(0);
        //trsCam = trsCam.Find("Main Camera");
    }
    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();
        Rotation();
    }

    private void CheckGround()
    {
        if (rigid.velocity.y < 0.0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        else if (rigid.velocity.y > 0.0f)
        {
            isGround = false;
        }
    }

    private void Moving()
    {
        /*
        moveDir.x = InputHorizontal();
        moveDir.z = InputVertical();      
        
        moveDir.y = rigid.velocity.y;
        
        rigid.velocity = transform.rotation * moveDir;
        */
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(new Vector3(0.0f, 0.0f, moveSpeed), ForceMode.Force);
        }        
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(new Vector3(0.0f, 0.0f, -moveSpeed), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector3(-moveSpeed, 0.0f, 0.0f), ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(moveSpeed, 0.0f, 0.0f), ForceMode.Force);
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
        if(isJump)
        {
            isJump = false;
            rigid.AddForce(new Vector3(0.0f, jumpForce), ForceMode.Impulse);
        }        
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitvity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitvity * Time.deltaTime;

        rotateValue += new Vector2(-mouseY, mouseX);

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(new Vector2(0.0f, rotateValue.y));
        trsCam.rotation = Quaternion.Euler(new Vector2(rotateValue.x, rotateValue.y));  // 캐릭터 회전만큼 카메라도 따라가야함
    }
}
