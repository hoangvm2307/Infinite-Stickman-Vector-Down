using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArm : MonoBehaviour
{
    [Range(0, 1)] public int isLeftOrRight;
    public float speed = 300f;
    [SerializeField] private Transform hipsPos;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hipsPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //hipsPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;
        //enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        Vector3 difference = hipsPos.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.deltaTime));
    }
}
