using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float restingAngle = 0f;
    public float force = 750f;

    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ActivateMuscle());
    }

    private void FixedUpdate() 
    {
        //rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force));
    }
    IEnumerator ActivateMuscle()
    {
        yield return new WaitForSeconds(3);
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force));
        StartCoroutine(ActivateMuscle());
    }
}
