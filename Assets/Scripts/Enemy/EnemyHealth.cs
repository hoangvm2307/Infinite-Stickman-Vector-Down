using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private SpriteRenderer[] spriteRenderes;
    [SerializeField] private Material matWhite;
    private Material matDefault;
    [SerializeField] private Transform enemyHips;
    [SerializeField] CoinData coinData;
    [SerializeField] private int bonus;
    public static event Action OnEnemyDeath;
    public bool isAlive;
    private bool canSpawnCoin;
    [SerializeField] private AudioSource hurtSource;
    void Awake()
    {
        isAlive = true;

        foreach (SpriteRenderer sr in spriteRenderes)
        {
            matDefault = sr.material;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            spriteRenderes[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    void Start()
    {
        //isAlive = true;
         
        //foreach(SpriteRenderer sr in spriteRenderes)
        //{
        //    matDefault = sr.material;
        //}
        //for(int i = 0; i < transform.childCount; i++)
        //{
        //    spriteRenderes[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        //}
    }
    void Update()
    {
    }
    void OnEnable()
    {
        health = 100;
    }
    public void Damage(int damage)
    { 
        if (isAlive)
        {
            hurtSource.Play();
            health -= damage;           
            foreach (SpriteRenderer sr in spriteRenderes)
            {
                sr.material = matWhite;
            }
            if (health <= 0)
            {
                coinData.IncreaseCoin(bonus);
                health = 0;
                isAlive = false;
                OnEnemyDeath?.Invoke(); //Ripple Effect 
                StartCoroutine(WaitForDeactivate());
                Invoke("ResetMaterial", 0.1f);
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
        SmartPool.instance.SpawnObjectFromPool("Enemy Blood FX", enemyHips.position, Quaternion.identity);
    }                                                                                   
    void ResetMaterial()
    {
        foreach(SpriteRenderer sr in spriteRenderes)
        {
            sr.material = matDefault;
        }
    }
    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
