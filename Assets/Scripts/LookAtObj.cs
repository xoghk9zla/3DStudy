using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObj : MonoBehaviour
{
    [SerializeField] Transform trsLookAt;

    // Update is called once per frame
    void Update()
    {
        if(trsLookAt == null)
        {
            return;
        }

        transform.LookAt(trsLookAt);
    }
}
