using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private Rigidbody2D rb;
    private Transform playerHips;
    private Transform enemyHips;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private AudioSource shootSound;
    private void Awake()
    {
        playerHips = GameObject.Find("Player_Hips").transform;
        enemyHips = GameObject.Find("Enemy_Hips").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
         
        //playerHips = GameObject.Find("Player_Hips").transform;
        //enemyHips = GameObject.Find("Enemy_Hips").transform;
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(EnemyShoot());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FlipGun();
    }
    IEnumerator EnemyShoot()
    {
        yield return new WaitForSeconds(2.5f);
        shootSound.Play();
        SmartPool.instance.SpawnFromPool("Enemy Bullet", 
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: firePos.rotation, 
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);
        rb.AddForce((rb.gameObject.transform.position - firePos.position) * 1000);
        StartCoroutine(EnemyShoot());
    }
    private void DeactivateEnemyWeapon()
    {
        StopCoroutine(EnemyShoot());
    }
    private void FlipGun()
    {
        if (playerHips.position.x < enemyHips.position.x)
        {
            spriteRenderer.flipY = false;
        }
        else
        {
            spriteRenderer.flipY = true;
        }
    }
}
