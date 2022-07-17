using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForDeactivate());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(7f);
        gameObject.SetActive(false);
    }
}
