using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    MELEE,
    RANGED
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float minDist, maxDist;
    private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxSpeed;
    private Vector2 movement;
    private void Awake()
    {
        playerPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;

        enemyHealth = GetComponentInParent<EnemyHealth>();
    }
    void Start()
    {
        //playerPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;
         
        //enemyHealth = GetComponentInParent<EnemyHealth>();
    }
    private void OnEnable()
    {
        StartCoroutine(EnemyChasePlayer());
        moveSpeed = maxSpeed;
    }
    // Update is called once per frame
    void Update()
    {
 
        if (!enemyHealth.isAlive)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = maxSpeed;
        }
        //if (Vector3.Distance(transform.position, playerPos.position) > maxDist)
        //{
        //    transform.parent.gameObject.SetActive(false);
        //}
    }

    void MeleeEnemyChasePlayer()
    {
        Vector3 direction = playerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
        if (Vector3.Distance(transform.position, playerPos.position) >= minDist)
        {
            //rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
            rb.AddForce((playerPos.position - transform.position).normalized * moveSpeed);
        }
    }
    IEnumerator EnemyChasePlayer()
    {
        yield return new WaitForSeconds(2f);
        if (Vector3.Distance(transform.position, playerPos.position) >= minDist)
        {
            rb.AddForce((playerPos.position - transform.position).normalized * moveSpeed);
            StartCoroutine(EnemyChasePlayer());
        }
    }
}//CLASS















