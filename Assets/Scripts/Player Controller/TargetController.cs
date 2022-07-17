using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Vector3 diretion = target.position - transform.position;
        float angle = Mathf.Atan2(diretion.y, diretion.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}//CLASS























