using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRound : MonoBehaviour
{
    public float manaCost;
    void Start()
    {
        
    }
    //private void OnEnable()
    //{
    //    StartCoroutine(WaitForDeactivate());
    //}
    void Update()
    {
        
    }

    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}//CLASS












