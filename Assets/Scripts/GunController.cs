using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject objBullet;
    [SerializeField] Transform trsMuzzle;
    [SerializeField] Transform trsDynamic;
    [SerializeField] float gunForce = 100.0f;
    private Camera camMain;
    private float distance = 250.0f;

    [SerializeField] private bool isGrenade;

    private void Start()
    {
        camMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GunPointer();
        CheckFire();
        CheckGrenade();
    }

    // 총기가 카메라 한가운데 보이는 오브젝트를 노리도록 만들어줌
    private void GunPointer()
    {
        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward, 
            out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);
        }
        else   // 그라운드 오브젝트에 닿지 않았을 때
        {
            Vector3 lookPos = camMain.transform.position + camMain.transform.forward * distance;
            transform.LookAt(lookPos);
        }
    }

    private void CheckFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        GameObject go = Instantiate(objBullet, trsMuzzle.position, trsMuzzle.rotation, trsDynamic);
        BulletController sc = go.GetComponent<BulletController>();

        if (isGrenade)  // 유탄일 때
        {
            sc.AddForce(gunForce * 0.5f);
        }
        else
        {
            if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, distance, LayerMask.GetMask("Ground")))
            {
                sc.SetDestination(hit.point, gunForce);
            }
            else
            {
                Vector3 lookPos = camMain.transform.position + camMain.transform.forward * 1000.0f;
                sc.SetDestination(lookPos, gunForce);
            }
        }        
    }

    private void CheckGrenade()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = true;
        }
    }
}
