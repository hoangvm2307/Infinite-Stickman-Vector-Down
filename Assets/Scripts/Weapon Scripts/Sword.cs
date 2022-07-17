using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponData weaponData;
    private int enemyLayer = 8;
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
        if (collision.gameObject.layer == enemyLayer || collision.CompareTag("Enemy Spawner"))
        {
            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (damageable != null)
            {
                damageable.Damage(weaponData.AttackDamage);
            }
        }
        else if(collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
    }


}//CLASS









