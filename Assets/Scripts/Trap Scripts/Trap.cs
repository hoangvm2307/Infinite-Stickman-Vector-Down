using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damage);
        }
    }
}
