using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceMuscle : MonoBehaviour
{
    public Muscle[] muscles;

    private void FixedUpdate()
    {
        foreach (Muscle muscle in muscles)
        {
            muscle.ActivateMuscle();
        }
    }

}
[System.Serializable]
public class Muscle 
{
    public float restingAngle = 0f;
    public float force = 750f;
    [SerializeField] private Rigidbody2D rb;
 
    public void ActivateMuscle()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force));
    }    
}
