using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    private void OnEnable()
    {
        //StartCoroutine(WaitForDeactivate());
    }
    void Start()
    {
        StartCoroutine(WaitForDeactivate());
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
