using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Enemy Spawner Spawner")]
    [SerializeField] private float spawnEnemyRadius;
    [SerializeField] private float timeSpawnEnemySpawner;
    private int enemySpawnerCounter;
    [Header("Ammo Spawner")]
    [SerializeField] private float spawnAmmoRadius;
    [SerializeField] private float timeSpawnAmmo;
    [Header("Energy Drink Spawner")]
    [SerializeField] private float spawnEnergyDrinkRadius;
    [SerializeField] private float timeSpawnEneryDrink;
    [Header("Weapon Spawner")]
    [SerializeField] private float spawnWeaponRadius;
    [SerializeField] private float timeSpawnWeapon;
    [Header("Upgrade")]
    [SerializeField] private float spawnUpgradeHealthManaRadius;
    [SerializeField] private float timeSpawnUpgradeHealthMana;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] private bool canSpawnObject;
    [SerializeField] private GameplayController gameplayController;
    private void Awake()
    {
        canSpawnObject = true;
        //StartCoroutine(SpawnEnemySpawner());
        //StartCoroutine(SpawnEnergyDrink());
        //StartCoroutine(SpawnWeaponDropItem());
        //StartCoroutine(SpawnUpgrade());
    }
    void Start()
    {
        //canSpawnObject = true;
    }
    void Update()
    {
        //if (!GameplayController.instance.isPausing && canSpawnObject)
        //{
        //    StartCoroutine(SpawnEnemySpawner());
        //    StartCoroutine(SpawnEnergyDrink());
        //    StartCoroutine(SpawnWeaponDropItem());
        //    StartCoroutine(SpawnUpgrade());
        //    canSpawnObject = false;
        //}
    }
    private void OnEnable()
    {
        StartCoroutine(SpawnEnemySpawner());
        StartCoroutine(SpawnEnergyDrink());
        StartCoroutine(SpawnWeaponDropItem());
        StartCoroutine(SpawnUpgrade());
    }
    IEnumerator SpawnEnemySpawner()
    {
        if (enemySpawnerCounter <= 8)
        {
            yield return new WaitForSeconds((float)(timeSpawnEnemySpawner / gameplayController.difficulty));
            int randomPos = Random.Range(0, spawnPosition.Length);
            SmartPool.instance.SpawnObjectFromPool("Enemy Spawner Spawn Effect FX", spawnPosition[randomPos].position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);

            SmartPool.instance.SpawnObjectFromPool("Enemy Spawner", spawnPosition[randomPos].position, Quaternion.identity);
            GameplayController.instance.enemySpawnerCounter++;
            StartCoroutine(SpawnEnemySpawner());
        }
    }
    IEnumerator SpawnWeaponDropItem()
    {
        yield return new WaitForSeconds(timeSpawnWeapon);
        //Vector2 spawnPos = GameObject.Find(NameTag.PLAYER_HIPS).transform.position;
        //spawnPos += Random.insideUnitCircle.normalized * spawnWeaponRadius;
        int randomPos = Random.Range(0, spawnPosition.Length);

        int random = Random.Range(0, GameplayController.instance.weaponList.Count);
        SmartPool.instance.SpawnObjectFromPool(GameplayController.instance.weaponList[random].WeaponName, spawnPosition[randomPos].position, Quaternion.identity);

        StartCoroutine(SpawnWeaponDropItem());
    }
    IEnumerator SpawnAmmo()
    {
        yield return new WaitForSeconds(timeSpawnAmmo);
        //Vector2 spawnPos = GameObject.Find(NameTag.PLAYER_HIPS).transform.position;
        //spawnPos += Random.insideUnitCircle.normalized * spawnAmmoRadius;
        int randomPos = Random.Range(0, spawnPosition.Length);

        SmartPool.instance.SpawnObjectFromPool("Ammo", spawnPosition[randomPos].position, Quaternion.identity);

         
        StartCoroutine(SpawnAmmo());
    }

    IEnumerator SpawnEnergyDrink()
    {
        yield return new WaitForSeconds(timeSpawnEneryDrink);
        Vector2 spawnPos = GameObject.Find(NameTag.PLAYER_HIPS).transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnEnergyDrinkRadius;
        int randomPos = Random.Range(0, spawnPosition.Length);
        SmartPool.instance.SpawnObjectFromPool("Energy Drink", spawnPosition[randomPos].position, Quaternion.identity);
   
        StartCoroutine(SpawnEnergyDrink());
    }
    IEnumerator SpawnUpgrade()
    {
        yield return new WaitForSeconds(timeSpawnUpgradeHealthMana);
        Vector2 spawnPos = GameObject.Find(NameTag.PLAYER_HIPS).transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnUpgradeHealthManaRadius;
        int randomPos = Random.Range(0, spawnPosition.Length);
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            SmartPool.instance.SpawnObjectFromPool("Mana Upgrade", spawnPosition[randomPos].position, Quaternion.identity);
            print("SpawnMana");
        }
        else
        {
            SmartPool.instance.SpawnObjectFromPool("Health Upgrade", spawnPosition[randomPos].position, Quaternion.identity);
            print("SpawnHealth");
        }

        StartCoroutine(SpawnUpgrade());
    }
}//CLASS













