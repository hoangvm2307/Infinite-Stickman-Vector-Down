using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [Range(0, 1)] public int isLeftOrRight;
    public float speed = 300f;
    public Camera cam;
    private Vector3 mousePos;
    [SerializeField] private Transform hipsPos;
    private Rigidbody2D rb;
    private bool isPlayer;
    private PlayerHealth playerHealth;

    public Joystick joystick;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hipsPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;
        cam = Camera.main;
        isPlayer = transform.root.CompareTag("Player");
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    private void Start() 
    {
        //rb = GetComponent<Rigidbody2D>();
        //hipsPos = GameObject.Find(NameTag.PLAYER_HIPS).transform;
        //cam = Camera.main;
        //isPlayer = transform.root.CompareTag("Player");
        //playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        //if (transform.root.CompareTag("Player") && playerHealth.isPlayerAlive)
        //{
        //    mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        //    Vector3 difference = mousePos - transform.position;
        //    float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
        //    rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.deltaTime));
        //}
        if (transform.root.CompareTag("Player") && playerHealth.isPlayerAlive)
        {
            mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0f);
            Vector3 moveVector = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical);
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                float rotationZ = Mathf.Atan2(moveVector.x, -moveVector.y) * Mathf.Rad2Deg;
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ - 90, speed * Time.deltaTime));
            }
        }
        //if (Input.touchCount > 0)
        //{
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        Touch touch = Input.GetTouch(i);
        //        if (touch.phase == TouchPhase.Began)
        //        {
        //            if (touch.position.x > Screen.width / 2)
        //            {
        //                GameplayController.instance.ShootBullet();
        //            }
        //        }
        //        else if (touch.phase == TouchPhase.Moved)
        //        {
        //            if (transform.root.CompareTag("Player") && playerHealth.isPlayerAlive)
        //            {
        //                mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        //                Vector3 moveVector = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical);
        //                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        //                {
        //                    float rotationZ = Mathf.Atan2(moveVector.x, -moveVector.y) * Mathf.Rad2Deg;
        //                    rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ - 90, speed * Time.deltaTime));
        //                }
        //            }
        //        }
        //    }
        //}
    }
}