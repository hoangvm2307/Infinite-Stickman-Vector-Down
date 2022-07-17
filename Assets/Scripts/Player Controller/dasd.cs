using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dasd : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Melee Enemy") && collision.CompareTag("Ranged Enemy"))
    //    {
    //        print("Touched Enemy");
    //        playerCollider.enabled = false;
    //        StartCoroutine(ReturnNormal());
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Melee Enemy") && collision.gameObject.CompareTag("Ranged Enemy"))
        {
            print("Touched Enemy");
            playerCollider.enabled = false;
            StartCoroutine(ReturnNormal());
        }
    }
    IEnumerator ReturnNormal()
    {
        yield return new WaitForSeconds(1.5f);
        playerCollider.enabled = true;
    }
}
