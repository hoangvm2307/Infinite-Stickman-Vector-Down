using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] AbilityController playerAbility;
    [SerializeField] GameObject abilityPanel;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameObject objectSpawner;
    [SerializeField] private GameObject firstEnemySpawner;
    private void Awake()
    {
        // playerAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>();
        //gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }
    public void ChooseForceField()
    {
        playerAbility.ability = Ability.FORCEFIELD;
        abilityPanel.SetActive(false);
        GameplayController.instance.Refresh();
        audioManager.backgroundMusicSource.Play();
        gameplayController.isPausing = false;
        spawnEnemyAndObjectSpawner();
    }
    public void ChooseEightSwords()
    {
        playerAbility.ability = Ability.SWORDAROUND;
        abilityPanel.SetActive(false);
        GameplayController.instance.Refresh();
        audioManager.backgroundMusicSource.Play();
        spawnEnemyAndObjectSpawner();
        gameplayController.isPausing = false;
    }
    public void ChooseEnergyShield()
    {
        playerAbility.ability = Ability.ENERGYSHIELD;
        abilityPanel.SetActive(false);
        GameplayController.instance.Refresh();
        audioManager.backgroundMusicSource.Play();
        spawnEnemyAndObjectSpawner();
        gameplayController.isPausing = false;
    }
    public void ChooseTimeControl()
    {
        playerAbility.ability = Ability.SLOWDOWN;
        abilityPanel.SetActive(false);
        GameplayController.instance.Refresh();
        audioManager.backgroundMusicSource.Play();
        spawnEnemyAndObjectSpawner();
        gameplayController.isPausing = false;
    }
    void spawnEnemyAndObjectSpawner()
    {
        firstEnemySpawner.SetActive(true);
        objectSpawner.SetActive(true);
    }
}
