using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : SubWeapon
{    
    private SpriteRenderer spriteRenderer;
    [Header("Components")]
    [SerializeField] private LineRenderer lineRenderer;
    private PlayerHealth playerHealth;
    private Camera cam;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float shotgunAngle;
    private Vector3 mousePos;

    [Header("Position")]
    [SerializeField] private Transform playerHips;
    [SerializeField] private Transform enemyHips;
    [SerializeField] private Transform firePos;

    [Header("Bool")]
    [SerializeField] public bool isPlayer;
    [Header("Sound FX")]
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isPlayer = transform.root.CompareTag("Player");
        cam = Camera.main;
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //isPlayer = transform.root.CompareTag("Player");
        //cam = Camera.main;
        //playerHealth = GetComponentInParent<PlayerHealth>();
    }
    private void OnEnable()
    {
        GameplayController.OnPressedBulletButton += ProjectileShoot;
    }
    private void OnDisable()
    {
        GameplayController.OnPressedBulletButton -= ProjectileShoot;
    }
    private void FixedUpdate()
    {
        spriteRenderer.sprite = weaponData.Icon;
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerHips = GameObject.Find(NameTag.PLAYER_HIPS).transform;

        if (!isPlayer)
        enemyHips = GameObject.Find(NameTag.ENEMY_HIPS).transform;

        if (playerHealth.isPlayerAlive)
        {
            //ProjectileShoot();
        }
        //FlipGun();
    }
    private void Shoot()
    {
        switch (weaponData.typeControl)
        {
            case TypeControlAttack.Click:
                if (Input.GetMouseButtonUp(0) && isPlayer)
                {
                    CallAttack();
                }
                break;
            case TypeControlAttack.Hold:
                if (Input.GetMouseButton(0) && isPlayer)
                {
                    CallAttack();
                }
                break;
        }
    }
 
    #region RaycastShoot
    public override IEnumerator ProcessShoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePos.position, (mousePos - firePos.position).normalized, 100, layerMask: targetLayer);
        if (hitInfo)
        {
            hitInfo.transform.parent.TryGetComponent(out IDamageable damageable);
            hitInfo.transform.TryGetComponent(out Rigidbody2D enemyRb);

            if (damageable != null)
            {
                print(weaponData.AttackDamage);
                damageable.Damage(weaponData.AttackDamage);
                if (enemyRb != null) enemyRb.AddForce((hitInfo.transform.position - transform.position) * weaponData.ForceBody / 6);
            }

            lineRenderer.SetPosition(0, firePos.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePos.position);
            lineRenderer.SetPosition(1, firePos.position - firePos.right * 40);
        }
        rb.AddForce((rb.gameObject.transform.position - firePos.position) * weaponData.ForceBody);

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.015f);
        lineRenderer.enabled = false;
    }
    #endregion
    private void FlipGun()
    {
        if (transform.root.CompareTag("Player"))
        {
            if (mousePos.x < playerHips.transform.position.x)
            {
                spriteRenderer.flipY = false;
            }
            else
            {
                spriteRenderer.flipY = true;
            }
        }
        else //Enemy
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
    public void ProjectileShoot()
    {
        if (isPlayer && playerHealth.isPlayerAlive)
        {
            if (weaponData.nameWp != NameWeapon.SHOTGUN)
            {
                SmartPool.instance.SpawnFromPool("Bullet", 
                    position: firePos.position, 
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: firePos.rotation, 
                    damage: weaponData.AttackDamage, 
                    timeExist:weaponData.TimeExist);             
            }
            else
            {
                SmartPool.instance.SpawnFromPool("Bullet",
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: firePos.rotation,
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);
                SmartPool.instance.SpawnFromPool("Bullet",
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: Quaternion.Euler(firePos.eulerAngles.x, firePos.eulerAngles.y, firePos.eulerAngles.z + shotgunAngle),
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);
                SmartPool.instance.SpawnFromPool("Bullet",
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: Quaternion.Euler(firePos.eulerAngles.x, firePos.eulerAngles.y, firePos.eulerAngles.z - shotgunAngle),
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);
                SmartPool.instance.SpawnFromPool("Bullet",
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: Quaternion.Euler(firePos.eulerAngles.x, firePos.eulerAngles.y, firePos.eulerAngles.z + 2*shotgunAngle),
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);
                SmartPool.instance.SpawnFromPool("Bullet",
                    position: firePos.position,
                    direction: new Vector3(-transform.root.localScale.x, 0f, 0f),
                    rotation: Quaternion.Euler(firePos.eulerAngles.x, firePos.eulerAngles.y, firePos.eulerAngles.z - 2*shotgunAngle),
                    damage: weaponData.AttackDamage,
                    timeExist: weaponData.TimeExist);                

            }
            audioSource.Play();
            rb.AddForce((rb.gameObject.transform.position - firePos.position) * weaponData.ForceBody);
        }
    }
}

