using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Rigid Body")]
    private Rigidbody2D rb;
    private Vector3 direction;
    [Header("Agents")]
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private int playerBulletLayer = 11;
    private int suppliesLayer = 10;
    private int cannotShootLayer = 13;
    private int abilityLayer = 14;
    private int enemyBulletLayer = 12;
    private TrailRenderer trailRenderer;
    [SerializeField] private GameObject bulletFX;
    public int damage;
    public float timeExist;
    [SerializeField] private HealthManager healthManager;
    private void OnEnable()
    {
        StartCoroutine(WaitForDeactivate());
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    void Start()
    {
        
    }
    private void OnDisable()
    {
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(timeExist);
        trailRenderer.Clear();
        if (this.gameObject.layer == enemyBulletLayer)
        {
            SmartPool.instance.SpawnObjectFromPool("Enemy Bullet FX", transform.position, Quaternion.identity);
        }
        else
        {
            SmartPool.instance.SpawnObjectFromPool("Bullet FX", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool cannotShoot = !collision.CompareTag("Ammo") && !collision.CompareTag("Boundary") && !collision.CompareTag("Weapon Collector");
        if (gameObject.layer == playerBulletLayer && collision.gameObject.layer != cannotShootLayer)
        {
            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (damageable != null)
            {
                damageable.Damage(damage);
                if (enemyRb != null) enemyRb.AddForce((collision.transform.position - transform.position) * force);
            }   
            //if(collision.CompareTag("Enemy Spawner"))
            //{
            //    SmartPool.instance.SpawnObjectFromPool("Enemy Spawner Blood FX", collision.transform.position, Quaternion.identity);
            //    print("True");
            //}
        }
        else if(collision.gameObject.layer != suppliesLayer && !collision.CompareTag("Boundary") 
            && collision.gameObject.layer != cannotShootLayer && collision.gameObject.layer != abilityLayer)
        {
            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (damageable != null)
            {
                damageable.Damage(damage);
                if (enemyRb != null) enemyRb.AddForce((collision.transform.position - transform.position) * force);
            }
        }
        if (this.gameObject.layer == 12)//12 = enemy bullet layer 
        {
            SmartPool.instance.SpawnObjectFromPool("Enemy Bullet FX", transform.position, Quaternion.identity);
        }
        else
        {
            SmartPool.instance.SpawnObjectFromPool("Bullet FX", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
