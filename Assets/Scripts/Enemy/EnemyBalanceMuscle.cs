using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBalanceMuscle : MonoBehaviour
{
    public Muscle[] muscles;
    public Collider2D[] enemyColliders;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
       // enemyHealth = GetComponent<EnemyHealth>();
    }
    private void FixedUpdate()
    {
        if (enemyHealth.isAlive)
        {
            foreach (Muscle muscle in muscles)
            {
                muscle.ActivateMuscle();
            }
            //foreach(Collider2D enemyCol in enemyColliders)
            //{
            //    enemyCol.enabled = true;
            //}
        }
        //else
        //{
        //    foreach (Collider2D enemyCol in enemyColliders)
        //    {
        //        enemyCol.enabled = false;
        //    }
        //}
    }

}
[System.Serializable]
public class EnemyMuscle
{
    public float restingAngle = 0f;
    public float force = 750f;
    [SerializeField] private Rigidbody2D rb;

    public void ActivateMuscle()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force));
    }
}