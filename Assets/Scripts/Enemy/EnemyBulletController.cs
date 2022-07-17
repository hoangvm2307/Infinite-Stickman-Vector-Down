using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [Header("Rigid Body")]
    private Rigidbody2D rb;
    private Vector3 direction;
    [Header("Agents")]
    [SerializeField] private float speed;
    [SerializeField] private float force;
    private int playerBulletLayer = 11;
    private int suppliesLayer = 10;
    private TrailRenderer trailRenderer;
    [SerializeField] private GameObject bulletFX;
    public int damage;
    [SerializeField] private HealthManager healthManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();

    }
    void Start()
    {
        //trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(WaitForDeactivate());
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
        yield return new WaitForSeconds(1.5f);
        trailRenderer.Clear();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != suppliesLayer && !collision.CompareTag("Boundary"))
        {

            IDamageable damageable = collision.transform.parent.GetComponent<IDamageable>();
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (damageable != null)
            {
                damageable.Damage(damage);
                if (enemyRb != null) enemyRb.AddForce((collision.transform.position - transform.position) * force);
            }
        }
        Instantiate(bulletFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
