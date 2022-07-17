using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [Header("Force Ability")]
    public float fieldOfImpact;
    public float force;
    public LayerMask targetMask;
    public float manaCost;
    void Start()
    {
        
    }

    public void ForceFieldAbility(Vector3 position)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(point: position, radius: fieldOfImpact, layerMask: targetMask);
        foreach (Collider2D obj in objects)
        {
            if (!obj.CompareTag("Enemy Spawner"))
            {
                Vector2 direction = obj.transform.position - position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }
        }
    }
}
