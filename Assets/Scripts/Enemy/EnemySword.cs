using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponData weaponData;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //cam = Camera.main;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ranged Enemy"))
        {
            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (damageable != null)
            {
                damageable.Damage(weaponData.AttackDamage);
            }
        }
        else if (collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
    }


}//CLASS









