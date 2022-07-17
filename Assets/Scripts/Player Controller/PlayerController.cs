using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementForce;
    public float jumpForce;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;
    public LayerMask whatIsGround;

    [Header("Camera Follow")]
    public Camera cam;
    [Range(0f, 1f)] public float interpolation = 0.1f;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    [Header("Animation")]
    public Animator anim;
    public Transform head;

    private Rigidbody2D rb;

    private void Start() 
    { 
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    private void Update()
    {
        //print(rb.velocity.magnitude);
    }
    private void FixedUpdate() 
    {
        CameraFollow();
    }

    private bool IsGrounded() 
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, whatIsGround);
        return hit.collider != null;
    }

    private void CameraFollow() 
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + offset, interpolation);
    }
}