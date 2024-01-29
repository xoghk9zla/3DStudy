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
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Rotating();
    }

    private void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=
                //transform.forward *mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
                Vector3.forward * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position +=
                //-transform.forward * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
                Vector3.back * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
                //transform.right * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
                Vector3.left * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position +=
                //-transform.right * mouseMoveSpeed * Time.deltaTime;
                //transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
                //transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
                Vector3.right * mouseMoveSpeed * Time.deltaTime;
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
        transform.rotation = Quaternion.Euler(rotateValue);
    }

    private bool CheckFrame(int _limitFrame)
    {
        int curFrame = (int)(1/Time.deltaTime);
        return _limitFrame < curFrame;
    }
}
