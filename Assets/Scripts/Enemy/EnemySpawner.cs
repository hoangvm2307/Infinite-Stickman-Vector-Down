using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySpawner : MonoBehaviour, IDamageable
{
    [Header("Enemy Spawner")]
    [SerializeField] private float spawnEnemyRadius;
    [SerializeField] private float timeSpawnEnemy;
    [SerializeField] private GameObject spawnEffect;

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
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private AudioSource explosionSource;
    private void Awake()
    {
        isAlive = true;

        foreach (SpriteRenderer sr in spriteRenderes)
        {
            matDefault = sr.material;
        }
    }
    void Start()
    {
        //isAlive = true;

        //foreach (SpriteRenderer sr in spriteRenderes)
        //{
        //    matDefault = sr.material;
        //}
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);
        //Vector2 spawnPos = transform.position;
         
        for (int i = 0; i < 4; i++)
        {
            //spawnPos += UnityEngine.Random.insideUnitCircle.normalized * spawnEnemyRadius;
            int randomPos = UnityEngine.Random.Range(0, spawnPos.Length);
            SmartPool.instance.SpawnObjectFromPool("Enemy Spawn Effect", spawnPos[randomPos].position, Quaternion.identity);
            int randSpawnTime = UnityEngine.Random.Range(1, 3);
            yield return new WaitForSeconds(randSpawnTime); 
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
                SmartPool.instance.SpawnObjectFromPool("Sniper Enemy", spawnPos[randomPos].position, Quaternion.identity);
            }
            else
            {
                SmartPool.instance.SpawnObjectFromPool("Melee Enemy", spawnPos[randomPos].position, Quaternion.identity);
            }
        }

        StartCoroutine(SpawnEnemy());
    }
    void OnEnable()
    {
        isAlive = true;
        health = 500; 
        StartCoroutine(SpawnEnemy());
    }
    public void Damage(int damage)
    {
        if (isAlive)
        {
            explosionSource.Play();
            health -= damage;
            foreach (SpriteRenderer sr in spriteRenderes)
            {
                sr.material = matWhite;
            }
            if (health <= 0)
            {
                print("Dead");
                coinData.IncreaseCoin(bonus);
                health = 0;
                isAlive = false;
                OnEnemyDeath?.Invoke(); //Ripple Effect 
                StartCoroutine(WaitForDeactivate());
                Invoke("ResetMaterial", 0.1f);
                GameplayController.instance.enemySpawnerCounter--;
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
        }
        SmartPool.instance.SpawnObjectFromPool("Enemy Spawner Blood FX", enemyHips.position, Quaternion.identity);
    }
    void ResetMaterial()
    {
        foreach (SpriteRenderer sr in spriteRenderes)
        {
            sr.material = matDefault;
        }
    }
    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(2.4f);
        gameObject.SetActive(false);
    }
}
