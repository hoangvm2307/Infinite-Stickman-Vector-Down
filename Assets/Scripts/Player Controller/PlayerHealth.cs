using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private GameObject bloodFX;
    [SerializeField] private Transform playerHips;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private CoinData coinData;
    private float hpCheckTime = 0.75f;
    private int tempHP;
    public bool isPlayerAlive;
    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private GameplayController gameplayController;
    private void Awake()
    {
        isPlayerAlive = true;
        gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }
    void Start()
    {
        //isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        ConstraintDamageReceive();
    }
    public void Damage(int damage)
    {
        //hurtSource.Play();
        healthManager.DecreaseHealth(damage);
        SmartPool.instance.SpawnObjectFromPool("Blood FX", playerHips.position, Quaternion.identity);
        if(healthManager.health <= 0)
        {
            healthManager.health = 0;
            gameplayController.gameOver = true;
            StartCoroutine(gameplayController.OpenGameOverPanel());
            isPlayerAlive = false;
        }
    }
    public void DecreaseMana(int mana)
    {
        
    }
    public void ConstraintDamageReceive()
    {
        hpCheckTime -= Time.deltaTime;
        if (hpCheckTime <= 0)
        {
            tempHP = healthManager.health;
            healthManager.tempHealth = healthManager.health;
            hpCheckTime = 0.75f;
        }
         
    }
}//CLASS





















