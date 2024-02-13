using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Vector3 dir;
    float force;
    Rigidbody rigid;
    bool isGrenade = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrenade)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, dir, force * Time.deltaTime);
        CheckDestination();
    }

    private void CheckDestination()
    {
        if(Vector3.Distance(transform.position, dir) == 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 _dir, float _force)
    {
        rigid.useGravity = false;
        dir = _dir;
        force = _force;
    }

    public void AddForce(float _force)
    {
        isGrenade = true;
        rigid.useGravity = true;
        rigid.AddForce(transform.rotation * Vector3.forward * _force, ForceMode.Impulse);
    }
}
