using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForDeactivate());
    }
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}//CLASS

