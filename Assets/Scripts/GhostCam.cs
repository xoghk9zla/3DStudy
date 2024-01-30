using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float mouseMoveSpeed = 5.0f;
    private Vector3 rotateValue;

    // Start is called before the first frame update
    void Start()
    {
        rotateValue = transform.rotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouse();

        Moving();
        Rotating();
    }

    private void CheckMouse()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;   // 마우스가 보이지 않도록, 항상 마우스는 화면의 가운데 존재
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }        
    }

    private void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=
                transform.forward *mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
                //Vector3.forward * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position +=
                -transform.forward * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
                //Vector3.back * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
                transform.right * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
                //Vector3.left * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position +=
                -transform.right * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
                //Vector3.right * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position +=
                transform.up * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position +=
                -transform.up * mouseMoveSpeed * Time.deltaTime;
        }
    }

    private void Rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);

        /*
        if(rotateValue.x > 90.0f)
        {
            rotateValue.x = 90.0f;
        }
        else if(rotateValue.x < -90.0f)
        {
            rotateValue.x = -90.0f;
        }
        */
        rotateValue.x = Mathf.Clamp(rotateValue.x, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(rotateValue);
    }

    private bool CheckFrame(int _limitFrame)
    {
        int curFrame = (int)(1/Time.deltaTime);
        return _limitFrame < curFrame;
    }
}
